using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDCore.Mathematics
{
    /// <summary>
    /// 二维向量类。移植于Robert Penner的AS代码。
    /// </summary>
    public class Vector2D
    {
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }
        private double _X;

        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }
        private double _Y;

        /// <summary>
        ///  构造函数
        /// </summary>
        public Vector2D()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">X向量</param>
        /// <param name="y">Y向量</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="vector2d">参照向量</param>
        public Vector2D(Vector2D vector2d)
        {
            X = vector2d.X;
            Y = vector2d.Y;
        }

        /// <summary>
        /// 重设向量值，并返回自身。
        /// </summary>
        /// <param name="x">X向量</param>
        /// <param name="y">Y向量</param>
        /// <returns>自身</returns>
        public Vector2D Reset(double x, double y)
        {
            X = x;
            Y = y;
            return this;
        }

        /// <summary>
        /// 设置为与目标相同的向量值，并返回自身。
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <returns>自身</returns>
        public Vector2D ResetWith(Vector2D target)
        {
            X = target.X;
            Y = target.Y;
            return this;
        }

        /// <summary>
        /// 克隆一个向量值相同的对象。
        /// </summary>
        /// <returns>一个向量值相同的对象</returns>
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }

        /// <summary>
        /// 向量相加，并返回自身。
        /// </summary>
        /// <param name="value">相加的对象</param>
        /// <returns>自身</returns>
        public Vector2D Plus(Vector2D value)
        {
            X += value.X;
            Y += value.Y;
            return this;
        }

        /// <summary>
        /// 向量增加，并返回自身。
        /// </summary>
        /// <param name="xValue">X轴增量</param>
        /// <param name="yValue">Y轴增量</param>
        /// <returns>自身</returns>
        public Vector2D Plus(double xValue, double yValue)
        {
            X += xValue;
            Y += yValue;
            return this;
        }

        /// <summary>
        /// 向量相减，并返回自身。
        /// </summary>
        /// <param name="value">相减的对象</param>
        /// <returns>自身</returns>
        public Vector2D Minus(Vector2D value)
        {
            X -= value.X;
            Y -= value.Y;
            return this;
        }

        /// <summary>
        /// 向量减少，并返回自身。
        /// </summary>
        /// <param name="xValue">X轴减量</param>
        /// <param name="yValue">Y轴减量</param>
        /// <returns>自身</returns>
        public Vector2D Minus(double xValue, double yValue)
        {
            X -= xValue;
            Y -= yValue;
            return this;
        }

        /// <summary>
        /// 负运算，获得相反数，并返回自身。
        /// </summary>
        /// <returns>自身</returns>
        public Vector2D Negate()
        {
            X = -X;
            Y = -Y;
            return this;
        }

        /// <summary>
        /// 缩放，即乘运算，并返回自身。
        /// </summary>
        /// <param name="value">乘算值</param>
        /// <returns>自身</returns>
        public Vector2D Scale(double value)
        {
            return Scale(value, value);
        }

        /// <summary>
        /// 缩放，即乘运算，并返回自身。
        /// </summary>
        /// <param name="value">乘算向量值</param>
        /// <returns>自身</returns>
        public Vector2D Scale(Vector2D value)
        {
            return Scale(value.X, value.Y);
        }

        /// <summary>
        /// 缩放，即乘运算，并返回自身。
        /// </summary>
        /// <param name="xValue">X乘算值</param>
        /// <param name="yValue">Y乘算值</param>
        /// <returns>自身</returns>
        public Vector2D Scale(double xValue, double yValue)
        {
            X *= xValue;
            Y *= yValue;
            return this;
        }

        /// <summary>
        /// 依据指定的中心点缩放，即乘运算，并返回自身。
        /// </summary>
        /// <param name="xValue">X乘算值</param>
        /// <param name="yValue">Y乘算值</param>
        /// <param name="center">中心点</param>
        /// <returns>自身</returns>
        public Vector2D ScaleByCustomCenter(double xValue, double yValue, Vector2D center)
        {
            Minus(center);
            Scale(xValue, yValue);
            Plus(center);
            return this;
        }

        /// <summary>
        /// 依据指定的中心点缩放，即乘运算，并返回自身。
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="value">乘算向量值</param>
        /// <returns>自身</returns>
        public Vector2D ScaleByCustomCenter(Vector2D value, Vector2D center)
        {
            return ScaleByCustomCenter(value.X, value.Y, center);
        }

        /// <summary>
        /// 向量长度，也称为向量的绝对值或模。根据X、Y的值，利用勾股定理求得。
        /// 设置此值可在不改变向量方向的前提下，重设向量的长度，但如果当前长度为0时，则该向量没有方向，那么设置长度后将会使X值等于设定的长度，Y值等于0。
        /// </summary>
        /// <returns>长度值</returns>
        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
            set
            {
                var len = Length;
                if (len == 0) X = value;
                else Scale(value / len);
            }
        }

        /// <summary>
        /// 重设向量长度，并返回自身。设置此值可在不改变向量方向的前提下，重设向量的长度，但如果当前长度为0时，则该向量没有方向，那么设置长度后将会使X值等于设定的长度，Y值等于0。
        /// </summary>
        /// <param name="value">长度值</param>
        /// <returns>自身</returns>
        public Vector2D ResetLength(double value)
        {
            Length = value;
            return this;
        }

        /// <summary>
        /// 向量方向角度。
        /// 设置此值可以在不改变向量长度的前提下，重设向量方向角度。
        /// </summary>
        public double Angle
        {
            get
            {
                return SkyDCoreMathAssist.Atan2Angle(_Y, _X);
            }
            set
            {
                var len = Length;
                X = len * SkyDCoreMathAssist.CosAngle(value);
                Y = len * SkyDCoreMathAssist.SinAngle(value);
            }
        }

        /// <summary>
        /// 重设向量角度，并返回自身。设置此值可在不改变向量长度的前提下，重设向量方向角度。
        /// </summary>
        /// <param name="value">新角度值，一个0至360之间的值</param>
        /// <returns>自身</returns>
        public Vector2D ResetAngle(double value)
        {
            Angle = value;
            return this;
        }

        /// <summary>
        /// 以当前角度为基础，旋转指定角度，并返回自身。
        /// </summary>
        /// <param name="angle">旋转角度值，一个0至360之间的值</param>
        /// <returns>自身</returns>
        public Vector2D Rotate(double angle)
        {
            double ca = SkyDCoreMathAssist.CosAngle(angle);
            double sa = SkyDCoreMathAssist.SinAngle(angle);
            double rx = X * ca - Y * sa;
            double ry = X * sa + Y * ca;
            X = rx;
            Y = ry;
            return this;
        }

        /// <summary>
        /// 以当前角度为基础，围绕指定中心点旋转指定角度，并返回自身。
        /// </summary>
        /// <param name="angle">旋转角度值，一个0至360之间的值</param>
        /// <param name="center">参照中心点</param>
        /// <returns>自身</returns>
        public Vector2D RotateByCustomCenter(double angle, Vector2D center)
        {
            Minus(center);
            Rotate(angle);
            Plus(center);
            return this;
        }

        /// <summary>
        /// 将两轴分别乘算并相加以获得点积（数量积）。如果两个向量点积为0，则它们互相垂直。
        /// </summary>
        /// <param name="v">乘算对象</param>
        /// <returns>点积</returns>
        public double Dot(Vector2D v)
        {
            return X * v.X + Y * v.Y;
        }

        /// <summary>
        /// 判断是否垂直于目标向量
        /// </summary>
        /// <param name="v">检测垂直的目标向量</param>
        /// <returns>是否垂直</returns>
        public bool IsPerpTo(Vector2D v)
        {
            return Dot(v) == 0;
        }

        /// <summary>
        /// 获得顺时针法向量，即与当前向量成90°夹角的向量。通常在力学计算中使用。
        /// </summary>
        /// <returns>法向量对象</returns>
        public Vector2D GetNormalCW()
        {
            return new Vector2D(Y, -X);
        }

        /// <summary>
        /// 获得逆时针法向量，即与当前向量成90°夹角的向量。通常在力学计算中使用。
        /// </summary>
        /// <returns>法向量对象</returns>
        public Vector2D GetNormalCCW()
        {
            return new Vector2D(-Y, X);
        }

        /// <summary>
        /// 计算向量间夹角的角度
        /// </summary>
        /// <param name="v">目标向量</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public double AngleBetween(Vector2D v)
        {
            double dp = Dot(v);
            double cosAngle = dp / (Length * v.Length);
            return SkyDCoreMathAssist.AcosAngle(cosAngle);
        }

        /// <summary>
        /// 约束长度，并返回自身。即如果自身长度大于给定长度，则重设长度为给定值。
        /// </summary>
        /// <param name="v">值</param>
        /// <returns>自身</returns>
        public Vector2D RestrainLength(double v)
        {
            if (Length > v)
                Length = v;
            return this;
        }

        /// <summary>
        /// 将向量设为自身与目标向量的均值
        /// </summary>
        /// <param name="v">目标向量</param>
        /// <returns>自身</returns>
        public Vector2D AverageWith(Vector2D v)
        {
            X = (X + v.X) / 2;
            Y = (Y + v.Y) / 2;
            return this;
        }

        /// <summary>
        /// 将X轴的值设定为指定值，并将Y轴按同比例缩放
        /// </summary>
        /// <param name="v">指定值</param>
        /// <returns>自身</returns>
        public Vector2D ProportionallyConstraintsWidthX(double v)
        {
            var sc = X / v;
            X = v;
            Y = Y / sc;
            return this;
        }

        /// <summary>
        /// 将Y轴的值设定为指定值，并将X轴按同比例缩放
        /// </summary>
        /// <param name="v">指定值</param>
        /// <returns>自身</returns>
        public Vector2D ProportionallyConstraintsWidthY(double v)
        {
            var sc = Y / v;
            Y = v;
            X = X / sc;
            return this;
        }
    }
}
