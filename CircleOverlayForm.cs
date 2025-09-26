using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Tool
{
	internal sealed class CircleOverlayForm : Form
	{
		private Point circleCenter;
		private int circleRadius;
		private bool dragging = false;
		private Point dragStart;
		private readonly Action<Point, int> onCommit;
		private readonly Action? onCancel;

		public CircleOverlayForm(Rectangle boundsOnScreen, Point initialCenter, int initialRadius, Action<Point, int> onCommit, Action? onCancel = null)
		{
			this.onCommit = onCommit;
			this.onCancel = onCancel;
			this.FormBorderStyle = FormBorderStyle.None;
			this.StartPosition = FormStartPosition.Manual;
			this.Bounds = boundsOnScreen;
			this.TopMost = true;
			this.ShowInTaskbar = false;
			this.DoubleBuffered = true;
			this.circleCenter = initialCenter;
			this.circleRadius = initialRadius;
			this.KeyPreview = true;
			this.BackColor = Color.Black;
			this.Opacity = 0.5; // translucent overlay so background is dimmed but visible

			this.Paint += Overlay_Paint;
			this.MouseDown += Overlay_MouseDown;
			this.MouseUp += Overlay_MouseUp;
			this.MouseMove += Overlay_MouseMove;
			this.MouseWheel += Overlay_MouseWheel;
			this.KeyDown += Overlay_KeyDown;
		}

		public void UpdateBounds(Rectangle boundsOnScreen)
		{
			this.Bounds = boundsOnScreen;
			ConstrainCircleInsideClient();
			this.Invalidate();
		}

		private void Overlay_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				onCommit(circleCenter, circleRadius);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				// ESC key cancels circle mode
				onCancel?.Invoke();
				this.Hide();
			}
		}

		private void Overlay_MouseDown(object? sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && IsPointInsideCircle(e.Location))
			{
				dragging = true;
				dragStart = e.Location;
				this.Capture = true;
			}
		}

		private void Overlay_MouseUp(object? sender, MouseEventArgs e)
		{
			dragging = false;
			this.Capture = false;
		}

		private void Overlay_MouseMove(object? sender, MouseEventArgs e)
		{
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

		private void Overlay_MouseWheel(object? sender, MouseEventArgs e)
		{
			if (!IsPointInsideCircle(e.Location)) return;
			int delta = e.Delta > 0 ? 10 : -10;
			circleRadius = Math.Max(20, Math.Min(Math.Min(ClientSize.Width, ClientSize.Height), circleRadius + delta));
			ConstrainCircleInsideClient();
			this.Invalidate();
		}

		private void Overlay_Paint(object? sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			using (Brush dim = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
			{
				g.FillRectangle(dim, this.ClientRectangle);
			}
			using (Pen ring = new Pen(Color.Yellow, 3))
			{
				Rectangle rect = new Rectangle(circleCenter.X - circleRadius, circleCenter.Y - circleRadius, circleRadius * 2, circleRadius * 2);
				g.DrawEllipse(ring, rect);
			}
		}

		private bool IsPointInsideCircle(Point p)
		{
			long dx = p.X - circleCenter.X;
			long dy = p.Y - circleCenter.Y;
			return (dx * dx + dy * dy) <= (long)circleRadius * circleRadius;
		}

		private void ConstrainCircleInsideClient()
		{
			// Allow circle to extend beyond window boundaries
			int x = Math.Max(-circleRadius, Math.Min(ClientSize.Width + circleRadius, circleCenter.X));
			int y = Math.Max(-circleRadius, Math.Min(ClientSize.Height + circleRadius, circleCenter.Y));
			circleCenter = new Point(x, y);
		}
	}
}


