using System;
using System.Windows.Forms;

namespace LetsgoFarmAutomateFree
{
    
    public partial class Settings : Form
    {
        // 构造函数接收当前值
        public Settings()
        {
            InitializeComponent();
            // 初始化显示当前值
            LoadConfig(LetsGoFarm.Conf);
        }
        private void LoadConfig(AppConfig appConfig) {
            // 标题
            textBoxTitle.Text = appConfig.WindowTitle;
            // 倒计时
            textBoxLoop.Text = appConfig.LoopSeconds.ToString();
            // 自动注入秒数
            textBoxAutoiInject.Text = appConfig.AutoiInjectSeconds.ToString();
        }
        // 确定按钮点击
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // 标题和倒计时时间
            LetsGoFarm.Conf.WindowTitle = textBoxTitle.Text;
            LetsGoFarm.Conf.LoopSeconds = int.Parse(textBoxLoop.Text);
            // 自动注入秒数
            LetsGoFarm.Conf.AutoiInjectSeconds = int.Parse(textBoxAutoiInject.Text);
            LetsGoFarm.Conf.Save();
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
    }
}
