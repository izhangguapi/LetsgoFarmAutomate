using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace LetsgoFarmAutomate {
    public partial class LetsGoFarm :Form {
        private static AppConfig appConfig = new();
        public static AppConfig Conf => appConfig;
        private int remainingSeconds;
        private bool isTimerRunning = false;
        private readonly string announcement = "��ҡ��ɨ���¼���԰�סCtrl+�������ϷŴ󣬵�¼��ɺ�ˢ�¿ɻָ�";
        public LetsGoFarm() {
            InitializeComponent();
            LoadConfig();
            InitializeWebView().ConfigureAwait(false);
            // ���ع���
            linkLabel1.Text = announcement;
            OutputLog(announcement, Color.Red);
        }
        /// <summary>
        /// ���������ļ�
        /// </summary>
        private void LoadConfig() {
            // ���ر������ò���ʾ
            appConfig = AppConfig.Load();
            lblCountdown.Text = $"{appConfig.LoopSeconds}";
            // ��ȡ�汾��Ϣ
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "δ֪�汾";
            // ���ش��ڱ���
            Text = appConfig.WindowTitle + $" - v{version}";
        }
        /// <summary>
        /// ��ʼ��WebView2�ؼ�
        /// </summary>
        /// <returns></returns>
        private async Task InitializeWebView() {
            try {
                // ȷ����ʼ�����
                await webView.EnsureCoreWebView2Async(null);
                // �����Զ���������
                webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = true;
                webView.CoreWebView2.Settings.IsGeneralAutofillEnabled = true;

                // WebMessage��ƽ̨ͨѶ
                webView.CoreWebView2.WebMessageReceived += async (s, e) => {
                    string rawMessage = e.TryGetWebMessageAsString();

                    try {
                        // ���� JSON ����
                        var message = JsonConvert.DeserializeObject<dynamic>(rawMessage)!;

                        // ��ȡ��Ϣ���ݺ���ɫ
                        string text = message.text;
                        string colorName = message.color;
                        // ����ɫ�ַ���ת��ΪColor����
                        Color logColor = Color.FromName(colorName);
                        // �����ɫ������Ч����δ֪���ƣ���ʹ��Ĭ����ɫ
                        if (!logColor.IsKnownColor) {
                            logColor = Color.Black; // Ĭ�Ϻ�ɫ
                        }
                        if (text == "REFRESH") {
                            OutputLog("ˢ��һ��", Color.Green);
                            await AutoiInjectScript();
                        } else {
                            OutputLog(text, logColor);
                        }
                    } catch {
                        // ���ݾɰ浥�ַ�����Ϣ
                        if (rawMessage == "REFRESH") {
                            OutputLog("ˢ��һ��", Color.Green);
                            await AutoiInjectScript();
                        } else {
                            OutputLog(rawMessage, Color.Red);
                        }
                    }
                };
                // ������Ϸҳ��
                webView.Source = new Uri("https://gamer.qq.com/v2/game/96897");
                // ����ģʽ
#if DEBUG
                // �򿪿����߹���
                webView.CoreWebView2.OpenDevToolsWindow();
#endif
            } catch (Exception ex) {
                OutputLog($"WebView2��ʼ��ʧ��: {ex.Message}", Color.Red);
            }
            // �Զ�ע��ű�
            await AutoiInjectScript(false);
        }

        /// <summary>
        /// ˢ�°�ť����¼������¼���WebView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, EventArgs e) {
            timerCountdown.Stop();
            isTimerRunning = false;
            btnRun.BackColor = Color.FromArgb(128, 255, 128);
            btnRun.Text = "����";
            OutputLog("��ˢ�£��ű���ͣ����");
            _ = AutoiInjectScript();
        }
        /// <summary>
        /// ���а�ť����¼������ƽű������к���ͣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRun_Click(object sender, EventArgs e) {
            if (isTimerRunning) {
                timerCountdown.Stop();
                timerDaily.Stop();
                OutputLog("��ͣ����", Color.Red);
                btnRun.BackColor = Color.FromArgb(128, 255, 128);
                btnRun.Text = "����";
            } else {
                // ��ʼ������ʱ������
                btnRun.BackColor = Color.FromArgb(255, 128, 128);
                btnRun.Text = "��ͣ";
                // ��������ʱ
                timerCountdown.Start();
                if (appConfig.Pray || appConfig.Aquarium || appConfig.Hotspring) {
                    // ����ÿ�ն�ʱ��
                    timerDaily.Start();
                }
                OutputLog("��ʼ����", Color.Green);
            }
            isTimerRunning = !isTimerRunning;
        }
        /// <summary>
        /// ע��ű����������ز�ִ�����ű��ͼ����ű�
        /// </summary>
        private async void InjectScript() {
            const string mainScript = "LetsgoFarmAutomate.Properties.main.js";
            const string toolsScript = "LetsgoFarmAutomate.Properties.tools.js";

            try {
                // ���ű�
                var mainCode = await LoadScriptAsync(mainScript);
                // ���߽ű�
                var toolsCode = await LoadScriptAsync(toolsScript);
                // �жϽű��Ƿ�Ϊ��
                if (!string.IsNullOrEmpty(toolsCode) && !string.IsNullOrEmpty(mainCode)) {
                    // �ж�SendKey�Ƿ����
                    if (appConfig.CheckSendKey) {
                        toolsCode = $"let currentSendKey = '{appConfig.SendKey}';" + toolsCode;
                        OutputLog("SendKey����д", Color.Green);
                    }
                    await webView.CoreWebView2.ExecuteScriptAsync(mainCode + toolsCode);
                    OutputLog("�ű���ע�룬�������Ͻ�'�����ӳ�'����'������ݷ�ʽ'�Ƿ���ʧ", Color.Green);
                }
            } catch (Exception ex) {
                OutputLog($"�ű�ע��ʧ��: {ex.Message}", Color.Red);
            }
        }
        /// <summary>
        /// ����Ƕ����Դ�ű�
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private async Task<string?> LoadScriptAsync(string resourceName) {
            try {
                await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                if (stream != null) {
                    return await new StreamReader(stream).ReadToEndAsync();
                }
                OutputLog($"�Ҳ����ű���Դ: {resourceName}", Color.Red);
                return null;
            } catch (Exception ex) {
                OutputLog($"���ؽű�ʧ��({resourceName}): {ex.Message}", Color.Red);
                return null;
            }
        }
        /// <summary>
        /// ע��ű���ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void BtnInjectScript_Click(object sender, EventArgs e) {
        //    InjectScript();
        //}
        /// <summary>
        /// ���ð�ť����¼��������ô���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetting_Click(object sender, EventArgs e) {
            using var settingsForm = new Settings();
            if (settingsForm.ShowDialog() == DialogResult.OK) {
                OutputLog("�����ѱ���", Color.Green);
                // ���¼�������
                LoadConfig();
            }
        }
        /// <summary>
        /// �Զ�ע��JS�ű�������Ӱ�����ִ�еĽű�
        /// </summary>
        /// <param name="js">ע��ű������еĽű�����</param>
        /// <param name="ms">ע��ű���ȴ����ٺ���</param>
        /// <returns></returns>
        private async Task AutoiInjectScript(bool reload = true) {
            if (reload) {
                webView.CoreWebView2.Reload();
            }
            OutputLog($"{appConfig.AutoiInjectSeconds}����Զ�ע��ű�");
            await Task.Delay(appConfig.AutoiInjectSeconds * 1000);
            InjectScript();
        }
        /// <summary>
        /// ����ʱ���¼�,����ִ�����˻����ƴ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TimerCountdown_Tick(object? sender, EventArgs e) {
            remainingSeconds--;
            if (remainingSeconds < 0) {
                remainingSeconds = appConfig.LoopSeconds;
                // ���õ���ʱ�����Ƿ�ˢ��
                if (appConfig.IsRefresh) {
                    await AutoiInjectScript();
                }
                // ע��ű�
                await webView.CoreWebView2.ExecuteScriptAsync("window.postMessage({ action: 'runDrone' }, '*');");
            } else {
                lblCountdown.Text = $"{remainingSeconds}";
            }
        }
        /// <summary>
        /// ÿ�ն�ʱ���¼�,����ִ��ÿ�ն�ʱ����
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
                string type = appConfig.PrayGold ? "��� " : "����";
                OutputLog("��ũ��Ը������У�ѡ�����" + type + "�������Ҷ�", Color.Orange);
            }
            if (appConfig.Aquarium & nowTime == appConfig.AquariumTime1 || nowTime == appConfig.AquariumTime2) {
                string js = "window.postMessage({ action: 'aquarium' }, '*');";
                await webView.CoreWebView2.ExecuteScriptAsync(js);
                OutputLog("�ջ�ˮ���������У������Ҷ�", Color.Orange);
            }
            if (appConfig.Hotspring & nowTime == appConfig.HotspringTime) {
                string tea = appConfig.Tea ? "ѡ��Ȳ�" : "ѡ�񲻺Ȳ�";
                string frienduid = appConfig.FriendUID!.Length > 0 ? "�����Ѽ�����Ȫ��appConfig.FriendUID��" : "���Լ�����Ȫ";
                string js = "window.postMessage({ action: 'hotspring' ,params: [" + appConfig.Tea.ToString().ToLower() + "," + appConfig.FriendUID + "]}, '*');";
                await webView.CoreWebView2.ExecuteScriptAsync(js);
                OutputLog($"����Ȫ������У�{tea}��{frienduid}�������Ҷ�", Color.Orange);
            }
        }
        /// <summary>
        /// ��ֹ��ָ�WebView�Ŀ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisableWebview_Click(object sender, EventArgs e) {
            //����
#if DEBUG

#endif

            if (webView.Enabled) {
                btnDisableWebview.Text = "�ָ�����";
            } else {
                btnDisableWebview.Text = "��ֹ����";
            }
            webView.Enabled = !webView.Enabled;
        }
        /// <summary>
        /// ��ʾ��������־
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLog_Click(object sender, EventArgs e) {
            richTextBoxLog.Visible = !richTextBoxLog.Visible;
            if (richTextBoxLog.Visible) {
                btnLog.Text = "������־";
                Height += 142;
                MinimumSize = new Size(816, 661);
            } else {
                btnLog.Text = "��ʾ��־";
                MinimumSize = new Size(816, 519);
                Height += -142;
            }
        }
        // ���ط�������������
        private void OutputLog(string text) => OutputLog(text, Color.Black);

        private void OutputLog(string text, Color color) {
            if (richTextBoxLog.InvokeRequired) {
                richTextBoxLog.Invoke(new Action<string, Color>(OutputLog), text, color);
                return;
            }
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logContent = $"{now} - {text}\r\n";

            // ����ԭʼ��ɫ
            Color originalColor = richTextBoxLog.SelectionColor;

            try {
                // �ؼ��޸����ȶ�λ��ĩβ��������ɫ
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength; // �� ��������
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