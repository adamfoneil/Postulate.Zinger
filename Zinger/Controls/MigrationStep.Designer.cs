
namespace Zinger.Controls
{
    partial class MigrationStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MigrationStep));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMapFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbSelectFrom = new FastColoredTextBoxNS.FastColoredTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbSourceIdentityCol = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDestIdentityCol = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectFrom)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbSelectFrom);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvColumns);
            this.splitContainer1.Size = new System.Drawing.Size(410, 427);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvColumns
            // 
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSource,
            this.colDestColumn,
            this.colMapFrom});
            this.dgvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumns.Location = new System.Drawing.Point(0, 0);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.Size = new System.Drawing.Size(410, 218);
            this.dgvColumns.TabIndex = 2;
            // 
            // colSource
            // 
            this.colSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSource.DataPropertyName = "Source";
            this.colSource.HeaderText = "Source Column";
            this.colSource.Name = "colSource";
            // 
            // colDestColumn
            // 
            this.colDestColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDestColumn.DataPropertyName = "Dest";
            this.colDestColumn.HeaderText = "Dest Column";
            this.colDestColumn.Name = "colDestColumn";
            // 
            // colMapFrom
            // 
            this.colMapFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMapFrom.DataPropertyName = "KeyMapTable";
            this.colMapFrom.HeaderText = "Map From Step";
            this.colMapFrom.Name = "colMapFrom";
            // 
            // tbSelectFrom
            // 
            this.tbSelectFrom.AutoCompleteBracketsList = new char[] {
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
            this.tbSelectFrom.AutoIndentCharsPatterns = "";
            this.tbSelectFrom.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbSelectFrom.BackBrush = null;
            this.tbSelectFrom.CharHeight = 14;
            this.tbSelectFrom.CharWidth = 8;
            this.tbSelectFrom.CommentPrefix = "--";
            this.tbSelectFrom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSelectFrom.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbSelectFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSelectFrom.IsReplaceMode = false;
            this.tbSelectFrom.Language = FastColoredTextBoxNS.Language.SQL;
            this.tbSelectFrom.LeftBracket = '(';
            this.tbSelectFrom.Location = new System.Drawing.Point(0, 16);
            this.tbSelectFrom.Name = "tbSelectFrom";
            this.tbSelectFrom.Paddings = new System.Windows.Forms.Padding(0);
            this.tbSelectFrom.RightBracket = ')';
            this.tbSelectFrom.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbSelectFrom.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbSelectFrom.ServiceColors")));
            this.tbSelectFrom.Size = new System.Drawing.Size(410, 135);
            this.tbSelectFrom.TabIndex = 4;
            this.tbSelectFrom.Zoom = 100;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(410, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Select From:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tbSourceIdentityCol, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbDestIdentityCol, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 151);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(410, 54);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tbSourceIdentityCol
            // 
            this.tbSourceIdentityCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSourceIdentityCol.Location = new System.Drawing.Point(3, 30);
            this.tbSourceIdentityCol.Name = "tbSourceIdentityCol";
            this.tbSourceIdentityCol.Size = new System.Drawing.Size(199, 20);
            this.tbSourceIdentityCol.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Source Identity Column:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Dest Identity Column:";
            // 
            // tbDestIdentityCol
            // 
            this.tbDestIdentityCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDestIdentityCol.Location = new System.Drawing.Point(208, 30);
            this.tbDestIdentityCol.Name = "tbDestIdentityCol";
            this.tbDestIdentityCol.Size = new System.Drawing.Size(199, 20);
            this.tbDestIdentityCol.TabIndex = 5;
            // 
            // MigrationStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MigrationStep";
            this.Size = new System.Drawing.Size(410, 427);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectFrom)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMapFrom;
        private FastColoredTextBoxNS.FastColoredTextBox tbSelectFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbSourceIdentityCol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDestIdentityCol;
    }
}
