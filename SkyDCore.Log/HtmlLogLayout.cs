using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net.Core;

namespace SkyDCore.Log
{
    public class HtmlLogLayout : log4net.Layout.LayoutSkeleton
    {
        private static int _Id;
        private static string _Script => @"
<script type=""text/javascript"">
function dynamicLoadCss() {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.type='text/css';
        link.rel = 'stylesheet';
        link.href = 'https://cdn.bootcss.com/bootstrap/4.0.0/css/bootstrap.min.css';
        head.appendChild(link);
    }

dynamicLoadCss();
</script>";

        public HtmlLogLayout()
        {
            this.Header = $"{_Script}<h4 style=\"margin:64px 8px 24px 8px;\">&gt; 日志记录开始 <small class=\"text-muted\">{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}</small></h1>";
            this.Footer = $"<h4 style=\"margin:24px 8px 64px 8px;\">&lt; 日志记录结束 <small class=\"text-muted\">{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}</small>";
            this.IgnoresException = false;
        }

        public override void ActivateOptions()
        {

        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var textType = "muted";
            if (loggingEvent.Level == Level.Info)
            {
                textType = "info";
            }
            else if (loggingEvent.Level == Level.Warn)
            {
                textType = "warning";
            }
            else if (loggingEvent.Level == Level.Error || loggingEvent.Level == Level.Fatal)
            {
                textType = "danger";
            }
            //else if (loggingEvent.Level == Level.Fine)
            //{
            //    textType = "success";
            //}

            var bgType = textType == "muted" ? "secondary" : textType;
            string exception = null;
            if (loggingEvent.ExceptionObject != null)
            {
                exception = $"<pre class=\"text-danger\"><code>{System.Web.HttpUtility.HtmlEncode(loggingEvent.ExceptionObject.ToString())}</code></pre>";
            }
            writer.Write($"<div style=\"word-break:break-all;margin:8px;\" class=\"alert alert-{bgType}\" role=\"alert\"><strong class=\"text-{textType}\">{loggingEvent.Level}</strong> <span>#{DateTime.Now.ToFileTime()}{++_Id}</span> <strong>{loggingEvent.LoggerName}</strong> [{loggingEvent.ThreadName}] <span class=\"text-muted\">{loggingEvent.TimeStamp:yyyy-MM-dd tthh:mm:ss,fff}</span><br/><span>{System.Web.HttpUtility.HtmlEncode(loggingEvent.MessageObject.ToString())}</span>{exception}</div>");
        }
    }
}
