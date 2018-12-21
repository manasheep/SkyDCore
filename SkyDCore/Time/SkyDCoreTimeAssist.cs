using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SkyDCore.Text;
using SkyDCore.Mathematics;

namespace SkyDCore.Time
{
    /// <summary>
    /// 时间粒度
    /// </summary>
    public enum TimeGranularity
    {
        [Description("毫秒")]
        Millisecond,
        [Description("秒")]
        Second,
        [Description("分钟")]
        Minute,
        [Description("小时")]
        Hour,
        [Description("天")]
        Day,
        [Description("星期")]
        Week,
        [Description("月")]
        Month,
        [Description("年")]
        Year
    }

    /// <summary>
    /// 时间辅助类
    /// </summary>
    public static class SkyDCoreTimeAssist
    {
        /// <summary>
        /// 转换为常用日期格式方法，如：2006年11月3日 21:35:09
        /// </summary>
        public static string ToNormalDateTimeFormat(this DateTime t)
        {
            return t.ToString(SkyDCoreTextAssist.NormalDateTimeFormat);
        }

        /// <summary>
        /// 转换为常用日期格式方法，如：2006年11月3日
        /// </summary>
        public static string ToNormalDateFormat(this DateTime t)
        {
            return t.ToString(SkyDCoreTextAssist.NormalDateFormat);
        }

        /// <summary>
        /// 转换为标准日期时间格式方法，如：2006-11-03 21:35:09
        /// </summary>
        public static string ToStandardDateTimeFormat(this DateTime t)
        {
            return t.ToString(SkyDCoreTextAssist.StandardDateTimeFormat);
        }

        /// <summary>
        /// 转换为标准日期格式方法，如：2006-11-03
        /// </summary>
        public static string ToStandardDateFormat(this DateTime t)
        {
            return t.ToString(SkyDCoreTextAssist.StandardDateFormat);
        }

        /// <summary>
        /// 转换为标准时间格式方法，如：21:35:09
        /// </summary>
        public static string ToStandardTimeFormat(this DateTime t)
        {
            return t.ToString(SkyDCoreTextAssist.StandardTimeFormat);
        }

        /// <summary>
        /// 转换为日期时间编码，如：20080802191521986
        /// </summary>
        public static string ToDateTimeCode(this DateTime t)
        {
            return string.Format("{0}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}{6:d3}", t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second, t.Millisecond);
        }

        /// <summary>
        /// 如48:55:12
        /// </summary>
        public static string ToStandardTimeFormat(this TimeSpan t)
        {
            return string.Format("{0}:{1}:{2}", (int)t.TotalHours, t.Minutes, t.Seconds);
        }

        /// <summary>
        /// 如48小时55分12秒
        /// </summary>
        public static string ToNormalTimeFormat(this TimeSpan t)
        {
            return string.Format("{0}小时{1}分{2}秒", (int)t.TotalHours, t.Minutes, t.Seconds);
        }

        /// <summary>
        /// 输出友好形式的时间字符串，如：65天17小时56分 或 12秒357毫秒 或 229分钟49秒
        /// </summary>
        /// <param name="accuracy">指示统计的精确程度，该值不可高于粒度</param>
        /// <param name="granularity">指示统计的最大粒度，该值不可低于精确度</param>
        /// <param name="isKeepThePrefix">指示是否保留为0的前置高粒度位，如：0小时0分钟29秒311毫秒，否则显示为：29秒311毫秒</param>
        /// <param name="isKeepTheMostSignificantNumber">指示是否只保留最高位，如：6天 或 120分钟</param>
        /// <returns>友好形式的时间字符串</returns>
        public static string ToFriendlyDateTimeFormat(this TimeSpan t, TimeGranularity accuracy, TimeGranularity granularity, bool isKeepThePrefix, bool isKeepTheMostSignificantNumber)
        {
            return ToFriendlyDateTimeFormat(t, accuracy, granularity, false, isKeepThePrefix, isKeepTheMostSignificantNumber);
        }

        /// <summary>
        /// 输出友好形式的时间字符串，如：65天17小时56分 或 12秒357毫秒 或 229分钟49秒
        /// </summary>
        /// <param name="accuracy">指示统计的精确程度，该值不可高于粒度</param>
        /// <param name="granularity">指示统计的最大粒度，该值不可低于精确度</param>
        /// <param name="roundOff">当精确度和粒度相等时，指示值是否应当进行四舍五入</param>
        /// <param name="isKeepThePrefix">指示是否保留为0的前置高粒度位，如：0小时0分钟29秒311毫秒，否则显示为：29秒311毫秒</param>
        /// <param name="isKeepTheMostSignificantNumber">指示是否只保留最高位，如：6天 或 120分钟 或 9年</param>
        /// <returns>友好形式的时间字符串</returns>
        public static string ToFriendlyDateTimeFormat(this TimeSpan t, TimeGranularity accuracy, TimeGranularity granularity, bool roundOff, bool isKeepThePrefix, bool isKeepTheMostSignificantNumber)
        {
            if (granularity < accuracy) throw new Exception("粒度不得小于精确度");
            StringBuilder s = new StringBuilder();
            s.Append(Output(TimeGranularity.Year, "年", t.Years(), t.TotalYears(), accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Month, "个月", t.Months(), t.TotalMonths(), accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Week, "星期", t.Weeks(), t.TotalWeeks(), accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Day, "天", t.Days(), t.TotalDays, accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Hour, "小时", t.Hours, t.TotalHours, accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Minute, "分钟", t.Minutes, t.TotalMinutes, accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Second, "秒", t.Seconds, t.TotalSeconds, accuracy, granularity, roundOff, isKeepThePrefix));
            if (isKeepTheMostSignificantNumber && s.Length > 0) goto Output;
            s.Append(Output(TimeGranularity.Millisecond, "毫秒", t.Milliseconds, t.TotalMilliseconds, accuracy, granularity, roundOff, isKeepThePrefix));
        //if (仅保留最高有效位 && s.Length > 0) goto Output;
        Output: return s.ToString();
        }

        static string Output(TimeGranularity level, string showName, int showValue, double totalValue, TimeGranularity accuracy, TimeGranularity granularity, bool roundOff, bool isKeepTheMostSignificantNumber)
        {
            if (accuracy > level || granularity < level || (!isKeepTheMostSignificantNumber && totalValue < 1)) return null;
            if (granularity > level) return showValue + showName;
            return (roundOff && accuracy == granularity ? totalValue.RoundOff(0) : (int)totalValue) + showName;
        }

        public static int Days(this TimeSpan t)
        {
            return t.Days % 365 % 30 % 7;
        }

        public static int Weeks(this TimeSpan t)
        {
            return t.Days % 365 % 30 / 7;
        }

        public static int Months(this TimeSpan t)
        {
            return t.Days % 365 / 30;
        }

        public static int Years(this TimeSpan t)
        {
            return t.Days / 365;
        }

        public static double TotalWeeks(this TimeSpan t)
        {
            return t.TotalDays / 7;
        }

        public static double TotalMonths(this TimeSpan t)
        {
            return t.TotalDays / 30;
        }

        public static double TotalYears(this TimeSpan t)
        {
            return t.TotalDays / 365;
        }

    }
}
