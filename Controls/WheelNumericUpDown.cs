using System;
using System.Windows.Forms;

namespace Tool
{
	public class WheelNumericUpDown : NumericUpDown
	{
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			int direction = e.Delta > 0 ? 1 : -1;
			decimal step = 10m;
			decimal next = this.Value + direction * step;
			if (next < this.Minimum) next = this.Minimum;
			if (next > this.Maximum) next = this.Maximum;
			this.Value = next;
			// Do not call base.OnMouseWheel to suppress default 1-step change
		}
	}
}
