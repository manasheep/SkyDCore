using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDCore.Log
{
    /// <summary>
    /// 不含脚本的HTML日志
    /// </summary>
    public class HtmlLogLayoutNoScript : HtmlLogLayout
    {
        protected override string _Script => String.Empty;
    }
}
