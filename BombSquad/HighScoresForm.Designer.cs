namespace BombSquad
{
    partial class HighScoresForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEasyName = new System.Windows.Forms.Label();
            this.lblEasyScore = new System.Windows.Forms.Label();
            this.lblMediumScore = new System.Windows.Forms.Label();
            this.lblMediumName = new System.Windows.Forms.Label();
            this.lblHardScore = new System.Windows.Forms.Label();
            this.lblHardName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(624, 72);
            this.label1.TabIndex = 0;
            this.label1.Text = "BombSquad Hall of Fame";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(624, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Easy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(624, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "Medium";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(624, 38);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hard";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEasyName
            // 
            this.lblEasyName.Location = new System.Drawing.Point(12, 123);
            this.lblEasyName.Name = "lblEasyName";
            this.lblEasyName.Size = new System.Drawing.Size(312, 38);
            this.lblEasyName.TabIndex = 4;
            this.lblEasyName.Text = "Easy Name";
            this.lblEasyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEasyScore
            // 
            this.lblEasyScore.Location = new System.Drawing.Point(324, 123);
            this.lblEasyScore.Name = "lblEasyScore";
            this.lblEasyScore.Size = new System.Drawing.Size(312, 38);
            this.lblEasyScore.TabIndex = 5;
            this.lblEasyScore.Text = "Easy Score";
            this.lblEasyScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMediumScore
            // 
            this.lblMediumScore.Location = new System.Drawing.Point(324, 236);
            this.lblMediumScore.Name = "lblMediumScore";
            this.lblMediumScore.Size = new System.Drawing.Size(312, 38);
            this.lblMediumScore.TabIndex = 7;
            this.lblMediumScore.Text = "Medium Score";
            this.lblMediumScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMediumName
            // 
            this.lblMediumName.Location = new System.Drawing.Point(12, 236);
            this.lblMediumName.Name = "lblMediumName";
            this.lblMediumName.Size = new System.Drawing.Size(312, 38);
            this.lblMediumName.TabIndex = 6;
            this.lblMediumName.Text = "Medium Name";
            this.lblMediumName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHardScore
            // 
            this.lblHardScore.Location = new System.Drawing.Point(324, 349);
            this.lblHardScore.Name = "lblHardScore";
            this.lblHardScore.Size = new System.Drawing.Size(312, 38);
            this.lblHardScore.TabIndex = 9;
            this.lblHardScore.Text = "Hard Score";
            this.lblHardScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHardName
            // 
            this.lblHardName.Location = new System.Drawing.Point(12, 349);
            this.lblHardName.Name = "lblHardName";
            this.lblHardName.Size = new System.Drawing.Size(312, 38);
            this.lblHardName.TabIndex = 8;
            this.lblHardName.Text = "Hard Name";
            this.lblHardName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(239, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 51);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HighScoresForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(648, 521);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblHardScore);
            this.Controls.Add(this.lblHardName);
            this.Controls.Add(this.lblMediumScore);
            this.Controls.Add(this.lblMediumName);
            this.Controls.Add(this.lblEasyScore);
            this.Controls.Add(this.lblEasyName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HighScoresForm";
            this.Text = "HighScoresForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEasyName;
        private System.Windows.Forms.Label lblEasyScore;
        private System.Windows.Forms.Label lblMediumScore;
        private System.Windows.Forms.Label lblMediumName;
        private System.Windows.Forms.Label lblHardScore;
        private System.Windows.Forms.Label lblHardName;
        private System.Windows.Forms.Button button1;
    }
}