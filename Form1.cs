using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; // 添加此行以使用 ConfigurationManager
using Microsoft.Web.WebView2.WinForms;
using CefSharp;
using CefSharp.WinForms;


namespace CarForm
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser browser;
        private WebView2 webView; // 使用 WebView2 替代 ChromiumWebBrowser
        //public ChromiumWebBrowser browser;
        private bool dragging = false; // 是否在拖动窗体
        private bool resizing = false; // 是否在调整大小
        private Point dragCursorPoint; // 鼠标按下时的光标位置
        private Point dragFormPoint; // 窗体的初始位置
        private Point resizeCursorPoint; // 调整大小时的鼠标位置
        private Size originalSize; // 原始窗体大小
        private const int edgeSize = 10; // 边缘区域的大小
        private Timer animationTimer;
        private string urlKey;

        private PictureBox loadingGif; // 播放GIF

        public Form1(string urlKey)
        {
            
            InitializeComponent();
            //InitBrowser(urlKey);
            //InitWebView(urlKey);
            this.urlKey = urlKey;

            // 初始化启动面板
            panelstart.Dock = DockStyle.Fill;
            panelstart.BackColor = Color.Black;


            loadingGif = new PictureBox
            {
                Image = Properties.Resources.StartGif, // 换成你的GIF资源名
                SizeMode = PictureBoxSizeMode.CenterImage,
                Dock = DockStyle.Fill
            };
            panelstart.Controls.Add(loadingGif);
            panelstart.BringToFront();

            this.Controls.Add(panelstart);


            // 初始化透明面板，覆盖窗体左上角用于拖动和右下角用于调整大小
            transparentPanel = new TransparentPanel
            {
                Size = new Size(15, 15), // 设置面板的大小为20x20像素
                Location = new Point(0, 0) // 设置面板的位置为窗体的左上角，保持10像素偏移
            };
            this.Controls.Add(transparentPanel);
            transparentPanel.BringToFront(); // 确保面板在最上层

            // 注册鼠标事件
            transparentPanel.MouseDown += new MouseEventHandler(TransparentPanel_MouseDown);
            transparentPanel.MouseMove += new MouseEventHandler(TransparentPanel_MouseMove);
            transparentPanel.MouseUp += new MouseEventHandler(TransparentPanel_MouseUp);
        }


        public void InitWebView(string urlKey)
        {
            // 从 App.config 中读取 URL
            string url = ConfigurationManager.AppSettings[urlKey];

            // 创建 WebView2 控件
            webView = new WebView2
            {
                Dock = DockStyle.Fill // 让 WebView2 控件填充整个窗体
            };
      

            // 将 WebView2 控件添加到窗体中
            this.Controls.Add(webView);

            webView.ZoomFactor = 0.5; // 设置缩放为 100%


            // 初始化 WebView2 控件
            webView.EnsureCoreWebView2Async(null).ContinueWith(task =>
            {
                // 确保代码在 UI 线程中执行
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        // 设置 URL
                        webView.CoreWebView2.Navigate(url);
                    }));
                }
                else
                {
                    // 如果已经在 UI 线程中
                    webView.CoreWebView2.Navigate(url);
                }
            });
        }
        //public void InitBrowser(string urlKey)
        //{
        //    // 从 App.config 中读取 URL
        //    string url = ConfigurationManager.AppSettings[urlKey];

        //    browser = new ChromiumWebBrowser(url)
        //    {
        //        Dock = DockStyle.Fill // 让浏览器控件填充整个窗体
        //    };


        //    this.Controls.Add(browser);
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            animationTimer = new Timer();
            animationTimer.Interval = 8000; // 播放8秒（8000毫秒）
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationTimer.Stop();

            // 隐藏 panelStart
            panelstart.Visible = false;

            //// 开始加载网页
            //InitWebView(urlKey);
            InitBrowser(urlKey);  // 注意这里换成了InitBrowser
        }

        public void InitBrowser(string urlKey)
        {
            string url = ConfigurationManager.AppSettings[urlKey];

            browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(browser);
            browser.BringToFront(); // 确保浏览器控件显示在最上面
        }

        private void transparentPanel_Paint(object sender, PaintEventArgs e)
        {

        }



        // 鼠标事件处理
        private void TransparentPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 检查鼠标是否在面板的右下角边缘区域，用于调整大小
                if (IsInResizeArea(e.Location))
                {
                    resizing = true;
                    resizeCursorPoint = Cursor.Position; // 记录鼠标按下时的位置
                    originalSize = this.Size; // 记录窗体的原始大小
                }
                else
                {
                    dragging = true;
                    dragCursorPoint = Cursor.Position; // 记录鼠标按下时的位置
                    dragFormPoint = this.Location; // 记录窗体的初始位置
                }
            }
        }

        private void TransparentPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                // 拖动窗体
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
            else if (resizing)
            {
                // 调整窗体大小
                int offsetX = Cursor.Position.X - resizeCursorPoint.X;
                int offsetY = Cursor.Position.Y - resizeCursorPoint.Y;

                // 设置新的窗体大小，限制最小宽度和高度
                int newWidth = Math.Max(originalSize.Width + offsetX, 200);
                int newHeight = Math.Max(originalSize.Height + offsetY, 150);
                this.Size = new Size(newWidth, newHeight);
            }
            else
            {
                // 根据鼠标位置设置不同的光标样式
                if (IsInResizeArea(e.Location))
                {
                    this.Cursor = Cursors.SizeNWSE; // 设置光标为调整大小样式
                }
                else
                {
                    this.Cursor = Cursors.Default; // 设置光标为默认样式
                }
            }
        }

        private void TransparentPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
                resizing = false; // 结束调整大小状态
                this.Cursor = Cursors.Default; // 重置光标样式
            }
        }

        // 检查鼠标是否在调整大小区域
        private bool IsInResizeArea(Point mousePosition)
        {
            return mousePosition.X >= transparentPanel.Width - edgeSize && mousePosition.Y >= transparentPanel.Height - edgeSize;
        }

        private void TransparentPanel_DoubleClick(object sender, EventArgs e)
        {
        
        }

        private void panelstart_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
