using Newtonsoft.Json;

namespace LetsgoFarmAutomate {
    public class AppConfig {
        public string WindowTitle { get; set; } = "星宝农场自动化";
        public int LoopSeconds { get; set; } = 124;
        public bool IsRefresh { get; set; } = false;
        public int AutoiInjectSeconds { get; set; } = 8;
        public bool Pray { get; set; } = true;
        public bool PrayGold { get; set; } = false;
        public bool PrayEXP { get; set; } = true;
        public TimeSpan PrayTime { get; set; } = new TimeSpan(08, 00, 00);
        public bool Aquarium { get; set; } = true;
        public TimeSpan AquariumTime1 { get; set; } = new TimeSpan(00, 00, 00);
        public TimeSpan AquariumTime2 { get; set; } = new TimeSpan(12, 00, 00);
        public bool Hotspring { get; set; } = true;
        public string? FriendUID { get; set; } = "";
        public bool Tea { get; set; } = false;
        public TimeSpan HotspringTime { get; set; } = new TimeSpan(08, 05, 00);
        public bool CheckSendKey { get; set; } = false;
        public string? SendKey { get; set; } = "";
        private static string ConfigPath =>
            Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)!, "config.json");

        public static AppConfig Load() {
            try {
                if (File.Exists(ConfigPath)) {
                    string json = File.ReadAllText(ConfigPath);
                    return JsonConvert.DeserializeObject<AppConfig>(json)!; // 使用 null包容运算符
                }
            } catch { }
            return new AppConfig();
        }
        public void Save() {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(ConfigPath, json);
        }
    }
}