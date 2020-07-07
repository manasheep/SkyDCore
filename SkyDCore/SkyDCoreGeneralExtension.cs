using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using SkyDCore.Text;
using SkyDCore.General;
using System.Reflection;
using System.Diagnostics;
using SkyDCore.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
using SkyDCore.Encryption;
using System.Collections.ObjectModel;

/// <summary>
/// 通用扩展
/// </summary>
public static class SkyDCoreGeneralExtension
{
    static Random R = new Random();

    /// <summary>
    /// 以或的形式组合两个表达式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="expr1">表达式1</param>
    /// <param name="expr2">表达式2</param>
    /// <returns>组合后的表达式</returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>
              (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
    }

    /// <summary>
    /// 以与的形式组合两个表达式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="expr1">表达式1</param>
    /// <param name="expr2">表达式2</param>
    /// <returns>组合后的表达式</returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>
              (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
    }

    /// <summary>
    /// 转换为可观察集合
    /// </summary>
    /// <typeparam name="T">集合类型</typeparam>
    /// <param name="enumerable">可枚举集合</param>
    /// <returns>可观察集合</returns>
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
    {
        ObservableCollection<T> list = new ObservableCollection<T>();
        if (enumerable == null) return list;
        foreach (var data in enumerable)
        {
            list.Add(data);
        }
        return list;
    }

    /// <summary>
    /// 转换为可枚举集合
    /// </summary>
    /// <typeparam name="T">集合类型</typeparam>
    /// <param name="enumerator">枚举器</param>
    /// <returns>可枚举集合</returns>
    public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
    {
        while (enumerator.MoveNext())
            yield return enumerator.Current;
    }

    /// <summary>
    /// 返回常规格式化的GUID字符串（即不含“-”的形式）
    /// </summary>
    /// <param name="guid">Guid</param>
    /// <returns>常规格式化的GUID字符串</returns>
    public static string ToStringN(this Guid guid)
    {
        return guid.ToString("N");
    }

    /// <summary>
    /// 转换为数组
    /// </summary>
    /// <param name="list">列表</param>
    /// <returns>数组</returns>
    public static object[] ToArray(this IList list)
    {
        return list.Cast<object>().ToArray();
    }

    /// <summary>
    /// 转换为数组
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="list">列表</param>
    /// <returns>数组</returns>
    public static T[] ToArray<T>(this IList<T> list)
    {
        return list.Cast<T>().ToArray();
    }

    /// <summary>
    /// 转换为列表
    /// </summary>
    /// <param name="list">列表</param>
    /// <returns>列表</returns>
    public static List<object> ConvertToObjectList(this IList list)
    {
        return list.Cast<object>().ToList();
    }

    /// <summary>
    /// 转换为列表
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="list">列表</param>
    /// <returns>列表</returns>
    public static List<T> ConvertToList<T>(this IList<T> list)
    {
        return list.Cast<T>().ToList();
    }

    /// <summary>
    /// 交换两个项的位置
    /// </summary>
    /// <param name="list">列表</param>
    /// <param name="indexA">项A的索引位置</param>
    /// <param name="indexB">项B的索引位置</param>
    public static void Interchange(this IList list, int indexA, int indexB)
    {
        var temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;
    }

    /// <summary>
    /// 将指定项前移
    /// </summary>
    /// <param name="list">列表</param>
    /// <param name="item">项对象</param>
    /// <returns>如果改变了位置，则返回新的位置索引，否则返回null</returns>
    public static int? MovePrevious(this IList list, object item)
    {
        var index = list.IndexOf(item);
        if (index > 0)
        {
            list.Interchange(index, index - 1);
            return index - 1;
        }
        return null;
    }

    /// <summary>
    /// 将指定项后移
    /// </summary>
    /// <param name="list">列表</param>
    /// <param name="item">项对象</param>
    /// <returns>如果改变了位置，则返回新的位置索引，否则返回null</returns>
    public static int? MoveNext(this IList list, object item)
    {
        var index = list.IndexOf(item);
        if (index < list.Count - 1)
        {
            list.Interchange(index, index + 1);
            return index + 1;
        }
        return null;
    }

    /// <summary>
    /// 将指定项移到最前端
    /// </summary>
    /// <param name="list">列表</param>
    /// <param name="item">项对象</param>
    /// <returns>如果改变了位置，则返回新的位置索引，否则返回null</returns>
    public static int? MoveToBegin(this IList list, object item)
    {
        var index = list.IndexOf(item);
        if (index > 0)
        {
            list.Interchange(index, 0);
            return 0;
        }
        return null;
    }

    /// <summary>
    /// 将指定项移到最末端
    /// </summary>
    /// <param name="list">列表</param>
    /// <param name="item">项对象</param>
    /// <returns>如果改变了位置，则返回新的位置索引，否则返回null</returns>
    public static int? MoveToEnd(this IList list, object item)
    {
        var index = list.IndexOf(item);
        if (index < list.Count - 1)
        {
            list.Interchange(index, list.Count - 1);
            return list.Count - 1;
        }
        return null;
    }

    /// <summary>
    /// 将对象的公共属性值映射到另一个对象，可以是不同类型但具有相同字段名称的目标对象
    /// </summary>
    /// <typeparam name="TIn">源类型</typeparam>
    /// <typeparam name="TOut">输出类型</typeparam>
    /// <param name="tIn">源对象</param>
    /// <param name="tOut">输出对象</param>
    /// <param name="inputAndOutpuMemberCheck">输入或输出属性验证</param>
    /// <returns>映射后的目标对象</returns>
    public static TOut MapTo<TIn, TOut>(this TIn tIn, TOut tOut, Predicate<Tuple<PropertyInfo, PropertyInfo>> inputAndOutpuMemberCheck = null)
    {
        //TOut tOut = Activator.CreateInstance<TOut>();
        var tInType = tIn.GetType();
        foreach (var itemOut in tOut.GetType().GetProperties())
        {
            var itemIn = tInType.GetProperty(itemOut.Name); ;
            if (inputAndOutpuMemberCheck != null && !inputAndOutpuMemberCheck(new Tuple<PropertyInfo, PropertyInfo>(itemIn, itemOut))) continue;
            if (itemOut.CanWrite && itemIn.CanRead)
            {
                itemOut.SetValue(tOut, itemIn.GetValue(tIn, null), null);
            }
        }
        return tOut;
    }

    /// <summary>
    /// 分页获取集合中的实体对象集合
    /// </summary>
    /// <param name="pageIndex">当前页数，从0开始记数</param>
    /// <param name="objectNumPerPage">每页承载的实体数目</param>
    /// <returns>分页后的实体对象集合</returns>
    public static IQueryable<T> PagingGet<T>(this IQueryable<T> o, int pageIndex, int objectNumPerPage)
    {
        return o.Skip(pageIndex * objectNumPerPage).Take(objectNumPerPage);
    }

    /// <summary>
    /// 计算分页后的总页数
    /// </summary>
    /// <param name="objectNumPerPage">每页承载的实体数目</param>
    /// <returns>分页后的总页数</returns>
    public static int PageCount<T>(this IQueryable<T> o, int objectNumPerPage)
    {
        var x = o.Count();
        if (x == 0) return 0;
        return x / objectNumPerPage + (x % objectNumPerPage > 0 ? 1 : 0);
    }

    /// <summary>
    /// 分页获取集合中的实体对象集合
    /// </summary>
    /// <param name="pageIndex">当前页数，从0开始记数</param>
    /// <param name="objectNumPerPage">每页承载的实体数目</param>
    /// <returns>分页后的实体对象集合</returns>
    public static IEnumerable<T> PagingGet<T>(this IEnumerable<T> o, int pageIndex, int objectNumPerPage)
    {
        return o.Skip(pageIndex * objectNumPerPage).Take(objectNumPerPage);
    }

    /// <summary>
    /// 计算分页后的总页数
    /// </summary>
    /// <param name="objectNumPerPage">每页承载的实体数目</param>
    /// <returns>分页后的总页数</returns>
    public static int PagingCount<T>(this IEnumerable<T> o, int objectNumPerPage)
    {
        var x = o.Count();
        if (x == 0) return 0;
        return x / objectNumPerPage + (x % objectNumPerPage > 0 ? 1 : 0);
    }

    /// <summary>
    /// 将另一个字典的内容复制到此字典
    /// </summary>
    /// <typeparam name="Key">键</typeparam>
    /// <typeparam name="Value">值</typeparam>
    /// <param name="combineWithDic">待并入的字典</param>
    /// <param name="isSkipConflictKey">指示发现已有的键时，是否跳过</param>
    public static void Combine<Key, Value>(this IDictionary<Key, Value> sourceDic, IDictionary<Key, Value> combineWithDic, bool isSkipConflictKey)
    {
        foreach (Key f in combineWithDic.Keys)
        {
            if (sourceDic.ContainsKey(f))
            {
                if (!isSkipConflictKey) sourceDic[f] = combineWithDic[f];
            }
            else
            {
                sourceDic.Add(f, combineWithDic[f]);
            }
        }
    }

    /// <summary>
    /// 如果键存在则更新其值，否则就添加一个新键值对
    /// </summary>
    public static void AddOrUpdate<Key, Value>(this IDictionary<Key, Value> o, Key key, Value value)
    {
        if (o.ContainsKey(key)) o[key] = value;
        else o.Add(key, value);
    }


    /// <summary>
    /// 获取集合中指定索引位置的对象，如果没有则返回默认值
    /// </summary>
    public static T GetItemByIndex<T>(this IEnumerable<T> o, int index)
    {
        var e = o.GetEnumerator();
        for (int i = 0; i < index; i++)
        {
            if (!e.MoveNext()) return default(T);
        }
        return e.Current;
    }

    /// <summary>
    /// 获取集合中指定索引位置的对象，如果没有则返回null
    /// </summary>
    public static object GetItemByIndex(this IEnumerable o, int index)
    {
        var e = o.GetEnumerator();
        for (int i = 0; i < index; i++)
        {
            if (!e.MoveNext()) return null;
        }
        return e.Current;
    }

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
        return SkyDCoreTextAssist.ExpandAndToString(s, separator);
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
    /// 判断值是否介于两值之间
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
    /// 判断值是否结介于值之间（与两值中的任意一个相等也返回true）
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
    /// 循环
    /// </summary>
    /// <param name="count">循环次数</param>
    /// <param name="action">操作表达式，参数为从0开始的循环计数</param>
    public static void For(this int count, Action<int> action)
    {
        for (int i = 0; i < count; i++)
        {
            action.Invoke(i);
        }
    }

    /// <summary>
    /// 转换为List对象
    /// </summary>
    public static List<T> ConvertToList<T>(this IEnumerable<T> source)
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

    /// <summary>
    /// 转换匿名类型。这个需求来源于界面中使用BackgroundWorker，为了给DoWork传递多个参数，又不想定义一个类型来完成，于是我会用到TolerantCast方法。
    /// 来源：http://www.cnblogs.com/JamesLi2015/p/4663292.html
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">待转换对象</param>
    /// <param name="example">默认值对象</param>
    /// <returns>转换过后的匿名对象</returns>
    /* 使用范例
     * //创建匿名类型
var parm = new { Bucket = bucket, AuxiliaryAccIsCheck = chbAuxiliaryAcc.Checked, AllAccountIsCheck = chbAllAccount.Checked };
backgroundWorker.RunWorkerAsync(parm);


 private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
 {
//解析转换匿名类型
  var parm = e.Argument.TolerantCast(new { Bucket = new RelationPredicateBucket(), AuxiliaryAccIsCheck = false, AllAccountIsCheck = false });*/
    public static T TolerantCast<T>(this object o, T example)
        where T : class
    {
        IComparer<string> comparer = StringComparer.CurrentCultureIgnoreCase;
        //Get constructor with lowest number of parameters and its parameters
        var constructor = typeof(T).GetConstructors(
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            ).OrderBy(c => c.GetParameters().Length).First();
        var parameters = constructor.GetParameters();

        //Get properties of input object
        var sourceProperties = new List<PropertyInfo>(o.GetType().GetProperties());

        if (parameters.Length > 0)
        {
            var values = new object[parameters.Length];
            for (int i = 0; i < values.Length; i++)
            {
                Type t = parameters[i].ParameterType;
                //See if the current parameter is found as a property in the input object
                var source = sourceProperties.Find(delegate (PropertyInfo item)
                {
                    return comparer.Compare(item.Name, parameters[i].Name) == 0;
                });

                //See if the property is found, is readable, and is not indexed
                if (source != null && source.CanRead &&
                    source.GetIndexParameters().Length == 0)
                {
                    //See if the types match.
                    if (source.PropertyType == t)
                    {
                        //Get the value from the property in the input object and save it for use
                        //in the constructor call.
                        values[i] = source.GetValue(o, null);
                        continue;
                    }
                    else
                    {
                        //See if the property value from the input object can be converted to
                        //the parameter type
                        try
                        {
                            values[i] = Convert.ChangeType(source.GetValue(o, null), t);
                            continue;
                        }
                        catch
                        {
                            //Impossible. Forget it then.
                        }
                    }
                }
                //If something went wrong (i.e. property not found, or property isn't
                //converted/copied), get a default value.
                values[i] = t.IsValueType ? Activator.CreateInstance(t) : null;
            }
            //Call the constructor with the collected values and return it.
            return (T)constructor.Invoke(values);
        }
        //Call the constructor without parameters and return the it.
        return (T)constructor.Invoke(null);
    }

    /// <summary>
    /// 获取当前代码所在方法的方法名
    /// </summary>
    /// <param name="o">任意对象，仅供调用方便，不作任何处理</param>
    /// <returns>当前代码所在方法的方法名</returns>
    public static string GetCurrentMethodName(this object o)
    {
        return GetStackMethodName(o, 2);
    }

    /// <summary>
    /// 获取当前代码所在的指定堆栈层级的方法的方法名
    /// </summary>
    /// <param name="o">任意对象，仅供调用方便，不作任何处理</param>
    /// <param name="ignoreStackLevel">忽略堆栈层数，默认忽略1层堆栈，也就忽略了此方法GetMethodName，这样拿到的就正好是外部调用GetStackMethodName的方法信息</param>
    /// <returns>当前代码所在方法的方法名</returns>
    public static string GetStackMethodName(this object o, int ignoreStackLevel = 1)
    {
        var method = new StackFrame(ignoreStackLevel).GetMethod();
        var property = (
                  from p in method.DeclaringType.GetProperties(
                           BindingFlags.Instance |
                           BindingFlags.Static |
                           BindingFlags.Public |
                           BindingFlags.NonPublic)
                  where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
                  select p).FirstOrDefault();
        return property == null ? method.Name : property.Name;
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的名称
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="expression">获取属性的表达式</param>     
    /// <returns>属性的名称</returns>     
    public static string GetPropertyName<T, PT>(this T o, Expression<Func<T, PT>> expression)
    {
        return SkyDCoreReflectionAssist.GetPropertyName(expression);
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的名称
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="expression">获取属性的表达式</param>     
    /// <returns>属性的名称</returns>     
    public static string GetPropertyName<T, PT>(this Expression<Func<T, PT>> expression)
    {
        return SkyDCoreReflectionAssist.GetPropertyName(expression);
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的类型
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="expression">获取属性的表达式</param>     
    /// <returns>属性的类型</returns>     
    public static Type GetPropertyType<T, PT>(this T o, Expression<Func<T, PT>> expression)
    {
        return SkyDCoreReflectionAssist.GetPropertyType(expression);
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的类型
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="expression">获取属性的表达式</param>     
    /// <returns>属性的类型</returns>     
    public static Type GetPropertyType<T, PT>(this Expression<Func<T, PT>> expression)
    {
        return SkyDCoreReflectionAssist.GetPropertyType(expression);
    }

    /// <summary>
    /// 获取枚举的注释特性（DescriptionAttribute）值
    /// </summary>
    /// <param name="o">枚举值</param>
    /// <returns>注释</returns>
    public static string GetDescription(this Enum o)
    {
        var enumType = o.GetType();
        var name = Enum.GetName(enumType, Convert.ToInt32(o));
        if (name == null)
            return string.Empty;
        object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (objs.Length == 0)
        {
            return string.Empty;
        }
        else
        {
            var attr = objs[0] as DescriptionAttribute;
            return attr.Description;
        }
    }

    /// <summary>
    /// 获取枚举的整数值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="o">枚举值</param>
    /// <returns>整数值</returns>
    public static int GetIntValue<T>(this T o) where T : Enum
    {
        return Convert.ToInt32(o);
    }

    /// <summary>
    /// 获取枚举的全部值数组
    /// </summary>
    /// <param name="o">枚举值</param>
    /// <returns>全部值的数组</returns>
    public static Array GetAllEnumValues(this Enum o)
    {
        return Enum.GetValues(o.GetType());
    }

    /// <summary>
    /// 获取枚举的全部值列表
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="o">枚举值</param>
    /// <returns>全部值列表</returns>
    public static List<T> GetAllEnumValueList<T>(this T o) where T : Enum
    {
        return Enum.GetValues(o.GetType()).Cast<T>().ToList();
    }

    /// <summary>
    /// 验证枚举的值是否是已定义的
    /// </summary>
    /// <param name="o">枚举值</param>
    /// <returns>是否已定义</returns>
    public static bool CheckIsDefined(this Enum o)
    {
        return Enum.IsDefined(o.GetType(), o);
    }

    /// <summary>
    /// 转换为异步编程模型（Asynchronous Programming Model），用于WF的AsyncCodeActivity中的BeginExecute方法中使用，如果不进行此转换，通常就会因不包含AsyncState属性而引发InvalidOperationException
    /// 代码源自：http://tweetycodingxp.blogspot.jp/2013/06/using-task-based-asynchronous-pattern.html
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="task">原Task对象</param>
    /// <param name="callback">BeginExecute方法中的callback参数</param>
    /// <param name="state">BeginExecute方法中的state参数</param>
    /// <returns>异步编程模型形式的Task</returns>
    public static Task<TResult> ToApm<TResult>(this Task<TResult> task, AsyncCallback callback, object state)
    {
        if (task.AsyncState == state)
        {
            if (callback != null)
            {
                task.ContinueWith(delegate { callback(task); },
                                  CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
            }
            return task;
        }

        var tcs = new TaskCompletionSource<TResult>(state);
        task.ContinueWith(obj =>
        {
            if (task.IsFaulted) tcs.TrySetException(task.Exception.InnerExceptions);
            else if (task.IsCanceled) tcs.TrySetCanceled();
            else tcs.TrySetResult(task.Result);

            if (callback != null) callback(tcs.Task);
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
        return tcs.Task;
    }

    /// <summary>
    /// 判断是否为数值类型。目前已知不支持BigInteger。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为数值类型</returns>
    public static bool IsNumericType(this Type t)
    {
        var tc = Type.GetTypeCode(t);
        return (t.IsPrimitive && t.IsValueType && !t.IsEnum && tc != TypeCode.Char && tc != TypeCode.Boolean) || tc == TypeCode.Decimal;
    }

    /// <summary>
    /// 判断是否为可空数值类型。目前已知不支持BigInteger。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为可空数值类型</returns>
    public static bool IsNumericOrNullableNumericType(this Type t)
    {
        return t.IsNumericType() || (t.IsNullableType() && t.GetGenericArguments()[0].IsNumericType());
    }

    /// <summary>
    /// 判断是否为可空类型。
    /// 注意，直接调用可空对象的.GetType()方法返回的会是其泛型值的实际类型，用其进行此判断肯定返回false。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为可空类型</returns>
    public static bool IsNullableType(this Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// 通过反射获取对象的指定属性值
    /// </summary>
    /// <param name="o"></param>
    /// <param name="propertyName">属性名</param>
    /// <returns>属性值</returns>
    public static object GetPropertyValue(this object o, string propertyName)
    {
        var t = o.GetType();
        return t.GetProperty(propertyName).GetValue(o, null);
    }

    /// <summary>
    /// 通过反射获取对象的指定字段值
    /// </summary>
    /// <param name="o"></param>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段值</returns>
    public static object GetFieldValue(this object o, string fieldName)
    {
        var t = o.GetType();
        return t.GetField(fieldName).GetValue(o);
    }

    /// <summary>
    /// 将对象的所有属性及字段的值复制到另一个对象上
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="targetObject">复制到的对象</param>
    public static void CopyTo<T>(this T o, T targetObject)
    {
        var t = o.GetType();
        foreach (var p in t.GetProperties())
        {
            if (p.CanRead && p.CanWrite)
            {
                p.SetValue(targetObject, p.GetValue(o, null), null);
            }
        }
        foreach (var f in t.GetFields())
        {
            f.SetValue(targetObject, f.GetValue(o));
        }
    }

    /// <summary>
    /// 通过反射获得类型名称及全部属性名值的输出
    /// </summary>
    /// <param name="o"></param>
    /// <returns>类型名称及全部属性名值的输出</returns>
    public static string GetTypeNameAndAllPropertyInfo(this object o)
    {
        StringBuilder sb = new StringBuilder();
        var t = o.GetType();
        sb.AppendLine(t.FullName + "类型对象");
        foreach (System.Reflection.PropertyInfo p in t.GetProperties())
        {
            sb.AppendLine(p.Name + ":" + p.GetValue(o, null));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 返回该字符串的CRC32代码
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的CRC32代码</returns>
    // ReSharper disable once InconsistentNaming
    public static ulong GetCRC32Code(this string s)
    {
        return new CRC32().StringCRC(s);
    }

    /// <summary>
    /// 返回该字符串的哈希字符串
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的哈希字符串</returns>
    public static string GetHashString(this string s)
    {
        return Cryptography.GetPWDHash(s);
    }

    /// <summary>
    /// 返回该字符串的加密字符串（对称加密）
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的加密字符串</returns>
    public static string GetEncodeCryptographyString(this string s)
    {
        return Cryptography.GetEncrypt(s);
    }

    /// <summary>
    /// 返回该字符串的解密字符串（对称解密）
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的解密字符串</returns>
    public static string GetDecodeCryptographyString(this string s)
    {
        return Cryptography.GetDecrypt(s);
    }

    /// <summary>
    /// 返回该字符串的MD5编码字符串
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的MD5编码字符串</returns>
    // ReSharper disable once InconsistentNaming
    public static string GetMD5String(this string s)
    {
        return MD5.HashString(s);
    }

    /// <summary>
    /// 返回该字符串的BASE64编码字符串（编码为ASCII文本，用于网络传输）
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encoding">内容字符串编码</param>
    /// <returns>该字符串的BASE64编码字符串</returns>
    // ReSharper disable once InconsistentNaming
    public static string GetEncodeBASE64String(this string s, Encoding encoding)
    {
        return BASE64.EncryptString(s, encoding);
    }

    /// <summary>
    /// 返回该字符串的BASE64解码字符串（解码自ASCII文本，用于网络传输）
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encoding">内容字符串编码</param>
    /// <returns>该字符串的BASE64解码字符串</returns>
    // ReSharper disable once InconsistentNaming
    public static string GetDecodeBASE64String(this string s, Encoding encoding)
    {
        return BASE64.DecryptString(s, encoding);
    }

    /// <summary>
    /// 将对象的字符串形式输出到指定目录的log.txt文件中
    /// </summary>
    public static T Log<T>(this T t, string outputDirectoryPath)
    {
        StreamWriter sw = new StreamWriter(outputDirectoryPath.AsPathString().Combine("log.txt"), true);
        sw.WriteLine("{0} : {1}".FormatWith(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), t == null ? "[Null]" : t.ToString()));
        sw.Close();
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到指定目录的log.txt文件中
    /// </summary>
    public static T Log<T>(this T t, string outputDirectoryPath, Func<T, object> func)
    {
        func(t).Log(outputDirectoryPath);
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t)
    {
        System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString());
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t, Func<T, object> func)
    {
        var o = func(t);
        System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString());
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t, string tag)
    {
        System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString(), tag);
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t, Func<T, object> func, string tag)
    {
        var o = func(t);
        System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString(), tag);
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    ///<param name="formatString">用于格式化的字符串，以{0}表示此对象的显示位置</param>
    public static T TraceFormat<T>(this T t, string formatString)
    {
        System.Diagnostics.Trace.WriteLine(string.Format(formatString, t == null ? "[Null]" : t.ToString()));
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    ///<param name="formatString">用于格式化的字符串，以{0}表示此对象的显示位置</param>
    public static T TraceFormat<T>(this T t, string formatString, string tag)
    {
        System.Diagnostics.Trace.WriteLine(string.Format(formatString, t == null ? "[Null]" : t.ToString()), tag);
        return t;
    }

    /// <summary>
    /// 生成随机枚举值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <returns>随机枚举值</returns>
    public static T NextEnum<T>(this Random random)
    where T : struct
    {
        Type type = typeof(T);
        if (type.IsEnum == false) throw new InvalidOperationException();

        var array = Enum.GetValues(type);
        var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
        return (T)array.GetValue(index);
    }

    /// <summary> 
    /// 将 Stream 写入文件 
    /// </summary> 
    public static void ToFile(this Stream stream, string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(stream.ToBytes());
        bw.Close();
        fs.Close();
    }

    #region 集合排序
    /// <summary>
    /// 对集合进行排序
    /// </summary>
    /// <typeparam name="T">排序对象类型</typeparam>
    /// <param name="list">列表</param>
    /// <param name="comparison">比较方式代理，可以传入Lambda表达式，表达式返回一个int值，两相比较时值越小排名越靠前</param>
    /// <example>
    /// <code>
    /// PageCollection.Sort((s1, s2) =>
    /// {
    ///     var s1a = s1.FileName.ToCharArray();
    ///     var s2a = s2.FileName.ToCharArray();
    ///     for (int i = 0; i &lt; Math.Min(s1a.Length, s2a.Length); i++)
    ///     {
    ///         var r = s1a[i] - s2a[i];
    ///         if (r != 0) return r;
    ///     }
    ///     return 0;
    /// });
    /// </code>
    /// </example>
    public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
    {
        ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
    }

    /// <summary>
    /// 逆转集合项顺序
    /// </summary>
    /// <typeparam name="T">排序对象类型</typeparam>
    /// <param name="list">列表</param>
    public static void Reverse<T>(this IList<T> list)
    {
        ArrayList.Adapter((IList)list).Reverse();
    }

    /// <summary>
    /// 逆转集合中的部分项的顺序
    /// </summary>
    /// <typeparam name="T">排序对象类型</typeparam>
    /// <param name="list">列表</param>
    /// <param name="index">起始索引位置</param>
    /// <param name="count">囊括项目数量</param>
    public static void Reverse<T>(this IList<T> list, int index, int count)
    {
        ArrayList.Adapter((IList)list).Reverse(index, count);
    }

    /// <summary>
    /// 排序，并返回经过排序的新集合
    /// </summary>
    /// <typeparam name="T">排序对象类型</typeparam>
    /// <param name="list">列表</param>
    /// <param name="comparison">比较方式代理，可以传入Lambda表达式，表达式返回一个int值，两相比较时值越小排名越靠前</param>
    /// <example>
    /// <code>
    /// PageCollection.Sort((s1, s2) =>
    /// {
    ///     var s1a = s1.FileName.ToCharArray();
    ///     var s2a = s2.FileName.ToCharArray();
    ///     for (int i = 0; i &lt; Math.Min(s1a.Length, s2a.Length); i++)
    ///     {
    ///         var r = s1a[i] - s2a[i];
    ///         if (r != 0) return r;
    ///     }
    ///     return 0;
    /// });
    /// </code>
    /// <returns>经过排序的集合</returns>
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, Comparison<T> comparison)
    {
        return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
    }
    #endregion

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

    public static HtmlString AsHtmlString(this string s)
    {
        return s.As<HtmlString>();
    }

    #endregion
}
