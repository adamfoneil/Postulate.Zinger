namespace Zinger.Forms
{
    partial class frmQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuery));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnConnections = new System.Windows.Forms.ToolStripButton();
            this.cbConnection = new System.Windows.Forms.ToolStripComboBox();
            this.btnRunQuery = new System.Windows.Forms.ToolStripButton();
            this.btnDataToScript = new System.Windows.Forms.ToolStripButton();
            this.btnSchema = new System.Windows.Forms.ToolStripButton();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.queryEditor1 = new Zinger.Controls.QueryEditor();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.resultClassBuilder1 = new Zinger.Controls.ResultClassBuilder();
            this.splcQueryAndSourceTree = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.schemaBrowser1 = new Zinger.Controls.SchemaBrowser();
            this.tabNavigation = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndSourceTree)).BeginInit();
            this.splcQueryAndSourceTree.Panel1.SuspendLayout();
            this.splcQueryAndSourceTree.Panel2.SuspendLayout();
            this.splcQueryAndSourceTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnections,
            this.cbConnection,
            this.btnRunQuery,
            this.btnDataToScript,
            this.btnSchema,
            this.btnImportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(725, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnConnections
            // 
            this.btnConnections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConnections.Image = ((System.Drawing.Image)(resources.GetObject("btnConnections.Image")));
            this.btnConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnections.Name = "btnConnections";
            this.btnConnections.Size = new System.Drawing.Size(23, 22);
            this.btnConnections.Text = "Connections";
            this.btnConnections.Click += new System.EventHandler(this.btnConnections_Click);
            // 
            // cbConnection
            // 
            this.cbConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConnection.Name = "cbConnection";
            this.cbConnection.Size = new System.Drawing.Size(200, 25);
            this.cbConnection.SelectedIndexChanged += new System.EventHandler(this.cbConnection_SelectedIndexChanged);
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnRunQuery.Image")));
            this.btnRunQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(180, 22);
            this.btnRunQuery.Text = "Run Query / Build C# Output";
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // btnDataToScript
            // 
            this.btnDataToScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDataToScript.Image = ((System.Drawing.Image)(resources.GetObject("btnDataToScript.Image")));
            this.btnDataToScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataToScript.Name = "btnDataToScript";
            this.btnDataToScript.Size = new System.Drawing.Size(91, 22);
            this.btnDataToScript.Text = "Data to Script...";
            this.btnDataToScript.Click += new System.EventHandler(this.btnDataToScript_Click);
            // 
            // btnSchema
            // 
            this.btnSchema.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSchema.Enabled = false;
            this.btnSchema.Image = ((System.Drawing.Image)(resources.GetObject("btnSchema.Image")));
            this.btnSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSchema.Name = "btnSchema";
            this.btnSchema.Size = new System.Drawing.Size(69, 22);
            this.btnSchema.Text = "Schema";
            this.btnSchema.Click += new System.EventHandler(this.btnSchema_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(86, 22);
            this.btnImportExcel.Text = "Import Excel...";
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 5);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(725, 461);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.queryEditor1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(717, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // queryEditor1
            // 
            this.queryEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryEditor1.Enabled = false;
            this.queryEditor1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queryEditor1.Location = new System.Drawing.Point(3, 3);
            this.queryEditor1.Name = "queryEditor1";
            this.queryEditor1.Provider = null;
            this.queryEditor1.QueryName = null;
            this.queryEditor1.Size = new System.Drawing.Size(711, 425);
            this.queryEditor1.Sql = "";
            this.queryEditor1.TabIndex = 0;
            this.queryEditor1.Executed += new System.EventHandler(this.queryEditor1_Executed);
            this.queryEditor1.Modified += new System.EventHandler(this.queryEditor1_Modified);            
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.resultClassBuilder1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(717, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "C#";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // resultClassBuilder1
            // 
            this.resultClassBuilder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultClassBuilder1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultClassBuilder1.Location = new System.Drawing.Point(3, 3);
            this.resultClassBuilder1.Name = "resultClassBuilder1";
            this.resultClassBuilder1.QueryClass = "";
            this.resultClassBuilder1.QueryName = "";
            this.resultClassBuilder1.ResultClass = "";
            this.resultClassBuilder1.Size = new System.Drawing.Size(711, 425);
            this.resultClassBuilder1.TabIndex = 0;
            this.resultClassBuilder1.QueryNameChanged += new System.EventHandler(this.resultClassBuilder1_QueryNameChanged);
            // 
            // splcQueryAndSourceTree
            // 
            this.splcQueryAndSourceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcQueryAndSourceTree.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splcQueryAndSourceTree.Location = new System.Drawing.Point(0, 25);
            this.splcQueryAndSourceTree.Name = "splcQueryAndSourceTree";
            // 
            // splcQueryAndSourceTree.Panel1
            // 
            this.splcQueryAndSourceTree.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splcQueryAndSourceTree.Panel2
            // 
            this.splcQueryAndSourceTree.Panel2.Controls.Add(this.tabNavigation);
            this.splcQueryAndSourceTree.Panel2Collapsed = true;
            this.splcQueryAndSourceTree.Size = new System.Drawing.Size(725, 461);
            this.splcQueryAndSourceTree.SplitterDistance = 499;
            this.splcQueryAndSourceTree.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.schemaBrowser1);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(725, 461);
            this.splitContainer1.SplitterDistance = 493;
            this.splitContainer1.TabIndex = 2;
            // 
            // schemaBrowser1
            // 
            this.schemaBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaBrowser1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.schemaBrowser1.Location = new System.Drawing.Point(0, 0);
            this.schemaBrowser1.Name = "schemaBrowser1";
            this.schemaBrowser1.Size = new System.Drawing.Size(96, 100);
            this.schemaBrowser1.TabIndex = 0;
            // 
            // tabNavigation
            // 
            this.tabNavigation.Controls.Add(this.tabPage3);
            this.tabNavigation.Controls.Add(this.tabPage4);
            this.tabNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNavigation.Location = new System.Drawing.Point(0, 0);
            this.tabNavigation.Name = "tabNavigation";
            this.tabNavigation.SelectedIndex = 0;
            this.tabNavigation.Size = new System.Drawing.Size(96, 100);
            this.tabNavigation.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(88, 74);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Objects";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(88, 74);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Source Files";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // frmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 486);
            this.Controls.Add(this.splcQueryAndSourceTree);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Query";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQuery_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splcQueryAndSourceTree.Panel1.ResumeLayout(false);
            this.splcQueryAndSourceTree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndSourceTree)).EndInit();
            this.splcQueryAndSourceTree.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabNavigation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splcQueryAndSourceTree;
        private System.Windows.Forms.TabControl tabNavigation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private Controls.QueryEditor queryEditor1;
        private System.Windows.Forms.ToolStripButton btnConnections;
        private System.Windows.Forms.ToolStripComboBox cbConnection;
        private System.Windows.Forms.ToolStripButton btnRunQuery;
		private Controls.ResultClassBuilder resultClassBuilder1;
        private System.Windows.Forms.ToolStripButton btnDataToScript;
        private System.Windows.Forms.ToolStripButton btnSchema;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.SchemaBrowser schemaBrowser1;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
    }
}

