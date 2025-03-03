using System.ComponentModel;
using System.Windows.Forms;

namespace LetsgoFarm_Web
{
    partial class LetsgoFarm
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
        protected override void OnClosing(CancelEventArgs e)
        {
            //让用户选择点击
            DialogResult result = MessageBox.Show("是否确认关闭？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //判断是否取消事件
            if (result == DialogResult.No)
            {
                //取消退出
                e.Cancel = true;
            }
            else
            {
                //释放资源
                webView?.Dispose();
                base.OnClosing(e);
            }
        }
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LetsgoFarm));
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lblCountdown = new System.Windows.Forms.ToolStripMenuItem();
            this.RunBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.OthersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenOCRWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.Test = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.InjectScript = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCountdown = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 24);
            this.webView.Margin = new System.Windows.Forms.Padding(0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1200, 675);
            this.webView.TabIndex = 0;
            this.webView.TabStop = false;
            this.webView.ZoomFactor = 1D;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCountdown,
            this.RunBtn,
            this.RefreshBtn,
            this.OthersToolStripMenuItem,
            this.Settings,
            this.InjectScript});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lblCountdown
            // 
            this.lblCountdown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblCountdown.AutoSize = false;
            this.lblCountdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblCountdown.Enabled = false;
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(60, 20);
            this.lblCountdown.Text = "120秒";
            // 
            // RunBtn
            // 
            this.RunBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(44, 20);
            this.RunBtn.Text = "运行";
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(44, 20);
            this.RefreshBtn.Text = "刷新";
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // Settings
            // 
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(44, 20);
            this.Settings.Text = "设置";
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // InjectScript
            // 
            this.InjectScript.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.InjectScript.Name = "InjectScript";
            this.InjectScript.Size = new System.Drawing.Size(68, 20);
            this.InjectScript.Text = "注入脚本";
            this.InjectScript.Click += new System.EventHandler(this.InjectScriptToolStripMenuItem_Click);
            // 
            // timerCountdown
            // 
            this.timerCountdown.Interval = 1000;
            // 
            // LetsgoFarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 699);
            this.Controls.Add(this.webView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LetsgoFarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "元梦之星";
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Timer timerCountdown;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem OthersToolStripMenuItem;
        private ToolStripMenuItem OpenOCRWindow;
        private ToolStripMenuItem Test;
        private ToolStripMenuItem Settings;
        private ToolStripMenuItem RunBtn;
        private ToolStripMenuItem RefreshBtn;
        private ToolStripMenuItem InjectScript;
        private ToolStripMenuItem lblCountdown;
    }
}

