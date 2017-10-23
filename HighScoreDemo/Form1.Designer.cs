namespace HighScoreDemo
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScore1 = new System.Windows.Forms.TextBox();
            this.txtScore2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtScore3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(111, 291);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(117, 34);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(247, 291);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 34);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name 1";
            // 
            // txtName1
            // 
            this.txtName1.Location = new System.Drawing.Point(102, 47);
            this.txtName1.Name = "txtName1";
            this.txtName1.Size = new System.Drawing.Size(128, 26);
            this.txtName1.TabIndex = 3;
            this.txtName1.Text = "Dummy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Score 1";
            // 
            // txtScore1
            // 
            this.txtScore1.Location = new System.Drawing.Point(315, 47);
            this.txtScore1.Name = "txtScore1";
            this.txtScore1.Size = new System.Drawing.Size(128, 26);
            this.txtScore1.TabIndex = 5;
            this.txtScore1.Text = "999";
            // 
            // txtScore2
            // 
            this.txtScore2.Location = new System.Drawing.Point(315, 101);
            this.txtScore2.Name = "txtScore2";
            this.txtScore2.Size = new System.Drawing.Size(128, 26);
            this.txtScore2.TabIndex = 9;
            this.txtScore2.Text = "999";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Score 2";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(102, 101);
            this.txtName2.Name = "txtName2";
            this.txtName2.Size = new System.Drawing.Size(128, 26);
            this.txtName2.TabIndex = 7;
            this.txtName2.Text = "Dummy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name 2";
            // 
            // txtScore3
            // 
            this.txtScore3.Location = new System.Drawing.Point(315, 154);
            this.txtScore3.Name = "txtScore3";
            this.txtScore3.Size = new System.Drawing.Size(128, 26);
            this.txtScore3.TabIndex = 13;
            this.txtScore3.Text = "999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Score 3";
            // 
            // txtName3
            // 
            this.txtName3.Location = new System.Drawing.Point(102, 154);
            this.txtName3.Name = "txtName3";
            this.txtName3.Size = new System.Drawing.Size(128, 26);
            this.txtName3.TabIndex = 11;
            this.txtName3.Text = "Dummy";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Name 3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 362);
            this.Controls.Add(this.txtScore3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtName3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtScore2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtScore1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtScore1;
        private System.Windows.Forms.TextBox txtScore2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtScore3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName3;
        private System.Windows.Forms.Label label6;
    }
}

