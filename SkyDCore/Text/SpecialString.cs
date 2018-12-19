using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDCore.Text
{
    /// <summary>
    /// 特殊字符串基类
    /// </summary>
    public abstract class SpecialString : ISpecialString
    {
        public SpecialString()
        {

        }

        public SpecialString(string value)
        {
            _Value = value;
        }

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        private string _Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
