using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace SkyDCore.Reflection
{
    public static class SkyDCoreReflectionAssist
    {
        /// <summary>
        /// 获取嵌套调用方法堆栈集合
        /// </summary>
        /// <returns>嵌套调用方法堆栈集合</returns>
        public static IEnumerable<string> GetMethodStackCollection()
        {
            return new System.Diagnostics.StackTrace(true).GetFrames().Skip(1).Select(q => q.GetMethod().DeclaringType.FullName + "." + q.GetMethod().Name);
        }

        /// <summary>
        /// 获取当前方法信息
        /// </summary>
        /// <returns>当前方法信息</returns>
        public static MethodBase GetMethodInfo()
        {
            System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
            System.Reflection.MethodBase mb = ss.GetFrame(1).GetMethod();
            return mb;
        }

        /// <summary>
        /// 获取当前方法全名
        /// </summary>
        /// <returns>当前方法全名</returns>
        public static string GetMethodFullName()
        {
            System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
            System.Reflection.MethodBase mb = ss.GetFrame(1).GetMethod();
            return mb.DeclaringType.FullName + "." + mb.Name;
        }

        /// <summary>
        /// 获取当前方法名称
        /// </summary>
        /// <returns>当前方法名称</returns>
        public static string GetMethodName()
        {
            System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
            System.Reflection.MethodBase mb = ss.GetFrame(1).GetMethod();
            return mb.Name;
        }

        /// <summary>     
        /// 获取属性的名称
        /// </summary>     
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="PT">属性类型</typeparam>
        /// <param name="表达式">获取属性的表达式</param>     
        /// <returns>属性的名称</returns>     
        public static string GetPropertyName<T, PT>(Expression<Func<T, PT>> 表达式)
        {
            string rtn = string.Empty;
            if (表达式.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)表达式.Body).Operand).Member.Name;
            }
            else if (表达式.Body is MemberExpression)
            {
                rtn = ((MemberExpression)表达式.Body).Member.Name;
            }
            else if (表达式.Body is ParameterExpression)
            {
                rtn = 表达式.Body.Type.Name;
            }
            return rtn;
        }

        /// <summary>     
        /// 获取属性的类型
        /// </summary>     
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="PT">属性类型</typeparam>
        /// <param name="表达式">获取属性的表达式</param>     
        /// <returns>属性的类型</returns>     
        public static Type GetPropertyType<T, PT>(Expression<Func<T, PT>> 表达式)
        {
            Type rtn = null;
            if (表达式.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)表达式.Body).Operand).Member.GetPropertyValue("PropertyType") as Type;
            }
            else if (表达式.Body is MemberExpression)
            {
                rtn = ((MemberExpression)表达式.Body).Member.GetPropertyValue("PropertyType") as Type;
            }
            else if (表达式.Body is ParameterExpression)
            {
                rtn = 表达式.Body.Type;
            }
            return rtn;
        }

        /// <summary>
        /// 通过反射获取属性值
        /// </summary>
        /// <param name="属性名">属性名</param>
        /// <returns>属性值</returns>
        public static object 反射获取属性值(this object o, string 属性名)
        {
            return o.GetType().GetProperty(属性名, flags).GetValue(o, null);
        }

        /// <summary>
        /// 通过反射设置属性值
        /// </summary>
        /// <param name="属性名">属性名</param>
        /// <param name="属性值">属性值</param>
        public static void 反射设置属性值(this object o, string 属性名, object 属性值)
        {
            o.GetType().GetProperty(属性名, flags).SetValue(o, 属性值, null);
        }

        private static BindingFlags flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreCase;

        /// <summary>
        /// 获取指定类的特定类型属性的映射索引，可以通过调用获取到的实例的GetValue及SetValue方法操作属性值
        /// </summary>
        /// <typeparam name="类型">要获取的对象类型</typeparam>
        /// <typeparam name="属性类型">要返回的映射索引的属性类型</typeparam>
        /// <returns>属性映射索引</returns>
        public static List<PropertyInfo> 获取属性映射索引<类型, 属性类型>()
        {
            List<PropertyInfo> l = new List<PropertyInfo>();
            foreach (PropertyInfo f in typeof(类型).GetProperties())
            {
                if (f.PropertyType == typeof(属性类型)) l.Add(f);
            }
            return l;
        }

        /// <summary>
        /// 获取指定类的所有类型属性的映射索引，可以通过调用获取到的实例的GetValue及SetValue方法操作属性值
        /// </summary>
        /// <typeparam name="类型">要获取的对象类型</typeparam>
        /// <returns>属性映射索引</returns>
        public static List<PropertyInfo> 获取属性映射索引<类型>()
        {
            List<PropertyInfo> l = new List<PropertyInfo>();
            foreach (PropertyInfo f in typeof(类型).GetProperties())
            {
                l.Add(f);
            }
            return l;
        }

        /// <summary>
        /// 获取指定枚举类型的所有枚举项
        /// </summary>
        /// <returns>枚举项</returns>
        public static Array 获取枚举项<枚举类型>()
        {
            return Enum.GetValues(typeof(枚举类型));
        }

        /// <summary>
        /// 获得程序集全名，如“Core, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null”
        /// </summary>
        /// <param name="类型">要解析的类型</param>
        /// <returns>程序集全名</returns>
        public static string 获取类型所在程序集全名(this Type 类型)
        {
            return 类型.Assembly.FullName;
        }

        /// <summary>
        /// 获得类型全名，如“Core.函数库.解析”
        /// </summary>
        /// <param name="类型">要解析的类型</param>
        /// <returns>类型全名</returns>
        public static string 获取类型全名(this Type 类型)
        {
            return 类型.FullName;
        }

        /// <summary>
        /// 获取程序集限定名，如“Core.函数库.解析, Core, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null”
        /// </summary>
        /// <param name="类型">要解析的类型</param>
        /// <returns>程序集限定名</returns>
        public static string 获取类型程序集限定名(this Type 类型)
        {
            return 类型.AssemblyQualifiedName;
        }
    }
}
