using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkyDCore.IO
{
    public enum StorageUnit
    {
        /// <summary>
        /// 字节 byte (B)
        /// </summary>
        [Description("字节")]
        B = 1,
        /// <summary>
        /// 千字节 Kilobyte(K/KB)
        /// </summary>
        [Description("千字节")]
        KB = 1024,
        /// <summary>
        /// 兆字节 Megabyte(M/MB)
        /// </summary>
        [Description("兆字节")]
        MB = 1048576,
        /// <summary>
        /// 千兆字节 Gigabyte(G/GB)
        /// </summary>
        [Description("千兆字节")]
        GB = 1073741824
    }
}
