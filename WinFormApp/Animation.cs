/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
Copyright © 2018 chibayuki@foxmail.com

动态桌面
Version 1.0.1807.0.R1.180710-0000

This file is part of "动态桌面" (Livedesk)

"动态桌面" (Livedesk) is released under the GPLv3 license
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormApp
{
    public static class Animation
    {
        #region 动画类型

        // 动画类型枚举
        public enum Types { NULL = -1, LightSpot, TrianglePiece, Shine, Meteor, Snow, GravityParticle, GravityGrid, SpreadSpot, COUNT }
        // 当前动画类型
        private static Types _Type = Types.NULL;
        public static Types Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;

                Animations.LightSpot.Enabled = (_Type == Types.LightSpot);
                Animations.TrianglePiece.Enabled = (_Type == Types.TrianglePiece);
                Animations.Shine.Enabled = (_Type == Types.Shine);
                Animations.Meteor.Enabled = (_Type == Types.Meteor);
                Animations.Snow.Enabled = (_Type == Types.Snow);
                Animations.GravityParticle.Enabled = (_Type == Types.GravityParticle);
                Animations.GravityGrid.Enabled = (_Type == Types.GravityGrid);
                Animations.SpreadSpot.Enabled = (_Type == Types.SpreadSpot);
            }
        }

        #endregion

        #region 图形

        // 主显示的边界
        public static Rectangle ScreenBounds => Screen.PrimaryScreen.Bounds;
        // 窗体边界
        public static Rectangle FormBounds
        {
            get
            {
                switch (Type)
                {
                    case Types.LightSpot: return Animations.LightSpot.FormBounds;
                    case Types.TrianglePiece: return Animations.TrianglePiece.FormBounds;
                    case Types.Shine: return Animations.Shine.FormBounds;
                    case Types.Meteor: return Animations.Meteor.FormBounds;
                    case Types.Snow: return Animations.Snow.FormBounds;
                    case Types.GravityParticle: return Animations.GravityParticle.FormBounds;
                    case Types.GravityGrid: return Animations.GravityGrid.FormBounds;
                    case Types.SpreadSpot: return Animations.SpreadSpot.FormBounds;
                }
                return ScreenBounds;
            }
        }

        // 位图
        public static Bitmap Bitmap
        {
            get
            {
                switch (Type)
                {
                    case Types.LightSpot: return Animations.LightSpot.Bitmap;
                    case Types.TrianglePiece: return Animations.TrianglePiece.Bitmap;
                    case Types.Shine: return Animations.Shine.Bitmap;
                    case Types.Meteor: return Animations.Meteor.Bitmap;
                    case Types.Snow: return Animations.Snow.Bitmap;
                    case Types.GravityParticle: return Animations.GravityParticle.Bitmap;
                    case Types.GravityGrid: return Animations.GravityGrid.Bitmap;
                    case Types.SpreadSpot: return Animations.SpreadSpot.Bitmap;
                }
                return null;
            }
        }

        #endregion

        #region 动画

        // 颜色模式
        public enum ColorModes { NULL = -1, Random, Custom, COUNT }

        // 着色模式
        public enum GlowModes { NULL = -1, OuterGlow, InnerGlow, EvenGlow, COUNT }

        // 全局设置
        public static class Settings
        {
            // 使用抗锯齿模式绘图
            public static bool AntiAlias = true;

            // 限制重绘帧率
            public static bool LimitFPS = true;
            // 帧率的取值范围
            public static double FPS_MinValue = 12, FPS_MaxValue = 120;
            // 帧率的默认值
            public static double FPS_DefaultValue = 30;
            // 帧率
            private static double _FPS = FPS_DefaultValue;
            public static double FPS
            {
                get
                {
                    return _FPS;
                }
                set
                {
                    if (value <= 0)
                    {
                        LimitFPS = false;
                    }
                    else
                    {
                        _FPS = Math.Max(FPS_MinValue, Math.Min(value, FPS_MaxValue));
                    }
                }
            }

            // 自动启动
            public static bool AutoStart
            {
                get
                {
                    return Configuration.CheckAutoStartShortcut();
                }
                set
                {
                    if (value)
                    {
                        Configuration.CreateAutoStartShortcut();
                    }
                    else
                    {
                        Configuration.DeleteAutoStartShortcut();
                    }
                }
            }

            // 开始菜单快捷方式
            public static bool StartMenuShortcut
            {
                get
                {
                    return Configuration.CheckStartMenuShortcut();
                }
                set
                {
                    if (value)
                    {
                        Configuration.CreateStartMenuShortcut();
                    }
                    else
                    {
                        Configuration.DeleteStartMenuShortcut();
                    }
                }
            }
            // 桌面快捷方式
            public static bool DesktopShortcut
            {
                get
                {
                    return Configuration.CheckDesktopShortcut();
                }
                set
                {
                    if (value)
                    {
                        Configuration.CreateDesktopShortcut();
                    }
                    else
                    {
                        Configuration.DeleteDesktopShortcut();
                    }
                }
            }
        }

        // 动画
        public static class Animations
        {
            #region 圆形光点

            public class LightSpot
            {
                // 列表
                private static List<LightSpot> List = new List<LightSpot>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            LightSpot Element = new LightSpot();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (LightSpot Element in List)
                    {
                        Element.TotalSeconds += DeltaTime.Elapsed.TotalSeconds;
                        Element._UpdateColor();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (LightSpot Element in List)
                            {
                                if (Element.TotalSeconds >= 0)
                                {
                                    Com.PointD Loc = new Com.PointD(Element.CurrentLocation);
                                    double _CR = Element.CurrentRadius;

                                    if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-_CR), (float)(-_CR), (float)(FormBounds.Width + 2 * _CR), (float)(FormBounds.Height + 2 * _CR))))
                                    // 在位图中绘制列表中的所有图形
                                    {
                                        switch (Settings.GlowMode)
                                        {
                                            case GlowModes.OuterGlow:
                                                {
                                                    RectangleF Bounds_Outer_Outer = Element.Bounds;
                                                    RectangleF Bounds_Outer_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.25F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.25F, Bounds_Outer_Outer.Width * 0.5F, Bounds_Outer_Outer.Height * 0.5F);
                                                    RectangleF Bounds_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.2F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.2F, Bounds_Outer_Outer.Width * 0.6F, Bounds_Outer_Outer.Height * 0.6F);

                                                    PathGradientBrush PGB_Outer = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    GraphicsPath Path_Outer = new GraphicsPath();
                                                    Path_Outer.AddEllipse(Bounds_Outer_Inner);
                                                    Path_Outer.AddEllipse(Bounds_Outer_Outer);
                                                    Grap.FillPath(PGB_Outer, Path_Outer);
                                                    PGB_Outer.Dispose();
                                                    Path_Outer.Dispose();

                                                    GraphicsPath Path_Inner = new GraphicsPath();
                                                    Path_Inner.AddEllipse(Bounds_Inner);
                                                    PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                    {
                                                        CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.8F, 0.8F)
                                                    };
                                                    Grap.FillPath(PGB_Inner, Path_Inner);
                                                    Path_Inner.Dispose();
                                                    PGB_Inner.Dispose();
                                                }
                                                break;

                                            case GlowModes.InnerGlow:
                                                {
                                                    PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    Grap.FillPath(PGB, Element.Path);
                                                    PGB.Dispose();
                                                }
                                                break;

                                            case GlowModes.EvenGlow:
                                                {
                                                    SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                    Grap.FillPath(SB, Element.Path);
                                                    SB.Dispose();
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    // 当图形中心离开位图边界时将其重置
                                    {
                                        Element.Reset();
                                    }
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(0, 50, 0, 0);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 半径的取值范围
                    private const double _Radius_MinValue = 1, _Radius_MaxValue = 64;
                    // 最小半径的取值范围与默认值
                    public const double MinRadius_MinValue = _Radius_MinValue, MinRadius_MaxValue = _Radius_MaxValue, MinRadius_DefaultValue = 2;
                    // 最小半径（像素）
                    private static double _MinRadius = MinRadius_DefaultValue;
                    public static double MinRadius
                    {
                        get
                        {
                            return _MinRadius;
                        }
                        set
                        {
                            _MinRadius = Math.Max(MinRadius_MinValue, Math.Min(value, MinRadius_MaxValue));

                            if (_MaxRadius < _MinRadius)
                            {
                                _MaxRadius = _MinRadius;
                            }
                        }
                    }
                    // 最大半径的取值范围与默认值
                    public const double MaxRadius_MinValue = _Radius_MinValue, MaxRadius_MaxValue = _Radius_MaxValue, MaxRadius_DefaultValue = 12;
                    // 最大半径（像素）
                    private static double _MaxRadius = MaxRadius_DefaultValue;
                    public static double MaxRadius
                    {
                        get
                        {
                            return _MaxRadius;
                        }
                        set
                        {
                            _MaxRadius = Math.Max(MaxRadius_MinValue, Math.Min(value, MaxRadius_MaxValue));

                            if (_MinRadius > _MaxRadius)
                            {
                                _MinRadius = _MaxRadius;
                            }
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 8192, Count_DefaultValue = 300;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled)
                            {
                                _SetListCount(_Count);
                            }
                        }
                    }

                    // 振幅与半径的比值的取值范围与默认值
                    public const double Amplitude_MinValue = 0.5, Amplitude_MaxValue = 64, Amplitude_DefaultValue = 1.5;
                    // 振幅与半径的比值
                    private static double _Amplitude = Amplitude_DefaultValue;
                    public static double Amplitude
                    {
                        get
                        {
                            return _Amplitude;
                        }
                        set
                        {
                            _Amplitude = Math.Max(Amplitude_MinValue, Math.Min(value, Amplitude_MaxValue));
                        }
                    }

                    // 波长与半径的比值的取值范围与默认值
                    public const double WaveLength_MinValue = 0.5, WaveLength_MaxValue = 512, WaveLength_DefaultValue = 18;
                    // 波长与半径的比值
                    private static double _WaveLength = WaveLength_DefaultValue;
                    public static double WaveLength
                    {
                        get
                        {
                            return _WaveLength;
                        }
                        set
                        {
                            _WaveLength = Math.Max(WaveLength_MinValue, Math.Min(value, WaveLength_MaxValue));
                        }
                    }

                    // 波速与半径的比值的取值范围与默认值
                    public const double WaveVelocity_MinValue = 0.5, WaveVelocity_MaxValue = 64, WaveVelocity_DefaultValue = 1.25;
                    // 波速与半径的比值
                    private static double _WaveVelocity = WaveVelocity_DefaultValue;
                    public static double WaveVelocity
                    {
                        get
                        {
                            return _WaveVelocity;
                        }
                        set
                        {
                            _WaveVelocity = Math.Max(WaveVelocity_MinValue, Math.Min(value, WaveVelocity_MaxValue));
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinRadius = MinRadius_DefaultValue;
                        MaxRadius = MaxRadius_DefaultValue;
                        Count = Count_DefaultValue;
                        Amplitude = Amplitude_DefaultValue;
                        WaveLength = WaveLength_DefaultValue;
                        WaveVelocity = WaveVelocity_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                    }
                }

                // 初始化到当前时刻的总秒数
                private double TotalSeconds;

                // 当前颜色的 Alpha 通道
                private int _Alpha => Math.Max(0, Math.Min((int)(255 * CurrentLocation.Y / FormBounds.Height), 255));
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (CurrentRadius - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 此前所有颜色持续时间的总秒数
                private double _TotalSecondsOfOldColors;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = (TotalSeconds - _TotalSecondsOfOldColors) * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfOldColors = TotalSeconds;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 初始半径参数
                private double _InitialRadius;
                // 初始半径（像素）
                private double InitialRadius => (1 - _InitialRadius) * Settings.MinRadius + _InitialRadius * Settings.MaxRadius;
                // 当前半径（像素）
                private double CurrentRadius
                {
                    get
                    {
                        double _IR = InitialRadius;
                        return Math.Max(1, Math.Min(_IR * CurrentLocation.Y / FormBounds.Height, _IR));
                    }
                }

                // 振幅（像素）
                private double Amplitude => Settings.Amplitude * InitialRadius;
                // 波长（像素）
                private double WaveLength => Settings.WaveLength * InitialRadius;
                // 波速（像素/秒）
                private double WaveVelocity => Settings.WaveVelocity * InitialRadius;
                // 周期（秒）
                private double Period => WaveLength / WaveVelocity;
                // 角速度（弧度/秒）
                private double AngularVelocity => 2 * Math.PI / Period;
                // 初始相位参数
                private double _InitialPhase;
                // 初始相位（弧度）
                private double InitialPhase => _InitialPhase * (2 * Math.PI);
                // 当前相位（弧度）
                private double CurrentPhase => Com.Geometry.AngleMapping(AngularVelocity * TotalSeconds + InitialPhase);
                // 初始中心位置参数
                private Com.PointD _InitialLocation;
                // 初始中心位置（像素，像素）
                private PointF InitialLocation
                {
                    get
                    {
                        Com.PointD __IL = _InitialLocation;
                        return new PointF((float)((1 - __IL.X) * (-(InitialRadius - Amplitude)) + __IL.X * (FormBounds.Width + (InitialRadius - Amplitude))), (float)((1 - __IL.Y) * (-InitialRadius) + __IL.Y * (FormBounds.Height + InitialRadius)));
                    }
                }
                // 当前中心位置（像素，像素）
                private PointF CurrentLocation
                {
                    get
                    {
                        PointF _IL = InitialLocation;
                        return new PointF((float)(_IL.X + Amplitude * Math.Cos(CurrentPhase)), (float)(_IL.Y - WaveVelocity * TotalSeconds));
                    }
                }

                // 边界
                private RectangleF Bounds
                {
                    get
                    {
                        PointF _CL = CurrentLocation;
                        double _CR = CurrentRadius;
                        return new RectangleF((float)(_CL.X - _CR), (float)(_CL.Y - _CR), (float)(2 * _CR), (float)(2 * _CR));
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddEllipse(Bounds);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    TotalSeconds = 0;
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfOldColors = 0;
                    _InitialRadius = Rand.NextDouble();
                    _InitialPhase = Rand.NextDouble();
                    _InitialLocation = new Com.PointD(Rand.NextDouble(), 1);
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                    // 使同时产生的大量图形能够更均匀的先后出现在位图中，而不是同时出现
                    TotalSeconds -= (Rand.NextDouble() * (FormBounds.Height / WaveVelocity));
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 三角形碎片

            public class TrianglePiece
            {
                // 列表
                private static List<TrianglePiece> List = new List<TrianglePiece>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            TrianglePiece Element = new TrianglePiece();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (TrianglePiece Element in List)
                    {
                        Element.TotalSeconds += DeltaTime.Elapsed.TotalSeconds;
                        Element._UpdateColor();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (TrianglePiece Element in List)
                            {
                                if (Element.TotalSeconds >= 0)
                                {
                                    Com.PointD Loc = new Com.PointD(Element.CurrentLocation);
                                    double _CR = Element.CurrentRadius;

                                    if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-_CR), (float)(-_CR), (float)(FormBounds.Width + 2 * _CR), (float)(FormBounds.Height + 2 * _CR))))
                                    // 在位图中绘制列表中的所有图形
                                    {
                                        switch (Settings.GlowMode)
                                        {
                                            case GlowModes.OuterGlow:
                                                {
                                                    PointF[] MotionVertex_Outer_Outer = Element.MotionVertex;
                                                    PointF[] MotionVertex_Outer_Inner = new PointF[3];
                                                    PointF[] MotionVertex_Inner = new PointF[3];
                                                    Com.PointD[] SideMid = new Com.PointD[3] { (new Com.PointD(MotionVertex_Outer_Outer[1]) + new Com.PointD(MotionVertex_Outer_Outer[2])) / 2, (new Com.PointD(MotionVertex_Outer_Outer[2]) + new Com.PointD(MotionVertex_Outer_Outer[0])) / 2, (new Com.PointD(MotionVertex_Outer_Outer[0]) + new Com.PointD(MotionVertex_Outer_Outer[1])) / 2 };

                                                    for (int i = 0; i < 3; i++)
                                                    {
                                                        MotionVertex_Outer_Inner[i] = (new Com.PointD(MotionVertex_Outer_Outer[i]) * 0.7 + SideMid[i] * 0.3).ToPointF();
                                                        MotionVertex_Inner[i] = (new Com.PointD(MotionVertex_Outer_Outer[i]) * 0.8 + SideMid[i] * 0.2).ToPointF();
                                                    }

                                                    PathGradientBrush PGB_Outer = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.7F, 0.7F)
                                                    };
                                                    GraphicsPath Path_Outer = new GraphicsPath();
                                                    Path_Outer.AddPolygon(MotionVertex_Outer_Inner);
                                                    Path_Outer.AddPolygon(MotionVertex_Outer_Outer);
                                                    Grap.FillPath(PGB_Outer, Path_Outer);
                                                    PGB_Outer.Dispose();
                                                    Path_Outer.Dispose();

                                                    GraphicsPath Path_Inner = new GraphicsPath();
                                                    Path_Inner.AddPolygon(MotionVertex_Inner);
                                                    PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                    {
                                                        CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.875F, 0.875F)
                                                    };
                                                    Grap.FillPath(PGB_Inner, Path_Inner);
                                                    Path_Inner.Dispose();
                                                    PGB_Inner.Dispose();
                                                }
                                                break;

                                            case GlowModes.InnerGlow:
                                                {
                                                    PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.7F, 0.7F)
                                                    };
                                                    Grap.FillPath(PGB, Element.Path);
                                                    PGB.Dispose();
                                                }
                                                break;

                                            case GlowModes.EvenGlow:
                                                {
                                                    SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                    Grap.FillPath(SB, Element.Path);
                                                    SB.Dispose();
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    // 当图形中心离开位图边界时将其重置
                                    {
                                        Element.Reset();
                                    }
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(0, 50, 0, 0);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 外接圆半径的取值范围
                    private const double _Radius_MinValue = 2, _Radius_MaxValue = 128;
                    // 最小外接圆半径的取值范围与默认值
                    public const double MinRadius_MinValue = _Radius_MinValue, MinRadius_MaxValue = _Radius_MaxValue, MinRadius_DefaultValue = 4;
                    // 最小外接圆半径（像素）
                    private static double _MinRadius = MinRadius_DefaultValue;
                    public static double MinRadius
                    {
                        get
                        {
                            return _MinRadius;
                        }
                        set
                        {
                            _MinRadius = Math.Max(MinRadius_MinValue, Math.Min(value, MinRadius_MaxValue));

                            if (_MaxRadius < _MinRadius) _MaxRadius = _MinRadius;
                        }
                    }
                    // 最大外接圆半径的取值范围与默认值
                    public const double MaxRadius_MinValue = _Radius_MinValue, MaxRadius_MaxValue = _Radius_MaxValue, MaxRadius_DefaultValue = 24;
                    // 最大外接圆半径（像素）
                    private static double _MaxRadius = MaxRadius_DefaultValue;
                    public static double MaxRadius
                    {
                        get
                        {
                            return _MaxRadius;
                        }
                        set
                        {
                            _MaxRadius = Math.Max(MaxRadius_MinValue, Math.Min(value, MaxRadius_MaxValue));

                            if (_MinRadius > _MaxRadius) _MinRadius = _MaxRadius;
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 4096, Count_DefaultValue = 200;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled) _SetListCount(_Count);
                        }
                    }

                    // 振幅与半径的比值的取值范围与默认值
                    public const double Amplitude_MinValue = 0.5, Amplitude_MaxValue = 64, Amplitude_DefaultValue = 1;
                    // 振幅与半径的比值
                    private static double _Amplitude = Amplitude_DefaultValue;
                    public static double Amplitude
                    {
                        get
                        {
                            return _Amplitude;
                        }
                        set
                        {
                            _Amplitude = Math.Max(Amplitude_MinValue, Math.Min(value, Amplitude_MaxValue));
                        }
                    }

                    // 波长与半径的比值的取值范围与默认值
                    public const double WaveLength_MinValue = 0.5, WaveLength_MaxValue = 512, WaveLength_DefaultValue = 12;
                    // 波长与半径的比值
                    private static double _WaveLength = WaveLength_DefaultValue;
                    public static double WaveLength
                    {
                        get
                        {
                            return _WaveLength;
                        }
                        set
                        {
                            _WaveLength = Math.Max(WaveLength_MinValue, Math.Min(value, WaveLength_MaxValue));
                        }
                    }

                    // 波速与半径的比值的取值范围与默认值
                    public const double WaveVelocity_MinValue = 0.5, WaveVelocity_MaxValue = 64, WaveVelocity_DefaultValue = 0.8;
                    // 波速与半径的比值
                    private static double _WaveVelocity = WaveVelocity_DefaultValue;
                    public static double WaveVelocity
                    {
                        get
                        {
                            return _WaveVelocity;
                        }
                        set
                        {
                            _WaveVelocity = Math.Max(WaveVelocity_MinValue, Math.Min(value, WaveVelocity_MaxValue));
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinRadius = MinRadius_DefaultValue;
                        MaxRadius = MaxRadius_DefaultValue;
                        Count = Count_DefaultValue;
                        Amplitude = Amplitude_DefaultValue;
                        WaveLength = WaveLength_DefaultValue;
                        WaveVelocity = WaveVelocity_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                    }
                }

                // 初始化到当前时刻的总秒数
                private double TotalSeconds;

                // 当前颜色的 Alpha 通道
                private int _Alpha => Math.Max(0, Math.Min((int)(255 * CurrentLocation.Y / FormBounds.Height), 255));
                // 当前动多边形外接圆半径（像素）
                private double _CCR
                {
                    get
                    {
                        double a = Com.PointD.DistanceBetween(new Com.PointD(MotionVertex[0]), new Com.PointD(MotionVertex[1])), b = Com.PointD.DistanceBetween(new Com.PointD(MotionVertex[1]), new Com.PointD(MotionVertex[2])), c = Com.PointD.DistanceBetween(new Com.PointD(MotionVertex[2]), new Com.PointD(MotionVertex[0]));
                        // 三角形外接圆半径公式
                        return (a * b * c) / Math.Sqrt((a + b + c) * (a + b - c) * (a - b + c) * (-a + b + c));
                    }
                }
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (_CCR - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 此前所有颜色持续时间的总秒数
                private double _TotalSecondsOfOldColors;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = (TotalSeconds - _TotalSecondsOfOldColors) * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfOldColors = TotalSeconds;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 初始定多边形外接圆半径参数
                private double _InitialRadius;
                // 初始定多边形外接圆半径（像素）
                private double InitialRadius => (1 - _InitialRadius) * Settings.MinRadius + _InitialRadius * Settings.MaxRadius;
                // 当前定多边形外接圆半径（像素）
                private double CurrentRadius
                {
                    get
                    {
                        double _IR = InitialRadius;
                        return Math.Max(1, Math.Min(_IR * CurrentLocation.Y / FormBounds.Height, _IR));
                    }
                }

                // 振幅（像素）
                private double Amplitude => Settings.Amplitude * InitialRadius;
                // 波长（像素）
                private double WaveLength => Settings.WaveLength * InitialRadius;
                // 波速（像素/秒）
                private double WaveVelocity => Settings.WaveVelocity * InitialRadius;
                // 周期（秒）
                private double Period => WaveLength / WaveVelocity;
                // 角速度（弧度/秒）
                private double AngularVelocity => 2 * Math.PI / Period;
                // 初始相位参数
                private double _InitialPhase;
                // 初始相位（弧度）
                private double InitialPhase => _InitialPhase * (2 * Math.PI);
                // 当前相位（弧度）
                private double CurrentPhase => Com.Geometry.AngleMapping(AngularVelocity * TotalSeconds + InitialPhase);
                // 初始定多边形外接圆中心位置参数
                private Com.PointD _InitialLocation;
                // 初始定多边形外接圆中心位置（像素，像素）
                private PointF InitialLocation
                {
                    get
                    {
                        Com.PointD __IL = _InitialLocation;
                        return new PointF((float)((1 - __IL.X) * (-(InitialRadius - Amplitude)) + __IL.X * (FormBounds.Width + (InitialRadius - Amplitude))), (float)((1 - __IL.Y) * (-InitialRadius) + __IL.Y * (FormBounds.Height + InitialRadius)));
                    }
                }
                // 当前定多边形外接圆中心位置（像素，像素）
                private PointF CurrentLocation
                {
                    get
                    {
                        PointF _IL = InitialLocation;
                        return new PointF((float)(_IL.X + Amplitude * Math.Cos(CurrentPhase)), (float)(_IL.Y - WaveVelocity * TotalSeconds));
                    }
                }

                // 定多边形顶点在外接圆上的相位（弧度）
                private double[] StaticVertexPhase;
                // 定多边形顶点（像素，像素）
                private PointF[] StaticVertex
                {
                    get
                    {
                        PointF[] PF = new PointF[3];
                        PointF _CL = CurrentLocation;
                        double _CR = CurrentRadius;

                        for (int i = 0; i < 3; i++)
                        {
                            PF[i] = new PointF((float)(_CL.X + _CR * Math.Cos(StaticVertexPhase[i])), (float)(_CL.Y + _CR * Math.Sin(StaticVertexPhase[i])));
                        }

                        return PF;
                    }
                }
                // 动多边形顶点（像素，像素）
                private PointF[] MotionVertex
                {
                    get
                    {
                        PointF[] PF = new PointF[3];
                        PointF[] PF_From = new PointF[3] { StaticVertex[0], StaticVertex[1], StaticVertex[2] }, PF_To = new PointF[3] { StaticVertex[1], StaticVertex[2], StaticVertex[0] };
                        double PolygonPhase = Com.Geometry.AngleMapping(CurrentPhase * 3);

                        for (int i = 0; i < 3; i++)
                        {
                            PF[i] = new PointF((float)(PF_From[i].X + (PF_To[i].X - PF_From[i].X) * PolygonPhase / (2 * Math.PI)), (float)(PF_From[i].Y + (PF_To[i].Y - PF_From[i].Y) * PolygonPhase / (2 * Math.PI)));
                        }

                        return PF;
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddPolygon(MotionVertex);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    TotalSeconds = 0;
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfOldColors = 0;
                    _InitialRadius = Rand.NextDouble();
                    _InitialPhase = Rand.NextDouble();
                    _InitialLocation = new Com.PointD(Rand.NextDouble(), 1);
                    // 防止多边形锐角过小
                    do
                    {
                        StaticVertexPhase = new double[3]
                        {
Rand.NextDouble() * 2 * Math.PI, Rand.NextDouble() * 2 * Math.PI, Rand.NextDouble() * 2 * Math.PI };
                    }
                    while (Com.PointD.DistanceBetween(new Com.PointD(StaticVertex[0]), new Com.PointD(StaticVertex[1])) < InitialRadius || Com.PointD.DistanceBetween(new Com.PointD(StaticVertex[1]), new Com.PointD(StaticVertex[2])) < InitialRadius || Com.PointD.DistanceBetween(new Com.PointD(StaticVertex[2]), new Com.PointD(StaticVertex[0])) < InitialRadius);
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                    // 使同时产生的大量图形能够更均匀的先后出现在位图中，而不是同时出现
                    TotalSeconds -= (Rand.NextDouble() * (FormBounds.Height / WaveVelocity));
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 光芒

            public class Shine
            {
                // 列表
                private static List<Shine> List = new List<Shine>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            Shine Element = new Shine();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (Shine Element in List)
                    {
                        Element.TotalSeconds += DeltaTime.Elapsed.TotalSeconds;
                        Element._UpdateColor();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (Shine Element in List)
                            {
                                if (Element.TotalSeconds >= 0)
                                {
                                    if (Element.TotalSeconds >= Element.Period)
                                    // 当图形持续时长达到周期时将其重置
                                    {
                                        Element.Reset();
                                    }
                                    else
                                    {
                                        Com.PointD Loc = new Com.PointD(Element.CurrentLocation);
                                        double _CR = Element.CurrentRadius;

                                        if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-_CR), (float)(-_CR), (float)(FormBounds.Width + 2 * _CR), (float)(FormBounds.Height + 2 * _CR))))
                                        // 在位图中绘制列表中的所有图形
                                        {
                                            switch (Settings.GlowMode)
                                            {
                                                case GlowModes.OuterGlow:
                                                    {
                                                        RectangleF Bounds_Outer_Outer = Element.Bounds;
                                                        RectangleF Bounds_Outer_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.25F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.25F, Bounds_Outer_Outer.Width * 0.5F, Bounds_Outer_Outer.Height * 0.5F);
                                                        RectangleF Bounds_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.2F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.2F, Bounds_Outer_Outer.Width * 0.6F, Bounds_Outer_Outer.Height * 0.6F);

                                                        PathGradientBrush PGB_Outer = new PathGradientBrush(Element.Path)
                                                        {
                                                            CenterColor = Element.CurrentColor,
                                                            SurroundColors = new Color[] { Color.Transparent },
                                                            FocusScales = new PointF(0.5F, 0.5F)
                                                        };
                                                        GraphicsPath Path_Outer = new GraphicsPath();
                                                        Path_Outer.AddEllipse(Bounds_Outer_Inner);
                                                        Path_Outer.AddEllipse(Bounds_Outer_Outer);
                                                        Grap.FillPath(PGB_Outer, Path_Outer);
                                                        PGB_Outer.Dispose();
                                                        Path_Outer.Dispose();

                                                        GraphicsPath Path_Inner = new GraphicsPath();
                                                        Path_Inner.AddEllipse(Bounds_Inner);
                                                        PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                        {
                                                            CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                            SurroundColors = new Color[] { Color.Transparent },
                                                            FocusScales = new PointF(0.8F, 0.8F)
                                                        };
                                                        Grap.FillPath(PGB_Inner, Path_Inner);
                                                        Path_Inner.Dispose();
                                                        PGB_Inner.Dispose();
                                                    }
                                                    break;

                                                case GlowModes.InnerGlow:
                                                    {
                                                        PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                        {
                                                            CenterColor = Element.CurrentColor,
                                                            SurroundColors = new Color[] { Color.Transparent },
                                                            FocusScales = new PointF(0.5F, 0.5F)
                                                        };
                                                        Grap.FillPath(PGB, Element.Path);
                                                        PGB.Dispose();
                                                    }
                                                    break;

                                                case GlowModes.EvenGlow:
                                                    {
                                                        SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                        Grap.FillPath(SB, Element.Path);
                                                        SB.Dispose();
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        // 当图形中心离开位图边界时将其重置
                                        {
                                            Element.Reset();
                                        }
                                    }
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(15, 15, 15, 15);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 半径的取值范围
                    private const double _Radius_MinValue = 2, _Radius_MaxValue = 128;
                    // 最小半径的取值范围与默认值
                    public const double MinRadius_MinValue = _Radius_MinValue, MinRadius_MaxValue = _Radius_MaxValue, MinRadius_DefaultValue = 4;
                    // 最小半径（像素）
                    private static double _MinRadius = MinRadius_DefaultValue;
                    public static double MinRadius
                    {
                        get
                        {
                            return _MinRadius;
                        }
                        set
                        {
                            _MinRadius = Math.Max(MinRadius_MinValue, Math.Min(value, MinRadius_MaxValue));

                            if (_MaxRadius < _MinRadius) _MaxRadius = _MinRadius;
                        }
                    }
                    // 最大半径的取值范围与默认值
                    public const double MaxRadius_MinValue = _Radius_MinValue, MaxRadius_MaxValue = _Radius_MaxValue, MaxRadius_DefaultValue = 24;
                    // 最大半径（像素）
                    private static double _MaxRadius = MaxRadius_DefaultValue;
                    public static double MaxRadius
                    {
                        get
                        {
                            return _MaxRadius;
                        }
                        set
                        {
                            _MaxRadius = Math.Max(MaxRadius_MinValue, Math.Min(value, MaxRadius_MaxValue));

                            if (_MinRadius > _MaxRadius) _MinRadius = _MaxRadius;
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 4096, Count_DefaultValue = 200;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled) _SetListCount(_Count);
                        }
                    }

                    // 位移大小的取值范围与默认值
                    public const double Displacement_MinValue = 0.5, Displacement_MaxValue = 32, Displacement_DefaultValue = 2;
                    // 位移大小
                    private static double _Displacement = Displacement_DefaultValue;
                    public static double Displacement
                    {
                        get
                        {
                            return _Displacement;
                        }
                        set
                        {
                            _Displacement = Math.Max(Displacement_MinValue, Math.Min(value, Displacement_MaxValue));
                        }
                    }

                    // 周期的取值范围
                    private const double _Period_MinValue = 0.5, _Period_MaxValue = 32;
                    // 最小周期的取值范围与默认值
                    public const double MinPeriod_MinValue = _Period_MinValue, MinPeriod_MaxValue = _Period_MaxValue, MinPeriod_DefaultValue = 2;
                    // 最小周期（像素）
                    private static double _MinPeriod = MinPeriod_DefaultValue;
                    public static double MinPeriod
                    {
                        get
                        {
                            return _MinPeriod;
                        }
                        set
                        {
                            _MinPeriod = Math.Max(MinPeriod_MinValue, Math.Min(value, MinPeriod_MaxValue));

                            if (_MaxPeriod < _MinPeriod) _MaxPeriod = _MinPeriod;
                        }
                    }
                    // 最大周期的取值范围与默认值
                    public const double MaxPeriod_MinValue = _Period_MinValue, MaxPeriod_MaxValue = _Period_MaxValue, MaxPeriod_DefaultValue = 5;
                    // 最大周期（像素）
                    private static double _MaxPeriod = MaxPeriod_DefaultValue;
                    public static double MaxPeriod
                    {
                        get
                        {
                            return _MaxPeriod;
                        }
                        set
                        {
                            _MaxPeriod = Math.Max(MaxPeriod_MinValue, Math.Min(value, MaxPeriod_MaxValue));

                            if (_MinPeriod > _MaxPeriod) _MinPeriod = _MaxPeriod;
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinRadius = MinRadius_DefaultValue;
                        MaxRadius = MaxRadius_DefaultValue;
                        Count = Count_DefaultValue;
                        Displacement = Displacement_DefaultValue;
                        MinPeriod = MinPeriod_DefaultValue;
                        MaxPeriod = MaxPeriod_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                    }
                }

                // 初始化到当前时刻的总秒数
                private double TotalSeconds;

                // 当前颜色的 Alpha 通道
                private int _Alpha
                {
                    get
                    {
                        double _R = 0;
                        double _W = FormBounds.Width / 2, _H = FormBounds.Height / 2;
                        PointF _CL = CurrentLocation;
                        double _X = Math.Abs(_CL.X - _W), _Y = Math.Abs(_CL.Y - _H);

                        if (_X * _H == _Y * _W)
                        {
                            _R = Math.Sqrt(_X * _X + _Y * _Y);
                        }
                        else if (_X * _H > _Y * _W)
                        {
                            _R = _X * Math.Sqrt(1 + (_H * _H) / (_W * _W));
                        }
                        else
                        {
                            _R = _Y * Math.Sqrt(1 + (_W * _W) / (_H * _H));
                        }

                        return Math.Max(0, Math.Min((int)(255 * (1 - _R / Math.Sqrt(_W * _W + _H * _H)) * (1 - Math.Abs(2 * TotalSeconds / Period - 1))), 255));
                    }
                }
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (CurrentRadius - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 此前所有颜色持续时间的总秒数
                private double _TotalSecondsOfOldColors;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = (TotalSeconds - _TotalSecondsOfOldColors) * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfOldColors = TotalSeconds;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 初始半径参数
                private double _InitialRadius;
                // 初始半径（像素）
                private double InitialRadius => (1 - _InitialRadius) * Settings.MinRadius + _InitialRadius * Settings.MaxRadius;
                // 当前半径（像素）
                private double CurrentRadius
                {
                    get
                    {
                        double _IR = InitialRadius;
                        return Math.Max(1, Math.Min(_IR * (1 - Math.Abs(2 * TotalSeconds / Period - 1) / 2), _IR));
                    }
                }

                // 周期参数
                private double _Period;
                // 周期（秒）
                private double Period => (1 - _Period) * Settings.MinPeriod + _Period * Settings.MaxPeriod;
                // 速度大小（像素/秒）
                private double Velocity_Abs => Settings.Displacement * InitialRadius / Period;
                // 速度方向参数
                private double _Velocity_Angle;
                // 速度方向（弧度）
                private double Velocity_Angle => _Velocity_Angle * (2 * Math.PI);
                // 速度（像素/秒，像素/秒）
                private Com.PointD Velocity => Velocity_Abs * new Com.PointD(Math.Cos(Velocity_Angle), Math.Sin(Velocity_Angle));
                // 初始中心位置参数
                private Com.PointD _InitialLocation;
                // 初始中心位置（像素，像素）
                private PointF InitialLocation
                {
                    get
                    {
                        Com.PointD __IL = _InitialLocation;
                        return new PointF((float)((1 - __IL.X) * (-InitialRadius) + __IL.X * (FormBounds.Width + InitialRadius)), (float)((1 - __IL.Y) * (-InitialRadius) + __IL.Y * (FormBounds.Height + InitialRadius)));
                    }
                }
                // 当前中心位置（像素，像素）
                private PointF CurrentLocation
                {
                    get
                    {
                        PointF _IL = InitialLocation;
                        return new PointF((float)(_IL.X + Velocity.X * TotalSeconds), (float)(_IL.Y + Velocity.Y * TotalSeconds));
                    }
                }

                // 边界
                private RectangleF Bounds
                {
                    get
                    {
                        PointF _CL = CurrentLocation;
                        double _CR = CurrentRadius;
                        return new RectangleF((float)(_CL.X - _CR), (float)(_CL.Y - _CR), (float)(2 * _CR), (float)(2 * _CR));
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddEllipse(Bounds);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    TotalSeconds = 0;
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfOldColors = 0;
                    _InitialRadius = Rand.NextDouble();
                    _Period = Rand.NextDouble();
                    _Velocity_Angle = Rand.NextDouble();
                    _InitialLocation = new Com.PointD(Rand.NextDouble(), Rand.NextDouble());
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                    // 使同时产生的大量图形能够更均匀的先后出现在位图中，而不是同时出现
                    TotalSeconds -= (Rand.NextDouble() * Period);
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 流星

            public class Meteor
            {
                // 列表
                private static List<Meteor> List = new List<Meteor>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            Meteor Element = new Meteor();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (Meteor Element in List)
                    {
                        Element.TotalSeconds += DeltaTime.Elapsed.TotalSeconds;
                        Element._UpdateColor();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (Meteor Element in List)
                            {
                                if (Element.TotalSeconds >= 0)
                                {
                                    PointF _CL = Element.CurrentLocation;
                                    Com.PointD Loc = new Com.PointD(_CL);
                                    double _CR = Element.CurrentRadius;
                                    double _CLength = Element.CurrentLength;

                                    if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-(_CLength + _CR)), (float)(-(_CLength + _CR)), (float)(FormBounds.Width + 2 * (_CLength + _CR)), (float)(FormBounds.Height + 2 * (_CLength + _CR)))))
                                    // 在位图中绘制列表中的所有图形
                                    {
                                        // 绘制粒子
                                        switch (Settings.GlowMode)
                                        {
                                            case GlowModes.OuterGlow:
                                                {
                                                    double Angle = Element.CurrentVelocity.VectorAngle;

                                                    GraphicsPath Path_Outer_Outer = Element.Path;
                                                    GraphicsPath Path_Outer_Inner = new GraphicsPath();
                                                    GraphicsPath Path_Inner = new GraphicsPath();
                                                    Path_Outer_Inner.AddArc(new RectangleF((float)(_CL.X - (_CR * 0.5)), (float)(_CL.Y - (_CR * 0.5)), (float)(2 * (_CR * 0.5)), (float)(2 * (_CR * 0.5))), (float)(Angle / Math.PI * 180 - 90), 180F);
                                                    Path_Outer_Inner.AddLines(new PointF[] { (Loc + (_CR * 0.5) * new Com.PointD(Math.Cos(Angle + Math.PI / 2), Math.Sin(Angle + Math.PI / 2))).ToPointF(), (Loc + (_CLength * 0.5) * new Com.PointD(Math.Cos(Angle + Math.PI), Math.Sin(Angle + Math.PI))).ToPointF(), (Loc + (_CR * 0.5) * new Com.PointD(Math.Cos(Angle - Math.PI / 2), Math.Sin(Angle - Math.PI / 2))).ToPointF() });
                                                    Path_Outer_Inner.CloseFigure();
                                                    Path_Inner.AddArc(new RectangleF((float)(_CL.X - (_CR * 0.6)), (float)(_CL.Y - (_CR * 0.6)), (float)(2 * (_CR * 0.6)), (float)(2 * (_CR * 0.6))), (float)(Angle / Math.PI * 180 - 90), 180F);
                                                    Path_Inner.AddLines(new PointF[] { (Loc + (_CR * 0.6) * new Com.PointD(Math.Cos(Angle + Math.PI / 2), Math.Sin(Angle + Math.PI / 2))).ToPointF(), (Loc + (_CLength * 0.6) * new Com.PointD(Math.Cos(Angle + Math.PI), Math.Sin(Angle + Math.PI))).ToPointF(), (Loc + (_CR * 0.6) * new Com.PointD(Math.Cos(Angle - Math.PI / 2), Math.Sin(Angle - Math.PI / 2))).ToPointF() });
                                                    Path_Inner.CloseFigure();

                                                    PathGradientBrush PGB_Outer = new PathGradientBrush(Path_Outer_Outer)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    GraphicsPath Path_Outer = new GraphicsPath();
                                                    Path_Outer.AddPath(Path_Outer_Inner, false);
                                                    Path_Outer.AddPath(Path_Outer_Outer, false);
                                                    Grap.FillPath(PGB_Outer, Path_Outer);
                                                    PGB_Outer.Dispose();
                                                    Path_Outer.Dispose();

                                                    PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                    {
                                                        CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.8F, 0.8F)
                                                    };
                                                    Grap.FillPath(PGB_Inner, Path_Inner);
                                                    Path_Inner.Dispose();
                                                    PGB_Inner.Dispose();
                                                }
                                                break;

                                            case GlowModes.InnerGlow:
                                                {
                                                    PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    Grap.FillPath(PGB, Element.Path);
                                                    PGB.Dispose();
                                                }
                                                break;

                                            case GlowModes.EvenGlow:
                                                {
                                                    SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                    Grap.FillPath(SB, Element.Path);
                                                    SB.Dispose();
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    // 当图形中心离开位图边界时将其重置
                                    {
                                        Element.Reset();
                                    }
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(0, 0, 0, 50);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 半径的取值范围
                    private const double _Radius_MinValue = 0.5, _Radius_MaxValue = 32;
                    // 最小半径的取值范围与默认值
                    public const double MinRadius_MinValue = _Radius_MinValue, MinRadius_MaxValue = _Radius_MaxValue, MinRadius_DefaultValue = 1;
                    // 最小半径（像素）
                    private static double _MinRadius = MinRadius_DefaultValue;
                    public static double MinRadius
                    {
                        get
                        {
                            return _MinRadius;
                        }
                        set
                        {
                            _MinRadius = Math.Max(MinRadius_MinValue, Math.Min(value, MinRadius_MaxValue));

                            if (_MaxRadius < _MinRadius) _MaxRadius = _MinRadius;
                        }
                    }
                    // 最大半径的取值范围与默认值
                    public const double MaxRadius_MinValue = _Radius_MinValue, MaxRadius_MaxValue = _Radius_MaxValue, MaxRadius_DefaultValue = 4;
                    // 最大半径（像素）
                    private static double _MaxRadius = MaxRadius_DefaultValue;
                    public static double MaxRadius
                    {
                        get
                        {
                            return _MaxRadius;
                        }
                        set
                        {
                            _MaxRadius = Math.Max(MaxRadius_MinValue, Math.Min(value, MaxRadius_MaxValue));

                            if (_MinRadius > _MaxRadius) _MinRadius = _MaxRadius;
                        }
                    }

                    // 长度与半径的比值的取值范围与默认值
                    public const double Length_MinValue = 2, Length_MaxValue = 128, Length_DefaultValue = 32;
                    // 长度与半径的比值
                    private static double _Length = Length_DefaultValue;
                    public static double Length
                    {
                        get
                        {
                            return _Length;
                        }
                        set
                        {
                            _Length = Math.Max(Length_MinValue, Math.Min(value, Length_MaxValue));
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 4096, Count_DefaultValue = 100;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled) _SetListCount(_Count);
                        }
                    }

                    // 初始速度大小与半径的比值的取值范围与默认值
                    public const double Velocity_MinValue = 4, Velocity_MaxValue = 512, Velocity_DefaultValue = 16;
                    // 初始速度大小与半径的比值
                    private static double _Velocity = Velocity_DefaultValue;
                    public static double Velocity
                    {
                        get
                        {
                            return _Velocity;
                        }
                        set
                        {
                            _Velocity = Math.Max(Velocity_MinValue, Math.Min(value, Velocity_MaxValue));
                        }
                    }

                    // 初始速度方向的取值范围与默认值
                    public const double Angle_MinValue = 5, Angle_MaxValue = 175, Angle_DefaultValue = 150;
                    // 初始速度方向（角度）
                    private static double _Angle = Angle_DefaultValue;
                    public static double Angle
                    {
                        get
                        {
                            return _Angle;
                        }
                        set
                        {
                            _Angle = Math.Max(Angle_MinValue, Math.Min(value, Angle_MaxValue));
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 重力加速度的取值范围与默认值
                    public const double GravitationalAcceleration_MinValue = 0, GravitationalAcceleration_MaxValue = 64, GravitationalAcceleration_DefaultValue = 2;
                    // 重力加速度（像素/平方秒）
                    private static double _GravitationalAcceleration = GravitationalAcceleration_DefaultValue;
                    public static double GravitationalAcceleration
                    {
                        get
                        {
                            return _GravitationalAcceleration;
                        }
                        set
                        {
                            _GravitationalAcceleration = Math.Max(GravitationalAcceleration_MinValue, Math.Min(value, GravitationalAcceleration_MaxValue));
                        }
                    }

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinRadius = MinRadius_DefaultValue;
                        MaxRadius = MaxRadius_DefaultValue;
                        Length = Length_DefaultValue;
                        Count = Count_DefaultValue;
                        Velocity = Velocity_DefaultValue;
                        Angle = Angle_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                        GravitationalAcceleration = GravitationalAcceleration_DefaultValue;
                    }
                }

                // 初始化到当前时刻的总秒数
                private double TotalSeconds;

                // 当前颜色的 Alpha 通道
                private int _Alpha => Math.Max(0, Math.Min((int)(255 * Math.Pow(1 - CurrentLocation.Y / FormBounds.Height, 2)), 255));
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (CurrentRadius - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 此前所有颜色持续时间的总秒数
                private double _TotalSecondsOfOldColors;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = (TotalSeconds - _TotalSecondsOfOldColors) * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfOldColors = TotalSeconds;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 初始半径参数
                private double _InitialRadius;
                // 初始半径（像素）
                private double InitialRadius => (1 - _InitialRadius) * Settings.MinRadius + _InitialRadius * Settings.MaxRadius;
                // 当前半径（像素）
                private double CurrentRadius
                {
                    get
                    {
                        double _IR = InitialRadius;
                        return Math.Max(1, Math.Min(_IR * Math.Pow(1 - CurrentLocation.Y / FormBounds.Height, 2), _IR));
                    }
                }
                // 初始长度（像素）
                private double InitialLength => Settings.Length * InitialRadius;
                // 当前长度（像素）
                private double CurrentLength => Math.Max(1, Math.Min(InitialLength * (1 - CurrentLocation.Y / FormBounds.Height), InitialLength));

                // 初始速度大小（像素/秒）
                private double InitialVelocity_Abs => Settings.Velocity * InitialRadius;
                // 初始速度方向（弧度）
                private double InitialVelocity_Angle => Settings.Angle / 180 * Math.PI;
                // 初始速度（像素/秒，像素/秒）
                private Com.PointD InitialVelocity => InitialVelocity_Abs * new Com.PointD(Math.Cos(InitialVelocity_Angle), Math.Sin(InitialVelocity_Angle));
                // 当前速度（像素/秒，像素/秒）
                private Com.PointD CurrentVelocity => new Com.PointD(InitialVelocity.X, InitialVelocity.Y + (Settings.GravitationalAcceleration * InitialRadius / Settings.MaxRadius) * TotalSeconds);
                // 初始中心位置参数
                private Com.PointD _InitialLocation;
                // 初始中心位置（像素，像素）
                private PointF InitialLocation
                {
                    get
                    {
                        Com.PointD __IL = _InitialLocation;
                        return new PointF((float)((1 - __IL.X) * (-InitialRadius) + __IL.X * (FormBounds.Width + InitialRadius)), (float)((1 - __IL.Y) * (-InitialRadius) + __IL.Y * (FormBounds.Height + InitialRadius)));
                    }
                }
                // 当前中心位置（像素，像素）
                private PointF CurrentLocation
                {
                    get
                    {
                        PointF _IL = InitialLocation;
                        return new PointF((float)(_IL.X + InitialVelocity.X * TotalSeconds), (float)(_IL.Y + (InitialVelocity.Y + CurrentVelocity.Y) * TotalSeconds / 2));
                    }
                }

                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        PointF _CL = CurrentLocation;
                        Com.PointD Loc = new Com.PointD(_CL);
                        double _CR = CurrentRadius;
                        double _CLength = CurrentLength;
                        double Angle = CurrentVelocity.VectorAngle;

                        GraphicsPath GP = new GraphicsPath();
                        GP.AddArc(new RectangleF((float)(_CL.X - _CR), (float)(_CL.Y - _CR), (float)(2 * _CR), (float)(2 * _CR)), (float)(Angle / Math.PI * 180 - 90), 180F);
                        GP.AddLines(new PointF[] { (Loc + _CR * new Com.PointD(Math.Cos(Angle + Math.PI / 2), Math.Sin(Angle + Math.PI / 2))).ToPointF(), (Loc + _CLength * new Com.PointD(Math.Cos(Angle + Math.PI), Math.Sin(Angle + Math.PI))).ToPointF(), (Loc + _CR * new Com.PointD(Math.Cos(Angle - Math.PI / 2), Math.Sin(Angle - Math.PI / 2))).ToPointF() });
                        GP.CloseFigure();
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    TotalSeconds = 0;
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfOldColors = 0;
                    _InitialRadius = Rand.NextDouble();

                    // 按照概率决定在位图的哪一边界生成此元素
                    double _W = FormBounds.Height / (2 * Math.Abs(Math.Tan(InitialVelocity_Angle))), P = _W / (_W + FormBounds.Width);
                    double R_Float = Rand.NextDouble();

                    if (R_Float >= P)
                    {
                        _InitialLocation = new Com.PointD(Rand.NextDouble(), 0);
                    }
                    else
                    {
                        if (InitialVelocity_Angle < Math.PI / 2)
                        {
                            _InitialLocation = new Com.PointD(0, Rand.NextDouble());
                        }
                        else
                        {
                            _InitialLocation = new Com.PointD(1, Rand.NextDouble());
                        }
                    }
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                    // 使同时产生的大量图形能够更均匀的先后出现在位图中，而不是同时出现
                    TotalSeconds -= (Rand.NextDouble() * Math.Min(FormBounds.Width / Math.Abs(InitialVelocity.X), FormBounds.Height / Math.Abs(InitialVelocity.Y)));
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 雪

            public class Snow
            {
                // 列表
                private static List<Snow> List = new List<Snow>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            Snow Element = new Snow();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (Snow Element in List)
                    {
                        Element.TotalSeconds += DeltaTime.Elapsed.TotalSeconds;
                        Element._UpdateColor();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (Snow Element in List)
                            {
                                if (Element.TotalSeconds >= 0)
                                {
                                    PointF _CL = Element.CurrentLocation;
                                    Com.PointD Loc = new Com.PointD(_CL);
                                    double _CR = Element.CurrentRadius;
                                    if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-_CR), (float)(-_CR), (float)(FormBounds.Width + 2 * _CR), (float)(FormBounds.Height + 2 * _CR))))
                                    // 在位图中绘制列表中的所有图形
                                    {
                                        // 绘制粒子
                                        switch (Settings.GlowMode)
                                        {
                                            case GlowModes.OuterGlow:
                                                {
                                                    RectangleF Bounds_Outer_Outer = Element.Bounds;
                                                    RectangleF Bounds_Outer_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.25F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.25F, Bounds_Outer_Outer.Width * 0.5F, Bounds_Outer_Outer.Height * 0.5F);
                                                    RectangleF Bounds_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.2F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.2F, Bounds_Outer_Outer.Width * 0.6F, Bounds_Outer_Outer.Height * 0.6F);

                                                    PathGradientBrush PGB_Outer = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    GraphicsPath Path_Outer = new GraphicsPath();
                                                    Path_Outer.AddEllipse(Bounds_Outer_Inner);
                                                    Path_Outer.AddEllipse(Bounds_Outer_Outer);
                                                    Grap.FillPath(PGB_Outer, Path_Outer);
                                                    PGB_Outer.Dispose();
                                                    Path_Outer.Dispose();

                                                    GraphicsPath Path_Inner = new GraphicsPath();
                                                    Path_Inner.AddEllipse(Bounds_Inner);
                                                    PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                    {
                                                        CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.8F, 0.8F)
                                                    };
                                                    Grap.FillPath(PGB_Inner, Path_Inner);
                                                    Path_Inner.Dispose();
                                                    PGB_Inner.Dispose();
                                                }
                                                break;

                                            case GlowModes.InnerGlow:
                                                {
                                                    PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    Grap.FillPath(PGB, Element.Path);
                                                    PGB.Dispose();
                                                }
                                                break;

                                            case GlowModes.EvenGlow:
                                                {
                                                    SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                    Grap.FillPath(SB, Element.Path);
                                                    SB.Dispose();
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    // 当图形中心离开位图边界时将其重置
                                    {
                                        Element.Reset();
                                    }
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(0, 0, 0, 50);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 半径的取值范围
                    private const double _Radius_MinValue = 1, _Radius_MaxValue = 64;
                    // 最小半径的取值范围与默认值
                    public const double MinRadius_MinValue = _Radius_MinValue, MinRadius_MaxValue = _Radius_MaxValue, MinRadius_DefaultValue = 2;
                    // 最小半径（像素）
                    private static double _MinRadius = MinRadius_DefaultValue;
                    public static double MinRadius
                    {
                        get
                        {
                            return _MinRadius;
                        }
                        set
                        {
                            _MinRadius = Math.Max(MinRadius_MinValue, Math.Min(value, MinRadius_MaxValue));

                            if (_MaxRadius < _MinRadius) _MaxRadius = _MinRadius;
                        }
                    }
                    // 最大半径的取值范围与默认值
                    public const double MaxRadius_MinValue = _Radius_MinValue, MaxRadius_MaxValue = _Radius_MaxValue, MaxRadius_DefaultValue = 8;
                    // 最大半径（像素）
                    private static double _MaxRadius = MaxRadius_DefaultValue;
                    public static double MaxRadius
                    {
                        get
                        {
                            return _MaxRadius;
                        }
                        set
                        {
                            _MaxRadius = Math.Max(MaxRadius_MinValue, Math.Min(value, MaxRadius_MaxValue));

                            if (_MinRadius > _MaxRadius) _MinRadius = _MaxRadius;
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 8192, Count_DefaultValue = 150;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled) _SetListCount(_Count);
                        }
                    }

                    // 初速度大小与半径的比值的取值范围与默认值
                    public const double Velocity_MinValue = 1, Velocity_MaxValue = 128, Velocity_DefaultValue = 2.5;
                    // 初速度大小与半径的比值
                    private static double _Velocity = Velocity_DefaultValue;
                    public static double Velocity
                    {
                        get
                        {
                            return _Velocity;
                        }
                        set
                        {
                            _Velocity = Math.Max(Velocity_MinValue, Math.Min(value, Velocity_MaxValue));
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinRadius = MinRadius_DefaultValue;
                        MaxRadius = MaxRadius_DefaultValue;
                        Count = Count_DefaultValue;
                        Velocity = Velocity_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                    }
                }

                // 初始化到当前时刻的总秒数
                private double TotalSeconds;

                // 当前颜色的 Alpha 通道
                private int _Alpha => Math.Max(0, Math.Min((int)(255 * (1 - CurrentLocation.Y / FormBounds.Height)), 255));
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (CurrentRadius - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 此前所有颜色持续时间的总秒数
                private double _TotalSecondsOfOldColors;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = (TotalSeconds - _TotalSecondsOfOldColors) * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfOldColors = TotalSeconds;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 初始半径参数
                private double _InitialRadius;
                // 初始半径（像素）
                private double InitialRadius => (1 - _InitialRadius) * Settings.MinRadius + _InitialRadius * Settings.MaxRadius;
                // 当前半径（像素）
                private double CurrentRadius
                {
                    get
                    {
                        double _IR = InitialRadius;
                        return Math.Max(1, Math.Min(_IR * (1 - CurrentLocation.Y / FormBounds.Height), _IR));
                    }
                }

                // 速度大小（像素/秒）
                private double Velocity_Abs => Settings.Velocity * InitialRadius;
                // 初始中心位置参数
                private Com.PointD _InitialLocation;
                // 初始中心位置（像素，像素）
                private PointF InitialLocation
                {
                    get
                    {
                        Com.PointD __IL = _InitialLocation;
                        return new PointF((float)((1 - __IL.X) * (-InitialRadius) + __IL.X * (FormBounds.Width + InitialRadius)), (float)((1 - __IL.Y) * (-InitialRadius) + __IL.Y * (FormBounds.Height + InitialRadius)));
                    }
                }
                // 当前中心位置（像素，像素）
                private PointF CurrentLocation
                {
                    get
                    {
                        PointF _IL = InitialLocation;
                        return new PointF(_IL.X, (float)(_IL.Y + Velocity_Abs * TotalSeconds));
                    }
                }

                // 边界
                private RectangleF Bounds
                {
                    get
                    {
                        PointF _CL = CurrentLocation;
                        double _CR = CurrentRadius;
                        return new RectangleF((float)(_CL.X - _CR), (float)(_CL.Y - _CR), (float)(2 * _CR), (float)(2 * _CR));
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddEllipse(Bounds);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    TotalSeconds = 0;
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfOldColors = 0;
                    _InitialRadius = Rand.NextDouble();
                    _InitialLocation = new Com.PointD(Rand.NextDouble(), 0);
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                    // 使同时产生的大量图形能够更均匀的先后出现在位图中，而不是同时出现
                    TotalSeconds -= (Rand.NextDouble() * (FormBounds.Height / Velocity_Abs));
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 引力粒子

            public class GravityParticle
            {
                // 列表
                private static List<GravityParticle> List = new List<GravityParticle>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            GravityParticle Element = new GravityParticle();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    // 更新颜色
                    foreach (GravityParticle Element in List)
                    {
                        Element._UpdateColor();
                    }

                    // 弹性碰撞
                    for (int i = 0; i < List.Count; i++)
                    {
                        for (int j = i + 1; j < List.Count; j++)
                        {
                            GravityParticle Element1 = List[i], Element2 = List[j];
                            Com.PointD Loc1 = new Com.PointD(Element1.Location), Loc2 = new Com.PointD(Element2.Location);
                            double R1 = Element1.Radius, R2 = Element2.Radius;
                            double Dist = Com.PointD.DistanceBetween(Loc1, Loc2);

                            if (Dist < R1 + R2)
                            {
                                double M1 = Element1.Mass, M2 = Element2.Mass;
                                Com.PointD V1 = Element1.Velocity, V2 = Element2.Velocity;
                                double E = Settings.ElasticRestitutionCoefficient;

                                // 刚体两体弹性碰撞速度公式
                                Com.PointD _V1 = ((M1 - E * M2) * V1 + (1 + E) * M2 * V2) / (M1 + M2), _V2 = ((1 + E) * M1 * V1 + (M2 - E * M1) * V2) / (M1 + M2);
                                Element1.Velocity = _V1;
                                Element2.Velocity = _V2;

                                // 将交叉的粒子按照质量反比分开
                                double d = (R1 + R2) - Dist;
                                double Angle_E1_E2 = Com.Geometry.GetAngleOfTwoPoints(Loc1, Loc2);
                                double Angle_E2_E1 = Angle_E1_E2 + Math.PI;
                                Element1.Location = (Loc1 + d * (M2 / (M1 + M2)) * new Com.PointD(Math.Cos(Angle_E2_E1), Math.Sin(Angle_E2_E1))).ToPointF();
                                Element2.Location = (Loc2 + d * (M1 / (M1 + M2)) * new Com.PointD(Math.Cos(Angle_E1_E2), Math.Sin(Angle_E1_E2))).ToPointF();
                            }
                        }
                    }

                    // 更新中心位置与速度
                    foreach (GravityParticle Element in List)
                    {
                        Element._UpdateLocationAndVelocity();
                    }

                    // 当较小的粒子完全包含于较大的粒子时，将较小的粒子重置
                    for (int i = 0; i < List.Count; i++)
                    {
                        for (int j = i + 1; j < List.Count; j++)
                        {
                            GravityParticle Element1 = List[i], Element2 = List[j];
                            double R1 = Element1.Radius, R2 = Element2.Radius;
                            double Dist = Com.PointD.DistanceBetween(new Com.PointD(Element1.Location), new Com.PointD(Element2.Location));

                            if (Dist + R1 <= R2)
                            {
                                Element1.Reset();
                            }
                            else if (Dist + R2 <= R1)
                            {
                                Element2.Reset();
                            }
                        }
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (GravityParticle Element in List)
                            {
                                Com.PointD Loc = new Com.PointD(Element.Location);
                                double _R = Element.Radius;
                                if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-_R), (float)(-_R), (float)(FormBounds.Width + 2 * _R), (float)(FormBounds.Height + 2 * _R))))
                                // 在位图中绘制列表中的所有图形
                                {
                                    switch (Settings.GlowMode)
                                    {
                                        case GlowModes.OuterGlow:
                                            {
                                                RectangleF Bounds_Outer_Outer = Element.Bounds;
                                                RectangleF Bounds_Outer_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.25F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.25F, Bounds_Outer_Outer.Width * 0.5F, Bounds_Outer_Outer.Height * 0.5F);
                                                RectangleF Bounds_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.2F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.2F, Bounds_Outer_Outer.Width * 0.6F, Bounds_Outer_Outer.Height * 0.6F);

                                                PathGradientBrush PGB_Outer = new PathGradientBrush(Element.Path)
                                                {
                                                    CenterColor = Element.CurrentColor,
                                                    SurroundColors = new Color[] { Color.Transparent },
                                                    FocusScales = new PointF(0.5F, 0.5F)
                                                };
                                                GraphicsPath Path_Outer = new GraphicsPath();
                                                Path_Outer.AddEllipse(Bounds_Outer_Inner);
                                                Path_Outer.AddEllipse(Bounds_Outer_Outer);
                                                Grap.FillPath(PGB_Outer, Path_Outer);
                                                PGB_Outer.Dispose();
                                                Path_Outer.Dispose();

                                                GraphicsPath Path_Inner = new GraphicsPath();
                                                Path_Inner.AddEllipse(Bounds_Inner);
                                                PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                {
                                                    CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                    SurroundColors = new Color[] { Color.Transparent },
                                                    FocusScales = new PointF(0.8F, 0.8F)
                                                };
                                                Grap.FillPath(PGB_Inner, Path_Inner);
                                                Path_Inner.Dispose();
                                                PGB_Inner.Dispose();
                                            }
                                            break;

                                        case GlowModes.InnerGlow:
                                            {
                                                PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                {
                                                    CenterColor = Element.CurrentColor,
                                                    SurroundColors = new Color[] { Color.Transparent },
                                                    FocusScales = new PointF(0.5F, 0.5F)
                                                };
                                                Grap.FillPath(PGB, Element.Path);
                                                PGB.Dispose();
                                            }
                                            break;

                                        case GlowModes.EvenGlow:
                                            {
                                                SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                Grap.FillPath(SB, Element.Path);
                                                SB.Dispose();
                                            }
                                            break;
                                    }
                                }
                                else
                                // 当图形中心离开位图边界时将其重置
                                {
                                    Element.Reset();
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(15, 15, 15, 15);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 质量的取值范围
                    public const double _Mass_MinValue = 1, _Mass_MaxValue = 4096;
                    // 最小质量的取值范围与默认值
                    public const double MinMass_MinValue = _Mass_MinValue, MinMass_MaxValue = _Mass_MaxValue, MinMass_DefaultValue = 4;
                    // 最小质量（质量单位）
                    private static double _MinMass = MinMass_DefaultValue;
                    public static double MinMass
                    {
                        get
                        {
                            return _MinMass;
                        }
                        set
                        {
                            _MinMass = Math.Max(MinMass_MinValue, Math.Min(value, MinMass_MaxValue));

                            if (_MaxMass < _MinMass) _MaxMass = _MinMass;
                        }
                    }
                    // 最大质量的取值范围与默认值
                    public const double MaxMass_MinValue = _Mass_MinValue, MaxMass_MaxValue = _Mass_MaxValue, MaxMass_DefaultValue = 64;
                    // 最大质量（质量单位）
                    private static double _MaxMass = MaxMass_DefaultValue;
                    public static double MaxMass
                    {
                        get
                        {
                            return _MaxMass;
                        }
                        set
                        {
                            _MaxMass = Math.Max(MaxMass_MinValue, Math.Min(value, MaxMass_MaxValue));

                            if (_MinMass > _MaxMass) _MinMass = _MaxMass;
                        }
                    }

                    // 密度（质量单位/平方像素）
                    public const double Density = 1;

                    // 最小半径（像素）
                    public static double MinRadius => Math.Max(1, Math.Sqrt(MinMass / Density));
                    // 最大半径（像素）
                    public static double MaxRadius => Math.Max(1, Math.Sqrt(MaxMass / Density));

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 1024, Count_DefaultValue = 100;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled) _SetListCount(_Count);
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 鼠标指针质量的取值范围与默认值
                    public const double CursorMass_MinValue = 0, CursorMass_MaxValue = 262144, CursorMass_DefaultValue = 4096;
                    // 鼠标指针质量（质量单位）
                    private static double _CursorMass = CursorMass_DefaultValue;
                    public static double CursorMass
                    {
                        get
                        {
                            return _CursorMass;
                        }
                        set
                        {
                            _CursorMass = Math.Max(CursorMass_MinValue, Math.Min(value, CursorMass_MaxValue));
                        }
                    }

                    // 引力常量的取值范围与默认值
                    public const double GravityConstant_MinValue = 0, GravityConstant_MaxValue = 16, GravityConstant_DefaultValue = 1;
                    // 引力常量（平方像素/(质量单位*平方秒)）
                    private static double _GravityConstant = GravityConstant_DefaultValue;
                    public static double GravityConstant
                    {
                        get
                        {
                            return _GravityConstant;
                        }
                        set
                        {
                            _GravityConstant = Math.Max(GravityConstant_MinValue, Math.Min(value, GravityConstant_MaxValue));
                        }
                    }

                    // 恢复系数的取值范围与默认值
                    public const double ElasticRestitutionCoefficient_MinValue = 0, ElasticRestitutionCoefficient_MaxValue = 2, ElasticRestitutionCoefficient_DefaultValue = 1;
                    // 恢复系数
                    private static double _ElasticRestitutionCoefficient = ElasticRestitutionCoefficient_DefaultValue;
                    public static double ElasticRestitutionCoefficient
                    {
                        get
                        {
                            return _ElasticRestitutionCoefficient;
                        }
                        set
                        {
                            _ElasticRestitutionCoefficient = Math.Max(ElasticRestitutionCoefficient_MinValue, Math.Min(value, ElasticRestitutionCoefficient_MaxValue));
                        }
                    }

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinMass = MinMass_DefaultValue;
                        MaxMass = MaxMass_DefaultValue;
                        Count = Count_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                        CursorMass = CursorMass_DefaultValue;
                        GravityConstant = GravityConstant_DefaultValue;
                        ElasticRestitutionCoefficient = ElasticRestitutionCoefficient_DefaultValue;
                    }
                }

                // 当前颜色的 Alpha 通道
                private int _Alpha
                {
                    get
                    {
                        double _R = 0;
                        double _W = FormBounds.Width / 2, _H = FormBounds.Height / 2;
                        PointF _CL = Location;
                        double _X = Math.Abs(_CL.X - _W), _Y = Math.Abs(_CL.Y - _H);

                        if (_X * _H == _Y * _W)
                        {
                            _R = Math.Sqrt(_X * _X + _Y * _Y);
                        }
                        else if (_X * _H > _Y * _W)
                        {
                            _R = _X * Math.Sqrt(1 + (_H * _H) / (_W * _W));
                        }
                        else
                        {
                            _R = _Y * Math.Sqrt(1 + (_W * _W) / (_H * _H));
                        }

                        return Math.Max(0, Math.Min((int)(255 * (1 - _R / Math.Sqrt(_W * _W + _H * _H))), 255));
                    }
                }
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (Radius - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 当前颜色持续时间的总秒数
                private double _TotalSecondsOfCurrentColor;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        _TotalSecondsOfCurrentColor += DeltaTime.Elapsed.TotalSeconds;

                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = _TotalSecondsOfCurrentColor * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfCurrentColor = 0;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 质量参数
                private double _Mass;
                // 质量（质量单位）
                private double Mass => (1 - _Mass) * Settings.MinMass + _Mass * Settings.MaxMass;
                // 半径（像素）
                private double Radius => Math.Max(1, Math.Sqrt(Mass / Settings.Density));
                // 速度（像素/秒，像素/秒）
                private Com.PointD Velocity;
                // 加速度（像素/平方秒，像素/平方秒）
                private Com.PointD Acceleration
                {
                    get
                    {
                        Com.PointD Acc = new Com.PointD(0, 0);

                        Com.PointD Loc = new Com.PointD(Location);

                        if (Settings.CursorMass > 0)
                        // 计算鼠标指针对此粒子产生的引力加速度
                        {
                            Com.PointD CurLoc = new Com.PointD(Cursor.Position.X - FormBounds.X, Cursor.Position.Y - FormBounds.Y);

                            double CurDist = Com.PointD.DistanceBetween(Loc, CurLoc);

                            if (CurDist > 0)
                            {
                                double _R = Radius;

                                // 万有引力公式（二维）与牛顿第二定律
                                double Acc_Abs = Settings.GravityConstant * Settings.CursorMass / Math.Max(_R, CurDist);

                                // 均匀球体内部的引力大小随距离是线性变化的
                                if (CurDist < _R)
                                {
                                    Acc_Abs *= CurDist / _R;
                                }

                                double Angle = Com.Geometry.GetAngleOfTwoPoints(Loc, CurLoc);
                                Acc.X += Acc_Abs * Math.Cos(Angle);
                                Acc.Y += Acc_Abs * Math.Sin(Angle);
                            }
                        }

                        // 计算所有粒子对此粒子产生的引力加速度
                        foreach (GravityParticle Element in List)
                        {
                            Com.PointD ELoc = new Com.PointD(Element.Location);
                            double ER = Element.Radius;
                            double EM = Element.Mass;

                            double Dist = Com.PointD.DistanceBetween(Loc, ELoc);

                            if (Dist > 0)
                            {
                                // 万有引力公式（二维）与牛顿第二定律
                                double Acc_Abs = Settings.GravityConstant * EM / Math.Max(ER, Dist);

                                // 均匀球体内部的引力大小随距离是线性变化的
                                if (Dist < ER)
                                {
                                    Acc_Abs *= Dist / ER;
                                }

                                double Angle = Com.Geometry.GetAngleOfTwoPoints(Loc, ELoc);
                                Acc.X += Acc_Abs * Math.Cos(Angle);
                                Acc.Y += Acc_Abs * Math.Sin(Angle);
                            }
                        }

                        return Acc;
                    }
                }
                // 中心位置（像素，像素）
                private PointF Location;
                // 更新中心位置与速度
                private void _UpdateLocationAndVelocity()
                {
                    Com.PointD _Acc = Acceleration;
                    double dT = DeltaTime.Elapsed.TotalSeconds;

                    Location.X += (float)((2 * Velocity.X + _Acc.X * dT) * dT / 2);
                    Location.Y += (float)((2 * Velocity.Y + _Acc.Y * dT) * dT / 2);

                    Velocity.X += _Acc.X * dT;
                    Velocity.Y += _Acc.Y * dT;
                }

                // 边界
                private RectangleF Bounds
                {
                    get
                    {
                        PointF _L = Location;
                        double _R = Radius;
                        return new RectangleF((float)(_L.X - _R), (float)(_L.Y - _R), (float)(2 * _R), (float)(2 * _R));
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddEllipse(Bounds);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfCurrentColor = 0;

                    _Mass = Rand.NextDouble();
                    double _R = Radius;

                    // 在位图的任一边界上生成此元素
                    Com.PointD _Location;
                    double _V_Abs = 8 * Math.Sqrt((Settings.MinMass + Rand.NextDouble() * (Settings.MaxMass - Settings.MinMass)) / Mass);
                    double _V_Angle = Rand.NextDouble() * (Math.PI / 2);
                    int R_Int = Rand.Next(0, 4);

                    switch (R_Int)
                    {
                        case 0:
                            {
                                _Location = new Com.PointD(0, Rand.NextDouble());
                            }
                            break;

                        case 1:
                            {
                                _Location = new Com.PointD(Rand.NextDouble(), 0);
                                _V_Angle += Math.PI / 2;
                            }
                            break;

                        case 2:
                            {
                                _Location = new Com.PointD(1, Rand.NextDouble());
                                _V_Angle += Math.PI;
                            }
                            break;

                        default:
                            {
                                _Location = new Com.PointD(Rand.NextDouble(), 1);
                                _V_Angle += Math.PI * 3 / 2;
                            }
                            break;
                    }

                    Location = new PointF((float)((1 - _Location.X) * (-_R) + _Location.X * (FormBounds.Width + _R)), (float)((1 - _Location.Y) * (-_R) + _Location.Y * (FormBounds.Height + _R)));
                    Velocity = _V_Abs * new Com.PointD(Math.Cos(_V_Angle), Math.Sin(_V_Angle));
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 引力网

            public class GravityGrid
            {
                // 列表
                private static List<GravityGrid> List = new List<GravityGrid>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            GravityGrid Element = new GravityGrid();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (GravityGrid Element in List)
                    {
                        Element._UpdateColor();
                        Element._UpdateLocationAndVelocity();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            // 窗体边界对角线长度的 1/8
                            double PartDiag = Math.Sqrt(Math.Pow(FormBounds.Width, 2) + Math.Pow(FormBounds.Height, 2)) / 8;

                            double _R = Settings.Radius;
                            Com.PointD CurLoc = new Com.PointD(Cursor.Position.X - FormBounds.X, Cursor.Position.Y - FormBounds.Y);
                            PointF Cur = CurLoc.ToPointF();

                            for (int i = 0; i < List.Count; i++)
                            {
                                GravityGrid Element1 = List[i];
                                PointF _L1 = Element1.Location;
                                Com.PointD Loc1 = new Com.PointD(_L1);

                                if (Com.Geometry.PointIsVisibleInRectangle(Loc1, new RectangleF((float)(-_R), (float)(-_R), (float)(FormBounds.Width + 2 * _R), (float)(FormBounds.Height + 2 * _R))))
                                // 在位图中绘制列表中的所有图形
                                {
                                    // 绘制与其他所有点的连线
                                    for (int j = i + 1; j < List.Count; j++)
                                    {
                                        GravityGrid Element2 = List[j];
                                        PointF _L2 = Element2.Location;
                                        Com.PointD Loc2 = new Com.PointD(_L2);

                                        if (Com.Geometry.PointIsVisibleInRectangle(Loc1, new RectangleF((float)(-_R), (float)(-_R), (float)(FormBounds.Width + 2 * _R), (float)(FormBounds.Height + 2 * _R))))
                                        {
                                            double Dist = Com.PointD.DistanceBetween(Loc1, Loc2);

                                            if (Dist > 2 * _R && Dist < PartDiag)
                                            {
                                                double _Alpha = Math.Max(0, Math.Min(Math.Pow((PartDiag - Dist) / (PartDiag - 2 * _R), 2), 1));
                                                Color Cr1 = Color.FromArgb((int)(_Alpha * Element1._Alpha), Com.ColorManipulation.ShiftLightnessByHSL(Element1.CurrentColor, 0.5)), Cr2 = Color.FromArgb((int)(_Alpha * Element2._Alpha), Com.ColorManipulation.ShiftLightnessByHSL(Element2.CurrentColor, 0.5));
                                                LinearGradientBrush LGB = new LinearGradientBrush(_L1, _L2, Cr1, Cr2);
                                                Pen P = new Pen(LGB, Settings.LineWidth);
                                                Grap.DrawLine(P, _L1, _L2);
                                                LGB.Dispose();
                                                P.Dispose();
                                            }
                                        }
                                    }

                                    // 绘制与鼠标指针的连线
                                    double Dist_Cur = Com.PointD.DistanceBetween(Loc1, CurLoc);

                                    if (Dist_Cur > 2 * _R && Dist_Cur < PartDiag)
                                    {
                                        double _Alpha = Math.Max(0, Math.Min(Math.Pow((PartDiag - Dist_Cur) / (PartDiag - 2 * _R), 2), 1));
                                        Color Cr = Color.FromArgb((int)(_Alpha * Element1._Alpha), Com.ColorManipulation.ShiftLightnessByHSL(Element1.CurrentColor, 0.5));
                                        Pen P = new Pen(Cr, Settings.LineWidth);
                                        Grap.DrawLine(P, _L1, Cur);
                                        P.Dispose();
                                    }

                                    // 绘制粒子
                                    switch (Settings.GlowMode)
                                    {
                                        case GlowModes.OuterGlow:
                                            {
                                                RectangleF Bounds_Outer_Outer = Element1.Bounds;
                                                RectangleF Bounds_Outer_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.25F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.25F, Bounds_Outer_Outer.Width * 0.5F, Bounds_Outer_Outer.Height * 0.5F);
                                                RectangleF Bounds_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.2F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.2F, Bounds_Outer_Outer.Width * 0.6F, Bounds_Outer_Outer.Height * 0.6F);

                                                PathGradientBrush PGB_Outer = new PathGradientBrush(Element1.Path)
                                                {
                                                    CenterColor = Element1.CurrentColor,
                                                    SurroundColors = new Color[] { Color.Transparent },
                                                    FocusScales = new PointF(0.5F, 0.5F)
                                                };
                                                GraphicsPath Path_Outer = new GraphicsPath();
                                                Path_Outer.AddEllipse(Bounds_Outer_Inner);
                                                Path_Outer.AddEllipse(Bounds_Outer_Outer);
                                                Grap.FillPath(PGB_Outer, Path_Outer);
                                                PGB_Outer.Dispose();
                                                Path_Outer.Dispose();

                                                GraphicsPath Path_Inner = new GraphicsPath();
                                                Path_Inner.AddEllipse(Bounds_Inner);
                                                PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                {
                                                    CenterColor = Color.FromArgb(Element1._Alpha, Color.White),
                                                    SurroundColors = new Color[] { Color.Transparent },
                                                    FocusScales = new PointF(0.8F, 0.8F)
                                                };
                                                Grap.FillPath(PGB_Inner, Path_Inner);
                                                Path_Inner.Dispose();
                                                PGB_Inner.Dispose();
                                            }
                                            break;

                                        case GlowModes.InnerGlow:
                                            {
                                                PathGradientBrush PGB = new PathGradientBrush(Element1.Path)
                                                {
                                                    CenterColor = Element1.CurrentColor,
                                                    SurroundColors = new Color[] { Color.Transparent },
                                                    FocusScales = new PointF(0.5F, 0.5F)
                                                };
                                                Grap.FillPath(PGB, Element1.Path);
                                                PGB.Dispose();
                                            }
                                            break;

                                        case GlowModes.EvenGlow:
                                            {
                                                SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element1.CurrentColor, 0.25));
                                                Grap.FillPath(SB, Element1.Path);
                                                SB.Dispose();
                                            }
                                            break;
                                    }
                                }
                                else
                                // 当图形中心离开位图边界时将其重置
                                {
                                    Element1.Reset();
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(15, 15, 15, 15);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 半径的取值范围与默认值
                    public const double Radius_MinValue = 1, Radius_MaxValue = 64, Radius_DefaultValue = 2;
                    // 半径（像素）
                    private static double _Radius = Radius_DefaultValue;
                    public static double Radius
                    {
                        get
                        {
                            return _Radius;
                        }
                        set
                        {
                            _Radius = Math.Max(Radius_MinValue, Math.Min(value, Radius_MaxValue));
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 1024, Count_DefaultValue = 100;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled)
                            {
                                _SetListCount(_Count);
                            }
                        }
                    }

                    // 线宽的取值范围与默认值
                    public const float LineWidth_MinValue = 0.5F, LineWidth_MaxValue = 16F, LineWidth_DefaultValue = 1F;
                    // 线宽（像素）
                    private static float _LineWidth = LineWidth_DefaultValue;
                    public static float LineWidth
                    {
                        get
                        {
                            return _LineWidth;
                        }
                        set
                        {
                            _LineWidth = Math.Max(LineWidth_MinValue, Math.Min(value, LineWidth_MaxValue));
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 鼠标指针质量的取值范围与默认值
                    public const double CursorMass_MinValue = 256, CursorMass_MaxValue = 65536, CursorMass_DefaultValue = 4096;
                    // 鼠标指针质量（质量单位）
                    private static double _CursorMass = CursorMass_DefaultValue;
                    public static double CursorMass
                    {
                        get
                        {
                            return _CursorMass;
                        }
                        set
                        {
                            _CursorMass = Math.Max(CursorMass_MinValue, Math.Min(value, CursorMass_MaxValue));
                        }
                    }

                    // 鼠标指针斥力范围的取值范围与默认值
                    public const int CursorRepulsionRadius_MinValue = 0, CursorRepulsionRadius_MaxValue = 512, CursorRepulsionRadius_DefaultValue = 100;
                    // 鼠标指针斥力范围
                    private static int _CursorRepulsionRadius = CursorRepulsionRadius_DefaultValue;
                    public static int CursorRepulsionRadius
                    {
                        get
                        {
                            return _CursorRepulsionRadius;
                        }
                        set
                        {
                            _CursorRepulsionRadius = Math.Max(CursorRepulsionRadius_MinValue, Math.Min(value, CursorRepulsionRadius_MaxValue));
                        }
                    }

                    // 引力常量（平方像素/(质量单位*平方秒)）
                    public const double GravityConstant = 1;

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        Radius = Radius_DefaultValue;
                        Count = Count_DefaultValue;
                        LineWidth = LineWidth_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                        CursorMass = CursorMass_DefaultValue;
                        CursorRepulsionRadius = CursorRepulsionRadius_DefaultValue;
                    }
                }

                // 当前颜色的 Alpha 通道
                private int _Alpha
                {
                    get
                    {
                        double _R = 0;
                        double _W = FormBounds.Width / 2, _H = FormBounds.Height / 2;
                        PointF _CL = Location;
                        double _X = Math.Abs(_CL.X - _W), _Y = Math.Abs(_CL.Y - _H);

                        if (_X * _H == _Y * _W)
                        {
                            _R = Math.Sqrt(_X * _X + _Y * _Y);
                        }
                        else if (_X * _H > _Y * _W)
                        {
                            _R = _X * Math.Sqrt(1 + (_H * _H) / (_W * _W));
                        }
                        else
                        {
                            _R = _Y * Math.Sqrt(1 + (_W * _W) / (_H * _H));
                        }

                        return Math.Max(0, Math.Min((int)(255 * (1 - _R / Math.Sqrt(_W * _W + _H * _H))), 255));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 当前颜色持续时间的总秒数
                private double _TotalSecondsOfCurrentColor;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, _CurrentColor);
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        _TotalSecondsOfCurrentColor += DeltaTime.Elapsed.TotalSeconds;

                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = _TotalSecondsOfCurrentColor * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfCurrentColor = 0;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 当粒子处于鼠标指针斥力范围附近达到此毫秒数后，使鼠标指针对此粒子的引力加速度反向
                private double _RepulsionAfterThisMS => Settings.CursorRepulsionRadius * 20;
                // 粒子已经处于鼠标指针斥力范围附近的毫秒数
                private double _MSOfNearByCursor;

                // 速度（像素/秒，像素/秒）
                private Com.PointD Velocity;
                // 加速度（像素/平方秒，像素/平方秒）
                private Com.PointD Acceleration
                {
                    get
                    {
                        Com.PointD Acc = new Com.PointD(0, 0);

                        Com.PointD Loc = new Com.PointD(Location);

                        Com.PointD CurLoc = new Com.PointD(Cursor.Position.X - FormBounds.X, Cursor.Position.Y - FormBounds.Y);

                        double CurDist = Com.PointD.DistanceBetween(Loc, CurLoc);

                        // 当粒子处于鼠标指针斥力范围附近时，更新计时参数；当粒子离开鼠标指针斥力范围较远时，重置计时参数
                        if (CurDist < Settings.CursorRepulsionRadius * 2)
                        {
                            if (_MSOfNearByCursor < _RepulsionAfterThisMS)
                            {
                                // 在同一计时周期内，若多次引用 Acceleration 将导致 _MSOfNearByCursor 严重偏大。
                                _MSOfNearByCursor += DeltaTime.Elapsed.TotalMilliseconds;
                            }
                        }
                        else
                        {
                            _MSOfNearByCursor = 0;
                        }

                        if (CurDist > 0)
                        // 计算鼠标指针对此粒子产生的引力加速度
                        {
                            double _R = Settings.Radius;

                            // 万有引力公式（二维）与牛顿第二定律
                            double Acc_Abs = Settings.GravityConstant * Settings.CursorMass / Math.Max(Settings.CursorRepulsionRadius, Math.Max(_R, CurDist));

                            // 当粒子到鼠标指针的距离小于粒子直径时令引力为 0
                            if (CurDist < 2 * _R)
                            {
                                Acc_Abs = 0;
                            }

                            // 当粒子处于鼠标指针斥力范围内达到一定时间后，使该粒子减速，并且使引力加速度反向
                            if (_MSOfNearByCursor >= _RepulsionAfterThisMS && CurDist < Settings.CursorRepulsionRadius)
                            {
                                Acc.X -= Velocity.X * 0.5;
                                Acc.Y -= Velocity.Y * 0.5;

                                Acc_Abs *= -1;
                            }

                            double Angle = Com.Geometry.GetAngleOfTwoPoints(Loc, CurLoc);
                            Acc.X += Acc_Abs * Math.Cos(Angle);
                            Acc.Y += Acc_Abs * Math.Sin(Angle);
                        }

                        // 当粒子处于鼠标指针斥力范围附近时，计算所有粒子对此粒子产生的引力加速度
                        if (CurDist < Settings.CursorRepulsionRadius * 2)
                        {
                            for (int i = 0; i < List.Count; i++)
                            {
                                GravityGrid Element = List[i];
                                Com.PointD ELoc = new Com.PointD(Element.Location);
                                double ER = Settings.Radius;
                                double EM = Settings.CursorMass * 0.25 / Settings.Count;

                                double Dist = Com.PointD.DistanceBetween(Loc, ELoc);

                                if (Dist > 0)
                                {
                                    // 万有引力公式（二维）与牛顿第二定律
                                    double Acc_Abs = Settings.GravityConstant * EM / Math.Max(ER, Dist);

                                    // 均匀球体内部的引力大小随距离是线性变化的
                                    if (Dist < ER)
                                    {
                                        Acc_Abs *= Dist / ER;
                                    }

                                    // 当粒子处于鼠标指针斥力范围边缘时，使此粒子的引力加速度反向
                                    if (CurDist > Settings.CursorRepulsionRadius * 0.9 && CurDist < Settings.CursorRepulsionRadius * 1.1)
                                    {
                                        Acc_Abs *= -1;
                                    }

                                    double Angle = Com.Geometry.GetAngleOfTwoPoints(Loc, ELoc);
                                    Acc.X += Acc_Abs * Math.Cos(Angle);
                                    Acc.Y += Acc_Abs * Math.Sin(Angle);
                                }
                            }
                        }

                        return Acc;
                    }
                }
                // 中心位置（像素，像素）
                private PointF Location;
                // 更新中心位置与速度
                private void _UpdateLocationAndVelocity()
                {
                    Com.PointD _Acc = Acceleration;
                    double dT = DeltaTime.Elapsed.TotalSeconds;

                    Location.X += (float)((2 * Velocity.X + _Acc.X * dT) * dT / 2);
                    Location.Y += (float)((2 * Velocity.Y + _Acc.Y * dT) * dT / 2);

                    Velocity.X += _Acc.X * dT;
                    Velocity.Y += _Acc.Y * dT;
                }

                // 边界
                private RectangleF Bounds
                {
                    get
                    {
                        PointF _L = Location;
                        double _R = Settings.Radius;
                        return new RectangleF((float)(_L.X - _R), (float)(_L.Y - _R), (float)(2 * _R), (float)(2 * _R));
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddEllipse(Bounds);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfCurrentColor = 0;

                    double _R = Settings.Radius;

                    // 在位图的任一边界上生成此元素
                    Com.PointD _Location;
                    double _V_Abs = 8 * (Settings.Radius_MinValue + Rand.NextDouble() * (Settings.Radius_MaxValue - Settings.Radius_MinValue)) / _R;
                    double _V_Angle = Rand.NextDouble() * (Math.PI / 2);
                    int R_Int = Rand.Next(0, 4);

                    switch (R_Int)
                    {
                        case 0:
                            {
                                _Location = new Com.PointD(0, Rand.NextDouble());
                            }
                            break;

                        case 1:
                            {
                                _Location = new Com.PointD(Rand.NextDouble(), 0);
                                _V_Angle += Math.PI / 2;
                            }
                            break;

                        case 2:
                            {
                                _Location = new Com.PointD(1, Rand.NextDouble());
                                _V_Angle += Math.PI;
                            }
                            break;

                        default:
                            {
                                _Location = new Com.PointD(Rand.NextDouble(), 1);
                                _V_Angle += Math.PI * 3 / 2;
                            }
                            break;
                    }

                    Location = new PointF((float)((1 - _Location.X) * (-_R) + _Location.X * (FormBounds.Width + _R)), (float)((1 - _Location.Y) * (-_R) + _Location.Y * (FormBounds.Height + _R)));
                    Velocity = _V_Abs * new Com.PointD(Math.Cos(_V_Angle), Math.Sin(_V_Angle));
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion

            #region 扩散光点

            public class SpreadSpot
            {
                // 列表
                private static List<SpreadSpot> List = new List<SpreadSpot>(Settings.Count_MaxValue);
                // 设置列表容量
                private static void _SetListCount(int Count)
                {
                    if (Count <= 0)
                    {
                        List.Clear();
                    }
                    else if (List.Count < Count)
                    {
                        for (int i = 0; i < Count - List.Count; i++)
                        {
                            SpreadSpot Element = new SpreadSpot();
                            Element.CreateNew();
                            List.Add(Element);
                        }
                    }
                    else if (List.Count > Count)
                    {
                        List.RemoveRange(Count, List.Count - Count);
                    }
                }
                // 更新列表
                private static void _UpdateList()
                {
                    foreach (SpreadSpot Element in List)
                    {
                        Element.TotalSeconds += DeltaTime.Elapsed.TotalSeconds;
                        Element._UpdateColor();
                    }
                }

                // 时间增量
                private static class DeltaTime
                {
                    private static DateTime _DT0, _DT1;
                    private static bool _LastIsDT0;

                    // 时间增量的最大毫秒数
                    public const double MaxDeltaMS = 200;

                    // 时间间隔
                    public static TimeSpan Elapsed
                    {
                        get
                        {
                            TimeSpan _Elapsed = (_DT0 - _DT1).Duration();

                            if (Math.Abs(_Elapsed.TotalMilliseconds) > MaxDeltaMS)
                            {
                                _Elapsed = TimeSpan.FromMilliseconds(Math.Sign(_Elapsed.TotalMilliseconds) * MaxDeltaMS);
                            }

                            return _Elapsed;
                        }
                    }

                    // 重置
                    public static void Reset()
                    {
                        _DT0 = _DT1 = DateTime.Now;
                        _LastIsDT0 = false;
                    }
                    // 更新
                    public static void Update()
                    {
                        if (_LastIsDT0)
                        {
                            _DT1 = DateTime.Now;
                            _LastIsDT0 = false;
                        }
                        else
                        {
                            _DT0 = DateTime.Now;
                            _LastIsDT0 = true;
                        }
                    }
                }

                // 此动画已启用
                private static bool _Enabled = false;
                public static bool Enabled
                {
                    get
                    {
                        return _Enabled;
                    }
                    set
                    {
                        _Enabled = value;
                        _SetListCount(_Enabled ? Settings.Count : 0);
                        // 重置时间增量
                        DeltaTime.Reset();
                    }
                }

                // 位图
                public static Bitmap Bitmap
                {
                    get
                    {
                        try
                        {
                            // 更新时间增量
                            DeltaTime.Update();
                            // 更新列表
                            _UpdateList();

                            // 初始化位图与绘图画面
                            Bitmap Bmp = new Bitmap(FormBounds.Width, FormBounds.Height);
                            Graphics Grap = Graphics.FromImage(Bmp);

                            if (Animation.Settings.AntiAlias)
                            {
                                Grap.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            foreach (SpreadSpot Element in List)
                            {
                                if (Element.TotalSeconds >= 0)
                                {
                                    Com.PointD Loc = new Com.PointD(Element.CurrentLocation);
                                    double _CR = Element.CurrentRadius;

                                    if (Com.Geometry.PointIsVisibleInRectangle(Loc, new RectangleF((float)(-_CR), (float)(-_CR), (float)(FormBounds.Width + 2 * _CR), (float)(FormBounds.Height + 2 * _CR))) && (Element.CurrentLocation3D.Z >= 0))
                                    // 在位图中绘制列表中的所有图形
                                    {
                                        switch (Settings.GlowMode)
                                        {
                                            case GlowModes.OuterGlow:
                                                {
                                                    RectangleF Bounds_Outer_Outer = Element.Bounds;
                                                    RectangleF Bounds_Outer_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.25F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.25F, Bounds_Outer_Outer.Width * 0.5F, Bounds_Outer_Outer.Height * 0.5F);
                                                    RectangleF Bounds_Inner = new RectangleF(Bounds_Outer_Outer.X + Bounds_Outer_Outer.Width * 0.2F, Bounds_Outer_Outer.Y + Bounds_Outer_Outer.Height * 0.2F, Bounds_Outer_Outer.Width * 0.6F, Bounds_Outer_Outer.Height * 0.6F);

                                                    PathGradientBrush PGB_Outer = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    GraphicsPath Path_Outer = new GraphicsPath();
                                                    Path_Outer.AddEllipse(Bounds_Outer_Inner);
                                                    Path_Outer.AddEllipse(Bounds_Outer_Outer);
                                                    Grap.FillPath(PGB_Outer, Path_Outer);
                                                    PGB_Outer.Dispose();
                                                    Path_Outer.Dispose();

                                                    GraphicsPath Path_Inner = new GraphicsPath();
                                                    Path_Inner.AddEllipse(Bounds_Inner);
                                                    PathGradientBrush PGB_Inner = new PathGradientBrush(Path_Inner)
                                                    {
                                                        CenterColor = Color.FromArgb(Element._Alpha, Color.White),
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.8F, 0.8F)
                                                    };
                                                    Grap.FillPath(PGB_Inner, Path_Inner);
                                                    Path_Inner.Dispose();
                                                    PGB_Inner.Dispose();
                                                }
                                                break;

                                            case GlowModes.InnerGlow:
                                                {
                                                    PathGradientBrush PGB = new PathGradientBrush(Element.Path)
                                                    {
                                                        CenterColor = Element.CurrentColor,
                                                        SurroundColors = new Color[] { Color.Transparent },
                                                        FocusScales = new PointF(0.5F, 0.5F)
                                                    };
                                                    Grap.FillPath(PGB, Element.Path);
                                                    PGB.Dispose();
                                                }
                                                break;

                                            case GlowModes.EvenGlow:
                                                {
                                                    SolidBrush SB = new SolidBrush(Com.ColorManipulation.ShiftLightnessByHSL(Element.CurrentColor, 0.25));
                                                    Grap.FillPath(SB, Element.Path);
                                                    SB.Dispose();
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    // 当图形中心离开位图边界时将其重置
                                    {
                                        Element.Reset();
                                    }
                                }
                            }

                            Grap.Dispose();

                            return Bmp;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }

                // 窗体边界在 LTRB 方向需要保留的空间
                private static readonly Padding HoldSpace = new Padding(15, 15, 15, 15);
                // 此动画需要的窗体边界
                public static Rectangle FormBounds => new Rectangle(ScreenBounds.X + HoldSpace.Left, ScreenBounds.Y + HoldSpace.Top, ScreenBounds.Width - (HoldSpace.Left + HoldSpace.Right), ScreenBounds.Height - (HoldSpace.Top + HoldSpace.Bottom));

                // 设置
                public static class Settings
                {
                    // 半径的取值范围
                    private const double _Radius_MinValue = 1, _Radius_MaxValue = 256;
                    // 最小半径的取值范围与默认值
                    public const double MinRadius_MinValue = _Radius_MinValue, MinRadius_MaxValue = _Radius_MaxValue, MinRadius_DefaultValue = 2;
                    // 最小半径（像素）
                    private static double _MinRadius = MinRadius_DefaultValue;
                    public static double MinRadius
                    {
                        get
                        {
                            return _MinRadius;
                        }
                        set
                        {
                            _MinRadius = Math.Max(MinRadius_MinValue, Math.Min(value, MinRadius_MaxValue));

                            if (_MaxRadius < _MinRadius) _MaxRadius = _MinRadius;
                        }
                    }
                    // 最大半径的取值范围与默认值
                    public const double MaxRadius_MinValue = _Radius_MinValue, MaxRadius_MaxValue = _Radius_MaxValue, MaxRadius_DefaultValue = 16;
                    // 最大半径（像素）
                    private static double _MaxRadius = MaxRadius_DefaultValue;
                    public static double MaxRadius
                    {
                        get
                        {
                            return _MaxRadius;
                        }
                        set
                        {
                            _MaxRadius = Math.Max(MaxRadius_MinValue, Math.Min(value, MaxRadius_MaxValue));

                            if (_MinRadius > _MaxRadius) _MinRadius = _MaxRadius;
                        }
                    }

                    // 数量的取值范围与默认值
                    public const int Count_MinValue = 1, Count_MaxValue = 8192, Count_DefaultValue = 300;
                    // 数量
                    private static int _Count = Count_DefaultValue;
                    public static int Count
                    {
                        get
                        {
                            return _Count;
                        }
                        set
                        {
                            _Count = Math.Max(Count_MinValue, Math.Min(value, Count_MaxValue));

                            if (_Enabled)
                            {
                                _SetListCount(_Count);
                            }
                        }
                    }

                    // 光源位置的 X 坐标与位图宽度的比值的取值范围与默认值
                    public static double SourceX_MinValue = 0, SourceX_MaxValue = 1, SourceX_DefaultValue = 0.5;
                    // 光源位置的 X 坐标与位图宽度的比值
                    private static double _SourceX = SourceX_DefaultValue;
                    public static double SourceX
                    {
                        get
                        {
                            return _SourceX;
                        }
                        set
                        {
                            _SourceX = Math.Max(SourceX_MinValue, Math.Min(value, SourceX_MaxValue));
                        }
                    }

                    // 光源位置的 Y 坐标与位图高度的比值的取值范围与默认值
                    public static double SourceY_MinValue = 0, SourceY_MaxValue = 1, SourceY_DefaultValue = 0.5;
                    // 光源位置的 Y 坐标与位图高度的比值
                    private static double _SourceY = SourceY_DefaultValue;
                    public static double SourceY
                    {
                        get
                        {
                            return _SourceY;
                        }
                        set
                        {
                            _SourceY = Math.Max(SourceY_MinValue, Math.Min(value, SourceY_MaxValue));
                        }
                    }

                    // 光源位置的 Z 坐标与位图对角线长度的比值的取值范围与默认值
                    public static double SourceZ_MinValue = 0.01, SourceZ_MaxValue = 16, SourceZ_DefaultValue = 1;
                    // 光源位置的 Z 坐标与位图对角线长度的比值
                    private static double _SourceZ = SourceZ_DefaultValue;
                    public static double SourceZ
                    {
                        get
                        {
                            return _SourceZ;
                        }
                        set
                        {
                            _SourceZ = Math.Max(SourceZ_MinValue, Math.Min(value, SourceZ_MaxValue));
                        }
                    }

                    // 光源大小与位图对角线长度的比值的取值范围与默认值
                    public static double SourceSize_MinValue = 0, SourceSize_MaxValue = 0.5, SourceSize_DefaultValue = 0.05;
                    // 光源大小与位图对角线长度的比值
                    private static double _SourceSize = SourceSize_DefaultValue;
                    public static double SourceSize
                    {
                        get
                        {
                            return _SourceSize;
                        }
                        set
                        {
                            _SourceSize = Math.Max(SourceSize_MinValue, Math.Min(value, SourceSize_MaxValue));
                        }
                    }

                    // 速度与半径的比值的取值范围与默认值
                    public const double Velocity_MinValue = 0.5, Velocity_MaxValue = 64, Velocity_DefaultValue = 2.5;
                    // 速度与半径的比值
                    private static double _Velocity = Velocity_DefaultValue;
                    public static double Velocity
                    {
                        get
                        {
                            return _Velocity;
                        }
                        set
                        {
                            _Velocity = Math.Max(Velocity_MinValue, Math.Min(value, Velocity_MaxValue));
                        }
                    }

                    // 颜色模式的默认值
                    public static readonly ColorModes ColorMode_DefaultValue = ColorModes.Random;
                    // 颜色模式
                    public static ColorModes ColorMode = ColorMode_DefaultValue;

                    // 颜色模式为随机时是否渐变的默认值
                    public static readonly bool GradientWhenRandom_DefaultValue = true;
                    // 颜色模式为随机时是否渐变
                    public static bool GradientWhenRandom = GradientWhenRandom_DefaultValue;

                    // 颜色渐变速度的取值范围与默认值
                    public static readonly double GradientVelocity_MinValue = 8, GradientVelocity_MaxValue = 512, GradientVelocity_DefaultValue = 64;
                    // 颜色渐变速度
                    public static double GradientVelocity = GradientVelocity_DefaultValue;

                    // 颜色的默认值
                    public static readonly Color Color_DefaultValue = Color.White;
                    // 颜色
                    public static Color Color = Color_DefaultValue;

                    // 着色模式的默认值
                    public static readonly GlowModes GlowMode_DefaultValue = GlowModes.OuterGlow;
                    // 着色模式
                    public static GlowModes GlowMode = GlowMode_DefaultValue;

                    // 将所有设置重置为默认值
                    public static void ResetToDefault()
                    {
                        MinRadius = MinRadius_DefaultValue;
                        MaxRadius = MaxRadius_DefaultValue;
                        Count = Count_DefaultValue;
                        SourceX = SourceX_DefaultValue;
                        SourceY = SourceY_DefaultValue;
                        SourceZ = SourceZ_DefaultValue;
                        SourceSize = SourceSize_DefaultValue;
                        Velocity = Velocity_DefaultValue;
                        ColorMode = ColorMode_DefaultValue;
                        GradientWhenRandom = GradientWhenRandom_DefaultValue;
                        GradientVelocity = GradientVelocity_DefaultValue;
                        Color = Color_DefaultValue;
                        GlowMode = GlowMode_DefaultValue;
                    }
                }

                // 初始化到当前时刻的总秒数
                private double TotalSeconds;

                // 主显示的边界的对角线长度（像素）
                private double _ScreenDiag => new Com.PointD(ScreenBounds.Size).VectorModule;
                // 主显示的边界的中心位置（像素，像素）
                private Com.PointD _ScreenCenter => new Com.PointD(ScreenBounds.Size) / 2;

                // 光源位置（像素，像素，像素）
                private Com.PointD3D _SourceLocation => new Com.PointD3D(Settings.SourceX * ScreenBounds.Width, Settings.SourceY * ScreenBounds.Height, Settings.SourceZ * _ScreenDiag);

                // 当前颜色的 Alpha 通道
                private int _Alpha
                {
                    get
                    {
                        double _R = 0;
                        double _W = FormBounds.Width / 2, _H = FormBounds.Height / 2;
                        PointF _CL = CurrentLocation;
                        double _X = Math.Abs(_CL.X - _W), _Y = Math.Abs(_CL.Y - _H);

                        if (_X * _H == _Y * _W)
                        {
                            _R = Math.Sqrt(_X * _X + _Y * _Y);
                        }
                        else if (_X * _H > _Y * _W)
                        {
                            _R = _X * Math.Sqrt(1 + (_H * _H) / (_W * _W));
                        }
                        else
                        {
                            _R = _Y * Math.Sqrt(1 + (_W * _W) / (_H * _H));
                        }

                        return Math.Max(0, Math.Min((int)(255 * (1 - _R / Math.Sqrt(_W * _W + _H * _H)) * (1 - Math.Pow(CurrentLocation3D.Z / _SourceLocation.Z, 2))), 255));
                    }
                }
                // 最小半径与最大半径对应的颜色明度调整
                private const double _LShift_MinRadius = 0, _LShift_MaxRadius = 0.75;
                // 当前颜色的颜色明度调整
                private double _LShift
                {
                    get
                    {
                        double __LShift;

                        if (Settings.MaxRadius > Settings.MinRadius)
                        {
                            __LShift = _LShift_MinRadius + (_LShift_MaxRadius - _LShift_MinRadius) * (CurrentRadius - Settings.MinRadius) / (Settings.MaxRadius - Settings.MinRadius);
                        }
                        else
                        {
                            __LShift = (_LShift_MinRadius + _LShift_MaxRadius) / 2;
                        }

                        return Math.Max(0, Math.Min(__LShift, 1));
                    }
                }
                // 初始颜色
                private Color InitialColor;
                // 目标颜色
                private Color _DestinationColor;
                // 此前所有颜色持续时间的总秒数
                private double _TotalSecondsOfOldColors;
                // 当前颜色参数
                private Color _CurrentColor;
                // 当前颜色
                private Color CurrentColor => Color.FromArgb(_Alpha, Com.ColorManipulation.ShiftLightnessByHSL(_CurrentColor, _LShift));
                // 更新当前颜色
                private void _UpdateColor()
                {
                    if (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom)
                    {
                        Com.PointD3D Cr3D_From = new Com.PointD3D(InitialColor.R, InitialColor.G, InitialColor.B);
                        Com.PointD3D Cr3D_To = new Com.PointD3D(_DestinationColor.R, _DestinationColor.G, _DestinationColor.B);
                        Com.PointD3D Cr3D_Dist = Cr3D_To - Cr3D_From;
                        double DistMod = Cr3D_Dist.VectorModule;
                        double DistNow = (TotalSeconds - _TotalSecondsOfOldColors) * Settings.GradientVelocity;

                        if (DistNow >= DistMod)
                        {
                            DistNow = DistMod;

                            InitialColor = _DestinationColor;
                            _DestinationColor = Com.ColorManipulation.GetRandomColor();
                            _TotalSecondsOfOldColors = TotalSeconds;
                        }

                        Com.PointD3D Cr3D_Now = Com.PointD3D.Max(new Com.PointD3D(0, 0, 0), Com.PointD3D.Min(Cr3D_From + DistNow * Cr3D_Dist.VectorNormalize, new Com.PointD3D(255, 255, 255)));
                        _CurrentColor = Color.FromArgb((int)Cr3D_Now.X, (int)Cr3D_Now.Y, (int)Cr3D_Now.Z);
                    }
                    else
                    {
                        _CurrentColor = InitialColor;
                    }
                }

                // 初始半径参数
                private double _InitialRadius;
                // 初始半径（像素）
                private double InitialRadius => (1 - _InitialRadius) * Settings.MinRadius + _InitialRadius * Settings.MaxRadius;
                // 当前半径（像素）
                private double CurrentRadius
                {
                    get
                    {
                        double _IR = InitialRadius, __SD = _ScreenDiag;
                        return Math.Max(1, Math.Min(__SD * _IR / (__SD + CurrentLocation3D.Z), _IR));
                    }
                }

                // 速度方向角的 XY 平面内分量参数
                private double _Angle_XY;
                // 速度方向角的 XY 平面内分量（弧度）
                private double Angle_XY => _Angle_XY * (2 * Math.PI);
                // 速度方向角的 XY 平面外分量参数
                private double _Angle_Z;
                // 速度方向角的 XY 平面外分量（弧度）
                private double Angle_Z => _Angle_Z * (0.5 * Math.PI);

                // 速度大小（像素/秒）
                private double _Velocity => Settings.Velocity * InitialRadius;
                // 速度（像素/秒，像素/秒，像素/秒）
                private Com.PointD3D Velocity
                {
                    get
                    {
                        double _AXY = Angle_XY, _AZ = Angle_Z;
                        return new Com.PointD3D(_Velocity * Math.Cos(_AZ) * Math.Cos(_AXY), _Velocity * Math.Cos(_AZ) * Math.Sin(_AXY), _Velocity * Math.Sin(_AZ));
                    }
                }

                // 初始中心位置参数
                private double _InitialLocation3D;
                // 初始中心位置（像素，像素，像素）
                private Com.PointD3D InitialLocation3D
                {
                    get
                    {
                        double __IL = _InitialLocation3D, _SX = _SourceLocation.X, _SY = _SourceLocation.Y, _SZ = _SourceLocation.Z;
                        return new Com.PointD3D((1 - __IL) * (_SX - Settings.SourceSize / 2 - InitialRadius) + __IL * (_SX + Settings.SourceSize / 2 + InitialRadius), (1 - __IL) * (_SY - Settings.SourceSize / 2 - InitialRadius) + __IL * (_SY + Settings.SourceSize / 2 + InitialRadius), _SZ);
                    }
                }
                // 当前中心位置（像素，像素，像素）
                private Com.PointD3D CurrentLocation3D
                {
                    get
                    {
                        Com.PointD3D _IL = InitialLocation3D, _V = Velocity;
                        return new Com.PointD3D(_IL.X + _V.X * TotalSeconds, _IL.Y + _V.Y * TotalSeconds, _SourceLocation.Z - _V.Z * TotalSeconds);
                    }
                }
                // 当前中心位置（像素，像素）
                private PointF CurrentLocation
                {
                    get
                    {
                        Com.PointD _SC = _ScreenCenter;
                        return CurrentLocation3D.ProjectToXY(new Com.PointD3D(_SC.X, _SC.Y, -2), _ScreenDiag).ToPointF();
                    }
                }

                // 边界
                private RectangleF Bounds
                {
                    get
                    {
                        PointF _CL = CurrentLocation;
                        double _CR = CurrentRadius;
                        return new RectangleF((float)(_CL.X - _CR), (float)(_CL.Y - _CR), (float)(2 * _CR), (float)(2 * _CR));
                    }
                }
                // 路径
                private GraphicsPath Path
                {
                    get
                    {
                        GraphicsPath GP = new GraphicsPath();
                        GP.AddEllipse(Bounds);
                        return GP;
                    }
                }

                private static Random Rand = new Random();
                // 初始化
                private void Initialize()
                {
                    TotalSeconds = 0;
                    InitialColor = (Settings.ColorMode == ColorModes.Custom ? Settings.Color : Com.ColorManipulation.GetRandomColor());
                    _DestinationColor = (Settings.ColorMode == ColorModes.Random && Settings.GradientWhenRandom ? Com.ColorManipulation.GetRandomColor() : Color.Empty);
                    _TotalSecondsOfOldColors = 0;
                    _InitialRadius = Rand.NextDouble();
                    _Angle_XY = Rand.NextDouble();
                    _Angle_Z = Rand.NextDouble();
                    _InitialLocation3D = Rand.NextDouble();
                }
                // 新建
                private void CreateNew()
                {
                    Initialize();
                    // 使同时产生的大量图形能够更均匀的先后出现在位图中，而不是同时出现
                    TotalSeconds -= (Rand.NextDouble() * (FormBounds.Height / _Velocity));
                }
                // 重置
                private void Reset()
                {
                    Initialize();
                }
            }

            #endregion
        }

        #endregion

    }
}