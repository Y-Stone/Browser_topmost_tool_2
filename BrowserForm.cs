using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.Controls;

namespace Tool
{
	public partial class BrowserForm : Form
	{
		private readonly MainForm controller;
		private bool showChrome = true;
		private bool circleActive = false;
		private bool circleCommitted = false;
		private Point circleCenter;
		private int circleRadius = 120;
		private bool dragging = false;
		private Point dragStart;
		private Region? originalRegion;
		private CircleOverlayForm? overlayForm;

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
				circleCenter = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
				this.DoubleBuffered = true;
			};
			this.Resize += (_, __) => { controller.OnBrowserSizeChanged(this.Width, this.Height); UpdateOverlayFormBounds(); };
			this.LocationChanged += (_, __) => UpdateOverlayFormBounds();
			this.Shown += (_, __) => UpdateOverlayFormBounds();
			this.FormClosed += (_, __) => HideOverlayForm();
			this.KeyPreview = true;
			this.KeyDown += BrowserForm_KeyDown;
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

		public bool IsCircleActive => circleActive;

		public void ToggleCircleOverlay()
		{
			if (!circleActive)
			{
				ActivateCircleMode();
			}
			else
			{
				DeactivateCircleMode();
			}
		}

		private void ActivateCircleMode()
		{
			circleActive = true;
			circleCommitted = false;
			dragging = false;
			originalRegion = this.Region != null ? this.Region.Clone() : null;
			DisableWebViewInteractions();
			ShowOverlayForm();
		}

		private void DeactivateCircleMode()
		{
			circleActive = false;
			circleCommitted = false;
			dragging = false;
			EnableWebViewInteractions();
			if (originalRegion != null)
			{
				this.Region = originalRegion;
				originalRegion = null;
			}
			else
			{
				this.Region = null;
			}
			HideOverlayForm();
			this.Invalidate();
		}

		private void DisableWebViewInteractions()
		{
			try
			{
				if (webView.CoreWebView2 != null)
				{
					webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
					webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
					webView.CoreWebView2.Settings.IsZoomControlEnabled = false;
				}
			}
			catch { }
			webView.Enabled = false;
		}

		private void EnableWebViewInteractions()
		{
			try
			{
				if (webView.CoreWebView2 != null)
				{
					webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;
					webView.CoreWebView2.Settings.IsStatusBarEnabled = true;
					webView.CoreWebView2.Settings.IsZoomControlEnabled = true;
				}
			}
			catch { }
			webView.Enabled = true;
		}

		private bool IsPointInsideCircle(Point p)
		{
			long dx = p.X - circleCenter.X;
			long dy = p.Y - circleCenter.Y;
			return (dx * dx + dy * dy) <= (long)circleRadius * circleRadius;
		}

		private void BrowserForm_MouseDown(object? sender, MouseEventArgs e)
		{
			if (!circleActive || circleCommitted) return;
			if (e.Button == MouseButtons.Left && IsPointInsideCircle(e.Location))
			{
				dragging = true;
				dragStart = e.Location;
				this.Capture = true;
			}
		}

		private void BrowserForm_MouseUp(object? sender, MouseEventArgs e)
		{
			if (!circleActive || circleCommitted) return;
			dragging = false;
			this.Capture = false;
		}

		private void BrowserForm_MouseMove(object? sender, MouseEventArgs e)
		{
			if (!circleActive || circleCommitted) return;
			if (dragging)
			{
				int dx = e.X - dragStart.X;
				int dy = e.Y - dragStart.Y;
				circleCenter = new Point(circleCenter.X + dx, circleCenter.Y + dy);
				dragStart = e.Location;
				ConstrainCircleInsideClient();
				this.Invalidate();
			}
		}

		private void BrowserForm_MouseWheel(object? sender, MouseEventArgs e)
		{
			if (!circleActive || circleCommitted) return;
			if (!IsPointInsideCircle(e.Location)) return;
			int delta = e.Delta > 0 ? 10 : -10;
			circleRadius = Math.Max(20, Math.Min(Math.Min(ClientSize.Width, ClientSize.Height), circleRadius + delta));
			ConstrainCircleInsideClient();
			this.Invalidate();
		}

		private void BrowserForm_Paint(object? sender, PaintEventArgs e)
		{
			if (!circleActive) return;
			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			using (Brush dim = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
			{
				g.FillRectangle(dim, this.ClientRectangle);
			}
			using (Pen ring = new Pen(Color.DarkRed, 3))
			{
				Rectangle rect = new Rectangle(circleCenter.X - circleRadius, circleCenter.Y - circleRadius, circleRadius * 2, circleRadius * 2);
				g.DrawEllipse(ring, rect);
			}
		}

		private void BrowserForm_KeyDown(object? sender, KeyEventArgs e)
		{
			if (!circleActive) return;
			if (e.KeyCode == Keys.Enter && !circleCommitted)
			{
				CommitCircleClip();
			}
		}

		private void CommitCircleClip()
		{
			circleCommitted = true;
			// Create circular region to show only inside circle
			GraphicsPath path = new GraphicsPath();
			path.AddEllipse(new Rectangle(circleCenter.X - circleRadius, circleCenter.Y - circleRadius, circleRadius * 2, circleRadius * 2));
			this.Region = new Region(path);
			// After commit, window draggable only (no content interaction)
			DisableWebViewInteractions();
			HideOverlayForm();
			// enable dragging the window by client area with left mouse
			this.MouseDown -= BrowserForm_Drag_MouseDown;
			this.MouseDown += BrowserForm_Drag_MouseDown;
		}

		private void BrowserForm_Drag_MouseDown(object? sender, MouseEventArgs e)
		{
			if (!circleCommitted) return;
			if (e.Button == MouseButtons.Left)
			{
				// simulate title bar drag
				ReleaseCapture();
				SendMessage(this.Handle, 0xA1, (IntPtr)2, IntPtr.Zero); // WM_NCLBUTTONDOWN, HTCAPTION
			}
		}

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool ReleaseCapture();
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		private void ConstrainCircleInsideClient()
		{
			int x = Math.Max(circleRadius, Math.Min(ClientSize.Width - circleRadius, circleCenter.X));
			int y = Math.Max(circleRadius, Math.Min(ClientSize.Height - circleRadius, circleCenter.Y));
			circleCenter = new Point(x, y);
		}

		private void ShowOverlayForm()
		{
			if (overlayForm == null)
			{
				Rectangle bounds = this.RectangleToScreen(this.ClientRectangle);
				overlayForm = new CircleOverlayForm(bounds, circleCenter, circleRadius, 
					(center, radius) => {
						circleCenter = center;
						circleRadius = radius;
						CommitCircleClip();
					},
					() => {
						// ESC key callback - same as clicking "取消圆圈"
						DeactivateCircleMode();
						if (controller != null)
						{
							controller.UpdateCircleButtonText(false);
						}
					});
				overlayForm.Owner = this;
			}
			UpdateOverlayFormBounds();
			overlayForm.Show(this);
			overlayForm.BringToFront();
			try { overlayForm.Activate(); } catch { }
		}

		private void HideOverlayForm()
		{
			if (overlayForm != null)
			{
				overlayForm.Hide();
			}
		}

		private void UpdateOverlayFormBounds()
		{
			if (overlayForm == null) return;
			Rectangle bounds = this.RectangleToScreen(this.ClientRectangle);
			overlayForm.UpdateBounds(bounds);
		}
	}
}
