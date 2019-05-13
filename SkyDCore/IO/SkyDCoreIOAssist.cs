using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SkyDCore.Text;
using System.Runtime.InteropServices;
using System.Web;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Runtime.Serialization.Json;

namespace SkyDCore.IO
{
    /// <summary>
    /// 输入输出辅助类
    /// </summary>
    public static class SkyDCoreIOAssist
    {
        /// <summary>
        /// 将字符串转化为内存流
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns>内存流</returns>
        public static MemoryStream ToMemoryStream(this string s,Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(s));
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filePath">文件存放路径</param>
        /// <returns>MD5值</returns>
        public static string CalculateMD5(string filePath)
        {
            return HashHelper.ComputeMD5(filePath);
        }

        /// <summary>
        /// 计算文件的CRC32值
        /// </summary>
        /// <param name="filePath">文件存放路径</param>
        /// <returns>CRC32值</returns>
        public static string CalculateCRC32(string filePath)
        {
            return HashHelper.ComputeCRC32(filePath);
        }

        /// <summary>
        /// 计算文件的SHA1值
        /// </summary>
        /// <param name="filePath">文件存放路径</param>
        /// <returns>SHA1值</returns>
        public static string CalculateSHA1(string filePath)
        {
            return HashHelper.ComputeSHA1(filePath);
        }

        /// <summary>
        /// 读取文件的二进制数据
        /// </summary>
        public static byte[] ReadBinaryData(string filePath)
        {
            return ReadBinaryData(new FileInfo(filePath));
        }

        /// <summary>
        /// 读取文件的二进制数据
        /// </summary>
        public static byte[] ReadBinaryData(this FileInfo fileInfo)
        {
            var b = new BinaryReader(fileInfo.OpenRead());
            var l = new byte[b.BaseStream.Length];
            for (long i = 0; i < b.BaseStream.Length; i++)
            {
                l[i] = b.ReadByte();
            }
            b.Close();
            return l;
        }

        /// <summary>
        /// 将二进制文件数据写入文件
        /// </summary>
        public static void WriteBinaryData(string filePath, byte[] data)
        {
            WriteBinaryData(new FileInfo(filePath), data);
        }

        /// <summary>
        /// 将二进制文件数据写入文件
        /// </summary>
        public static void WriteBinaryData(this FileInfo fileInfo, byte[] data)
        {
            var b = new BinaryWriter(fileInfo.Open(FileMode.OpenOrCreate));
            b.Write(data);
            b.Close();
        }

        /// <summary>
        /// 将多个对象的字符串形式写入文件
        /// </summary>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="objects">待写入的一个或多个对象</param>
        public static void WriteFile(string filePath, Encoding encoding, params object[] objects)
        {
            WriteFile(filePath, encoding, false, objects);
        }

        /// <summary>
        /// 将多个对象的字符串形式写入文件
        /// </summary>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="objects">待写入的一个或多个对象</param>
        /// <param name="isAppend">是否追加到最后</param>
        public static void WriteFile(string filePath, Encoding encoding, bool isAppend, params object[] objects)
        {
            var S = new StringBuilder();
            foreach (var f in objects)
            {
                if (S.Length > 0) S.AppendLine("");
                S.Append(f.ToString());
            }
            var s = new StreamWriter(filePath, isAppend, encoding);
            s.Write(S.ToString());
            s.Close();
        }

        /// <summary>
        /// 从文件中读出文本内容
        /// </summary>
        /// <param name="filePath">要打开的文件路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>内容</returns>
        public static string ReadFile(string filePath, Encoding encoding)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var s = new StreamReader(fs, encoding);
            var t = s.ReadToEnd();
            s.Close();
            return t;
        }

        /// <summary>
        ///  验证路径名中是否包含不允许的字符，包含则返回false。
        /// </summary>
        /// <param name="path">路径名字符串</param>
        /// <returns>验证结果</returns>
        public static bool ValidationPathRuel(this string path)
        {
            return path.CheckIsIncludeChars(false, Path.GetInvalidPathChars()) == false;
        }

        /// <summary>
        ///  验证文件名中是否包含不允许的字符，包含则返回false。
        /// </summary>
        /// <param name="fileName">文件名字符串</param>
        /// <returns>验证结果</returns>
        public static bool ValidationFileNameRuel(this string fileName)
        {
            return fileName.CheckIsIncludeChars(false, Path.GetInvalidFileNameChars()) == false;
        }

        /// <summary>
        /// 获取指定类型所在程序集的所有嵌入资源名称
        /// </summary>
        /// <param name="type">依据此类型判断资源所在程序集</param>
        /// <returns>嵌入资源名称数组</returns>
        public static string[] GetApplicationEmbedResources(this Type type)
        {
            var a = type.Assembly;
            return a.GetManifestResourceNames();
        }

        /// <summary>
        ///  获取指定类型所在程序集内的指定嵌入资源流
        /// </summary>
        /// <param name="type">依据此类型判断资源所在程序集</param>
        /// <param name="resourceName">嵌入资源名称，格式为：(程序集默认命名空间).(目录名).(文件名)</param>
        /// <returns>嵌入资源文件的流</returns>
        public static Stream GetApplicationEmbedResourceStream(this Type type, string resourceName)
        {
            var a = type.Assembly;
            return a.GetManifestResourceStream(resourceName);
        }

        /// <summary>
        /// 使用异或算法对文件进行加密，同密钥运行第二次即为解密
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="key">密钥</param>
        public static void XorEncryption(string filePath, string key)
        {
            var b = File.ReadAllBytes(filePath);
            for (var i = 0; i < b.Length; i++)
            {
                b[i] = (byte)(b[i] ^ key[i % key.Length]);
                i++;
            }
            File.WriteAllBytes(filePath, b);
        }

        /// <summary>
        /// 在指定的文件列表中删除多余指定数量的旧文件
        /// </summary>
        /// <param name="files">要处理的文件列表</param>
        /// <param name="reservedNum">要保留的较新文件的数量</param>
        public static void DeleteRedundantOldFiles(string[] files, int reservedNum)
        {
            for (var i = reservedNum; i < files.Length; i++)
            {
                var MinDateFile = new FileInfo(files[0]);
                foreach (var f in files)
                {
                    var F = new FileInfo(f);
                    if (F.LastWriteTime < MinDateFile.LastWriteTime) MinDateFile = F;
                }
                MinDateFile.Delete();
            }
        }

        /// <summary>
        /// 在指定的文件列表中删除多余指定数量的旧文件
        /// </summary>
        /// <param name="files">要处理的文件列表</param>
        /// <param name="reservedNum">要保留的新文件数量</param>
        public static void DeleteRedundantOldFiles(FileInfo[] files, int reservedNum)
        {
            for (var i = reservedNum; i < files.Length; i++)
            {
                var MinDateFile = files[0];
                foreach (var f in files)
                {
                    var F = f;
                    if (F.LastWriteTime < MinDateFile.LastWriteTime) MinDateFile = F;
                }
                MinDateFile.Delete();
            }
        }

        //#region  删除文件到回收站代码

        //private const int FO_DELETE = 0x3;
        //private const ushort FOF_NOCONFIRMATION = 0x10;
        //private const ushort FOF_ALLOWUNDO = 0x40;

        //[DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //private static extern int SHFileOperation([In, Out] _SHFILEOPSTRUCT str);

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        //private class _SHFILEOPSTRUCT
        //{
        //    public IntPtr hwnd;
        //    public UInt32 wFunc;
        //    public string pFrom;
        //    public string pTo;
        //    public UInt16 fFlags;
        //    public Int32 fAnyOperationsAborted;
        //    public IntPtr hNameMappings;
        //    public string lpszProgressTitle;
        //}

        ///// <summary>
        ///// 将文件删除到回收站中
        ///// </summary>
        ///// <param name="filePath">文件路径</param>
        //public static int DeleteFileToRecycleBin(string filePath)
        //{
        //    var pm = new _SHFILEOPSTRUCT();
        //    pm.wFunc = FO_DELETE;
        //    pm.pFrom = filePath + '\0';
        //    pm.pTo = null;
        //    pm.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
        //    return SHFileOperation(pm);
        //}

        //#endregion

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool CheckFileIsExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>是否存在</returns>
        public static bool CheckDirectoryIsExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 根据指定路径创建目录,如果该目录已存在则不进行任何操作
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void CreateDirectory(string path)
        {
            if (!CheckFileIsExists(path)) Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 获取指定路径的文件扩展名的小写形式
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath).ToLower();
        }

        /// <summary>
        /// 获取指定路径的文件名的小写形式
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath).ToLower();
        }

        /// <summary>
        /// 检测此目录及其子目录是否为空目录，并删除所有的空目录
        /// </summary>
        public static void DeleteEmptyDirectory(string dir)
        {
            DeleteEmptyDirectory(new DirectoryInfo(dir));
        }

        /// <summary>
        /// 检测此目录及其子目录是否为空目录，并删除所有的空目录
        /// </summary>
        public static void DeleteEmptyDirectory(DirectoryInfo dir)
        {
            foreach (var f in dir.GetDirectories())
            {
                DeleteEmptyDirectory(f);
            }
            if (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0)
                dir.Delete();
        }

        /// <summary>
        /// 获得指定相对路径的绝对路径，适用于非Web程序
        /// </summary>
        /// <param name="relativePath">指定的相对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        /// <summary>
        /// 将指定目录下的所有文件及子目录复制到目标目录中
        /// </summary>
        /// <param name="sourceDir">要操作的目录</param>
        /// <param name="targetDir">目标目录</param>
        public static void DuplicateDirectoryFiles(string sourceDir, string targetDir)
        {
            DuplicateDirectoryFiles(new DirectoryInfo(sourceDir), new DirectoryInfo(targetDir));
        }

        /// <summary>
        /// 将指定目录下的所有文件及子目录复制到目标目录中
        /// </summary>
        /// <param name="sourceDir">要操作的目录</param>
        /// <param name="targetDir">目标目录</param>
        public static void DuplicateDirectoryFiles(this DirectoryInfo sourceDir, DirectoryInfo targetDir)
        {
            if (!targetDir.Exists) targetDir.Create();
            foreach (var f in sourceDir.GetFiles())
            {
                f.CopyTo(targetDir.FullName.AsPathString().Combine(f.Name), true);
            }
            foreach (var d in sourceDir.GetDirectories())
            {
                DuplicateDirectoryFiles(d, new DirectoryInfo(targetDir.FullName.AsPathString().Combine(d.Name)));
            }
        }

        /// <summary>
        /// 将指定目录下的所有符合条件的文件及子目录复制到目标目录中
        /// </summary>
        /// <param name="sourceDir">要操作的目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="directoryFilter">判断目录是否应当被复制的方法</param>
        /// <param name="fileFilter">判断文件是否应当被复制的方法</param>
        public static void DuplicateDirectoryFiles(this DirectoryInfo sourceDir, DirectoryInfo targetDir, Predicate<FileInfo> fileFilter, Predicate<DirectoryInfo> directoryFilter)
        {
            if (!targetDir.Exists) targetDir.Create();
            foreach (var f in sourceDir.GetFiles())
            {
                if (fileFilter(f))
                {
                    f.CopyTo(targetDir.FullName.AsPathString().Combine(f.Name), true);
                }
            }
            foreach (var d in sourceDir.GetDirectories())
            {
                if (directoryFilter(d))
                {
                    DuplicateDirectoryFiles(d, new DirectoryInfo(targetDir.FullName.AsPathString().Combine(d.Name)), fileFilter, directoryFilter);
                }
            }
        }

        /// <summary>
        /// 删除目录中的所有文件
        /// </summary>
        /// <param name="dir">要操作的目录</param>
        public static void DeleteDirectoryFiles(this DirectoryInfo dir)
        {
            foreach (var f in dir.GetFiles())
            {
                f.Delete();
            }
            foreach (var f in dir.GetDirectories())
            {
                DeleteDirectoryFiles(f);
                f.Delete();
            }
        }

        /// <summary>
        /// 删除目录中的所有文件
        /// </summary>
        /// <param name="dir">要操作的目录</param>
        public static void DeleteDirectoryFiles(string dir)
        {
            DeleteDirectoryFiles(new DirectoryInfo(dir));
        }

        /// <summary>
        /// 判断是否为目录
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        public static bool CheckIsDirectory(string path)
        {
            var f = new FileInfo(path);
            return f.Attributes.ToString().IndexOf("Directory") >= 0;
        }

        /// <summary>
        /// 比对文件A的内容是否与文件B的内容相同，如果文件不存在或处理过程中发生异常，则返回null
        /// </summary>
        /// <param name="fileA">文件A</param>
        /// <param name="fileB">文件B</param>
        /// <returns>比对结果</returns>
        public static bool? FileCompare(this FileInfo fileA, FileInfo fileB)
        {
            if (!fileA.Exists || !fileB.Exists) return null;
            if (fileA.Length != fileB.Length) return false;
            if (fileA.FullName == fileB.FullName) return true;
            var f1 = fileA.OpenRead();
            var f2 = fileB.OpenRead();
            try
            {
                var b1 = 0;
                var b2 = 0;
                do
                {
                    b1 = f1.ReadByte();
                    b2 = f2.ReadByte();
                    if (b1 != b2) return false;
                }
                while (b1 != -1);
                return true;
            }
            catch (Exception e) { e.Message.Trace(); return null; }
            finally { f1.Close(); f2.Close(); }
        }

        /// <summary>
        /// 将对象序列化并转为字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>包含序列化内容的字符串</returns>
        public static string SerializeToJsonString(this Object obj)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            var serializer = new DataContractJsonSerializer(obj.GetType());
            //实例化一个内存流，用于存放序列化后的数据
            var stream = new MemoryStream();
            //使用WriteObject序列化对象
            serializer.WriteObject(stream, obj);
            //写入内存流中
            var dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            //通过UTF8格式转换为字符串
            return Encoding.UTF8.GetString(dataBytes);
        }

        /// <summary>
        /// 泛型方法，从序列化Json字符串中读取指定类型的对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromJsonString<T>(this string jsonString)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            var serializer = new DataContractJsonSerializer(typeof(T));
            //把Json传入内存流中保存
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象
            return (T)(serializer.ReadObject(stream));
        }

        /// <summary>
        /// 通过XML序列化和反序列化实现对象的克隆
        /// </summary>
        /// <returns>克隆后的对象</returns>
        public static T SerializeClone<T>(this T obj)
        {
            return DeserializeFromXmlString<T>(SerializeToXmlString(obj));
        }

        /// <summary>
        /// 将对象序列化并保存到文件
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="path">文件保存的位置</param>
        public static void SerializeToXmlFile(this object obj, string path)
        {
            var XS = new XmlSerializer(obj.GetType());
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var wt = XmlWriter.Create(stream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
            XS.Serialize(wt, obj);
            stream.Close();
        }

        /// <summary>
        /// 将对象序列化到内存流
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>内存流</returns>
        public static MemoryStream SerializeToXmlMemoryStream(this object obj)
        {
            var XS = new XmlSerializer(obj.GetType());
            var stream = new MemoryStream();
            var wt = XmlWriter.Create(stream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
            XS.Serialize(wt, obj);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// 将对象序列化并转为字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>包含序列化内容的字符串</returns>
        public static string SerializeToXmlString(this object obj)
        {
            var XS = new XmlSerializer(obj.GetType());
            var S = new StringBuilder();
            TextWriter TW = new StringWriter(S);
            XS.Serialize(TW, obj);
            TW.Close();
            return S.ToString();
        }

        /// <summary>
        /// 泛型方法，从序列化XML文件中读取指定类型的对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="path">文件保存的位置</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromXmlFile<T>(string path)
        {
            var XS = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var reader = new XmlTextReader(stream);
            reader.Normalization = false;
            var OBJ = (T)XS.Deserialize(reader);
            stream.Close();
            return OBJ;
        }

        /// <summary>
        /// 泛型方法，从序列化XML文件流中读取指定类型的对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="stream">数据输入流</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromXmlStream<T>(this Stream stream)
        {
            var XS = new XmlSerializer(typeof(T));
            stream.Position = 0;
            var reader = new XmlTextReader(stream);
            reader.Normalization = false;
            var OBJ = (T)XS.Deserialize(reader);
            return OBJ;
        }

        /// <summary>
        /// 泛型方法，从字符串中读取指定类型的对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="str">包含序列化内容的字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromXmlString<T>(string str)
        {
            var XS = new XmlSerializer(typeof(T));
            TextReader TR = new StringReader(str);
            var reader = new XmlTextReader(TR);
            reader.Normalization = false;
            var OBJ = (T)XS.Deserialize(reader);
            TR.Close();
            return OBJ;
        }

        /// <summary>
        /// 将指定的对象序列化为二进制文件
        /// </summary>
        /// <param name="obj">要进行序列化的对象</param>
        /// <param name="path">保存的文件路径</param>
        public static void SerializeToBinaryFile(this object obj, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        /// <summary>
        ///  将指定的对象序列化为内存流
        /// </summary>
        /// <param name="obj">要进行序列化的对象</param>
        /// <returns>包含序列化信息的内存流</returns>
        public static MemoryStream SerializeToBinaryMemoryStream(this object obj)
        {
            var MS = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(MS, obj);
            MS.Seek(0, SeekOrigin.Begin);
            return MS;
        }

        /// <summary>
        /// 通过序列化的二进制文件反序列化对象
        /// </summary>
        /// <param name="path">此前保存序列化文件的路径</param>
        /// <returns>反序列化后的对象</returns>
        public static object DeserializeFromBinaryFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var OBJ = formatter.Deserialize(stream);
            stream.Close();
            return OBJ;
        }

        /// <summary>
        /// 通过包含序列化的内存流反序列化对象
        /// </summary>
        /// <param name="stream">输入数据流</param>
        /// <returns>反序列化后的对象</returns>
        public static object DeserializeFromBinaryStream(this Stream stream)
        {
            var bf = new BinaryFormatter();
            stream.Position = 0;
            return bf.Deserialize(stream);
        }

        /// <summary>
        /// 获取exe或dll等文件的版本号
        /// </summary>
        /// <param name="path">文件所在路径</param>
        /// <returns>版本号</returns>
        public static string GetFileVersion(string path)
        {
            return FileVersionInfo.GetVersionInfo(path).FileVersion;
        }

        /// <summary>
        /// 获取文件尺寸数值
        /// </summary>
        /// <param name="unit">尺寸单位</param>
        public static double GetFileSize(this FileInfo fileInfo, StorageUnit unit)
        {
            return fileInfo.Length / (int)unit;
        }
    }
}
