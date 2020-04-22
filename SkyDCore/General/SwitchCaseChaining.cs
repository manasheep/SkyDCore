using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDCore.General
{
    public abstract partial class SwitchCaseBase<T>
    {
        protected bool DefaultSet;

        protected void CheckDefaultSet()
        {
            if (DefaultSet) throw new Exception("Default操作必须在方法链末端执行，在其后不得执行Case操作。");
        }

        protected virtual T Value
        {
            get
            {
                return _Value;
            }
        }
        protected T _Value;

        protected virtual bool IsBroke
        {
            get
            {
                return _IsBroke;
            }
        }
        protected bool _IsBroke;

        internal void Break()
        {
            _IsBroke = true;
        }
    }

    public partial class Switch<T> : SwitchCaseBase<T>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Value">原始值</param>
        public Switch(T Value)
        {
            _Value = Value;
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        public Case<T, R> CaseReturn<R>(T Value, Func<T, R> Run)
        {
            return CaseReturn(f => f.Equals(Value), Run);
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="ReturnValue">返回结果</param>
        public Case<T, R> CaseReturn<R>(T Value, R ReturnValue)
        {
            return CaseReturn(f => f.Equals(Value), f => ReturnValue);
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="ReturnValue">返回结果</param>
        public Case<T, R> CaseReturn<R>(Predicate<T> Check, R ReturnValue)
        {
            return CaseReturn(Check, f => ReturnValue, true);
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        public Case<T, R> CaseReturn<R>(Predicate<T> Check, Func<T, R> Run)
        {
            return CaseReturn(Check, Run, true);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseReturn<R>(T Value, Func<T, R> Run, bool Break)
        {
            return CaseReturn(f => f.Equals(Value), Run, Break);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="ReturnValue">返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseReturn<R>(T Value, R ReturnValue, bool Break)
        {
            return CaseReturn(f => f.Equals(Value), f => ReturnValue, Break);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="ReturnValue">返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseReturn<R>(Predicate<T> Check, R ReturnValue, bool Break)
        {
            return CaseReturn(Check, f => ReturnValue, Break);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param> 
        public Case<T, R> CaseReturn<R>(Predicate<T> Check, Func<T, R> Run, bool Break)
        {
            CheckDefaultSet();
            var r = new Case<T, R>(this.Value, this.IsBroke, this.DefaultSet);
            if (IsBroke)
            {
                return r;
            }
            if (Check(Value))
            {
                r.SetReturnValue(Run(Value));
                if (Break) r.Break();
            }
            return r;
        }

        /// <summary>
        /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
        /// </summary>
        /// <param name="ReturnValue">返回结果</param>
        public Case<T, R> DefaultReturn<R>(R ReturnValue)
        {
            return DefaultReturn(f => ReturnValue);
        }

        /// <summary>
        /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
        /// </summary>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        public Case<T, R> DefaultReturn<R>(Func<T, R> Run)
        {
            DefaultSet = true;
            var r = new Case<T, R>(this.Value, this.IsBroke, this.DefaultSet);
            if (IsBroke)
            {
                return r;
            }
            r.SetReturnValue(Run(this.Value));
            return r;
        }

        /// <summary>
        /// 匹配并执行传入方法（匹配成功后自动Break）
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        public Switch<T> CaseRun(T Value, Action<T> Run)
        {
            return CaseRun(f => f.Equals(Value), Run);
        }

        /// <summary>
        /// 匹配并执行传入方法
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Switch<T> CaseRun(T Value, Action<T> Run, bool Break)
        {
            return CaseRun(f => f.Equals(Value), Run, Break);
        }

        /// <summary>
        /// 匹配并执行传入方法（匹配成功后自动Break）
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        public Switch<T> CaseRun(Predicate<T> Check, Action<T> Run)
        {
            return CaseRun(Check, Run, true);
        }

        /// <summary>
        /// 匹配并执行传入方法
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Switch<T> CaseRun(Predicate<T> Check, Action<T> Run, bool Break)
        {
            CheckDefaultSet();
            if (IsBroke) return this;
            if (Check(this.Value))
            {
                Run(this.Value);
                if (Break) _IsBroke = true;
            }
            return this;
        }

        /// <summary>
        /// 默认执行方法，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
        /// </summary>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        public void DefaultRun(Action<T> Run)
        {
            DefaultSet = true;
            if (IsBroke) return;
            Run(this.Value);
        }
    }

    public partial class Case<T, R> : SwitchCaseBase<T>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Value">原始值</param>
        public Case(T Value)
        {
            _Value = Value;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Value">原始值</param>
        /// <param name="Do">处理返回结果的方法，该方法将在每次执行CaseReturn并匹配成功时或执行DefaultReturn时调用，方法的第一个参数是新传入的返回值，第二个参数是当前的返回值</param>
        public Case(T Value, Func<R, R, R> Do)
        {
            _Value = Value;
            this.Do = Do;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Value">原始值</param>
        /// <param name="IsBroke">是否已结束</param>
        /// <param name="DefaultSet">是否已执行过默认操作</param>
        public Case(T Value, bool IsBroke, bool DefaultSet)
        {
            _Value = Value;
            _IsBroke = IsBroke;
            this.DefaultSet = DefaultSet;
        }

        protected Func<R, R, R> Do;

        /// <summary>
        /// 最终返回结果
        /// </summary>
        public R ReturnValue
        {
            get
            {
                return _ReturnValue;
            }
        }
        private R _ReturnValue;

        internal void SetReturnValue(R Value)
        {
            if (Do == null)
                _ReturnValue = Value;
            else
                _ReturnValue = Do(Value, ReturnValue);
        }

        /// <summary>
        /// 匹配并执行传入方法（匹配成功后自动Break）
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        public Case<T, R> CaseRun(T Value, Action<T> Run)
        {
            return CaseRun(f => f.Equals(Value), Run);
        }

        /// <summary>
        /// 匹配并执行传入方法
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseRun(T Value, Action<T> Run, bool Break)
        {
            return CaseRun(f => f.Equals(Value), Run, Break);
        }


        /// <summary>
        /// 匹配并执行传入方法（匹配成功后自动Break）
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        public Case<T, R> CaseRun(Predicate<T> Check, Action<T> Run)
        {
            return CaseRun(Check, Run, true);
        }

        /// <summary>
        /// 匹配并执行传入方法
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseRun(Predicate<T> Check, Action<T> Run, bool Break)
        {
            CheckDefaultSet();
            if (IsBroke) return this;
            if (Check(this.Value))
            {
                Run(this.Value);
                if (Break) _IsBroke = true;
            }
            return this;
        }

        /// <summary>
        /// 默认执行方法，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
        /// </summary>
        /// <param name="Run">执行的方法，其参数为原始值</param>
        public Case<T, R> DefaultRun(Action<T> Run)
        {
            DefaultSet = true;
            if (IsBroke) return this;
            Run(this.Value);
            return this;
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        public Case<T, R> CaseReturn(T Value, Func<T, R> Run)
        {
            return CaseReturn(f => f.Equals(Value), Run);
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="ReturnValue">返回结果</param>
        public Case<T, R> CaseReturn(T Value, R ReturnValue)
        {
            return CaseReturn(f => f.Equals(Value), f => ReturnValue);
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        public Case<T, R> CaseReturn(Predicate<T> Check, Func<T, R> Run)
        {
            return CaseReturn(Check, Run, true);
        }

        /// <summary>
        /// 匹配并生成返回值（匹配成功后自动Break）。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="ReturnValue">返回结果</param>
        public Case<T, R> CaseReturn(Predicate<T> Check, R ReturnValue)
        {
            return CaseReturn(Check, f => ReturnValue, true);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseReturn(T Value, Func<T, R> Run, bool Break)
        {
            return CaseReturn(f => f.Equals(Value), Run, Break);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Value">匹配值</param>
        /// <param name="ReturnValue">返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseReturn(T Value, R ReturnValue, bool Break)
        {
            return CaseReturn(f => f.Equals(Value), f => ReturnValue, Break);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="ReturnValue">返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param>
        public Case<T, R> CaseReturn(Predicate<T> Check, R ReturnValue, bool Break)
        {
            return CaseReturn(Check, f => ReturnValue, Break);
        }

        /// <summary>
        /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
        /// </summary>
        /// <param name="Check">匹配验证方法</param>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        /// <param name="Break">是否在匹配成功后结束</param> 
        public Case<T, R> CaseReturn(Predicate<T> Check, Func<T, R> Run, bool Break)
        {
            CheckDefaultSet();
            if (IsBroke)
            {
                return this;
            }
            if (Check(Value))
            {
                SetReturnValue(Run(Value));
                if (Break) this.Break();
            }
            return this;
        }

        /// <summary>
        /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
        /// </summary>
        /// <param name="ReturnValue">返回结果</param>
        public Case<T, R> DefaultReturn(R ReturnValue)
        {
            return DefaultReturn(f => ReturnValue);
        }

        /// <summary>
        /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
        /// </summary>
        /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
        public Case<T, R> DefaultReturn(Func<T, R> Run)
        {
            DefaultSet = true;
            if (IsBroke)
            {
                return this;
            }
            SetReturnValue(Run(this.Value));
            return this;
        }
    }
}
