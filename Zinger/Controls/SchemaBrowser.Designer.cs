﻿namespace Zinger.Controls
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
            this.tvwObjects = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.pbLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.llRefresh = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
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
            this.imlSmallIcons.Images.SetKeyName(5, "unique");
            this.imlSmallIcons.Images.SetKeyName(6, "join");
            this.imlSmallIcons.Images.SetKeyName(7, "folder");
            this.imlSmallIcons.Images.SetKeyName(8, "view");
            this.imlSmallIcons.Images.SetKeyName(9, "table-function");
            this.imlSmallIcons.Images.SetKeyName(10, "param-in");
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
            this.tvwObjects.Size = new System.Drawing.Size(231, 254);
            this.tvwObjects.TabIndex = 1;
            this.tvwObjects.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwObjects_BeforeExpand);
            this.tvwObjects.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwObjects_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectColumnsToolStripMenuItem,
            this.rowCountToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // selectColumnsToolStripMenuItem
            // 
            this.selectColumnsToolStripMenuItem.Name = "selectColumnsToolStripMenuItem";
            this.selectColumnsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.selectColumnsToolStripMenuItem.Text = "Select Columns";
            this.selectColumnsToolStripMenuItem.Click += new System.EventHandler(this.selectColumnsToolStripMenuItem_Click);
            // 
            // rowCountToolStripMenuItem
            // 
            this.rowCountToolStripMenuItem.Enabled = false;
            this.rowCountToolStripMenuItem.Name = "rowCountToolStripMenuItem";
            this.rowCountToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.rowCountToolStripMenuItem.Text = "Row Count";
            // 
            // tbSearch
            // 
            this.tbSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSearch.Location = new System.Drawing.Point(0, 0);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(231, 21);
            this.tbSearch.TabIndex = 5;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbLoading,
            this.llRefresh});
            this.statusStrip2.Location = new System.Drawing.Point(0, 275);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(231, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 7;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // pbLoading
            // 
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(100, 16);
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoading.Visible = false;
            // 
            // llRefresh
            // 
            this.llRefresh.IsLink = true;
            this.llRefresh.Name = "llRefresh";
            this.llRefresh.Size = new System.Drawing.Size(83, 17);
            this.llRefresh.Spring = true;
            this.llRefresh.Text = "Refresh";
            this.llRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llRefresh.Click += new System.EventHandler(this.llRefresh_Click);
            // 
            // SchemaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvwObjects);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.statusStrip2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SchemaBrowser";
            this.Size = new System.Drawing.Size(231, 297);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imlSmallIcons;
        private System.Windows.Forms.TreeView tvwObjects;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectColumnsToolStripMenuItem;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ToolStripMenuItem rowCountToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripProgressBar pbLoading;
        private System.Windows.Forms.ToolStripStatusLabel llRefresh;
    }
}
