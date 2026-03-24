namespace PatchSeller.Launcher.ActionForms
{
    partial class VersionDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionDetail));
            parentTabControl = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            parentTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // parentTabControl
            // 
            parentTabControl.Controls.Add(tabPage1);
            parentTabControl.Controls.Add(tabPage2);
            parentTabControl.Location = new Point(12, 12);
            parentTabControl.Name = "parentTabControl";
            parentTabControl.SelectedIndex = 0;
            parentTabControl.Size = new Size(776, 426);
            parentTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(768, 398);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPages";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 398);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // VersionDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(parentTabControl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "VersionDetail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VersionDetail";
            parentTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl parentTabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}