using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading; // Cần thêm cái này cho CancellationToken
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
    }

  
}