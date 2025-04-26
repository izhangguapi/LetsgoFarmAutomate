
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace LetsgoFarmAutomateFree {
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
            panel1 = new Panel();
            btnCancel = new Button();
            btnDefault = new Button();
            btnOK = new Button();
            lblTitle1 = new Label();
            groupBoxWindow = new GroupBox();
            tableLayoutPanelOther = new TableLayoutPanel();
            lblLoopOrAuto = new Label();
            textBoxTitle = new TextBox();
            textBoxLoop = new TextBox();
            textBoxAutoiInject = new TextBox();
            label1 = new Label();
            panel1.SuspendLayout();
            groupBoxWindow.SuspendLayout();
            tableLayoutPanelOther.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnDefault);
            panel1.Controls.Add(btnOK);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 121);
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
            tableLayoutPanelOther.ColumnCount = 2;
            tableLayoutPanelOther.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelOther.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260F));
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
            textBoxTitle.Location = new Point(123, 3);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(254, 23);
            textBoxTitle.TabIndex = 0;
            textBoxTitle.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxLoop
            // 
            textBoxLoop.Anchor = AnchorStyles.None;
            textBoxLoop.Location = new Point(200, 33);
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
            textBoxAutoiInject.Location = new Point(200, 63);
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
            // Settings
            // 
            ClientSize = new Size(396, 161);
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
            ResumeLayout(false);


        }

        #endregion
        private Panel panel1;
        private Button btnOK;
        private Button btnCancel;
        private Button btnDefault;
        private Label lblTitle1;
        private GroupBox groupBoxWindow;
        private TableLayoutPanel tableLayoutPanelOther;
        private Label lblLoopOrAuto;
        private TextBox textBoxTitle;
        private TextBox textBoxLoop;
        private TextBox textBoxAutoiInject;
        private Label label1;
    }
}