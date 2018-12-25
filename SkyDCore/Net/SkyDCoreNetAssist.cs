using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using SkyDCore.Text;

namespace SkyDCore.Net
{
    /// <summary>
    /// 网络帮助类
    /// </summary>
    public static class SkyDCoreNetAssist
    {
        /// <summary>
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="ipAddress">要进行验证的IP地址</param>
        /// <param name="ipNetworkSegmentArray">作为验证依据的IP网段数组</param>
        /// <returns>是否匹配</returns>
        public static bool ValidationIPNetworkSegment(string ipAddress, string[] ipNetworkSegmentArray)
        {
            string[] userip = ipAddress.Split(@".");
            for (int ipIndex = 0; ipIndex < ipNetworkSegmentArray.Length; ipIndex++)
            {
                string[] tmpip = ipNetworkSegmentArray[ipIndex].Split(@".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将IP地址转为整数形式
        /// </summary>
        /// <returns>整数</returns>
        public static long ToInt64(this IPAddress ip)
        {
            int x = 3;
            long o = 0;
            foreach (byte f in ip.GetAddressBytes())
            {
                o += (long)f << 8 * x--;
            }
            return o;
        }

        /// <summary>
        /// 将整数转为IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static IPAddress ToIPAddress(this long l)
        {
            var b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                b[3 - i] = (byte)(l >> 8 * i & 255);
            }
            return new IPAddress(b);
        }

        /// <summary>
        /// 通过域名获得IP地址
        /// </summary>
        /// <param name="hostDomainName">要查看的域名</param>
        /// <returns>IP地址列表</returns>
        public static IPAddress[] GetIPAddress(string hostDomainName)
        {
            return Dns.GetHostAddresses(hostDomainName);
        }

        /// <summary>
        /// 验证字符串是否符合IP地址规则，如：192.168.0.1
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>是否符合IP地址规则</returns>
        public static bool ValidationIsIPAddress(this string s)
        {
            return s.RegexIsMatch(@"^(((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))$");
        }

        /// <summary>
        /// 获取字符串的IP地址及端口号匹配项，如：192.168.0.1:8080，如果匹配成功的话，组$1代表IP地址部分，组$11代表端口部分
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>IP地址及端口号匹配项</returns>
        public static Match GetIPAddressAndPortMatch(this string s)
        {
            return s.RegexMatch(@"^(((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))):([0-9]|[1-9]\d{1}|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$");
        }
    }
}
