namespace Zinger.Controls
{
	partial class ResultClassBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultClassBuilder));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbQueryName = new System.Windows.Forms.ToolStripTextBox();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tbResultClass = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tbQueryClass = new FastColoredTextBoxNS.FastColoredTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkTestableQuery = new System.Windows.Forms.CheckBox();
            this.webUrlLinkLabel1 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.webUrlLinkLabel2 = new WinForms.Library.Controls.WebUrlLinkLabel();
            this.toolStrip2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbResultClass)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbQueryClass)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tbQueryName,
            this.btnCopy});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(482, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel1.Text = "Query Name:";
            // 
            // tbQueryName
            // 
            this.tbQueryName.Name = "tbQueryName";
            this.tbQueryName.Size = new System.Drawing.Size(200, 25);
            this.tbQueryName.TextChanged += new System.EventHandler(this.tbQueryName_TextChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 25);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(482, 233);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tbResultClass);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(474, 207);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Result Class";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tbResultClass
            // 
            this.tbResultClass.AutoCompleteBracketsList = new char[] {
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
            this.tbResultClass.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.tbResultClass.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbResultClass.BackBrush = null;
            this.tbResultClass.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.tbResultClass.CharHeight = 14;
            this.tbResultClass.CharWidth = 8;
            this.tbResultClass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbResultClass.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbResultClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResultClass.IsReplaceMode = false;
            this.tbResultClass.Language = FastColoredTextBoxNS.Language.CSharp;
            this.tbResultClass.LeftBracket = '(';
            this.tbResultClass.LeftBracket2 = '{';
            this.tbResultClass.Location = new System.Drawing.Point(3, 3);
            this.tbResultClass.Name = "tbResultClass";
            this.tbResultClass.Paddings = new System.Windows.Forms.Padding(0);
            this.tbResultClass.RightBracket = ')';
            this.tbResultClass.RightBracket2 = '}';
            this.tbResultClass.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbResultClass.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbResultClass.ServiceColors")));
            this.tbResultClass.Size = new System.Drawing.Size(468, 201);
            this.tbResultClass.TabIndex = 0;
            this.tbResultClass.Zoom = 100;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tbQueryClass);
            this.tabPage6.Controls.Add(this.panel1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(474, 207);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Query Class";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tbQueryClass
            // 
            this.tbQueryClass.AutoCompleteBracketsList = new char[] {
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
            this.tbQueryClass.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.tbQueryClass.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbQueryClass.BackBrush = null;
            this.tbQueryClass.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.tbQueryClass.CharHeight = 14;
            this.tbQueryClass.CharWidth = 8;
            this.tbQueryClass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbQueryClass.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbQueryClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQueryClass.IsReplaceMode = false;
            this.tbQueryClass.Language = FastColoredTextBoxNS.Language.CSharp;
            this.tbQueryClass.LeftBracket = '(';
            this.tbQueryClass.LeftBracket2 = '{';
            this.tbQueryClass.Location = new System.Drawing.Point(3, 63);
            this.tbQueryClass.Name = "tbQueryClass";
            this.tbQueryClass.Paddings = new System.Windows.Forms.Padding(0);
            this.tbQueryClass.ReadOnly = true;
            this.tbQueryClass.RightBracket = ')';
            this.tbQueryClass.RightBracket2 = '}';
            this.tbQueryClass.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbQueryClass.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbQueryClass.ServiceColors")));
            this.tbQueryClass.Size = new System.Drawing.Size(468, 141);
            this.tbQueryClass.TabIndex = 0;
            this.tbQueryClass.Zoom = 100;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webUrlLinkLabel2);
            this.panel1.Controls.Add(this.chkTestableQuery);
            this.panel1.Controls.Add(this.webUrlLinkLabel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 60);
            this.panel1.TabIndex = 1;
            // 
            // chkTestableQuery
            // 
            this.chkTestableQuery.AutoSize = true;
            this.chkTestableQuery.Location = new System.Drawing.Point(33, 34);
            this.chkTestableQuery.Name = "chkTestableQuery";
            this.chkTestableQuery.Size = new System.Drawing.Size(158, 17);
            this.chkTestableQuery.TabIndex = 2;
            this.chkTestableQuery.Text = "Use TestableQuery<T>";
            this.chkTestableQuery.UseVisualStyleBackColor = true;
            // 
            // webUrlLinkLabel1
            // 
            this.webUrlLinkLabel1.AutoSize = true;
            this.webUrlLinkLabel1.Location = new System.Drawing.Point(409, 10);
            this.webUrlLinkLabel1.Name = "webUrlLinkLabel1";
            this.webUrlLinkLabel1.Size = new System.Drawing.Size(45, 13);
            this.webUrlLinkLabel1.TabIndex = 1;
            this.webUrlLinkLabel1.TabStop = true;
            this.webUrlLinkLabel1.Text = "GitHub";
            this.webUrlLinkLabel1.Url = "https://github.com/adamfoneil/Dapper.QX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This generated code is intended for use with Dapper.QX";
            // 
            // webUrlLinkLabel2
            // 
            this.webUrlLinkLabel2.AutoSize = true;
            this.webUrlLinkLabel2.Location = new System.Drawing.Point(197, 35);
            this.webUrlLinkLabel2.Name = "webUrlLinkLabel2";
            this.webUrlLinkLabel2.Size = new System.Drawing.Size(70, 13);
            this.webUrlLinkLabel2.TabIndex = 3;
            this.webUrlLinkLabel2.TabStop = true;
            this.webUrlLinkLabel2.Text = "learn more";
            this.webUrlLinkLabel2.Url = "https://github.com/adamfoneil/Dapper.QX#testing";
            // 
            // ResultClassBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ResultClassBuilder";
            this.Size = new System.Drawing.Size(482, 258);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbResultClass)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbQueryClass)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox tbQueryName;
		private System.Windows.Forms.ToolStripButton btnCopy;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private FastColoredTextBoxNS.FastColoredTextBox tbQueryClass;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private FastColoredTextBoxNS.FastColoredTextBox tbResultClass;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel1;
        private System.Windows.Forms.CheckBox chkTestableQuery;
        private WinForms.Library.Controls.WebUrlLinkLabel webUrlLinkLabel2;
    }
}
