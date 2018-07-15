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
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormApp
{
    public static class Configuration
    {
        #region 配置设置

        // 兼容的版本列表，用于从最新的兼容版本迁移配置设置
        public static readonly List<Version> OldVersionList = new List<Version>
        {
            new Version(1, 0, 1704, 0),
            new Version(1, 0, 1704, 357),
            new Version(1, 0, 1704, 470),
            new Version(1, 0, 1711, 0),
            new Version(1, 0, 1712, 0),/*
            new Version(1, 0, 1712, 127)*/
        };

        // 根目录：此产品
        public static readonly string RootDir_Product = Environment.SystemDirectory.Substring(0, 1) + @":\ProgramData\AppConfig\Livedesk";
        // 根目录：当前版本
        public static readonly string RootDir_CurrentVersion = RootDir_Product + "\\" + Application.ProductVersion;

        // 配置文件所在目录
        public static readonly string ConfigFileDir = RootDir_CurrentVersion + @"\Config";
        // 动画设置文件路径：圆形光点
        public static readonly string AnimationSettingsFilePath_LightSpot = ConfigFileDir + @"\LightSpot.cfg";
        // 动画设置文件路径：三角形碎片
        public static readonly string AnimationSettingsFilePath_TrianglePiece = ConfigFileDir + @"\TrianglePiece.cfg";
        // 动画设置文件路径：光芒
        public static readonly string AnimationSettingsFilePath_Shine = ConfigFileDir + @"\Shine.cfg";
        // 动画设置文件路径：流星
        public static readonly string AnimationSettingsFilePath_Meteor = ConfigFileDir + @"\Meteor.cfg";
        // 动画设置文件路径：雪
        public static readonly string AnimationSettingsFilePath_Snow = ConfigFileDir + @"\Snow.cfg";
        // 动画设置文件路径：引力粒子
        public static readonly string AnimationSettingsFilePath_GravityParticle = ConfigFileDir + @"\GravityParticle.cfg";
        // 动画设置文件路径：引力网
        public static readonly string AnimationSettingsFilePath_GravityGrid = ConfigFileDir + @"\GravityGrid.cfg";
        // 动画设置文件路径：扩散光点
        public static readonly string AnimationSettingsFilePath_SpreadSpot = ConfigFileDir + @"\SpreadSpot.cfg";
        // 动画类型、动画全局设置与通用设置文件路径
        public static readonly string CommonSettingsFilePath = ConfigFileDir + @"\Settings.cfg";

        // 从兼容的版本迁移最新的配置文件
        public static void TransConfig()
        {
            if (!Directory.Exists(RootDir_CurrentVersion))
            {
                if (OldVersionList.Count > 0)
                {
                    List<Version> OldVersionList_Copy = new List<Version>(OldVersionList);
                    List<Version> OldVersionList_Sorted = new List<Version>(OldVersionList_Copy.Count);

                    while (OldVersionList_Copy.Count > 0)
                    {
                        Version NewestVersion = OldVersionList_Copy[0];

                        foreach (Version Ver in OldVersionList_Copy)
                        {
                            if (NewestVersion <= Ver)
                            {
                                NewestVersion = Ver;
                            }
                        }

                        OldVersionList_Sorted.Add(NewestVersion);
                        OldVersionList_Copy.Remove(NewestVersion);
                    }

                    for (int i = 0; i < OldVersionList_Sorted.Count; i++)
                    {
                        string Dir = RootDir_Product + "\\" + OldVersionList_Sorted[i];

                        if (Directory.Exists(Dir))
                        {
                            try
                            {
                                Com.IO.CopyFolder(Dir, RootDir_CurrentVersion, true, true, true);

                                break;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

        // 删除所有兼容的版本的配置文件
        public static void DelOldConfig()
        {
            if (OldVersionList.Count > 0)
            {
                foreach (Version Ver in OldVersionList)
                {
                    string Dir = RootDir_Product + "\\" + Ver;

                    if (Directory.Exists(Dir))
                    {
                        try
                        {
                            Directory.Delete(Dir, true);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
        }

        // 加载配置
        public static void LoadConfig()
        {
            Regex RegexInt = new Regex(@"[^0-9\-]");
            Regex RegexUint = new Regex(@"[^0-9]");
            Regex RegexFloat = new Regex(@"[^0-9\-\.]");

            // 动画设置：圆形光点
            try
            {
                if (File.Exists(AnimationSettingsFilePath_LightSpot) && new FileInfo(AnimationSettingsFilePath_LightSpot).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_LightSpot, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinRadius>", "</MinRadius>", false, false), string.Empty);
                        double MinRadius = Convert.ToDouble(SubStr);
                        if (MinRadius >= Animation.Animations.LightSpot.Settings.MinRadius_MinValue && MinRadius <= Animation.Animations.LightSpot.Settings.MinRadius_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.MinRadius = MinRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxRadius>", "</MaxRadius>", false, false), string.Empty);
                        double MaxRadius = Convert.ToDouble(SubStr);
                        if (MaxRadius >= Animation.Animations.LightSpot.Settings.MaxRadius_MinValue && MaxRadius <= Animation.Animations.LightSpot.Settings.MaxRadius_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.MaxRadius = MaxRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.LightSpot.Settings.Count_MinValue && Count <= Animation.Animations.LightSpot.Settings.Count_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Amplitude>", "</Amplitude>", false, false), string.Empty);
                        double Amplitude = Convert.ToDouble(SubStr);
                        if (Amplitude >= Animation.Animations.LightSpot.Settings.Amplitude_MinValue && Amplitude <= Animation.Animations.LightSpot.Settings.Amplitude_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.Amplitude = Amplitude;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<WaveLength>", "</WaveLength>", false, false), string.Empty);
                        double WaveLength = Convert.ToDouble(SubStr);
                        if (WaveLength >= Animation.Animations.LightSpot.Settings.WaveLength_MinValue && WaveLength <= Animation.Animations.LightSpot.Settings.WaveLength_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.WaveLength = WaveLength;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<WaveVelocity>", "</WaveVelocity>", false, false), string.Empty);
                        double WaveVelocity = Convert.ToDouble(SubStr);
                        if (WaveVelocity >= Animation.Animations.LightSpot.Settings.WaveVelocity_MinValue && WaveVelocity <= Animation.Animations.LightSpot.Settings.WaveVelocity_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.WaveVelocity = WaveVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.LightSpot.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.LightSpot.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.LightSpot.Settings.GradientWhenRandom = !Animation.Animations.LightSpot.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.LightSpot.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.LightSpot.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.LightSpot.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.LightSpot.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.LightSpot.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：三角形碎片
            try
            {
                if (File.Exists(AnimationSettingsFilePath_TrianglePiece) && new FileInfo(AnimationSettingsFilePath_TrianglePiece).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_TrianglePiece, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinRadius>", "</MinRadius>", false, false), string.Empty);
                        double MinRadius = Convert.ToDouble(SubStr);
                        if (MinRadius >= Animation.Animations.TrianglePiece.Settings.MinRadius_MinValue && MinRadius <= Animation.Animations.TrianglePiece.Settings.MinRadius_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.MinRadius = MinRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxRadius>", "</MaxRadius>", false, false), string.Empty);
                        double MaxRadius = Convert.ToDouble(SubStr);
                        if (MaxRadius >= Animation.Animations.TrianglePiece.Settings.MaxRadius_MinValue && MaxRadius <= Animation.Animations.TrianglePiece.Settings.MaxRadius_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.MaxRadius = MaxRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.TrianglePiece.Settings.Count_MinValue && Count <= Animation.Animations.TrianglePiece.Settings.Count_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Amplitude>", "</Amplitude>", false, false), string.Empty);
                        double Amplitude = Convert.ToDouble(SubStr);
                        if (Amplitude >= Animation.Animations.TrianglePiece.Settings.Amplitude_MinValue && Amplitude <= Animation.Animations.TrianglePiece.Settings.Amplitude_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.Amplitude = Amplitude;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<WaveLength>", "</WaveLength>", false, false), string.Empty);
                        double WaveLength = Convert.ToDouble(SubStr);
                        if (WaveLength >= Animation.Animations.TrianglePiece.Settings.WaveLength_MinValue && WaveLength <= Animation.Animations.TrianglePiece.Settings.WaveLength_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.WaveLength = WaveLength;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<WaveVelocity>", "</WaveVelocity>", false, false), string.Empty);
                        double WaveVelocity = Convert.ToDouble(SubStr);
                        if (WaveVelocity >= Animation.Animations.TrianglePiece.Settings.WaveVelocity_MinValue && WaveVelocity <= Animation.Animations.TrianglePiece.Settings.WaveVelocity_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.WaveVelocity = WaveVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.TrianglePiece.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.TrianglePiece.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.TrianglePiece.Settings.GradientWhenRandom = !Animation.Animations.TrianglePiece.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.TrianglePiece.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.TrianglePiece.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.TrianglePiece.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.TrianglePiece.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.TrianglePiece.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：光芒
            try
            {
                if (File.Exists(AnimationSettingsFilePath_Shine) && new FileInfo(AnimationSettingsFilePath_Shine).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_Shine, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinRadius>", "</MinRadius>", false, false), string.Empty);
                        double MinRadius = Convert.ToDouble(SubStr);
                        if (MinRadius >= Animation.Animations.Shine.Settings.MinRadius_MinValue && MinRadius <= Animation.Animations.Shine.Settings.MinRadius_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.MinRadius = MinRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxRadius>", "</MaxRadius>", false, false), string.Empty);
                        double MaxRadius = Convert.ToDouble(SubStr);
                        if (MaxRadius >= Animation.Animations.Shine.Settings.MaxRadius_MinValue && MaxRadius <= Animation.Animations.Shine.Settings.MaxRadius_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.MaxRadius = MaxRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.Shine.Settings.Count_MinValue && Count <= Animation.Animations.Shine.Settings.Count_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Displacement>", "</Displacement>", false, false), string.Empty);
                        double Displacement = Convert.ToDouble(SubStr);
                        if (Displacement >= Animation.Animations.Shine.Settings.Displacement_MinValue && Displacement <= Animation.Animations.Shine.Settings.Displacement_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.Displacement = Displacement;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinPeriod>", "</MinPeriod>", false, false), string.Empty);
                        double MinPeriod = Convert.ToDouble(SubStr);
                        if (MinPeriod >= Animation.Animations.Shine.Settings.MinPeriod_MinValue && MinPeriod <= Animation.Animations.Shine.Settings.MinPeriod_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.MinPeriod = MinPeriod;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxPeriod>", "</MaxPeriod>", false, false), string.Empty);
                        double MaxPeriod = Convert.ToDouble(SubStr);
                        if (MaxPeriod >= Animation.Animations.Shine.Settings.MaxPeriod_MinValue && MaxPeriod <= Animation.Animations.Shine.Settings.MaxPeriod_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.MaxPeriod = MaxPeriod;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.Shine.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.Shine.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.Shine.Settings.GradientWhenRandom = !Animation.Animations.Shine.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.Shine.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.Shine.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.Shine.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.Shine.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.Shine.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：流星
            try
            {
                if (File.Exists(AnimationSettingsFilePath_Meteor) && new FileInfo(AnimationSettingsFilePath_Meteor).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_Meteor, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinRadius>", "</MinRadius>", false, false), string.Empty);
                        double MinRadius = Convert.ToDouble(SubStr);
                        if (MinRadius >= Animation.Animations.Meteor.Settings.MinRadius_MinValue && MinRadius <= Animation.Animations.Meteor.Settings.MinRadius_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.MinRadius = MinRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxRadius>", "</MaxRadius>", false, false), string.Empty);
                        double MaxRadius = Convert.ToDouble(SubStr);
                        if (MaxRadius >= Animation.Animations.Meteor.Settings.MaxRadius_MinValue && MaxRadius <= Animation.Animations.Meteor.Settings.MaxRadius_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.MaxRadius = MaxRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Length>", "</Length>", false, false), string.Empty);
                        double Length = Convert.ToDouble(SubStr);
                        if (Length >= Animation.Animations.Meteor.Settings.Length_MinValue && Length <= Animation.Animations.Meteor.Settings.Length_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.Length = Length;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.Meteor.Settings.Count_MinValue && Count <= Animation.Animations.Meteor.Settings.Count_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Velocity>", "</Velocity>", false, false), string.Empty);
                        double Velocity = Convert.ToDouble(SubStr);
                        if (Velocity >= Animation.Animations.Meteor.Settings.Velocity_MinValue && Velocity <= Animation.Animations.Meteor.Settings.Velocity_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.Velocity = Velocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Angle>", "</Angle>", false, false), string.Empty);
                        double Angle = Convert.ToDouble(SubStr);
                        if (Angle >= Animation.Animations.Meteor.Settings.Angle_MinValue && Angle <= Animation.Animations.Meteor.Settings.Angle_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.Angle = Angle;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GravitationalAcceleration>", "</GravitationalAcceleration>", false, false), string.Empty);
                        double GravitationalAcceleration = Convert.ToDouble(SubStr);
                        if (GravitationalAcceleration >= Animation.Animations.Meteor.Settings.GravitationalAcceleration_MinValue && GravitationalAcceleration <= Animation.Animations.Meteor.Settings.GravitationalAcceleration_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.GravitationalAcceleration = GravitationalAcceleration;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.Meteor.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.Meteor.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.Meteor.Settings.GradientWhenRandom = !Animation.Animations.Meteor.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.Meteor.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.Meteor.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.Meteor.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.Meteor.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.Meteor.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：雪
            try
            {
                if (File.Exists(AnimationSettingsFilePath_Snow) && new FileInfo(AnimationSettingsFilePath_Snow).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_Snow, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinRadius>", "</MinRadius>", false, false), string.Empty);
                        double MinRadius = Convert.ToDouble(SubStr);
                        if (MinRadius >= Animation.Animations.Snow.Settings.MinRadius_MinValue && MinRadius <= Animation.Animations.Snow.Settings.MinRadius_MaxValue)
                        {
                            Animation.Animations.Snow.Settings.MinRadius = MinRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxRadius>", "</MaxRadius>", false, false), string.Empty);
                        double MaxRadius = Convert.ToDouble(SubStr);
                        if (MaxRadius >= Animation.Animations.Snow.Settings.MaxRadius_MinValue && MaxRadius <= Animation.Animations.Snow.Settings.MaxRadius_MaxValue)
                        {
                            Animation.Animations.Snow.Settings.MaxRadius = MaxRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.Snow.Settings.Count_MinValue && Count <= Animation.Animations.Snow.Settings.Count_MaxValue)
                        {
                            Animation.Animations.Snow.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Velocity>", "</Velocity>", false, false), string.Empty);
                        double Velocity = Convert.ToDouble(SubStr);
                        if (Velocity >= Animation.Animations.Snow.Settings.Velocity_MinValue && Velocity <= Animation.Animations.Snow.Settings.Velocity_MaxValue)
                        {
                            Animation.Animations.Snow.Settings.Velocity = Velocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.Snow.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.Snow.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.Snow.Settings.GradientWhenRandom = !Animation.Animations.Snow.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.Snow.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.Snow.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.Snow.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.Snow.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.Snow.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：引力粒子
            try
            {
                if (File.Exists(AnimationSettingsFilePath_GravityParticle) && new FileInfo(AnimationSettingsFilePath_GravityParticle).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_GravityParticle, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinMass>", "</MinMass>", false, false), string.Empty);
                        double MinMass = Convert.ToDouble(SubStr);
                        if (MinMass >= Animation.Animations.GravityParticle.Settings.MinMass_MinValue && MinMass <= Animation.Animations.GravityParticle.Settings.MinMass_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.MinMass = MinMass;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxMass>", "</MaxMass>", false, false), string.Empty);
                        double MaxMass = Convert.ToDouble(SubStr);
                        if (MaxMass >= Animation.Animations.GravityParticle.Settings.MaxMass_MinValue && MaxMass <= Animation.Animations.GravityParticle.Settings.MaxMass_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.MaxMass = MaxMass;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.GravityParticle.Settings.Count_MinValue && Count <= Animation.Animations.GravityParticle.Settings.Count_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<CursorMass>", "</CursorMass>", false, false), string.Empty);
                        double CursorMass = Convert.ToDouble(SubStr);
                        if (CursorMass >= Animation.Animations.GravityParticle.Settings.CursorMass_MinValue && CursorMass <= Animation.Animations.GravityParticle.Settings.CursorMass_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.CursorMass = CursorMass;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GravityConstant>", "</GravityConstant>", false, false), string.Empty);
                        double GravityConstant = Convert.ToDouble(SubStr);
                        if (GravityConstant >= Animation.Animations.GravityParticle.Settings.GravityConstant_MinValue && GravityConstant <= Animation.Animations.GravityParticle.Settings.GravityConstant_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.GravityConstant = GravityConstant;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<ElasticRestitutionCoefficient>", "</ElasticRestitutionCoefficient>", false, false), string.Empty);
                        double ElasticRestitutionCoefficient = Convert.ToDouble(SubStr);
                        if (ElasticRestitutionCoefficient >= Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient_MinValue && ElasticRestitutionCoefficient <= Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient = ElasticRestitutionCoefficient;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.GravityParticle.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.GravityParticle.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.GravityParticle.Settings.GradientWhenRandom = !Animation.Animations.GravityParticle.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.GravityParticle.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.GravityParticle.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.GravityParticle.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.GravityParticle.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.GravityParticle.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：引力网
            try
            {
                if (File.Exists(AnimationSettingsFilePath_GravityGrid) && new FileInfo(AnimationSettingsFilePath_GravityGrid).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_GravityGrid, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Radius>", "</Radius>", false, false), string.Empty);
                        double Radius = Convert.ToDouble(SubStr);
                        if (Radius >= Animation.Animations.GravityGrid.Settings.Radius_MinValue && Radius <= Animation.Animations.GravityGrid.Settings.Radius_MaxValue)
                        {
                            Animation.Animations.GravityGrid.Settings.Radius = Radius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.GravityGrid.Settings.Count_MinValue && Count <= Animation.Animations.GravityGrid.Settings.Count_MaxValue)
                        {
                            Animation.Animations.GravityGrid.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<LineWidth>", "</LineWidth>", false, false), string.Empty);
                        float LineWidth = Convert.ToSingle(SubStr);
                        if (LineWidth >= Animation.Animations.GravityGrid.Settings.LineWidth_MinValue && LineWidth <= Animation.Animations.GravityGrid.Settings.LineWidth_MaxValue)
                        {
                            Animation.Animations.GravityGrid.Settings.LineWidth = LineWidth;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<CursorMass>", "</CursorMass>", false, false), string.Empty);
                        double CursorMass = Convert.ToDouble(SubStr);
                        if (CursorMass >= Animation.Animations.GravityGrid.Settings.CursorMass_MinValue && CursorMass <= Animation.Animations.GravityGrid.Settings.CursorMass_MaxValue)
                        {
                            Animation.Animations.GravityGrid.Settings.CursorMass = CursorMass;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<CursorRepulsionRadius>", "</CursorRepulsionRadius>", false, false), string.Empty);
                        int CursorRepulsionRadius = Convert.ToInt32(SubStr);
                        if (CursorRepulsionRadius >= Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius_MinValue && CursorRepulsionRadius <= Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius_MaxValue)
                        {
                            Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius = CursorRepulsionRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.GravityGrid.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.GravityGrid.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.GravityGrid.Settings.GradientWhenRandom = !Animation.Animations.GravityGrid.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.GravityGrid.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.GravityGrid.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.GravityGrid.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.GravityGrid.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.GravityGrid.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画设置：扩散光点
            try
            {
                if (File.Exists(AnimationSettingsFilePath_SpreadSpot) && new FileInfo(AnimationSettingsFilePath_SpreadSpot).Length > 0)
                {
                    StreamReader Read = new StreamReader(AnimationSettingsFilePath_SpreadSpot, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MinRadius>", "</MinRadius>", false, false), string.Empty);
                        double MinRadius = Convert.ToDouble(SubStr);
                        if (MinRadius >= Animation.Animations.SpreadSpot.Settings.MinRadius_MinValue && MinRadius <= Animation.Animations.SpreadSpot.Settings.MinRadius_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.MinRadius = MinRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<MaxRadius>", "</MaxRadius>", false, false), string.Empty);
                        double MaxRadius = Convert.ToDouble(SubStr);
                        if (MaxRadius >= Animation.Animations.SpreadSpot.Settings.MaxRadius_MinValue && MaxRadius <= Animation.Animations.SpreadSpot.Settings.MaxRadius_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.MaxRadius = MaxRadius;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<Count>", "</Count>", false, false), string.Empty);
                        int Count = Convert.ToInt32(SubStr);
                        if (Count >= Animation.Animations.SpreadSpot.Settings.Count_MinValue && Count <= Animation.Animations.SpreadSpot.Settings.Count_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.Count = Count;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<SourceX>", "</SourceX>", false, false), string.Empty);
                        float SourceX = Convert.ToSingle(SubStr);
                        if (SourceX >= Animation.Animations.SpreadSpot.Settings.SourceX_MinValue && SourceX <= Animation.Animations.SpreadSpot.Settings.SourceX_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.SourceX = SourceX;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<SourceY>", "</SourceY>", false, false), string.Empty);
                        float SourceY = Convert.ToSingle(SubStr);
                        if (SourceY >= Animation.Animations.SpreadSpot.Settings.SourceY_MinValue && SourceY <= Animation.Animations.SpreadSpot.Settings.SourceY_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.SourceY = SourceY;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<SourceZ>", "</SourceZ>", false, false), string.Empty);
                        float SourceZ = Convert.ToSingle(SubStr);
                        if (SourceZ >= Animation.Animations.SpreadSpot.Settings.SourceZ_MinValue && SourceZ <= Animation.Animations.SpreadSpot.Settings.SourceZ_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.SourceZ = SourceZ;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<SourceSize>", "</SourceSize>", false, false), string.Empty);
                        float SourceSize = Convert.ToSingle(SubStr);
                        if (SourceSize >= Animation.Animations.SpreadSpot.Settings.SourceSize_MinValue && SourceSize <= Animation.Animations.SpreadSpot.Settings.SourceSize_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.SourceSize = SourceSize;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Velocity>", "</Velocity>", false, false), string.Empty);
                        float Velocity = Convert.ToSingle(SubStr);
                        if (Velocity >= Animation.Animations.SpreadSpot.Settings.Velocity_MinValue && Velocity <= Animation.Animations.SpreadSpot.Settings.Velocity_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.Velocity = Velocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<ColorMode>", "</ColorMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.ColorModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.SpreadSpot.Settings.ColorMode = (Animation.ColorModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<GradientWhenRandom>", "</GradientWhenRandom>", false, false).Contains((!Animation.Animations.SpreadSpot.Settings.GradientWhenRandom).ToString()))
                    {
                        Animation.Animations.SpreadSpot.Settings.GradientWhenRandom = !Animation.Animations.SpreadSpot.Settings.GradientWhenRandom;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<GradientVelocity>", "</GradientVelocity>", false, false), string.Empty);
                        double GradientVelocity = Convert.ToDouble(SubStr);
                        if (GradientVelocity >= Animation.Animations.SpreadSpot.Settings.GradientVelocity_MinValue && GradientVelocity <= Animation.Animations.SpreadSpot.Settings.GradientVelocity_MaxValue)
                        {
                            Animation.Animations.SpreadSpot.Settings.GradientVelocity = GradientVelocity;
                        }
                    }
                    catch { }

                    try
                    {
                        string SubStr = RegexInt.Replace(Com.Text.GetIntervalString(Cfg, "<Color>", "</Color>", false, false), string.Empty);
                        int Argb = Convert.ToInt32(SubStr);
                        Animation.Animations.SpreadSpot.Settings.Color = Color.FromArgb(Argb);
                    }
                    catch { }

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<GlowMode>", "</GlowMode>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.GlowModes)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Animations.SpreadSpot.Settings.GlowMode = (Animation.GlowModes)Obj;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 动画类型、动画全局设置与通用设置
            try
            {
                if (File.Exists(CommonSettingsFilePath) && new FileInfo(CommonSettingsFilePath).Length > 0)
                {
                    StreamReader Read = new StreamReader(CommonSettingsFilePath, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<AnimationType>", "</AnimationType>", false, false);
                        foreach (object Obj in Enum.GetValues(typeof(Animation.Types)))
                        {
                            if (SubStr.Trim().ToUpper() == Obj.ToString().ToUpper())
                            {
                                Animation.Type = (Animation.Types)Obj;
                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<AntiAlias>", "</AntiAlias>", false, false).Contains((!Animation.Settings.AntiAlias).ToString()))
                    {
                        Animation.Settings.AntiAlias = !Animation.Settings.AntiAlias;
                    }

                    if (Com.Text.GetIntervalString(Cfg, "<LimitFPS>", "</LimitFPS>", false, false).Contains((!Animation.Settings.LimitFPS).ToString()))
                    {
                        Animation.Settings.LimitFPS = !Animation.Settings.LimitFPS;
                    }

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<FPS>", "</FPS>", false, false), string.Empty);
                        double FPS = Convert.ToDouble(SubStr);
                        if (FPS >= Animation.Settings.FPS_MinValue && FPS <= Animation.Settings.FPS_MaxValue)
                        {
                            Animation.Settings.FPS = FPS;
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // 如果没有选择和保存过动画类型，那么将其设置为第一个枚举
            if (Animation.Type == Animation.Types.NULL)
            {
                Animation.Type = (Animation.Types)0;
            }
        }

        // 保存配置
        public static void SaveConfig()
        {
            try
            {
                if (!Directory.Exists(ConfigFileDir))
                {
                    Directory.CreateDirectory(ConfigFileDir);
                }

                // 动画设置：圆形光点
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinRadius>" + Animation.Animations.LightSpot.Settings.MinRadius + "</MinRadius>";
                    Cfg += "<MaxRadius>" + Animation.Animations.LightSpot.Settings.MaxRadius + "</MaxRadius>";
                    Cfg += "<Count>" + Animation.Animations.LightSpot.Settings.Count + "</Count>";
                    Cfg += "<Amplitude>" + Animation.Animations.LightSpot.Settings.Amplitude + "</Amplitude>";
                    Cfg += "<WaveLength>" + Animation.Animations.LightSpot.Settings.WaveLength + "</WaveLength>";
                    Cfg += "<WaveVelocity>" + Animation.Animations.LightSpot.Settings.WaveVelocity + "</WaveVelocity>";
                    Cfg += "<ColorMode>" + Animation.Animations.LightSpot.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.LightSpot.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.LightSpot.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.LightSpot.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.LightSpot.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_LightSpot) && new FileInfo(AnimationSettingsFilePath_LightSpot).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_LightSpot).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_LightSpot, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：三角形碎片
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinRadius>" + Animation.Animations.TrianglePiece.Settings.MinRadius + "</MinRadius>";
                    Cfg += "<MaxRadius>" + Animation.Animations.TrianglePiece.Settings.MaxRadius + "</MaxRadius>";
                    Cfg += "<Count>" + Animation.Animations.TrianglePiece.Settings.Count + "</Count>";
                    Cfg += "<Amplitude>" + Animation.Animations.TrianglePiece.Settings.Amplitude + "</Amplitude>";
                    Cfg += "<WaveLength>" + Animation.Animations.TrianglePiece.Settings.WaveLength + "</WaveLength>";
                    Cfg += "<WaveVelocity>" + Animation.Animations.TrianglePiece.Settings.WaveVelocity + "</WaveVelocity>";
                    Cfg += "<ColorMode>" + Animation.Animations.TrianglePiece.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.TrianglePiece.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.TrianglePiece.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.TrianglePiece.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.TrianglePiece.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_TrianglePiece) && new FileInfo(AnimationSettingsFilePath_TrianglePiece).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_TrianglePiece).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_TrianglePiece, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：光芒
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinRadius>" + Animation.Animations.Shine.Settings.MinRadius + "</MinRadius>";
                    Cfg += "<MaxRadius>" + Animation.Animations.Shine.Settings.MaxRadius + "</MaxRadius>";
                    Cfg += "<Count>" + Animation.Animations.Shine.Settings.Count + "</Count>";
                    Cfg += "<Displacement>" + Animation.Animations.Shine.Settings.Displacement + "</Displacement>";
                    Cfg += "<MinPeriod>" + Animation.Animations.Shine.Settings.MinPeriod + "</MinPeriod>";
                    Cfg += "<MaxPeriod>" + Animation.Animations.Shine.Settings.MaxPeriod + "</MaxPeriod>";
                    Cfg += "<ColorMode>" + Animation.Animations.Shine.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.Shine.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.Shine.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.Shine.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.Shine.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_Shine) && new FileInfo(AnimationSettingsFilePath_Shine).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_Shine).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_Shine, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：流星
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinRadius>" + Animation.Animations.Meteor.Settings.MinRadius + "</MinRadius>";
                    Cfg += "<MaxRadius>" + Animation.Animations.Meteor.Settings.MaxRadius + "</MaxRadius>";
                    Cfg += "<Length>" + Animation.Animations.Meteor.Settings.Length + "</Length>";
                    Cfg += "<Count>" + Animation.Animations.Meteor.Settings.Count + "</Count>";
                    Cfg += "<Velocity>" + Animation.Animations.Meteor.Settings.Velocity + "</Velocity>";
                    Cfg += "<Angle>" + Animation.Animations.Meteor.Settings.Angle + "</Angle>";
                    Cfg += "<GravitationalAcceleration>" + Animation.Animations.Meteor.Settings.GravitationalAcceleration + "</GravitationalAcceleration>";
                    Cfg += "<ColorMode>" + Animation.Animations.Meteor.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.Meteor.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.Meteor.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.Meteor.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.Meteor.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_Meteor) && new FileInfo(AnimationSettingsFilePath_Meteor).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_Meteor).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_Meteor, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：雪
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinRadius>" + Animation.Animations.Snow.Settings.MinRadius + "</MinRadius>";
                    Cfg += "<MaxRadius>" + Animation.Animations.Snow.Settings.MaxRadius + "</MaxRadius>";
                    Cfg += "<Count>" + Animation.Animations.Snow.Settings.Count + "</Count>";
                    Cfg += "<Velocity>" + Animation.Animations.Snow.Settings.Velocity + "</Velocity>";
                    Cfg += "<ColorMode>" + Animation.Animations.Snow.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.Snow.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.Snow.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.Snow.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.Snow.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_Snow) && new FileInfo(AnimationSettingsFilePath_Snow).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_Snow).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_Snow, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：引力粒子
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinMass>" + Animation.Animations.GravityParticle.Settings.MinMass + "</MinMass>";
                    Cfg += "<MaxMass>" + Animation.Animations.GravityParticle.Settings.MaxMass + "</MaxMass>";
                    Cfg += "<Count>" + Animation.Animations.GravityParticle.Settings.Count + "</Count>";
                    Cfg += "<CursorMass>" + Animation.Animations.GravityParticle.Settings.CursorMass + "</CursorMass>";
                    Cfg += "<GravityConstant>" + Animation.Animations.GravityParticle.Settings.GravityConstant + "</GravityConstant>";
                    Cfg += "<ElasticRestitutionCoefficient>" + Animation.Animations.GravityParticle.Settings.ElasticRestitutionCoefficient + "</ElasticRestitutionCoefficient>";
                    Cfg += "<ColorMode>" + Animation.Animations.GravityParticle.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.GravityParticle.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.GravityParticle.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.GravityParticle.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.GravityParticle.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_GravityParticle) && new FileInfo(AnimationSettingsFilePath_GravityParticle).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_GravityParticle).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_GravityParticle, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：引力网
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<Radius>" + Animation.Animations.GravityGrid.Settings.Radius + "</Radius>";
                    Cfg += "<Count>" + Animation.Animations.GravityGrid.Settings.Count + "</Count>";
                    Cfg += "<LineWidth>" + Animation.Animations.GravityGrid.Settings.LineWidth + "</LineWidth>";
                    Cfg += "<CursorMass>" + Animation.Animations.GravityGrid.Settings.CursorMass + "</CursorMass>";
                    Cfg += "<CursorRepulsionRadius>" + Animation.Animations.GravityGrid.Settings.CursorRepulsionRadius + "</CursorRepulsionRadius>";
                    Cfg += "<ColorMode>" + Animation.Animations.GravityGrid.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.GravityGrid.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.GravityGrid.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.GravityGrid.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.GravityGrid.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_GravityGrid) && new FileInfo(AnimationSettingsFilePath_GravityGrid).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_GravityGrid).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_GravityGrid, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画设置：扩散光点
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<MinRadius>" + Animation.Animations.SpreadSpot.Settings.MinRadius + "</MinRadius>";
                    Cfg += "<MaxRadius>" + Animation.Animations.SpreadSpot.Settings.MaxRadius + "</MaxRadius>";
                    Cfg += "<Count>" + Animation.Animations.SpreadSpot.Settings.Count + "</Count>";
                    Cfg += "<SourceX>" + Animation.Animations.SpreadSpot.Settings.SourceX + "</SourceX>";
                    Cfg += "<SourceY>" + Animation.Animations.SpreadSpot.Settings.SourceY + "</SourceY>";
                    Cfg += "<SourceZ>" + Animation.Animations.SpreadSpot.Settings.SourceZ + "</SourceZ>";
                    Cfg += "<SourceSize>" + Animation.Animations.SpreadSpot.Settings.SourceSize + "</SourceSize>";
                    Cfg += "<Velocity>" + Animation.Animations.SpreadSpot.Settings.Velocity + "</Velocity>";
                    Cfg += "<ColorMode>" + Animation.Animations.SpreadSpot.Settings.ColorMode + "</ColorMode>";
                    Cfg += "<GradientWhenRandom>" + Animation.Animations.SpreadSpot.Settings.GradientWhenRandom + "</GradientWhenRandom>";
                    Cfg += "<GradientVelocity>" + Animation.Animations.SpreadSpot.Settings.GradientVelocity + "</GradientVelocity>";
                    Cfg += "<Color>" + Animation.Animations.SpreadSpot.Settings.Color.ToArgb() + "</Color>";
                    Cfg += "<GlowMode>" + Animation.Animations.SpreadSpot.Settings.GlowMode + "</GlowMode>";
                    Cfg += "</Config>";

                    if (File.Exists(AnimationSettingsFilePath_SpreadSpot) && new FileInfo(AnimationSettingsFilePath_SpreadSpot).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AnimationSettingsFilePath_SpreadSpot).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(AnimationSettingsFilePath_SpreadSpot, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }

                // 动画类型、动画全局设置与通用设置
                try
                {
                    string Cfg = string.Empty;
                    Cfg += "<Config>";
                    Cfg += "<AnimationType>" + Animation.Type + "</AnimationType>";
                    Cfg += "<AntiAlias>" + Animation.Settings.AntiAlias + "</AntiAlias>";
                    Cfg += "<LimitFPS>" + Animation.Settings.LimitFPS + "</LimitFPS>";
                    Cfg += "<FPS>" + Animation.Settings.FPS + "</FPS>";
                    Cfg += "</Config>";

                    if (File.Exists(CommonSettingsFilePath) && new FileInfo(CommonSettingsFilePath).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(CommonSettingsFilePath).Attributes = FileAttributes.Normal;
                    }
                    StreamWriter SW = new StreamWriter(CommonSettingsFilePath, false);
                    SW.WriteLine(Cfg);
                    SW.Close();
                }
                catch { }
            }
            catch { }
        }

        #endregion

        #region 自动启动

        // 自动启动快捷方式路径
        public static string AutoStartShortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk";

        // 检查自动启动快捷方式
        public static bool CheckAutoStartShortcut()
        {
            try
            {
                if (File.Exists(AutoStartShortcutPath))
                {
                    IWshRuntimeLibrary.WshShell WShell = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut IWShortcut = (IWshRuntimeLibrary.IWshShortcut)WShell.CreateShortcut(AutoStartShortcutPath);
                    if (new FileInfo(IWShortcut.TargetPath).FullName == new FileInfo(Application.ExecutablePath).FullName)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // 在启动目录创建自动启动快捷方式
        public static bool CreateAutoStartShortcut()
        {
            try
            {
                IWshRuntimeLibrary.WshShell WShell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut IWShortcut = (IWshRuntimeLibrary.IWshShortcut)WShell.CreateShortcut(AutoStartShortcutPath);
                IWShortcut.TargetPath = Application.ExecutablePath;
                IWShortcut.WindowStyle = 1;
                IWShortcut.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }

        // 删除自动启动快捷方式
        public static bool DeleteAutoStartShortcut()
        {
            try
            {
                if (File.Exists(AutoStartShortcutPath))
                {
                    if (new FileInfo(AutoStartShortcutPath).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(AutoStartShortcutPath).Attributes = FileAttributes.Normal;
                    }

                    File.Delete(AutoStartShortcutPath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 开始菜单与桌面快捷方式

        // 开始菜单快捷方式路径
        public static string StartMenuShortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\" + Application.ProductName + ".lnk";

        // 检查开始菜单快捷方式
        public static bool CheckStartMenuShortcut()
        {
            try
            {
                if (File.Exists(StartMenuShortcutPath))
                {
                    IWshRuntimeLibrary.WshShell WShell = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut IWShortcut = (IWshRuntimeLibrary.IWshShortcut)WShell.CreateShortcut(StartMenuShortcutPath);
                    if (new FileInfo(IWShortcut.TargetPath).FullName == new FileInfo(Application.ExecutablePath).FullName)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // 在启动目录创建开始菜单快捷方式
        public static bool CreateStartMenuShortcut()
        {
            try
            {
                IWshRuntimeLibrary.WshShell WShell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut IWShortcut = (IWshRuntimeLibrary.IWshShortcut)WShell.CreateShortcut(StartMenuShortcutPath);
                IWShortcut.TargetPath = Application.ExecutablePath;
                IWShortcut.WindowStyle = 1;
                IWShortcut.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }

        // 删除开始菜单快捷方式
        public static bool DeleteStartMenuShortcut()
        {
            try
            {
                if (File.Exists(StartMenuShortcutPath))
                {
                    if (new FileInfo(StartMenuShortcutPath).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(StartMenuShortcutPath).Attributes = FileAttributes.Normal;
                    }

                    File.Delete(StartMenuShortcutPath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // 桌面快捷方式路径
        public static string DesktopShortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + Application.ProductName + ".lnk";

        // 检查桌面快捷方式
        public static bool CheckDesktopShortcut()
        {
            try
            {
                if (File.Exists(DesktopShortcutPath))
                {
                    IWshRuntimeLibrary.WshShell WShell = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut IWShortcut = (IWshRuntimeLibrary.IWshShortcut)WShell.CreateShortcut(DesktopShortcutPath);
                    if (new FileInfo(IWShortcut.TargetPath).FullName == new FileInfo(Application.ExecutablePath).FullName)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // 在启动目录创建桌面快捷方式
        public static bool CreateDesktopShortcut()
        {
            try
            {
                IWshRuntimeLibrary.WshShell WShell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut IWShortcut = (IWshRuntimeLibrary.IWshShortcut)WShell.CreateShortcut(DesktopShortcutPath);
                IWShortcut.TargetPath = Application.ExecutablePath;
                IWShortcut.WindowStyle = 1;
                IWShortcut.Save();

                return true;
            }
            catch
            {
                return false;
            }
        }

        // 删除桌面快捷方式
        public static bool DeleteDesktopShortcut()
        {
            try
            {
                if (File.Exists(DesktopShortcutPath))
                {
                    if (new FileInfo(DesktopShortcutPath).Attributes != FileAttributes.Normal)
                    {
                        new FileInfo(DesktopShortcutPath).Attributes = FileAttributes.Normal;
                    }

                    File.Delete(DesktopShortcutPath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}