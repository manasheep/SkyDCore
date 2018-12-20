using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security.Cryptography;

namespace SkyDCore.Text
{
    /// <summary>
    /// 文字辅助类
    /// </summary>
    public static class SkyDCoreTextAssist
    {
        /// <summary>
        /// 随机数
        /// </summary>
        private static Random R = new Random();

        #region 常量

        /// <summary>
        /// 用以在DataTime的ToString()方法中格式化日期时间格式字串
        /// </summary>
        public const string CodeDateTimeFormat = "yyMMddHHmmssffff";

        /// <summary>
        /// 用以在DataTime的ToString()方法中格式化日期时间格式字串
        /// </summary>
        public const string NormalDateTimeFormat = "yyyy年M月d日 HH:mm:ss";

        /// <summary>
        /// 用以在DataTime的ToString()方法中格式化日期时间格式字串
        /// </summary>
        public const string NormalDateFormat = "yyyy年M月d日";

        /// <summary>
        /// 用以在DataTime的ToString()方法中格式化日期时间格式字串
        /// </summary>
        public const string StandardDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 用以在DataTime的ToString()方法中格式化日期格式字串
        /// </summary>
        public const string StandardDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 用以在DataTime的ToString()方法中格式化时间格式字串
        /// </summary>
        public const string StandardTimeFormat = "HH:mm:ss";

        #endregion

        /// <summary>
        /// 比对两个字符串的相似度，每少余、多余、异于一个字符，就会增记1点差异值，最后将差异值返回
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="target">用于对比的字符串</param>
        /// <returns>差异值，该值不会超过两个字符串长度之和</returns>
        public static int CalculateSimilarity(this string source, string target)
        {
            int lenA = (int)source.Length;
            int lenB = (int)target.Length;
            int[,] c = new int[lenA + 1, lenB + 1];

            // i: begin point of strA
            // j: begin point of strB
            for (int i = 0; i < lenA; i++) c[i, lenB] = lenA - i;
            for (int j = 0; j < lenB; j++) c[lenA, j] = lenB - j;
            c[lenA, lenB] = 0;

            for (int i = lenA - 1; i >= 0; i--)
                for (int j = lenB - 1; j >= 0; j--)
                {
                    if (target[j] == source[i])
                        c[i, j] = c[i + 1, j + 1];
                    else
                        c[i, j] = minValue(c[i, j + 1], c[i + 1, j], c[i + 1, j + 1]) + 1;
                }

            return c[0, 0];
        }

        static int minValue(int a, int b, int c)
        {
            if (a < b && a < c) return a;
            else if (b < a && b < c) return b;
            else return c;
        }

        /// <summary>
        /// 查找字符串内是否包含指定字符
        /// </summary>
        /// <param name="source">要在其中进行查找的源字符串</param>
        /// <param name="isAllMatch">是否要求包含有所有查找字符的情况下才返回true，否则只需包含至少一个即可返回true</param>
        /// <param name="chars">用于查找的一个或多个字符</param>
        /// <returns>验证结果</returns>
        public static bool? CheckIsIncludeChars(this string source, bool isAllMatch, params char[] chars)
        {
            if (chars == null || !source.Verification()) return null;
            foreach (char c in chars)
            {
                bool b = false;
                foreach (char f in source)
                {
                    if (f == c)
                    {
                        if (!isAllMatch) return true;
                        b = true;
                        break;
                    }
                }
                if (isAllMatch && b == false) return false;
            }
            return isAllMatch;
        }

        /// <summary>
        /// 将字符串转换为对应的密钥代码
        /// </summary>
        /// <param name="source">密钥原文</param>
        /// <returns>密钥</returns>
        public static string CreateSecretKey(this string source)
        {
            var s = new string[]
                {
                    "oockid1dALPqqd",
                    "ci199jAVdjal178",
                    "ccccoo130C1a059jfllao",
                    "00dVdo10149kcaclaccoq",
                    "cnam10DD10d9ajp1",
                    "mmac9aA188ACdjjaiqp[",
                    "cc0q0ikaicaiaji11a",
                    "cc10K!LCL;ack1cD",
                    "cc00c1kOFOFOAJ<",
                    "CLLCocjj1dhhagxmX"
                };
            var S = new StringBuilder();
            foreach (char c in source)
            {
                S.Append(s[(int)c % 10]);
            }
            return S.ToString();
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="targetSubString">查找目标</param>
        /// <param name="replaceSubString">替换值</param>
        /// <param name="isIgnoreCase">是否忽略大小写</param>
        /// <returns>经过替换后的字符串</returns>
        public static string ReplaceString(this string source, string targetSubString, string replaceSubString, bool isIgnoreCase)
        {
            if (!isIgnoreCase) return source.Replace(targetSubString, replaceSubString);
            else
            {
                int i = 0;
                int j = 0;
                while (true)
                {
                    i = source.ToLower().IndexOf(targetSubString.ToLower(), j);
                    if (i < 0) break;
                    source = source.Remove(i, targetSubString.Length).Insert(i, replaceSubString);
                    j = i + replaceSubString.Length;
                }
                return source;
            }
        }

        /// <summary>
        /// 检索子字符串在母字符串中出现的次数
        /// </summary>
        public static int IncludeStringCount(this string source, string subString)
        {
            StringBuilder s = new StringBuilder(source);
            return (s.Length - s.Replace(subString, string.Empty).Length) / subString.Length;
        }

        /// <summary>
        /// 将指定字符串内的所有字符随机打乱顺序重新排列
        /// </summary>
        /// <param name="source">要处理的字符串</param>
        /// <returns>乱序排列后的字符串</returns>
        public static string Disorder(this string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                int Index = R.Next(source.Length);
                char TMP = source[i];
                source = ReplaceCharByIndex(source, i, source[Index]);
                source = ReplaceCharByIndex(source, Index, TMP);
            }
            return source;
        }

        /// <summary>
        /// 替换字符串内指定位置字符为另一个字符
        /// </summary>
        /// <param name="source">要处理的字符串</param>
        /// <param name="index">要替换字符的索引位置</param>
        /// <param name="replaceChar">要替换为的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string ReplaceCharByIndex(this string source, int index, char replaceChar)
        {
            source = source.Remove(index, 1);
            source = source.Insert(index, replaceChar.ToString());
            return source;
        }

        /// <summary>
        /// 改变文件名字符串的扩展名部分
        /// </summary>
        /// <param name="fileName">文件名字串,如"MyNotes.doc"</param>
        /// <param name="newExtension">新扩展名,如".txt"</param>
        public static string ChangeFileExtension(this string fileName, string newExtension)
        {
            return Path.ChangeExtension(fileName, newExtension);
        }

        /// <summary>
        /// 为字符串添加尾缀,如果该尾缀已存在,则不进行操作。通常用于路径字符串尾部添加斜杠或反斜杠。
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="suffix">要添加的尾缀</param>
        /// <returns>处理后的字符串</returns>
        public static string AddSuffix(this string source, string suffix)
        {
            if (source.EndsWith(suffix)) return source;
            else return source + suffix;
        }

        /// <summary>
        /// 遍历对象集合获取字符串形式的串接，以指定分隔符隔离每个对象
        /// </summary>
        /// <param name="collection">需处理的集合</param>
        /// <param name="separator">间隔字符</param>
        /// <returns>拼接后的字符串</returns>
        public static string ExpandAndToString(this System.Collections.IEnumerable collection, string separator)
        {
            StringBuilder S = new StringBuilder();
            foreach (object o in collection)
            {
                if (S.Length > 0) S.Append(separator);
                S.Append(o.ToString());
            }
            return S.ToString();
        }

        /// <summary>
        /// 遍历字符串数组获取字符串形式的串接，以指定分隔符隔离每个对象
        /// </summary>
        /// <param name="collection">需处理的数组</param>
        /// <param name="separator">间隔字符</param>
        /// <returns>拼接后的字符串</returns>
        public static string ExpandStringCollection(this IEnumerable<string> collection, string separator)
        {
            StringBuilder S = new StringBuilder();
            foreach (string o in collection)
            {
                if (S.Length > 0) S.Append(separator);
                S.Append(o);
            }
            return S.ToString();
        }

        /// <summary>
        /// 输出指定重复次数的字符串
        /// </summary>
        /// <param name="str">要重复的字符串</param>
        /// <param name="num">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repetition(this string str, int num)
        {
            StringBuilder S = new StringBuilder();
            for (int i = 0; i < num; i++) S.Append(str);
            return S.ToString();
        }

        /// <summary>
        /// 将字符串转为星号显示
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <returns>星号显示的字符串</returns>
        public static string ConvertToPasswordMask(this string source)
        {
            return Repetition("*", source.Length);
        }

        /// <summary>
        /// 验证一个字符串变量是否有效,当其为null或者空字符串以及空白字符时视为无效
        /// </summary>
        /// <param name="str">用于验证的字符串</param>
        /// <returns>是否有效</returns>
        public static bool Verification(this string str)
        {
            return Verification(str, false);
        }

        /// <summary>
        /// 验证一个字符串变量是否有效,当其为null或者空字符串时视为无效
        /// </summary>
        /// <param name="str">用于验证的字符串</param>
        /// <param name="IsAllowIncludeWhitespace">是否允许首尾存在空格及换行符等无意义字符</param>
        /// <returns>是否有效</returns>
        public static bool Verification(this string str, bool IsAllowIncludeWhitespace)
        {
            //return 字符串 != null && 字符串.Length > 0 && (允许存在空白字符 || 字符串.清除首尾空白().Length > 0);
            return !String.IsNullOrEmpty(str) && (IsAllowIncludeWhitespace || str.CleanWhitespace().Length > 0);
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">指定生成的随机字符串的长度</param>
        /// <returns>随机字符串</returns>
        public static string CreateRandomString(int length)
        {
            return CreateRandomString("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", length);
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="permitChars">指定包含所有可能出现的字符的字符串</param>
        /// <param name="length">指定生成的随机字符串的长度</param>
        /// <returns>随机字符串</returns>
        public static string CreateRandomString(string permitChars, int length)
        {
            StringBuilder S = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                S.Append(permitChars[R.Next(permitChars.Length)]);
            }
            return S.ToString();
        }

        /// <summary>
        /// 在[source]中查找[subString]的首次出现位置，如果[source]为空或未找到包含有[subString]则返回-1
        /// </summary>
        /// <param name="source">要在其中查找的母字符串</param>
        /// <param name="subString">用于查找的子字符串</param>
        /// <param name="IsIgnoreCase">是否匹配大小写</param>
        /// <returns>索引位置</returns>
        public static int FindSubStringIndex(this string source, string subString, bool IsIgnoreCase)
        {
            if (source.Verification(true))
            {
                if (IsIgnoreCase)
                {
                    source = source.ToLower();
                    subString = subString.ToLower();
                }
                return source.IndexOf(subString);
            }
            return -1;
        }

        /// <summary>
        /// 根据分隔符将字符串切割,返回切割后的字符串数组
        /// </summary>
        /// <param name="source">需要处理的字符串</param>
        /// <param name="separatorRegularExpression">分隔字符串的符号,可使用正则表达式语法</param>
        public static string[] Split(this string source, string separatorRegularExpression)
        {
            return Regex.Split(source, separatorRegularExpression);
        }

        /// <summary>
        /// 根据逗号将字符串切割,返回切割后的字符串数组
        /// </summary>
        /// <param name="source">需要处理的字符串</param>
        public static string[] Split(this string source)
        {
            return Split(source, ",");
        }

        /// <summary>
        /// 返回半角为单位的字符串长度, 1个汉字长度为2
        /// </summary>
        /// <param name="str">需要处理的字符串</param>
        /// <returns>长度</returns>
        public static int GetHalfAngleLength(this string str)
        {
            return Encoding.Unicode.GetBytes(str).Length;
        }

        /// <summary>
        /// 将字符串按指定长度裁切
        /// </summary>
        /// <param name="source">需要处理的字符串</param>
        /// <param name="length">要保留的字符长度字节数,汉字计为2个字节</param>
        /// <param name="ellipsis">超过长度所显示的省略字符串，例如:".."</param>
        public static string CutOff(this string source, int length, string ellipsis)
        {
            int x = 0;
            string s = "";
            for (int i = 0; i < source.Length; i++)
            {
                if (Regex.IsMatch(source[i].ToString(), @"[^\x00-\xff]"))
                {
                    if (x + 2 > length) break;
                    x += 2;
                }
                else x++;
                s += source[i];
                if (x >= length) break;
            }
            if (s.Length < source.Length) s += ellipsis;
            return s;
        }

        /// <summary>
        /// 将字符串按指定长度裁切
        /// </summary>
        /// <param name="source">需要处理的字符串</param>
        /// <param name="length">要保留的字符长度字节数,汉字计为2个字节</param>
        public static string CutOff(this string source, int length)
        {
            return CutOff(source, length, "..");
        }

        /// <summary>
        /// 验证字符串是否有效,有效则原样返回,无效则返回替代字符串
        /// </summary>
        /// <param name="source">要验证的字符串</param>
        /// <param name="replace">替代的字符串</param>
        /// <returns>字符串</returns>
        public static string VerificationAndReplace(this string source, string replace)
        {
            return Verification(source) ? source : replace;
        }

        /// <summary>
        /// 验证字符串是否有效,有效则将其格式化并返回,无效则返回替代字符串
        /// </summary>
        /// <param name="source">要验证的字符串</param>
        /// <param name="format">当字符串有效时，以此将其格式化并输出，使用“{0}”表示原字符串</param>
        /// <param name="replace">替代的字符串</param>
        /// <returns>字符串</returns>
        public static string VerificationAndReplace(this string source, string format, string replace)
        {
            return Verification(source) ? String.Format(format, source) : replace;
        }

        /// <summary>
        /// 正则表达式转义，转换输出字符串，使其不会使正则表达式对之产生歧义
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string RegularExpressionEscaping(this string str)
        {
            return Regex.Replace(str, @"([\[\]\(\)\{\}\,\.\$\^\*\+\?\|\-\\\/\<\>\:])", @"\$1");
        }

        /// <summary>
        /// Unicode转义，将字符串中的每个字符转为转义字符串，如\u0c2f
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string UnicodeEscaping(this string str)
        {
            StringBuilder S = new StringBuilder();
            var s = Encoding.Unicode.GetBytes(str);
            for (int i = 0; i < s.Length; i += 2)
            {
                S.Append(@"\u");
                S.Append(s[i + 1].ToString("x2"));
                S.Append(s[i].ToString("x2"));
            }
            return S.ToString();
        }

        /// <summary>
        /// 按照指定要求，清除字符串中的首尾空白，当传入字符串为null时直接返回。
        /// </summary>
        /// <param name="source">需处理的字符串</param>
        /// <param name="isAtStart">是否清除段首空白</param>
        /// <param name="isAtEnd">是否清除段尾空白</param>
        /// <param name="isCleanLineBreak">是否同时清除换行符</param>
        /// <returns>处理后的字符串</returns>
        public static string CleanWhitespace(this string source, bool isAtStart, bool isAtEnd, bool isCleanLineBreak)
        {
            if (source == null) return null;
            if (isAtStart)
            {
                if (isCleanLineBreak) source = Regex.Replace(source, @"^[\s\r\n]*", "");
                else source = Regex.Replace(source, @"^\s*", "");
            }
            if (isAtEnd)
            {
                if (isCleanLineBreak) source = Regex.Replace(source, @"[\s\r\n]*$", "");
                else source = Regex.Replace(source, @"\s*$", "");
            }
            return source;
        }

        /// <summary>
        /// 清除字符串中的首尾空白
        /// </summary>
        /// <param name="source">需处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string CleanWhitespace(this string source)
        {
            return CleanWhitespace(source, true, true, true);
        }

        /// <summary>
        /// 依据置换表置换输入文本，置换表[replacementDefinitionTable]的格式为：[查询字串][replaceSymbol][替换字串]，如“我的====_C_====我们的”，每行一条，如需替代为包含换行符的文本，换行符使用自定义符号[lineBreakSymbol]替代，如“====_R_====”
        /// </summary>
        /// <param name="source">要进行置换的原始文本</param>
        /// <param name="replacementDefinitionTable">置换表</param>
        /// <param name="replaceSymbol">自定义置换符，如“====_C_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="lineBreakSymbol">自定义回车替代符，如“====_R_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <returns>置换后的文本</returns>
        public static string Replacement(this string source, string replacementDefinitionTable, string replaceSymbol, string lineBreakSymbol)
        {
            MatchCollection M = Regex.Matches(replacementDefinitionTable, "(.+)" + replaceSymbol + "(.+)");
            foreach (Match f in M)
            {
                source = source.Replace(f.Groups[1].Value.Replace("/r", "").Replace("/n", ""), f.Groups[2].Value.Replace("/r", "").Replace("/n", "").Replace(lineBreakSymbol, "\r\n"));
            }
            return source;
        }

        /// <summary>
        /// 依据置换表置换输入文本，置换表[replacementDefinitionTable]的格式为：[查询字串][replaceSymbol][替换字串]，如“我的====_C_====我们的”，每行一条，如需替代为包含换行符的文本，换行符使用自定义符号[lineBreakSymbol]替代，如“====_R_====”
        /// </summary>
        /// <param name="source">要进行置换的原始文本</param>
        /// <param name="isUseRegularExpression">查询及替换时是否使用正则表达式</param>
        /// <param name="replacementDefinitionTable">置换表</param>
        /// <param name="replaceSymbol">自定义置换符，如“====_C_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="lineBreakSymbol">自定义回车替代符，如“====_R_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <returns>置换后的文本</returns>
        public static string Replacement(this string source, bool isUseRegularExpression, string replacementDefinitionTable, string replaceSymbol, string lineBreakSymbol)
        {
            int x = 0;
            return Replacement(source, isUseRegularExpression, replacementDefinitionTable, replaceSymbol, lineBreakSymbol, out x, out x);
        }

        /// <summary>
        /// 依据置换表置换输入文本，置换表[replacementDefinitionTable]的格式为：[查询字串][replaceSymbol][替换字串]，如“我的====_C_====我们的”，每行一条，如需替代为包含换行符的文本，换行符使用自定义符号[lineBreakSymbol]替代，如“====_R_====”
        /// </summary>
        /// <param name="source">要进行置换的原始文本</param>
        /// <param name="isUseRegularExpression">查询及替换时是否使用正则表达式</param>
        /// <param name="replacementDefinitionTable">置换表</param>
        /// <param name="replaceSymbol">自定义置换符，如“====_C_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="lineBreakSymbol">自定义回车替代符，如“====_R_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="takeEffectCount">返回置换生效的条目数</param>
        /// <param name="totalCount">返回置换条目的总数</param>
        /// <returns>置换后的文本</returns>
        public static string Replacement(this string source, bool isUseRegularExpression, string replacementDefinitionTable, string replaceSymbol, string lineBreakSymbol, out int takeEffectCount, out int totalCount)
        {
            takeEffectCount = totalCount = 0;
            MatchCollection M = Regex.Matches(replacementDefinitionTable, "([^\r\n]+)" + replaceSymbol + "([^\r\n]*)");
            foreach (Match f in M)
            {
                totalCount++;
                if (isUseRegularExpression)
                {
                    try
                    {
                        if (Regex.IsMatch(source, f.Groups[1].Value.Replace("/r", "").Replace("/n", ""))) takeEffectCount++;
                        source = Regex.Replace(source, f.Groups[1].Value.Replace("/r", "").Replace("/n", ""), f.Groups[2].Value.Replace("/r", "").Replace("/n", "").Replace(lineBreakSymbol, "\r\n"));
                    }
                    catch { }
                }
                else
                {
                    if (source.IndexOf(f.Groups[1].Value) >= 0) takeEffectCount++;
                    source = source.Replace(f.Groups[1].Value.Replace("/r", "").Replace("/n", ""), f.Groups[2].Value.Replace("/r", "").Replace("/n", "").Replace(lineBreakSymbol, "\r\n"));
                }
            }
            return source;
        }


        /// <summary>
        /// 获取字符串的起始字符
        /// </summary>
        /// <returns>首字符</returns>
        public static char GetFirstChar(this string s)
        {
            return s[0];
        }

        /// <summary>
        /// 获取字符串的末尾字符
        /// </summary>
        /// <returns>尾字符</returns>
        public static char GetLastChar(this string s)
        {
            return s[s.Length - 1];
        }

        /// <summary>
        /// 转换为Int16方法
        /// </summary>
        public static Int16 ParseToInt16(this string s)
        {
            return Int16.Parse(s);
        }

        /// <summary>
        /// 转换为Int32方法
        /// </summary>
        public static Int32 ParseToInt32(this string s)
        {
            return Int32.Parse(s);
        }

        /// <summary>
        /// 转换为Int64方法
        /// </summary>
        public static Int64 ParseToInt64(this string s)
        {
            return Int64.Parse(s);
        }

        /// <summary>
        /// 转换为double方法
        /// </summary>
        public static double ParseToDouble(this string s)
        {
            return double.Parse(s);
        }

        /// <summary>
        /// 转换为float方法
        /// </summary>
        public static float ParseToFloat(this string s)
        {
            return float.Parse(s);
        }


        /// <summary>
        /// 根据字数限制截取原始内容，并将截取后的字符串中最后一个换行符以后的字符删除
        /// </summary>
        public static string ParagraphCutOf(this string source, int length)
        {
            var s = source.Substring(0, 200);
            var l = s.LastIndexOf("\n");
            return s.Substring(0, l);
        }

        /// <summary>
        /// 转换字符串为首字母大写形式，如Name
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>首字母大写形式</returns>
        public static string ToCapitalize(this string s)
        {
            return s.ToUpper()[0] + s.ToLower().Substring(1);
        }

        /// <summary>
        /// 转换为驼峰命名法。忽略符号和空格，将字符串以驼峰形式重组，例如windows media player 10会重组为WindowsMediaPlayer10
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="isFirstCharToUpper">首字母是否大写</param>
        /// <returns>驼峰形式重组的字符串</returns>
        public static string ToCamelCase(this string s, bool isFirstCharToUpper)
        {
            var m = s.RegexMatches(@"[\w\d]+");
            StringBuilder sb = new StringBuilder();
            foreach (var f in m.Cast<Match>())
            {
                if (sb.Length == 0 && !isFirstCharToUpper) sb.Append(f.Value.ToLower());
                else sb.Append(f.Value.ToCapitalize());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 阿拉伯数字转中文大写数字
        /// </summary>
        /// <param name="numberString">阿拉伯数字</param>
        /// <param name="isToRMB">是否为人民币大写形式</param>
        /// <returns>转换结果</returns>
        public static string ToChineseCapitalNumbers(this string numberString, bool isToRMB)
        {
            var c = new ChineseCapitalNumbers();
            return c.Convert(numberString, isToRMB);
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string ToMD5(this string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
                ret.Append(b[i].ToString("x").PadLeft(2, '0'));
            return ret.ToString();
        }

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string ToSHA256(this string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }

    }
}
