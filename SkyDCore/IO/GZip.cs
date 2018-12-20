using System;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace SkyDCore.IO
{
    /// <summary>
    /// Gzip 的摘要说明
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// 对目标文件夹进行压缩，将压缩结果保存为指定文件
        /// </summary>
        /// <param name="compressPathArray">要压缩的文件或文件夹路径，可指定多个</param>
        /// <param name="saveFilePath">压缩文件保存路径</param>
        public static void Compress(string saveFilePath, params string[] compressPathArray)
        {
            ArrayList list = new ArrayList();
            foreach (TempFileName f in ConversionToFileList(compressPathArray))
            {
                byte[] destBuffer = File.ReadAllBytes(f.FullName);
                SerializeFileInfo sfi = new SerializeFileInfo(f.Name, destBuffer);
                list.Add(sfi);
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream s = new MemoryStream())
            {
                formatter.Serialize(s, list);
                s.Position = 0;
                CreateCompressFile(s, saveFilePath);
            }
        }

        /// <summary>
        /// 将输入的数据流进行压缩，然后输出到一个内存流
        /// </summary>
        /// <param name="stream">输入的数据流</param>
        /// <returns>输出的内存流</returns>
        public static MemoryStream Compress(Stream stream)
        {
            var ms = new MemoryStream();
            using (GZipStream output = new GZipStream(ms, CompressionMode.Compress, true))
            {
                byte[] bytes = new byte[4096];
                int n;
                while ((n = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    output.Write(bytes, 0, n);
                }
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private static List<TempFileName> ConversionToFileList(string[] fileArray)
        {
            var list = new List<TempFileName>();
            foreach (string file in fileArray)
            {
                var f = Path.GetFullPath(file);
                string s = Path.GetDirectoryName(f.EndsWith(@"\") ? f.Substring(0, f.Length - 1) : f);
                if (Directory.Exists(f))
                {
                    foreach (string sf in Directory.GetFiles(f, "*", SearchOption.AllDirectories))
                    {
                        list.Add(new TempFileName(sf.Replace(s, ""), sf));
                    }
                }
                else list.Add(new TempFileName(f.Replace(s, ""), f));
            }
            return list;
        }

        /**/
        /// <summary>
        /// 对目标压缩文件解压缩，将内容解压缩到指定文件夹
        /// </summary>
        /// <param name="filePath">压缩文件路径</param>
        /// <param name="uncompressDirectoryPath">解压缩目录</param>
        public static void Uncompress(string filePath, string uncompressDirectoryPath)
        {
            uncompressDirectoryPath = Path.GetFullPath(uncompressDirectoryPath + @"\");
            using (Stream source = File.OpenRead(filePath))
            {
                using (Stream destination = new MemoryStream())
                {
                    using (GZipStream input = new GZipStream(source, CompressionMode.Decompress, true))
                    {
                        if (!Directory.Exists(uncompressDirectoryPath)) Directory.CreateDirectory(uncompressDirectoryPath);
                        byte[] bytes = new byte[4096];
                        int n;
                        while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            destination.Write(bytes, 0, n);
                        }
                    }
                    destination.Flush();
                    destination.Position = 0;
                    DeSerializeFiles(destination, uncompressDirectoryPath);
                }
            }
        }

        /// <summary>
        /// 将输入的压缩数据进行解压缩，然后输出到一个内存流
        /// </summary>
        /// <param name="stream">输入的压缩数据流</param>
        /// <returns>输出的内存流</returns>
        public static MemoryStream Uncompress(Stream stream)
        {
            var ms = new MemoryStream();

            using (var input = new GZipStream(stream, CompressionMode.Decompress, true))
            {
                byte[] bytes = new byte[4096];
                int n;
                while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                {
                    ms.Write(bytes, 0, n);
                }
            }

            //using (var input=new DeflateStream(压缩数据流, CompressionMode.Decompress, true))
            //{
            //    byte[] bytes = new byte[4096];
            //    int n;
            //    while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
            //    {
            //        ms.Write(bytes, 0, n);
            //    }
            //}
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 将输入的压缩数据进行解压缩，输出解压缩后的数据
        /// </summary>
        /// <param name="data">输入的压缩数据流</param>
        /// <returns>解压缩后的数据</returns>
        public static byte[] Uncompress(byte[] data)
        {
            var input = new MemoryStream(data, false);
            input.Position = 0;
            var ms = Uncompress(input);
            return ms.ToBytes();
        }

        private static void DeSerializeFiles(Stream s, string dirPath)
        {
            BinaryFormatter b = new BinaryFormatter();
            ArrayList list = (ArrayList)b.Deserialize(s);

            foreach (SerializeFileInfo f in list)
            {
                string newName = dirPath + f.FileName;
                if (!Directory.Exists(Path.GetDirectoryName(newName))) Directory.CreateDirectory(Path.GetDirectoryName(newName));
                using (FileStream fs = new FileStream(newName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(f.FileBuffer, 0, f.FileBuffer.Length);
                    fs.Close();
                }
            }
        }

        private static void CreateCompressFile(Stream source, string destinationName)
        {
            using (Stream destination = new FileStream(destinationName, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream output = new GZipStream(destination, CompressionMode.Compress))
                {
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = source.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        output.Write(bytes, 0, n);
                    }
                }
            }
        }

        class TempFileName
        {
            public TempFileName(string Name, string FullName)
            {
                this.Name = Name;
                this.FullName = FullName;
            }

            public string Name { get; set; }
            public string FullName { get; set; }
        }

        [Serializable]
        class SerializeFileInfo
        {
            public SerializeFileInfo(string name, byte[] buffer)
            {
                fileName = name;
                fileBuffer = buffer;
            }

            string fileName;
            public string FileName
            {
                get
                {
                    return fileName;
                }
            }

            byte[] fileBuffer;
            public byte[] FileBuffer
            {
                get
                {
                    return fileBuffer;
                }
            }
        }

    }
}
