using PatchSeller.Launcher.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchSeller.Launcher.ActionForms
{
    public partial class VersionDetail : Form
    {
        PatchVersionDetailDTO _currentVer;
        public VersionDetail(PatchVersionDetailDTO currentVersion)
        {
            InitializeComponent();
            parentTabControl.TabPages.Clear();
            _currentVer = currentVersion;
            LoadData();
        }
        void LoadData()
        {
            double mb = _currentVer.FileSize / 1024.0 / 1024.0;
            TabPage tpInfo = new TabPage();
            tpInfo.Text = "Thông tin";
            RichTextBox rtbInfo = new RichTextBox();
            rtbInfo.Text ="Tên phiên bản: " + _currentVer.VersionName+"\n" + "Dung lượng: " +mb.ToString("0.##") + " MB";

            TabPage tpChangeLog = new TabPage();
            tpChangeLog.Text = "Nhật ký thay đổi";
            RichTextBox rtbChangeLog = new RichTextBox();
            rtbChangeLog.Text = _currentVer.Changelog;

            TabPage tpWorkWith = new TabPage();
            tpWorkWith.Text = "Tương thích";
            RichTextBox rtbWorkWith = new RichTextBox();
            rtbWorkWith.Text = _currentVer.WorkWithGameVersion;

            TabPage tpGuide = new TabPage();
            tpGuide.Text = "Cài thủ công";
            RichTextBox rtbGuide = new RichTextBox();
            rtbGuide.Text = _currentVer.InstallationGuide;

            rtbInfo.ReadOnly = true;
            rtbChangeLog.ReadOnly = true;
            rtbGuide.ReadOnly = true;
            rtbWorkWith.ReadOnly = true;

            tpInfo.Controls.Add(rtbInfo);
            tpChangeLog.Controls.Add(rtbChangeLog);
            tpGuide.Controls.Add(rtbGuide);
            tpWorkWith.Controls.Add(rtbWorkWith);

            parentTabControl.TabPages.Add(tpInfo);
            parentTabControl.TabPages.Add(tpChangeLog);
            parentTabControl.TabPages.Add(tpGuide);
            parentTabControl.TabPages.Add(tpWorkWith);
            parentTabControl.SelectedTab = tpInfo;

            rtbInfo.Dock = DockStyle.Fill;
            rtbChangeLog.Dock = DockStyle.Fill;
            rtbGuide.Dock = DockStyle.Fill;
            rtbWorkWith.Dock = DockStyle.Fill;

        }
    }
}
