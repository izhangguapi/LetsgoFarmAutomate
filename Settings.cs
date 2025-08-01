using System.Text.RegularExpressions;

namespace LetsgoFarmAutomate {

    public partial class Settings :Form {
        // 使用GeneratedRegex特性替代原有的Regex实例
        [GeneratedRegex(@"^[A-Za-z0-9]{34}$", RegexOptions.Compiled)]
        private static partial Regex SendKeyRegex();
        [GeneratedRegex(@"^(|[\d]{9}|[\d]{10})$", RegexOptions.Compiled)]
        private static partial Regex FriendUIDRegex();
        public Settings() {
            InitializeComponent();
            // 初始化显示当前值
            LoadConfig(LetsGoFarm.Conf);
        }

        private void LoadConfig(AppConfig appConfig) {
            // 标题、倒计时、自动注入秒数
            textBoxTitle.Text = appConfig.WindowTitle;
            textBoxLoop.Text = appConfig.LoopSeconds.ToString();
            checkBoxIsRefresh.Checked = appConfig.IsRefresh;
            textBoxAutoiInject.Text = appConfig.AutoiInjectSeconds.ToString();
            // 许愿
            pray.Checked = appConfig.Pray;
            prayGold.Checked = appConfig.PrayGold;
            prayEXP.Checked = appConfig.PrayEXP;
            prayTime.Value = DateTime.Today + appConfig.PrayTime;
            // 水族馆
            aquarium.Checked = appConfig.Aquarium;
            aquariumTime1.Value = DateTime.Today + appConfig.AquariumTime1;
            aquariumTime2.Value = DateTime.Today + appConfig.AquariumTime2;
            // 温泉
            hotspring.Checked = appConfig.Hotspring;
            hotspringTime.Value = DateTime.Today + appConfig.HotspringTime;
            textBoxFriendUID.Text = appConfig.FriendUID;
            tea.Checked = appConfig.Tea;
            // Server酱配置
            checkBoxSendKey.Checked = appConfig.CheckSendKey;
            textBoxSendKey.Text = appConfig.SendKey;
        }

        /// <summary>
        /// 确定按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e) {
            // 检查输入是否合法
            if (checkBoxSendKey.Checked) {
                if (!SendKeyRegex().IsMatch(textBoxSendKey.Text ?? "")) {
                    MessageBox.Show("SendKey只能为34位的大小写字母和数字组成", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            if (hotspring.Checked) {
                if (!FriendUIDRegex().IsMatch(textBoxFriendUID.Text ?? "")) {
                    MessageBox.Show("好友UID只能为空，或者9-10位的数字组成", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            // 标题、倒计时时、间自动注入秒数
            LetsGoFarm.Conf.WindowTitle = textBoxTitle.Text;
            LetsGoFarm.Conf.LoopSeconds = int.Parse(textBoxLoop.Text);
            LetsGoFarm.Conf.IsRefresh = checkBoxIsRefresh.Checked;
            LetsGoFarm.Conf.AutoiInjectSeconds = int.Parse(textBoxAutoiInject.Text);
            // 许愿
            LetsGoFarm.Conf.Pray = pray.Checked;
            LetsGoFarm.Conf.PrayGold = prayGold.Checked;
            LetsGoFarm.Conf.PrayEXP = prayEXP.Checked;
            LetsGoFarm.Conf.PrayTime = prayTime.Value.TimeOfDay;
            // 水族馆
            LetsGoFarm.Conf.Aquarium = aquarium.Checked;
            LetsGoFarm.Conf.AquariumTime1 = aquariumTime1.Value.TimeOfDay;
            LetsGoFarm.Conf.AquariumTime2 = aquariumTime2.Value.TimeOfDay;
            // 温泉
            LetsGoFarm.Conf.Hotspring = hotspring.Checked;
            LetsGoFarm.Conf.HotspringTime = hotspringTime.Value.TimeOfDay;
            LetsGoFarm.Conf.FriendUID = textBoxFriendUID.Text;
            LetsGoFarm.Conf.Tea = tea.Checked;
            // Server酱配置
            LetsGoFarm.Conf.CheckSendKey = checkBoxSendKey.Checked;
            LetsGoFarm.Conf.SendKey = textBoxSendKey.Text;
            // 保存配置并关闭窗口
            LetsGoFarm.Conf.Save();
            DialogResult = DialogResult.OK;
            Close();
        }
        /// <summary>
        /// 恢复默认配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefault_Click(object sender, EventArgs e) {
            AppConfig defaultConfig = new();
            LoadConfig(defaultConfig);
        }
        /// <summary>
        /// 文本框输入限制，只允许数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxLoop_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true; // 阻止非数字字符的输入
            }
        }

        private void CheckBoxSendKey_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxSendKey.Checked) {
                textBoxSendKey.Enabled = true;
            } else {
                textBoxSendKey.Enabled = false;
            }
        }
    }
}
