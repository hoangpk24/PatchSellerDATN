namespace PatchSeller.Launcher.ActionForms
{
    partial class PatchDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchDetail));
            bntSetup = new Button();
            dtgListVersion = new DataGridView();
            bntDetail = new Button();
            progressBar1 = new ProgressBar();
            lblStatus = new Label();
            label1 = new Label();
            label2 = new Label();
            lblCurrentVer = new Label();
            lblGameName = new Label();
            ((System.ComponentModel.ISupportInitialize)dtgListVersion).BeginInit();
            SuspendLayout();
            // 
            // bntSetup
            // 
            bntSetup.Location = new Point(713, 81);
            bntSetup.Name = "bntSetup";
            bntSetup.Size = new Size(75, 23);
            bntSetup.TabIndex = 6;
            bntSetup.Text = "Cài đặt";
            bntSetup.UseVisualStyleBackColor = true;
            bntSetup.Click += bntSetup_Click;
            // 
            // dtgListVersion
            // 
            dtgListVersion.AllowUserToAddRows = false;
            dtgListVersion.AllowUserToDeleteRows = false;
            dtgListVersion.AllowUserToResizeRows = false;
            dtgListVersion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgListVersion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgListVersion.Location = new Point(12, 110);
            dtgListVersion.MultiSelect = false;
            dtgListVersion.Name = "dtgListVersion";
            dtgListVersion.ReadOnly = true;
            dtgListVersion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgListVersion.ShowEditingIcon = false;
            dtgListVersion.Size = new Size(776, 328);
            dtgListVersion.TabIndex = 8;
            // 
            // bntDetail
            // 
            bntDetail.Location = new Point(551, 81);
            bntDetail.Name = "bntDetail";
            bntDetail.Size = new Size(75, 23);
            bntDetail.TabIndex = 9;
            bntDetail.Text = "Chi tiết";
            bntDetail.UseVisualStyleBackColor = true;
            bntDetail.Click += bntDetail_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(551, 52);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(237, 23);
            progressBar1.TabIndex = 10;
            progressBar1.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(551, 34);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 11;
            lblStatus.Text = "label1";
            lblStatus.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 12;
            label1.Text = "Game:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 13;
            label2.Text = "Phiên bản đang chọn:";
            // 
            // lblCurrentVer
            // 
            lblCurrentVer.AutoSize = true;
            lblCurrentVer.Location = new Point(141, 38);
            lblCurrentVer.Name = "lblCurrentVer";
            lblCurrentVer.Size = new Size(167, 15);
            lblCurrentVer.TabIndex = 14;
            lblCurrentVer.Text = "Đường dẫn tới thư mục game:";
            // 
            // lblGameName
            // 
            lblGameName.AutoSize = true;
            lblGameName.Location = new Point(59, 9);
            lblGameName.Name = "lblGameName";
            lblGameName.Size = new Size(167, 15);
            lblGameName.TabIndex = 15;
            lblGameName.Text = "Đường dẫn tới thư mục game:";
            // 
            // PatchDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblGameName);
            Controls.Add(lblCurrentVer);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(progressBar1);
            Controls.Add(bntDetail);
            Controls.Add(dtgListVersion);
            Controls.Add(bntSetup);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "PatchDetail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Detail";
            ((System.ComponentModel.ISupportInitialize)dtgListVersion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button bntSetup;
        private DataGridView dtgListVersion;
        private Button bntDetail;
        private ProgressBar progressBar1;
        private Label lblStatus;
        private Label label1;
        private Label label2;
        private Label lblCurrentVer;
        private Label lblGameName;
    }
}