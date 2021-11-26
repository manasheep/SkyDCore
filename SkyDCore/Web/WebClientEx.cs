using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SkyDCore.Web
{
    /// <summary>
    /// WebClient的扩展
    /// </summary>
    public class WebClientEx : WebClient
    {
        /// <summary>
        /// 请求超时设置，单位为毫秒，默认为10000。限制从发出请求开始算起，到与服务器建立连接后收到Http响应头的时间。
        /// </summary>
        public int Timeout { get; set; } = 10000;

        /// <summary>
        /// 读写超时设置，单位为毫秒，默认为60000。针对HTTP请求，限制从建立连接开始，到下载数据完毕所历经的时间。
        /// </summary>
        public int ReadWriteTimeout { get; set; } = 60000;

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = Timeout;
            var rq = request as HttpWebRequest;
            if (rq != null)
            {
                rq.ReadWriteTimeout = ReadWriteTimeout;
            }
            return request;
        }
    }
}
