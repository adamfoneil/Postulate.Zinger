namespace Zinger
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splcQueryAndSourceTree = new System.Windows.Forms.SplitContainer();
            this.splcQueryAndResults = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            this.splcQueryAndParams = new System.Windows.Forms.SplitContainer();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkParams = new System.Windows.Forms.CheckBox();
            this.tabNavigation = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.colParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colParamNullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colExpression = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndSourceTree)).BeginInit();
            this.splcQueryAndSourceTree.Panel1.SuspendLayout();
            this.splcQueryAndSourceTree.Panel2.SuspendLayout();
            this.splcQueryAndSourceTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndResults)).BeginInit();
            this.splcQueryAndResults.Panel1.SuspendLayout();
            this.splcQueryAndResults.Panel2.SuspendLayout();
            this.splcQueryAndResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndParams)).BeginInit();
            this.splcQueryAndParams.Panel1.SuspendLayout();
            this.splcQueryAndParams.Panel2.SuspendLayout();
            this.splcQueryAndParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(725, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
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
            this.tabControl1.Size = new System.Drawing.Size(499, 461);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splcQueryAndResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(491, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(411, 328);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "C#";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.splcQueryAndSourceTree.Panel1.Controls.Add(this.tabControl1);
            // 
            // splcQueryAndSourceTree.Panel2
            // 
            this.splcQueryAndSourceTree.Panel2.Controls.Add(this.tabNavigation);
            this.splcQueryAndSourceTree.Size = new System.Drawing.Size(725, 461);
            this.splcQueryAndSourceTree.SplitterDistance = 499;
            this.splcQueryAndSourceTree.TabIndex = 2;
            // 
            // splcQueryAndResults
            // 
            this.splcQueryAndResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcQueryAndResults.Location = new System.Drawing.Point(3, 3);
            this.splcQueryAndResults.Name = "splcQueryAndResults";
            this.splcQueryAndResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splcQueryAndResults.Panel1
            // 
            this.splcQueryAndResults.Panel1.Controls.Add(this.splcQueryAndParams);
            // 
            // splcQueryAndResults.Panel2
            // 
            this.splcQueryAndResults.Panel2.Controls.Add(this.dataGridView1);
            this.splcQueryAndResults.Panel2.Controls.Add(this.statusStrip1);
            this.splcQueryAndResults.Size = new System.Drawing.Size(485, 425);
            this.splcQueryAndResults.SplitterDistance = 291;
            this.splcQueryAndResults.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(485, 108);
            this.dataGridView1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 108);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(485, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(102, 17);
            this.toolStripStatusLabel1.Text = "{0} records, {1} ms";
            // 
            // tbQuery
            // 
            this.tbQuery.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.tbQuery.AutoIndentCharsPatterns = "";
            this.tbQuery.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbQuery.BackBrush = null;
            this.tbQuery.CharHeight = 14;
            this.tbQuery.CharWidth = 8;
            this.tbQuery.CommentPrefix = "--";
            this.tbQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbQuery.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQuery.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbQuery.IsReplaceMode = false;
            this.tbQuery.Language = FastColoredTextBoxNS.Language.SQL;
            this.tbQuery.LeftBracket = '(';
            this.tbQuery.Location = new System.Drawing.Point(0, 0);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Paddings = new System.Windows.Forms.Padding(0);
            this.tbQuery.RightBracket = ')';
            this.tbQuery.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbQuery.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbQuery.ServiceColors")));
            this.tbQuery.Size = new System.Drawing.Size(485, 163);
            this.tbQuery.TabIndex = 0;
            this.tbQuery.Zoom = 100;
            // 
            // splcQueryAndParams
            // 
            this.splcQueryAndParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcQueryAndParams.Location = new System.Drawing.Point(0, 0);
            this.splcQueryAndParams.Name = "splcQueryAndParams";
            this.splcQueryAndParams.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splcQueryAndParams.Panel1
            // 
            this.splcQueryAndParams.Panel1.Controls.Add(this.tbQuery);
            this.splcQueryAndParams.Panel1.Controls.Add(this.panel1);
            // 
            // splcQueryAndParams.Panel2
            // 
            this.splcQueryAndParams.Panel2.Controls.Add(this.dgvParams);
            this.splcQueryAndParams.Size = new System.Drawing.Size(485, 291);
            this.splcQueryAndParams.SplitterDistance = 197;
            this.splcQueryAndParams.TabIndex = 1;
            // 
            // dgvParams
            // 
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParamName,
            this.colParamType,
            this.colParamNullable,
            this.colExpression,
            this.colParamValue});
            this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParams.Location = new System.Drawing.Point(0, 0);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.Size = new System.Drawing.Size(485, 90);
            this.dgvParams.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkParams);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 34);
            this.panel1.TabIndex = 1;
            // 
            // chkParams
            // 
            this.chkParams.AutoSize = true;
            this.chkParams.Location = new System.Drawing.Point(12, 9);
            this.chkParams.Name = "chkParams";
            this.chkParams.Size = new System.Drawing.Size(92, 17);
            this.chkParams.TabIndex = 0;
            this.chkParams.Text = "Parameters";
            this.chkParams.UseVisualStyleBackColor = true;
            // 
            // tabNavigation
            // 
            this.tabNavigation.Controls.Add(this.tabPage3);
            this.tabNavigation.Controls.Add(this.tabPage4);
            this.tabNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNavigation.Location = new System.Drawing.Point(0, 0);
            this.tabNavigation.Name = "tabNavigation";
            this.tabNavigation.SelectedIndex = 0;
            this.tabNavigation.Size = new System.Drawing.Size(222, 461);
            this.tabNavigation.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(214, 435);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Objects";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(214, 435);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Source Files";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // colParamName
            // 
            this.colParamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamName.HeaderText = "Param Name";
            this.colParamName.MinimumWidth = 110;
            this.colParamName.Name = "colParamName";
            this.colParamName.Width = 110;
            // 
            // colParamType
            // 
            this.colParamType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamType.HeaderText = "Data Type";
            this.colParamType.Name = "colParamType";
            this.colParamType.Width = 71;
            // 
            // colParamNullable
            // 
            this.colParamNullable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamNullable.HeaderText = "N";
            this.colParamNullable.Name = "colParamNullable";
            this.colParamNullable.Width = 21;
            // 
            // colExpression
            // 
            this.colExpression.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colExpression.HeaderText = "Expression";
            this.colExpression.Name = "colExpression";
            // 
            // colParamValue
            // 
            this.colParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamValue.HeaderText = "Value";
            this.colParamValue.Name = "colParamValue";
            this.colParamValue.Width = 63;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 486);
            this.Controls.Add(this.splcQueryAndSourceTree);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zinger";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splcQueryAndSourceTree.Panel1.ResumeLayout(false);
            this.splcQueryAndSourceTree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndSourceTree)).EndInit();
            this.splcQueryAndSourceTree.ResumeLayout(false);
            this.splcQueryAndResults.Panel1.ResumeLayout(false);
            this.splcQueryAndResults.Panel2.ResumeLayout(false);
            this.splcQueryAndResults.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndResults)).EndInit();
            this.splcQueryAndResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuery)).EndInit();
            this.splcQueryAndParams.Panel1.ResumeLayout(false);
            this.splcQueryAndParams.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndParams)).EndInit();
            this.splcQueryAndParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.SplitContainer splcQueryAndResults;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private FastColoredTextBoxNS.FastColoredTextBox tbQuery;
        private System.Windows.Forms.SplitContainer splcQueryAndParams;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkParams;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.TabControl tabNavigation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colParamType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colParamNullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpression;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamValue;
    }
}

