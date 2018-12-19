using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SkyDCore.Encryption
{
    /// <summary>
    /// Cryptography 的摘要说明。
    /// </summary>
    public class Cryptography
    {
        //加密KEY
        private static SymmetricAlgorithm key;

        static Cryptography()
        {
            key = new DESCryptoServiceProvider();
        }
        public Cryptography(SymmetricAlgorithm inKey)
        {
            key = inKey;
        }
        /// <summary>
        /// 哈希字符串
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetPWDHash(string pwd)
        {
            string ret = "";
            byte[] bpwd = Encoding.ASCII.GetBytes(pwd.Trim());
            byte[] edata;
            SHA256 sha = new SHA256Managed();
            edata = sha.ComputeHash(bpwd);
            ret = Convert.ToBase64String(edata);
            return ret.Trim();
        }
        /// <summary>
        /// 对字符串做对称加密返回加密后的数据
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string GetEncrypt(string original)
        {
            return Convert.ToBase64String(Encrypt(original, key));
        }

        /// <summary>
        /// 对字符串做对称解密返回解密后的数据
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static string GetDecrypt(string encrypt)
        {
            return Decrypt(Convert.FromBase64String(encrypt), key);
        }

        public string BytesToString(byte[] bs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("000"));
            }
            return sb.ToString();
        }
        public static byte[] StringToBytes(string s)
        {
            int bl = s.Length / 3;
            byte[] bs = new byte[bl];
            for (int i = 0; i < bl; i++)
            {
                bs[i] = byte.Parse(s.Substring(3 * i, 3));
            }
            return bs;
        }

        private static byte[] Encrypt(string PlainText, SymmetricAlgorithm key)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(encStream);
            sw.WriteLine(PlainText);
            sw.Close();
            encStream.Close();
            byte[] buffer = ms.ToArray();
            ms.Close();
            return buffer;
        }

        private static string Decrypt(byte[] CypherText, SymmetricAlgorithm key)
        {
            MemoryStream ms = new MemoryStream(CypherText);
            CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(encStream);
            string val = sr.ReadLine();
            sr.Close();
            encStream.Close();
            ms.Close();
            return val;
        }
    }
}
