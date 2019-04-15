using System;

namespace Common
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class CommonExtendMethod
    {
        /// <summary>
        /// 验证该值是否大于0，一般用于验证Id
        /// </summary>
        /// <param name="i">值</param>
        /// <returns>是否大于0</returns>
        public static bool ValidZero(this int i)
        {
            return i > 0;
        }

        /// <summary>
        /// 快速获取指定格式的日期字符串
        /// </summary>
        /// <param name="format">日期格式字符串</param>
        /// <returns>日期字符串</returns>
        public static string GetCurrentDate(this string format)
        {
            if (format == "")
            {
                format = "yyyy-MM-dd";
            }
            return DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="s">值</param>
        /// <returns>为空返回true，否则返回false</returns>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串转换为int，一般用于id和分页参数
        /// </summary>
        /// <param name="s">要转换成int的字符串</param>
        /// <param name="num">代替空值</param>
        /// <returns></returns>
        public static int ParseInt(this string s, string num = "0")
        {
            return int.Parse(!IsEmpty(s) ? s : num);
        }

        /// <summary>
        /// 字符串转换为float，一般用于id和分页参数
        /// </summary>
        /// <param name="s">要转换成float的字符串</param>
        /// <param name="num">代替空值</param>
        /// <returns></returns>
        public static float ParseFloat(this string s, string num = "0.0")
        {
            return float.Parse(!IsEmpty(s) ? s : num);
        }

        /// <summary>
        /// 字符串转换为DateTime
        /// </summary>
        /// <param name="s">值</param>
        /// <returns>DateTime型值</returns>
        public static DateTime ParseDateTime(this string s)
        {
            return !IsEmpty(s) ? DateTime.Parse(s) : DateTime.Now;
        }

        /// <summary>
        /// 处理字符串是否为null
        /// </summary>
        /// <param name="s">值</param>
        /// <returns>传入值为null返回string.Empty</returns>
        public static string GetVal(this string s)
        {
            return s ?? string.Empty;
        }
    }
}
