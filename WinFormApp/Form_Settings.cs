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

namespace WinFormApp
{
    public partial class Form_Settings : Form
    {
        #region 窗体

        // 仅使用 RecommendColors
        Com.WinForm.FormManager FormManager;

        // 窗体构造
        public Form_Settings()
        {
            InitializeComponent();

            // 使窗体不在任务栏、任务视图与任务管理器「应用」中显示
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.ShowInTaskbar = false;
            // 使窗体不显示控件框和图标
            this.ControlBox = false;
            this.ShowIcon = false;
            // 将窗体置于顶层
            this.TopMost = true;
            // 使窗体显示在屏幕正中
            this.StartPosition = FormStartPosition.CenterScreen;

            // 构造 FormManager
            FormManager = new Com.WinForm.FormManager(new Form());
            FormManager.Theme = Com.WinForm.Theme.White;
            FormManager.ThemeColor = new Com.ColorX(Color.White);

            // "动画"区域：圆形光点
            TrackBar_LightSpot_MinRadius.ValueChanged += (s, e) => Label_LightSpot_MinRadius_Val.Text = ((int)(Animation.Animations.LightSpot.Settings.MinRadius * 100) * 0.01).ToString();
            TrackBar_LightSpot_MaxRadius.ValueChanged += (s, e) => Label_LightSpot_MaxRadius_Val.Text = ((int)(Animation.Animations.LightSpot.Settings.MaxRadius * 100) * 0.01).ToString();
            TrackBar_LightSpot_Count.ValueChanged += (s, e) => Label_LightSpot_Count_Val.Text = Animation.Animations.LightSpot.Settings.Count.ToString();
            TrackBar_LightSpot_Amplitude.ValueChanged += (s, e) => Label_LightSpot_Amplitude_Val.Text = ((int)(Animation.Animations.LightSpot.Settings.Amplitude * 100) * 0.01).ToString();
            TrackBar_LightSpot_WaveLength.ValueChanged += (s, e) => Label_LightSpot_WaveLength_Val.Text = ((int)(Animation.Animations.LightSpot.Settings.WaveLength * 10) * 0.1).ToString();
            TrackBar_LightSpot_WaveVelocity.ValueChanged += (s, e) => Label_LightSpot_WaveVelocity_Val.Text = ((int)(Animation.Animations.LightSpot.Settings.WaveVelocity * 100) * 0.01).ToString();
            TrackBar_LightSpot_GradientVelocity.ValueChanged += (s, e) => Label_LightSpot_GradientVelocity_Val.Text = ((int)(Animation.Animations.LightSpot.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：三角形碎片
            TrackBar_TrianglePiece_MinRadius.ValueChanged += (s, e) => Label_TrianglePiece_MinRadius_Val.Text = ((int)(Animation.Animations.TrianglePiece.Settings.MinRadius * 10) * 0.1).ToString();
            TrackBar_TrianglePiece_MaxRadius.ValueChanged += (s, e) => Label_TrianglePiece_MaxRadius_Val.Text = ((int)(Animation.Animations.TrianglePiece.Settings.MaxRadius * 10) * 0.1).ToString();
            TrackBar_TrianglePiece_Count.ValueChanged += (s, e) => Label_TrianglePiece_Count_Val.Text = Animation.Animations.TrianglePiece.Settings.Count.ToString();
            TrackBar_TrianglePiece_Amplitude.ValueChanged += (s, e) => Label_TrianglePiece_Amplitude_Val.Text = ((int)(Animation.Animations.TrianglePiece.Settings.Amplitude * 100) * 0.01).ToString();
            TrackBar_TrianglePiece_WaveLength.ValueChanged += (s, e) => Label_TrianglePiece_WaveLength_Val.Text = ((int)(Animation.Animations.TrianglePiece.Settings.WaveLength * 10) * 0.1).ToString();
            TrackBar_TrianglePiece_WaveVelocity.ValueChanged += (s, e) => Label_TrianglePiece_WaveVelocity_Val.Text = ((int)(Animation.Animations.TrianglePiece.Settings.WaveVelocity * 100) * 0.01).ToString();
            TrackBar_TrianglePiece_GradientVelocity.ValueChanged += (s, e) => Label_TrianglePiece_GradientVelocity_Val.Text = ((int)(Animation.Animations.TrianglePiece.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：光芒
            TrackBar_Shine_MinRadius.ValueChanged += (s, e) => Label_Shine_MinRadius_Val.Text = ((int)(Animation.Animations.Shine.Settings.MinRadius * 10) * 0.1).ToString();
            TrackBar_Shine_MaxRadius.ValueChanged += (s, e) => Label_Shine_MaxRadius_Val.Text = ((int)(Animation.Animations.Shine.Settings.MaxRadius * 10) * 0.1).ToString();
            TrackBar_Shine_Count.ValueChanged += (s, e) => Label_Shine_Count_Val.Text = Animation.Animations.Shine.Settings.Count.ToString();
            TrackBar_Shine_Displacement.ValueChanged += (s, e) => Label_Shine_Displacement_Val.Text = ((int)(Animation.Animations.Shine.Settings.Displacement * 100) * 0.01).ToString();
            TrackBar_Shine_MinPeriod.ValueChanged += (s, e) => Label_Shine_MinPeriod_Val.Text = ((int)(Animation.Animations.Shine.Settings.MinPeriod * 100) * 0.01).ToString();
            TrackBar_Shine_MaxPeriod.ValueChanged += (s, e) => Label_Shine_MaxPeriod_Val.Text = ((int)(Animation.Animations.Shine.Settings.MaxPeriod * 100) * 0.01).ToString();
            TrackBar_Shine_GradientVelocity.ValueChanged += (s, e) => Label_Shine_GradientVelocity_Val.Text = ((int)(Animation.Animations.Shine.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：流星
            TrackBar_Meteor_MinRadius.ValueChanged += (s, e) => Label_Meteor_MinRadius_Val.Text = ((int)(Animation.Animations.Meteor.Settings.MinRadius * 100) * 0.01).ToString();
            TrackBar_Meteor_MaxRadius.ValueChanged += (s, e) => Label_Meteor_MaxRadius_Val.Text = ((int)(Animation.Animations.Meteor.Settings.MaxRadius * 100) * 0.01).ToString();
            TrackBar_Meteor_Length.ValueChanged += (s, e) => Label_Meteor_Length_Val.Text = ((int)(Animation.Animations.Meteor.Settings.Length * 10) * 0.1).ToString();
            TrackBar_Meteor_Count.ValueChanged += (s, e) => Label_Meteor_Count_Val.Text = Animation.Animations.Meteor.Settings.Count.ToString();
            TrackBar_Meteor_Velocity.ValueChanged += (s, e) => Label_Meteor_Velocity_Val.Text = ((int)(Animation.Animations.Meteor.Settings.Velocity * 10) * 0.1).ToString();
            TrackBar_Meteor_Angle.ValueChanged += (s, e) => Label_Meteor_Angle_Val.Text = ((int)(Animation.Animations.Meteor.Settings.Angle * 10) * 0.1).ToString();
            TrackBar_Meteor_GravitationalAcceleration.ValueChanged += (s, e) => Label_Meteor_GravitationalAcceleration_Val.Text = ((int)(Animation.Animations.Meteor.Settings.GravitationalAcceleration * 100) * 0.01).ToString();
            TrackBar_Meteor_GradientVelocity.ValueChanged += (s, e) => Label_Meteor_GradientVelocity_Val.Text = ((int)(Animation.Animations.Meteor.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：雪
            TrackBar_Snow_MinRadius.ValueChanged += (s, e) => Label_Snow_MinRadius_Val.Text = ((int)(Animation.Animations.Snow.Settings.MinRadius * 100) * 0.01).ToString();
            TrackBar_Snow_MaxRadius.ValueChanged += (s, e) => Label_Snow_MaxRadius_Val.Text = ((int)(Animation.Animations.Snow.Settings.MaxRadius * 100) * 0.01).ToString();
            TrackBar_Snow_Count.ValueChanged += (s, e) => Label_Snow_Count_Val.Text = Animation.Animations.Snow.Settings.Count.ToString();
            TrackBar_Snow_Velocity.ValueChanged += (s, e) => Label_Snow_Velocity_Val.Text = ((int)(Animation.Animations.Snow.Settings.Velocity * 10) * 0.1).ToString();
            TrackBar_Snow_GradientVelocity.ValueChanged += (s, e) => Label_Snow_GradientVelocity_Val.Text = ((int)(Animation.Animations.Snow.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：引力粒子
            TrackBar_GravityParticle_MinMass.ValueChanged += (s, e) => Label_GravityParticle_MinMass_Val.Text = ((int)Animation.Animations.GravityParticle.Settings.MinMass).ToString();
            TrackBar_GravityParticle_MaxMass.ValueChanged += (s, e) => Label_GravityParticle_MaxMass_Val.Text = ((int)Animation.Animations.GravityParticle.Settings.MaxMass).ToString();
            TrackBar_GravityParticle_Count.ValueChanged += (s, e) => Label_GravityParticle_Count_Val.Text = Animation.Animations.GravityParticle.Settings.Count.ToString();
            TrackBar_GravityParticle_CursorMass.ValueChanged += (s, e) => Label_GravityParticle_CursorMass_Val.Text = ((int)Animation.Animations.GravityParticle.Settings.CursorMass).ToString();
            TrackBar_GravityParticle_GravityConstant.ValueChanged += (s, e) => Label_GravityParticle_GravityConstant_Val.Text = ((int)(Animation.Animations.GravityParticle.Settings.GravityConstant * 100) * 0.01).ToString();
            TrackBar_GravityParticle_ElasticRestitutionCoefficient.ValueChanged += (s, e) => Label_GravityParticle_ElasticRestitutionCoefficient_Val.Text = ((int)(Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient * 1000) * 0.001).ToString();
            TrackBar_GravityParticle_GradientVelocity.ValueChanged += (s, e) => Label_GravityParticle_GradientVelocity_Val.Text = ((int)(Animation.Animations.GravityParticle.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：引力网
            TrackBar_GravityGrid_Radius.ValueChanged += (s, e) => Label_GravityGrid_Radius_Val.Text = ((int)(Animation.Animations.GravityGrid.Settings.Radius * 100) * 0.01).ToString();
            TrackBar_GravityGrid_Count.ValueChanged += (s, e) => Label_GravityGrid_Count_Val.Text = Animation.Animations.GravityGrid.Settings.Count.ToString();
            TrackBar_GravityGrid_LineWidth.ValueChanged += (s, e) => Label_GravityGrid_LineWidth_Val.Text = ((int)(Animation.Animations.GravityGrid.Settings.LineWidth * 100) * 0.01F).ToString();
            TrackBar_GravityGrid_CursorMass.ValueChanged += (s, e) => Label_GravityGrid_CursorMass_Val.Text = ((int)Animation.Animations.GravityGrid.Settings.CursorMass).ToString();
            TrackBar_GravityGrid_CursorRepulsionRadius.ValueChanged += (s, e) => Label_GravityGrid_CursorRepulsionRadius_Val.Text = ((int)Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius).ToString();
            TrackBar_GravityGrid_GradientVelocity.ValueChanged += (s, e) => Label_GravityGrid_GradientVelocity_Val.Text = ((int)(Animation.Animations.GravityGrid.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "动画"区域：扩散光点
            TrackBar_SpreadSpot_MinRadius.ValueChanged += (s, e) => Label_SpreadSpot_MinRadius_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.MinRadius * 100) * 0.01).ToString();
            TrackBar_SpreadSpot_MaxRadius.ValueChanged += (s, e) => Label_SpreadSpot_MaxRadius_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.MaxRadius * 100) * 0.01).ToString();
            TrackBar_SpreadSpot_Count.ValueChanged += (s, e) => Label_SpreadSpot_Count_Val.Text = Animation.Animations.SpreadSpot.Settings.Count.ToString();
            TrackBar_SpreadSpot_SourceX.ValueChanged += (s, e) => Label_SpreadSpot_SourceX_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.SourceX * 1000) * 0.001).ToString();
            TrackBar_SpreadSpot_SourceY.ValueChanged += (s, e) => Label_SpreadSpot_SourceY_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.SourceY * 1000) * 0.001).ToString();
            TrackBar_SpreadSpot_SourceZ.ValueChanged += (s, e) => Label_SpreadSpot_SourceZ_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.SourceZ * 1000) * 0.001).ToString();
            TrackBar_SpreadSpot_SourceSize.ValueChanged += (s, e) => Label_SpreadSpot_SourceSize_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.SourceSize * 1000) * 0.001).ToString();
            TrackBar_SpreadSpot_Velocity.ValueChanged += (s, e) => Label_SpreadSpot_Velocity_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.Velocity * 100) * 0.01).ToString();
            TrackBar_SpreadSpot_GradientVelocity.ValueChanged += (s, e) => Label_SpreadSpot_GradientVelocity_Val.Text = ((int)(Animation.Animations.SpreadSpot.Settings.GradientVelocity * 10) * 0.1).ToString();

            // "通用"区域
            TrackBar_FPS.ValueChanged += (s, e) => Label_FPS_Val.Text = ((int)Animation.Settings.FPS).ToString();
        }

        // 窗体加载
        private void Form_Settings_Load(object sender, EventArgs e)
        {
            // 窗体标题
            this.Text = "\"" + Application.ProductName + "\" 设置";

            // 设置主题与颜色
            SetThemeAndColor();

            // 重置"动画"区域所有选项卡
            ResetTab_LightSpot();
            ResetTab_TrianglePiece();
            ResetTab_Shine();
            ResetTab_Meteor();
            ResetTab_Snow();
            ResetTab_GravityParticle();
            ResetTab_GravityGrid();
            ResetTab_SpreadSpot();

            // 初始化"动画"区域当前打开的选项卡
            AnimationTab = (AnimationTabs)Animation.Type;

            // 初始化功能区选项卡
            FunctionAreaTab = FunctionAreaTabs.Animation;

            // "通用"区域
            CheckBox_AntiAlias.CheckedChanged -= CheckBox_AntiAlias_CheckedChanged;
            CheckBox_AntiAlias.Checked = Animation.Settings.AntiAlias;
            CheckBox_AntiAlias.CheckedChanged += CheckBox_AntiAlias_CheckedChanged;

            CheckBox_LimitFPS.CheckedChanged -= CheckBox_LimitFPS_CheckedChanged;
            CheckBox_LimitFPS.Checked = Animation.Settings.LimitFPS;
            CheckBox_LimitFPS.CheckedChanged += CheckBox_LimitFPS_CheckedChanged;

            TrackBar_FPS.Minimum = (int)Animation.Settings.FPS_MinValue;
            TrackBar_FPS.Maximum = (int)Animation.Settings.FPS_MaxValue;
            TrackBar_FPS.Value = (int)Animation.Settings.FPS;
            TrackBar_FPS.Enabled = Animation.Settings.LimitFPS;

            CheckBox_AutoStart.Text = "登录 Windows 时自动启动 \"" + Application.ProductName + "\"";
            CheckBox_AutoStart.CheckedChanged -= CheckBox_AutoStart_CheckedChanged;
            CheckBox_AutoStart.Checked = Animation.Settings.AutoStart;
            CheckBox_AutoStart.CheckedChanged += CheckBox_AutoStart_CheckedChanged;

            CheckBox_StartMenuShortcut.CheckedChanged -= CheckBox_StartMenuShortcut_CheckedChanged;
            CheckBox_StartMenuShortcut.Checked = Animation.Settings.StartMenuShortcut;
            CheckBox_StartMenuShortcut.CheckedChanged += CheckBox_StartMenuShortcut_CheckedChanged;

            CheckBox_DesktopShortcut.CheckedChanged -= CheckBox_DesktopShortcut_CheckedChanged;
            CheckBox_DesktopShortcut.Checked = Animation.Settings.DesktopShortcut;
            CheckBox_DesktopShortcut.CheckedChanged += CheckBox_DesktopShortcut_CheckedChanged;

            // "关于"区域
            Label_ApplicationName.Text = Application.ProductName;
            Label_ApplicationEdition.Text = "Build " + new Version(Application.ProductVersion).Build;
            Label_Version.Text = "版本: " + Application.ProductVersion;
        }

        // 窗体关闭
        private void Form_Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 保存配置
            Configuration.SaveConfig();
        }

        // 窗体大小改变
        private void Form_Settings_SizeChanged(object sender, EventArgs e)
        {
            // 防止 FixedToolWindow 因鼠标双击而最大化
            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        #endregion

        #region 主题与颜色

        // 设置主题与颜色
        private void SetThemeAndColor()
        {
            this.BackColor = FormManager.RecommendColors.FormBackground.ToColor();

            // 底部区域

            Panel_BottomArea.BackColor = FormManager.RecommendColors.Background_INC.ToColor();
            Label_Close.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 功能区选项卡

            Panel_FunctionArea.BackColor = FormManager.RecommendColors.Background_DEC.ToColor();
            Panel_FunctionAreaOptionsBar.BackColor = FormManager.RecommendColors.Button_DEC.ToColor();

            FunctionAreaTab = _FunctionAreaTab;

            // "动画"区域

            Panel_AnimationTypesOptionsBar.BackColor = FormManager.RecommendColors.Background.ToColor();

            // 圆形光点

            Label_LightSpot_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_LightSpot_MinRadius.ForeColor = Label_LightSpot_MaxRadius.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_LightSpot_MinRadius.BackColor = TrackBar_LightSpot_MaxRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_LightSpot_MinRadius_Val.ForeColor = Label_LightSpot_MaxRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_LightSpot_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_LightSpot_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_Amplitude.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_LightSpot_Amplitude.BackColor = Panel_FunctionArea.BackColor;
            Label_LightSpot_Amplitude_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_WaveLength.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_LightSpot_WaveLength.BackColor = Panel_FunctionArea.BackColor;
            Label_LightSpot_WaveLength_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_WaveVelocity.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_LightSpot_WaveVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_LightSpot_WaveVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_LightSpot_ColorMode_Custom.ForeColor = RadioButton_LightSpot_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_LightSpot_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_LightSpot_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_LightSpot_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_LightSpot_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_LightSpot_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_LightSpot_GlowMode_OuterGlow.ForeColor = RadioButton_LightSpot_GlowMode_InnerGlow.ForeColor = RadioButton_LightSpot_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LightSpot_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_LightSpot_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 三角形碎片

            Label_TrianglePiece_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_TrianglePiece_MinRadius.ForeColor = Label_TrianglePiece_MaxRadius.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_TrianglePiece_MinRadius.BackColor = TrackBar_TrianglePiece_MaxRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_TrianglePiece_MinRadius_Val.ForeColor = Label_TrianglePiece_MaxRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_TrianglePiece_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_TrianglePiece_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_Amplitude.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_TrianglePiece_Amplitude.BackColor = Panel_FunctionArea.BackColor;
            Label_TrianglePiece_Amplitude_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_WaveLength.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_TrianglePiece_WaveLength.BackColor = Panel_FunctionArea.BackColor;
            Label_TrianglePiece_WaveLength_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_WaveVelocity.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_TrianglePiece_WaveVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_TrianglePiece_WaveVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_TrianglePiece_ColorMode_Custom.ForeColor = RadioButton_TrianglePiece_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_TrianglePiece_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_TrianglePiece_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_TrianglePiece_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_TrianglePiece_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_TrianglePiece_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_TrianglePiece_GlowMode_OuterGlow.ForeColor = RadioButton_TrianglePiece_GlowMode_InnerGlow.ForeColor = RadioButton_TrianglePiece_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_TrianglePiece_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_TrianglePiece_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 光芒

            Label_Shine_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Shine_MinRadius.ForeColor = Label_Shine_MaxRadius.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Shine_MinRadius.BackColor = TrackBar_Shine_MaxRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_Shine_MinRadius_Val.ForeColor = Label_Shine_MaxRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shine_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Shine_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_Shine_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shine_Displacement.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Shine_Displacement.BackColor = Panel_FunctionArea.BackColor;
            Label_Shine_Displacement_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shine_Period.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Shine_MinPeriod.ForeColor = Label_Shine_MaxPeriod.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Shine_MinPeriod.BackColor = TrackBar_Shine_MaxPeriod.BackColor = Panel_FunctionArea.BackColor;
            Label_Shine_MinPeriod_Val.ForeColor = Label_Shine_MaxPeriod_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shine_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_Shine_ColorMode_Custom.ForeColor = RadioButton_Shine_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_Shine_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_Shine_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Shine_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_Shine_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_Shine_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shine_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_Shine_GlowMode_OuterGlow.ForeColor = RadioButton_Shine_GlowMode_InnerGlow.ForeColor = RadioButton_Shine_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shine_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Shine_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 流星

            Label_Meteor_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Meteor_MinRadius.ForeColor = Label_Meteor_MaxRadius.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Meteor_MinRadius.BackColor = TrackBar_Meteor_MaxRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_MinRadius_Val.ForeColor = Label_Meteor_MaxRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_Length.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Meteor_Length.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_Length_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Meteor_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_Velocity.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Meteor_Velocity.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_Velocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_Angle.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Meteor_Angle.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_Angle_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_GravitationalAcceleration.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Meteor_GravitationalAcceleration.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_GravitationalAcceleration_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_Meteor_ColorMode_Custom.ForeColor = RadioButton_Meteor_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_Meteor_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_Meteor_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Meteor_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_Meteor_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_Meteor_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_Meteor_GlowMode_OuterGlow.ForeColor = RadioButton_Meteor_GlowMode_InnerGlow.ForeColor = RadioButton_Meteor_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Meteor_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Meteor_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 雪

            Label_Snow_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Snow_MinRadius.ForeColor = Label_Snow_MaxRadius.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Snow_MinRadius.BackColor = TrackBar_Snow_MaxRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_Snow_MinRadius_Val.ForeColor = Label_Snow_MaxRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Snow_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Snow_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_Snow_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Snow_Velocity.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_Snow_Velocity.BackColor = Panel_FunctionArea.BackColor;
            Label_Snow_Velocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Snow_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_Snow_ColorMode_Custom.ForeColor = RadioButton_Snow_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_Snow_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_Snow_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_Snow_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_Snow_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_Snow_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Snow_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_Snow_GlowMode_OuterGlow.ForeColor = RadioButton_Snow_GlowMode_InnerGlow.ForeColor = RadioButton_Snow_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Snow_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_Snow_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 引力粒子

            Label_GravityParticle_Mass.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_GravityParticle_MinMass.ForeColor = Label_GravityParticle_MaxMass.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_GravityParticle_MinMass.BackColor = TrackBar_GravityParticle_MaxMass.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityParticle_MinMass_Val.ForeColor = Label_GravityParticle_MaxMass_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityParticle_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityParticle_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_CursorMass.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityParticle_CursorMass.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityParticle_CursorMass_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_GravityConstant.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityParticle_GravityConstant.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityParticle_GravityConstant_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_ElasticRestitutionCoefficient.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityParticle_ElasticRestitutionCoefficient.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityParticle_ElasticRestitutionCoefficient_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_GravityParticle_ColorMode_Custom.ForeColor = RadioButton_GravityParticle_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_GravityParticle_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_GravityParticle_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_GravityParticle_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityParticle_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_GravityParticle_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_GravityParticle_GlowMode_OuterGlow.ForeColor = RadioButton_GravityParticle_GlowMode_InnerGlow.ForeColor = RadioButton_GravityParticle_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityParticle_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_GravityParticle_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 引力网

            Label_GravityGrid_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityGrid_Radius.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityGrid_Radius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityGrid_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityGrid_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_LineWidth.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityGrid_LineWidth.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityGrid_LineWidth_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_CursorMass.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityGrid_CursorMass.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityGrid_CursorMass_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_CursorRepulsionRadius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_GravityGrid_CursorRepulsionRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityGrid_CursorRepulsionRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_GravityGrid_ColorMode_Custom.ForeColor = RadioButton_GravityGrid_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_GravityGrid_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_GravityGrid_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_GravityGrid_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_GravityGrid_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_GravityGrid_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_GravityGrid_GlowMode_OuterGlow.ForeColor = RadioButton_GravityGrid_GlowMode_InnerGlow.ForeColor = RadioButton_GravityGrid_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_GravityGrid_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_GravityGrid_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // 扩散光点

            Label_SpreadSpot_Radius.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_SpreadSpot_MinRadius.ForeColor = Label_SpreadSpot_MaxRadius.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_SpreadSpot_MinRadius.BackColor = TrackBar_SpreadSpot_MaxRadius.BackColor = Panel_FunctionArea.BackColor;
            Label_SpreadSpot_MinRadius_Val.ForeColor = Label_SpreadSpot_MaxRadius_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_Count.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_SpreadSpot_Count.BackColor = Panel_FunctionArea.BackColor;
            Label_SpreadSpot_Count_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_SourceLocation.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_SpreadSpot_SourceX.ForeColor = Label_SpreadSpot_SourceY.ForeColor = Label_SpreadSpot_SourceZ.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_SpreadSpot_SourceX.BackColor = TrackBar_SpreadSpot_SourceY.BackColor = TrackBar_SpreadSpot_SourceZ.BackColor = Panel_FunctionArea.BackColor;
            Label_SpreadSpot_SourceX_Val.ForeColor = Label_SpreadSpot_SourceY_Val.ForeColor = Label_SpreadSpot_SourceZ_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_SourceSize.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_SpreadSpot_SourceSize.BackColor = Panel_FunctionArea.BackColor;
            Label_SpreadSpot_SourceSize_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_Velocity.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            TrackBar_SpreadSpot_Velocity.BackColor = Panel_FunctionArea.BackColor;
            Label_SpreadSpot_Velocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_Color.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_SpreadSpot_ColorMode_Custom.ForeColor = RadioButton_SpreadSpot_ColorMode_Random.ForeColor = FormManager.RecommendColors.Text.ToColor();
            CheckBox_SpreadSpot_GradientWhenRandom.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_SpreadSpot_GradientVelocity.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_SpreadSpot_GradientVelocity.BackColor = Panel_FunctionArea.BackColor;
            Label_SpreadSpot_GradientVelocity_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_SpreadSpot_Color_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_GlowMode.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            RadioButton_SpreadSpot_GlowMode_OuterGlow.ForeColor = RadioButton_SpreadSpot_GlowMode_InnerGlow.ForeColor = RadioButton_SpreadSpot_GlowMode_EvenGlow.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_SpreadSpot_Reset.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_SpreadSpot_ResetToDefault.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();

            // "通用"区域

            Label_AntiAlias.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            CheckBox_AntiAlias.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_LimitFPS.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            CheckBox_LimitFPS.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_LimitFPS_Note.ForeColor = FormManager.RecommendColors.Text.ToColor();
            TrackBar_FPS.BackColor = Panel_FunctionArea.BackColor;
            Label_FPS_Val.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_AutoStart.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            CheckBox_AutoStart.ForeColor = FormManager.RecommendColors.Text.ToColor();

            Label_Shortcut.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            CheckBox_StartMenuShortcut.ForeColor = CheckBox_DesktopShortcut.ForeColor = FormManager.RecommendColors.Text.ToColor();

            // "关于"区域

            Label_ApplicationName.ForeColor = FormManager.RecommendColors.Text_INC.ToColor();
            Label_ApplicationEdition.ForeColor = Label_Version.ForeColor = Label_Copyright.ForeColor = FormManager.RecommendColors.Text.ToColor();
            Label_GitHub_Part1.ForeColor = Label_GitHub_Base.ForeColor = Label_GitHub_Part2.ForeColor = Label_GitHub_Release.ForeColor = FormManager.RecommendColors.Text.ToColor();

            // 控件替代
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_LightSpot_Color_Val, Label_LightSpot_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_TrianglePiece_Color_Val, Label_TrianglePiece_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Shine_Color_Val, Label_Shine_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Meteor_Color_Val, Label_Meteor_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Snow_Color_Val, Label_Snow_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GravityParticle_Color_Val, Label_GravityParticle_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GravityGrid_Color_Val, Label_GravityGrid_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_SpreadSpot_Color_Val, Label_SpreadSpot_Color_Val_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134));

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_LightSpot_ResetToDefault, Label_LightSpot_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_TrianglePiece_ResetToDefault, Label_TrianglePiece_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Shine_ResetToDefault, Label_Shine_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Meteor_ResetToDefault, Label_Meteor_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Snow_ResetToDefault, Label_Snow_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GravityParticle_ResetToDefault, Label_GravityParticle_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GravityGrid_ResetToDefault, Label_GravityGrid_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_SpreadSpot_ResetToDefault, Label_SpreadSpot_ResetToDefault_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GitHub_Base, Label_GitHub_Base_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GitHub_Release, Label_GitHub_Release_Click, Color.Transparent, FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_Close, Label_Close_Click, FormManager.RecommendColors.Button.ToColor(), FormManager.RecommendColors.Button_DEC.ToColor(), FormManager.RecommendColors.Button_INC.ToColor());
        }

        #endregion

        #region 背景绘图

        #region 功能区

        // Panel_FunctionAreaOptionsBar 绘图
        private void Panel_FunctionAreaOptionsBar_Paint(object sender, PaintEventArgs e)
        {
            Graphics Grap = e.Graphics;

            //

            Control[] TabCtrl = new Control[(int)FunctionAreaTabs.COUNT] { Label_Tab_Animation, Label_Tab_Common, Label_Tab_About };

            List<bool> TabBtnPointed = new List<bool>(TabCtrl.Length);
            List<bool> TabBtnSeld = new List<bool>(TabCtrl.Length);

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                TabBtnPointed.Add(Com.Geometry.CursorIsInControl(TabCtrl[i]));
                TabBtnSeld.Add(FunctionAreaTab == (FunctionAreaTabs)i);
            }

            Color TabBtnCr_Bk_Pointed = Com.ColorManipulation.ShiftLightnessByHSL(Panel_FunctionAreaOptionsBar.BackColor, -0.1), TabBtnCr_Bk_Seld = Com.ColorManipulation.ShiftLightnessByHSL(Panel_FunctionAreaOptionsBar.BackColor, -0.2), TabBtnCr_Bk_Uns = Panel_FunctionAreaOptionsBar.BackColor;

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                Color TabBtnCr_Bk = (TabBtnSeld[i] ? TabBtnCr_Bk_Seld : (TabBtnPointed[i] ? TabBtnCr_Bk_Pointed : TabBtnCr_Bk_Uns));

                Grap.FillRectangle(new SolidBrush(TabBtnCr_Bk), TabCtrl[i].Bounds);

                if (TabBtnSeld[i])
                {
                    Grap.FillRectangle(new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(TabBtnCr_Bk_Seld, -0.4)), new RectangleF(new PointF(TabCtrl[i].Left, TabCtrl[i].Top + TabCtrl[i].Height / 8), new SizeF(TabCtrl[i].Height / 8, TabCtrl[i].Height * 3 / 4)));
                }
                else
                {
                    Grap.DrawLine(new Pen(Com.ColorManipulation.ShiftLightnessByHSL(TabBtnCr_Bk_Uns, -0.1)), new Point(TabCtrl[i].Left, TabCtrl[i].Bottom - 1), new Point(TabCtrl[i].Right, TabCtrl[i].Bottom - 1));
                }
            }
        }

        #endregion

        #region "动画"区域

        // Panel_AnimationTypesOptionsBar 绘图
        private void Panel_AnimationTypesOptionsBar_Paint(object sender, PaintEventArgs e)
        {
            Graphics Grap = e.Graphics;

            //

            Control[] TabCtrl = new Control[(int)AnimationTabs.COUNT] { Label_LightSpot, Label_TrianglePiece, Label_Shine, Label_Meteor, Label_Snow, Label_GravityParticle, Label_GravityGrid, Label_SpreadSpot };

            List<bool> TabBtnPointed = new List<bool>(TabCtrl.Length);
            List<bool> TabBtnSeld = new List<bool>(TabCtrl.Length);

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                TabBtnPointed.Add(Com.Geometry.CursorIsInControl(TabCtrl[i]));
                TabBtnSeld.Add(AnimationTab == (AnimationTabs)i);
            }

            Color TabBtnCr_Bk_Pointed = Com.ColorManipulation.ShiftLightnessByHSL(Panel_AnimationTypesOptionsBar.BackColor, -0.1), TabBtnCr_Bk_Seld = Com.ColorManipulation.ShiftLightnessByHSL(Panel_AnimationTypesOptionsBar.BackColor, -0.2), TabBtnCr_Bk_Uns = Panel_AnimationTypesOptionsBar.BackColor;

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                Color TabBtnCr_Bk = (TabBtnSeld[i] ? TabBtnCr_Bk_Seld : (TabBtnPointed[i] ? TabBtnCr_Bk_Pointed : TabBtnCr_Bk_Uns));

                e.Graphics.FillRectangle(new SolidBrush(TabBtnCr_Bk), TabCtrl[i].Bounds);

                if (TabBtnSeld[i])
                {
                    e.Graphics.FillRectangle(new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(TabBtnCr_Bk_Seld, -0.4)), new RectangleF(new PointF(TabCtrl[i].Left, TabCtrl[i].Top + TabCtrl[i].Height / 8), new SizeF(TabCtrl[i].Height / 8, TabCtrl[i].Height * 3 / 4)));
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(Com.ColorManipulation.ShiftLightnessByHSL(TabBtnCr_Bk_Uns, -0.1)), new Point(TabCtrl[i].Left, TabCtrl[i].Bottom - 1), new Point(TabCtrl[i].Right, TabCtrl[i].Bottom - 1));
                }
            }
        }

        #endregion

        #region 圆形光点

        // Panel_LightSpot_Radius 绘图
        private void Panel_LightSpot_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_Count 绘图
        private void Panel_LightSpot_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_Amplitude 绘图
        private void Panel_LightSpot_Amplitude_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_Amplitude;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_WaveLength 绘图
        private void Panel_LightSpot_WaveLength_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_WaveLength;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_WaveVelocity 绘图
        private void Panel_LightSpot_WaveVelocity_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_WaveVelocity;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_Color 绘图
        private void Panel_LightSpot_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_GlowMode 绘图
        private void Panel_LightSpot_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LightSpot_ResetToDefault 绘图
        private void Panel_LightSpot_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LightSpot_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 三角形碎片

        // Panel_TrianglePiece_Radius 绘图
        private void Panel_TrianglePiece_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_Count 绘图
        private void Panel_TrianglePiece_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_Amplitude 绘图
        private void Panel_TrianglePiece_Amplitude_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_Amplitude;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_WaveLength 绘图
        private void Panel_TrianglePiece_WaveLength_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_WaveLength;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_WaveVelocity 绘图
        private void Panel_TrianglePiece_WaveVelocity_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_WaveVelocity;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_Color 绘图
        private void Panel_TrianglePiece_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_GlowMode 绘图
        private void Panel_TrianglePiece_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_TrianglePiece_ResetToDefault 绘图
        private void Panel_TrianglePiece_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_TrianglePiece_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 光芒

        // Panel_Shine_Radius 绘图
        private void Panel_Shine_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shine_Count 绘图
        private void Panel_Shine_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shine_Displacement 绘图
        private void Panel_Shine_Displacement_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_Displacement;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shine_Period 绘图
        private void Panel_Shine_Period_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_Period;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shine_Color 绘图
        private void Panel_Shine_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shine_GlowMode 绘图
        private void Panel_Shine_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shine_ResetToDefault 绘图
        private void Panel_Shine_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shine_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 流星

        // Panel_Meteor_Radius 绘图
        private void Panel_Meteor_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_Length 绘图
        private void Panel_Meteor_Length_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Length;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_Count 绘图
        private void Panel_Meteor_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_Velocity 绘图
        private void Panel_Meteor_Velocity_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Velocity;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_Angle 绘图
        private void Panel_Meteor_Angle_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Angle;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_GravitationalAcceleration 绘图
        private void Panel_Meteor_GravitationalAcceleration_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_GravitationalAcceleration;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_Color 绘图
        private void Panel_Meteor_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_GlowMode 绘图
        private void Panel_Meteor_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Meteor_ResetToDefault 绘图
        private void Panel_Meteor_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Meteor_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 雪

        // Panel_Snow_Radius 绘图
        private void Panel_Snow_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Snow_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Snow_Count 绘图
        private void Panel_Snow_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Snow_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Snow_Velocity 绘图
        private void Panel_Snow_Velocity_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Snow_Velocity;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Snow_Color 绘图
        private void Panel_Snow_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Snow_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Snow_GlowMode 绘图
        private void Panel_Snow_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Snow_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Snow_ResetToDefault 绘图
        private void Panel_Snow_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Snow_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 引力粒子

        // Panel_GravityParticle_Mass 绘图
        private void Panel_GravityParticle_Mass_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_Mass;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_Count 绘图
        private void Panel_GravityParticle_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_CursorMass 绘图
        private void Panel_GravityParticle_CursorMass_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_CursorMass;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_GravityConstant 绘图
        private void Panel_GravityParticle_GravityConstant_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_GravityConstant;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_ElasticRestitutionCoefficient 绘图
        private void Panel_GravityParticle_ElasticRestitutionCoefficient_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_ElasticRestitutionCoefficient;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_Color 绘图
        private void Panel_GravityParticle_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_GlowMode 绘图
        private void Panel_GravityParticle_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityParticle_ResetToDefault 绘图
        private void Panel_GravityParticle_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityParticle_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 引力网

        // Panel_GravityGrid_Radius 绘图
        private void Panel_GravityGrid_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_Count 绘图
        private void Panel_GravityGrid_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_LineWidth 绘图
        private void Panel_GravityGrid_LineWidth_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_LineWidth;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_CursorMass 绘图
        private void Panel_GravityGrid_CursorMass_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_CursorMass;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_CursorRepulsionRadius 绘图
        private void Panel_GravityGrid_CursorRepulsionRadius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_CursorRepulsionRadius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_Color 绘图
        private void Panel_GravityGrid_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_GlowMode 绘图
        private void Panel_GravityGrid_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_GravityGrid_ResetToDefault 绘图
        private void Panel_GravityGrid_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_GravityGrid_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 扩散光点

        // Panel_SpreadSpot_Radius 绘图
        private void Panel_SpreadSpot_Radius_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_Radius;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_Count 绘图
        private void Panel_SpreadSpot_Count_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_Count;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_SourceLocation 绘图
        private void Panel_SpreadSpot_SourceLocation_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_SourceLocation;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_SourceSize 绘图
        private void Panel_SpreadSpot_SourceSize_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_SourceSize;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_Velocity 绘图
        private void Panel_SpreadSpot_Velocity_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_Velocity;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_Color 绘图
        private void Panel_SpreadSpot_Color_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_Color;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_GlowMode 绘图
        private void Panel_SpreadSpot_GlowMode_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_GlowMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_SpreadSpot_ResetToDefault 绘图
        private void Panel_SpreadSpot_ResetToDefault_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_SpreadSpot_Reset;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region "通用"区域

        // Panel_AntiAlias 绘图
        private void Panel_AntiAlias_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_AntiAlias;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_LimitFPS 绘图
        private void Panel_LimitFPS_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_LimitFPS;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_AutoStart 绘图
        private void Panel_AutoStart_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_AutoStart;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        // Panel_Shortcut 绘图
        private void Panel_Shortcut_Paint(object sender, PaintEventArgs e)
        {
            Control Cntr = sender as Control;
            if (Cntr != null)
            {
                Pen P = new Pen(FormManager.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Shortcut;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #endregion

        #region 鼠标滚轮功能

        // 鼠标滚轮在 Panel_FunctionAreaOptionsBar 滚动
        private void Panel_FunctionAreaOptionsBar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0 && (int)FunctionAreaTab < (int)FunctionAreaTabs.COUNT - 1)
            {
                FunctionAreaTab += 1;
            }
            else if (e.Delta > 0 && (int)FunctionAreaTab > 0)
            {
                FunctionAreaTab -= 1;
            }
        }

        // 鼠标滚轮在 Panel_Animations_AnimationTypesContainer 滚动
        private void Panel_Animations_AnimationTypesContainer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0 && (int)AnimationTab < (int)AnimationTabs.COUNT - 1)
            {
                AnimationTab += 1;
            }
            else if (e.Delta > 0 && (int)AnimationTab > 0)
            {
                AnimationTab -= 1;
            }
        }

        #endregion

        #region 功能区

        // 功能区选项卡枚举
        private enum FunctionAreaTabs { NULL = -1, Animation, Common, About, COUNT }
        // 当前打开的功能区选项卡
        private FunctionAreaTabs _FunctionAreaTab = FunctionAreaTabs.NULL;
        private FunctionAreaTabs FunctionAreaTab
        {
            get
            {
                return _FunctionAreaTab;
            }
            set
            {
                _FunctionAreaTab = value;

                Color TabBtnCr_Fr_Seld = FormManager.RecommendColors.Text_INC.ToColor(), TabBtnCr_Fr_Uns = FormManager.RecommendColors.Text.ToColor();
                Color TabBtnCr_Bk_Seld = Color.Transparent, TabBtnCr_Bk_Uns = Color.Transparent;
                Font TabBtnFt_Seld = new Font("微软雅黑", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 134), TabBtnFt_Uns = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);

                Label_Tab_Animation.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Animation ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Animation.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Animation ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Animation.Font = (_FunctionAreaTab == FunctionAreaTabs.Animation ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_Common.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Common ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Common.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Common ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Common.Font = (_FunctionAreaTab == FunctionAreaTabs.Common ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_About.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_About.BackColor = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_About.Font = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnFt_Seld : TabBtnFt_Uns);

                // Panel 的 AutoScroll 功能似乎存在 bug，下面的代码可以规避某些显示问题
                if (Panel_FunctionAreaTab.AutoScroll)
                {
                    Panel_FunctionAreaTab.AutoScroll = false;
                    foreach (object Obj in Panel_FunctionAreaTab.Controls)
                    {
                        if (Obj is Panel)
                        {
                            Panel Pnl = Obj as Panel;
                            Pnl.Location = new Point(0, 0);
                        }
                    }
                    Panel_FunctionAreaTab.AutoScroll = true;
                }

                Panel_Tab_Animations.Visible = (_FunctionAreaTab == FunctionAreaTabs.Animation);
                Panel_Tab_Common.Visible = (_FunctionAreaTab == FunctionAreaTabs.Common);
                Panel_Tab_About.Visible = (_FunctionAreaTab == FunctionAreaTabs.About);
            }
        }

        // 鼠标进入 Label_Tab。
        private void Label_Tab_MouseEnter(object sender, EventArgs e)
        {
            Panel_FunctionAreaOptionsBar.Refresh();
        }

        // 鼠标离开 Label_Tab。
        private void Label_Tab_MouseLeave(object sender, EventArgs e)
        {
            Panel_FunctionAreaOptionsBar.Refresh();
        }

        // 鼠标按下 Label_Tab_Animation
        private void Label_Tab_Animation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Animation)
                {
                    FunctionAreaTab = FunctionAreaTabs.Animation;
                }
            }
        }

        // 鼠标按下 Label_Tab_Common
        private void Label_Tab_Common_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Common)
                {
                    FunctionAreaTab = FunctionAreaTabs.Common;
                }
            }
        }

        // 鼠标按下 Label_Tab_About
        private void Label_Tab_About_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.About)
                {
                    FunctionAreaTab = FunctionAreaTabs.About;
                }
            }
        }

        #endregion

        #region "动画"区域

        #region 选项卡

        // 动画选项卡枚举
        private enum AnimationTabs { NULL = -1, LightSpot, TrianglePiece, Shine, Meteor, Snow, GravityParticle, GravityGrid, SpreadSpot, COUNT }
        // 当前打开的动画选项卡
        private AnimationTabs _AnimationTab = AnimationTabs.NULL;
        private AnimationTabs AnimationTab
        {
            get
            {
                return _AnimationTab;
            }
            set
            {
                _AnimationTab = value;

                Animation.Types AnimationType = (Animation.Types)_AnimationTab;
                if (Animation.Type != AnimationType)
                {
                    Animation.Type = AnimationType;
                }

                Color TabBtnCr_Fr_Seld = FormManager.RecommendColors.Text_INC.ToColor(), TabBtnCr_Fr_Uns = FormManager.RecommendColors.Text.ToColor();
                Color TabBtnCr_Bk_Seld = Color.Transparent, TabBtnCr_Bk_Uns = Color.Transparent;
                Font TabBtnFt_Seld = new Font("微软雅黑", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 134), TabBtnFt_Uns = new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134);

                Label_LightSpot.ForeColor = (_AnimationTab == AnimationTabs.LightSpot ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_LightSpot.BackColor = (_AnimationTab == AnimationTabs.LightSpot ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_LightSpot.Font = (_AnimationTab == AnimationTabs.LightSpot ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_TrianglePiece.ForeColor = (_AnimationTab == AnimationTabs.TrianglePiece ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_TrianglePiece.BackColor = (_AnimationTab == AnimationTabs.TrianglePiece ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_TrianglePiece.Font = (_AnimationTab == AnimationTabs.TrianglePiece ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Shine.ForeColor = (_AnimationTab == AnimationTabs.Shine ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Shine.BackColor = (_AnimationTab == AnimationTabs.Shine ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Shine.Font = (_AnimationTab == AnimationTabs.Shine ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Meteor.ForeColor = (_AnimationTab == AnimationTabs.Meteor ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Meteor.BackColor = (_AnimationTab == AnimationTabs.Meteor ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Meteor.Font = (_AnimationTab == AnimationTabs.Meteor ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Snow.ForeColor = (_AnimationTab == AnimationTabs.Snow ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Snow.BackColor = (_AnimationTab == AnimationTabs.Snow ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Snow.Font = (_AnimationTab == AnimationTabs.Snow ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_GravityParticle.ForeColor = (_AnimationTab == AnimationTabs.GravityParticle ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_GravityParticle.BackColor = (_AnimationTab == AnimationTabs.GravityParticle ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_GravityParticle.Font = (_AnimationTab == AnimationTabs.GravityParticle ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_GravityGrid.ForeColor = (_AnimationTab == AnimationTabs.GravityGrid ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_GravityGrid.BackColor = (_AnimationTab == AnimationTabs.GravityGrid ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_GravityGrid.Font = (_AnimationTab == AnimationTabs.GravityGrid ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_SpreadSpot.ForeColor = (_AnimationTab == AnimationTabs.SpreadSpot ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_SpreadSpot.BackColor = (_AnimationTab == AnimationTabs.SpreadSpot ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_SpreadSpot.Font = (_AnimationTab == AnimationTabs.SpreadSpot ? TabBtnFt_Seld : TabBtnFt_Uns);

                // Panel 的 AutoScroll 功能似乎存在 bug，下面的代码可以规避某些显示问题
                if (Panel_Animations_AnimationsContainer.AutoScroll)
                {
                    Panel_Animations_AnimationsContainer.AutoScroll = false;
                    foreach (object Obj in Panel_Animations_AnimationsContainer.Controls)
                    {
                        if (Obj is Panel)
                        {
                            Panel Pnl = Obj as Panel;
                            Pnl.Location = new Point(0, 0);
                        }
                    }
                    Panel_Animations_AnimationsContainer.AutoScroll = true;
                }

                Panel_Animations_LightSpot.Visible = (_AnimationTab == AnimationTabs.LightSpot);
                Panel_Animations_TrianglePiece.Visible = (_AnimationTab == AnimationTabs.TrianglePiece);
                Panel_Animations_Shine.Visible = (_AnimationTab == AnimationTabs.Shine);
                Panel_Animations_Meteor.Visible = (_AnimationTab == AnimationTabs.Meteor);
                Panel_Animations_Snow.Visible = (_AnimationTab == AnimationTabs.Snow);
                Panel_Animations_GravityParticle.Visible = (_AnimationTab == AnimationTabs.GravityParticle);
                Panel_Animations_GravityGrid.Visible = (_AnimationTab == AnimationTabs.GravityGrid);
                Panel_Animations_SpreadSpot.Visible = (_AnimationTab == AnimationTabs.SpreadSpot);
            }
        }

        // 鼠标进入 Label_AnimationTab。
        private void Label_AnimationTab_MouseEnter(object sender, EventArgs e)
        {
            Panel_AnimationTypesOptionsBar.Refresh();
        }

        // 鼠标离开 Label_AnimationTab。
        private void Label_AnimationTab_MouseLeave(object sender, EventArgs e)
        {
            Panel_AnimationTypesOptionsBar.Refresh();
        }

        // 鼠标按下 Label_LightSpot
        private void Label_LightSpot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.LightSpot)
                {
                    AnimationTab = AnimationTabs.LightSpot;
                }
            }
        }

        // 鼠标按下 Label_TrianglePiece
        private void Label_TrianglePiece_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.TrianglePiece)
                {
                    AnimationTab = AnimationTabs.TrianglePiece;
                }
            }
        }

        // 鼠标按下 Label_Shine
        private void Label_Shine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.Shine)
                {
                    AnimationTab = AnimationTabs.Shine;
                }
            }
        }

        // 鼠标按下 Label_Meteor
        private void Label_Meteor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.Meteor)
                {
                    AnimationTab = AnimationTabs.Meteor;
                }
            }
        }

        // 鼠标按下 Label_Snow
        private void Label_Snow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.Snow)
                {
                    AnimationTab = AnimationTabs.Snow;
                }
            }
        }

        // 鼠标按下 Label_GravityParticle
        private void Label_GravityParticle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.GravityParticle)
                {
                    AnimationTab = AnimationTabs.GravityParticle;
                }
            }
        }

        // 鼠标按下 Label_GravityGrid
        private void Label_GravityGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.GravityGrid)
                {
                    AnimationTab = AnimationTabs.GravityGrid;
                }
            }
        }

        // 鼠标按下 Label_SpreadSpot
        private void Label_SpreadSpot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AnimationTab != AnimationTabs.SpreadSpot)
                {
                    AnimationTab = AnimationTabs.SpreadSpot;
                }
            }
        }

        #endregion

        #region 圆形光点

        // 重置选项卡
        private void ResetTab_LightSpot()
        {
            TrackBar_LightSpot_MinRadius.Minimum = (int)(Animation.Animations.LightSpot.Settings.MinRadius_MinValue * 100);
            TrackBar_LightSpot_MinRadius.Maximum = (int)(Animation.Animations.LightSpot.Settings.MinRadius_MaxValue * 100);
            TrackBar_LightSpot_MinRadius.Value = (int)(Animation.Animations.LightSpot.Settings.MinRadius * 100);
            TrackBar_LightSpot_MaxRadius.Minimum = (int)(Animation.Animations.LightSpot.Settings.MaxRadius_MinValue * 100);
            TrackBar_LightSpot_MaxRadius.Maximum = (int)(Animation.Animations.LightSpot.Settings.MaxRadius_MaxValue * 100);
            TrackBar_LightSpot_MaxRadius.Value = (int)(Animation.Animations.LightSpot.Settings.MaxRadius * 100);

            TrackBar_LightSpot_Count.Minimum = Animation.Animations.LightSpot.Settings.Count_MinValue;
            TrackBar_LightSpot_Count.Maximum = Animation.Animations.LightSpot.Settings.Count_MaxValue;
            TrackBar_LightSpot_Count.Value = Animation.Animations.LightSpot.Settings.Count;

            TrackBar_LightSpot_Amplitude.Minimum = (int)(Animation.Animations.LightSpot.Settings.Amplitude_MinValue * 100);
            TrackBar_LightSpot_Amplitude.Maximum = (int)(Animation.Animations.LightSpot.Settings.Amplitude_MaxValue * 100);
            TrackBar_LightSpot_Amplitude.Value = (int)(Animation.Animations.LightSpot.Settings.Amplitude * 100);

            TrackBar_LightSpot_WaveLength.Minimum = (int)(Animation.Animations.LightSpot.Settings.WaveLength_MinValue * 10);
            TrackBar_LightSpot_WaveLength.Maximum = (int)(Animation.Animations.LightSpot.Settings.WaveLength_MaxValue * 10);
            TrackBar_LightSpot_WaveLength.Value = (int)(Animation.Animations.LightSpot.Settings.WaveLength * 10);

            TrackBar_LightSpot_WaveVelocity.Minimum = (int)(Animation.Animations.LightSpot.Settings.WaveVelocity_MinValue * 100);
            TrackBar_LightSpot_WaveVelocity.Maximum = (int)(Animation.Animations.LightSpot.Settings.WaveVelocity_MaxValue * 100);
            TrackBar_LightSpot_WaveVelocity.Value = (int)(Animation.Animations.LightSpot.Settings.WaveVelocity * 100);

            RadioButton_LightSpot_ColorMode_Random.CheckedChanged -= RadioButton_LightSpot_ColorMode_Random_CheckedChanged;
            RadioButton_LightSpot_ColorMode_Custom.CheckedChanged -= RadioButton_LightSpot_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.LightSpot.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_LightSpot_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_LightSpot_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_LightSpot_ColorMode_Random.CheckedChanged += RadioButton_LightSpot_ColorMode_Random_CheckedChanged;
            RadioButton_LightSpot_ColorMode_Custom.CheckedChanged += RadioButton_LightSpot_ColorMode_Custom_CheckedChanged;

            CheckBox_LightSpot_GradientWhenRandom.CheckedChanged -= CheckBox_LightSpot_GradientWhenRandom_CheckedChanged;
            CheckBox_LightSpot_GradientWhenRandom.Checked = Animation.Animations.LightSpot.Settings.GradientWhenRandom;
            CheckBox_LightSpot_GradientWhenRandom.CheckedChanged += CheckBox_LightSpot_GradientWhenRandom_CheckedChanged;
            CheckBox_LightSpot_GradientWhenRandom.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_LightSpot_GradientVelocity.Minimum = (int)(Animation.Animations.LightSpot.Settings.GradientVelocity_MinValue * 10);
            TrackBar_LightSpot_GradientVelocity.Maximum = (int)(Animation.Animations.LightSpot.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_LightSpot_GradientVelocity.Value = (int)(Animation.Animations.LightSpot.Settings.GradientVelocity * 10);
            TrackBar_LightSpot_GradientVelocity.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.LightSpot.Settings.GradientWhenRandom);

            Label_LightSpot_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.LightSpot.Settings.Color);
            Label_LightSpot_Color_Val.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_LightSpot_GlowMode_OuterGlow.CheckedChanged -= RadioButton_LightSpot_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_LightSpot_GlowMode_InnerGlow.CheckedChanged -= RadioButton_LightSpot_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_LightSpot_GlowMode_EvenGlow.CheckedChanged -= RadioButton_LightSpot_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.LightSpot.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_LightSpot_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_LightSpot_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_LightSpot_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_LightSpot_GlowMode_OuterGlow.CheckedChanged += RadioButton_LightSpot_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_LightSpot_GlowMode_InnerGlow.CheckedChanged += RadioButton_LightSpot_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_LightSpot_GlowMode_EvenGlow.CheckedChanged += RadioButton_LightSpot_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_LightSpot_MinRadius
        private void TrackBar_LightSpot_MinRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.MinRadius = TrackBar_LightSpot_MinRadius.Value * 0.01;
            // 同步最大值的自动修正
            TrackBar_LightSpot_MaxRadius.Value = (int)(Animation.Animations.LightSpot.Settings.MaxRadius * 100);
        }

        // 调节 TrackBar_LightSpot_MaxRadius
        private void TrackBar_LightSpot_MaxRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.MaxRadius = TrackBar_LightSpot_MaxRadius.Value * 0.01;
            // 同步最小值的自动修正
            TrackBar_LightSpot_MinRadius.Value = (int)(Animation.Animations.LightSpot.Settings.MinRadius * 100);
        }

        // 调节 TrackBar_LightSpot_Count
        private void TrackBar_LightSpot_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.Count = TrackBar_LightSpot_Count.Value;
        }

        // 调节 TrackBar_LightSpot_Amplitude
        private void TrackBar_LightSpot_Amplitude_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.Amplitude = TrackBar_LightSpot_Amplitude.Value * 0.01;
        }

        // 调节 TrackBar_LightSpot_WaveLength
        private void TrackBar_LightSpot_WaveLength_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.WaveLength = TrackBar_LightSpot_WaveLength.Value * 0.1;
        }

        // 调节 TrackBar_LightSpot_WaveVelocity
        private void TrackBar_LightSpot_WaveVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.WaveVelocity = TrackBar_LightSpot_WaveVelocity.Value * 0.01;
        }

        // RadioButton_LightSpot_ColorMode_Random 的 Checked 更改
        private void RadioButton_LightSpot_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_LightSpot_ColorMode_Random.Checked)
            {
                Animation.Animations.LightSpot.Settings.ColorMode = Animation.ColorModes.Random;
            }

            CheckBox_LightSpot_GradientWhenRandom.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Random);
            TrackBar_LightSpot_GradientVelocity.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.LightSpot.Settings.GradientWhenRandom);

            Label_LightSpot_Color_Val.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_LightSpot_GradientWhenRandom 的 Checked 更改
        private void CheckBox_LightSpot_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.GradientWhenRandom = CheckBox_LightSpot_GradientWhenRandom.Checked;
            TrackBar_LightSpot_GradientVelocity.Enabled = Animation.Animations.LightSpot.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_LightSpot_GradientVelocity
        private void TrackBar_LightSpot_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.GradientVelocity = TrackBar_LightSpot_GradientVelocity.Value * 0.1;
        }

        // RadioButton_LightSpot_ColorMode_Custom 的 Checked 更改
        private void RadioButton_LightSpot_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_LightSpot_ColorMode_Custom.Checked)
            {
                Animation.Animations.LightSpot.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            CheckBox_LightSpot_GradientWhenRandom.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Random);
            TrackBar_LightSpot_GradientVelocity.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.LightSpot.Settings.GradientWhenRandom);

            Label_LightSpot_Color_Val.Enabled = (Animation.Animations.LightSpot.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_LightSpot_Color_Val
        private void Label_LightSpot_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.LightSpot.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.LightSpot.Settings.Color = ColorDialog_Color.Color;
                Label_LightSpot_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.LightSpot.Settings.Color);
            }
        }

        // RadioButton_LightSpot_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_LightSpot_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_LightSpot_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.LightSpot.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_LightSpot_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_LightSpot_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_LightSpot_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.LightSpot.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_LightSpot_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_LightSpot_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_LightSpot_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.LightSpot.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_LightSpot_ResetToDefault
        private void Label_LightSpot_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.LightSpot.Settings.ResetToDefault();

            ResetTab_LightSpot();
        }

        #endregion

        #region 三角形碎片

        // 重置选项卡
        private void ResetTab_TrianglePiece()
        {
            TrackBar_TrianglePiece_MinRadius.Minimum = (int)(Animation.Animations.TrianglePiece.Settings.MinRadius_MinValue * 10);
            TrackBar_TrianglePiece_MinRadius.Maximum = (int)(Animation.Animations.TrianglePiece.Settings.MinRadius_MaxValue * 10);
            TrackBar_TrianglePiece_MinRadius.Value = (int)(Animation.Animations.TrianglePiece.Settings.MinRadius * 10);
            TrackBar_TrianglePiece_MaxRadius.Minimum = (int)(Animation.Animations.TrianglePiece.Settings.MaxRadius_MinValue * 10);
            TrackBar_TrianglePiece_MaxRadius.Maximum = (int)(Animation.Animations.TrianglePiece.Settings.MaxRadius_MaxValue * 10);
            TrackBar_TrianglePiece_MaxRadius.Value = (int)(Animation.Animations.TrianglePiece.Settings.MaxRadius * 10);

            TrackBar_TrianglePiece_Count.Minimum = Animation.Animations.TrianglePiece.Settings.Count_MinValue;
            TrackBar_TrianglePiece_Count.Maximum = Animation.Animations.TrianglePiece.Settings.Count_MaxValue;
            TrackBar_TrianglePiece_Count.Value = Animation.Animations.TrianglePiece.Settings.Count;

            TrackBar_TrianglePiece_Amplitude.Minimum = (int)(Animation.Animations.TrianglePiece.Settings.Amplitude_MinValue * 100);
            TrackBar_TrianglePiece_Amplitude.Maximum = (int)(Animation.Animations.TrianglePiece.Settings.Amplitude_MaxValue * 100);
            TrackBar_TrianglePiece_Amplitude.Value = (int)(Animation.Animations.TrianglePiece.Settings.Amplitude * 100);

            TrackBar_TrianglePiece_WaveLength.Minimum = (int)(Animation.Animations.TrianglePiece.Settings.WaveLength_MinValue * 10);
            TrackBar_TrianglePiece_WaveLength.Maximum = (int)(Animation.Animations.TrianglePiece.Settings.WaveLength_MaxValue * 10);
            TrackBar_TrianglePiece_WaveLength.Value = (int)(Animation.Animations.TrianglePiece.Settings.WaveLength * 10);

            TrackBar_TrianglePiece_WaveVelocity.Minimum = (int)(Animation.Animations.TrianglePiece.Settings.WaveVelocity_MinValue * 100);
            TrackBar_TrianglePiece_WaveVelocity.Maximum = (int)(Animation.Animations.TrianglePiece.Settings.WaveVelocity_MaxValue * 100);
            TrackBar_TrianglePiece_WaveVelocity.Value = (int)(Animation.Animations.TrianglePiece.Settings.WaveVelocity * 100);

            RadioButton_TrianglePiece_ColorMode_Random.CheckedChanged -= RadioButton_TrianglePiece_ColorMode_Random_CheckedChanged;
            RadioButton_TrianglePiece_ColorMode_Custom.CheckedChanged -= RadioButton_TrianglePiece_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.TrianglePiece.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_TrianglePiece_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_TrianglePiece_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_TrianglePiece_ColorMode_Random.CheckedChanged += RadioButton_TrianglePiece_ColorMode_Random_CheckedChanged;
            RadioButton_TrianglePiece_ColorMode_Custom.CheckedChanged += RadioButton_TrianglePiece_ColorMode_Custom_CheckedChanged;

            CheckBox_TrianglePiece_GradientWhenRandom.CheckedChanged -= CheckBox_TrianglePiece_GradientWhenRandom_CheckedChanged;
            CheckBox_TrianglePiece_GradientWhenRandom.Checked = Animation.Animations.TrianglePiece.Settings.GradientWhenRandom;
            CheckBox_TrianglePiece_GradientWhenRandom.CheckedChanged += CheckBox_TrianglePiece_GradientWhenRandom_CheckedChanged;
            CheckBox_TrianglePiece_GradientWhenRandom.Enabled = (Animation.Animations.TrianglePiece.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_TrianglePiece_GradientVelocity.Minimum = (int)(Animation.Animations.TrianglePiece.Settings.GradientVelocity_MinValue * 10);
            TrackBar_TrianglePiece_GradientVelocity.Maximum = (int)(Animation.Animations.TrianglePiece.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_TrianglePiece_GradientVelocity.Value = (int)(Animation.Animations.TrianglePiece.Settings.GradientVelocity * 10);
            TrackBar_TrianglePiece_GradientVelocity.Enabled = (Animation.Animations.TrianglePiece.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.TrianglePiece.Settings.GradientWhenRandom);

            Label_TrianglePiece_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.TrianglePiece.Settings.Color);
            Label_TrianglePiece_Color_Val.Enabled = (Animation.Animations.TrianglePiece.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_TrianglePiece_GlowMode_OuterGlow.CheckedChanged -= RadioButton_TrianglePiece_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_TrianglePiece_GlowMode_InnerGlow.CheckedChanged -= RadioButton_TrianglePiece_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_TrianglePiece_GlowMode_EvenGlow.CheckedChanged -= RadioButton_TrianglePiece_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.TrianglePiece.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_TrianglePiece_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_TrianglePiece_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_TrianglePiece_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_TrianglePiece_GlowMode_OuterGlow.CheckedChanged += RadioButton_TrianglePiece_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_TrianglePiece_GlowMode_InnerGlow.CheckedChanged += RadioButton_TrianglePiece_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_TrianglePiece_GlowMode_EvenGlow.CheckedChanged += RadioButton_TrianglePiece_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_TrianglePiece_MinRadius
        private void TrackBar_TrianglePiece_MinRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.MinRadius = TrackBar_TrianglePiece_MinRadius.Value * 0.1;
            // 同步最大值的自动修正
            TrackBar_TrianglePiece_MaxRadius.Value = (int)(Animation.Animations.TrianglePiece.Settings.MaxRadius * 10);
        }

        // 调节 TrackBar_TrianglePiece_MaxRadius
        private void TrackBar_TrianglePiece_MaxRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.MaxRadius = TrackBar_TrianglePiece_MaxRadius.Value * 0.1;
            // 同步最小值的自动修正
            TrackBar_TrianglePiece_MinRadius.Value = (int)(Animation.Animations.TrianglePiece.Settings.MinRadius * 10);
        }

        // 调节 TrackBar_TrianglePiece_Count
        private void TrackBar_TrianglePiece_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.Count = TrackBar_TrianglePiece_Count.Value;
        }

        // 调节 TrackBar_TrianglePiece_Amplitude
        private void TrackBar_TrianglePiece_Amplitude_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.Amplitude = TrackBar_TrianglePiece_Amplitude.Value * 0.01;
        }

        // 调节 TrackBar_TrianglePiece_WaveLength
        private void TrackBar_TrianglePiece_WaveLength_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.WaveLength = TrackBar_TrianglePiece_WaveLength.Value * 0.1;
        }

        // 调节 TrackBar_TrianglePiece_WaveVelocity
        private void TrackBar_TrianglePiece_WaveVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.WaveVelocity = TrackBar_TrianglePiece_WaveVelocity.Value * 0.01;
        }

        // RadioButton_TrianglePiece_ColorMode_Random 的 Checked 更改
        private void RadioButton_TrianglePiece_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_TrianglePiece_ColorMode_Random.Checked)
            {
                Animation.Animations.TrianglePiece.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_TrianglePiece_Color_Val.Enabled = (Animation.Animations.TrianglePiece.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_TrianglePiece_GradientWhenRandom 的 Checked 更改
        private void CheckBox_TrianglePiece_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.GradientWhenRandom = CheckBox_TrianglePiece_GradientWhenRandom.Checked;
            TrackBar_TrianglePiece_GradientVelocity.Enabled = Animation.Animations.TrianglePiece.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_TrianglePiece_GradientVelocity
        private void TrackBar_TrianglePiece_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.GradientVelocity = TrackBar_TrianglePiece_GradientVelocity.Value * 0.1;
        }

        // RadioButton_TrianglePiece_ColorMode_Custom 的 Checked 更改
        private void RadioButton_TrianglePiece_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_TrianglePiece_ColorMode_Custom.Checked)
            {
                Animation.Animations.TrianglePiece.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_TrianglePiece_Color_Val.Enabled = (Animation.Animations.TrianglePiece.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_TrianglePiece_Color_Val
        private void Label_TrianglePiece_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.TrianglePiece.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.TrianglePiece.Settings.Color = ColorDialog_Color.Color;
                Label_TrianglePiece_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.TrianglePiece.Settings.Color);
            }
        }

        // RadioButton_TrianglePiece_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_TrianglePiece_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_TrianglePiece_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.TrianglePiece.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_TrianglePiece_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_TrianglePiece_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_TrianglePiece_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.TrianglePiece.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_TrianglePiece_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_TrianglePiece_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_TrianglePiece_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.TrianglePiece.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_TrianglePiece_ResetToDefault
        private void Label_TrianglePiece_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.TrianglePiece.Settings.ResetToDefault();

            ResetTab_TrianglePiece();
        }

        #endregion

        #region 光芒

        // 重置选项卡
        private void ResetTab_Shine()
        {
            TrackBar_Shine_MinRadius.Minimum = (int)(Animation.Animations.Shine.Settings.MinRadius_MinValue * 10);
            TrackBar_Shine_MinRadius.Maximum = (int)(Animation.Animations.Shine.Settings.MinRadius_MaxValue * 10);
            TrackBar_Shine_MinRadius.Value = (int)(Animation.Animations.Shine.Settings.MinRadius * 10);
            TrackBar_Shine_MaxRadius.Minimum = (int)(Animation.Animations.Shine.Settings.MaxRadius_MinValue * 10);
            TrackBar_Shine_MaxRadius.Maximum = (int)(Animation.Animations.Shine.Settings.MaxRadius_MaxValue * 10);
            TrackBar_Shine_MaxRadius.Value = (int)(Animation.Animations.Shine.Settings.MaxRadius * 10);

            TrackBar_Shine_Count.Minimum = Animation.Animations.Shine.Settings.Count_MinValue;
            TrackBar_Shine_Count.Maximum = Animation.Animations.Shine.Settings.Count_MaxValue;
            TrackBar_Shine_Count.Value = Animation.Animations.Shine.Settings.Count;

            TrackBar_Shine_Displacement.Minimum = (int)(Animation.Animations.Shine.Settings.Displacement_MinValue * 100);
            TrackBar_Shine_Displacement.Maximum = (int)(Animation.Animations.Shine.Settings.Displacement_MaxValue * 100);
            TrackBar_Shine_Displacement.Value = (int)(Animation.Animations.Shine.Settings.Displacement * 100);

            TrackBar_Shine_MinPeriod.Minimum = (int)(Animation.Animations.Shine.Settings.MinPeriod_MinValue * 100);
            TrackBar_Shine_MinPeriod.Maximum = (int)(Animation.Animations.Shine.Settings.MinPeriod_MaxValue * 100);
            TrackBar_Shine_MinPeriod.Value = (int)(Animation.Animations.Shine.Settings.MinPeriod * 100);
            TrackBar_Shine_MaxPeriod.Minimum = (int)(Animation.Animations.Shine.Settings.MaxPeriod_MinValue * 100);
            TrackBar_Shine_MaxPeriod.Maximum = (int)(Animation.Animations.Shine.Settings.MaxPeriod_MaxValue * 100);
            TrackBar_Shine_MaxPeriod.Value = (int)(Animation.Animations.Shine.Settings.MaxPeriod * 100);

            RadioButton_Shine_ColorMode_Random.CheckedChanged -= RadioButton_Shine_ColorMode_Random_CheckedChanged;
            RadioButton_Shine_ColorMode_Custom.CheckedChanged -= RadioButton_Shine_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.Shine.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_Shine_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_Shine_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_Shine_ColorMode_Random.CheckedChanged += RadioButton_Shine_ColorMode_Random_CheckedChanged;
            RadioButton_Shine_ColorMode_Custom.CheckedChanged += RadioButton_Shine_ColorMode_Custom_CheckedChanged;

            CheckBox_Shine_GradientWhenRandom.CheckedChanged -= CheckBox_Shine_GradientWhenRandom_CheckedChanged;
            CheckBox_Shine_GradientWhenRandom.Checked = Animation.Animations.Shine.Settings.GradientWhenRandom;
            CheckBox_Shine_GradientWhenRandom.CheckedChanged += CheckBox_Shine_GradientWhenRandom_CheckedChanged;
            CheckBox_Shine_GradientWhenRandom.Enabled = (Animation.Animations.Shine.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_Shine_GradientVelocity.Minimum = (int)(Animation.Animations.Shine.Settings.GradientVelocity_MinValue * 10);
            TrackBar_Shine_GradientVelocity.Maximum = (int)(Animation.Animations.Shine.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_Shine_GradientVelocity.Value = (int)(Animation.Animations.Shine.Settings.GradientVelocity * 10);
            TrackBar_Shine_GradientVelocity.Enabled = (Animation.Animations.Shine.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.Shine.Settings.GradientWhenRandom);

            Label_Shine_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.Shine.Settings.Color);
            Label_Shine_Color_Val.Enabled = (Animation.Animations.Shine.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_Shine_GlowMode_OuterGlow.CheckedChanged -= RadioButton_Shine_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_Shine_GlowMode_InnerGlow.CheckedChanged -= RadioButton_Shine_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_Shine_GlowMode_EvenGlow.CheckedChanged -= RadioButton_Shine_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.Shine.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_Shine_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_Shine_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_Shine_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_Shine_GlowMode_OuterGlow.CheckedChanged += RadioButton_Shine_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_Shine_GlowMode_InnerGlow.CheckedChanged += RadioButton_Shine_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_Shine_GlowMode_EvenGlow.CheckedChanged += RadioButton_Shine_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_Shine_MinRadius
        private void TrackBar_Shine_MinRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.MinRadius = TrackBar_Shine_MinRadius.Value * 0.1;
            // 同步最大值的自动修正
            TrackBar_Shine_MaxRadius.Value = (int)(Animation.Animations.Shine.Settings.MaxRadius * 10);
        }

        // 调节 TrackBar_Shine_MaxRadius
        private void TrackBar_Shine_MaxRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.MaxRadius = TrackBar_Shine_MaxRadius.Value * 0.1;
            // 同步最小值的自动修正
            TrackBar_Shine_MinRadius.Value = (int)(Animation.Animations.Shine.Settings.MinRadius * 10);
        }

        // 调节 TrackBar_Shine_Count
        private void TrackBar_Shine_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.Count = TrackBar_Shine_Count.Value;
        }

        // 调节 TrackBar_Shine_Displacement
        private void TrackBar_Shine_Displacement_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.Displacement = TrackBar_Shine_Displacement.Value * 0.01;
            // 同步最大值的自动修正
            TrackBar_Shine_MaxPeriod.Value = (int)(Animation.Animations.Shine.Settings.MaxPeriod * 100);
        }

        // 调节 TrackBar_Shine_MinPeriod
        private void TrackBar_Shine_MinPeriod_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.MinPeriod = TrackBar_Shine_MinPeriod.Value * 0.01;
            // 同步最大值的自动修正
            TrackBar_Shine_MaxPeriod.Value = (int)(Animation.Animations.Shine.Settings.MaxPeriod * 100);
        }

        // 调节 TrackBar_Shine_MaxPeriod
        private void TrackBar_Shine_MaxPeriod_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.MaxPeriod = TrackBar_Shine_MaxPeriod.Value * 0.01;
            // 同步最小值的自动修正
            TrackBar_Shine_MinPeriod.Value = (int)(Animation.Animations.Shine.Settings.MinPeriod * 100);
        }

        // RadioButton_Shine_ColorMode_Random 的 Checked 更改
        private void RadioButton_Shine_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Shine_ColorMode_Random.Checked)
            {
                Animation.Animations.Shine.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_Shine_Color_Val.Enabled = (Animation.Animations.Shine.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_Shine_GradientWhenRandom 的 Checked 更改
        private void CheckBox_Shine_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.GradientWhenRandom = CheckBox_Shine_GradientWhenRandom.Checked;
            TrackBar_Shine_GradientVelocity.Enabled = Animation.Animations.Shine.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_Shine_GradientVelocity
        private void TrackBar_Shine_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.GradientVelocity = TrackBar_Shine_GradientVelocity.Value * 0.1;
        }

        // RadioButton_Shine_ColorMode_Custom 的 Checked 更改
        private void RadioButton_Shine_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Shine_ColorMode_Custom.Checked)
            {
                Animation.Animations.Shine.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_Shine_Color_Val.Enabled = (Animation.Animations.Shine.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_Shine_Color_Val
        private void Label_Shine_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.Shine.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.Shine.Settings.Color = ColorDialog_Color.Color;
                Label_Shine_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.Shine.Settings.Color);
            }
        }

        // RadioButton_Shine_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_Shine_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Shine_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.Shine.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_Shine_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_Shine_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Shine_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.Shine.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_Shine_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_Shine_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Shine_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.Shine.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_Shine_ResetToDefault
        private void Label_Shine_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.Shine.Settings.ResetToDefault();

            ResetTab_Shine();
        }

        #endregion

        #region 流星

        // 重置选项卡
        private void ResetTab_Meteor()
        {
            TrackBar_Meteor_MinRadius.Minimum = (int)(Animation.Animations.Meteor.Settings.MinRadius_MinValue * 100);
            TrackBar_Meteor_MinRadius.Maximum = (int)(Animation.Animations.Meteor.Settings.MinRadius_MaxValue * 100);
            TrackBar_Meteor_MinRadius.Value = (int)(Animation.Animations.Meteor.Settings.MinRadius * 100);
            TrackBar_Meteor_MaxRadius.Minimum = (int)(Animation.Animations.Meteor.Settings.MaxRadius_MinValue * 100);
            TrackBar_Meteor_MaxRadius.Maximum = (int)(Animation.Animations.Meteor.Settings.MaxRadius_MaxValue * 100);
            TrackBar_Meteor_MaxRadius.Value = (int)(Animation.Animations.Meteor.Settings.MaxRadius * 100);

            TrackBar_Meteor_Length.Minimum = (int)(Animation.Animations.Meteor.Settings.Length_MinValue * 10);
            TrackBar_Meteor_Length.Maximum = (int)(Animation.Animations.Meteor.Settings.Length_MaxValue * 10);
            TrackBar_Meteor_Length.Value = (int)(Animation.Animations.Meteor.Settings.Length * 10);

            TrackBar_Meteor_Count.Minimum = Animation.Animations.Meteor.Settings.Count_MinValue;
            TrackBar_Meteor_Count.Maximum = Animation.Animations.Meteor.Settings.Count_MaxValue;
            TrackBar_Meteor_Count.Value = Animation.Animations.Meteor.Settings.Count;

            TrackBar_Meteor_Velocity.Minimum = (int)(Animation.Animations.Meteor.Settings.Velocity_MinValue * 10);
            TrackBar_Meteor_Velocity.Maximum = (int)(Animation.Animations.Meteor.Settings.Velocity_MaxValue * 10);
            TrackBar_Meteor_Velocity.Value = (int)(Animation.Animations.Meteor.Settings.Velocity * 10);

            TrackBar_Meteor_Angle.Minimum = (int)(Animation.Animations.Meteor.Settings.Angle_MinValue * 10);
            TrackBar_Meteor_Angle.Maximum = (int)(Animation.Animations.Meteor.Settings.Angle_MaxValue * 10);
            TrackBar_Meteor_Angle.Value = (int)(Animation.Animations.Meteor.Settings.Angle * 10);

            TrackBar_Meteor_GravitationalAcceleration.Minimum = (int)(Animation.Animations.Meteor.Settings.GravitationalAcceleration_MinValue * 100);
            TrackBar_Meteor_GravitationalAcceleration.Maximum = (int)(Animation.Animations.Meteor.Settings.GravitationalAcceleration_MaxValue * 100);
            TrackBar_Meteor_GravitationalAcceleration.Value = (int)(Animation.Animations.Meteor.Settings.GravitationalAcceleration * 100);

            RadioButton_Meteor_ColorMode_Random.CheckedChanged -= RadioButton_Meteor_ColorMode_Random_CheckedChanged;
            RadioButton_Meteor_ColorMode_Custom.CheckedChanged -= RadioButton_Meteor_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.Meteor.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_Meteor_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_Meteor_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_Meteor_ColorMode_Random.CheckedChanged += RadioButton_Meteor_ColorMode_Random_CheckedChanged;
            RadioButton_Meteor_ColorMode_Custom.CheckedChanged += RadioButton_Meteor_ColorMode_Custom_CheckedChanged;

            CheckBox_Meteor_GradientWhenRandom.CheckedChanged -= CheckBox_Meteor_GradientWhenRandom_CheckedChanged;
            CheckBox_Meteor_GradientWhenRandom.Checked = Animation.Animations.Meteor.Settings.GradientWhenRandom;
            CheckBox_Meteor_GradientWhenRandom.CheckedChanged += CheckBox_Meteor_GradientWhenRandom_CheckedChanged;
            CheckBox_Meteor_GradientWhenRandom.Enabled = (Animation.Animations.Meteor.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_Meteor_GradientVelocity.Minimum = (int)(Animation.Animations.Meteor.Settings.GradientVelocity_MinValue * 10);
            TrackBar_Meteor_GradientVelocity.Maximum = (int)(Animation.Animations.Meteor.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_Meteor_GradientVelocity.Value = (int)(Animation.Animations.Meteor.Settings.GradientVelocity * 10);
            TrackBar_Meteor_GradientVelocity.Enabled = (Animation.Animations.Meteor.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.Meteor.Settings.GradientWhenRandom);

            Label_Meteor_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.Meteor.Settings.Color);
            Label_Meteor_Color_Val.Enabled = (Animation.Animations.Meteor.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_Meteor_GlowMode_OuterGlow.CheckedChanged -= RadioButton_Meteor_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_Meteor_GlowMode_InnerGlow.CheckedChanged -= RadioButton_Meteor_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_Meteor_GlowMode_EvenGlow.CheckedChanged -= RadioButton_Meteor_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.Meteor.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_Meteor_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_Meteor_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_Meteor_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_Meteor_GlowMode_OuterGlow.CheckedChanged += RadioButton_Meteor_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_Meteor_GlowMode_InnerGlow.CheckedChanged += RadioButton_Meteor_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_Meteor_GlowMode_EvenGlow.CheckedChanged += RadioButton_Meteor_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_Meteor_MinRadius
        private void TrackBar_Meteor_MinRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.MinRadius = TrackBar_Meteor_MinRadius.Value * 0.01;
            // 同步最大值的自动修正
            TrackBar_Meteor_MaxRadius.Value = (int)(Animation.Animations.Meteor.Settings.MaxRadius * 100);
        }

        // 调节 TrackBar_Meteor_MaxRadius
        private void TrackBar_Meteor_MaxRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.MaxRadius = TrackBar_Meteor_MaxRadius.Value * 0.01;
            // 同步最小值的自动修正
            TrackBar_Meteor_MinRadius.Value = (int)(Animation.Animations.Meteor.Settings.MinRadius * 100);
        }

        // 调节 TrackBar_Meteor_Length
        private void TrackBar_Meteor_Length_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.Length = TrackBar_Meteor_Length.Value * 0.1;
        }

        // 调节 TrackBar_Meteor_Count
        private void TrackBar_Meteor_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.Count = TrackBar_Meteor_Count.Value;
        }

        // 调节 TrackBar_Meteor_Velocity
        private void TrackBar_Meteor_Velocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.Velocity = TrackBar_Meteor_Velocity.Value * 0.1;
        }

        // 调节 TrackBar_Meteor_Angle
        private void TrackBar_Meteor_Angle_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.Angle = TrackBar_Meteor_Angle.Value * 0.1;
        }

        // 调节 TrackBar_Meteor_GravitationalAcceleration
        private void TrackBar_Meteor_GravitationalAcceleration_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.GravitationalAcceleration = TrackBar_Meteor_GravitationalAcceleration.Value * 0.01;
        }

        // RadioButton_Meteor_ColorMode_Random 的 Checked 更改
        private void RadioButton_Meteor_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Meteor_ColorMode_Random.Checked)
            {
                Animation.Animations.Meteor.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_Meteor_Color_Val.Enabled = (Animation.Animations.Meteor.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_Meteor_GradientWhenRandom 的 Checked 更改
        private void CheckBox_Meteor_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.GradientWhenRandom = CheckBox_Meteor_GradientWhenRandom.Checked;
            TrackBar_Meteor_GradientVelocity.Enabled = Animation.Animations.Meteor.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_Meteor_GradientVelocity
        private void TrackBar_Meteor_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.GradientVelocity = TrackBar_Meteor_GradientVelocity.Value * 0.1;
        }

        // RadioButton_Meteor_ColorMode_Custom 的 Checked 更改
        private void RadioButton_Meteor_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Meteor_ColorMode_Custom.Checked)
            {
                Animation.Animations.Meteor.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_Meteor_Color_Val.Enabled = (Animation.Animations.Meteor.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_Meteor_Color_Val
        private void Label_Meteor_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.Meteor.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.Meteor.Settings.Color = ColorDialog_Color.Color;
                Label_Meteor_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.Meteor.Settings.Color);
            }
        }

        // RadioButton_Meteor_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_Meteor_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Meteor_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.Meteor.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_Meteor_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_Meteor_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Meteor_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.Meteor.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_Meteor_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_Meteor_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Meteor_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.Meteor.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_Meteor_ResetToDefault
        private void Label_Meteor_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.Meteor.Settings.ResetToDefault();

            ResetTab_Meteor();
        }

        #endregion

        #region 雪

        // 重置选项卡
        private void ResetTab_Snow()
        {
            TrackBar_Snow_MinRadius.Minimum = (int)(Animation.Animations.Snow.Settings.MinRadius_MinValue * 100);
            TrackBar_Snow_MinRadius.Maximum = (int)(Animation.Animations.Snow.Settings.MinRadius_MaxValue * 100);
            TrackBar_Snow_MinRadius.Value = (int)(Animation.Animations.Snow.Settings.MinRadius * 100);
            TrackBar_Snow_MaxRadius.Minimum = (int)(Animation.Animations.Snow.Settings.MaxRadius_MinValue * 100);
            TrackBar_Snow_MaxRadius.Maximum = (int)(Animation.Animations.Snow.Settings.MaxRadius_MaxValue * 100);
            TrackBar_Snow_MaxRadius.Value = (int)(Animation.Animations.Snow.Settings.MaxRadius * 100);

            TrackBar_Snow_Count.Minimum = Animation.Animations.Snow.Settings.Count_MinValue;
            TrackBar_Snow_Count.Maximum = Animation.Animations.Snow.Settings.Count_MaxValue;
            TrackBar_Snow_Count.Value = Animation.Animations.Snow.Settings.Count;

            TrackBar_Snow_Velocity.Minimum = (int)(Animation.Animations.Snow.Settings.Velocity_MinValue * 10);
            TrackBar_Snow_Velocity.Maximum = (int)(Animation.Animations.Snow.Settings.Velocity_MaxValue * 10);
            TrackBar_Snow_Velocity.Value = (int)(Animation.Animations.Snow.Settings.Velocity * 10);

            RadioButton_Snow_ColorMode_Random.CheckedChanged -= RadioButton_Snow_ColorMode_Random_CheckedChanged;
            RadioButton_Snow_ColorMode_Custom.CheckedChanged -= RadioButton_Snow_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.Snow.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_Snow_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_Snow_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_Snow_ColorMode_Random.CheckedChanged += RadioButton_Snow_ColorMode_Random_CheckedChanged;
            RadioButton_Snow_ColorMode_Custom.CheckedChanged += RadioButton_Snow_ColorMode_Custom_CheckedChanged;

            CheckBox_Snow_GradientWhenRandom.CheckedChanged -= CheckBox_Snow_GradientWhenRandom_CheckedChanged;
            CheckBox_Snow_GradientWhenRandom.Checked = Animation.Animations.Snow.Settings.GradientWhenRandom;
            CheckBox_Snow_GradientWhenRandom.CheckedChanged += CheckBox_Snow_GradientWhenRandom_CheckedChanged;
            CheckBox_Snow_GradientWhenRandom.Enabled = (Animation.Animations.Snow.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_Snow_GradientVelocity.Minimum = (int)(Animation.Animations.Snow.Settings.GradientVelocity_MinValue * 10);
            TrackBar_Snow_GradientVelocity.Maximum = (int)(Animation.Animations.Snow.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_Snow_GradientVelocity.Value = (int)(Animation.Animations.Snow.Settings.GradientVelocity * 10);
            TrackBar_Snow_GradientVelocity.Enabled = (Animation.Animations.Snow.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.Snow.Settings.GradientWhenRandom);

            Label_Snow_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.Snow.Settings.Color);
            Label_Snow_Color_Val.Enabled = (Animation.Animations.Snow.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_Snow_GlowMode_OuterGlow.CheckedChanged -= RadioButton_Snow_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_Snow_GlowMode_InnerGlow.CheckedChanged -= RadioButton_Snow_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_Snow_GlowMode_EvenGlow.CheckedChanged -= RadioButton_Snow_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.Snow.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_Snow_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_Snow_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_Snow_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_Snow_GlowMode_OuterGlow.CheckedChanged += RadioButton_Snow_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_Snow_GlowMode_InnerGlow.CheckedChanged += RadioButton_Snow_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_Snow_GlowMode_EvenGlow.CheckedChanged += RadioButton_Snow_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_Snow_MinRadius
        private void TrackBar_Snow_MinRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.MinRadius = TrackBar_Snow_MinRadius.Value * 0.01;
            // 同步最大值的自动修正
            TrackBar_Snow_MaxRadius.Value = (int)(Animation.Animations.Snow.Settings.MaxRadius * 100);
        }

        // 调节 TrackBar_Snow_MaxRadius
        private void TrackBar_Snow_MaxRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.MaxRadius = TrackBar_Snow_MaxRadius.Value * 0.01;
            // 同步最小值的自动修正
            TrackBar_Snow_MinRadius.Value = (int)(Animation.Animations.Snow.Settings.MinRadius * 100);
        }

        // 调节 TrackBar_Snow_Count
        private void TrackBar_Snow_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.Count = TrackBar_Snow_Count.Value;
        }

        // 调节 TrackBar_Snow_Velocity
        private void TrackBar_Snow_Velocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.Velocity = TrackBar_Snow_Velocity.Value * 0.1;
        }

        // RadioButton_Snow_ColorMode_Random 的 Checked 更改
        private void RadioButton_Snow_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Snow_ColorMode_Random.Checked)
            {
                Animation.Animations.Snow.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_Snow_Color_Val.Enabled = (Animation.Animations.Snow.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_Snow_GradientWhenRandom 的 Checked 更改
        private void CheckBox_Snow_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.GradientWhenRandom = CheckBox_Snow_GradientWhenRandom.Checked;
            TrackBar_Snow_GradientVelocity.Enabled = Animation.Animations.Snow.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_Snow_GradientVelocity
        private void TrackBar_Snow_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.GradientVelocity = TrackBar_Snow_GradientVelocity.Value * 0.1;
        }

        // RadioButton_Snow_ColorMode_Custom 的 Checked 更改
        private void RadioButton_Snow_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Snow_ColorMode_Custom.Checked)
            {
                Animation.Animations.Snow.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_Snow_Color_Val.Enabled = (Animation.Animations.Snow.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_Snow_Color_Val
        private void Label_Snow_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.Snow.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.Snow.Settings.Color = ColorDialog_Color.Color;
                Label_Snow_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.Snow.Settings.Color);
            }
        }

        // RadioButton_Snow_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_Snow_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Snow_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.Snow.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_Snow_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_Snow_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Snow_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.Snow.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_Snow_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_Snow_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_Snow_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.Snow.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_Snow_ResetToDefault
        private void Label_Snow_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.Snow.Settings.ResetToDefault();

            ResetTab_Snow();
        }

        #endregion

        #region 引力粒子

        // 重置选项卡
        private void ResetTab_GravityParticle()
        {
            TrackBar_GravityParticle_MinMass.Minimum = (int)Animation.Animations.GravityParticle.Settings.MinMass_MinValue;
            TrackBar_GravityParticle_MinMass.Maximum = (int)Animation.Animations.GravityParticle.Settings.MinMass_MaxValue;
            TrackBar_GravityParticle_MinMass.Value = (int)Animation.Animations.GravityParticle.Settings.MinMass;
            TrackBar_GravityParticle_MaxMass.Minimum = (int)Animation.Animations.GravityParticle.Settings.MaxMass_MinValue;
            TrackBar_GravityParticle_MaxMass.Maximum = (int)Animation.Animations.GravityParticle.Settings.MaxMass_MaxValue;
            TrackBar_GravityParticle_MaxMass.Value = (int)Animation.Animations.GravityParticle.Settings.MaxMass;

            TrackBar_GravityParticle_Count.Minimum = Animation.Animations.GravityParticle.Settings.Count_MinValue;
            TrackBar_GravityParticle_Count.Maximum = Animation.Animations.GravityParticle.Settings.Count_MaxValue;
            TrackBar_GravityParticle_Count.Value = Animation.Animations.GravityParticle.Settings.Count;

            TrackBar_GravityParticle_CursorMass.Minimum = (int)Animation.Animations.GravityParticle.Settings.CursorMass_MinValue;
            TrackBar_GravityParticle_CursorMass.Maximum = (int)Animation.Animations.GravityParticle.Settings.CursorMass_MaxValue;
            TrackBar_GravityParticle_CursorMass.Value = (int)Animation.Animations.GravityParticle.Settings.CursorMass;

            TrackBar_GravityParticle_GravityConstant.Minimum = (int)(Animation.Animations.GravityParticle.Settings.GravityConstant_MinValue * 100);
            TrackBar_GravityParticle_GravityConstant.Maximum = (int)(Animation.Animations.GravityParticle.Settings.GravityConstant_MaxValue * 100);
            TrackBar_GravityParticle_GravityConstant.Value = (int)(Animation.Animations.GravityParticle.Settings.GravityConstant * 100);

            TrackBar_GravityParticle_ElasticRestitutionCoefficient.Minimum = (int)(Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient_MinValue * 1000);
            TrackBar_GravityParticle_ElasticRestitutionCoefficient.Maximum = (int)(Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient_MaxValue * 1000);
            TrackBar_GravityParticle_ElasticRestitutionCoefficient.Value = (int)(Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient * 1000);

            RadioButton_GravityParticle_ColorMode_Random.CheckedChanged -= RadioButton_GravityParticle_ColorMode_Random_CheckedChanged;
            RadioButton_GravityParticle_ColorMode_Custom.CheckedChanged -= RadioButton_GravityParticle_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.GravityParticle.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_GravityParticle_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_GravityParticle_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_GravityParticle_ColorMode_Random.CheckedChanged += RadioButton_GravityParticle_ColorMode_Random_CheckedChanged;
            RadioButton_GravityParticle_ColorMode_Custom.CheckedChanged += RadioButton_GravityParticle_ColorMode_Custom_CheckedChanged;

            CheckBox_GravityParticle_GradientWhenRandom.CheckedChanged -= CheckBox_GravityParticle_GradientWhenRandom_CheckedChanged;
            CheckBox_GravityParticle_GradientWhenRandom.Checked = Animation.Animations.GravityParticle.Settings.GradientWhenRandom;
            CheckBox_GravityParticle_GradientWhenRandom.CheckedChanged += CheckBox_GravityParticle_GradientWhenRandom_CheckedChanged;
            CheckBox_GravityParticle_GradientWhenRandom.Enabled = (Animation.Animations.GravityParticle.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_GravityParticle_GradientVelocity.Minimum = (int)(Animation.Animations.GravityParticle.Settings.GradientVelocity_MinValue * 10);
            TrackBar_GravityParticle_GradientVelocity.Maximum = (int)(Animation.Animations.GravityParticle.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_GravityParticle_GradientVelocity.Value = (int)(Animation.Animations.GravityParticle.Settings.GradientVelocity * 10);
            TrackBar_GravityParticle_GradientVelocity.Enabled = (Animation.Animations.GravityParticle.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.GravityParticle.Settings.GradientWhenRandom);

            Label_GravityParticle_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.GravityParticle.Settings.Color);
            Label_GravityParticle_Color_Val.Enabled = (Animation.Animations.GravityParticle.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_GravityParticle_GlowMode_OuterGlow.CheckedChanged -= RadioButton_GravityParticle_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_GravityParticle_GlowMode_InnerGlow.CheckedChanged -= RadioButton_GravityParticle_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_GravityParticle_GlowMode_EvenGlow.CheckedChanged -= RadioButton_GravityParticle_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.GravityParticle.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_GravityParticle_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_GravityParticle_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_GravityParticle_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_GravityParticle_GlowMode_OuterGlow.CheckedChanged += RadioButton_GravityParticle_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_GravityParticle_GlowMode_InnerGlow.CheckedChanged += RadioButton_GravityParticle_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_GravityParticle_GlowMode_EvenGlow.CheckedChanged += RadioButton_GravityParticle_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_GravityParticle_MinMass
        private void TrackBar_GravityParticle_MinMass_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.MinMass = TrackBar_GravityParticle_MinMass.Value;
            // 同步最大值的自动修正
            TrackBar_GravityParticle_MaxMass.Value = (int)Animation.Animations.GravityParticle.Settings.MaxMass;
        }

        // 调节 TrackBar_GravityParticle_MaxMass
        private void TrackBar_GravityParticle_MaxMass_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.MaxMass = TrackBar_GravityParticle_MaxMass.Value;
            // 同步最小值的自动修正
            TrackBar_GravityParticle_MinMass.Value = (int)Animation.Animations.GravityParticle.Settings.MinMass;
        }

        // 调节 TrackBar_GravityParticle_Count
        private void TrackBar_GravityParticle_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.Count = TrackBar_GravityParticle_Count.Value;
        }

        // 调节 TrackBar_GravityParticle_CursorMass
        private void TrackBar_GravityParticle_CursorMass_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.CursorMass = TrackBar_GravityParticle_CursorMass.Value;
        }

        // 调节 TrackBar_GravityParticle_GravityConstant
        private void TrackBar_GravityParticle_GravityConstant_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.GravityConstant = TrackBar_GravityParticle_GravityConstant.Value * 0.01;
        }

        // 调节 TrackBar_GravityParticle_ElasticRestitutionCoefficient
        private void TrackBar_GravityParticle_ElasticRestitutionCoefficient_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient = TrackBar_GravityParticle_ElasticRestitutionCoefficient.Value * 0.001;
        }

        // RadioButton_GravityParticle_ColorMode_Random 的 Checked 更改
        private void RadioButton_GravityParticle_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityParticle_ColorMode_Random.Checked)
            {
                Animation.Animations.GravityParticle.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_GravityParticle_Color_Val.Enabled = (Animation.Animations.GravityParticle.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_GravityParticle_GradientWhenRandom 的 Checked 更改
        private void CheckBox_GravityParticle_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.GradientWhenRandom = CheckBox_GravityParticle_GradientWhenRandom.Checked;
            TrackBar_GravityParticle_GradientVelocity.Enabled = Animation.Animations.GravityParticle.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_GravityParticle_GradientVelocity
        private void TrackBar_GravityParticle_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.GradientVelocity = TrackBar_GravityParticle_GradientVelocity.Value * 0.1;
        }

        // RadioButton_GravityParticle_ColorMode_Custom 的 Checked 更改
        private void RadioButton_GravityParticle_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityParticle_ColorMode_Custom.Checked)
            {
                Animation.Animations.GravityParticle.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_GravityParticle_Color_Val.Enabled = (Animation.Animations.GravityParticle.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_GravityParticle_Color_Val
        private void Label_GravityParticle_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.GravityParticle.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.GravityParticle.Settings.Color = ColorDialog_Color.Color;
                Label_GravityParticle_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.GravityParticle.Settings.Color);
            }
        }

        // RadioButton_GravityParticle_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_GravityParticle_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityParticle_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.GravityParticle.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_GravityParticle_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_GravityParticle_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityParticle_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.GravityParticle.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_GravityParticle_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_GravityParticle_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityParticle_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.GravityParticle.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_GravityParticle_ResetToDefault
        private void Label_GravityParticle_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.GravityParticle.Settings.ResetToDefault();

            ResetTab_GravityParticle();
        }

        #endregion

        #region 引力网

        // 重置选项卡
        private void ResetTab_GravityGrid()
        {
            TrackBar_GravityGrid_Radius.Minimum = (int)(Animation.Animations.GravityGrid.Settings.Radius_MinValue * 100);
            TrackBar_GravityGrid_Radius.Maximum = (int)(Animation.Animations.GravityGrid.Settings.Radius_MaxValue * 100);
            TrackBar_GravityGrid_Radius.Value = (int)(Animation.Animations.GravityGrid.Settings.Radius * 100);

            TrackBar_GravityGrid_Count.Minimum = Animation.Animations.GravityGrid.Settings.Count_MinValue;
            TrackBar_GravityGrid_Count.Maximum = Animation.Animations.GravityGrid.Settings.Count_MaxValue;
            TrackBar_GravityGrid_Count.Value = Animation.Animations.GravityGrid.Settings.Count;

            TrackBar_GravityGrid_LineWidth.Minimum = (int)(Animation.Animations.GravityGrid.Settings.LineWidth_MinValue * 100);
            TrackBar_GravityGrid_LineWidth.Maximum = (int)(Animation.Animations.GravityGrid.Settings.LineWidth_MaxValue * 100);
            TrackBar_GravityGrid_LineWidth.Value = (int)(Animation.Animations.GravityGrid.Settings.LineWidth * 100);

            TrackBar_GravityGrid_CursorMass.Minimum = (int)Animation.Animations.GravityGrid.Settings.CursorMass_MinValue;
            TrackBar_GravityGrid_CursorMass.Maximum = (int)Animation.Animations.GravityGrid.Settings.CursorMass_MaxValue;
            TrackBar_GravityGrid_CursorMass.Value = (int)Animation.Animations.GravityGrid.Settings.CursorMass;

            TrackBar_GravityGrid_CursorRepulsionRadius.Minimum = (int)Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius_MinValue;
            TrackBar_GravityGrid_CursorRepulsionRadius.Maximum = (int)Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius_MaxValue;
            TrackBar_GravityGrid_CursorRepulsionRadius.Value = (int)Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius;

            RadioButton_GravityGrid_ColorMode_Random.CheckedChanged -= RadioButton_GravityGrid_ColorMode_Random_CheckedChanged;
            RadioButton_GravityGrid_ColorMode_Custom.CheckedChanged -= RadioButton_GravityGrid_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.GravityGrid.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_GravityGrid_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_GravityGrid_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_GravityGrid_ColorMode_Random.CheckedChanged += RadioButton_GravityGrid_ColorMode_Random_CheckedChanged;
            RadioButton_GravityGrid_ColorMode_Custom.CheckedChanged += RadioButton_GravityGrid_ColorMode_Custom_CheckedChanged;

            CheckBox_GravityGrid_GradientWhenRandom.CheckedChanged -= CheckBox_GravityGrid_GradientWhenRandom_CheckedChanged;
            CheckBox_GravityGrid_GradientWhenRandom.Checked = Animation.Animations.GravityGrid.Settings.GradientWhenRandom;
            CheckBox_GravityGrid_GradientWhenRandom.CheckedChanged += CheckBox_GravityGrid_GradientWhenRandom_CheckedChanged;
            CheckBox_GravityGrid_GradientWhenRandom.Enabled = (Animation.Animations.GravityGrid.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_GravityGrid_GradientVelocity.Minimum = (int)(Animation.Animations.GravityGrid.Settings.GradientVelocity_MinValue * 10);
            TrackBar_GravityGrid_GradientVelocity.Maximum = (int)(Animation.Animations.GravityGrid.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_GravityGrid_GradientVelocity.Value = (int)(Animation.Animations.GravityGrid.Settings.GradientVelocity * 10);
            TrackBar_GravityGrid_GradientVelocity.Enabled = (Animation.Animations.GravityGrid.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.GravityGrid.Settings.GradientWhenRandom);

            Label_GravityGrid_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.GravityGrid.Settings.Color);
            Label_GravityGrid_Color_Val.Enabled = (Animation.Animations.GravityGrid.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_GravityGrid_GlowMode_OuterGlow.CheckedChanged -= RadioButton_GravityGrid_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_GravityGrid_GlowMode_InnerGlow.CheckedChanged -= RadioButton_GravityGrid_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_GravityGrid_GlowMode_EvenGlow.CheckedChanged -= RadioButton_GravityGrid_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.GravityGrid.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_GravityGrid_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_GravityGrid_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_GravityGrid_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_GravityGrid_GlowMode_OuterGlow.CheckedChanged += RadioButton_GravityGrid_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_GravityGrid_GlowMode_InnerGlow.CheckedChanged += RadioButton_GravityGrid_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_GravityGrid_GlowMode_EvenGlow.CheckedChanged += RadioButton_GravityGrid_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_GravityGrid_Radius
        private void TrackBar_GravityGrid_Radius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.Radius = TrackBar_GravityGrid_Radius.Value * 0.01;
        }

        // 调节 TrackBar_GravityGrid_Count
        private void TrackBar_GravityGrid_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.Count = TrackBar_GravityGrid_Count.Value;
        }

        // 调节 TrackBar_GravityGrid_LineWidth
        private void TrackBar_GravityGrid_LineWidth_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.LineWidth = TrackBar_GravityGrid_LineWidth.Value * 0.01F;
        }

        // 调节 TrackBar_GravityGrid_CursorMass
        private void TrackBar_GravityGrid_CursorMass_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.CursorMass = TrackBar_GravityGrid_CursorMass.Value;
        }

        // 调节 TrackBar_GravityGrid_CursorRepulsionRadius
        private void TrackBar_GravityGrid_CursorRepulsionRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius = TrackBar_GravityGrid_CursorRepulsionRadius.Value;
        }

        // RadioButton_GravityGrid_ColorMode_Random 的 Checked 更改
        private void RadioButton_GravityGrid_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityGrid_ColorMode_Random.Checked)
            {
                Animation.Animations.GravityGrid.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_GravityGrid_Color_Val.Enabled = (Animation.Animations.GravityGrid.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_GravityGrid_GradientWhenRandom 的 Checked 更改
        private void CheckBox_GravityGrid_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.GradientWhenRandom = CheckBox_GravityGrid_GradientWhenRandom.Checked;
            TrackBar_GravityGrid_GradientVelocity.Enabled = Animation.Animations.GravityGrid.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_GravityGrid_GradientVelocity
        private void TrackBar_GravityGrid_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.GradientVelocity = TrackBar_GravityGrid_GradientVelocity.Value * 0.1;
        }

        // RadioButton_GravityGrid_ColorMode_Custom 的 Checked 更改
        private void RadioButton_GravityGrid_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityGrid_ColorMode_Custom.Checked)
            {
                Animation.Animations.GravityGrid.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_GravityGrid_Color_Val.Enabled = (Animation.Animations.GravityGrid.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_GravityGrid_Color_Val
        private void Label_GravityGrid_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.GravityGrid.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.GravityGrid.Settings.Color = ColorDialog_Color.Color;
                Label_GravityGrid_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.GravityGrid.Settings.Color);
            }
        }

        // RadioButton_GravityGrid_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_GravityGrid_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityGrid_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.GravityGrid.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_GravityGrid_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_GravityGrid_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityGrid_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.GravityGrid.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_GravityGrid_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_GravityGrid_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_GravityGrid_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.GravityGrid.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_GravityGrid_ResetToDefault
        private void Label_GravityGrid_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.GravityGrid.Settings.ResetToDefault();

            ResetTab_GravityGrid();
        }

        #endregion

        #region 扩散光点

        // 重置选项卡
        private void ResetTab_SpreadSpot()
        {
            TrackBar_SpreadSpot_MinRadius.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.MinRadius_MinValue * 100);
            TrackBar_SpreadSpot_MinRadius.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.MinRadius_MaxValue * 100);
            TrackBar_SpreadSpot_MinRadius.Value = (int)(Animation.Animations.SpreadSpot.Settings.MinRadius * 100);
            TrackBar_SpreadSpot_MaxRadius.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.MaxRadius_MinValue * 100);
            TrackBar_SpreadSpot_MaxRadius.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.MaxRadius_MaxValue * 100);
            TrackBar_SpreadSpot_MaxRadius.Value = (int)(Animation.Animations.SpreadSpot.Settings.MaxRadius * 100);

            TrackBar_SpreadSpot_Count.Minimum = Animation.Animations.SpreadSpot.Settings.Count_MinValue;
            TrackBar_SpreadSpot_Count.Maximum = Animation.Animations.SpreadSpot.Settings.Count_MaxValue;
            TrackBar_SpreadSpot_Count.Value = Animation.Animations.SpreadSpot.Settings.Count;

            TrackBar_SpreadSpot_SourceX.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.SourceX_MinValue * 1000);
            TrackBar_SpreadSpot_SourceX.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.SourceX_MaxValue * 1000);
            TrackBar_SpreadSpot_SourceX.Value = (int)(Animation.Animations.SpreadSpot.Settings.SourceX * 1000);
            TrackBar_SpreadSpot_SourceY.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.SourceY_MinValue * 1000);
            TrackBar_SpreadSpot_SourceY.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.SourceY_MaxValue * 1000);
            TrackBar_SpreadSpot_SourceY.Value = (int)(Animation.Animations.SpreadSpot.Settings.SourceY * 1000);
            TrackBar_SpreadSpot_SourceZ.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.SourceZ_MinValue * 1000);
            TrackBar_SpreadSpot_SourceZ.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.SourceZ_MaxValue * 1000);
            TrackBar_SpreadSpot_SourceZ.Value = (int)(Animation.Animations.SpreadSpot.Settings.SourceZ * 1000);

            TrackBar_SpreadSpot_SourceSize.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.SourceSize_MinValue * 1000);
            TrackBar_SpreadSpot_SourceSize.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.SourceSize_MaxValue * 1000);
            TrackBar_SpreadSpot_SourceSize.Value = (int)(Animation.Animations.SpreadSpot.Settings.SourceSize * 1000);

            TrackBar_SpreadSpot_Velocity.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.Velocity_MinValue * 100);
            TrackBar_SpreadSpot_Velocity.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.Velocity_MaxValue * 100);
            TrackBar_SpreadSpot_Velocity.Value = (int)(Animation.Animations.SpreadSpot.Settings.Velocity * 100);

            RadioButton_SpreadSpot_ColorMode_Random.CheckedChanged -= RadioButton_SpreadSpot_ColorMode_Random_CheckedChanged;
            RadioButton_SpreadSpot_ColorMode_Custom.CheckedChanged -= RadioButton_SpreadSpot_ColorMode_Custom_CheckedChanged;
            switch (Animation.Animations.SpreadSpot.Settings.ColorMode)
            {
                case Animation.ColorModes.Random: RadioButton_SpreadSpot_ColorMode_Random.Checked = true; break;
                case Animation.ColorModes.Custom: RadioButton_SpreadSpot_ColorMode_Custom.Checked = true; break;
            }
            RadioButton_SpreadSpot_ColorMode_Random.CheckedChanged += RadioButton_SpreadSpot_ColorMode_Random_CheckedChanged;
            RadioButton_SpreadSpot_ColorMode_Custom.CheckedChanged += RadioButton_SpreadSpot_ColorMode_Custom_CheckedChanged;

            CheckBox_SpreadSpot_GradientWhenRandom.CheckedChanged -= CheckBox_SpreadSpot_GradientWhenRandom_CheckedChanged;
            CheckBox_SpreadSpot_GradientWhenRandom.Checked = Animation.Animations.SpreadSpot.Settings.GradientWhenRandom;
            CheckBox_SpreadSpot_GradientWhenRandom.CheckedChanged += CheckBox_SpreadSpot_GradientWhenRandom_CheckedChanged;
            CheckBox_SpreadSpot_GradientWhenRandom.Enabled = (Animation.Animations.SpreadSpot.Settings.ColorMode == Animation.ColorModes.Random);

            TrackBar_SpreadSpot_GradientVelocity.Minimum = (int)(Animation.Animations.SpreadSpot.Settings.GradientVelocity_MinValue * 10);
            TrackBar_SpreadSpot_GradientVelocity.Maximum = (int)(Animation.Animations.SpreadSpot.Settings.GradientVelocity_MaxValue * 10);
            TrackBar_SpreadSpot_GradientVelocity.Value = (int)(Animation.Animations.SpreadSpot.Settings.GradientVelocity * 10);
            TrackBar_SpreadSpot_GradientVelocity.Enabled = (Animation.Animations.SpreadSpot.Settings.ColorMode == Animation.ColorModes.Random && Animation.Animations.SpreadSpot.Settings.GradientWhenRandom);

            Label_SpreadSpot_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.SpreadSpot.Settings.Color);
            Label_SpreadSpot_Color_Val.Enabled = (Animation.Animations.SpreadSpot.Settings.ColorMode == Animation.ColorModes.Custom);

            RadioButton_SpreadSpot_GlowMode_OuterGlow.CheckedChanged -= RadioButton_SpreadSpot_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_SpreadSpot_GlowMode_InnerGlow.CheckedChanged -= RadioButton_SpreadSpot_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_SpreadSpot_GlowMode_EvenGlow.CheckedChanged -= RadioButton_SpreadSpot_GlowMode_EvenGlow_CheckedChanged;
            switch (Animation.Animations.SpreadSpot.Settings.GlowMode)
            {
                case Animation.GlowModes.OuterGlow: RadioButton_SpreadSpot_GlowMode_OuterGlow.Checked = true; break;
                case Animation.GlowModes.InnerGlow: RadioButton_SpreadSpot_GlowMode_InnerGlow.Checked = true; break;
                case Animation.GlowModes.EvenGlow: RadioButton_SpreadSpot_GlowMode_EvenGlow.Checked = true; break;
            }
            RadioButton_SpreadSpot_GlowMode_OuterGlow.CheckedChanged += RadioButton_SpreadSpot_GlowMode_OuterGlow_CheckedChanged;
            RadioButton_SpreadSpot_GlowMode_InnerGlow.CheckedChanged += RadioButton_SpreadSpot_GlowMode_InnerGlow_CheckedChanged;
            RadioButton_SpreadSpot_GlowMode_EvenGlow.CheckedChanged += RadioButton_SpreadSpot_GlowMode_EvenGlow_CheckedChanged;
        }

        // 调节 TrackBar_SpreadSpot_MinRadius
        private void TrackBar_SpreadSpot_MinRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.MinRadius = TrackBar_SpreadSpot_MinRadius.Value * 0.01;
            // 同步最大值的自动修正
            TrackBar_SpreadSpot_MaxRadius.Value = (int)(Animation.Animations.SpreadSpot.Settings.MaxRadius * 100);
        }

        // 调节 TrackBar_SpreadSpot_MaxRadius
        private void TrackBar_SpreadSpot_MaxRadius_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.MaxRadius = TrackBar_SpreadSpot_MaxRadius.Value * 0.01;
            // 同步最小值的自动修正
            TrackBar_SpreadSpot_MinRadius.Value = (int)(Animation.Animations.SpreadSpot.Settings.MinRadius * 100);
        }

        // 调节 TrackBar_SpreadSpot_Count
        private void TrackBar_SpreadSpot_Count_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.Count = TrackBar_SpreadSpot_Count.Value;
        }

        // 调节 TrackBar_SpreadSpot_SourceX
        private void TrackBar_SpreadSpot_SourceX_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.SourceX = TrackBar_SpreadSpot_SourceX.Value * 0.001;
        }

        // 调节 TrackBar_SpreadSpot_SourceY
        private void TrackBar_SpreadSpot_SourceY_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.SourceY = TrackBar_SpreadSpot_SourceY.Value * 0.001;
        }

        // 调节 TrackBar_SpreadSpot_SourceZ
        private void TrackBar_SpreadSpot_SourceZ_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.SourceZ = TrackBar_SpreadSpot_SourceZ.Value * 0.001;
        }

        // 调节 TrackBar_SpreadSpot_SourceSize
        private void TrackBar_SpreadSpot_SourceSize_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.SourceSize = TrackBar_SpreadSpot_SourceSize.Value * 0.001;
        }

        // 调节 TrackBar_SpreadSpot_Velocity
        private void TrackBar_SpreadSpot_Velocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.Velocity = TrackBar_SpreadSpot_Velocity.Value * 0.01;
        }

        // RadioButton_SpreadSpot_ColorMode_Random 的 Checked 更改
        private void RadioButton_SpreadSpot_ColorMode_Random_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_SpreadSpot_ColorMode_Random.Checked)
            {
                Animation.Animations.SpreadSpot.Settings.ColorMode = Animation.ColorModes.Random;
            }

            Label_SpreadSpot_Color_Val.Enabled = (Animation.Animations.SpreadSpot.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // CheckBox_SpreadSpot_GradientWhenRandom 的 Checked 更改
        private void CheckBox_SpreadSpot_GradientWhenRandom_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.GradientWhenRandom = CheckBox_SpreadSpot_GradientWhenRandom.Checked;
            TrackBar_SpreadSpot_GradientVelocity.Enabled = Animation.Animations.SpreadSpot.Settings.GradientWhenRandom;
        }

        // 调节 TrackBar_SpreadSpot_GradientVelocity
        private void TrackBar_SpreadSpot_GradientVelocity_Scroll(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.GradientVelocity = TrackBar_SpreadSpot_GradientVelocity.Value * 0.1;
        }

        // RadioButton_SpreadSpot_ColorMode_Custom 的 Checked 更改
        private void RadioButton_SpreadSpot_ColorMode_Custom_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_SpreadSpot_ColorMode_Custom.Checked)
            {
                Animation.Animations.SpreadSpot.Settings.ColorMode = Animation.ColorModes.Custom;
            }

            Label_SpreadSpot_Color_Val.Enabled = (Animation.Animations.SpreadSpot.Settings.ColorMode == Animation.ColorModes.Custom);
        }

        // 单击 Label_SpreadSpot_Color_Val
        private void Label_SpreadSpot_Color_Val_Click(object sender, EventArgs e)
        {
            ColorDialog_Color.Color = Animation.Animations.SpreadSpot.Settings.Color;
            if (ColorDialog_Color.ShowDialog() == DialogResult.OK)
            {
                Animation.Animations.SpreadSpot.Settings.Color = ColorDialog_Color.Color;
                Label_SpreadSpot_Color_Val.Text = Com.ColorManipulation.GetColorName(Animation.Animations.SpreadSpot.Settings.Color);
            }
        }

        // RadioButton_SpreadSpot_GlowMode_OuterGlow 的 Checked 更改
        private void RadioButton_SpreadSpot_GlowMode_OuterGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_SpreadSpot_GlowMode_OuterGlow.Checked)
            {
                Animation.Animations.SpreadSpot.Settings.GlowMode = Animation.GlowModes.OuterGlow;
            }
        }

        // RadioButton_SpreadSpot_GlowMode_InnerGlow 的 Checked 更改
        private void RadioButton_SpreadSpot_GlowMode_InnerGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_SpreadSpot_GlowMode_InnerGlow.Checked)
            {
                Animation.Animations.SpreadSpot.Settings.GlowMode = Animation.GlowModes.InnerGlow;
            }
        }

        // RadioButton_SpreadSpot_GlowMode_EvenGlow 的 Checked 更改
        private void RadioButton_SpreadSpot_GlowMode_EvenGlow_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton_SpreadSpot_GlowMode_EvenGlow.Checked)
            {
                Animation.Animations.SpreadSpot.Settings.GlowMode = Animation.GlowModes.EvenGlow;
            }
        }

        // 单击 Label_SpreadSpot_ResetToDefault
        private void Label_SpreadSpot_ResetToDefault_Click(object sender, EventArgs e)
        {
            Animation.Animations.SpreadSpot.Settings.ResetToDefault();

            ResetTab_SpreadSpot();
        }

        #endregion

        #endregion

        #region "通用"区域

        // CheckBox_AntiAlias 的 Checked 更改
        private void CheckBox_AntiAlias_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Settings.AntiAlias = CheckBox_AntiAlias.Checked;
        }

        // CheckBox_LimitFPS 的 Checked 更改
        private void CheckBox_LimitFPS_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Settings.LimitFPS = CheckBox_LimitFPS.Checked;
            TrackBar_FPS.Enabled = Animation.Settings.LimitFPS;
        }

        // 调节 TrackBar_FPS
        private void TrackBar_FPS_Scroll(object sender, EventArgs e)
        {
            Animation.Settings.FPS = TrackBar_FPS.Value;
        }

        // CheckBox_AutoStart 的 Checked 更改
        private void CheckBox_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Settings.AutoStart = CheckBox_AutoStart.Checked;

            CheckBox_AutoStart.CheckedChanged -= CheckBox_AutoStart_CheckedChanged;
            CheckBox_AutoStart.Checked = Animation.Settings.AutoStart;
            CheckBox_AutoStart.CheckedChanged += CheckBox_AutoStart_CheckedChanged;
        }

        // CheckBox_StartMenuShortcut 的 Checked 更改
        private void CheckBox_StartMenuShortcut_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Settings.StartMenuShortcut = CheckBox_StartMenuShortcut.Checked;

            CheckBox_StartMenuShortcut.CheckedChanged -= CheckBox_StartMenuShortcut_CheckedChanged;
            CheckBox_StartMenuShortcut.Checked = Animation.Settings.StartMenuShortcut;
            CheckBox_StartMenuShortcut.CheckedChanged += CheckBox_StartMenuShortcut_CheckedChanged;
        }

        // CheckBox_DesktopShortcut 的 Checked 更改
        private void CheckBox_DesktopShortcut_CheckedChanged(object sender, EventArgs e)
        {
            Animation.Settings.DesktopShortcut = CheckBox_DesktopShortcut.Checked;

            CheckBox_DesktopShortcut.CheckedChanged -= CheckBox_DesktopShortcut_CheckedChanged;
            CheckBox_DesktopShortcut.Checked = Animation.Settings.DesktopShortcut;
            CheckBox_DesktopShortcut.CheckedChanged += CheckBox_DesktopShortcut_CheckedChanged;
        }

        #endregion

        #region "关于"区域

        private static readonly string URL_GitHub_Base = @"https://github.com/chibayuki/Livedesk"; // 此项目在 GitHub 的 URL。
        private static readonly string URL_GitHub_Release = URL_GitHub_Base + @"/releases/latest"; // 此项目的最新发布版本在 GitHub 的 URL。

        // 单击 Label_GitHub_Base。
        private void Label_GitHub_Base_Click(object sender, EventArgs e)
        {
            Process.Start(URL_GitHub_Base);
        }

        // 单击 Label_GitHub_Release。
        private void Label_GitHub_Release_Click(object sender, EventArgs e)
        {
            Process.Start(URL_GitHub_Release);
        }

        #endregion

        #region 底部区域

        // 单击 Label_Close
        private void Label_Close_Click(object sender, EventArgs e)
        {
            // 关闭设置窗体
            this.Close();
        }

        #endregion

    }
}