using Newtonsoft.Json;

namespace LetsgoFarmAutomateFree {
    public class AppConfig {
        public string WindowTitle { get; set; } = "星宝农场自动化";
        public int LoopSeconds { get; set; } = 124;
        public int AutoiInjectSeconds { get; set; } = 8;


        private static string ConfigPath =>
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)!, "config.json");

        public static AppConfig Load() {
            try {
                if (File.Exists(ConfigPath)) {
                    string json = File.ReadAllText(ConfigPath);
                    return JsonConvert.DeserializeObject<AppConfig>(json)!; // 使用 null包容运算符
                }
            } catch { /* 忽略错误 */ }

            return new AppConfig();
        }

        public void Save() {
            try {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(ConfigPath, json);
            } catch (Exception ex) {
                MessageBox.Show($"配置保存失败：{ex.Message}");
            }
        }
    }
}