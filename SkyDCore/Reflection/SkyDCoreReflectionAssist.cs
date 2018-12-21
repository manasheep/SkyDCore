using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace SkyDCore.Reflection
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
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
        /// <param name="expressionToGetProperty">获取属性的表达式</param>     
        /// <returns>属性的名称</returns>     
        public static string GetPropertyName<T, PT>(Expression<Func<T, PT>> expressionToGetProperty)
        {
            string rtn = string.Empty;
            if (expressionToGetProperty.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expressionToGetProperty.Body).Operand).Member.Name;
            }
            else if (expressionToGetProperty.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expressionToGetProperty.Body).Member.Name;
            }
            else if (expressionToGetProperty.Body is ParameterExpression)
            {
                rtn = expressionToGetProperty.Body.Type.Name;
            }
            return rtn;
        }

        /// <summary>     
        /// 获取属性的类型
        /// </summary>     
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="PT">属性类型</typeparam>
        /// <param name="expressionToGetProperty">获取属性的表达式</param>     
        /// <returns>属性的类型</returns>     
        public static Type GetPropertyType<T, PT>(Expression<Func<T, PT>> expressionToGetProperty)
        {
            Type rtn = null;
            if (expressionToGetProperty.Body is UnaryExpression)
            {
                rtn = SkyDCoreGeneralExtension.GetPropertyValue(((MemberExpression)((UnaryExpression)expressionToGetProperty.Body).Operand).Member, "PropertyType") as Type;
            }
            else if (expressionToGetProperty.Body is MemberExpression)
            {
                rtn = SkyDCoreGeneralExtension.GetPropertyValue(((MemberExpression)expressionToGetProperty.Body).Member, "PropertyType") as Type;
            }
            else if (expressionToGetProperty.Body is ParameterExpression)
            {
                rtn = expressionToGetProperty.Body.Type;
            }
            return rtn;
        }

        /// <summary>
        /// 通过反射获取属性值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this object o, string propertyName)
        {
            return o.GetType().GetProperty(propertyName, flags).GetValue(o, null);
        }

        /// <summary>
        /// 通过反射设置属性值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        public static void SetPropertyValue(this object o, string propertyName, object propertyValue)
        {
            o.GetType().GetProperty(propertyName, flags).SetValue(o, propertyValue, null);
        }

        private static BindingFlags flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreCase;

        /// <summary>
        /// 获取指定类的特定类型属性的映射索引，可以通过调用获取到的实例的GetValue及SetValue方法操作属性值
        /// </summary>
        /// <typeparam name="Type">要获取的对象类型</typeparam>
        /// <typeparam name="PropertyType">要返回的映射索引的属性类型</typeparam>
        /// <returns>属性映射索引</returns>
        public static List<PropertyInfo> GetPropertyMapIndexes<Type, PropertyType>()
        {
            List<PropertyInfo> l = new List<PropertyInfo>();
            foreach (PropertyInfo f in typeof(Type).GetProperties())
            {
                if (f.PropertyType == typeof(PropertyType)) l.Add(f);
            }
            return l;
        }

        /// <summary>
        /// 获取指定类的所有类型属性的映射索引，可以通过调用获取到的实例的GetValue及SetValue方法操作属性值
        /// </summary>
        /// <typeparam name="Type">要获取的对象类型</typeparam>
        /// <returns>属性映射索引</returns>
        public static List<PropertyInfo> GetPropertyMapIndexes<Type>()
        {
            List<PropertyInfo> l = new List<PropertyInfo>();
            foreach (PropertyInfo f in typeof(Type).GetProperties())
            {
                l.Add(f);
            }
            return l;
        }

        /// <summary>
        /// 获取指定枚举类型的所有枚举项
        /// </summary>
        /// <returns>枚举项</returns>
        public static Array GetEnumItems<Enum>()
        {
            return System.Enum.GetValues(typeof(Enum));
        }

        /// <summary>
        /// 获得程序集全名，如“Core, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null”
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>程序集全名</returns>
        public static string GetAssemblyFullNameByType(this Type type)
        {
            return type.Assembly.FullName;
        }

        /// <summary>
        /// 获得类型全名，如“Core.函数库.解析”
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>类型全名</returns>
        public static string GetTypeFullName(this Type type)
        {
            return type.FullName;
        }

        /// <summary>
        /// 获取程序集限定名，如“Core.函数库.解析, Core, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null”
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>程序集限定名</returns>
        public static string GetTypeAssemblyQualifiedName(this Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}
