using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
//using CefSharp;
//using CefSharp.WinForms;

namespace CarForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 创建并显示三个不同的网址窗体
            Form1 form1 = new Form1("WebsiteURL1");
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = new Point(0, 0);
            form1.Size = new Size(960, 540); // 设置窗体的大小
            form1.Show();

            Form1 form2 = new Form1("WebsiteURL2");
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = new Point(960,0);
            form2.Size = new Size(960, 540); // 设置窗体的大小
            form2.Show();

            Form1 form3 = new Form1("WebsiteURL3");
            form3.StartPosition = FormStartPosition.Manual;
            ///form3.Location = new Point(0, 540);
            form3.Location = new Point(960, 540);
            form3.Size = new Size(960, 540); // 设置窗体的大小
            form3.Show();

            Application.Run(form3);
        }

    }
}
