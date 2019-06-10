using log4net;
using log4net.Core;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Log4NetExtension
{
    static Log4NetExtension()
    {
        Init("SkyDCoreLogRepository", "log4net.config");
    }

    /// <summary>
    /// 初始化。通常不需要手动调用该方法，但在传统ASP.net网站中，需要手动调用，并传入一个新的名称，以及使用Server.MapPath("log4net.config")作为文件路径。
    /// </summary>
    /// <param name="name">存储仓库名称</param>
    /// <param name="filepath">配置文件路径。在传统ASP.net网站中，通常使用Server.MapPath("log4net.config")作为文件路径。</param>
    public static void Init(string name, string filepath)
    {
        _Repository = LogManager.CreateRepository(name);
        //从config文件中加载Log4net日志配置
        log4net.Config.XmlConfigurator.Configure(_Repository, new FileInfo(filepath));
        _LoggerDic = new Dictionary<Type, ILog>();
    }

    public static ILoggerRepository _Repository { get; private set; }
    private static Dictionary<Type, ILog> _LoggerDic { get; set; }

    public static ILog GetLogger(this object o)
    {
        return GetLogger(o.GetType());
    }

    public static ILog GetLogger()
    {
        return GetLogger(typeof(Log4NetExtension));
    }

    public static ILog GetLogger(this Type t)
    {
        lock (_LoggerDic)
        {
            if (_LoggerDic.Keys.Contains(t)) return _LoggerDic[t];
            var log = LogManager.GetLogger(_Repository.Name, t);
            _LoggerDic.Add(t, log);
            return log;
        }
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，然后通过Log4Net记录Debug日志，并将该对象返回
    /// </summary>
    public static T TraceAndLog<T>(this T t)
    {
        GetLogger().Debug($"[Trace] {t}");
        System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString());
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，然后通过Log4Net记录Debug日志，并将该对象返回
    /// </summary>
    public static T TraceAndLog<T>(this T t, Func<T, object> func)
    {
        var o = func(t);
        GetLogger().Debug($"[Trace] {o}");
        System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString());
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，然后通过Log4Net记录Debug日志，并将该对象返回
    /// </summary>
    public static T TraceAndLog<T>(this T t, string category)
    {
        GetLogger().Debug($"[Trace] ({category}) {t}");
        System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString(), category);
        return t;
    }
}
