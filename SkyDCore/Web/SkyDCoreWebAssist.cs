using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;
using System;
using System.Text;

namespace SkyDCore.Web
{
    public static class SkyDCoreWebAssist
    {
        /// <summary>
        /// 将颜色转换为网页代码形式，不含#符号，如FFDA00
        /// </summary>
        public static string GetHexColorString(this Color c)
        {
            return string.Format("{0:x2}{1:x2}{2:x2}", c.R, c.G, c.B);
        }

        /// <summary>
        /// 清除字符串内的UBB标签
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static string CleanUbbTag(this string ubbCodeString)
        {
            while (Regex.IsMatch(ubbCodeString, @"\[(\w+)=?[^\]]*\]([\s\S]*?)\[/\1\]"))
                ubbCodeString = Regex.Replace(ubbCodeString, @"\[(\w+)\s?[^\]]*\]([\s\S]*?)\[/\1\]", "$2");
            return ubbCodeString;
        }

        /// <summary>
        /// 清除字符串内的HTML标签的方式之一，适合对较为规则的文档进行简单替换。
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static string CleanHtmlTag(this string htmlCodeString)
        {
            var ex = @"<\s*(?<tag>\w+)\s*.*?>(?<text>.*?)</\k<tag>\s*>";
            var op = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            htmlCodeString = htmlCodeString.RegexReplace(@"<\s*(?<tag>\w+)\s*[^>]+?/>", "");
            while (htmlCodeString.RegexIsMatch(ex, op))
                htmlCodeString = htmlCodeString.RegexReplace(ex, "${text}", op);
            return htmlCodeString.RegexReplace(@"<\s*[\!-\[]*(?<tag>\w+)\s*[^>]+?>", "");
        }

        /// <summary>
        /// 清除字符串内的HTML标签的方式之二，可以提供更好的文档代码容错性，并拥有更多选项。
        /// </summary>
        /// <param name="isCleanScriptCode">是否清除脚本代码</param>
        /// <param name="isConvertSpecialTagToLineBreak">是否转换特定标记为换行符，包括br、hr及p、div、li、h1、h2……的结尾</param>
        // ReSharper disable once InconsistentNaming
        public static string CleanHtmlTag(this string htmlCodeString, bool isCleanScriptCode, bool isConvertSpecialTagToLineBreak)
        {
            string v = htmlCodeString;
            if (isCleanScriptCode)
            {
                v = v.RegexReplace(@"<\s*script.+?>.*?<\s*/script\s*>", String.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            if (isConvertSpecialTagToLineBreak)
            {
                v = v.RegexReplace(@"<\s*/\s*(?:p|br|div|li|h1|h2|h3|h4|h5|h6|hr|tr|dd|table|ul|ol|dl)\s*>", "【【【LineBreak】】】", RegexOptions.IgnoreCase);
                v = v.RegexReplace(@"<\s*(?:br|hr|p|tr|dd|div)[^>]*?/>", "【【【LineBreak】】】", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            v = v.RegexReplace(@"<[^>]+>", String.Empty).Replace("&nbsp;", " ").RegexReplace(@"\s+", " ");
            if (isConvertSpecialTagToLineBreak)
            {
                v = v.Replace("【【【LineBreak】】】", "\r\n");
            }
            return v;
        }

        /// <summary>
        /// 检查目标字符串中是否有危险的HTML代码
        /// </summary>
        /// <param name="htmlCodeString"></param>
        /// <returns>目标字符串中是否有危险的HTML代码</returns>
        // ReSharper disable once InconsistentNaming
        public static bool CheckIsIncludeDangerHtmlCode(this string htmlCodeString)
        {
            if (htmlCodeString.IsNullOrEmptyOrWhitespace()) return false;
            foreach (Match f in htmlCodeString.RegexMatches(@"<[^>]+?>"))
            {
                if (f.Value.RegexIsMatch(@"^<\s*(script|iframe|frameset|frame|form|link)", RegexOptions.IgnoreCase))
                {
                    return true;
                }
                if (f.Value.RegexIsMatch(@"\son\w+\s*=\s*(['""]?).+?\1(?=[\s >])", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                {
                    return true;
                }
                if (f.Value.RegexIsMatch(@"\s\w+\s*=\s*(['""]?)\s * (javascript | vbscript)\s *:.+?\1(?=[\s >])", RegexOptions.IgnoreCase | RegexOptions.Singleline))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将字符串内的HTML标记符(不包括换行、空格等字符)转换为在HTML页面中可呈现的形式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 逆向执行“进行HTML编码”方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string HtmlDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// HTML转义。首先执行“进行HTML编码”方法，然后转换空格、Tab及换行符为HTML页面中可呈现的形式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string HtmlEscaping(this string str)
        {
            return Regex.Replace(HtmlEncode(str).Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"), @"(\r)?\n", "<br />");
        }

        /// <summary>
        /// 解码特殊符号，将字符串中的特殊符号编码还原为特殊符号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>还原后的字符串</returns>
        public static string DecodeHtmlSpecialSymbol(this string str)
        {
            return
                str.Replace("&Alpha;", @"Α")
                    .Replace("&#913;", @"Α")
                    .Replace("&Beta;", @"Β")
                    .Replace("&#914;", @"Β")
                    .Replace("&Gamma;", @"Γ")
                    .Replace("&#915;", @"Γ")
                    .Replace("&Delta;", @"Δ")
                    .Replace("&#916;", @"Δ")
                    .Replace("&Epsilon;", @"Ε")
                    .Replace("&#917;", @"Ε")
                    .Replace("&Zeta;", @"Ζ")
                    .Replace("&#918;", @"Ζ")
                    .Replace("&Eta;", @"Η")
                    .Replace("&#919;", @"Η")
                    .Replace("&Theta;", @"Θ")
                    .Replace("&#920;", @"Θ")
                    .Replace("&Iota;", @"Ι")
                    .Replace("&#921;", @"Ι")
                    .Replace("&Kappa;", @"Κ")
                    .Replace("&#922;", @"Κ")
                    .Replace("&Lambda;", @"Λ")
                    .Replace("&#923;", @"Λ")
                    .Replace("&Mu;", @"Μ")
                    .Replace("&#924;", @"Μ")
                    .Replace("&Nu;", @"Ν")
                    .Replace("&#925;", @"Ν")
                    .Replace("&Xi;", @"Ξ")
                    .Replace("&#926;", @"Ξ")
                    .Replace("&Omicron;", @"Ο")
                    .Replace("&#927;", @"Ο")
                    .Replace("&Pi;", @"Π")
                    .Replace("&#928;", @"Π")
                    .Replace("&Rho;", @"Ρ")
                    .Replace("&#929;", @"Ρ")
                    .Replace("&Sigma;", @"Σ")
                    .Replace("&#931;", @"Σ")
                    .Replace("&Tau;", @"Τ")
                    .Replace("&#932;", @"Τ")
                    .Replace("&Upsilon;", @"Υ")
                    .Replace("&#933;", @"Υ")
                    .Replace("&Phi;", @"Φ")
                    .Replace("&#934;", @"Φ")
                    .Replace("&Chi;", @"Χ")
                    .Replace("&#935;", @"Χ")
                    .Replace("&Psi;", @"Ψ")
                    .Replace("&#936;", @"Ψ")
                    .Replace("&Omega;", @"Ω")
                    .Replace("&#937;", @"Ω")
                    .Replace("&alpha;", @"α")
                    .Replace("&#945;", @"α")
                    .Replace("&beta;", @"β")
                    .Replace("&#946;", @"β")
                    .Replace("&gamma;", @"γ")
                    .Replace("&#947;", @"γ")
                    .Replace("&delta;", @"δ")
                    .Replace("&#948;", @"δ")
                    .Replace("&epsilon;", @"ε")
                    .Replace("&#949;", @"ε")
                    .Replace("&zeta;", @"ζ")
                    .Replace("&#950;", @"ζ")
                    .Replace("&eta;", @"η")
                    .Replace("&#951;", @"η")
                    .Replace("&theta;", @"θ")
                    .Replace("&#952;", @"θ")
                    .Replace("&iota;", @"ι")
                    .Replace("&#953;", @"ι")
                    .Replace("&kappa;", @"κ")
                    .Replace("&#954;", @"κ")
                    .Replace("&lambda;", @"λ")
                    .Replace("&#955;", @"λ")
                    .Replace("&mu;", @"μ")
                    .Replace("&#956;", @"μ")
                    .Replace("&nu;", @"ν")
                    .Replace("&#957;", @"ν")
                    .Replace("&xi;", @"ξ")
                    .Replace("&#958;", @"ξ")
                    .Replace("&omicron;", @"ο")
                    .Replace("&#959;", @"ο")
                    .Replace("&pi;", @"π")
                    .Replace("&#960;", @"π")
                    .Replace("&rho;", @"ρ")
                    .Replace("&#961;", @"ρ")
                    .Replace("&sigmaf;", @"ς")
                    .Replace("&#962;", @"ς")
                    .Replace("&sigma;", @"σ")
                    .Replace("&#963;", @"σ")
                    .Replace("&tau;", @"τ")
                    .Replace("&#964;", @"τ")
                    .Replace("&upsilon;", @"υ")
                    .Replace("&#965;", @"υ")
                    .Replace("&phi;", @"φ")
                    .Replace("&#966;", @"φ")
                    .Replace("&chi;", @"χ")
                    .Replace("&#967;", @"χ")
                    .Replace("&psi;", @"ψ")
                    .Replace("&#968;", @"ψ")
                    .Replace("&omega;", @"ω")
                    .Replace("&#969;", @"ω")
                    .Replace("&thetasym;", @"ϑ")
                    .Replace("&#977;", @"ϑ")
                    .Replace("&upsih;", @"ϒ")
                    .Replace("&#978;", @"ϒ")
                    .Replace("&piv;", @"ϖ")
                    .Replace("&#982;", @"ϖ")
                    .Replace("&bull;", @"•")
                    .Replace("&#8226;", @"•")
                    .Replace("&hellip;", @"…")
                    .Replace("&#8230;", @"…")
                    .Replace("&prime;", @"′")
                    .Replace("&#8242;", @"′")
                    .Replace("&Prime;", @"″")
                    .Replace("&#8243;", @"″")
                    .Replace("&oline;", @"‾")
                    .Replace("&#8254;", @"‾")
                    .Replace("&frasl;", @"⁄")
                    .Replace("&#8260;", @"⁄")
                    .Replace("&weierp;", @"℘")
                    .Replace("&#8472;", @"℘")
                    .Replace("&image;", @"ℑ")
                    .Replace("&#8465;", @"ℑ")
                    .Replace("&real;", @"ℜ")
                    .Replace("&#8476;", @"ℜ")
                    .Replace("&trade;", @"™")
                    .Replace("&#8482;", @"™")
                    .Replace("&alefsym;", @"ℵ")
                    .Replace("&#8501;", @"ℵ")
                    .Replace("&larr;", @"←")
                    .Replace("&#8592;", @"←")
                    .Replace("&uarr;", @"↑")
                    .Replace("&#8593;", @"↑")
                    .Replace("&rarr;", @"→")
                    .Replace("&#8594;", @"→")
                    .Replace("&darr;", @"↓")
                    .Replace("&#8595;", @"↓")
                    .Replace("&harr;", @"↔")
                    .Replace("&#8596;", @"↔")
                    .Replace("&crarr;", @"↵")
                    .Replace("&#8629;", @"↵")
                    .Replace("&lArr;", @"⇐")
                    .Replace("&#8656;", @"⇐")
                    .Replace("&uArr;", @"⇑")
                    .Replace("&#8657;", @"⇑")
                    .Replace("&rArr;", @"⇒")
                    .Replace("&#8658;", @"⇒")
                    .Replace("&dArr;", @"⇓")
                    .Replace("&#8659;", @"⇓")
                    .Replace("&hArr;", @"⇔")
                    .Replace("&#8660;", @"⇔")
                    .Replace("&forall;", @"∀")
                    .Replace("&#8704;", @"∀")
                    .Replace("&part;", @"∂")
                    .Replace("&#8706;", @"∂")
                    .Replace("&exist;", @"∃")
                    .Replace("&#8707;", @"∃")
                    .Replace("&empty;", @"∅")
                    .Replace("&#8709;", @"∅")
                    .Replace("&nabla;", @"∇")
                    .Replace("&#8711;", @"∇")
                    .Replace("&isin;", @"∈")
                    .Replace("&#8712;", @"∈")
                    .Replace("&notin;", @"∉")
                    .Replace("&#8713;", @"∉")
                    .Replace("&ni;", @"∋")
                    .Replace("&#8715;", @"∋")
                    .Replace("&prod;", @"∏")
                    .Replace("&#8719;", @"∏")
                    .Replace("&sum;", @"∑")
                    .Replace("&#8722;", @"∑")
                    .Replace("&minus;", @"−")
                    .Replace("&#8722;", @"−")
                    .Replace("&lowast;", @"∗")
                    .Replace("&#8727;", @"∗")
                    .Replace("&radic;", @"√")
                    .Replace("&#8730;", @"√")
                    .Replace("&prop;", @"∝")
                    .Replace("&#8733;", @"∝")
                    .Replace("&infin;", @"∞")
                    .Replace("&#8734;", @"∞")
                    .Replace("&ang;", @"∠")
                    .Replace("&#8736;", @"∠")
                    .Replace("&and;", @"∧")
                    .Replace("&#8869;", @"∧")
                    .Replace("&or;", @"∨")
                    .Replace("&#8870;", @"∨")
                    .Replace("&cap;", @"∩")
                    .Replace("&#8745;", @"∩")
                    .Replace("&cup;", @"∪")
                    .Replace("&#8746;", @"∪")
                    .Replace("&int;", @"∫")
                    .Replace("&#8747;", @"∫")
                    .Replace("&there4;", @"∴")
                    .Replace("&#8756;", @"∴")
                    .Replace("&sim;", @"∼")
                    .Replace("&#8764;", @"∼")
                    .Replace("&cong;", @"≅")
                    .Replace("&#8773;", @"≅")
                    .Replace("&asymp;", @"≈")
                    .Replace("&#8773;", @"≈")
                    .Replace("&ne;", @"≠")
                    .Replace("&#8800;", @"≠")
                    .Replace("&equiv;", @"≡")
                    .Replace("&#8801;", @"≡")
                    .Replace("&le;", @"≤")
                    .Replace("&#8804;", @"≤")
                    .Replace("&ge;", @"≥")
                    .Replace("&#8805;", @"≥")
                    .Replace("&sub;", @"⊂")
                    .Replace("&#8834;", @"⊂")
                    .Replace("&sup;", @"⊃")
                    .Replace("&#8835;", @"⊃")
                    .Replace("&nsub;", @"⊄")
                    .Replace("&#8836;", @"⊄")
                    .Replace("&sube;", @"⊆")
                    .Replace("&#8838;", @"⊆")
                    .Replace("&supe;", @"⊇")
                    .Replace("&#8839;", @"⊇")
                    .Replace("&oplus;", @"⊕")
                    .Replace("&#8853;", @"⊕")
                    .Replace("&otimes;", @"⊗")
                    .Replace("&#8855;", @"⊗")
                    .Replace("&perp;", @"⊥")
                    .Replace("&#8869;", @"⊥")
                    .Replace("&sdot;", @"⋅")
                    .Replace("&#8901;", @"⋅")
                    .Replace("&lceil;", @"⌈")
                    .Replace("&#8968;", @"⌈")
                    .Replace("&rceil;", @"⌉")
                    .Replace("&#8969;", @"⌉")
                    .Replace("&lfloor;", @"⌊")
                    .Replace("&#8970;", @"⌊")
                    .Replace("&rfloor;", @"⌋")
                    .Replace("&#8971;", @"⌋")
                    .Replace("&loz;", @"◊")
                    .Replace("&#9674;", @"◊")
                    .Replace("&spades;", @"♠")
                    .Replace("&#9824;", @"♠")
                    .Replace("&clubs;", @"♣")
                    .Replace("&#9827;", @"♣")
                    .Replace("&hearts;", @"♥")
                    .Replace("&#9829;", @"♥")
                    .Replace("&diams;", @"♦")
                    .Replace("&#9830;", @"♦")
                    .Replace("&nbsp;", @" ")
                    .Replace("&#160;", @" ")
                    .Replace("&iexcl;", @"¡")
                    .Replace("&#161;", @"¡")
                    .Replace("&cent;", @"¢")
                    .Replace("&#162;", @"¢")
                    .Replace("&pound;", @"£")
                    .Replace("&#163;", @"£")
                    .Replace("&curren;", @"¤")
                    .Replace("&#164;", @"¤")
                    .Replace("&yen;", @"¥")
                    .Replace("&#165;", @"¥")
                    .Replace("&brvbar;", @"¦")
                    .Replace("&#166;", @"¦")
                    .Replace("&sect;", @"§")
                    .Replace("&#167;", @"§")
                    .Replace("&uml;", @"¨")
                    .Replace("&#168;", @"¨")
                    .Replace("&copy;", @"©")
                    .Replace("&#169;", @"©")
                    .Replace("&ordf;", @"ª")
                    .Replace("&#170;", @"ª")
                    .Replace("&laquo;", @"«")
                    .Replace("&#171;", @"«")
                    .Replace("&not;", @"¬")
                    .Replace("&#172;", @"¬")
                    .Replace("&shy;", @"	")
                    .Replace("&#173;", @"	")
                    .Replace("&reg;", @"®")
                    .Replace("&#174;", @"®")
                    .Replace("&macr;", @"¯")
                    .Replace("&#175;", @"¯")
                    .Replace("&deg;", @"°")
                    .Replace("&#176;", @"°")
                    .Replace("&plusmn;", @"±")
                    .Replace("&#177;", @"±")
                    .Replace("&sup2;", @"²")
                    .Replace("&#178;", @"²")
                    .Replace("&sup3;", @"³")
                    .Replace("&#179;", @"³")
                    .Replace("&acute;", @"´")
                    .Replace("&#180;", @"´")
                    .Replace("&micro;", @"µ")
                    .Replace("&#181;", @"µ")
                    .Replace("&quot;", @"""")
                    .Replace("&#34;", @"""")
                    .Replace("&lt;", @"<")
                    .Replace("&#60;", @"<")
                    .Replace("&gt;", @">")
                    .Replace("&#62;", @">")
                    .Replace("&#39;", "'")
                    //html4.0
                    .Replace("&quot;", "\"")
                    .Replace("&#34;", "\"")
                    .Replace("&#x22;", "\"")
                    .Replace("&amp;", "&")
                    .Replace("&#38;", "&")
                    .Replace("&#x26;", "&")
                    .Replace("&lt;", "<")
                    .Replace("&#60;", "<")
                    .Replace("&#x3C;", "<")
                    .Replace("&gt;", ">")
                    .Replace("&#62;", ">")
                    .Replace("&#x3E;", ">")
                    .Replace("&OElig;", "Œ")
                    .Replace("&#338;", "Œ")
                    .Replace("&#x152;", "Œ")
                    .Replace("&oelig;", "œ")
                    .Replace("&#339;", "œ")
                    .Replace("&#x153;", "œ")
                    .Replace("&Scaron;", "Š")
                    .Replace("&#352;", "Š")
                    .Replace("&#x160;", "Š")
                    .Replace("&scaron;", "š")
                    .Replace("&#353;", "š")
                    .Replace("&#x161;", "š")
                    .Replace("&Yuml;", "Ÿ")
                    .Replace("&#376;", "Ÿ")
                    .Replace("&#x178;", "Ÿ")
                    .Replace("&circ;", "ˆ")
                    .Replace("&#710;", "ˆ")
                    .Replace("&#x2C6;", "ˆ")
                    .Replace("&tilde;", "˜")
                    .Replace("&#732;", "˜")
                    .Replace("&#x2DC;", "˜")
                    .Replace("&ensp;", " ")
                    .Replace("&#8194;", " ")
                    .Replace("&#x2002;", " ")
                    .Replace("&emsp;", " ")
                    .Replace("&#8195;", " ")
                    .Replace("&#x2003;", " ")
                    .Replace("&thinsp;", " ")
                    .Replace("&#8201;", " ")
                    .Replace("&#x2009;", " ")
                    .Replace("&zwnj;", "‌")
                    .Replace("&#8204;", "‌")
                    .Replace("&#x200C;", "‌")
                    .Replace("&zwj;", "‍")
                    .Replace("&#8205;", "‍")
                    .Replace("&#x200D;", "‍")
                    .Replace("&lrm;", "‎")
                    .Replace("&#8206;", "‎")
                    .Replace("&#x200E;", "‎")
                    .Replace("&rlm;", "‏")
                    .Replace("&#8207;", "‏")
                    .Replace("&#x200F;", "‏")
                    .Replace("&ndash;", "–")
                    .Replace("&#8211;", "–")
                    .Replace("&#x2013;", "–")
                    .Replace("&mdash;", "—")
                    .Replace("&#8212;", "—")
                    .Replace("&#x2014;", "—")
                    .Replace("&lsquo;", "‘")
                    .Replace("&#8216;", "‘")
                    .Replace("&#x2018;", "‘")
                    .Replace("&rsquo;", "’")
                    .Replace("&#8217;", "’")
                    .Replace("&#x2019;", "’")
                    .Replace("&sbquo;", "‚")
                    .Replace("&#8218;", "‚")
                    .Replace("&#x201A;", "‚")
                    .Replace("&ldquo;", "“")
                    .Replace("&#8220;", "“")
                    .Replace("&#x201C;", "“")
                    .Replace("&rdquo;", "”")
                    .Replace("&#8221;", "”")
                    .Replace("&#x201D;", "”")
                    .Replace("&bdquo;", "„")
                    .Replace("&#8222;", "„")
                    .Replace("&#x201E;", "„")
                    .Replace("&dagger;", "†")
                    .Replace("&#8224;", "†")
                    .Replace("&#x2020;", "†")
                    .Replace("&Dagger;", "‡")
                    .Replace("&#8225;", "‡")
                    .Replace("&#x2021;", "‡")
                    .Replace("&permil;", "‰")
                    .Replace("&#8240;", "‰")
                    .Replace("&#x2030;", "‰")
                    .Replace("&lsaquo;", "‹")
                    .Replace("&#8249;", "‹")
                    .Replace("&#x2039;", "‹")
                    .Replace("&rsaquo;", "›")
                    .Replace("&#8250;", "›")
                    .Replace("&#x203A;", "›")
                    .Replace("&euro;", "€")
                    .Replace("&#8364;", "€")
                    .Replace("&#x20AC;", "€")
                    ;
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string UrlEncode(this string str, Encoding encoding)
        {
            return HttpUtility.UrlEncode(str, encoding);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string UrlDecode(this string str, Encoding encoding)
        {
            return HttpUtility.UrlDecode(str, encoding);
        }

        /// <summary>
        /// 返回 URL 地址字符串的编码结果
        /// </summary>
        /// <param name="URL地址">URL路径字符串</param>
        /// <returns>编码结果</returns>
        // ReSharper disable once InconsistentNaming
        public static string UrlPathEncode(this string URL地址)
        {
            return HttpUtility.UrlPathEncode(URL地址);
        }
    }
}
