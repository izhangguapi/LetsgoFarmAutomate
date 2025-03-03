using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LetsgoFarm_Web
{
    public partial class LetsgoFarm : Form
    {
        public static AppConfig _config;
        private int remainingSeconds;
        private bool isTimerRunning;
        private string jsCode = "var goToFarmPercentage=0.037;var blank=[0.8,0.93];var jump_btn=[0.73,0.82];var drone_btn=[0.76,0.65];var toggle_left=[0.8,0.8];var toggle_right=[0.94,0.8];var close_btn=[0.72,0.27];var accept_btn=[0.63,0.65];var refuse_btn=[0.36,0.65];var ok_btn=[0.5,0.65];var reset_btn=[0.95,0.9];function sleep(time){return new Promise((resolve)=>setTimeout(resolve,time))}function simulateKeyDown(char){var event=new KeyboardEvent(\"keydown\",{bubbles:true,cancelable:true,key:char,code:`Key${char.toUpperCase()}`,keyCode:char.charCodeAt(0),});document.dispatchEvent(event)}function simulateKeyUp(char){var event=new KeyboardEvent(\"keyup\",{bubbles:true,cancelable:true,key:char,code:`Key${char.toUpperCase()}`,keyCode:char.charCodeAt(0),});document.dispatchEvent(event)}async function simulateKeyPress(char,time=100){simulateKeyDown(char);await sleep(time);simulateKeyUp(char)}function simulateMouseEvent(x,y,type=\"click\"){const gameElement=document.querySelector(\".gmsdk-video-player\");const mouseEvent=new MouseEvent(type,{bubbles:true,clientX:x,clientY:y,});gameElement.dispatchEvent(mouseEvent)}async function simulateMouseClick(x,y){const gameElement=document.querySelector(\".gmsdk-video-player\");const rect=gameElement.getBoundingClientRect();const actualX=rect.left+rect.width*x;const actualY=rect.top+rect.height*y;console.log(\"实际点击坐标：\",actualX,actualY);simulateMouseEvent(actualX,actualY,\"mousedown\");await sleep(50);simulateMouseEvent(actualX,actualY,\"mouseup\")}async function simulateMouseDrag(direction,distanceRatio,duration=500){const gameElement=document.querySelector(\".gmsdk-video-player\");if(!gameElement){console.error(\"游戏画面元素未找到\");return}const rect=gameElement.getBoundingClientRect();const gameWidth=rect.width;const gameHeight=rect.height;const centerX=rect.left+gameWidth/2;const centerY=rect.top+gameHeight/2;const pixelDistance=gameWidth*distanceRatio;let endX=centerX;let endY=centerY;switch(direction.toLowerCase()){case\"left\":endX-=pixelDistance;break;case\"right\":endX+=pixelDistance;break;case\"top\":endY-=pixelDistance;break;case\"bottom\":endY+=pixelDistance;break;default:throw new Error(\"无效方向，支持: left/right/top/bottom\");}console.log(\"拖动参数：\",{start:[centerX,centerY],end:[endX,endY],distance:pixelDistance,direction,});simulateMouseEvent(centerX,centerY,\"mousedown\");const steps=duration/10;const stepX=(endX-centerX)/steps;const stepY=(endY-centerY)/steps;for(let i=0;i<=steps;i++){await sleep(10);const currentX=centerX+stepX*i;const currentY=centerY+stepY*i;simulateMouseEvent(currentX,currentY,\"mousemove\")}simulateMouseEvent(endX,endY,\"mouseup\")}async function goToFarm(){simulateKeyPress(\"R\");await sleep(500);simulateKeyDown(\"W\");simulateKeyDown(\"A\");await sleep(4500);simulateKeyUp(\"W\");await sleep(300);simulateKeyUp(\"A\");await sleep(500);simulateMouseDrag(\"right\",goToFarmPercentage)}async function goToHut(){simulateKeyPress(\"R\");await sleep(500);simulateKeyDown(\"W\");await sleep(700);simulateKeyDown(\"A\");await sleep(7000);simulateKeyUp(\"W\");simulateKeyUp(\"A\")}async function runDrone(){simulateMouseClick(jump_btn[0],jump_btn[1]);await sleep(50);for(let i=0;i<5;i++){simulateMouseClick(close_btn[0],close_btn[1]);await sleep(50);simulateMouseClick(ok_btn[0],ok_btn[1]);await sleep(50);simulateMouseClick(blank[0],blank[1]);await sleep(50)}simulateMouseClick(reset_btn[0],reset_btn[1]);await sleep(500);simulateKeyDown(\"A\");await sleep(200);simulateMouseClick(jump_btn[0],jump_btn[1]);await sleep(1000);simulateKeyUp(\"A\");simulateMouseClick(drone_btn[0],drone_btn[1])}function initialization(){document.querySelector(\".dw\")?.remove();document.querySelector(\".g-pc-k\")?.remove();document.querySelector(\".g-pc-m-tip\")?.remove();console.log(\"初始化完成\")}initialization();";
        public LetsgoFarm()
        {
            InitializeComponent();
            LoadConfig();
            InitializeWebView().ConfigureAwait(false);
            timerCountdown.Interval = 1000; // 1秒触发一次
            timerCountdown.Tick += TimerCountdown_Tick;
        }
        private void LoadConfig()
        {
            // 加载本地配置并显示
            _config = AppConfig.Load();
            lblCountdown.Text = $"{_config.TotalCountdownSeconds}秒";
            Text = _config.WindowTitle;
        }
        // 定义倒计时总时长（单位：秒）
        private async Task InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                webView.Source = new Uri("https://gamer.qq.com/v2/game/96897");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WebView2初始化失败: {ex.Message}");
            }
        }
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("刷新页面");
            webView.CoreWebView2.Reload();
            timerCountdown.Stop();
            remainingSeconds = _config.TotalCountdownSeconds;
            lblCountdown.Text = $"{_config.TotalCountdownSeconds}秒";
            RunBtn.Text = "运行";
            InjectScript.Enabled = true;
        }
        private async void RunBtn_Click(object sender, EventArgs e)
        {
            if (!isTimerRunning)
            {
                // 立即执行代码
                await ExecuteDroneCode();
                // 初始化倒计时并启动
                remainingSeconds = _config.TotalCountdownSeconds;
                lblCountdown.Text = $"{remainingSeconds}秒";
                RunBtn.Text = "停止";
                timerCountdown.Start();
            }
            else
            {
                timerCountdown.Stop();
                remainingSeconds = _config.TotalCountdownSeconds;
                lblCountdown.Text = $"{_config.TotalCountdownSeconds}秒";
                RunBtn.Text = "运行";
            }
            isTimerRunning = !isTimerRunning;
        }
        // 定时器触发事件
        private async void TimerCountdown_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds < 0)
            {
                await ExecuteDroneCode();
                remainingSeconds = _config.TotalCountdownSeconds;
                timerCountdown.Start();
            }
            else
            {
                lblCountdown.Text = $"{remainingSeconds}秒";
            }
        }
        // 执行无人机控制代码
        private async Task ExecuteDroneCode()
        {
            try
            {
                string code1 = "runDrone();";
                await webView.CoreWebView2.ExecuteScriptAsync(code1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行脚本失败: {ex.Message}");
            }
        }
        private async void InjectScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string result = await webView.CoreWebView2.ExecuteScriptAsync(jsCode);
                if (result != "undefined")
                {
                    MessageBox.Show("注入成功");
                }
                InjectScript.Enabled = false;
            }
            catch
            {
                MessageBox.Show("注入失败");
            }
        }
        private void Settings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // 重新加载配置
                    LoadConfig();
                }
            }
        }
    }
}
