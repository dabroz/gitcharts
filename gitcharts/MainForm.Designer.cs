namespace gitcharts
{
    partial class MainForm
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
            this.cURL = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cWidth = new System.Windows.Forms.TextBox();
            this.cHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cGIT = new System.Windows.Forms.TextBox();
            this.cGnuplot = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cIgnore = new System.Windows.Forms.TextBox();
            this.cInclude = new System.Windows.Forms.TextBox();
            this.c2Level = new System.Windows.Forms.TextBox();
            this.cRevs = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // cURL
            // 
            this.cURL.Location = new System.Drawing.Point(56, 16);
            this.cURL.Name = "cURL";
            this.cURL.Size = new System.Drawing.Size(256, 20);
            this.cURL.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "LOC chart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width";
            // 
            // cWidth
            // 
            this.cWidth.Location = new System.Drawing.Point(56, 48);
            this.cWidth.Name = "cWidth";
            this.cWidth.Size = new System.Drawing.Size(100, 20);
            this.cWidth.TabIndex = 4;
            // 
            // cHeight
            // 
            this.cHeight.Location = new System.Drawing.Point(208, 48);
            this.cHeight.Name = "cHeight";
            this.cHeight.Size = new System.Drawing.Size(100, 20);
            this.cHeight.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Height";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "(enter path to local repository, relative to GitCharts directory)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "git";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "gnuplot";
            // 
            // cGIT
            // 
            this.cGIT.Location = new System.Drawing.Point(56, 88);
            this.cGIT.Name = "cGIT";
            this.cGIT.Size = new System.Drawing.Size(304, 20);
            this.cGIT.TabIndex = 10;
            // 
            // cGnuplot
            // 
            this.cGnuplot.Location = new System.Drawing.Point(56, 120);
            this.cGnuplot.Name = "cGnuplot";
            this.cGnuplot.Size = new System.Drawing.Size(304, 20);
            this.cGnuplot.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(368, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "path to Git";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(368, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "path to Gnuplot";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "ignore";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "include";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 208);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "2level";
            // 
            // cIgnore
            // 
            this.cIgnore.Location = new System.Drawing.Point(56, 160);
            this.cIgnore.Name = "cIgnore";
            this.cIgnore.Size = new System.Drawing.Size(312, 20);
            this.cIgnore.TabIndex = 17;
            // 
            // cInclude
            // 
            this.cInclude.Location = new System.Drawing.Point(56, 184);
            this.cInclude.Name = "cInclude";
            this.cInclude.Size = new System.Drawing.Size(312, 20);
            this.cInclude.TabIndex = 18;
            // 
            // c2Level
            // 
            this.c2Level.Location = new System.Drawing.Point(56, 208);
            this.c2Level.Name = "c2Level";
            this.c2Level.Size = new System.Drawing.Size(312, 20);
            this.c2Level.TabIndex = 19;
            // 
            // cRevs
            // 
            this.cRevs.Location = new System.Drawing.Point(56, 240);
            this.cRevs.Name = "cRevs";
            this.cRevs.Size = new System.Drawing.Size(100, 20);
            this.cRevs.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 240);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "revs";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(376, 160);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(391, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "directories to be ignores, separated by semicolon -- for example: Build;Include;L" +
                "ibs";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(376, 184);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(228, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "extensions to be included separated by comma";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(368, 216);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(408, 32);
            this.label15.TabIndex = 24;
            this.label15.Text = "sep with semicolon; 2 level directories will be included as separate entries. For" +
                " example if you put \"Tools\" here, you will have \"Tools\\A\", \"Tools\\B\" etc on char" +
                "t";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(160, 240);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(169, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "max number of revisions to include";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 463);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cRevs);
            this.Controls.Add(this.c2Level);
            this.Controls.Add(this.cInclude);
            this.Controls.Add(this.cIgnore);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cGnuplot);
            this.Controls.Add(this.cGIT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cURL);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cURL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cWidth;
        private System.Windows.Forms.TextBox cHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cGIT;
        private System.Windows.Forms.TextBox cGnuplot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox cIgnore;
        private System.Windows.Forms.TextBox cInclude;
        private System.Windows.Forms.TextBox c2Level;
        private System.Windows.Forms.TextBox cRevs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}

