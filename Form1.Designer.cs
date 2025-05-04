
namespace CarForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.transparentPanel = new System.Windows.Forms.Panel();
            this.panelstart = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // transparentPanel
            // 
            this.transparentPanel.BackColor = System.Drawing.Color.Transparent;
            this.transparentPanel.Location = new System.Drawing.Point(1, 0);
            this.transparentPanel.Name = "transparentPanel";
            this.transparentPanel.Size = new System.Drawing.Size(12, 12);
            this.transparentPanel.TabIndex = 1;
            this.transparentPanel.Click += new System.EventHandler(this.TransparentPanel_DoubleClick);
            this.transparentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.transparentPanel_Paint);
            // 
            // panelstart
            // 
            this.panelstart.Location = new System.Drawing.Point(0, 0);
            this.panelstart.Name = "panelstart";
            this.panelstart.Size = new System.Drawing.Size(960, 540);
            this.panelstart.TabIndex = 2;
            this.panelstart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelstart_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 540);
            this.Controls.Add(this.panelstart);
            this.Controls.Add(this.transparentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel transparentPanel;
        private System.Windows.Forms.Panel panelstart;
        private System.Windows.Forms.Timer timer1;
    }
}

