using System;
using System.Windows.Forms;

namespace Tool.Controls
{
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            // 设置控件为透明背景，但保留绘制内容
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint, true);
            
            // 设置背景为透明
            this.BackColor = System.Drawing.Color.Transparent;
        }
    }
}