namespace LetsgoFarm_Web
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.numSeconds = new System.Windows.Forms.NumericUpDown();
            this.Timelabel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Titlelabel = new System.Windows.Forms.Label();
            this.WindowTitleTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // numSeconds
            // 
            this.numSeconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numSeconds.Font = new System.Drawing.Font("宋体", 12F);
            this.numSeconds.Location = new System.Drawing.Point(93, 9);
            this.numSeconds.Margin = new System.Windows.Forms.Padding(0);
            this.numSeconds.Maximum = new decimal(new int[] {
            595,
            0,
            0,
            0});
            this.numSeconds.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numSeconds.Name = "numSeconds";
            this.numSeconds.Size = new System.Drawing.Size(60, 22);
            this.numSeconds.TabIndex = 0;
            this.numSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numSeconds.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // Timelabel
            // 
            this.Timelabel.AutoSize = true;
            this.Timelabel.Font = new System.Drawing.Font("宋体", 12F);
            this.Timelabel.Location = new System.Drawing.Point(9, 11);
            this.Timelabel.Margin = new System.Windows.Forms.Padding(0);
            this.Timelabel.Name = "Timelabel";
            this.Timelabel.Size = new System.Drawing.Size(71, 16);
            this.Timelabel.TabIndex = 1;
            this.Timelabel.Text = "循环时间";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(241, 200);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 50);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(53, 200);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Titlelabel
            // 
            this.Titlelabel.AutoSize = true;
            this.Titlelabel.Font = new System.Drawing.Font("宋体", 12F);
            this.Titlelabel.Location = new System.Drawing.Point(9, 47);
            this.Titlelabel.Margin = new System.Windows.Forms.Padding(0);
            this.Titlelabel.Name = "Titlelabel";
            this.Titlelabel.Size = new System.Drawing.Size(71, 16);
            this.Titlelabel.TabIndex = 4;
            this.Titlelabel.Text = "窗口标题";
            // 
            // WindowTitleTextBox
            // 
            this.WindowTitleTextBox.Font = new System.Drawing.Font("宋体", 12F);
            this.WindowTitleTextBox.Location = new System.Drawing.Point(93, 42);
            this.WindowTitleTextBox.Name = "WindowTitleTextBox";
            this.WindowTitleTextBox.Size = new System.Drawing.Size(288, 26);
            this.WindowTitleTextBox.TabIndex = 5;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.WindowTitleTextBox);
            this.Controls.Add(this.Titlelabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Timelabel);
            this.Controls.Add(this.numSeconds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            ((System.ComponentModel.ISupportInitialize)(this.numSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numSeconds;
        private System.Windows.Forms.Label Timelabel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label Titlelabel;
        private System.Windows.Forms.TextBox WindowTitleTextBox;
    }
}