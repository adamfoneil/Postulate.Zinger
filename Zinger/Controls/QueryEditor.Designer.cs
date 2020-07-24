namespace Zinger.Controls
{
    partial class QueryEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryEditor));
            this.splcQueryAndResults = new System.Windows.Forms.SplitContainer();
            this.splcQueryAndParams = new System.Windows.Forms.SplitContainer();
            this.tbQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.colParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colParamNullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colExpression = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslQueryMetrics = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbExecuting = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndResults)).BeginInit();
            this.splcQueryAndResults.Panel1.SuspendLayout();
            this.splcQueryAndResults.Panel2.SuspendLayout();
            this.splcQueryAndResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndParams)).BeginInit();
            this.splcQueryAndParams.Panel1.SuspendLayout();
            this.splcQueryAndParams.Panel2.SuspendLayout();
            this.splcQueryAndParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splcQueryAndResults
            // 
            this.splcQueryAndResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcQueryAndResults.Location = new System.Drawing.Point(0, 0);
            this.splcQueryAndResults.Name = "splcQueryAndResults";
            this.splcQueryAndResults.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splcQueryAndResults.Panel1
            // 
            this.splcQueryAndResults.Panel1.Controls.Add(this.splcQueryAndParams);
            // 
            // splcQueryAndResults.Panel2
            // 
            this.splcQueryAndResults.Panel2.Controls.Add(this.dgvResults);
            this.splcQueryAndResults.Panel2.Controls.Add(this.statusStrip1);
            this.splcQueryAndResults.Size = new System.Drawing.Size(471, 494);
            this.splcQueryAndResults.SplitterDistance = 338;
            this.splcQueryAndResults.TabIndex = 1;
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
            // 
            // splcQueryAndParams.Panel2
            // 
            this.splcQueryAndParams.Panel2.Controls.Add(this.dgvParams);
            this.splcQueryAndParams.Size = new System.Drawing.Size(471, 338);
            this.splcQueryAndParams.SplitterDistance = 228;
            this.splcQueryAndParams.TabIndex = 1;
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
            this.tbQuery.Size = new System.Drawing.Size(471, 228);
            this.tbQuery.TabIndex = 0;
            this.tbQuery.Zoom = 100;
            this.tbQuery.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.tbQuery_TextChanged);
            this.tbQuery.TextChanging += new System.EventHandler<FastColoredTextBoxNS.TextChangingEventArgs>(this.tbQuery_TextChanging);
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
            this.dgvParams.Size = new System.Drawing.Size(471, 106);
            this.dgvParams.TabIndex = 0;
            this.dgvParams.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvParams_CellValueChanged);
            this.dgvParams.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvParams_DataError);
            this.dgvParams.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvParams_DefaultValuesNeeded);
            this.dgvParams.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvParams_UserAddedRow);
            this.dgvParams.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvParams_UserDeletedRow);
            // 
            // colParamName
            // 
            this.colParamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamName.DataPropertyName = "Name";
            this.colParamName.HeaderText = "Param Name";
            this.colParamName.MinimumWidth = 110;
            this.colParamName.Name = "colParamName";
            this.colParamName.Width = 110;
            // 
            // colParamType
            // 
            this.colParamType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamType.DataPropertyName = "DataType";
            this.colParamType.HeaderText = "Data Type";
            this.colParamType.Name = "colParamType";
            this.colParamType.Width = 40;
            // 
            // colParamNullable
            // 
            this.colParamNullable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamNullable.DataPropertyName = "IsOptional";
            this.colParamNullable.HeaderText = "N";
            this.colParamNullable.Name = "colParamNullable";
            this.colParamNullable.Width = 21;
            // 
            // colExpression
            // 
            this.colExpression.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colExpression.DataPropertyName = "Expression";
            this.colExpression.HeaderText = "Expression";
            this.colExpression.Name = "colExpression";
            // 
            // colParamValue
            // 
            this.colParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colParamValue.DataPropertyName = "Value";
            this.colParamValue.HeaderText = "Value";
            this.colParamValue.Name = "colParamValue";
            this.colParamValue.Width = 63;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(471, 130);
            this.dgvResults.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslQueryMetrics,
            this.pbExecuting});
            this.statusStrip1.Location = new System.Drawing.Point(0, 130);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(471, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslQueryMetrics
            // 
            this.tslQueryMetrics.Name = "tslQueryMetrics";
            this.tslQueryMetrics.Size = new System.Drawing.Size(39, 17);
            this.tslQueryMetrics.Text = "Ready";
            // 
            // pbExecuting
            // 
            this.pbExecuting.Name = "pbExecuting";
            this.pbExecuting.Size = new System.Drawing.Size(100, 16);
            this.pbExecuting.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbExecuting.Visible = false;
            // 
            // QueryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splcQueryAndResults);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "QueryEditor";
            this.Size = new System.Drawing.Size(471, 494);
            this.Load += new System.EventHandler(this.QueryEditor_Load);
            this.splcQueryAndResults.Panel1.ResumeLayout(false);
            this.splcQueryAndResults.Panel2.ResumeLayout(false);
            this.splcQueryAndResults.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndResults)).EndInit();
            this.splcQueryAndResults.ResumeLayout(false);
            this.splcQueryAndParams.Panel1.ResumeLayout(false);
            this.splcQueryAndParams.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcQueryAndParams)).EndInit();
            this.splcQueryAndParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splcQueryAndResults;
        private System.Windows.Forms.SplitContainer splcQueryAndParams;
        private FastColoredTextBoxNS.FastColoredTextBox tbQuery;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslQueryMetrics;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colParamType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colParamNullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpression;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamValue;
        private System.Windows.Forms.ToolStripProgressBar pbExecuting;
    }
}
