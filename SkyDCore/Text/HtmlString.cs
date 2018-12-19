using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using SkyDCore.Web;

namespace SkyDCore.Text
{
    public class HtmlString : SpecialString
    {
        /// <summary>
        /// 将普通文本转义为可以正确显示在网页上的HTML代码
        /// </summary>
        /// <returns>转义后的HTML代码</returns>
        public string Encoding()
        {
            return Value.HtmlEscaping();
        }

        /// <summary>
        /// 清除HTML标记，还原纯文本
        /// </summary>
        /// <param name="isClearScript">是否清除脚本代码</param>
        /// <param name="isTransformLineBreaks">是否转换特定标记为换行符，包括br、hr及p、div、li、h1、h2……的结尾</param>
        /// <returns>纯文本</returns>
        public string ClearHtmlMarkup(bool isClearScript, bool isTransformLineBreaks)
        {
            return Value.CleanHtmlTag(isClearScript, isTransformLineBreaks);
        }

        /// <summary>
        /// 获取标题
        /// </summary>
        public string GetTitle()
        {
            return Value.RegexMatch(@"<title.*?>(.+?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value.Trim();
        }

        /// <summary>
        /// 获取所有超链接（a）匹配项，其中组1为链接地址值，组2为链接显示内容
        /// </summary>
        public IEnumerable<Match> GetLinks()
        {
            return Value.RegexMatches(@"<a.+?href=[""'](.+?)[""'].*?>(.+?)</\s*?a>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Cast<Match>();
        }

        /// <summary>
        /// 获取所有图像（img）匹配项，其中组1为图像地址值
        /// </summary>
        public IEnumerable<Match> GetImages()
        {
            return Value.RegexMatches(@"<img.+?src=[""'](.+?)[""'].*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Cast<Match>();
        }
    }
}
