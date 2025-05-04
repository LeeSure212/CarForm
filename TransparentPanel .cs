using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace CarForm
{
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            // 允许透明背景
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.FromArgb(50, Color.Blue); // 使用半透明颜色示例
        }

        // 重写该方法以不绘制背景
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 不调用基类方法，避免绘制背景
        }
    }
}
