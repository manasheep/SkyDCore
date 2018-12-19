using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace SkyDCore.Text
{
    public class UriString : SpecialString
    {
        /// <summary>
        /// 将绝对Uri与相对路径组合。
        /// 如果传入的是绝对路径，则原样返回。
        /// 通常用于处理网页内的相对路径超链接，如将“http://abc.com”或“http://abc.com/index.htm”与“info.htm”组合的话，就会生成“http://abc.com/info.htm”
        /// </summary>
        /// <param name="RelativePath">待组合的相对路径，可以是“../abc.htm”形式</param>
        public string CombineRelativePath(string RelativePath)
        {
            //try
            //{
            //    return new Uri(RelativePath).AbsoluteUri;
            //}
            //catch
            //{
            //var u = new Uri(Value);
            //return new Uri(u.LocalPath.EndsWith("/") ? u : new Uri(Path.GetDirectoryName(Value).Replace(@"\", "/").Replace(":/", "://")), RelativePath).AbsoluteUri;
            return new Uri(new Uri(Value), RelativePath).AbsoluteUri;
            //}
        }

        /// <summary>
        /// 转换本地文件Uri为本地路径格式。
        /// 如“file:///C:/abc/avatar.xml”将被转换为“C:\abc\avatar.xml”。
        /// 如果该Uri不是本地文件Uri，那么将抛出异常。
        /// </summary>
        public string ToLocalFilePath()
        {
            if (!Value.ToLower().StartsWith("file:///")) throw new Exception("这不是一个本地文件Uri");
            return Value.Substring(8).Replace("/", "\\");
        }

        /// <summary>
        /// 获取Url参数名值字典
        /// </summary>
        public Dictionary<string, string> UrlParameterDictionary
        {
            get
            {
                var u = new Uri(Value);
                var d = new Dictionary<string, string>();
                foreach (var f in Regex.Split(u.Query, @"\&"))
                {
                    if (f.IsNullOrEmpty())
                    {
                        continue;
                    }
                    var q = Regex.Split(f, @"\=");
                    d.Add(q[0], q[1]);
                }
                return d;
            }
        }

        /// <summary>
        /// 以新的名值参数字典值替代当前Url参数
        /// </summary>
        /// <param name="替代Url参数名值字典">新的参数字典，如已有相同参数则覆盖为此字典中的新值</param>
        /// <param name="追加原参数中不存在的参数">当前参数中不存在新字典中的某参数时，是否予以追加</param>
        /// <param name="舍弃替代参数中没有的参数">是否将新参数字典中没有的参数舍弃</param>
        /// <returns>新的完整Url</returns>
        public string ReplaceUrlParameters(Dictionary<string, string> 替代Url参数名值字典, bool 追加原参数中不存在的参数, bool 舍弃替代参数中没有的参数)
        {
            var u = UrlParameterDictionary;
            foreach (var f in 替代Url参数名值字典.Keys)
            {
                if (追加原参数中不存在的参数 && !f.IsAnyMatch((q, c) => q.ToLower() == c.ToLower(), u.Keys.Cast<string>().ToArray()))
                {
                    u.Add(f, 替代Url参数名值字典[f]);
                }
            }
            foreach (var f in u.Keys.ToArray())
            {
                var t = 替代Url参数名值字典.Keys.FirstOrDefault(q => q.ToLower() == f.ToLower());
                if (t != null) u[f] = 替代Url参数名值字典[t];
                else if (舍弃替代参数中没有的参数)
                {
                    u.Remove(f);
                }
            }
            StringBuilder s = new StringBuilder();
            foreach (var f in u.Keys)
            {
                if (s.Length > 0)
                {
                    s.Append('&');
                }
                s.Append(f + "=" + u[f]);
            }
            return Regex.Replace(Value, @"\?.*$", "") + "?" + s;
        }

        /// <summary>
        /// 添加一个参数，如果参数已存在，其将被替换为新值
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <param name="参数值">参数值</param>
        /// <returns>新的UriString对象</returns>
        public UriString AddUrlParameter(string 参数名, string 参数值)
        {
            var d = new Dictionary<string, string>();
            d.Add(参数名, 参数值);
            return ReplaceUrlParameters(d, true, false).AsUriString();
        }

        /// <summary>
        /// 替换一个参数为新值，如果当前此参数不存在，则忽略
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <param name="参数值">参数值</param>
        /// <returns>新的UriString对象</returns>
        public UriString ReplaceUrlParameter(string 参数名, string 参数值)
        {
            var d = new Dictionary<string, string>();
            d.Add(参数名, 参数值);
            return ReplaceUrlParameters(d, false, false).AsUriString();
        }

        /// <summary>
        /// 移除一个参数，如果参数本身即不存在，则忽略
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <returns>新的UriString对象</returns>
        public UriString RemoveUrlParameter(string 参数名)
        {
            var u = UrlParameterDictionary;
            foreach (var f in u.Keys)
            {
                if (f.ToLower() == 参数名.ToLower())
                {
                    u.Remove(f);
                    break;
                }
            }
            return ReplaceUrlParameters(u, false, true).AsUriString();
        }
    }
}
