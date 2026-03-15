using GoogleServiceLib;
using PatchSeller.Launcher.DTOs;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchSeller.Launcher.ActionForms
{
    public partial class PatchDetail : Form
    {
        string documentStorage;
        string pathToGameFolder;

        List<PatchVersionDetailDTO> listVersion;
        int currentVersionId;
        PatchVersionDetailDTO currentVersionDetail;
        string _token;
        int _userId;
        public PatchDetail(int patchId, int userId, string token, UserPurchasePatchResponse currentPatch)
        {
            InitializeComponent();
            this.Text = "Chi tiết gói " + currentPatch.PatchName;
            lblGameName.Text = currentPatch.GameName;
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string finalPath = Path.Combine(myDocumentsPath, "ITeam");
            documentStorage = finalPath;
            _token = token;
            _userId = userId;

            _ = LoadData(patchId);
        }
        string pathFileDownloaded;

        public async Task DownloadFileWithProgressAsync(string url, string folderPath, IProgress<double> progress)
        {
            using HttpClient client = new HttpClient();

            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            string fileName = string.Empty;

            try
            {
                using var metaClient = new HttpClient();
                metaClient.BaseAddress = new Uri("https://localhost:7226");

                var encodedUrl = Uri.EscapeDataString(url);
                var metaResponse = await metaClient.GetAsync($"/WeatherForecast/check-file?url={encodedUrl}");

                if (metaResponse.IsSuccessStatusCode)
                {
                    var metaJson = await metaResponse.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(metaJson);
                    if (doc.RootElement.TryGetProperty("name", out var nameProp))
                    {
                        fileName = nameProp.GetString() ?? string.Empty;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lỗi khi lấy thông tin tệp");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Lỗi khi tiếp cận tệp");
                return;
            }

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);

            var driveService = new GoogleDriveService();
            await driveService.DownloadFileAsync(url, folderPath, progress);
            pathFileDownloaded = filePath;            
        }

        private async Task<string?> GetURLDownload(int patchVersionId)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7226");

                if (!string.IsNullOrEmpty(_token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                }

                var response = await client.GetAsync($"/patch-version/download/{patchVersionId}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không lấy được link tải phiên bản hiện tại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                var url = await response.Content.ReadAsStringAsync();

                return url.Trim().Trim('"');
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy link tải: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async Task LoadData(int patchId)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7226");

                var response = await client.GetAsync($"/patch-version/get-by-patch-id/{patchId}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không lấy được danh sách phiên bản patch.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var listPatchVer = JsonSerializer.Deserialize<List<PatchVersionDetailDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<PatchVersionDetailDTO>();

                listVersion = listPatchVer;


                int stt = 1;
                dtgListVersion.ColumnCount = 6;
                dtgListVersion.Columns[0].Name = "STT";
                dtgListVersion.Columns[1].Name = "ID";
                dtgListVersion.Columns[2].Name = "Tiêu đề";
                dtgListVersion.Columns[3].Name = "Phiên bản game tương thích";
                dtgListVersion.Columns[4].Name = "Dung lượng";
                dtgListVersion.Columns[5].Name = "Ngày cập nhật";
                dtgListVersion.Columns[1].Visible = false;
                dtgListVersion.Rows.Clear();

                foreach (var item in listVersion)
                {
                    double mb = item.FileSize / 1024.0 / 1024.0;
                    dtgListVersion.Rows.Add(stt++, item.PatchVersionId, item.VersionName, item.WorkWithGameVersion, mb.ToString("0.##") + " MB", item.CreateAt);
                }

                dtgListVersion.CellClick += DtgListVersion_CellClick;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phiên bản patch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DtgListVersion_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                currentVersionId = (int)dtgListVersion.Rows[e.RowIndex].Cells[1].Value;
                currentVersionDetail = listVersion.FirstOrDefault(x => x.PatchVersionId == currentVersionId);
                lblCurrentVer.Text  = dtgListVersion.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch
            {

            }
        }

        public void CreateFolderInDocuments(string subFolderName)
        {

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string finalPath = Path.Combine(myDocumentsPath, subFolderName);

            try
            {
                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi tạo thư mục: {ex.Message}");
            }
        }



        private void bntDetail_Click(object sender, EventArgs e)
        {
            VersionDetail versionDetail = new VersionDetail(currentVersionDetail);
            versionDetail.ShowDialog();
        }

        public void ExtractFile(string zipPath, string extractPath, string? password = null)
        {
            var readerOptions = new ReaderOptions
            {
                Password = string.IsNullOrEmpty(password) ? null : password
            };

            using (FileStream fs = new FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var archive = ArchiveFactory.OpenArchive(fs, readerOptions))
                {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                    {
                        entry.WriteToDirectory(extractPath, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            } 
        }

        private async void bntDownload_Click(object sender, EventArgs e)
        {         



        }

        private async void bntSetup_Click(object sender, EventArgs e)
        {

            if (currentVersionDetail == null)
            {
                MessageBox.Show("Vui lòng chọn mục cần cài đặt tại bảng dưới");
                return;
            }

            if (! await CheckConditionToSetup(_userId))
            {

                MessageBox.Show("Không đủ điều kiện để cài đặt");
                return;
            }  

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.UseDescriptionForTitle = true; 
                fbd.Description = "Chọn thư mục cài Patch Game";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    pathToGameFolder = fbd.SelectedPath;
                }
            }

            if(pathToGameFolder == null)
            {
                MessageBox.Show("Hãy chọn thư mục game để cài");
                return;
            }    

            lblStatus.Visible = true;
            progressBar1.Visible = true;
            bntSetup.Enabled = false;
            progressBar1.Value = 0;
            var progressIndicator = new Progress<double>(val =>
            {
                progressBar1.Value = (int)val;
                lblStatus.Text = $"Đang tải: {val:N1}%";
            });

            try
            {
                string url = await GetURLDownload(currentVersionId);
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ITeam");

                await DownloadFileWithProgressAsync(url, folder, progressIndicator);

                lblStatus.Text = "Tải xuống thành công, đang cài đặt";
                ExtractFile(pathFileDownloaded, pathToGameFolder, currentVersionDetail.ExtractionPassword);
                MessageBox.Show("Cài đặt thành công");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                lblStatus.Text = "Lỗi khi tải file.";
            }
            finally
            {
                bntSetup.Enabled = true;
                lblStatus.Visible = false;
                progressBar1.Visible = false;
            }

        }

        public string GetSerialNumber()
        {
            string serial = string.Empty;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_Bios");

                foreach (ManagementObject obj in searcher.Get())
                {
                    serial = obj["SerialNumber"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                serial = "Không lấy được " + ex.Message;
            }

            return string.IsNullOrEmpty(serial) ? "N/A" : serial;
        }

        private async Task<bool> CheckConditionToSetup(int userId)
        {
            try
            {             

                string serialNum = GetSerialNumber();
                int patchId = currentVersionDetail.PatchId;

                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7226");

                if (!string.IsNullOrEmpty(_token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                }

                var url = $"/install-log/get-by-user-and-patch-id?userId={userId}&patchId={patchId}";

                InstallLog? ins = null;

                try
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        ins = await response.Content.ReadFromJsonAsync<InstallLog>();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ins = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                if (ins != null)
                {
                    if (ins.BIOSSerialNumber != GetSerialNumber() && string.IsNullOrEmpty(ins.BIOSSerialNumber) == false)
                    {
                        DialogResult result = MessageBox.Show("Tài khoản này đã cài đặt bản vá ở một máy khác có mã Serial là: " + ins.BIOSSerialNumber + "\nBạn có muốn ghi đè và sử dụng bản vá lên máy này không?",
                                      "Xác nhận",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            ins.BIOSSerialNumber = GetSerialNumber();
                            ins.InstallDate = DateTime.Now;
                            var urlUpdate = $"/install-log/update";
                            var responseUpdate = await client.PutAsJsonAsync(urlUpdate, ins);
                            if (!responseUpdate.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Lỗi khi cập nhật bản ghi cài đặt");
                                return false;
                            }
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (ins.BIOSSerialNumber == GetSerialNumber())
                        return true;
                }

               
                ins.UserId = _userId;
                ins.PatchId = patchId;
                ins.PatchVersionId = currentVersionId;
                ins.BIOSSerialNumber = GetSerialNumber();
                ins.InstallDate = DateTime.Now;
                var urlCreate = $"/install-log/create";
                var responseCreate = await client.PostAsJsonAsync(urlCreate, ins);
                if (!responseCreate.IsSuccessStatusCode)
                {
                    MessageBox.Show("Lỗi khi tạo bản ghi cài đặt");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra điều kiện cài đặt: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
