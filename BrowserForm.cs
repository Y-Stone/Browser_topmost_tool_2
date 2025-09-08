using Microsoft.Web.WebView2.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
	public partial class BrowserForm : Form
	{
		private readonly MainForm controller;
		private bool showChrome = true;

		public BrowserForm(MainForm controller, string url, int width, int height, double opacity)
		{
			InitializeComponent();
			this.controller = controller;
			this.TopMost = true;
			this.Width = width;
			this.Height = height;
			this.Opacity = Math.Max(0.1, Math.Min(1.0, opacity));
			this.Load += async (_, __) =>
			{
				await EnsureWebView2Async();
				ApplyUrl(url);
			};
			this.Resize += (_, __) => controller.OnBrowserSizeChanged(this.Width, this.Height);
		}

		public void SetChromeVisible(bool visible)
		{
			showChrome = visible;
			this.FormBorderStyle = visible ? FormBorderStyle.Sizable : FormBorderStyle.None;
			this.ControlBox = visible;
			this.MinimizeBox = visible;
			this.MaximizeBox = visible;
		}

		public void ApplySettings(string url, int width, int height, double opacity)
		{
			this.Width = width;
			this.Height = height;
			ApplyOpacity(opacity);
			ApplyUrl(url);
		}

		public void ApplySize(int width, int height)
		{
			this.Width = width;
			this.Height = height;
		}

		public void ApplyOpacity(double opacity)
		{
			this.Opacity = Math.Max(0.1, Math.Min(1.0, opacity));
		}

		private async Task EnsureWebView2Async()
		{
			if (webView.CoreWebView2 != null) return;
			await webView.EnsureCoreWebView2Async();
			webView.CoreWebView2!.Settings.AreDefaultContextMenusEnabled = true;
			webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
			webView.CoreWebView2.Settings.IsZoomControlEnabled = true;
		}

		private void ApplyUrl(string url)
		{
			if (string.IsNullOrWhiteSpace(url)) return;
			if (!url.StartsWith("http://") && !url.StartsWith("https://")) url = "https://" + url;
			if (webView.CoreWebView2 != null) webView.CoreWebView2.Navigate(url);
			else webView.Source = new Uri(url);
		}
	}
}
