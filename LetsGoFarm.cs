using LetsgoFarmAutomateFree;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetsgoFarmAutomateFree {
    public partial class LetsGoFarm :Form {
        private static AppConfig appConfig = new();
        public static AppConfig Conf => appConfig;
        private int remainingSeconds;
        private bool isTimerRunning = false;
        private readonly string announcement = "该软件完全免费，如果您是购买的，请差评并退款，无摇杆扫码登录可以按住Ctrl+鼠标滚轮上放大，登录完成后刷新可恢复";
        public LetsGoFarm() {
            InitializeComponent();
            LoadConfig();
            InitializeWebView().ConfigureAwait(false);
        }
        private void LoadConfig() {
            // 加载本地配置并显示
            appConfig = AppConfig.Load();
            lblCountdown.Text = $"{appConfig.LoopSeconds}";
            Text = appConfig.WindowTitle;
            // 获取版本信息
            string version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "未知版本";
            Debug.WriteLine($"当前版本: {version}");
            Text = appConfig.WindowTitle + $" - v{version}";
            // 加载公告
            linkLabel1.Text = announcement;
            OutputLog(announcement, Color.Red);
        }
        // 定义倒计时总时长（单位：秒）
        private async Task InitializeWebView() {
            try {
                await webView.EnsureCoreWebView2Async();
                webView.Source = new Uri("https://gamer.qq.com/v2/game/96897");
                webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = true;
                webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = true;
                await AutoiInjectScript();
            } catch (Exception ex) {
                MessageBox.Show($"WebView2初始化失败: {ex.Message}");
            }
        }
        // 定时器触发事件
        private async void TimerCountdown_Tick(object sender, EventArgs e) {
            remainingSeconds--;
            if (remainingSeconds < 0) {
                await ExecuteDroneCode();
                remainingSeconds = appConfig.LoopSeconds;
                timerCountdown.Start();
            } else {
                lblCountdown.Text = $"{remainingSeconds}";
            }
        }
        /// <summary>
        /// 自动注入JS脚本，参数影响后续执行的脚本
        /// </summary>
        /// <param name="js">注入脚本后运行的脚本函数</param>
        /// <param name="ms">注入脚本后等待多少毫秒</param>
        /// <returns></returns>
        private async Task AutoiInjectScript() {
            OutputLog($"{appConfig.AutoiInjectSeconds}秒后自动注入脚本");
            await Task.Delay(appConfig.AutoiInjectSeconds * 1000);
            InjectScript();
        }
        // 执行无人机控制代码
        private async Task ExecuteDroneCode() {
            try {
                string code1 = "runDrone();";
                await webView.CoreWebView2.ExecuteScriptAsync(code1);
            } catch (Exception ex) {
                MessageBox.Show($"执行脚本失败: {ex.Message}");
            }
        }
        private async void InjectScript() {
            const string JavaScript = "LetsgoFarmAutomate.Properties.main.js";
            // 注入主脚本
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(JavaScript);
            if (stream != null) {
                var jsCode = await new StreamReader(stream!).ReadToEndAsync();
                await webView.CoreWebView2.ExecuteScriptAsync(jsCode);
                OutputLog("脚本已注入，请检查左上角'网络延迟'处的'创建快捷方式'是否消失", Color.Green);
            } else { 
                OutputLog("未找到脚本", Color.Red);
            }

        }
        private void Settings_Click(object sender, EventArgs e) {
            using (var settingsForm = new Settings()) {
                if (settingsForm.ShowDialog() == DialogResult.OK) {
                    // 重新加载配置
                    LoadConfig();
                }
            }
        }
        private void OutputLog(string text) => OutputLog(text, Color.Black);

        private void OutputLog(string text, Color color) {
            if (richTextBoxLog.InvokeRequired) {
                richTextBoxLog.Invoke(new Action<string, Color>(OutputLog), text, color);
                return;
            }
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logContent = $"{now} - {text}\r\n";

            // 保存原始颜色
            Color originalColor = richTextBoxLog.SelectionColor;

            try {
                // 关键修复：先定位到末尾再设置颜色
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength; // ← 新增这行
                richTextBoxLog.SelectionColor = color;
                richTextBoxLog.AppendText(logContent);
            } finally {
                richTextBoxLog.SelectionColor = originalColor;
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                richTextBoxLog.ScrollToCaret();
            }
        }
        /// <summary>
        /// 禁止或恢复WebView的控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisableWebview_Click(object sender, EventArgs e) {
            if (webView.Enabled) {
                btnDisableWebview.Text = "恢复控制";
            } else {
                btnDisableWebview.Text = "禁止控制";
            }
            webView.Enabled = !webView.Enabled;
        }
        private void BtnSetting_Click(object sender, EventArgs e) {
            using var settingsForm = new Settings();
            if (settingsForm.ShowDialog() == DialogResult.OK) {
                OutputLog("设置已保存", Color.Green);
                // 重新加载配置
                LoadConfig();
            }
        }
        /// <summary>
        /// 运行按钮点击事件，控制脚本的运行和暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRun_Click(object sender, EventArgs e) {
            if (isTimerRunning) {
                timerCountdown.Stop();
                OutputLog("暂停运行", Color.Red);
                btnRun.BackColor = Color.FromArgb(128, 255, 128);
                btnRun.Text = "运行";
            } else {
                // 初始化倒计时并启动
                btnRun.BackColor = Color.FromArgb(255, 128, 128);
                btnRun.Text = "暂停";
                // 启动倒计时
                timerCountdown.Start();
                OutputLog("开始运行", Color.Green);
            }
            isTimerRunning = !isTimerRunning;
        }
        /// <summary>
        /// 刷新按钮点击事件，重新加载WebView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, EventArgs e) {
            timerCountdown.Stop();
            isTimerRunning = false;
            btnRun.BackColor = Color.FromArgb(128, 255, 128);
            btnRun.Text = "运行";
            OutputLog("已刷新，脚本暂停运行");
        }
        /// <summary>
        /// 显示或隐藏日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLog_Click(object sender, EventArgs e) {
            richTextBoxLog.Visible = !richTextBoxLog.Visible;
            Height += richTextBoxLog.Visible ? 142 : -142;
            btnLog.Text = richTextBoxLog.Visible ? "隐藏日志" : "显示日志";
        }
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(new ProcessStartInfo {
                FileName = "https://github.com/izhangguapi/letsgo-farm",
                UseShellExecute = true
            });
        }

    }
}
