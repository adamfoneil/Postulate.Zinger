
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.tslProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslCancel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnAddStepColumns = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.llInsertSql = new System.Windows.Forms.LinkLabel();
            this.pbValidation = new System.Windows.Forms.PictureBox();
            this.llSourceSql = new System.Windows.Forms.LinkLabel();
            this.btnValidateStep = new System.Windows.Forms.Button();
            this.lblStepResult = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.migrationStep1 = new Zinger.Controls.MigrationStep();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnRefreshProgress = new System.Windows.Forms.Button();
            this.dgvProgress = new System.Windows.Forms.DataGridView();
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
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbValidation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgress)).BeginInit();
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
            this.tabPage2.Size = new System.Drawing.Size(333, 300);
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
            this.dgvParams.Size = new System.Drawing.Size(327, 294);
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
            // btnAddStepColumns
            // 
            this.btnAddStepColumns.Location = new System.Drawing.Point(32, 151);
            this.btnAddStepColumns.Name = "btnAddStepColumns";
            this.btnAddStepColumns.Size = new System.Drawing.Size(115, 23);
            this.btnAddStepColumns.TabIndex = 13;
            this.btnAddStepColumns.Text = "Add Columns";
            this.btnAddStepColumns.UseVisualStyleBackColor = true;
            this.btnAddStepColumns.Click += new System.EventHandler(this.btnAddStepColumns_Click);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(185, 6);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(71, 23);
            this.btnRun.TabIndex = 12;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // llInsertSql
            // 
            this.llInsertSql.AutoSize = true;
            this.llInsertSql.Location = new System.Drawing.Point(100, 124);
            this.llInsertSql.Name = "llInsertSql";
            this.llInsertSql.Size = new System.Drawing.Size(57, 13);
            this.llInsertSql.TabIndex = 11;
            this.llInsertSql.TabStop = true;
            this.llInsertSql.Text = "Insert SQL";
            this.llInsertSql.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llInsertSql_LinkClicked);
            // 
            // pbValidation
            // 
            this.pbValidation.Location = new System.Drawing.Point(6, 35);
            this.pbValidation.Name = "pbValidation";
            this.pbValidation.Size = new System.Drawing.Size(17, 20);
            this.pbValidation.TabIndex = 8;
            this.pbValidation.TabStop = false;
            // 
            // llSourceSql
            // 
            this.llSourceSql.AutoSize = true;
            this.llSourceSql.Location = new System.Drawing.Point(29, 124);
            this.llSourceSql.Name = "llSourceSql";
            this.llSourceSql.Size = new System.Drawing.Size(65, 13);
            this.llSourceSql.TabIndex = 10;
            this.llSourceSql.TabStop = true;
            this.llSourceSql.Text = "Source SQL";
            this.llSourceSql.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSourceSql_LinkClicked);
            // 
            // btnValidateStep
            // 
            this.btnValidateStep.Location = new System.Drawing.Point(6, 6);
            this.btnValidateStep.Name = "btnValidateStep";
            this.btnValidateStep.Size = new System.Drawing.Size(71, 23);
            this.btnValidateStep.TabIndex = 9;
            this.btnValidateStep.Text = "Test";
            this.btnValidateStep.UseVisualStyleBackColor = true;
            this.btnValidateStep.Click += new System.EventHandler(this.btnValidateStep_Click);
            // 
            // lblStepResult
            // 
            this.lblStepResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStepResult.Location = new System.Drawing.Point(29, 35);
            this.lblStepResult.Name = "lblStepResult";
            this.lblStepResult.Size = new System.Drawing.Size(227, 83);
            this.lblStepResult.TabIndex = 7;
            this.lblStepResult.Text = "label6";
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
            this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
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
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(272, 386);
            this.tabControl2.TabIndex = 14;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pbValidation);
            this.tabPage3.Controls.Add(this.btnRun);
            this.tabPage3.Controls.Add(this.lblStepResult);
            this.tabPage3.Controls.Add(this.btnValidateStep);
            this.tabPage3.Controls.Add(this.llSourceSql);
            this.tabPage3.Controls.Add(this.btnAddStepColumns);
            this.tabPage3.Controls.Add(this.llInsertSql);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(264, 360);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Controls";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvProgress);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(264, 360);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Progress";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnRefreshProgress
            // 
            this.btnRefreshProgress.Location = new System.Drawing.Point(15, 6);
            this.btnRefreshProgress.Name = "btnRefreshProgress";
            this.btnRefreshProgress.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshProgress.TabIndex = 0;
            this.btnRefreshProgress.Text = "Update";
            this.btnRefreshProgress.UseVisualStyleBackColor = true;
            this.btnRefreshProgress.Click += new System.EventHandler(this.btnRefreshProgress_Click);
            // 
            // dgvProgress
            // 
            this.dgvProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProgress.Location = new System.Drawing.Point(3, 41);
            this.dgvProgress.Name = "dgvProgress";
            this.dgvProgress.Size = new System.Drawing.Size(258, 316);
            this.dgvProgress.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefreshProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 38);
            this.panel1.TabIndex = 2;
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbValidation)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgress)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnAddStepColumns;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.LinkLabel llInsertSql;
        private System.Windows.Forms.PictureBox pbValidation;
        private System.Windows.Forms.LinkLabel llSourceSql;
        private System.Windows.Forms.Button btnValidateStep;
        private System.Windows.Forms.Label lblStepResult;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Controls.MigrationStep migrationStep1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnRefreshProgress;
        private System.Windows.Forms.DataGridView dgvProgress;
        private System.Windows.Forms.Panel panel1;
    }
}