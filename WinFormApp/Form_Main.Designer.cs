namespace WinFormApp
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.NotifyIcon_Main = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuStrip_Main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator_0 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon_Main
            // 
            this.NotifyIcon_Main.ContextMenuStrip = this.ContextMenuStrip_Main;
            this.NotifyIcon_Main.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon_Main.Icon")));
            this.NotifyIcon_Main.Text = "动态桌面";
            this.NotifyIcon_Main.Visible = true;
            this.NotifyIcon_Main.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_Main_MouseDoubleClick);
            // 
            // ContextMenuStrip_Main
            // 
            this.ContextMenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Settings,
            this.ToolStripSeparator_0,
            this.ToolStripMenuItem_Exit});
            this.ContextMenuStrip_Main.Name = "ContextMenuStrip_Main";
            this.ContextMenuStrip_Main.Size = new System.Drawing.Size(178, 60);
            // 
            // ToolStripMenuItem_Settings
            // 
            this.ToolStripMenuItem_Settings.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolStripMenuItem_Settings.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_Settings.Image")));
            this.ToolStripMenuItem_Settings.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.ToolStripMenuItem_Settings.Name = "ToolStripMenuItem_Settings";
            this.ToolStripMenuItem_Settings.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_Settings.Text = "\"AppName\" 设置";
            this.ToolStripMenuItem_Settings.Click += new System.EventHandler(this.ToolStripMenuItem_Settings_Click);
            // 
            // ToolStripSeparator_0
            // 
            this.ToolStripSeparator_0.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.ToolStripSeparator_0.Name = "ToolStripSeparator_0";
            this.ToolStripSeparator_0.Size = new System.Drawing.Size(177, 6);
            // 
            // ToolStripMenuItem_Exit
            // 
            this.ToolStripMenuItem_Exit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolStripMenuItem_Exit.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
            this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItem_Exit.Text = "退出";
            this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Activated += new System.EventHandler(this.Form_Main_Activated);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Main_Paint);
            this.ContextMenuStrip_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon_Main;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Settings;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator_0;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
    }
}

