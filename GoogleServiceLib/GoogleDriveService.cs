using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleServiceLib
{
    public class GoogleDriveService : IGoogleDriveService
    {




        private string GetIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
           
            if (url.Contains("id="))
            {
                var parts = url.Split(new[] { "id=" }, StringSplitOptions.None);
                return parts[1].Split('&')[0];
            }

            if (url.Contains("/d/"))
            {
                var parts = url.Split(new[] { "/d/" }, StringSplitOptions.None);
                return parts[1].Split('/')[0];
            }

            return null;
        }

        private async Task<DriveService> GetServiceAsync()
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets { ClientId = _clientId, ClientSecret = _clientSecret },
                Scopes = new[] { DriveService.Scope.Drive }
            });

            var tokenResponse = new TokenResponse { RefreshToken = _refreshToken };
            var credential = new UserCredential(flow, "user", tokenResponse);

            if (credential.Token.IsExpired(Google.Apis.Util.SystemClock.Default))
            {
                await credential.RefreshTokenAsync(CancellationToken.None);
            }

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ITeam"
            });
        }

        public async Task<bool> MoveToTrashAsync(string fileOrFolderId)
        {
            try
            {
                var service = await GetServiceAsync();

                var updateFile = new Google.Apis.Drive.v3.Data.File { Trashed = true };
                
                await service.Files.Update(updateFile, fileOrFolderId).ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi chuyển vào thùng rác: {ex.Message}");
                return false;
            }
        }

        public async Task<DriveNode> GetDriveTreeAsync(string rootFolderId = null)
        {
            var service = await GetServiceAsync();
            string targetId = rootFolderId ?? _folderId;

            var rootRequest = service.Files.Get(targetId);
            rootRequest.Fields = "id, name, mimeType, createdTime";
            var rootFile = await rootRequest.ExecuteAsync();

            var rootNode = new DriveNode
            {
                Id = rootFile.Id,
                Name = rootFile.Name,
                Type = "folder",
                MimeType = rootFile.MimeType,
                CreatedTime = rootFile.CreatedTime
            };

  
            await BuildTreeRecursive(service, rootNode);

            return rootNode;
        }

        private async Task BuildTreeRecursive(DriveService service, DriveNode parentNode)
        {
            var request = service.Files.List();
      
            request.Q = $"'{parentNode.Id}' in parents and trashed = false";
            request.Fields = "files(id, name, size, mimeType, webViewLink, webContentLink, createdTime)";

            var result = await request.ExecuteAsync();

            foreach (var file in result.Files)
            {
                var childNode = new DriveNode
                {
                    Id = file.Id,
                    Name = file.Name,
                    MimeType = file.MimeType,
                    Link = file.WebViewLink,
                    LinkDownload = file.WebContentLink,
                    CreatedTime = file.CreatedTime,
                
                    SizeMb = file.Size.HasValue ? Math.Round((double)file.Size.Value / (1024 * 1024), 2) : 0
                };

                if (file.MimeType == "application/vnd.google-apps.folder")
                {
                    childNode.Type = "folder";
                 
                    await BuildTreeRecursive(service, childNode);
                }
                else
                {
                    childNode.Type = "file";
                }

                parentNode.Children.Add(childNode);
            }
        }

        public async Task<List<UploadResult>> GetAllFilesAsync()
        {
            var service = await GetServiceAsync();
            var request = service.Files.List();
            request.Q = $"'{_folderId}' in parents and trashed = false";
            request.Fields = "files(id, name, size, webContentLink, webViewLink)";

            var result = await request.ExecuteAsync();

            return result.Files.Select(file => new UploadResult
            {
                FileName = file.Name,
                Link = file.WebViewLink,
                LinkDownload = file.WebContentLink,
                Size = file.Size,
                SizeMb = Math.Round((double)(file.Size ?? 0) / (1024 * 1024), 2)
            }).ToList();
        }

        public async Task<UploadResult> UploadFileAsync(Stream fileStream, string fileName)
        {
            var service = await GetServiceAsync();

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { _folderId }
            };

            var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
            request.Fields = "id, name, webContentLink, webViewLink, size";

            var progress = await request.UploadAsync();
            if (progress.Status == Google.Apis.Upload.UploadStatus.Failed)
                throw new Exception(progress.Exception.Message);

            var file = request.ResponseBody;

            var permission = new Google.Apis.Drive.v3.Data.Permission
            {
                Role = "reader",
                Type = "anyone"
            };
            await service.Permissions.Create(permission, file.Id).ExecuteAsync();

            double mbSize = 0;
            if (file.Size.HasValue)
            {
                mbSize = Math.Round((double)file.Size.Value / (1024 * 1024), 2);
            }

            return new UploadResult { Link = file.WebViewLink, LinkDownload = file.WebContentLink, Size = file.Size, SizeMb = mbSize,FileName = file.Name };
        }

        public async Task<bool> RenameNodeAsync(string id, string newName)
        {
            try
            {
                var service = await GetServiceAsync();

                var updateFile = new Google.Apis.Drive.v3.Data.File
                {
                    Name = newName
                };

                var request = service.Files.Update(updateFile, id);
                await request.ExecuteAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đổi tên: {ex.Message}");
                return false;
            }
        }

        private async Task<string> GetOrCreateFolderPathAsync(DriveService service, string subPath)
        {
            var folders = subPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string currentParentId = _folderId;

            foreach (var folderName in folders)
            {
                var listRequest = service.Files.List();
                listRequest.Q = $"name = '{folderName}' and mimeType = 'application/vnd.google-apps.folder' and '{currentParentId}' in parents and trashed = false";
                listRequest.Fields = "files(id)";

                var result = await listRequest.ExecuteAsync();
                var folder = result.Files.FirstOrDefault();

                if (folder != null)
                {
                    currentParentId = folder.Id;
                }
                else
                {
                    var folderMetadata = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = folderName,
                        MimeType = "application/vnd.google-apps.folder",
                        Parents = new List<string> { currentParentId }
                    };

                    var newFolder = await service.Files.Create(folderMetadata).ExecuteAsync();
                    currentParentId = newFolder.Id;
                }
            }

            return currentParentId; 
        }

        public async Task<UploadResult> UploadFileWithFolderPathAsync(Stream fileStream, string fileName, string subPath = null)
        {
            var service = await GetServiceAsync();

          
            string targetFolderId = string.IsNullOrEmpty(subPath)
                                    ? _folderId
                                    : await GetOrCreateFolderPathAsync(service, subPath);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { targetFolderId }
            };

            var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
            request.Fields = "id, name, webContentLink, webViewLink, size";

            var progress = await request.UploadAsync();
            if (progress.Status == Google.Apis.Upload.UploadStatus.Failed)
                throw new Exception(progress.Exception.Message);

            var file = request.ResponseBody;

            var permission = new Google.Apis.Drive.v3.Data.Permission { Role = "reader", Type = "anyone" };
            await service.Permissions.Create(permission, file.Id).ExecuteAsync();

            return new UploadResult
            {
                FileName = file.Name, 
                Link = file.WebViewLink,
                LinkDownload = file.WebContentLink,
                Size = file.Size,
                SizeMb = Math.Round((double)(file.Size ?? 0) / (1024 * 1024), 2)
            };
        }

        public async Task<bool> DeleteFileAsync(string fileLink)
        {
            try
            {
                var service = await GetServiceAsync();
                var fileId = GetIdFromUrl(fileLink);

                if (string.IsNullOrEmpty(fileId))
                {
                    throw new Exception("Không tìm thấy ID hợp lệ từ link");
                }

                await service.Files.Delete(fileId).ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa file: " + ex.Message);
                return false;
            }
        }

        public async Task<Google.Apis.Drive.v3.Data.File> GetFileMetadataAsync(string fileLink)
        {
            try
            {
                var service = await GetServiceAsync();
                var fileId = GetIdFromUrl(fileLink); 

                if (string.IsNullOrEmpty(fileId)) return null;

                var request = service.Files.Get(fileId);
                request.Fields = "id, name, size, mimeType, webViewLink, trashed";

                var file = await request.ExecuteAsync();

                if (file.Trashed.HasValue && file.Trashed.Value)
                {
                    return null;
                }

                return file;
            }           
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<VirusScanReport> ScanFileByIdAsync(string fileId)
        {
            var driveService = await GetServiceAsync();
            using var stream = new MemoryStream();
            var request = driveService.Files.Get(fileId);
            await request.DownloadAsync(stream);
            stream.Position = 0;

            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(stream);
            string fileHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            stream.Position = 0; 

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "1e601b33a6910f8c5f6a644cf154040d7692cd65596ba3556358af2782b335c1");

            var response = await client.GetAsync($"https://www.virustotal.com/api/v3/files/{fileHash}");

            if (response.IsSuccessStatusCode)
            {
                return ParseVtReport(await response.Content.ReadAsStringAsync(), fileHash);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(stream);
                content.Add(fileContent, "file", "game_patch.abc");

                var uploadResponse = await client.PostAsync("https://www.virustotal.com/api/v3/files", content);

                if (uploadResponse.IsSuccessStatusCode)
                {
                    var uploadJson = await uploadResponse.Content.ReadAsStringAsync();
                    dynamic result = JObject.Parse(uploadJson);
                    string analysisId = result.data.id; 

                    return new VirusScanReport
                    {
                        Status = "Đang phân tích (Queued)",
                        DetailedUrl = $"https://www.virustotal.com/gui/file/{fileHash}"
                    };
                }
            }

            return new VirusScanReport { Status = "Lỗi hệ thống khi quét" };
        }

        private VirusScanReport ParseVtReport(string jsonContent, string fileHash)
        {
            dynamic json = JObject.Parse(jsonContent);
            var stats = json.data.attributes.last_analysis_stats;
            return new VirusScanReport
            {
                Malicious = (int)stats.malicious,
                Harmless = (int)stats.harmless,
                Undetected = (int)stats.undetected,
                DetailedUrl = $"https://www.virustotal.com/gui/file/{fileHash}",
                Status = (int)stats.malicious > 0 ? "Malicious" : "Clean"
            };
        }

        public async Task DownloadFileAsync(string fileUrl, string folderPath, IProgress<double> progress)
        {
            var service = await GetServiceAsync(); 
            var fileId = GetIdFromUrl(fileUrl);

            if (string.IsNullOrEmpty(fileId)) return;

            var requestMetadata = service.Files.Get(fileId);
            requestMetadata.Fields = "name, size";
            var file = await requestMetadata.ExecuteAsync();

            string filePath = Path.Combine(folderPath, file.Name);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                var downloadRequest = service.Files.Get(fileId);

                downloadRequest.MediaDownloader.ProgressChanged += (p) =>
                {
                    if (p.Status == Google.Apis.Download.DownloadStatus.Downloading)
                    {
                        double pct = (double)p.BytesDownloaded / (file.Size ?? 1) * 100;
                        progress.Report(Math.Round(pct, 1));
                    }
                };

                await downloadRequest.DownloadAsync(fileStream);
            }
        }

        public async Task<List<Google.Apis.Drive.v3.Data.File>> FindFilesByChecksumAsync(string targetMd5)
        {
            var service = await GetServiceAsync();
            var foundFiles = new List<Google.Apis.Drive.v3.Data.File>();

            var request = service.Files.List();
            request.Q = "trashed = false";
     
            request.Fields = "files(id, name, md5Checksum, size)";
            request.PageSize = 100;

            do
            {
                var result = await request.ExecuteAsync();
                var matches = result.Files.Where(f => f.Md5Checksum != null &&
                                                      f.Md5Checksum.Equals(targetMd5, StringComparison.OrdinalIgnoreCase));

                foundFiles.AddRange(matches);
                request.PageToken = result.NextPageToken;

            } while (request.PageToken != null);

            return foundFiles;
        }
    }

  
}