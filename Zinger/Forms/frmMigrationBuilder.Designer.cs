
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
            this.btnImportKeyMap = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSteps = new System.Windows.Forms.DataGridView();
            this.colOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvParams = new System.Windows.Forms.DataGridView();
            this.colParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.migrationStep1 = new Zinger.Controls.MigrationStep();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.tslProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslCancel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRefreshProgress = new System.Windows.Forms.Button();
            this.pbValidation = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblStepResult = new System.Windows.Forms.Label();
            this.btnValidateStep = new System.Windows.Forms.Button();
            this.llSourceSql = new System.Windows.Forms.LinkLabel();
            this.btnAddStepColumns = new System.Windows.Forms.Button();
            this.llInsertSql = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.llImportKeyMap = new System.Windows.Forms.LinkLabel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbValidation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.btnBuildColumns,
            this.btnImportKeyMap});
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
            // btnImportKeyMap
            // 
            this.btnImportKeyMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnImportKeyMap.Image = ((System.Drawing.Image)(resources.GetObject("btnImportKeyMap.Image")));
            this.btnImportKeyMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportKeyMap.Name = "btnImportKeyMap";
            this.btnImportKeyMap.Size = new System.Drawing.Size(126, 22);
            this.btnImportKeyMap.Text = "Import Key Map Table";
            this.btnImportKeyMap.Click += new System.EventHandler(this.btnImportKeyMap_Click);
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
            this.splitContainer1.Size = new System.Drawing.Size(832, 386);
            this.splitContainer1.SplitterDistance = 291;
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
            this.tabControl1.Size = new System.Drawing.Size(291, 386);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvSteps);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(283, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Steps";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvSteps
            // 
            this.dgvSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrder,
            this.colDestTable,
            this.colDescription});
            this.dgvSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSteps.Location = new System.Drawing.Point(3, 3);
            this.dgvSteps.Name = "dgvSteps";
            this.dgvSteps.Size = new System.Drawing.Size(277, 354);
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
            this.colDestTable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDestTable.DataPropertyName = "DestTable";
            this.colDestTable.HeaderText = "Into Table";
            this.colDestTable.Name = "colDestTable";
            this.colDestTable.Width = 80;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvParams);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(283, 360);
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
            this.dgvParams.Size = new System.Drawing.Size(277, 354);
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
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.migrationStep1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel2.Controls.Add(this.pbValidation);
            this.splitContainer2.Panel2.Controls.Add(this.btnRun);
            this.splitContainer2.Panel2.Controls.Add(this.lblStepResult);
            this.splitContainer2.Panel2.Controls.Add(this.btnValidateStep);
            this.splitContainer2.Panel2.Controls.Add(this.llSourceSql);
            this.splitContainer2.Panel2.Controls.Add(this.btnAddStepColumns);
            this.splitContainer2.Panel2.Controls.Add(this.llInsertSql);
            this.splitContainer2.Size = new System.Drawing.Size(537, 386);
            this.splitContainer2.SplitterDistance = 261;
            this.splitContainer2.TabIndex = 14;
            // 
            // migrationStep1
            // 
            this.migrationStep1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.migrationStep1.Location = new System.Drawing.Point(0, 0);
            this.migrationStep1.Name = "migrationStep1";
            this.migrationStep1.Size = new System.Drawing.Size(261, 386);
            this.migrationStep1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "success");
            this.imageList1.Images.SetKeyName(1, "fail");
            this.imageList1.Images.SetKeyName(2, "loading");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbMain,
            this.tslProgress,
            this.tslCancel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 470);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(832, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pbMain
            // 
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(300, 16);
            this.pbMain.Visible = false;
            // 
            // tslProgress
            // 
            this.tslProgress.Name = "tslProgress";
            this.tslProgress.Size = new System.Drawing.Size(39, 17);
            this.tslProgress.Text = "Ready";
            // 
            // tslCancel
            // 
            this.tslCancel.IsLink = true;
            this.tslCancel.Name = "tslCancel";
            this.tslCancel.Size = new System.Drawing.Size(41, 17);
            this.tslCancel.Text = "cancel";
            this.tslCancel.Visible = false;
            this.tslCancel.Click += new System.EventHandler(this.tslCancel_Click);
            // 
            // btnRefreshProgress
            // 
            this.btnRefreshProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshProgress.Location = new System.Drawing.Point(151, 13);
            this.btnRefreshProgress.Name = "btnRefreshProgress";
            this.btnRefreshProgress.Size = new System.Drawing.Size(80, 23);
            this.btnRefreshProgress.TabIndex = 22;
            this.btnRefreshProgress.Text = "Refresh";
            this.btnRefreshProgress.UseVisualStyleBackColor = true;
            this.btnRefreshProgress.Click += new System.EventHandler(this.btnRefreshProgress_Click);
            // 
            // pbValidation
            // 
            this.pbValidation.Location = new System.Drawing.Point(11, 42);
            this.pbValidation.Name = "pbValidation";
            this.pbValidation.Size = new System.Drawing.Size(17, 20);
            this.pbValidation.TabIndex = 16;
            this.pbValidation.TabStop = false;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(190, 11);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(71, 23);
            this.btnRun.TabIndex = 20;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblStepResult
            // 
            this.lblStepResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStepResult.Location = new System.Drawing.Point(34, 42);
            this.lblStepResult.Name = "lblStepResult";
            this.lblStepResult.Size = new System.Drawing.Size(227, 81);
            this.lblStepResult.TabIndex = 15;
            this.lblStepResult.Text = "label6";
            // 
            // btnValidateStep
            // 
            this.btnValidateStep.Location = new System.Drawing.Point(11, 11);
            this.btnValidateStep.Name = "btnValidateStep";
            this.btnValidateStep.Size = new System.Drawing.Size(71, 23);
            this.btnValidateStep.TabIndex = 17;
            this.btnValidateStep.Text = "Test";
            this.btnValidateStep.UseVisualStyleBackColor = true;
            this.btnValidateStep.Click += new System.EventHandler(this.btnValidateStep_Click);
            // 
            // llSourceSql
            // 
            this.llSourceSql.AutoSize = true;
            this.llSourceSql.Location = new System.Drawing.Point(34, 129);
            this.llSourceSql.Name = "llSourceSql";
            this.llSourceSql.Size = new System.Drawing.Size(65, 13);
            this.llSourceSql.TabIndex = 18;
            this.llSourceSql.TabStop = true;
            this.llSourceSql.Text = "Source SQL";
            this.llSourceSql.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSourceSql_LinkClicked);
            // 
            // btnAddStepColumns
            // 
            this.btnAddStepColumns.Location = new System.Drawing.Point(37, 150);
            this.btnAddStepColumns.Name = "btnAddStepColumns";
            this.btnAddStepColumns.Size = new System.Drawing.Size(115, 23);
            this.btnAddStepColumns.TabIndex = 21;
            this.btnAddStepColumns.Text = "Add Columns";
            this.btnAddStepColumns.UseVisualStyleBackColor = true;
            this.btnAddStepColumns.Click += new System.EventHandler(this.btnAddStepColumns_Click);
            // 
            // llInsertSql
            // 
            this.llInsertSql.AutoSize = true;
            this.llInsertSql.Location = new System.Drawing.Point(105, 129);
            this.llInsertSql.Name = "llInsertSql";
            this.llInsertSql.Size = new System.Drawing.Size(57, 13);
            this.llInsertSql.TabIndex = 19;
            this.llInsertSql.TabStop = true;
            this.llInsertSql.Text = "Insert SQL";
            this.llInsertSql.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llInsertSql_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.propertyGrid1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(11, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 183);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.Location = new System.Drawing.Point(7, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 46);
            this.label3.TabIndex = 23;
            this.label3.Text = "To get accurate progress info, make sure to import the latest Key Map table first" +
    ".";
            // 
            // llImportKeyMap
            // 
            this.llImportKeyMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llImportKeyMap.AutoSize = true;
            this.llImportKeyMap.Location = new System.Drawing.Point(150, 39);
            this.llImportKeyMap.Name = "llImportKeyMap";
            this.llImportKeyMap.Size = new System.Drawing.Size(81, 13);
            this.llImportKeyMap.TabIndex = 24;
            this.llImportKeyMap.TabStop = true;
            this.llImportKeyMap.Text = "Import Key Map";
            this.llImportKeyMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llImportKeyMap_LinkClicked);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 85);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(243, 95);
            this.propertyGrid1.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnRefreshProgress);
            this.panel1.Controls.Add(this.llImportKeyMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 69);
            this.panel1.TabIndex = 26;
            // 
            // frmMigrationBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 492);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbValidation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnBuildColumns;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamVal;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.ToolStripButton btnImportKeyMap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pbMain;
        private System.Windows.Forms.ToolStripStatusLabel tslProgress;
        private System.Windows.Forms.ToolStripStatusLabel tslCancel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Controls.MigrationStep migrationStep1;
        private System.Windows.Forms.Button btnRefreshProgress;
        private System.Windows.Forms.PictureBox pbValidation;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStepResult;
        private System.Windows.Forms.Button btnValidateStep;
        private System.Windows.Forms.LinkLabel llSourceSql;
        private System.Windows.Forms.Button btnAddStepColumns;
        private System.Windows.Forms.LinkLabel llInsertSql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llImportKeyMap;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel panel1;
    }
}