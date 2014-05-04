namespace _EmuchSign
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.btnSign = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tool_Note = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMenu_Sign = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tool_About = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.Location = new System.Drawing.Point(3, 27);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(294, 150);
            this.rtxtMsg.TabIndex = 3;
            this.rtxtMsg.Text = "";
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(189, 132);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(75, 23);
            this.btnSign.TabIndex = 2;
            this.btnSign.Text = "签到";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具ToolStripMenuItem,
            this.TSMenu_Sign});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(301, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Tool_Note,
            this.设置ToolStripMenuItem,
            this.Menu_Tool_About});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // Menu_Tool_Note
            // 
            this.Menu_Tool_Note.Name = "Menu_Tool_Note";
            this.Menu_Tool_Note.Size = new System.Drawing.Size(152, 22);
            this.Menu_Tool_Note.Text = "记事本";
            this.Menu_Tool_Note.Click += new System.EventHandler(this.Menu_Tool_Note_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置ToolStripMenuItem.Text = "帐户设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.TSMenu_Set_Click);
            // 
            // TSMenu_Sign
            // 
            this.TSMenu_Sign.Name = "TSMenu_Sign";
            this.TSMenu_Sign.Size = new System.Drawing.Size(44, 21);
            this.TSMenu_Sign.Text = "签到";
            this.TSMenu_Sign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // Menu_Tool_About
            // 
            this.Menu_Tool_About.Name = "Menu_Tool_About";
            this.Menu_Tool_About.Size = new System.Drawing.Size(152, 22);
            this.Menu_Tool_About.Text = "说明";
            this.Menu_Tool_About.Click += new System.EventHandler(this.Menu_Tool_About_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 180);
            this.Controls.Add(this.rtxtMsg);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小木虫签到";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMenu_Sign;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tool_Note;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tool_About;
    }
}