# SkyDCore.Log

## 简介

针对log4net进行功能扩展，可以实现快速获取ILog对象，及HTML格式输出日志的功能。

## 调用范例

this.GetLogger().Info("程序启动");
任意对象皆可调用此扩展方法。

## 配置文件（log4net.config）范例

注意：此文件的输出设置要设为“如果较新则复制”。

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <!-- This section contains the log4net configuration settings -->
  <log4net debug="true">
    <!--输出到txt文件-->
    <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender" >
      <!--输出到什么目录-->
      <file value="Logs/" />
      <!--是否覆写到文件中-->
      <appendToFile value="true" />
      <!--创建文件的规则-->
      <rollingStyle value="Composite" />
      <datePattern value="yyyy_MM_dd'.txt'"/>
      <!--注：此处如果设为“yyyy/yyyy_MM/yyyy_MM_dd'.txt'”的话，偶尔会出现奇怪的目录层叠BUG，故放弃-->
      <!--切割最多文件数 -1表示不限制产生日志文件数-->
      <maxSizeRollBackups value="-1" />
      <!--单个日志文件最大的大小-->
      <maximumFileSize value="1024KB" />
      <!--是否使用静态文件名-->
      <staticLogFileName value="false" />
      <preserveLogFileNameExtension value="true"/>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%-5level %date [%-5.5thread] %-40.40c - %message%newline" />
      </layout>
      <!--过滤器-->
      <!--<filter type="log4net.Filter.LevelRangeFilter">
			<LevelMin value="DEBUG"/>
			<LevelMax value="FATAL"/>
		  </filter>-->
    </appender>
    <!--输出到html文件-->
    <appender name="RollingHtmlFileAppender" type="log4net.Appender.RollingFileAppender" >
      <!--输出到什么目录-->
      <file value="Logs/" />
      <!--是否覆写到文件中-->
      <appendToFile value="true" />
      <!--创建文件的规则-->
      <rollingStyle value="Composite" />
      <datePattern value="yyyy_MM_dd'.html'"/>
      <!--注：此处如果设为“yyyy/yyyy_MM/yyyy_MM_dd'.txt'”的话，偶尔会出现奇怪的目录层叠BUG，故放弃-->
      <!--切割最多文件数 -1表示不限制产生日志文件数-->
      <maxSizeRollBackups value="-1" />
      <!--单个日志文件最大的大小-->
      <maximumFileSize value="2048KB" />
      <!--是否使用静态文件名-->
      <staticLogFileName value="false" />
      <preserveLogFileNameExtension value="true"/>
      <!--这里的类型必须使用完全限定名，获取方法为：typeof(SkyDCore.Log.HtmlLogLayout).AssemblyQualifiedName-->
      <layout type="SkyDCore.Log.HtmlLogLayout, SkyDCore.Log, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
      <!--过滤器-->
      <!--<filter type="log4net.Filter.LevelRangeFilter">
			<LevelMin value="DEBUG"/>
			<LevelMax value="FATAL"/>
		  </filter>-->
    </appender>
    <root>
      <level value="ALL" />
      <appender-ref ref="RollingHtmlFileAppender" />
    </root>
  </log4net>
</configuration>
```

## 在 ASP.Net Core 中捕获全局异常的范例

自定义全局异常过滤器：

```c#
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataExchangeCenter.WebSite.Filters
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            this.GetLogger().Error("全局异常过滤器捕获的异常", context.Exception);
        }
    }
}
```
在 Startup.cs 中的 ConfigureServices 方法里启用该过滤器：
```c#
// 添加全局异常过滤器
services.AddMvc(option =>
{
    option.Filters.Add<Filters.GlobalExceptionFilter>();
});
```

## 在 WPF 中捕获全局异常的范例

修改 App.xaml.cs 文件：

```C#
        public App()
        {
            // 设置日志记录全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// 当某个异常未被捕获时出现。主要指的是非UI线程。
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error($"触发未捕获异常（{nameof(CurrentDomain_UnhandledException)}）", (Exception)e.ExceptionObject);
        }

        /// <summary>
        /// 如果异常是由应用程序引发，但未处理，发生。主要指的是UI线程。
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Log.Error($"触发未处理异常（{nameof(App_DispatcherUnhandledException)}）", e.Exception);
        }
```