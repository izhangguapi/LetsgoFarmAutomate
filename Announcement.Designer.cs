namespace LetsgoFarmAutomate {
    partial class Announcement {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Announcement));
            labelTitle = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            labelAnnouncement = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Dock = DockStyle.Fill;
            labelTitle.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelTitle.Location = new Point(3, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(528, 36);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "公告";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 151);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(528, 407);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.Location = new Point(3, 112);
            label1.Name = "label1";
            label1.Size = new Size(528, 36);
            label1.TabIndex = 2;
            label1.Text = "自愿赞助";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelAnnouncement
            // 
            labelAnnouncement.AutoSize = true;
            labelAnnouncement.Dock = DockStyle.Fill;
            labelAnnouncement.Location = new Point(3, 36);
            labelAnnouncement.Name = "labelAnnouncement";
            labelAnnouncement.Size = new Size(528, 76);
            labelAnnouncement.TabIndex = 3;
            labelAnnouncement.Text = "脚本目前只有月卡功能可使用，无月卡功能正在磨磨唧唧的开发中...\r\n交流QQ群：1016274395\r\n\r\n 此脚本免费且开源，如果您是从任意渠道购买的，请立即退款、差评并举报！ ";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(labelAnnouncement, 0, 1);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 3);
            tableLayoutPanel1.Controls.Add(labelTitle, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(534, 561);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // Announcement
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 561);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Announcement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "公告";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelTitle;
        private PictureBox pictureBox1;
        private Label label1;
        private Label labelAnnouncement;
        private TableLayoutPanel tableLayoutPanel1;
    }
}