namespace Zinger.Forms
{
    partial class frmScriptData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptData));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.cbTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbOmitIdentity = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbIdentityInsert = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(315, 119);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(159, 119);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 26);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy to Clipboard";
            this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cbTable
            // 
            this.cbTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(12, 25);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(378, 21);
            this.cbTable.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Insert into Table:";
            // 
            // rbOmitIdentity
            // 
            this.rbOmitIdentity.AutoSize = true;
            this.rbOmitIdentity.Checked = true;
            this.rbOmitIdentity.Location = new System.Drawing.Point(131, 66);
            this.rbOmitIdentity.Name = "rbOmitIdentity";
            this.rbOmitIdentity.Size = new System.Drawing.Size(52, 17);
            this.rbOmitIdentity.TabIndex = 4;
            this.rbOmitIdentity.TabStop = true;
            this.rbOmitIdentity.Text = "Omit";
            this.rbOmitIdentity.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Identity Column:";
            // 
            // rbIdentityInsert
            // 
            this.rbIdentityInsert.AutoSize = true;
            this.rbIdentityInsert.Location = new System.Drawing.Point(189, 66);
            this.rbIdentityInsert.Name = "rbIdentityInsert";
            this.rbIdentityInsert.Size = new System.Drawing.Size(201, 17);
            this.rbIdentityInsert.TabIndex = 6;
            this.rbIdentityInsert.TabStop = true;
            this.rbIdentityInsert.Text = "Include, IDENTITY_INSERT ON";
            this.rbIdentityInsert.UseVisualStyleBackColor = true;
            // 
            // frmScriptData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(402, 157);
            this.Controls.Add(this.rbIdentityInsert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbOmitIdentity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTable);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScriptData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data to Script";
            this.Load += new System.EventHandler(this.frmScriptData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ComboBox cbTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbOmitIdentity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbIdentityInsert;
    }
}