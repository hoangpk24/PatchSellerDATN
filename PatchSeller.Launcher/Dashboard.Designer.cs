namespace PatchSeller.Launcher
{
    partial class Dashboard
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
            dtgListPatch = new DataGridView();
            lblWelcome = new Label();
            label2 = new Label();
            bntBrowser = new Button();
            txtSeach = new TextBox();
            bntDetail = new Button();
            bntReload = new Button();
            lblSerialNumber = new Label();
            label1 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dtgListPatch).BeginInit();
            SuspendLayout();
            // 
            // dtgListPatch
            // 
            dtgListPatch.AllowUserToAddRows = false;
            dtgListPatch.AllowUserToDeleteRows = false;
            dtgListPatch.AllowUserToResizeRows = false;
            dtgListPatch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgListPatch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgListPatch.Location = new Point(12, 110);
            dtgListPatch.MultiSelect = false;
            dtgListPatch.Name = "dtgListPatch";
            dtgListPatch.ReadOnly = true;
            dtgListPatch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgListPatch.ShowEditingIcon = false;
            dtgListPatch.Size = new Size(776, 328);
            dtgListPatch.TabIndex = 0;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(79, 9);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(151, 15);
            lblWelcome.TabIndex = 2;
            lblWelcome.Text = "Chào mừng, Nguyễn Văn A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(260, 28);
            label2.Name = "label2";
            label2.Size = new Size(331, 32);
            label2.TabIndex = 3;
            label2.Text = "Danh sách các bản vá đã mua";
            // 
            // bntBrowser
            // 
            bntBrowser.Location = new Point(713, 81);
            bntBrowser.Name = "bntBrowser";
            bntBrowser.Size = new Size(75, 23);
            bntBrowser.TabIndex = 4;
            bntBrowser.Text = "Duyệt";
            bntBrowser.UseVisualStyleBackColor = true;
            bntBrowser.Click += bntBrowser_Click;
            // 
            // txtSeach
            // 
            txtSeach.Location = new Point(12, 82);
            txtSeach.Name = "txtSeach";
            txtSeach.PlaceholderText = "Tìm kiếm";
            txtSeach.Size = new Size(253, 23);
            txtSeach.TabIndex = 6;
            txtSeach.TextChanged += txtSeach_TextChanged;
            // 
            // bntDetail
            // 
            bntDetail.Location = new Point(632, 81);
            bntDetail.Name = "bntDetail";
            bntDetail.Size = new Size(75, 23);
            bntDetail.TabIndex = 7;
            bntDetail.Text = "Chi tiết";
            bntDetail.UseVisualStyleBackColor = true;
            bntDetail.Click += button1_Click;
            // 
            // bntReload
            // 
            bntReload.Location = new Point(551, 81);
            bntReload.Name = "bntReload";
            bntReload.Size = new Size(75, 23);
            bntReload.TabIndex = 8;
            bntReload.Text = "Làm mới";
            bntReload.UseVisualStyleBackColor = true;
            bntReload.Click += bntReload_Click;
            // 
            // lblSerialNumber
            // 
            lblSerialNumber.AutoSize = true;
            lblSerialNumber.Location = new Point(79, 28);
            lblSerialNumber.Name = "lblSerialNumber";
            lblSerialNumber.Size = new Size(0, 15);
            lblSerialNumber.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 10;
            label1.Text = "Tài khoản:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 28);
            label3.Name = "label3";
            label3.Size = new Size(27, 15);
            label3.TabIndex = 11;
            label3.Text = "Mã:";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(lblSerialNumber);
            Controls.Add(bntReload);
            Controls.Add(bntDetail);
            Controls.Add(txtSeach);
            Controls.Add(bntBrowser);
            Controls.Add(label2);
            Controls.Add(lblWelcome);
            Controls.Add(dtgListPatch);
            Name = "Dashboard";
            Text = "Dashboard";
            ((System.ComponentModel.ISupportInitialize)dtgListPatch).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dtgListPatch;
        private Label lblWelcome;
        private Label label2;
        private Button bntBrowser;
        private TextBox txtSeach;
        private Button bntDetail;
        private Button bntReload;
        private Label lblSerialNumber;
        private Label label1;
        private Label label3;
    }
}