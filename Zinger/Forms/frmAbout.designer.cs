namespace Zinger.Forms
{
    partial class frmAbout
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
            this.label1 = new System.Windows.Forms.Label();
            this.webUrlLinkLabel1 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.webUrlLinkLabel2 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zinger";
            // 
            // webUrlLinkLabel1
            // 
            this.webUrlLinkLabel1.AutoSize = true;
            this.webUrlLinkLabel1.Location = new System.Drawing.Point(98, 19);
            this.webUrlLinkLabel1.Name = "webUrlLinkLabel1";
            this.webUrlLinkLabel1.Size = new System.Drawing.Size(95, 13);
            this.webUrlLinkLabel1.TabIndex = 1;
            this.webUrlLinkLabel1.TabStop = true;
            this.webUrlLinkLabel1.Text = "by Adam O\'Neil";
            this.webUrlLinkLabel1.Url = "http://www.aosoftware.net";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(74, 45);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(41, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "label3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Download and Install";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.lblNewVersion);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 41);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Location = new System.Drawing.Point(12, 13);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(169, 13);
            this.lblNewVersion.TabIndex = 5;
            this.lblNewVersion.Text = "Version {version} available:";
            // 
            // webUrlLinkLabel2
            // 
            this.webUrlLinkLabel2.AutoSize = true;
            this.webUrlLinkLabel2.Location = new System.Drawing.Point(25, 99);
            this.webUrlLinkLabel2.Name = "webUrlLinkLabel2";
            this.webUrlLinkLabel2.Size = new System.Drawing.Size(279, 13);
            this.webUrlLinkLabel2.TabIndex = 6;
            this.webUrlLinkLabel2.TabStop = true;
            this.webUrlLinkLabel2.Text = "https://github.com/adamfoneil/Postulate.Zinger";
            this.webUrlLinkLabel2.Url = "https://github.com/adamfoneil/Postulate.Zinger";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "GitHub repo";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(478, 235);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.webUrlLinkLabel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.webUrlLinkLabel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Zinger";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNewVersion;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel2;
        private System.Windows.Forms.Label label3;
    }
}