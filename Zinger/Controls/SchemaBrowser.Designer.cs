namespace Zinger.Controls
{
    partial class SchemaBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaBrowser));
            this.imlSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.tvwObjects = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imlSmallIcons
            // 
            this.imlSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmallIcons.ImageStream")));
            this.imlSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlSmallIcons.Images.SetKeyName(0, "schema");
            this.imlSmallIcons.Images.SetKeyName(1, "table");
            this.imlSmallIcons.Images.SetKeyName(2, "column");
            this.imlSmallIcons.Images.SetKeyName(3, "primaryKey");
            this.imlSmallIcons.Images.SetKeyName(4, "shortcut");
            // 
            // tbSearch
            // 
            this.tbSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSearch.Location = new System.Drawing.Point(0, 0);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(231, 21);
            this.tbSearch.TabIndex = 0;
            // 
            // tvwObjects
            // 
            this.tvwObjects.ContextMenuStrip = this.contextMenuStrip1;
            this.tvwObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwObjects.ImageIndex = 0;
            this.tvwObjects.ImageList = this.imlSmallIcons;
            this.tvwObjects.Location = new System.Drawing.Point(0, 21);
            this.tvwObjects.Name = "tvwObjects";
            this.tvwObjects.SelectedImageIndex = 0;
            this.tvwObjects.Size = new System.Drawing.Size(231, 380);
            this.tvwObjects.TabIndex = 1;
            this.tvwObjects.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwObjects_BeforeExpand);
            this.tvwObjects.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwObjects_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectColumnsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 26);
            // 
            // selectColumnsToolStripMenuItem
            // 
            this.selectColumnsToolStripMenuItem.Name = "selectColumnsToolStripMenuItem";
            this.selectColumnsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.selectColumnsToolStripMenuItem.Text = "Select Columns";
            this.selectColumnsToolStripMenuItem.Click += new System.EventHandler(this.selectColumnsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 379);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(231, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "Loading...";
            // 
            // SchemaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvwObjects);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbSearch);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SchemaBrowser";
            this.Size = new System.Drawing.Size(231, 401);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.TreeView tvwObjects;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectColumnsToolStripMenuItem;
    }
}
