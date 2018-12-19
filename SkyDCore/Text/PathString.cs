using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkyDCore.Text
{
    public class PathString : SpecialString
    {
        /// <summary>
        /// 转换本地路径为本地文件Uri格式。
        /// 如“C:\abc\avatar.xml”将被转换为“file:///C:/abc/avatar.xml”。
        /// </summary>
        public string ToLocalFileUri()
        {
            return "file:///" + Value.Replace("\\", "/");
        }

        ///// <summary>
        ///// 将绝对路径与相对路径组合。
        ///// 如果传入的是绝对路径，则原样返回。
        ///// 通常用于处文件相对路径计算，如将“C:\abc\”或“C:\abc\a.txt”与“info.htm”组合的话，就会生成“C:\abc\info.htm”
        ///// </summary>
        ///// <param name="RelativePath">待组合的相对路径，可以是“..\abc.htm”形式</param>
        //public string CombineRelativePath(string RelativePath)
        //{
        //    //return ToLocalFileUri().AsUriString().CombineRelativePath(RelativePath.Replace("\\", "/")).AsUriString().ToLocalFilePath();
        //    return Value.AsPathString().Combine(RelativePath).AsPathString().FullPath;
        //}

        ///// <summary>
        ///// 获取完整路径，等同于Path.GetFullPath()
        ///// </summary>
        //public string FullPath
        //{
        //    get
        //    {
        //        return Path.GetFullPath(Value);
        //    }
        //}
        //private string _FullPath;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return Path.GetFileName(Value);
            }
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension
        {
            get
            {
                return Path.GetExtension(Value);
            }
        }

        /// <summary>
        /// 所在目录名，等同于Path.GetDirectoryName()
        /// </summary>
        public string DirectoryName
        {
            get
            {
                return Path.GetDirectoryName(Value);
            }
        }

        ///// <summary>
        ///// 返回文件是否存在
        ///// </summary>
        //public bool FileExists
        //{
        //    get
        //    {
        //        return File.Exists(Value);
        //    }
        //}

        ///// <summary>
        ///// 返回目录是否存在
        ///// </summary>
        //public bool DirectoryExists
        //{
        //    get
        //    {
        //        return Directory.Exists(Value);
        //    }
        //}

        /// <summary>
        /// 返回是否为绝对路径
        /// </summary>
        public bool IsPathRooted
        {
            get
            {
                return Path.IsPathRooted(Value);
            }
        }

        /// <summary>
        /// 拼接路径
        /// </summary>
        public string Combine(string 待拼接路径)
        {
            return Path.Combine(Value, 待拼接路径);
        }

        /// <summary>
        /// 不带扩展名的文件名
        /// </summary>
        public string FileNameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Value);
            }
        }
    }
}
