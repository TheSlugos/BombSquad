namespace BombSquad
{
    partial class Form1
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
            this.mouseButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHighScores = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mouseButtonTimer
            // 
            this.mouseButtonTimer.Interval = 20;
            this.mouseButtonTimer.Tick += new System.EventHandler(this.mouseButtonTimer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(426, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.toolStripMenuItem1,
            this.levelToolStripMenuItem,
            this.menuHighScores,
            this.toolStripMenuItem2,
            this.menuExit});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(70, 29);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuNew.Size = new System.Drawing.Size(210, 30);
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.menuClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEasy,
            this.menuMedium,
            this.menuHard});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // menuEasy
            // 
            this.menuEasy.Name = "menuEasy";
            this.menuEasy.Size = new System.Drawing.Size(162, 30);
            this.menuEasy.Text = "Easy";
            this.menuEasy.Click += new System.EventHandler(this.menuClick);
            // 
            // menuMedium
            // 
            this.menuMedium.Name = "menuMedium";
            this.menuMedium.Size = new System.Drawing.Size(162, 30);
            this.menuMedium.Text = "Medium";
            this.menuMedium.Click += new System.EventHandler(this.menuClick);
            // 
            // menuHard
            // 
            this.menuHard.Name = "menuHard";
            this.menuHard.Size = new System.Drawing.Size(162, 30);
            this.menuHard.Text = "Hard";
            this.menuHard.Click += new System.EventHandler(this.menuClick);
            // 
            // menuHighScores
            // 
            this.menuHighScores.Name = "menuHighScores";
            this.menuHighScores.Size = new System.Drawing.Size(210, 30);
            this.menuHighScores.Text = "High Scores...";
            this.menuHighScores.Click += new System.EventHandler(this.menuClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(210, 30);
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.menuClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 402);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer mouseButtonTimer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuEasy;
        private System.Windows.Forms.ToolStripMenuItem menuMedium;
        private System.Windows.Forms.ToolStripMenuItem menuHard;
        private System.Windows.Forms.ToolStripMenuItem menuHighScores;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
    }
}

