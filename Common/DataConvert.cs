using System;

namespace Common
{
    public class DataConvert
    {
        public static int DateTimeToUnixInt(DateTime time)
        {
            DateTime time2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            TimeSpan span = time - time2;
            return (int)span.TotalSeconds;
        }

        public static decimal MoneyRound(decimal value, int decimals)
        {
            if (value < 0M)
            {
                return Math.Round(value + (5M / ((decimal)Math.Pow(10.0, decimals + 1))), decimals, MidpointRounding.AwayFromZero);
            }
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static double MoneyRound(double value, int decimals)
        {
            if (value < 0.0)
            {
                return Math.Round(value + (5.0 / Math.Pow(10.0, decimals + 1)), decimals, MidpointRounding.AwayFromZero);
            }
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static decimal MoneyToDecimal(string str)
        {
            try
            {
                str = str.Replace(",", "");
                return decimal.Parse(str);
            }
            catch
            {
                return 0M;
            }
        }

        public static decimal Round(decimal d, int i)
        {
            if (d >= 0M)
            {
                d += 5M * ((decimal)Math.Pow(10.0, -(i + 1)));
            }
            else
            {
                d += -5M * ((decimal)Math.Pow(10.0, -(i + 1)));
            }
            string str = d.ToString();
            string[] strArray = str.Split('.');
            int index = str.IndexOf('.');
            string str2 = strArray[0];
            string str3 = strArray[1];
            if (str3.Length > i)
            {
                str3 = str.Substring(index + 1, i);
            }
            d = decimal.Parse(str2 + "." + str3);
            return d;
        }

        public static double Round(double d, int i)
        {
            if (d >= 0.0)
            {
                d += 5.0 * Math.Pow(10.0, -(i + 1));
            }
            else
            {
                d += -5.0 * Math.Pow(10.0, -(i + 1));
            }
            string str = d.ToString();
            string[] strArray = str.Split('.');
            int index = str.IndexOf('.');
            string str2 = strArray[0];
            string str3 = strArray[1];
            if (str3.Length > i)
            {
                str3 = str.Substring(index + 1, i);
            }
            d = double.Parse(str2 + "." + str3);
            return d;
        }

        public static bool ToBool(object o)
        {
            return ToBool(o, false);
        }

        public static bool ToBool(object o, bool DefaultValue)
        {
            bool flag;
            if (bool.TryParse(ToString(o, true), out flag))
            {
                return flag;
            }
            return DefaultValue;
        }

        public static DateTime ToDateTime(object o)
        {
            return ToDateTime(o, DateTime.Now);
        }

        public static DateTime ToDateTime(object o, DateTime DefaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(ToString(o, true), out time))
            {
                return time;
            }
            return DefaultValue;
        }

        public static decimal ToDecimal(object o)
        {
            return ToDecimal(o, 0M);
        }

        public static decimal ToDecimal(object o, decimal DefaultValue)
        {
            decimal num;
            if (decimal.TryParse(ToString(o, true), out num))
            {
                return num;
            }
            return DefaultValue;
        }

        public static double ToDouble(object o)
        {
            return ToDouble(o, 0.0);
        }

        public static double ToDouble(object o, double DefaultValue)
        {
            double num;
            if (double.TryParse(ToString(o, true), out num))
            {
                return num;
            }
            return DefaultValue;
        }

        public static float ToFloat(object o)
        {
            return ToFloat(o, 0f);
        }

        public static float ToFloat(object o, float DefaultValue)
        {
            float num;
            if (float.TryParse(ToString(o, true), out num))
            {
                return num;
            }
            return DefaultValue;
        }

        public static int ToInt(object o)
        {
            return ToInt(o, 0);
        }

        public static int ToInt(object o, int DefaultValue)
        {
            int num;
            if (int.TryParse(ToString(o, true), out num))
            {
                return num;
            }
            return DefaultValue;
        }

        public static long ToLong(object o)
        {
            return ToLong(o, 0L);
        }

        public static long ToLong(object o, long DefaultValue)
        {
            long num;
            if (long.TryParse(ToString(o, true), out num))
            {
                return num;
            }
            return DefaultValue;
        }

        public static string ToMoney(object o)
        {
            return ToDecimal(o).ToString("###,###,###,###,###,##0.##");
        }

        public static string ToMoney(string str)
        {
            try
            {
                return decimal.Parse(str).ToString("###,###,###,###,###,##0.##");
            }
            catch
            {
                return "0";
            }
        }

        public static string ToString(object o)
        {
            return ToString(o, "", false);
        }

        public static string ToString(object o, bool bTrim)
        {
            return ToString(o, "", bTrim);
        }

        public static string ToString(object o, string DefaultValue)
        {
            return ToString(o, DefaultValue, false);
        }

        public static string ToString(object o, string DefaultValue, bool bTrim)
        {
            if (Equals(o, null) || Convert.IsDBNull(o))
            {
                return DefaultValue;
            }
            if (bTrim)
            {
                return o.ToString().Trim();
            }
            return o.ToString();
        }

        public static DateTime UnixIntToDateTime(string timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            long ticks = long.Parse(timeStamp + "0000000");
            TimeSpan span = new TimeSpan(ticks);
            return time.Add(span);
        }
    }
}
