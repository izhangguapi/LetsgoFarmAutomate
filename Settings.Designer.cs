
namespace LetsgoFarmAutomate {
    partial class Settings {
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

        protected override bool ProcessDialogKey(Keys keyData) {
            if (keyData == Keys.Escape) {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true; // 表示已处理 ESC 键
            }
            return base.ProcessDialogKey(keyData);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            aquariumTime1 = new DateTimePicker();
            panel1 = new Panel();
            btnCancel = new Button();
            btnDefault = new Button();
            btnOK = new Button();
            lblTitle1 = new Label();
            groupBoxWindow = new GroupBox();
            tableLayoutPanelOther = new TableLayoutPanel();
            checkBoxIsRefresh = new CheckBox();
            lblLoopOrAuto = new Label();
            textBoxTitle = new TextBox();
            textBoxLoop = new TextBox();
            textBoxAutoiInject = new TextBox();
            label1 = new Label();
            textBoxFriendUID = new TextBox();
            lblFriendUID = new Label();
            hotspring = new CheckBox();
            hotspringTime = new DateTimePicker();
            tea = new CheckBox();
            aquariumTime2 = new DateTimePicker();
            aquarium = new CheckBox();
            pray = new CheckBox();
            prayTime = new DateTimePicker();
            panel2 = new Panel();
            prayEXP = new RadioButton();
            prayGold = new RadioButton();
            checkBoxSendKey = new CheckBox();
            textBoxSendKey = new TextBox();
            groupBoxHotspring = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBoxNotice = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            groupBoxPray = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBoxAquarium = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            panel1.SuspendLayout();
            groupBoxWindow.SuspendLayout();
            tableLayoutPanelOther.SuspendLayout();
            panel2.SuspendLayout();
            groupBoxHotspring.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBoxNotice.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBoxPray.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBoxAquarium.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // aquariumTime1
            // 
            aquariumTime1.Anchor = AnchorStyles.None;
            aquariumTime1.CustomFormat = "HH:mm";
            aquariumTime1.Format = DateTimePickerFormat.Custom;
            aquariumTime1.Location = new Point(135, 3);
            aquariumTime1.Name = "aquariumTime1";
            aquariumTime1.ShowUpDown = true;
            aquariumTime1.Size = new Size(100, 23);
            aquariumTime1.TabIndex = 7;
            aquariumTime1.Value = new DateTime(2025, 4, 12, 0, 0, 0, 0);
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnDefault);
            panel1.Controls.Add(btnOK);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 421);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(3);
            panel1.Size = new Size(396, 40);
            panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Dock = DockStyle.Right;
            btnCancel.Location = new Point(243, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 34);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDefault
            // 
            btnDefault.Dock = DockStyle.Left;
            btnDefault.Location = new Point(3, 3);
            btnDefault.Name = "btnDefault";
            btnDefault.Size = new Size(75, 34);
            btnDefault.TabIndex = 12;
            btnDefault.Text = "恢复默认";
            btnDefault.UseVisualStyleBackColor = true;
            btnDefault.Click += BtnDefault_Click;
            // 
            // btnOK
            // 
            btnOK.Dock = DockStyle.Right;
            btnOK.Location = new Point(318, 3);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 34);
            btnOK.TabIndex = 14;
            btnOK.Text = "保存设置";
            btnOK.Click += BtnOK_Click;
            // 
            // lblTitle1
            // 
            lblTitle1.Anchor = AnchorStyles.None;
            lblTitle1.AutoSize = true;
            lblTitle1.Location = new Point(32, 6);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(56, 17);
            lblTitle1.TabIndex = 14;
            lblTitle1.Text = "窗口标题";
            lblTitle1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBoxWindow
            // 
            groupBoxWindow.Controls.Add(tableLayoutPanelOther);
            groupBoxWindow.Location = new Point(5, 5);
            groupBoxWindow.Margin = new Padding(0);
            groupBoxWindow.Name = "groupBoxWindow";
            groupBoxWindow.Size = new Size(386, 112);
            groupBoxWindow.TabIndex = 0;
            groupBoxWindow.TabStop = false;
            groupBoxWindow.Text = "窗口设置";
            // 
            // tableLayoutPanelOther
            // 
            tableLayoutPanelOther.ColumnCount = 3;
            tableLayoutPanelOther.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelOther.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanelOther.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanelOther.Controls.Add(checkBoxIsRefresh, 2, 1);
            tableLayoutPanelOther.Controls.Add(lblTitle1, 0, 0);
            tableLayoutPanelOther.Controls.Add(lblLoopOrAuto, 0, 1);
            tableLayoutPanelOther.Controls.Add(textBoxTitle, 1, 0);
            tableLayoutPanelOther.Controls.Add(textBoxLoop, 1, 1);
            tableLayoutPanelOther.Controls.Add(textBoxAutoiInject, 1, 2);
            tableLayoutPanelOther.Controls.Add(label1, 0, 2);
            tableLayoutPanelOther.Dock = DockStyle.Fill;
            tableLayoutPanelOther.Location = new Point(3, 19);
            tableLayoutPanelOther.Margin = new Padding(0);
            tableLayoutPanelOther.Name = "tableLayoutPanelOther";
            tableLayoutPanelOther.RowCount = 3;
            tableLayoutPanelOther.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelOther.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelOther.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanelOther.Size = new Size(380, 90);
            tableLayoutPanelOther.TabIndex = 0;
            // 
            // checkBoxIsRefresh
            // 
            checkBoxIsRefresh.Anchor = AnchorStyles.None;
            checkBoxIsRefresh.AutoSize = true;
            checkBoxIsRefresh.Location = new Point(292, 49);
            checkBoxIsRefresh.Name = "checkBoxIsRefresh";
            tableLayoutPanelOther.SetRowSpan(checkBoxIsRefresh, 2);
            checkBoxIsRefresh.Size = new Size(75, 21);
            checkBoxIsRefresh.TabIndex = 23;
            checkBoxIsRefresh.Text = "启用刷新";
            checkBoxIsRefresh.UseVisualStyleBackColor = true;
            // 
            // lblLoopOrAuto
            // 
            lblLoopOrAuto.Anchor = AnchorStyles.None;
            lblLoopOrAuto.AutoSize = true;
            lblLoopOrAuto.Location = new Point(32, 36);
            lblLoopOrAuto.Name = "lblLoopOrAuto";
            lblLoopOrAuto.Size = new Size(56, 17);
            lblLoopOrAuto.TabIndex = 19;
            lblLoopOrAuto.Text = "循环秒数";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.None;
            tableLayoutPanelOther.SetColumnSpan(textBoxTitle, 2);
            textBoxTitle.Location = new Point(123, 3);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(254, 23);
            textBoxTitle.TabIndex = 0;
            textBoxTitle.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxLoop
            // 
            textBoxLoop.Anchor = AnchorStyles.None;
            textBoxLoop.Location = new Point(150, 33);
            textBoxLoop.MaxLength = 3;
            textBoxLoop.Name = "textBoxLoop";
            textBoxLoop.Size = new Size(100, 23);
            textBoxLoop.TabIndex = 1;
            textBoxLoop.TextAlign = HorizontalAlignment.Center;
            textBoxLoop.KeyPress += TextBoxLoop_KeyPress;
            // 
            // textBoxAutoiInject
            // 
            textBoxAutoiInject.Anchor = AnchorStyles.None;
            textBoxAutoiInject.Location = new Point(150, 63);
            textBoxAutoiInject.MaxLength = 3;
            textBoxAutoiInject.Name = "textBoxAutoiInject";
            textBoxAutoiInject.Size = new Size(100, 23);
            textBoxAutoiInject.TabIndex = 21;
            textBoxAutoiInject.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(8, 66);
            label1.Name = "label1";
            label1.Size = new Size(104, 17);
            label1.TabIndex = 22;
            label1.Text = "自动注入等待秒数";
            // 
            // textBoxFriendUID
            // 
            textBoxFriendUID.Anchor = AnchorStyles.None;
            textBoxFriendUID.Location = new Point(145, 33);
            textBoxFriendUID.MaxLength = 10;
            textBoxFriendUID.Name = "textBoxFriendUID";
            textBoxFriendUID.Size = new Size(110, 23);
            textBoxFriendUID.TabIndex = 24;
            textBoxFriendUID.TextAlign = HorizontalAlignment.Center;
            // 
            // lblFriendUID
            // 
            lblFriendUID.Anchor = AnchorStyles.None;
            lblFriendUID.AutoSize = true;
            lblFriendUID.Location = new Point(33, 36);
            lblFriendUID.Name = "lblFriendUID";
            lblFriendUID.Size = new Size(54, 17);
            lblFriendUID.TabIndex = 23;
            lblFriendUID.Text = "好友UID";
            lblFriendUID.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hotspring
            // 
            hotspring.Anchor = AnchorStyles.None;
            hotspring.AutoSize = true;
            hotspring.Location = new Point(22, 4);
            hotspring.Name = "hotspring";
            hotspring.Size = new Size(75, 21);
            hotspring.TabIndex = 9;
            hotspring.Text = "是否开启";
            hotspring.UseVisualStyleBackColor = true;
            // 
            // hotspringTime
            // 
            hotspringTime.Anchor = AnchorStyles.None;
            hotspringTime.CustomFormat = "HH:mm";
            hotspringTime.Format = DateTimePickerFormat.Custom;
            hotspringTime.Location = new Point(150, 3);
            hotspringTime.Name = "hotspringTime";
            hotspringTime.ShowUpDown = true;
            hotspringTime.Size = new Size(100, 23);
            hotspringTime.TabIndex = 10;
            hotspringTime.Value = new DateTime(2025, 4, 12, 0, 0, 0, 0);
            // 
            // tea
            // 
            tea.Anchor = AnchorStyles.None;
            tea.AutoSize = true;
            tea.Location = new Point(292, 19);
            tea.Name = "tea";
            tableLayoutPanel1.SetRowSpan(tea, 2);
            tea.Size = new Size(75, 21);
            tea.TabIndex = 11;
            tea.Text = "自动喝茶";
            tea.UseVisualStyleBackColor = true;
            // 
            // aquariumTime2
            // 
            aquariumTime2.Anchor = AnchorStyles.None;
            aquariumTime2.CustomFormat = "HH:mm";
            aquariumTime2.Format = DateTimePickerFormat.Custom;
            aquariumTime2.Location = new Point(265, 3);
            aquariumTime2.Name = "aquariumTime2";
            aquariumTime2.ShowUpDown = true;
            aquariumTime2.Size = new Size(100, 23);
            aquariumTime2.TabIndex = 8;
            aquariumTime2.Value = new DateTime(2025, 4, 12, 0, 0, 0, 0);
            // 
            // aquarium
            // 
            aquarium.Anchor = AnchorStyles.None;
            aquarium.AutoSize = true;
            aquarium.Location = new Point(22, 4);
            aquarium.Name = "aquarium";
            aquarium.Size = new Size(75, 21);
            aquarium.TabIndex = 6;
            aquarium.Text = "是否开启";
            aquarium.UseVisualStyleBackColor = true;
            // 
            // pray
            // 
            pray.Anchor = AnchorStyles.None;
            pray.AutoSize = true;
            pray.Location = new Point(22, 19);
            pray.Name = "pray";
            pray.Size = new Size(75, 21);
            pray.TabIndex = 2;
            pray.Text = "是否开启";
            pray.UseVisualStyleBackColor = true;
            // 
            // prayTime
            // 
            prayTime.Anchor = AnchorStyles.None;
            prayTime.CustomFormat = "HH:mm";
            prayTime.Format = DateTimePickerFormat.Custom;
            prayTime.Location = new Point(135, 18);
            prayTime.Name = "prayTime";
            prayTime.ShowUpDown = true;
            prayTime.Size = new Size(100, 23);
            prayTime.TabIndex = 3;
            prayTime.Value = new DateTime(2025, 4, 12, 0, 0, 0, 0);
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.None;
            panel2.Controls.Add(prayEXP);
            panel2.Controls.Add(prayGold);
            panel2.Location = new Point(275, 9);
            panel2.Name = "panel2";
            panel2.Size = new Size(80, 41);
            panel2.TabIndex = 5;
            // 
            // prayEXP
            // 
            prayEXP.AutoSize = true;
            prayEXP.Dock = DockStyle.Bottom;
            prayEXP.Location = new Point(0, 20);
            prayEXP.Name = "prayEXP";
            prayEXP.Size = new Size(80, 21);
            prayEXP.TabIndex = 0;
            prayEXP.Text = "农场经验";
            prayEXP.UseVisualStyleBackColor = true;
            // 
            // prayGold
            // 
            prayGold.AutoSize = true;
            prayGold.Checked = true;
            prayGold.Dock = DockStyle.Top;
            prayGold.Location = new Point(0, 0);
            prayGold.Name = "prayGold";
            prayGold.Size = new Size(80, 21);
            prayGold.TabIndex = 0;
            prayGold.TabStop = true;
            prayGold.Text = "农场币";
            prayGold.UseVisualStyleBackColor = true;
            // 
            // checkBoxSendKey
            // 
            checkBoxSendKey.Anchor = AnchorStyles.None;
            checkBoxSendKey.AutoSize = true;
            checkBoxSendKey.Location = new Point(21, 4);
            checkBoxSendKey.Name = "checkBoxSendKey";
            checkBoxSendKey.Size = new Size(77, 21);
            checkBoxSendKey.TabIndex = 22;
            checkBoxSendKey.Text = "SendKey";
            checkBoxSendKey.UseVisualStyleBackColor = true;
            checkBoxSendKey.CheckedChanged += CheckBoxSendKey_CheckedChanged;
            // 
            // textBoxSendKey
            // 
            textBoxSendKey.Anchor = AnchorStyles.None;
            textBoxSendKey.Enabled = false;
            textBoxSendKey.Location = new Point(123, 3);
            textBoxSendKey.MaxLength = 34;
            textBoxSendKey.Name = "textBoxSendKey";
            textBoxSendKey.Size = new Size(254, 23);
            textBoxSendKey.TabIndex = 12;
            textBoxSendKey.TextAlign = HorizontalAlignment.Center;
            // 
            // groupBoxHotspring
            // 
            groupBoxHotspring.Controls.Add(tableLayoutPanel1);
            groupBoxHotspring.Location = new Point(5, 266);
            groupBoxHotspring.Margin = new Padding(0);
            groupBoxHotspring.Name = "groupBoxHotspring";
            groupBoxHotspring.Size = new Size(386, 82);
            groupBoxHotspring.TabIndex = 2;
            groupBoxHotspring.TabStop = false;
            groupBoxHotspring.Text = "自动泡温泉设置";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(hotspring, 0, 0);
            tableLayoutPanel1.Controls.Add(hotspringTime, 1, 0);
            tableLayoutPanel1.Controls.Add(tea, 2, 0);
            tableLayoutPanel1.Controls.Add(lblFriendUID, 0, 1);
            tableLayoutPanel1.Controls.Add(textBoxFriendUID, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(380, 60);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxNotice
            // 
            groupBoxNotice.Controls.Add(tableLayoutPanel2);
            groupBoxNotice.Location = new Point(5, 353);
            groupBoxNotice.Margin = new Padding(0);
            groupBoxNotice.Name = "groupBoxNotice";
            groupBoxNotice.Size = new Size(386, 52);
            groupBoxNotice.TabIndex = 3;
            groupBoxNotice.TabStop = false;
            groupBoxNotice.Text = "通知设置";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260F));
            tableLayoutPanel2.Controls.Add(checkBoxSendKey, 0, 0);
            tableLayoutPanel2.Controls.Add(textBoxSendKey, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.Size = new Size(380, 30);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBoxPray
            // 
            groupBoxPray.Controls.Add(tableLayoutPanel3);
            groupBoxPray.Location = new Point(5, 122);
            groupBoxPray.Margin = new Padding(0);
            groupBoxPray.Name = "groupBoxPray";
            groupBoxPray.Size = new Size(386, 82);
            groupBoxPray.TabIndex = 4;
            groupBoxPray.TabStop = false;
            groupBoxPray.Text = "自动许愿设置";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutPanel3.Controls.Add(pray, 0, 0);
            tableLayoutPanel3.Controls.Add(prayTime, 1, 0);
            tableLayoutPanel3.Controls.Add(panel2, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel3.Size = new Size(380, 60);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBoxAquarium
            // 
            groupBoxAquarium.Controls.Add(tableLayoutPanel4);
            groupBoxAquarium.Location = new Point(5, 209);
            groupBoxAquarium.Margin = new Padding(0);
            groupBoxAquarium.Name = "groupBoxAquarium";
            groupBoxAquarium.Size = new Size(386, 52);
            groupBoxAquarium.TabIndex = 5;
            groupBoxAquarium.TabStop = false;
            groupBoxAquarium.Text = "自动收水族箱设置";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutPanel4.Controls.Add(aquarium, 0, 0);
            tableLayoutPanel4.Controls.Add(aquariumTime1, 1, 0);
            tableLayoutPanel4.Controls.Add(aquariumTime2, 2, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel4.Size = new Size(380, 30);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // Settings
            // 
            ClientSize = new Size(396, 461);
            Controls.Add(groupBoxAquarium);
            Controls.Add(groupBoxPray);
            Controls.Add(groupBoxNotice);
            Controls.Add(groupBoxHotspring);
            Controls.Add(groupBoxWindow);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "设置";
            panel1.ResumeLayout(false);
            groupBoxWindow.ResumeLayout(false);
            tableLayoutPanelOther.ResumeLayout(false);
            tableLayoutPanelOther.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBoxHotspring.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBoxNotice.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBoxPray.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            groupBoxAquarium.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);


        }

        #endregion
        private DateTimePicker aquariumTime1;
        private Panel panel1;
        private Button btnOK;
        private Button btnCancel;
        private Button btnDefault;
        private Label lblTitle1;
        private GroupBox groupBoxWindow;
        private TableLayoutPanel tableLayoutPanelOther;
        private DateTimePicker prayTime;
        private DateTimePicker hotspringTime;
        private DateTimePicker aquariumTime2;
        private Label lblLoopOrAuto;
        private Panel panel2;
        private RadioButton prayEXP;
        private RadioButton prayGold;
        private CheckBox tea;
        private TextBox textBoxTitle;
        private CheckBox hotspring;
        private CheckBox aquarium;
        private CheckBox pray;
        private TextBox textBoxLoop;
        private TextBox textBoxSendKey;
        private TextBox textBoxAutoiInject;
        private CheckBox checkBoxSendKey;
        private Label lblFriendUID;
        private TextBox textBoxFriendUID;
        private GroupBox groupBoxHotspring;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBoxNotice;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBoxPray;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private GroupBox groupBoxAquarium;
        private TableLayoutPanel tableLayoutPanel4;
        private CheckBox checkBoxIsRefresh;
    }
}