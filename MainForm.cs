using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tool
{
	public partial class MainForm : Form
	{
		private BrowserForm? browser;
		private bool updating = false;
		private AppSettings settings;
		private const int HOTKEY_ID = 9000;
		private const int MOD_CONTROL = 0x0002;
		private const int MOD_ALT = 0x0001;
		private const int VK_W = 0x57;

		public MainForm()
		{
			InitializeComponent();
			// load settings
			settings = SettingsService.Load();
			ApplySettingsToUi(settings);

			trackOpacity.ValueChanged += trackOpacity_ValueChanged;
			txtOpacity.TextChanged += txtOpacity_TextChanged;
			numWidth.ValueChanged += SizeInput_ValueChanged;
			numHeight.ValueChanged += SizeInput_ValueChanged;
			numWidth.MouseWheel += Numeric_MouseWheel;
			numHeight.MouseWheel += Numeric_MouseWheel;
			chkHideBorder.CheckedChanged += (_, __) => browser?.SetChromeVisible(!chkHideBorder.Checked);
			this.FormClosing += MainForm_FormClosing;
			this.Load += MainForm_Load;
			this.FormClosed += MainForm_FormClosed;
		}

		private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
		{
			var result = MessageBox.Show(this, "是否保存当前设置后退出？", "退出确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				settings = CollectSettingsFromUi();
				SettingsService.Save(settings);
				MessageBox.Show(this, "设置已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void ApplySettingsToUi(AppSettings s)
		{
			updating = true;
			try
			{
				numWidth.Value = Math.Max(200, Math.Min(10000, s.Width));
				numHeight.Value = Math.Max(200, Math.Min(10000, s.Height));
				int track = (int)Math.Round(Math.Max(0.1, Math.Min(1.0, s.Opacity)) * 100);
				trackOpacity.Value = Math.Max(10, Math.Min(100, track));
				txtOpacity.Text = s.Opacity.ToString("0.00", CultureInfo.InvariantCulture);
				txtUrl.Text = s.Url ?? string.Empty;
				chkHideBorder.Checked = s.HideBorder;
			}
			finally { updating = false; }
		}

		private AppSettings CollectSettingsFromUi()
		{
			return new AppSettings
			{
				Width = (int)numWidth.Value,
				Height = (int)numHeight.Value,
				Opacity = ParseOpacity(txtOpacity.Text),
				Url = txtUrl.Text?.Trim() ?? string.Empty,
				HideBorder = chkHideBorder.Checked
			};
		}

		private void btnSave_Click(object? sender, EventArgs e)
		{
			settings = CollectSettingsFromUi();
			SettingsService.Save(settings);
			MessageBox.Show(this, "设置已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void Numeric_MouseWheel(object? sender, MouseEventArgs e)
		{
			if (sender is not NumericUpDown num) return;
			int step = 10;
			int val = (int)num.Value;
			val += e.Delta > 0 ? step : -step;
			val = Math.Max(200, Math.Min(10000, val));
			num.Value = val;
		}

		private void SizeInput_ValueChanged(object? sender, EventArgs e)
		{
			if (updating || browser == null || browser.IsDisposed) return;
			updating = true;
			try
			{
				int w = (int)numWidth.Value;
				int h = (int)numHeight.Value;
				browser.ApplySize(w, h);
			}
			finally { updating = false; }
		}

		public void OnBrowserSizeChanged(int width, int height)
		{
			if (updating) return;
			updating = true;
			try
			{
				numWidth.Value = Math.Max(200, Math.Min(10000, width));
				numHeight.Value = Math.Max(200, Math.Min(10000, height));
			}
			finally { updating = false; }
		}

		// removed browse button handler

		private void btnOpen_Click(object sender, EventArgs e)
		{
			string url = txtUrl.Text?.Trim() ?? string.Empty;
			if (string.IsNullOrEmpty(url))
			{
				MessageBox.Show(this, "请先输入要打开的网址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
			{
				MessageBox.Show(this, "网址必须以 http:// 或 https:// 开头", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// removed external browser validation

			int w = (int)numWidth.Value;
			int h = (int)numHeight.Value;
			double op = ParseOpacity(txtOpacity.Text);

			if (browser == null || browser.IsDisposed)
			{
				browser = new BrowserForm(this, url, w, h, op);
				browser.FormClosed += (_, __) => browser = null;
				browser.SetChromeVisible(!chkHideBorder.Checked);
				browser.Show();
				ForceTopMost(this);
			}
			else
			{
				browser.ApplySettings(url, w, h, op);
				browser.SetChromeVisible(!chkHideBorder.Checked);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (browser != null && !browser.IsDisposed) browser.Close();
			browser = null;
		}

		private void btnCircle_Click(object? sender, EventArgs e)
		{
			if (browser == null || browser.IsDisposed)
			{
				MessageBox.Show(this, "请先打开浏览器窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			try
			{
				browser.ToggleCircleOverlay();
				UpdateCircleButtonText(browser.IsCircleActive);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "切换圆圈失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void UpdateCircleButtonText(bool isActive)
		{
			btnCircle.Text = isActive ? "取消圆圈" : "圆圈展示";
			btnCircle.Invalidate();
		}

		private void MainForm_Load(object? sender, EventArgs e)
		{
			// Register global hotkey Ctrl+Alt+W
			RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL | MOD_ALT, VK_W);
		}

		private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
		{
			// Unregister global hotkey
			UnregisterHotKey(this.Handle, HOTKEY_ID);
		}

		protected override void WndProc(ref Message m)
		{
			const int WM_HOTKEY = 0x0312;
			if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
			{
				ToggleWindowsVisibility();
			}
			base.WndProc(ref m);
		}

		private void ToggleWindowsVisibility()
		{
			// Only toggle browser window if it exists
			if (browser != null && !browser.IsDisposed)
			{
				bool browserVisible = browser.Visible;
				browser.Visible = !browserVisible;
				if (!browserVisible)
				{
					browser.WindowState = FormWindowState.Normal;
					browser.BringToFront();
					browser.Activate();
				}
			}
		}

		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		private void trackOpacity_ValueChanged(object? sender, EventArgs e)
		{
			if (updating) return;
			updating = true;
			try
			{
				double v = Math.Round((double)trackOpacity.Value) / 100.0;
				txtOpacity.Text = v.ToString("0.00", CultureInfo.InvariantCulture);
				browser?.ApplyOpacity(v);
			}
			finally { updating = false; }
		}

		private void txtOpacity_TextChanged(object? sender, EventArgs e)
		{
			if (updating) return;
			updating = true;
			try
			{
				double v = ParseOpacity(txtOpacity.Text);
				int iv = (int)Math.Round(v * 100);
				iv = Math.Max(10, Math.Min(100, iv));
				if (trackOpacity.Value != iv) trackOpacity.Value = iv;
				browser?.ApplyOpacity(v);
			}
			finally { updating = false; }
		}

		private static double ParseOpacity(string? s)
		{
			if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var v)) return Clamp(v);
			if (double.TryParse(s, out v)) return Clamp(v);
			return 0.95;
			static double Clamp(double x) => Math.Max(0.1, Math.Min(1.0, x));
		}

		[DllImport("user32.dll")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		private const uint SWP_NOSIZE = 0x0001, SWP_NOMOVE = 0x0002, SWP_NOACTIVATE = 0x0010, SWP_SHOWWINDOW = 0x0040;
		internal static void ForceTopMost(Form f)
		{
			SetWindowPos(f.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
		}
	}
}
