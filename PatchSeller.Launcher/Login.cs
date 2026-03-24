using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PatchSeller.Launcher
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            linkLabel1.Links.Add(0, linkLabel1.Text.Length, "http://localhost:5001/forgot-password");
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            var url = e.Link.LinkData.ToString();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private async void bntLogin_Click(object sender, EventArgs e)
        {
            //txtUserName.Text = "nguyenvana";
            //txtPassword.Text = "User@123456";
            string userName = txtUserName.Text?.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passwordHash = HashPassword(password);

            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7226");

                var payload = new
                {
                    username = userName,
                    passwordHash = passwordHash
                };

                var json = JsonSerializer.Serialize(payload);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/Access/LoginCustomer", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản/mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                {
                    MessageBox.Show("Phản hồi đăng nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var checkClient = new HttpClient();
                checkClient.BaseAddress = new Uri("https://localhost:7226");
                checkClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                var checkResponse = await checkClient.GetAsync("/Access/Check");
                if (!checkResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không lấy được thông tin người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var userInfoJson = await checkResponse.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfo>(userInfoJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Dashboard dashboard = new Dashboard(userInfo.Id, loginResponse.Token);
                dashboard.FormClosed += Dashboard_FormClosed;
                this.Hide();
                dashboard.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối tới server: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string HashPassword(string password)
        {
            //4297F44B13955235245B2497399D7A93 
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Clear();
            return sb.ToString();
        }

        private void Dashboard_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool LoginSuccess { get; set; }
        public bool FirstLogin { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RankId { get; set; }
        public int CartId { get; set; }
        public string? RankName { get; set; }
        public int RewardPoint { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public bool IsExpired { get; set; }
    }
}
