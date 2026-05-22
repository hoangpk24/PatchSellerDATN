using GoogleServiceLib;
using PatchSeller.Launcher.ActionForms;
using PatchSeller.Launcher.DTOs;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchSeller.Launcher
{
    public partial class Dashboard : Form
    {
        private readonly int _userId;
        private readonly string? _token;
        private MeDetailResponse? _meDetail;

        UserPurchasePatchResponse currentPatch;

        private int curentPatchId = -1;

        private List<UserPurchasePatchResponse> userPurchasePatchResponses;

        public Dashboard(int userId, string? token)
        {
            InitializeComponent();
            lblSerialNumber.Text = GetSerialNumber();
            _userId = userId;
            _token = token;
            dtgListPatch.CellClick += DtgListPatch_CellClick;

            _ = LoadData(_userId);

        }

        private void DtgListPatch_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                curentPatchId = (int)dtgListPatch.Rows[e.RowIndex].Cells[1].Value;
                currentPatch = userPurchasePatchResponses.FirstOrDefault(x => x.PatchId == curentPatchId);

            }
            catch
            {

            }
        }

        private async Task LoadData(int userId)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7226");

                var response = await client.GetAsync($"/user/get-me-detail/{userId}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không lấy được thông tin tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var meDetail = JsonSerializer.Deserialize<MeDetailResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (meDetail == null)
                {
                    MessageBox.Show("Dữ liệu tài khoản không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _meDetail = meDetail;
                lblWelcome.Text = _meDetail.FullName;
                userPurchasePatchResponses = meDetail.PurchasedPatches;
                FillTable(userPurchasePatchResponses);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillTable(List<UserPurchasePatchResponse> listData)
        {
            if (listData != null)
            {

                int stt = 1;
                dtgListPatch.ColumnCount = 5;
                dtgListPatch.Columns[0].Name = "STT";
                dtgListPatch.Columns[1].Name = "ID";
                dtgListPatch.Columns[2].Name = "Tên Game";
                dtgListPatch.Columns[3].Name = "Tên bản vá";
                dtgListPatch.Columns[4].Name = "Ngày mua";
                dtgListPatch.Columns[1].Visible = false;
                dtgListPatch.Rows.Clear();

                foreach (var item in listData)
                {
                    dtgListPatch.Rows.Add(stt++, item.PatchId, item.GameName, item.PatchName, item.PurchasedAt);
                }

            }
            else
            {
                dtgListPatch.Rows.Clear();
            }

        }

        private async void bntReload_Click(object sender, EventArgs e)
        {
            await LoadData(_userId);
            FillTable(userPurchasePatchResponses);
            txtSeach.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetSerialNumber());
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (curentPatchId == -1)
            {
                MessageBox.Show("Bạn chưa chọn bản vá nào");
                return;

            }
            PatchDetail formPatchDetail = new PatchDetail(curentPatchId, _userId, _token, currentPatch);
            formPatchDetail.ShowDialog();
        }

        private void txtSeach_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSeach.Text.Length > 0)
                {
                    List<UserPurchasePatchResponse> newData = new List<UserPurchasePatchResponse>();
                    newData = userPurchasePatchResponses.Where(x => x.GameName.ToLower().Contains(txtSeach.Text.ToLower()) || x.PatchName.ToLower().Contains(txtSeach.Text.ToLower())).ToList();
                    FillTable(newData);
                }
                else
                {
                    FillTable(userPurchasePatchResponses);
                }


            }
            catch
            {

            }
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


        private async void bntBrowser_Click(object sender, EventArgs e)
        {
            var driveService = new GoogleDriveService();
            string pathToGameFolder;
            string pathToPatch;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn Patch Game để cài đặt";
                ofd.Filter = "Compressed Files (*.zip;*.rar;*.7z)|*.zip;*.rar;*.7z|All files (*.*)|*.*";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                ofd.Multiselect = false;
                ofd.CheckFileExists = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = ofd.FileName;
                    pathToPatch = selectedFilePath;

                    string md5Hash = await GetFileHashMD5Async(pathToPatch);
                    var foundFiles = await driveService.FindFilesByChecksumAsync(md5Hash);

                    if (foundFiles.Count > 0)
                    {
                        var firstFile = foundFiles[0];
                        string fileName = firstFile.Name;
                        string fileId = firstFile.Id;

                        MessageBox.Show("Xác minh file thành công, vui lòng chọn thư mục game để cài");

                        using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                        {
                            fbd.UseDescriptionForTitle = true;
                            fbd.Description = "Chọn thư mục game cần cài";

                            if (fbd.ShowDialog() == DialogResult.OK)
                            {
                                pathToGameFolder = fbd.SelectedPath;

                                //Get Patch Ver detail
                                using var client = new HttpClient();
                                client.BaseAddress = new Uri("https://localhost:7226");

                                var response = await client.GetAsync($"/patch-version/get-by-file-id/{fileId}");
                                if (!response.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Không lấy được thông tin chi tiết bản vá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                var json = await response.Content.ReadAsStringAsync();
                                var patchVer = JsonSerializer.Deserialize<PatchVersionDetailDTO>(json, new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                }) ?? new PatchVersionDetailDTO();
                               var isExsit = userPurchasePatchResponses.FirstOrDefault(x=>x.PatchId == patchVer.PatchId);
                                if(isExsit==null)
                                {
                                    MessageBox.Show("Tài khoản này chưa sở hữu Patch, vui lòng mua tại trang web");
                                    return;
                                }
                                else
                                {

                                    DialogResult result = MessageBox.Show("Xác nhận cài đặt bản" + patchVer.PatchName + "của game "+patchVer.GameName+"?",
                                     "Xác nhận",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {

                                        if(await CheckConditionToSetup(_userId,patchVer)==false)
                                        {
                                            MessageBox.Show("Không đủ điều kiện để cài đặt");
                                            return;
                                        }
                                        else
                                        {
                                           try
                                            {
                                                ExtractFile(pathToPatch, pathToGameFolder, patchVer.ExtractionPassword);
                                                MessageBox.Show("Cài đặt thành công");
                                            }
                                            catch
                                            {

                                            }
                                        }    

                                    }    


                                }    



                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tệp khớp trên máy chủ!");
                    }
                }
            }

        }


        private async Task<bool> CheckConditionToSetup(int userId, PatchVersionDetailDTO currentVersionDetail)
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

                InstallLog? ins =null;

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
                else
                {
                    InstallLog newIns = new InstallLog();
                    newIns.UserId = _userId;
                    newIns.PatchId = patchId;
                    newIns.PatchVersionId = currentVersionDetail.PatchVersionId;
                    newIns.BIOSSerialNumber = GetSerialNumber();
                    newIns.InstallDate = DateTime.Now;
                    var urlCreate = $"/install-log/create";
                    var responseCreate = await client.PostAsJsonAsync(urlCreate, newIns);
                    if (!responseCreate.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Lỗi khi tạo bản ghi cài đặt");
                        return false;
                    }
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



        public async Task<string> GetFileHashMD5Async(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var md5 = MD5.Create())
                {
                    byte[] hashBytes = await md5.ComputeHashAsync(stream);
                    return Convert.ToHexString(hashBytes).ToLower();
                }
            }
        }
    }
}
