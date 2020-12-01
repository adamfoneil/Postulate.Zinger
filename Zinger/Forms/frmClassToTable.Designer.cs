
namespace Zinger.Forms
{
    partial class frmClassToTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClassToTable));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbCSharpInput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tbSQLOutput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCSharpInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSQLOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 34);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(554, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create (or paste) a C# class in the left pane. A database table will be created o" +
    "n the right side.";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbCSharpInput);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbSQLOutput);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(717, 416);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 1;
            // 
            // tbCSharpInput
            // 
            this.tbCSharpInput.AutoCompleteBracketsList = new char[] {
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
            this.tbCSharpInput.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbCSharpInput.BackBrush = null;
            this.tbCSharpInput.CharHeight = 14;
            this.tbCSharpInput.CharWidth = 8;
            this.tbCSharpInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbCSharpInput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbCSharpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCSharpInput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbCSharpInput.IsReplaceMode = false;
            this.tbCSharpInput.Language = FastColoredTextBoxNS.Language.CSharp;
            this.tbCSharpInput.Location = new System.Drawing.Point(0, 25);
            this.tbCSharpInput.Name = "tbCSharpInput";
            this.tbCSharpInput.Paddings = new System.Windows.Forms.Padding(0);
            this.tbCSharpInput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbCSharpInput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbCSharpInput.ServiceColors")));
            this.tbCSharpInput.Size = new System.Drawing.Size(340, 391);
            this.tbCSharpInput.TabIndex = 0;
            this.tbCSharpInput.Zoom = 100;
            this.tbCSharpInput.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.tbCSharpInput_TextChangedDelayed);
            // 
            // tbSQLOutput
            // 
            this.tbSQLOutput.AutoCompleteBracketsList = new char[] {
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
            this.tbSQLOutput.AutoIndentCharsPatterns = "";
            this.tbSQLOutput.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.tbSQLOutput.BackBrush = null;
            this.tbSQLOutput.CharHeight = 14;
            this.tbSQLOutput.CharWidth = 8;
            this.tbSQLOutput.CommentPrefix = "--";
            this.tbSQLOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSQLOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbSQLOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSQLOutput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbSQLOutput.IsReplaceMode = false;
            this.tbSQLOutput.Language = FastColoredTextBoxNS.Language.SQL;
            this.tbSQLOutput.LeftBracket = '(';
            this.tbSQLOutput.Location = new System.Drawing.Point(0, 25);
            this.tbSQLOutput.Name = "tbSQLOutput";
            this.tbSQLOutput.Paddings = new System.Windows.Forms.Padding(0);
            this.tbSQLOutput.RightBracket = ')';
            this.tbSQLOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.tbSQLOutput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("tbSQLOutput.ServiceColors")));
            this.tbSQLOutput.Size = new System.Drawing.Size(373, 391);
            this.tbSQLOutput.TabIndex = 0;
            this.tbSQLOutput.Zoom = 100;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(340, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(373, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // frmClassToTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmClassToTable";
            this.Text = "C# Class to Table";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbCSharpInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSQLOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox tbCSharpInput;
        private FastColoredTextBoxNS.FastColoredTextBox tbSQLOutput;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
    }
}