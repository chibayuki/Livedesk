/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
Copyright © 2018 chibayuki@foxmail.com

动态桌面
Version 1.0.1807.0.R1.180710-0000

This file is part of "动态桌面" (Livedesk)

"动态桌面" (Livedesk) is released under the GPLv3 license
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinFormApp
{
    public partial class Form_Main : Form
    {
        #region DLL加载

        // User32
        private static class User32
        {
            public const int SE_SHUTDOWN_PRIVILEGE = 0x13;

            [DllImport("user32.dll")]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll")]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

            [DllImport("user32.dll")]
            public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

            [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetWindowLongA", ExactSpelling = true, SetLastError = true)]
            public static extern int GetWindowLongA(int hWnd, int nlndex);

            [DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SetWindowLongA", ExactSpelling = true, SetLastError = true)]
            public static extern int SetWindowLongA(int hWnd, int nlndex, int dwNewLong);
        }

        #endregion

        #region 窗体

        // 窗体构造
        public Form_Main()
        {
            InitializeComponent();

            // 使窗体不在任务栏、任务视图与任务管理器「应用」中显示
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.ShowInTaskbar = false;
            // 使窗体不显示控件框和图标
            this.ControlBox = false;
            this.ShowIcon = false;
            // 将窗体设置为双缓存模式
            this.DoubleBuffered = true;
            // 设置窗体边界
            this.Bounds = Animation.FormBounds;

            // 在透明窗体上绘制空位图
            Com.Painting2D.PaintImageOnTransparentForm(this, new Bitmap(this.Width, this.Height), 0);
            // 将窗体设置为鼠标可穿透
            User32.SetWindowLongA((int)this.Handle, -20, User32.GetWindowLongA((int)this.Handle, -20) | 524288 | 32);

            // 将窗体置于底层
            if (Environment.OSVersion.Version.Major >= 6)
            {
                User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
            }
            else
            {
                base.SendToBack();
                IntPtr hWndNewParent = User32.FindWindow("Progman", null);
                User32.SetParent(base.Handle, hWndNewParent);
            }

            // 设置控件属性
            ToolStripMenuItem_Exit.Text = "关闭 \"" + Application.ProductName + "\"";
        }

        // 窗体加载
        private void Form_Main_Load(object sender, EventArgs e)
        {
            // 防止进程多开
            string ModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
            string ProcessName = Path.GetFileNameWithoutExtension(ModuleName);
            Process[] Processes = Process.GetProcessesByName(ProcessName);
            if (Processes.Length > 1)
            {
                Application.Exit();
            }

            // 从以前的版本迁移最新的配置文件
            Configuration.TransConfig();
            // 删除所有以前的版本的配置文件
            Configuration.DelOldConfig();
            // 加载配置
            Configuration.LoadConfig();

            // 开始自动重绘
            RedrawThreadStart();
        }

        // 窗体激活
        private void Form_Main_Activated(object sender, EventArgs e)
        {
            // 将窗体置于底层
            if (Environment.OSVersion.Version.Major >= 6)
            {
                User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
            }
        }

        // 窗体重绘
        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            // 将窗体置于底层
            if (Environment.OSVersion.Version.Major >= 6)
            {
                User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
            }
        }

        #endregion

        #region 自动重绘

        // 位图
        private Bitmap Bmp;

        // 重绘线程
        private Thread RedrawThread;

        // 重绘线程开始工作
        private void RedrawThreadStart()
        {
            RedrawThread = new Thread(new ThreadStart(UpdateBitmap));
            RedrawThread.IsBackground = true;
            RedrawThread.Start();
        }

        // 重绘线程停止工作
        private void RedrawThreadStop()
        {
            if (RedrawThread != null && RedrawThread.IsAlive)
            {
                RedrawThread.Abort();
            }
        }

        // 在 UI 线程重绘位图
        private void RedrawBitmap()
        {
            if (Bmp != null)
            {
                // 在透明窗体上绘制位图
                Com.Painting2D.PaintImageOnTransparentForm(this, Bmp, 255);

                // 释放资源
                Bmp.Dispose();
            }

            // 当屏幕分辨率发生变化时重新设置窗体边界
            if (this.Bounds != Animation.FormBounds)
            {
                this.Bounds = Animation.FormBounds;
            }
        }

        // 在重绘线程更新位图
        private void UpdateBitmap()
        {
            // 重绘线程在每次循环挂起的最小毫秒数
            const int TimeoutMS_MIN = 5;
            // 重绘线程在每次循环挂起的毫秒数
            int TimeoutMS = TimeoutMS_MIN;

            // 重绘线程的循环次数
            int Count = 0;
            // 用于测量重绘线程用时的 Stopwatch
            Stopwatch Watch = new Stopwatch();

            while (true)
            {
                // 获取当前动画的位图
                Bmp = Animation.Bitmap;

                // 在UI线程执行重绘委托
                this.Invoke(new Action(RedrawBitmap));

                // 挂起适当的毫秒数以降低 CPU 占用，或者满足帧率限制的要求
                Thread.Sleep(TimeoutMS);

                if (Animation.Settings.LimitFPS && Animation.Settings.FPS > 0)
                {
                    if (Watch.IsRunning)
                    {
                        Count++;

                        double DeltaMS = Watch.ElapsedMilliseconds;
                        if (DeltaMS >= 500)
                        {
                            // 停止计时
                            Watch.Stop();

                            double CurrentFPS = Count / DeltaMS * 1000;

                            // 调整挂起的毫秒数
                            if (CurrentFPS < Animation.Settings.FPS * 0.9 || CurrentFPS > Animation.Settings.FPS * 1.1)
                            {
                                TimeoutMS = Math.Max(TimeoutMS_MIN, (int)Math.Round(TimeoutMS * CurrentFPS / Animation.Settings.FPS));
                            }
                            else
                            {
                                if (CurrentFPS < Animation.Settings.FPS * 0.99)
                                {
                                    TimeoutMS = Math.Max(TimeoutMS_MIN, TimeoutMS - 1);
                                }
                                else if (CurrentFPS > Animation.Settings.FPS * 1.01)
                                {
                                    TimeoutMS += 1;
                                }
                            }

                            Count = 0;
                            // 重新开始计时
                            Watch.Restart();
                        }
                    }
                    else
                    {
                        Count = 0;
                        // 开始计时
                        Watch.Start();
                    }
                }
                else
                {
                    TimeoutMS = TimeoutMS_MIN;
                }
            }
        }

        #endregion

        #region 通知区域

        // 设置窗体
        private Form_Settings Form_Settings = new Form_Settings();
        // 打开或关闭设置窗体
        private void OpenOrCloseSetting()
        {
            if (Form_Settings.Visible)
            {
                Form_Settings.Close();
            }
            else
            {
                Form_Settings.ShowDialog();
            }
        }

        // 鼠标双击 NotifyIcon_Main
        private void NotifyIcon_Main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenOrCloseSetting();
            }
        }

        // 单击 ToolStripMenuItem_Settings
        private void ToolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            OpenOrCloseSetting();
        }

        // 单击 ToolStripMenuItem_Exit
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // 停止自动重绘
            RedrawThreadStop();

            // 保存配置
            Configuration.SaveConfig();

            // 关闭程序
            Application.Exit();
        }

        #endregion

    }
}