using System;
using System.Windows.Forms;

namespace LetsgoFarm_Web
{
    
    public partial class SettingsForm : Form
    {
        // 构造函数接收当前值
        public SettingsForm()
        {
            InitializeComponent();
            // 初始化显示当前值
            numSeconds.Value = LetsgoFarm._config.TotalCountdownSeconds; 
            WindowTitleTextBox.Text = LetsgoFarm._config.WindowTitle;
        }

        // 确定按钮点击
        private void btnOK_Click(object sender, EventArgs e)
        {
            LetsgoFarm._config.TotalCountdownSeconds  = (int)numSeconds.Value;
            LetsgoFarm._config.WindowTitle = WindowTitleTextBox.Text;
            LetsgoFarm._config.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // 取消按钮点击
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
