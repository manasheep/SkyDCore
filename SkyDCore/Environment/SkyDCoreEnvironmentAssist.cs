using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkyDCore.Environment
{
    /// <summary>
    /// 运行环境辅助类
    /// </summary>
    public static class SkyDCoreEnvironmentAssist
    {
        /// <summary>
        /// 获取系统当前的用户名
        /// </summary>
        public static string SystemUserName
        {
            get { return System.Environment.UserName; }
        }

        /// <summary>
        /// 获取系统当前的公共语言运行库版本
        /// </summary>
        public static string RuntimeLibraryVersion
        {
            get { return System.Environment.Version.ToString(); }
        }

        /// <summary>
        /// 获得操作系统版本
        /// </summary>
        /// <值>操作系统版本</值>
        public static string OperationSystemVersion
        {
            get { return System.Environment.OSVersion.Version.ToString(); }
        }

        /// <summary>
        /// 获得系统目录路径
        /// </summary>
        /// <值>系统目录路径</值>
        public static string SystemDirectory
        {
            get { return System.Environment.SystemDirectory; }
        }

        /// <summary>
        /// 获得系统临时文件夹目录路径
        /// </summary>
        public static string TempDirectory
        {
            get { return Path.GetTempPath(); }
        }

        /// <summary>
        /// 获得系统桌面的物理目录路径
        /// </summary>
        public static string DesktopDirectory
        {
            get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory); }
        }

        /// <summary>
        /// 获得系统启动目录的路径
        /// </summary>
        public static string StartupDirectory
        {
            get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Startup); }
        }

        /// <summary>
        /// 获得系统启动时间，以毫秒为单位
        /// </summary>
        /// <值>系统启动时间</值>
        public static int SystemStartupTime
        {
            get { return System.Environment.TickCount; }
        }

    }
}
