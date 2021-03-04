
namespace Zinger.Forms
{
    partial class frmMigrationBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMigrationBuilder));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSourceConnection = new System.Windows.Forms.ComboBox();
            this.cbDestConnection = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBuildColumns = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSteps = new System.Windows.Forms.DataGridView();
            this.colOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.colParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMapFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRun = new System.Windows.Forms.Button();
            this.llInsertSql = new System.Windows.Forms.LinkLabel();
            this.pbValidation = new System.Windows.Forms.PictureBox();
            this.llSourceSql = new System.Windows.Forms.LinkLabel();
            this.btnValidateStep = new System.Windows.Forms.Button();
            this.lblStepResult = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbSourceIdentityCol = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDestIdentityCol = new System.Windows.Forms.TextBox();
            this.tbSelectFrom = new FastColoredTextBoxNS.FastColoredTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSteps)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbValidation)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbSourceConnection, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbDestConnection, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(832, 59);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Connection:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Connection:";
            // 
            // cbSourceConnection
            // 
            this.cbSourceConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSourceConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceConnection.FormattingEnabled = true;
            this.cbSourceConnection.Location = new System.Drawing.Point(3, 27);
            this.cbSourceConnection.Name = "cbSourceConnection";
            this.cbSourceConnection.Size = new System.Drawing.Size(410, 21);
            this.cbSourceConnection.TabIndex = 2;
            // 
            // cbDestConnection
            // 
            this.cbDestConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDestConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestConnection.FormattingEnabled = true;
            this.cbDestConnection.Location = new System.Drawing.Point(419, 27);
            this.cbDestConnection.Name = "cbDestConnection";
            this.cbDestConnection.Size = new System.Drawing.Size(410, 21);
            this.cbDestConnection.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnBuildColumns});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(832, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton1.Text = "New";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(40, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnBuildColumns
            // 
            this.btnBuildColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBuildColumns.Image = ((System.Drawing.Image)(resources.GetObject("btnBuildColumns.Image")));
            this.btnBuildColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuildColumns.Name = "btnBuildColumns";
            this.btnBuildColumns.Size = new System.Drawing.Size(84, 22);
            this.btnBuildColumns.Text = "Add Columns";
            this.btnBuildColumns.Click += new System.EventHandler(this.btnBuildColumns_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 84);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(832, 348);
            this.splitContainer1.SplitterDistance = 253;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(253, 348);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvSteps);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(245, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Steps";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvSteps
            // 
            this.dgvSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrder,
            this.colDestTable});
            this.dgvSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSteps.Location = new System.Drawing.Point(3, 3);
            this.dgvSteps.Name = "dgvSteps";
            this.dgvSteps.Size = new System.Drawing.Size(239, 316);
            this.dgvSteps.TabIndex = 0;
            // 
            // colOrder
            // 
            this.colOrder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colOrder.DataPropertyName = "Order";
            this.colOrder.HeaderText = "Order";
            this.colOrder.Name = "colOrder";
            this.colOrder.Width = 58;
            // 
            // colDestTable
            // 
            this.colDestTable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDestTable.DataPropertyName = "DestTable";
            this.colDestTable.HeaderText = "Into Table";
            this.colDestTable.Name = "colDestTable";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvParams);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(245, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parameters";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvParams
            // 
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParamName,
            this.colParamVal});
            this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParams.Location = new System.Drawing.Point(3, 3);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.Size = new System.Drawing.Size(239, 316);
            this.dgvParams.TabIndex = 0;
            // 
            // colParamName
            // 
            this.colParamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colParamName.DataPropertyName = "Name";
            this.colParamName.HeaderText = "Name";
            this.colParamName.Name = "colParamName";
            // 
            // colParamVal
            // 
            this.colParamVal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colParamVal.DataPropertyName = "Value";
            this.colParamVal.HeaderText = "Value";
            this.colParamVal.Name = "colParamVal";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 159);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvColumns);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnRun);
            this.splitContainer2.Panel2.Controls.Add(this.llInsertSql);
            this.splitContainer2.Panel2.Controls.Add(this.pbValidation);
            this.splitContainer2.Panel2.Controls.Add(this.llSourceSql);
            this.splitContainer2.Panel2.Controls.Add(this.btnValidateStep);
            this.splitContainer2.Panel2.Controls.Add(this.lblStepResult);
            this.splitContainer2.Size = new System.Drawing.Size(575, 189);
            this.splitContainer2.SplitterDistance = 326;
            this.splitContainer2.TabIndex = 3;
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
            this.dgvColumns.Size = new System.Drawing.Size(326, 189);
            this.dgvColumns.TabIndex = 1;
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
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(162, 13);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(71, 23);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // llInsertSql
            // 
            this.llInsertSql.AutoSize = true;
            this.llInsertSql.Location = new System.Drawing.Point(107, 108);
            this.llInsertSql.Name = "llInsertSql";
            this.llInsertSql.Size = new System.Drawing.Size(57, 13);
            this.llInsertSql.TabIndex = 4;
            this.llInsertSql.TabStop = true;
            this.llInsertSql.Text = "Insert SQL";
            this.llInsertSql.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llInsertSql_LinkClicked);
            // 
            // pbValidation
            // 
            this.pbValidation.Location = new System.Drawing.Point(13, 43);
            this.pbValidation.Name = "pbValidation";
            this.pbValidation.Size = new System.Drawing.Size(17, 20);
            this.pbValidation.TabIndex = 1;
            this.pbValidation.TabStop = false;
            // 
            // llSourceSql
            // 
            this.llSourceSql.AutoSize = true;
            this.llSourceSql.Location = new System.Drawing.Point(36, 108);
            this.llSourceSql.Name = "llSourceSql";
            this.llSourceSql.Size = new System.Drawing.Size(65, 13);
            this.llSourceSql.TabIndex = 3;
            this.llSourceSql.TabStop = true;
            this.llSourceSql.Text = "Source SQL";
            this.llSourceSql.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSourceSql_LinkClicked);
            // 
            // btnValidateStep
            // 
            this.btnValidateStep.Location = new System.Drawing.Point(10, 13);
            this.btnValidateStep.Name = "btnValidateStep";
            this.btnValidateStep.Size = new System.Drawing.Size(71, 23);
            this.btnValidateStep.TabIndex = 2;
            this.btnValidateStep.Text = "Test";
            this.btnValidateStep.UseVisualStyleBackColor = true;
            this.btnValidateStep.Click += new System.EventHandler(this.btnValidateStep_Click);
            // 
            // lblStepResult
            // 
            this.lblStepResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStepResult.Location = new System.Drawing.Point(36, 43);
            this.lblStepResult.Name = "lblStepResult";
            this.lblStepResult.Size = new System.Drawing.Size(197, 62);
            this.lblStepResult.TabIndex = 0;
            this.lblStepResult.Text = "label6";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Controls.Add(this.tbSelectFrom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(575, 159);
            this.panel2.TabIndex = 0;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 89);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.63636F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(379, 51);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tbSourceIdentityCol
            // 
            this.tbSourceIdentityCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSourceIdentityCol.Location = new System.Drawing.Point(3, 21);
            this.tbSourceIdentityCol.Name = "tbSourceIdentityCol";
            this.tbSourceIdentityCol.Size = new System.Drawing.Size(183, 20);
            this.tbSourceIdentityCol.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Source Identity Column:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Dest Identity Column:";
            // 
            // tbDestIdentityCol
            // 
            this.tbDestIdentityCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDestIdentityCol.Location = new System.Drawing.Point(192, 21);
            this.tbDestIdentityCol.Name = "tbDestIdentityCol";
            this.tbDestIdentityCol.Size = new System.Drawing.Size(184, 20);
            this.tbDestIdentityCol.TabIndex = 5;
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
            this.tbSelectFrom.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSelectFrom.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbSelectFrom.IsReplaceMode = false;
            this.tbSelectFrom.Language = FastColoredTextBoxNS.Language.SQL;
            this.tbSelectFrom.LeftBracket = '(';
            this.tbSelectFrom.Location = new System.Drawing.Point(0, 16);
            this.tbSelectFrom.Name = "tbSelectFrom";
            this.tbSelectFrom.Paddings = new System.Windows.Forms.Padding(0);
            this.tbSelectFrom.RightBracket = ')';
            this.tbSelectFrom.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbSelectFrom.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbSelectFrom.ServiceColors")));
            this.tbSelectFrom.Size = new System.Drawing.Size(575, 67);
            this.tbSelectFrom.TabIndex = 1;
            this.tbSelectFrom.Zoom = 100;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(575, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select From:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "success");
            this.imageList1.Images.SetKeyName(1, "fail");
            this.imageList1.Images.SetKeyName(2, "loading");
            // 
            // frmMigrationBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 432);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmMigrationBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Migrator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMigrationBuilder_FormClosing);
            this.Load += new System.EventHandler(this.frmMigrationBuilder_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSteps)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbValidation)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSourceConnection;
        private System.Windows.Forms.ComboBox cbDestConnection;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSteps;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestTable;
        private System.Windows.Forms.Panel panel2;
        private FastColoredTextBoxNS.FastColoredTextBox tbSelectFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMapFrom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnBuildColumns;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamVal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbSourceIdentityCol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDestIdentityCol;
        private System.Windows.Forms.PictureBox pbValidation;
        private System.Windows.Forms.Label lblStepResult;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnValidateStep;
        private System.Windows.Forms.LinkLabel llInsertSql;
        private System.Windows.Forms.LinkLabel llSourceSql;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnRun;
    }
}