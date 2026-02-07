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
     
        //Chèn đống secret vào đây

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
                ClientSecrets = new ClientSecrets { ClientId = _clientId, ClientSecret = _clientSecret }
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

        public async Task<UploadResult> UploadFileAsync(Stream fileStream, string fileName)
        {
            var service = await GetServiceAsync();

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
                Parents = new List<string> { _folderId }
            };

            var request = service.Files.Create(fileMetadata, fileStream, "application/octet-stream");
            request.Fields = "id, webContentLink, webViewLink, size";

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

            return new UploadResult { Link = file.WebViewLink, LinkDownload = file.WebContentLink, Size = file.Size, SizeMb = mbSize };
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