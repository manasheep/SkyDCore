using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkyDCore.General
{
    /// <summary>
    /// 用于排序的比较器，支持Lambda表达式
    /// </summary>
    /// <typeparam name="T">排序对象类型</typeparam>
    public class ComparisonComparer<T> : IComparer<T>, IComparer
    {
        private readonly Comparison<T> _comparison;
        public ComparisonComparer(Comparison<T> comparison)
        {
            _comparison = comparison;
        }
        public int Compare(T x, T y)
        {
            return _comparison(x, y);
        }
        public int Compare(object o1, object o2)
        {
            return _comparison((T)o1, (T)o2);
        }
    }
}
