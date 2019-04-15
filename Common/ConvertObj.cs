using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Common
{
    public class ConvertObj<T> where T : new()
    {
        //实体属性集合
        private static List<string> _props;

        /// <summary>
        /// DataTable映射到实体类
        /// </summary>
        /// <param name="dt">DataTable数据表</param>
        /// <returns>包含实体数据类的List集合</returns>
        public static List<T> GetDtToList(DataTable dt)
        {
            List<T> listObj = null;

            if (dt != null)
            {
                listObj = new List<T>();
                Type type = typeof(T);

                DataColumnCollection dcs = dt.Columns;
                PropertyInfo[] pis = type.GetProperties();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    if (type.FullName == null)
                    {
                        return null;
                    }
                    T t = new T();
                    for (int j = 0; j < dcs.Count; j++)
                    {
                        DataColumn dc = dcs[j];
                        string columnName = dc.ColumnName;
                        var value = row[columnName];
                        if (value != null && value != DBNull.Value)
                        {
                            for (int k = 0; k < pis.Length; k++)
                            {
                                if (columnName == pis[k].Name)
                                {
                                    pis[k].SetValue(t, row[columnName], null);
                                    break;
                                }
                            }
                        }
                    }
                    listObj.Add(t);
                }
            }

            return listObj;
        }

        /// <summary>
        /// 获取实体属性
        /// </summary>
        /// <returns>实体属性集合</returns>
        public static List<string> GetProp()
        {
            if (_props == null)
            {
                _props = new List<string>();
                Type type = typeof(T);
                PropertyInfo[] pis = type.GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    _props.Add(pi.Name.ToLower());
                }
            }
            return _props;
        }

        /// <summary>
        /// 实体属性是否存在
        /// </summary>
        /// <returns>是 或 否</returns>
        public static bool PropIsExists(string propKey)
        {
            return GetProp().Contains(propKey.ToLower());
        }

        /// <summary>
        /// 将实体转换为字符串
        /// </summary>
        /// <param name="t">T类型实例</param>
        /// <returns>结果字符串</returns>
        public static string ToString(T t)
        {
            string result = "{ ";

            Type type = typeof(T);
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                result += string.Format("{0}: {1} ,", pi.Name, pi.GetValue(t, null));
            }

            result = result.Remove(result.Length - 1, 1);
            result += " }";
            return result;
        }
    }
}
