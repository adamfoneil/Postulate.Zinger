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
            this.tvwObjects = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDefinitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rowCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createModelClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildInsertStatementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildClassInitializerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getDbDiagramioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.imlSmallIcons.Images.SetKeyName(11, "calculated");
            this.imlSmallIcons.Images.SetKeyName(12, "proc");
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
            this.viewDefinitionToolStripMenuItem,
            this.rowCountToolStripMenuItem,
            this.createModelClassToolStripMenuItem,
            this.setAliasToolStripMenuItem,
            this.removeAliasToolStripMenuItem,
            this.buildInsertStatementToolStripMenuItem,
            this.buildClassInitializerToolStripMenuItem,
            this.toolStripSeparator1,
            this.getDbDiagramioToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(207, 208);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // selectColumnsToolStripMenuItem
            // 
            this.selectColumnsToolStripMenuItem.Name = "selectColumnsToolStripMenuItem";
            this.selectColumnsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.selectColumnsToolStripMenuItem.Text = "Select Columns";
            this.selectColumnsToolStripMenuItem.Click += new System.EventHandler(this.selectColumnsToolStripMenuItem_Click);
            // 
            // viewDefinitionToolStripMenuItem
            // 
            this.viewDefinitionToolStripMenuItem.Name = "viewDefinitionToolStripMenuItem";
            this.viewDefinitionToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.viewDefinitionToolStripMenuItem.Text = "View Definition";
            this.viewDefinitionToolStripMenuItem.Click += new System.EventHandler(this.viewDefinitionToolStripMenuItem_Click);
            // 
            // rowCountToolStripMenuItem
            // 
            this.rowCountToolStripMenuItem.Enabled = false;
            this.rowCountToolStripMenuItem.Name = "rowCountToolStripMenuItem";
            this.rowCountToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.rowCountToolStripMenuItem.Text = "Row Count";
            // 
            // createModelClassToolStripMenuItem
            // 
            this.createModelClassToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createModelClassToolStripMenuItem.Image")));
            this.createModelClassToolStripMenuItem.Name = "createModelClassToolStripMenuItem";
            this.createModelClassToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.createModelClassToolStripMenuItem.Text = "Copy Model Class...";
            this.createModelClassToolStripMenuItem.Click += new System.EventHandler(this.createModelClassToolStripMenuItem_Click);
            // 
            // setAliasToolStripMenuItem
            // 
            this.setAliasToolStripMenuItem.Name = "setAliasToolStripMenuItem";
            this.setAliasToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.setAliasToolStripMenuItem.Text = "Set Alias...";
            this.setAliasToolStripMenuItem.Click += new System.EventHandler(this.setAliasToolStripMenuItem_Click);
            // 
            // removeAliasToolStripMenuItem
            // 
            this.removeAliasToolStripMenuItem.Name = "removeAliasToolStripMenuItem";
            this.removeAliasToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.removeAliasToolStripMenuItem.Text = "Remove Alias...";
            this.removeAliasToolStripMenuItem.Click += new System.EventHandler(this.removeAliasToolStripMenuItem_Click);
            // 
            // buildInsertStatementToolStripMenuItem
            // 
            this.buildInsertStatementToolStripMenuItem.Name = "buildInsertStatementToolStripMenuItem";
            this.buildInsertStatementToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.buildInsertStatementToolStripMenuItem.Text = "Build Insert Statement";
            this.buildInsertStatementToolStripMenuItem.Click += new System.EventHandler(this.buildInsertStatementToolStripMenuItem_Click);
            // 
            // buildClassInitializerToolStripMenuItem
            // 
            this.buildClassInitializerToolStripMenuItem.Name = "buildClassInitializerToolStripMenuItem";
            this.buildClassInitializerToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.buildClassInitializerToolStripMenuItem.Text = "Build Class Initializer";
            this.buildClassInitializerToolStripMenuItem.Click += new System.EventHandler(this.buildClassInitializerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // getDbDiagramioToolStripMenuItem
            // 
            this.getDbDiagramioToolStripMenuItem.Name = "getDbDiagramioToolStripMenuItem";
            this.getDbDiagramioToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.getDbDiagramioToolStripMenuItem.Text = "Get DbDiagram.io Syntax";
            this.getDbDiagramioToolStripMenuItem.Click += new System.EventHandler(this.getDbDiagramioToolStripMenuItem_Click);
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
            this.llRefresh.Size = new System.Drawing.Size(216, 17);
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
        private System.Windows.Forms.ToolStripMenuItem createModelClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAliasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildInsertStatementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildClassInitializerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDefinitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem getDbDiagramioToolStripMenuItem;
    }
}
