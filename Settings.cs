using System;
using System.IO;
using System.Text.Json;

namespace Tool
{
	public sealed class AppSettings
	{
		public int Width { get; set; } = 1200;
		public int Height { get; set; } = 800;
		public double Opacity { get; set; } = 0.95;
		public string Url { get; set; } = string.Empty;
		public bool HideBorder { get; set; } = false;
		// removed browser selection and custom path
	}

	public static class SettingsService
	{
		private static readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BrowserTopmostTool");
		private static readonly string FilePath = Path.Combine(Folder, "settings.json");

		public static void Save(AppSettings s)
		{
			Directory.CreateDirectory(Folder);
			var json = JsonSerializer.Serialize(s, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(FilePath, json);
		}

		public static AppSettings Load()
		{
			try
			{
				if (File.Exists(FilePath))
				{
					var json = File.ReadAllText(FilePath);
					var s = JsonSerializer.Deserialize<AppSettings>(json);
					if (s != null) return s;
				}
			}
			catch { }
			return new AppSettings();
		}
	}
}
