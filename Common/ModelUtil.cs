using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Common
{
    /// <summary>
    ///   #region DataTable转换成实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelUtil<T> where T : new()
    {

        /// <summary>
        /// 填充对象列表：用DataSet的第一个表填充实体类
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public static List<T> FillModel(DataSet ds)
        {
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            return FillModel(ds.Tables[0]);
        }

        /// <summary>  
        /// 填充对象列表：用DataSet的第index个表填充实体类
        /// </summary>  
        public static List<T> FillModel(DataSet ds, int index)
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            return FillModel(ds.Tables[index]);
        }

        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public static List<T> FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //T model = (T)Activator.CreateInstance(typeof(T));  
                T model = new T();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    List<PropertyInfo> propertyInfos = model.GetType().GetProperties().ToList();

                    PropertyInfo item = null;
                    foreach (var p in propertyInfos)
                    {
                        if (string.Compare(p.Name, dr.Table.Columns[i].ColumnName, true) == 0)
                        {
                            item = p;
                            break;
                        }
                    }
                    if (item != null && dr[i] != DBNull.Value)
                        try
                        {
                            item.SetValue(model, dr[i], null);
                        }
                        catch
                        {
                        }
                }

                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>  
        /// 填充对象：用DataRow填充实体类
        /// </summary>  
        public static T FillModel(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            //T model = (T)Activator.CreateInstance(typeof(T));  
            T model = new T();

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                List<PropertyInfo> propertyInfos = model.GetType().GetProperties().ToList();

                PropertyInfo item = propertyInfos.FirstOrDefault(p => String.Compare(p.Name, dr.Table.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase) == 0);
                if (item != null && dr[i] != DBNull.Value)
                    try
                    {
                        item.SetValue(model, dr[i], null);
                    }
                    catch
                    {
                    }
            }
            return model;
        }


        /// <summary>
        /// 实体类转换成DataSet
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataSet FillDataSet(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(FillDataTable(modelList));
            return ds;
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable FillDataTable(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataTable dt = CreateData(modelList[0]);

            foreach (T model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private static DataTable CreateData(T model)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }

        /// <summary>
        /// 将DataTable以转为List与Dictionary嵌套集合保存
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> ToListDictionary(DataTable dt)
        {
            List<Dictionary<string, string>> list = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = null;
                foreach (DataRow dr in dt.Rows)
                {
                    dic = new Dictionary<string, string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                    }
                    list.Add(dic);
                }
            }
            return list;
        }

        /// <summary>
        /// 请求的request的内容转换为model
        /// cza
        /// 2016-5-30 19:06:21
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T ConvertToModel(HttpContext context)
        {
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (!pi.CanWrite)
                    continue;
                object value = context.Request[pi.Name];
                if (value != null && value != DBNull.Value)
                {
                    try
                    {
                        if (value.ToString() != "")
                            pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);//这一步很重要，用于类型转换
                        else
                            pi.SetValue(t, value, null);
                    }
                    catch
                    { }
                }
            }

            return t;
        }


    }
}
