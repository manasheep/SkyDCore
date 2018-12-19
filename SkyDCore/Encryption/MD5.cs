using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SkyDCore.Encryption
{
    /// <summary>
    /// MD5 无逆向编码
    /// 获取唯一特征串，可用于密码加密
    /// (无法还原)
    /// </summary>
    public class MD5
    {

        /// <summary>
        /// 
        /// </summary>
        public MD5()
        {
        }

        /// <summary>
        /// 获取字符串的特征串
        /// </summary>
        /// <param name="sInputString">输入文本</param>
        /// <returns></returns>
        public static string HashString(string sInputString)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            string encoded = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(sInputString))).Replace("-", "");
            return encoded;
        }

        /// <summary>
        /// 获取文件的特征串
        /// </summary>
        /// <param name="sInputFilename">输入文件</param>
        /// <returns></returns>
        public string HashFile(string sInputFilename)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            FileStream inFile = new System.IO.FileStream(sInputFilename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bInput = new byte[inFile.Length];
            inFile.Read(bInput, 0, bInput.Length);
            inFile.Close();

            string encoded = BitConverter.ToString(md5.ComputeHash(bInput)).Replace("-", "");
            return encoded;
        }

    }
}
