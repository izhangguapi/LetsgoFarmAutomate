using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace LetsgoFarmAutomate {
    public partial class LetsGoFarm :Form {
        private static AppConfig appConfig = new();
        public static AppConfig Conf => appConfig;
        private int remainingSeconds;
        private bool isTimerRunning = false;
        private readonly string announcement = "无摇杆扫码登录可以按住Ctrl+鼠标滚轮上放大，登录完成后刷新可恢复";
        public LetsGoFarm() {
            InitializeComponent();
            LoadConfig();
            InitializeWebView().ConfigureAwait(false);
            // 加载公告
            linkLabel1.Text = announcement;
            OutputLog(announcement, Color.Red);
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig() {
            // 加载本地配置并显示
            appConfig = AppConfig.Load();
            lblCountdown.Text = $"{appConfig.LoopSeconds}";
            // 获取版本信息
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "未知版本";
            // 加载窗口标题
            Text = appConfig.WindowTitle + $" - v{version}";
        }
        /// <summary>
        /// 初始化WebView2控件
        /// </summary>
        /// <returns></returns>
        private async Task InitializeWebView() {
            try {
                // 确保初始化完成
                await webView.EnsureCoreWebView2Async(null);
                // 开启自动保存密码
                webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = true;
                webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = true;

                // WebMessage跨平台通讯
                webView.CoreWebView2.WebMessageReceived += async (s, e) => {
                    string rawMessage = e.TryGetWebMessageAsString();

                    try {
                        // 解析 JSON 数据
                        var message = JsonConvert.DeserializeObject<dynamic>(rawMessage)!;

                        // 提取消息内容和颜色
                        string text = message.text;
                        string colorName = message.color;
                        // 将颜色字符串转换为Color对象
                        Color logColor = Color.FromName(colorName);
                        // 如果颜色名称无效（如未知名称），使用默认颜色
                        if (!logColor.IsKnownColor) {
                            logColor = Color.Black; // 默认红色
                        }
                        if (text == "REFRESH") {
                            OutputLog("刷新一下", Color.Green);
                            await AutoiInjectScript();
                        } else {
                            OutputLog(text, logColor);
                        }
                    } catch {
                        // 兼容旧版单字符串消息
                        if (rawMessage == "REFRESH") {
                            OutputLog("刷新一下", Color.Green);
                            await AutoiInjectScript();
                        } else {
                            OutputLog(rawMessage, Color.Red);
                        }
                    }
                };
                // 加载游戏页面
                webView.Source = new Uri("https://gamer.qq.com/v2/game/96897");
                // 调试模式
#if DEBUG
                // 打开开发者工具
                webView.CoreWebView2.OpenDevToolsWindow();
#endif
            } catch (Exception ex) {
                OutputLog($"WebView2初始化失败: {ex.Message}", Color.Red);
            }
            // 自动注入脚本
            await AutoiInjectScript(false);
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
            _ = AutoiInjectScript();
        }
        /// <summary>
        /// 运行按钮点击事件，控制脚本的运行和暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRun_Click(object sender, EventArgs e) {
            if (isTimerRunning) {
                timerCountdown.Stop();
                timerDaily.Stop();
                OutputLog("暂停运行", Color.Red);
                btnRun.BackColor = Color.FromArgb(128, 255, 128);
                btnRun.Text = "运行";
            } else {
                // 初始化倒计时并启动
                btnRun.BackColor = Color.FromArgb(255, 128, 128);
                btnRun.Text = "暂停";
                // 启动倒计时
                timerCountdown.Start();
                if (appConfig.Pray || appConfig.Aquarium || appConfig.Hotspring) {
                    // 启动每日定时器
                    timerDaily.Start();
                }
                OutputLog("开始运行", Color.Green);
            }
            isTimerRunning = !isTimerRunning;
        }
        /// <summary>
        /// 注入脚本方法，加载并执行主脚本和监听脚本
        /// </summary>
        private async void InjectScript() {
            const string mainScript = "LetsgoFarmAutomate.Properties.main.js";
            const string toolsScript = "LetsgoFarmAutomate.Properties.tools.js";

            try {
                // 主脚本
                var mainCode = await LoadScriptAsync(mainScript);
                // 工具脚本
                var toolsCode = await LoadScriptAsync(toolsScript);
                // 判断脚本是否不为空
                if (!string.IsNullOrEmpty(toolsCode) && !string.IsNullOrEmpty(mainCode)) {
                    // 判断SendKey是否存在
                    if (appConfig.CheckSendKey) {
                        toolsCode = $"let currentSendKey = '{appConfig.SendKey}';" + toolsCode;
                        OutputLog("SendKey已填写", Color.Green);
                    }
                    await webView.CoreWebView2.ExecuteScriptAsync(mainCode + toolsCode);
                    OutputLog("脚本已注入，请检查左上角'网络延迟'处的'创建快捷方式'是否消失", Color.Green);
                }
            } catch (Exception ex) {
                OutputLog($"脚本注入失败: {ex.Message}", Color.Red);
            }
        }
        /// <summary>
        /// 加载嵌入资源脚本
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private async Task<string?> LoadScriptAsync(string resourceName) {
            try {
                await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                if (stream != null) {
                    return await new StreamReader(stream).ReadToEndAsync();
                }
                OutputLog($"找不到脚本资源: {resourceName}", Color.Red);
                return null;
            } catch (Exception ex) {
                OutputLog($"加载脚本失败({resourceName}): {ex.Message}", Color.Red);
                return null;
            }
        }
        /// <summary>
        /// 注入脚本按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void BtnInjectScript_Click(object sender, EventArgs e) {
        //    InjectScript();
        //}
        /// <summary>
        /// 设置按钮点击事件，打开设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetting_Click(object sender, EventArgs e) {
            using var settingsForm = new Settings();
            if (settingsForm.ShowDialog() == DialogResult.OK) {
                OutputLog("设置已保存", Color.Green);
                // 重新加载配置
                LoadConfig();
            }
        }
        /// <summary>
        /// 自动注入JS脚本，参数影响后续执行的脚本
        /// </summary>
        /// <param name="js">注入脚本后运行的脚本函数</param>
        /// <param name="ms">注入脚本后等待多少毫秒</param>
        /// <returns></returns>
        private async Task AutoiInjectScript(bool reload = true) {
            if (reload) {
                webView.CoreWebView2.Reload();
            }
            OutputLog($"{appConfig.AutoiInjectSeconds}秒后自动注入脚本");
            await Task.Delay(appConfig.AutoiInjectSeconds * 1000);
            InjectScript();
        }
        /// <summary>
        /// 倒计时器事件,用于执行无人机控制代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TimerCountdown_Tick(object? sender, EventArgs e) {
            remainingSeconds--;
            if (remainingSeconds < 0) {
                remainingSeconds = appConfig.LoopSeconds;
                // 配置倒计时结束是否刷新
                if (appConfig.IsRefresh) {
                    await AutoiInjectScript();
                }
                // 注入脚本
                await webView.CoreWebView2.ExecuteScriptAsync("window.postMessage({ action: 'runDrone' }, '*');");
            } else {
                lblCountdown.Text = $"{remainingSeconds}";
            }
        }
        /// <summary>
        /// 每日定时器事件,用于执行每日定时任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TimerDaily_Tick(object? sender, EventArgs e) {
            string nowTimeStr = DateTime.Now.ToString("HH:mm");
            TimeSpan nowTime = TimeSpan.Parse(nowTimeStr);
            if (appConfig.PrayGold) { }
            if (appConfig.Pray & nowTime == appConfig.PrayTime) {
                string js = "window.postMessage({ action: 'pray' ,params: [" + appConfig.PrayGold.ToString().ToLower() + "]}, '*');";
                await webView.CoreWebView2.ExecuteScriptAsync(js);
                string type = appConfig.PrayGold ? "金币 " : "经验";
                OutputLog("神农许愿加入队列，选择的是" + type + "，请勿乱动", Color.Orange);
            }
            if (appConfig.Aquarium & nowTime == appConfig.AquariumTime1 || nowTime == appConfig.AquariumTime2) {
                string js = "window.postMessage({ action: 'aquarium' }, '*');";
                await webView.CoreWebView2.ExecuteScriptAsync(js);
                OutputLog("收获水族箱加入队列，请勿乱动", Color.Orange);
            }
            if (appConfig.Hotspring & nowTime == appConfig.HotspringTime) {
                string tea = appConfig.Tea ? "选择喝茶" : "选择不喝茶";
                string frienduid = appConfig.FriendUID!.Length > 0 ? "进好友家泡温泉（appConfig.FriendUID）" : "在自家泡温泉";
                string js = "window.postMessage({ action: 'hotspring' ,params: [" + appConfig.Tea.ToString().ToLower() + "," + appConfig.FriendUID + "]}, '*');";
                await webView.CoreWebView2.ExecuteScriptAsync(js);
                OutputLog($"泡温泉加入队列，{tea}，{frienduid}，请勿乱动", Color.Orange);
            }
        }
        /// <summary>
        /// 禁止或恢复WebView的控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisableWebview_Click(object sender, EventArgs e) {
            //调试
#if DEBUG

#endif

            if (webView.Enabled) {
                btnDisableWebview.Text = "恢复控制";
            } else {
                btnDisableWebview.Text = "禁止控制";
            }
            webView.Enabled = !webView.Enabled;
        }
        /// <summary>
        /// 显示或隐藏日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLog_Click(object sender, EventArgs e) {
            richTextBoxLog.Visible = !richTextBoxLog.Visible;
            if (richTextBoxLog.Visible) {
                btnLog.Text = "隐藏日志";
                Height += 142;
                MinimumSize = new Size(816, 661);
            } else {
                btnLog.Text = "显示日志";
                MinimumSize = new Size(816, 519);
                Height += -142;
            }
        }
        // 重载方法保持向后兼容
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

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(new ProcessStartInfo {
                FileName = "https://qm.qq.com/cgi-bin/qm/qr?k=YDhhDTpz4JhzBCK0_IjuGHo-fmmVhZnI&jump_from=webapi&authKey=J6nWJ2UpCzp3Z/56yCerGx7PQcqtIHaKGUMjASht9aCUQu58rBMQtC/tCRZrwXk6",
                UseShellExecute = true
            });
        }
    }
}