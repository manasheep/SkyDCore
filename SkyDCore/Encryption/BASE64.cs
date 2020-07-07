using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SkyDCore.Encryption
{
    /// <summary>
    /// Base64 UUEncoded 编码
    /// 将二进制编码为ASCII文本，用于网络传输
    /// (可还原)
    /// </summary>
    public static class BASE64
    {

        /// <summary>
        /// 解码字符串
        /// </summary>
        /// <param name="sInputString">输入文本</param>
        /// <param name="encoding">内容字符串编码</param>
        /// <returns>解码后的字符串</returns>
        public static string DecryptString(string sInputString, Encoding encoding)
        {
            //char[] sInput = sInputString.ToCharArray();
            try
            {
                byte[] bOutput = Convert.FromBase64String(sInputString);
                return encoding.GetString(bOutput);
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("base 64 字符数组为null", e);
            }
            catch (FormatException e)
            {
                throw new Exception("格式错误，可能是长度无法整除4", e);
            }
        }

        /// <summary>
        /// 编码字符串
        /// </summary>
        /// <param name="sInputString">输入文本</param>
        /// <param name="encoding">内容字符串编码</param>
        /// <returns>编码后的字符串</returns>
        public static string EncryptString(string sInputString, Encoding encoding)
        {
            byte[] bInput = encoding.GetBytes(sInputString);
            try
            {
                return Convert.ToBase64String(bInput, 0, bInput.Length);
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("二进制数组为NULL", e);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new Exception("格式错误，可能是长度不够", e);
            }
        }

        /// <summary>
        /// 解码文件
        /// </summary>
        /// <param name="sInputFilename">输入文件</param>
        /// <param name="sOutputFilename">输出文件</param>
        public static void DecryptFile(string sInputFilename, string sOutputFilename)
        {
            System.IO.StreamReader inFile;
            char[] base64CharArray;

            try
            {
                inFile = new System.IO.StreamReader(sInputFilename,
                    System.Text.Encoding.ASCII);
                base64CharArray = new char[inFile.BaseStream.Length];
                inFile.Read(base64CharArray, 0, (int)inFile.BaseStream.Length);
                inFile.Close();
            }
            catch
            {//(System.Exception exp) {
                return;
            }

            // 转换Base64 UUEncoded为二进制输出
            byte[] binaryData;
            try
            {
                binaryData =
                    System.Convert.FromBase64CharArray(base64CharArray,
                    0,
                    base64CharArray.Length);
            }
            catch (System.ArgumentNullException)
            {
                //base 64 字符数组为null
                return;
            }
            catch (System.FormatException)
            {
                //长度错误，无法整除4
                return;
            }

            // 写输出数据
            System.IO.FileStream outFile;
            try
            {
                outFile = new System.IO.FileStream(sOutputFilename,
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write);
                outFile.Write(binaryData, 0, binaryData.Length);
                outFile.Close();
            }
            catch
            {// (System.Exception exp) {
                //流错误
            }

        }

        /// <summary>
        /// 编码文件
        /// </summary>
        /// <param name="sInputFilename">输入文件</param>
        /// <param name="sOutputFilename">输出文件</param>
        public static void EncryptFile(string sInputFilename, string sOutputFilename)
        {

            System.IO.FileStream inFile;
            byte[] binaryData;

            try
            {
                inFile = new System.IO.FileStream(sInputFilename,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read);
                binaryData = new Byte[inFile.Length];
                long bytesRead = inFile.Read(binaryData, 0,
                    (int)inFile.Length);
                inFile.Close();
            }
            catch
            { //(System.Exception exp) {
                return;
            }

            // 转换二进制输入为Base64 UUEncoded输出
            // 每3个字节在源数据里作为4个字节 
            long arrayLength = (long)((4.0d / 3.0d) * binaryData.Length);

            // 如果无法整除4
            if (arrayLength % 4 != 0)
            {
                arrayLength += 4 - arrayLength % 4;
            }

            char[] base64CharArray = new char[arrayLength];
            try
            {
                System.Convert.ToBase64CharArray(binaryData,
                    0,
                    binaryData.Length,
                    base64CharArray,
                    0);
            }
            catch (System.ArgumentNullException)
            {
                //二进制数组为NULL.
                return;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                //长度不够
                return;
            }

            // 写UUEncoded数据到文件内
            System.IO.StreamWriter outFile;
            try
            {
                outFile = new System.IO.StreamWriter(sOutputFilename,
                    false,
                    System.Text.Encoding.ASCII);
                outFile.Write(base64CharArray);
                outFile.Close();
            }
            catch
            {// (System.Exception exp) {
                //文件流出错
            }


        }
    }
}
