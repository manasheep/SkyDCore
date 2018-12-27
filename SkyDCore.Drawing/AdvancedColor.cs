using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SkyDCore.Drawing
{
    /// <summary>
    /// 高级颜色类，封装了一个基本颜色对象在其Value属性中，支持以HSB方式修改颜色。
    /// </summary>
    public class AdvancedColor
    {
        public Color Value
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
        private Color _Value;

        /// <summary>
        /// 不透明度，取值范围为0-255
        /// </summary>
        public byte A
        {
            get
            {
                return Value.A;
            }
            set
            {
                Value = Color.FromArgb(value, Value.R, Value.G, Value.B);
            }
        }

        /// <summary>
        /// 红色，取值范围为0-255
        /// </summary>
        public byte R
        {
            get
            {
                return Value.R;
            }
            set
            {
                Value = Color.FromArgb(Value.A, value, Value.G, Value.B);
            }
        }

        /// <summary>
        /// 绿色，取值范围为0-255
        /// </summary>
        public byte G
        {
            get
            {
                return Value.G;
            }
            set
            {
                Value = Color.FromArgb(Value.A, Value.R, value, Value.B);
            }
        }

        /// <summary>
        /// 蓝色，取值范围为0-255
        /// </summary>
        public byte B
        {
            get
            {
                return Value.B;
            }
            set
            {
                Value = Color.FromArgb(Value.A, Value.R, Value.G, value);
            }
        }

        /// <summary>
        /// 色相，取值范围为0-360
        /// </summary>
        public float Hue
        {
            get
            {
                return Value.GetHue();
            }
            set
            {
                ResetHSB(value, Value.GetSaturation(), Value.GetBrightness());
            }
        }

        /// <summary>
        /// 饱和度，取值范围为0-1
        /// </summary>
        public float Saturation
        {
            get
            {
                return Value.GetHue();
            }
            set
            {
                ResetHSB(Value.GetHue(), value, Value.GetBrightness());
            }
        }

        /// <summary>
        /// 亮度，取值范围为0-1
        /// </summary>
        public float Brightness
        {
            get
            {
                return Value.GetHue();
            }
            set
            {
                ResetHSB(Value.GetHue(), Value.GetSaturation(), value);
            }
        }

        void ResetHSB(float h, float s, float b)
        {
            Value = GetColorFromHSB(A, h, s, b);
        }

        /// <summary>
        /// 通过HSB值创建一个颜色对象
        /// </summary>
        /// <param name="A">不透明度，取值范围为0-255</param>
        /// <param name="H">色相，取值范围为0-360</param>
        /// <param name="S">饱和度，取值范围为0-1</param>
        /// <param name="B">亮度，取值范围为0-1</param>
        /// <returns></returns>
        public static Color GetColorFromHSB(byte A, float H, float S, float B)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if (S == 0)
            {
                r = g = b = (byte)(B * 255);
            }
            else
            {
                int i = (int)Math.Floor(H / 60) % 6;
                var f = H / 60 - i;
                var p = B * (1 - S);
                var q = B * (1 - S * f);
                var t = B * (1 - S * (1 - f));
                switch (i)
                {
                    case 0:
                        r = (byte)(B * 255); g = (byte)(t * 255); b = (byte)(p * 255);
                        break;
                    case 1:
                        r = (byte)(q * 255); g = (byte)(B * 255); b = (byte)(p * 255);
                        break;
                    case 2:
                        r = (byte)(p * 255); g = (byte)(B * 255); b = (byte)(t * 255);
                        break;
                    case 3:
                        r = (byte)(p * 255); g = (byte)(q * 255); b = (byte)(B * 255);
                        break;
                    case 4:
                        r = (byte)(t * 255); g = (byte)(p * 255); b = (byte)(B * 255);
                        break;
                    case 5:
                        r = (byte)(B * 255); g = (byte)(p * 255); b = (byte)(q * 255);
                        break;
                }
            }

            return Color.FromArgb(A, r, g, b);
        }
    }
}
