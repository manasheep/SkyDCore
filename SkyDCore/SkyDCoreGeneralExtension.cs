using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using SkyDCore.Text;
using SkyDCore.General;

/// <summary>
/// 通用扩展
/// </summary>
public static class SkyDCoreGeneralExtension
{
    static Random R = new Random();

    /// <summary>
    /// 转换Base64字符串为字节数组，如果格式有误则输出null
    /// </summary>
    /// <param name="base64String">Base64字符串</param>
    /// <returns>字节数组</returns>
    public static byte[] ConvertBase64StringToByteArray(this string base64String)
    {
        try
        {
            return Convert.FromBase64String(base64String);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// 转换字节数组为Base64字符串
    /// </summary>
    /// <param name="byteArray">字节数组</param>
    /// <returns>Base64字符串</returns>
    public static string ConvertToBase64String(this byte[] byteArray)
    {
        return Convert.ToBase64String(byteArray);
    }

    /// <summary>
    /// 追加项到末尾，并返回自身
    /// </summary>
    public static IList<T> AddByLink<T>(this IList<T> o, T item)
    {
        o.Add(item);
        return o;
    }

    /// <summary>
    /// 追加项到末尾，并返回自身
    /// </summary>
    public static IList<T> AddByLink<T>(this IList<T> o, params T[] items)
    {
        foreach (var f in items)
        {
            o.Add(f);
        }
        return o;
    }

    /// <summary>
    /// 追加集合到末尾，并返回自身
    /// </summary>
    public static IList<T> AddRangeByLink<T>(this IList<T> o, IEnumerable<T> items)
    {
        foreach (var f in items)
        {
            o.Add(f);
        }
        return o;
    }

    /// <summary>
    /// 连接集合中的所有数组，组成一个完整的大数组
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">数组集合</param>
    /// <returns>完整的大数组</returns>
    public static T[] ConnectAllArrays<T>(this IEnumerable<T[]> o)
    {
        var enumerable = o as T[][] ?? o.ToArray();
        var array = new T[enumerable.Sum(q => q.Length)];
        var x = 0;
        foreach (var f in enumerable)
        {
            Buffer.BlockCopy(f, 0, array, x, f.Length);
            x += f.Length;
        }
        return array;
    }

    /// <summary> 
    /// 将 Stream 转成 byte[] 
    /// </summary> 
    public static byte[] ToBytes(this Stream stream)
    {
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);

        // 设置当前流的位置为流的开始 
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        return bytes;
    }

    /// <summary> 
    /// 将 byte[] 转成 MemoryStream 
    /// </summary> 
    public static MemoryStream ToMemoryStream(this byte[] bytes)
    {
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// 将对象强制转换为布尔类型并返回，如果对象为空则返回false
    /// </summary>
    /// <param name="o">目标对象</param>
    /// <returns>布尔值</returns>
    public static bool AsBoolean(this object o)
    {
        return o != null && (bool)o;
    }

    /// <summary>
    /// 执行并释放对象，同using(……)关键字
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">目标对象</param>
    /// <param name="action">要执行的操作</param>
    public static void UsingRun<T>(this T o, Action<T> action) where T : IDisposable
    {
        using (o)
        {
            action(o);
        }
    }

    /// <summary>
    /// 执行并返回值，然后释放对象，同using(……)关键字
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <typeparam name="TR">返回值类型</typeparam>
    /// <param name="o">目标对象</param>
    /// <param name="action">要执行的操作</param>
    public static TR UsingRunAndReturn<T, TR>(this T o, Func<T, TR> action) where T : IDisposable
    {
        using (o)
        {
            return action(o);
        }
    }

    /// <summary>
    /// 根据布尔值分别返回不同结果
    /// </summary>
    /// <typeparam name="T">返回结果类型</typeparam>
    /// <param name="b">布尔值变量</param>
    /// <param name="tureResult">当为真时返回结果</param>
    /// <param name="falseResult">当为假时返回结果</param>
    /// <returns>返回结果</returns>
    public static T SwitchReturn<T>(this bool b, T tureResult, T falseResult)
    {
        return b ? tureResult : falseResult;
    }

    /// <summary>
    /// 根据可空布尔值分别返回不同结果
    /// </summary>
    /// <typeparam name="T">返回结果类型</typeparam>
    /// <param name="b">布尔值变量</param>
    /// <param name="tureResult">当为真时返回结果</param>
    /// <param name="falseResult">当为假时返回结果</param>
    /// <param name="nullResult">当为空时返回结果</param>
    /// <returns>返回结果</returns>
    public static T SwitchReturn<T>(this bool? b, T tureResult, T falseResult, T nullResult)
    {
        return b == null ? nullResult : b.Value.SwitchReturn(tureResult, falseResult);
    }

    /// <summary>
    /// 判断此GUID变量是否为默认值
    /// </summary>
    /// <param name="guid">GUID变量</param>
    /// <returns>是否为默认值</returns>
    public static bool IsDefaultValue(this Guid guid)
    {
        return guid == new Guid();
    }

    /// <summary>
    /// 获取集合的子集
    /// </summary>
    /// <param name="tc">目标集合</param>
    /// <param name="subsetStartIndex">子集由此索引开始</param>
    /// <param name="subsetLength">子集从起始位置获得此数量的项目</param>
    /// <returns>子集</returns>
    public static IEnumerable GetSubset(this IEnumerable tc, int subsetStartIndex, int subsetLength)
    {
        var x = 0;
        var max = subsetStartIndex + subsetLength;
        foreach (var item in tc)
        {
            if (x >= subsetStartIndex)
            {
                if (x >= max) break;
                yield return item;
            }
            x++;
        }
    }

    /// <summary>
    /// 获取集合的子集
    /// </summary>
    /// <param name="tc">目标集合</param>
    /// <param name="subsetStartIndex">子集由此索引开始</param>
    /// <param name="subsetLength">子集从起始位置获得此数量的项目</param>
    /// <returns>子集</returns>
    public static IEnumerable<T> GetSubset<T>(this IEnumerable<T> tc, int subsetStartIndex, int subsetLength)
    {
        var x = 0;
        var max = subsetStartIndex + subsetLength;
        foreach (var item in tc)
        {
            if (x >= subsetStartIndex)
            {
                if (x >= max) break;
                yield return item;
            }
            x++;
        }
    }

    /// <summary>
    /// 当对象非空时返回其ToString()方法，否则返回空字符串String.Empty
    /// </summary>
    /// <param name="o">对象</param>
    /// <returns>字符串形式</returns>
    public static string ToStringSafety(this object o)
    {
        if (o == null) return String.Empty;
        else return o.ToString();
    }

    /// <summary>
    /// 乱序步进循环操作
    /// </summary>
    /// <param name="i"></param>
    /// <param name="maxValue">最大值</param>
    /// <param name="stepValue">步进值</param>
    /// <param name="behavior">循环操作行为</param>
    public static void RandomFor(this int i, int maxValue, int stepValue, Action<int> behavior)
    {
        var li = new List<int>();
        for (int j = i; j < maxValue; j += stepValue)
        {
            li.Add(j);
        }
        foreach (var f in li.Random())
        {
            behavior(f);
        }
    }

    /// <summary>
    /// 添加对象的字符串形式到末尾新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="obj">要添加的对象，取用其字符串形式</param>
    /// <returns>组合的字符串</returns>
    public static string AppendLine(this string s, object obj)
    {
        return s + "\r\n" + obj;
    }

    /// <summary>
    /// 添加多个对象的字符串形式到末尾，每个对象都置于新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="obj">要添加的对象，取用其字符串形式</param>
    /// <returns>组合的字符串</returns>
    public static string AppendLine(this string s, params object[] obj)
    {
        var sb = new StringBuilder();
        sb.AppendLine(s);
        foreach (var f in obj)
        {
            sb.AppendLine(f == null ? "null" : f.ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 判断是否为空引用或空集合
    /// </summary>
    /// <returns>是否为空引用或空集合</returns>
    public static bool IsNullOrEmpty(this IEnumerable o)
    {
        return o == null || o.GetEnumerator().MoveNext() == false;
    }

    /// <summary>
    /// 判断是否为空引用或空集合
    /// </summary>
    /// <returns>是否为空引用或空集合</returns>
    public static bool IsNullOrEmpty(this IEnumerator o)
    {
        return o == null || o.MoveNext() == false;
    }

    /// <summary>
    /// 输出为UTC时间的ISO8601格式字符串，例如：2010-08-20T15:00:00Z
    /// </summary>
    /// <param name="s"></param>
    /// <returns>UTC时间的ISO8601格式字符串</returns>
    public static string ToUtcStringByISO8601(this DateTime s)
    {
        return s.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
    }

    /// <summary>
    /// 等同于：对象 as T
    /// </summary>
    public static T As<T>(this object o) where T : class
    {
        return o as T;
    }

    /// <summary>
    /// 同原生的Add方法，区别在于添加项后会返回自身，使得允许链式编程以连续添加项。
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="d">字典</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>自身</returns>
    public static IDictionary<TKey, TValue> AddByLink<TKey, TValue>(this IDictionary<TKey, TValue> d, TKey key, TValue value)
    {
        d.Add(key, value);
        return d;
    }

    /// <summary>
    /// 同原生的Add方法，区别在于添加项后会返回自身，使得允许链式编程以连续添加项。
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="c">键值对集合</param>
    /// <param name="items">值数组</param>
    /// <returns>自身</returns>
    public static ICollection<KeyValuePair<TKey, TValue>> AddByLink<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> c, params KeyValuePair<TKey, TValue>[] items)
    {
        foreach (var f in items)
        {
            c.Add(f);
        }
        return c;
    }

    /// <summary>
    /// 同原生的Add方法，区别在于添加项后会返回自身，使得允许链式编程以连续添加项。
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="c">键值对集合</param>
    /// <param name="items">值集合</param>
    /// <returns>自身</returns>
    public static ICollection<KeyValuePair<TKey, TValue>> AddByLink<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> c, IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
        foreach (var f in items)
        {
            c.Add(f);
        }
        return c;
    }

    /// <summary>
    /// 添加内容到StringBuilder末尾，如果此StringBuilder中已有内容，则会先添加一个分隔符，再添加内容。
    /// </summary>
    /// <param name="obj">要添加到末尾的内容</param>
    /// <param name="separator">分隔符，例如顿号、逗号、分号、空格等</param>
    /// <returns></returns>
    public static StringBuilder Append(this StringBuilder s, object obj, string separator)
    {
        return s.AppendAndSeparated(obj, separator);
    }

    /// <summary>
    /// 添加内容到StringBuilder末尾，如果此StringBuilder中已有内容，则会先添加一个分隔符，再添加内容。
    /// </summary>
    /// <param name="obj">要添加到末尾的内容</param>
    /// <param name="separator">分隔符，例如顿号、逗号、分号、空格等</param>
    /// <returns></returns>
    public static StringBuilder AppendAndSeparated(this StringBuilder s, object obj, string separator)
    {
        if (s.Length > 0) s.Append(separator);
        s.Append(obj);
        return s;
    }

    /// <summary>
    /// 将集合展开并以ToString形式拼接
    /// </summary>
    /// <param name="separator">分隔符，拼接时的间隔字符</param>
    /// <returns>拼接后的字符串</returns>
    public static string ExpandAndToString(this IEnumerable s, string separator)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var f in s)
        {
            if (sb.Length > 0) sb.Append(separator);
            sb.Append(f.ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 如果字符串内容是Uri，则返回此Uri的ToString()形式，否则将视为本地路径，从而转换为“file:///”形式返回。
    /// </summary>
    public static string ToUrl(this string s)
    {
        try
        {
            return new Uri(s).ToString();
        }
        catch
        {
            return new Uri(s.AsPathString().ToLocalFileUri()).ToString();
        }
    }

    /// <summary>
    /// 反转逻辑值
    /// </summary>
    public static bool Reverse(this bool b)
    {
        return !b;
    }

    /// <summary>
    /// 检测字符串是否为null或空字符串
    /// </summary>
    public static bool IsNullOrEmpty(this string s)
    {
        return string.IsNullOrEmpty(s);
    }

    /// <summary>
    /// 检测字符串是否为null或空白字符串（Trim()之后长度为0）
    /// </summary>
    public static bool IsNullOrEmptyOrWhitespace(this string s)
    {
        return string.IsNullOrEmpty(s) || s.Trim().Length == 0;
    }

    /// <summary>
    /// 当字符串为null或空字符串时执行自定义表达式
    /// </summary>
    public static void IsNullOrEmptyThen(this string s, Action<string> action)
    {
        if (string.IsNullOrEmpty(s)) action(s);
    }

    /// <summary>
    /// 当字符串为null或空字符串时执行自定义表达式，并返回处理后的字符串；否则返回其自身
    /// </summary>
    public static string IsNullOrEmptyThen(this string s, Func<string, string> func)
    {
        if (string.IsNullOrEmpty(s)) return func(s);
        return s;
    }

    /// <summary>
    /// 当字符串为null或空字符串时返回指定的字符串；否则返回其自身
    /// </summary>
    public static string IsNullOrEmptyThen(this string s, string replaceString)
    {
        if (string.IsNullOrEmpty(s)) return replaceString;
        return s;
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, object params1)
    {
        return string.Format(s, params1);
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, object params2, object params3)
    {
        return string.Format(s, params2, params3);
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, object params1, object params2, object params3)
    {
        return string.Format(s, params1, params2, params3);
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, params object[] paramsArray)
    {
        return string.Format(s, paramsArray);
    }

    /// <summary>
    /// 验证是否匹配
    /// </summary>
    public static bool RegexIsMatch(this string s, string regularExpression)
    {
        return Regex.IsMatch(s, regularExpression);
    }

    /// <summary>
    /// 验证是否匹配
    /// </summary>
    public static bool RegexIsMatch(this string s, string regularExpression, RegexOptions options)
    {
        return Regex.IsMatch(s, regularExpression, options);
    }

    /// <summary>
    /// 获取一个匹配项
    /// </summary>
    public static Match RegexMatch(this string s, string regularExpression)
    {
        return Regex.Match(s, regularExpression);
    }

    /// <summary>
    /// 获取一个匹配项
    /// </summary>
    public static Match RegexMatch(this string s, string regularExpression, RegexOptions options)
    {
        return Regex.Match(s, regularExpression, options);
    }

    /// <summary>
    /// 获取所有匹配项
    /// </summary>
    public static MatchCollection RegexMatches(this string s, string regularExpression)
    {
        return Regex.Matches(s, regularExpression);
    }

    /// <summary>
    /// 获取所有匹配项
    /// </summary>
    public static MatchCollection RegexMatches(this string s, string regularExpression, RegexOptions options)
    {
        return Regex.Matches(s, regularExpression, options);
    }

    /// <summary>
    /// 以匹配项拆分字符串
    /// </summary>
    public static string[] RegexSplit(this string s, string regularExpression)
    {
        return Regex.Split(s, regularExpression);
    }

    /// <summary>
    /// 以匹配项拆分字符串
    /// </summary>
    public static string[] RegexSplit(this string s, string regularExpression, RegexOptions options)
    {
        return Regex.Split(s, regularExpression, options);
    }

    /// <summary>
    /// 替换匹配项为新值
    /// </summary>
    public static string RegexReplace(this string s, string regularExpression, string replaceValue)
    {
        return Regex.Replace(s, regularExpression, replaceValue);
    }

    /// <summary>
    /// 替换匹配项为新值
    /// </summary>
    public static string RegexReplace(this string s, string regularExpression, string replaceValue, RegexOptions options)
    {
        return Regex.Replace(s, regularExpression, replaceValue, options);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他任意某个字符串
    /// </summary>
    public static bool IsContainsAny(this string s, int startIndex, int checkCharNumber, StringComparison rule, params string[] stringArray)
    {
        return s.IsAnyMatch((a, b) => a.IndexOf(b, startIndex, checkCharNumber, rule) >= 0, stringArray);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他任意某个字符串，忽略大小写差异
    /// </summary>
    public static bool IsContainsAny(this string s, params string[] stringArray)
    {
        return s.IsContainsAny(0, s.Length, StringComparison.OrdinalIgnoreCase, stringArray);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他所有字符串
    /// </summary>
    public static bool IsContainsAll(this string s, int startIndex, int checkCharNumber, StringComparison rule, params string[] stringArray)
    {
        return s.IsAllMatch((a, b) => a.IndexOf(b, startIndex, checkCharNumber, rule) >= 0, stringArray);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他所有字符串，忽略大小写差异
    /// </summary>
    public static bool IsContainsAll(this string s, params string[] stringArray)
    {
        return s.IsContainsAll(0, s.Length, StringComparison.OrdinalIgnoreCase, stringArray);
    }

    /// <summary>
    /// 执行动作后返回自身。适用于链式编程。
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="action">一个无返回值动作</param>
    /// <returns>自身</returns>
    public static T Do<T>(this T o, Action<T> action)
    {
        action(o);
        return o;
    }

    /// <summary>
    /// 依据权重值调整概率，从集合中随机抽取一个项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">集合</param>
    /// <param name="getWeightedFunc">获取权重值的方法</param>
    /// <returns>随机抽取到的一个项</returns>
    public static T RandomExtractOneByWeighted<T>(this IEnumerable<T> o, Func<T, int> getWeightedFunc)
    {
        var enumerable = o.ToArray();
        var total = enumerable.Sum(getWeightedFunc);
        var r = R.Next(total);
        var v = 0;
        foreach (var f in enumerable)
        {
            var fw = getWeightedFunc(f);
            if (v <= r && v + fw > r)
            {
                return f;
            }
            v += fw;
        }
        return enumerable.FirstOrDefault();
    }

    /// <summary>
    /// 返回经随机排序后的集合
    /// </summary>
    public static IEnumerable<T> Random<T>(this IEnumerable<T> o)
    {
        //var c = o.Count();
        //var l = new List<int>();
        //for (int i = 0; i < c; i++)
        //{
        //    l.Add(i);
        //}
        //while (l.Count > 0)
        //{
        //    var i = l[R.Next(l.Count)];
        //    l.Remove(i);
        //    yield return o.ElementAt(i);
        //}
        return o.OrderBy(q => R.Next());
    }

    /// <summary>
    /// 判断一个值是否存在于提供的多个值中
    /// </summary>
    public static bool IsIn<T>(this T t, params T[] values)
    {
        return values.Contains(t);
    }

    /// <summary>
    /// 判断一个值是否存在于提供的多个值中
    /// </summary>
    public static bool IsIn<T>(this T t, IEnumerable<T> values)
    {
        return values.Contains(t);
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的某一项
    /// </summary>
    /// <param name="predicateFunc">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAnyMatch<T, C>(this T t, Func<T, C, bool> predicateFunc, params C[] values)
    {
        return values.Any(f => predicateFunc(t, f));
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的某一项
    /// </summary>
    /// <param name="predicateFunc">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAnyMatch<T, C>(this T t, Func<T, C, bool> predicateFunc, IEnumerable<C> values)
    {
        return values.Any(f => predicateFunc(t, f));
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的所有项
    /// </summary>
    /// <param name="predicateFunc">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAllMatch<T, C>(this T t, Func<T, C, bool> predicateFunc, params C[] values)
    {
        return values.All(f => predicateFunc(t, f));
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的所有项
    /// </summary>
    /// <param name="predicateFunc">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAllMatch<T, C>(this T t, Func<T, C, bool> predicateFunc, IEnumerable<C> values)
    {
        return values.All(f => predicateFunc(t, f));
    }

    /// <summary>
    /// 判断值是否结语两值之间
    /// </summary>
    public static bool IsBetween<T>(this T t, T min, T max, bool includeMin, bool includeMax)
        where T : IComparable<T>
    {
        if (t == null) throw new ArgumentNullException("t");

        var lowerCompareResult = t.CompareTo(min);
        var upperCompareResult = t.CompareTo(max);

        return (includeMin && lowerCompareResult == 0) ||
            (includeMax && upperCompareResult == 0) ||
            (lowerCompareResult > 0 && upperCompareResult < 0);
    }

    /// <summary>
    /// 判断值是否结语两值之间（与两值中的任意一个相等也返回true）
    /// </summary>
    public static bool IsBetween<T>(this T t, T min, T max) where T : IComparable<T>
    {
        return t.IsBetween(min, max, true, true);
    }

    /// <summary>
    /// 遍历集合，返回第一个符合判断条件的项目的索引位置
    /// </summary>
    public static int IndexOf<T>(this IEnumerable<T> source, Predicate<T> predicate)
    {
        int x = 0;
        foreach (T element in source)
        {
            if (predicate(element)) return x;
            x++;
        }
        return -1;
    }

    /// <summary>
    /// 遍历集合，执行传入表达式
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T element in source)
            action(element);
    }

    /// <summary>
    /// 遍历集合，执行传入表达式
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        int i = 0;
        foreach (T element in source)
            action(element, i++);
    }

    /// <summary>
    /// 转换为List对象
    /// </summary>
    public static List<T> ToList<T>(this IEnumerable<T> source)
    {
        var l = new List<T>();
        foreach (var f in source)
        {
            l.Add(f);
        }
        return l;
    }

    /// <summary>
    /// 展开为字符串
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">集合</param>
    /// <param name="separator">每个项之间的间隔符号</param>
    /// <param name="funcForGetString">由对象获取相应字符串</param>
    /// <returns>展开并组合后的字符串</returns>
    public static string ExpandToString<T>(this IEnumerable<T> o, string separator, Func<T, string> funcForGetString)
    {
        StringBuilder S = new StringBuilder();
        foreach (T f in o)
        {
            if (S.Length > 0) S.Append(separator);
            S.Append(funcForGetString(f));
        }
        return S.ToString();
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable o, Func<T, IEnumerable<T>> funcForRecursion)
    {
        return RecursionEachSelect(o, funcForRecursion, null);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="Recursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable o, Func<T, IEnumerable<T>> Recursion, Predicate<T> predicate)
    {
        return RecursionEachSelect(o.Cast<T>(), Recursion, predicate);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable泛型形式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable<T> o, Func<T, IEnumerable<T>> funcForRecursion)
    {
        return RecursionEachSelect(o, funcForRecursion, null);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable泛型形式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable<T> o, Func<T, IEnumerable<T>> funcForRecursion, Predicate<T> predicate)
    {
        return RecursionEachSelect(o, funcForRecursion, predicate, true);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable泛型形式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <param name="isWhenFailToContinue">指示表达式检验失败后是否继续递归选取接下来的项，在不指定此参数的重载形式中默认为true</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable<T> o, Func<T, IEnumerable<T>> funcForRecursion, Predicate<T> predicate, bool isWhenFailToContinue)
    {
        foreach (var f in o)
        {
            if (predicate == null || predicate(f)) yield return f;
            else if (!isWhenFailToContinue)
            {
                yield break;
            }
            foreach (var d in RecursionSelect(f, funcForRecursion, predicate))
            {
                yield return d;
            }
        }
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, T> funcForRecursion)
    {
        return RecursionSelect(o, funcForRecursion, null);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, T> funcForRecursion, Predicate<T> predicate)
    {
        return RecursionSelect(o, funcForRecursion, predicate, true);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <param name="isWhenFailToContinue">指示表达式检验失败后是否继续递归选取接下来的项，在不指定此参数的重载形式中默认为true</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, T> funcForRecursion, Predicate<T> predicate, bool isWhenFailToContinue)
    {
        if (o == null) yield break;
        var f = funcForRecursion(o);
        if (predicate == null || predicate(f)) yield return f;
        else if (!isWhenFailToContinue)
        {
            yield break;
        }
        foreach (var d in RecursionSelect(f, funcForRecursion, predicate, isWhenFailToContinue))
        {
            yield return d;
        }
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, IEnumerable<T>> funcForRecursion)
    {
        return RecursionSelect(o, funcForRecursion, null);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, IEnumerable<T>> funcForRecursion, Predicate<T> predicate)
    {
        return RecursionSelect(o, funcForRecursion, predicate, true);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="funcForRecursion">通过此表达式选取要返回的子项</param>
    /// <param name="predicate">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <param name="isWhenFailToContinue">指示表达式检验失败后是否继续递归选取接下来的项，在不指定此参数的重载形式中默认为true</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, IEnumerable<T>> funcForRecursion, Predicate<T> predicate, bool isWhenFailToContinue)
    {
        foreach (var f in funcForRecursion(o))
        {
            if (predicate == null || predicate(f)) yield return f;
            else if (!isWhenFailToContinue)
            {
                yield break;
            }
            foreach (var d in RecursionSelect(f, funcForRecursion, predicate))
            {
                yield return d;
            }
        }
    }

    /// <summary>
    /// 清除列表内所有项，克隆目标集合的所有项到列表，以保持两个处内容一致。
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="source">克隆目标集合</param>
    public static void CloneItemsForm<T>(this List<T> o, IEnumerable<T> source)
    {
        o.Clear();
        o.AddRange(source);
    }

    /// <summary>
    /// 生成随机布尔值
    /// </summary>
    /// <returns>随机布尔值</returns>
    public static bool NextBool(this Random random)
    {
        return random.Next(2) == 0;
    }

    /// <summary>
    /// 生成随机byte数组
    /// </summary>
    /// <param name="length">数组长度</param>
    /// <returns>随机byte数组</returns>
    public static byte[] NextBytes(this Random random, int length)
    {
        var data = new byte[length];
        random.NextBytes(data);
        return data;
    }

    /// <summary>
    /// 生成随机UInt16值
    /// </summary>
    /// <returns>随机UInt16值</returns>
    public static UInt16 NextUInt16(this Random random)
    {
        return BitConverter.ToUInt16(random.NextBytes(2), 0);
    }

    /// <summary>
    /// 生成随机Int16值
    /// </summary>
    /// <returns>随机Int16值</returns>
    public static Int16 NextInt16(this Random random)
    {
        return BitConverter.ToInt16(random.NextBytes(2), 0);
    }

    /// <summary>
    /// 生成随机Float值
    /// </summary>
    /// <returns>随机Float值</returns>
    public static float NextFloat(this Random random)
    {
        return BitConverter.ToSingle(random.NextBytes(4), 0);
    }

    /// <summary>
    /// 生成随机时间
    /// </summary>
    /// <param name="minValue">最小值</param>
    /// <param name="maxValue">最大值</param>
    /// <returns>随机时间</returns>
    public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
    {
        var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
        return new DateTime(ticks);
    }

    /// <summary>
    /// 生成随机时间
    /// </summary>
    /// <returns>随机时间</returns>
    public static DateTime NextDateTime(this Random random)
    {
        return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
    }

    //可以再编写生成随机字符串、随机颜色等功能的方法

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static int CeilingToInt(this double v)
    {
        return (int)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static long CeilingToLong(this double v)
    {
        return (long)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static int CeilingToInt(this float v)
    {
        return (int)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static int CeilingToInt(this decimal v)
    {
        return (int)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static int FloorToInt(this double v)
    {
        return (int)Math.Floor(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static long FloorToLong(this double v)
    {
        return (long)Math.Floor(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static int FloorToInt(this float v)
    {
        return (int)Math.Floor(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static int FloorToInt(this decimal v)
    {
        return (int)Math.Floor(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static int RoundToInt(this double v)
    {
        return (int)Math.Round(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static long RoundToLong(this double v)
    {
        return (long)Math.Round(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static int RoundToInt(this float v)
    {
        return (int)Math.Round(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static int RoundToInt(this decimal v)
    {
        return (int)Math.Round(v);
    }

    #region 链式SwitchCase

    /// <summary>
    /// 执行Switch操作
    /// </summary>
    public static Switch<T> Switch<T>(this T v)
    {
        return new Switch<T>(v);
    }

    /// <summary>
    /// 执行Switch操作，并传入一个方法用于处理返回结果
    /// </summary>
    /// <param name="Do">处理返回结果的方法，该方法将在每次执行CaseReturn并匹配成功时或执行DefaultReturn时调用，方法的第一个参数是新传入的返回值，第二个参数是当前的返回值</param>
    public static Case<T, R> Switch<T, R>(this T v, Func<R, R, R> Do)
    {
        return new Case<T, R>(v, Do);
    }

    #endregion

    #region 特殊字符串

    /// <summary>
    /// 转换为特殊字符串类型
    /// </summary>
    public static T As<T>(this string s) where T : SpecialString, new()
    {
        var o = new T();
        o.Value = s;
        return o;
    }

    public static UriString AsUriString(this string s)
    {
        return s.As<UriString>();
    }

    public static PathString AsPathString(this string s)
    {
        return s.As<PathString>();
    }

    #endregion
}
