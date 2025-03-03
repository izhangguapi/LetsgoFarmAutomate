using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

public class AppConfig
{
    public int TotalCountdownSeconds { get; set; } = 120;
    public string WindowTitle { get; set; } = "元梦之星";
    public bool ShowAnnouncement { get; set; } = true;

    private static string ConfigPath =>
        Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)!, "config.json");

    public static AppConfig Load()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                string json = File.ReadAllText(ConfigPath);
                return JsonConvert.DeserializeObject<AppConfig>(json)!; // 使用 null包容运算符
            }
        }
        catch { /* 忽略错误 */ }

        return new AppConfig();
    }

    public void Save()
    {
        try
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(ConfigPath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"配置保存失败：{ex.Message}");
        }
    }
}