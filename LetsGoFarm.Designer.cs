using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace LetsgoFarmAutomateFree
{
    partial class LetsGoFarm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        //重写关闭窗口按钮方法
        protected override void OnClosing(CancelEventArgs e) {
            //让用户选择点击
            DialogResult result = MessageBox.Show("是否退出程序？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //判断是否取消事件
            if (result == DialogResult.Yes) {
                //释放资源
                webView?.Dispose();
                Dispose();
                base.OnClosing(e);
                Application.ExitThread();
            } else {
                //如果选择否，则取消关闭事件
                e.Cancel = true;
            }
        }
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LetsGoFarm));
            timerCountdown = new System.Windows.Forms.Timer(components);
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel1 = new Panel();
            btnDisableWebview = new Button();
            btnRun = new Button();
            lblCountdown = new Label();
            linkLabel1 = new LinkLabel();
            btnSetting = new Button();
            btnLog = new Button();
            btnRefresh = new Button();
            richTextBoxLog = new RichTextBox();
            ((ISupportInitialize)webView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // timerCountdown
            // 
            timerCountdown.Interval = 1000;
            timerCountdown.Tick += TimerCountdown_Tick;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 30);
            webView.Margin = new Padding(0);
            webView.Name = "webView";
            webView.Size = new Size(1200, 503);
            webView.TabIndex = 1;
            webView.TabStop = false;
            webView.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDisableWebview);
            panel1.Controls.Add(btnRun);
            panel1.Controls.Add(lblCountdown);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(btnSetting);
            panel1.Controls.Add(btnLog);
            panel1.Controls.Add(btnRefresh);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(2);
            panel1.Size = new Size(1200, 30);
            panel1.TabIndex = 0;
            // 
            // btnDisableWebview
            // 
            btnDisableWebview.BackColor = Color.FromArgb(255, 192, 128);
            btnDisableWebview.Dock = DockStyle.Right;
            btnDisableWebview.Location = new Point(1063, 2);
            btnDisableWebview.Name = "btnDisableWebview";
            btnDisableWebview.Size = new Size(65, 26);
            btnDisableWebview.TabIndex = 3;
            btnDisableWebview.Text = "禁止控制";
            btnDisableWebview.UseVisualStyleBackColor = false;
            btnDisableWebview.Click += BtnDisableWebview_Click;
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.FromArgb(128, 255, 128);
            btnRun.Dock = DockStyle.Right;
            btnRun.Location = new Point(1128, 2);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(40, 26);
            btnRun.TabIndex = 5;
            btnRun.Text = "运行";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += BtnRun_Click;
            // 
            // lblCountdown
            // 
            lblCountdown.Dock = DockStyle.Right;
            lblCountdown.Location = new Point(1168, 2);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(30, 26);
            lblCountdown.TabIndex = 6;
            lblCountdown.Text = "124";
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            linkLabel1.Font = new Font("Microsoft YaHei UI", 9F);
            linkLabel1.LinkColor = Color.Red;
            linkLabel1.Location = new Point(147, 2);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(910, 26);
            linkLabel1.TabIndex = 7;
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // btnSetting
            // 
            btnSetting.BackColor = Color.FromArgb(192, 255, 255);
            btnSetting.Dock = DockStyle.Left;
            btnSetting.Location = new Point(107, 2);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(40, 26);
            btnSetting.TabIndex = 2;
            btnSetting.Text = "设置";
            btnSetting.UseVisualStyleBackColor = false;
            btnSetting.Click += BtnSetting_Click;
            // 
            // btnLog
            // 
            btnLog.BackColor = Color.FromArgb(192, 255, 255);
            btnLog.Dock = DockStyle.Left;
            btnLog.Location = new Point(42, 2);
            btnLog.Name = "btnLog";
            btnLog.Size = new Size(65, 26);
            btnLog.TabIndex = 1;
            btnLog.Text = "显示日志";
            btnLog.UseVisualStyleBackColor = false;
            btnLog.Click += BtnLog_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(192, 255, 255);
            btnRefresh.Dock = DockStyle.Left;
            btnRefresh.Location = new Point(2, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(40, 26);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "刷新";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.BorderStyle = BorderStyle.None;
            richTextBoxLog.Dock = DockStyle.Bottom;
            richTextBoxLog.Location = new Point(0, 533);
            richTextBoxLog.Margin = new Padding(0);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            richTextBoxLog.Size = new Size(1200, 142);
            richTextBoxLog.TabIndex = 2;
            richTextBoxLog.TabStop = false;
            richTextBoxLog.Text = "";
            richTextBoxLog.Visible = false;
            // 
            // LetsGoFarm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1200, 675);
            Controls.Add(webView);
            Controls.Add(panel1);
            Controls.Add(richTextBoxLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MinimizeBox = false;
            MinimumSize = new Size(816, 519);
            Name = "LetsGoFarm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "星宝农场自动化";
            ((ISupportInitialize)webView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }


        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.Timer timerCountdown;
        private Panel panel1;
        private Label lblCountdown;
        private Button btnRun;
        private Button btnRefresh;
        private Button btnLog;
        private Button btnDisableWebview;
        private Button btnSetting;
        private RichTextBox richTextBoxLog;
        private LinkLabel linkLabel1;
    }
}

