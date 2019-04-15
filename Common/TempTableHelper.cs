using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    class TempTableHelper
    {
        #region Public static
        public static TempTableStings GetStrings(DataTable TempTable)
        {
            TempTableStings Strings = new TempTableStings();
            #region CreateString
            string FieldString = "";
            foreach (DataColumn column in TempTable.Columns)
            {
                FieldString += String.Format(FieldStringModel, column.ColumnName, StringOfType(column.DataType));
            }
            FieldString = FieldString.Remove(0, 1);
            Strings.CreatString = String.Format(CreatStringModel, "temp", FieldString);
            #endregion
            #region DropString
            Strings.DropString = String.Format(DropStringModel, "temp");
            #endregion
            #region DataString
            int maxDataLength = 0;
            if (TempTable.Rows.Count > 0)
            {
                string DataString = "";
                string TempString = "";

                int length;
                List<string> TempList = new List<string>();
                foreach (DataRow row in TempTable.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    TempList.Clear();
                    foreach (object item in row.ItemArray)
                    {
                        if (item == DBNull.Value || item.ToString().ToLower() == "null")
                        {
                            TempList.Add("null");
                        }
                        else
                        {
                            if (item is bool)
                            {
                                TempList.Add((bool)item ? "1" : "0");
                            }
                            else
                            {
                                TempList.Add(item.ToString());
                                length = System.Text.Encoding.Default.GetBytes(item.ToString()).Length;
                                maxDataLength = length > maxDataLength ? length : maxDataLength;
                            }
                        }
                    }
                    TempString = String.Join("','", TempList.ToArray());
                    TempString = "'" + TempString + "'";
                    TempString = TempString.Replace("'null'", "null");
                    DataString += String.Format(DataStringModel, "temp", TempString) + "\r\n";
                }
                Strings.DataString = DataString;
            }
            else
            {
                Strings.DataString = "";
            }

            if (maxDataLength < 255)
            {
                Strings.CreatString = Strings.CreatString.Replace("#DataLength#", "255");
            }
            else if (maxDataLength < 500)
            {
                Strings.CreatString = Strings.CreatString.Replace("#DataLength#", "500");
            }
            else if (maxDataLength < 1000)
            {
                Strings.CreatString = Strings.CreatString.Replace("#DataLength#", "1000");
            }
            else if (maxDataLength < 2000)
            {
                Strings.CreatString = Strings.CreatString.Replace("#DataLength#", "2000");
            }
            else if (maxDataLength < 8000)
            {
                Strings.CreatString = Strings.CreatString.Replace("#DataLength#", "8000");
            }
            else
            {
                Strings.CreatString = Strings.CreatString.Replace("#DataLength#", "max");
            }
            #endregion
            return Strings;
        }
        #endregion

        #region Private
        #region 成员
        private static string CreatStringModel = "create table #{0} ({1})";
        private static string FieldStringModel = ",[{0}] {1}";
        private static string DropStringModel = "drop table #{0}";
        private static string DataStringModelFirst = " select {0} ";
        private static string DataStringModel = " insert into #{0} select {1} ";
        #endregion
        #region 方法
        private static string StringOfType(Type type)
        {
            if (type == typeof(int))
            {
                return "int";
            }
            else if (type == typeof(double))
            {
                return "float";
            }
            else if (type == typeof(float))
            {
                return "float";
            }
            else if (type == typeof(bool))
            {
                return "bit";
            }
            else if (type == typeof(DateTime))
            {
                return "smalldatetime";
            }
            else
            {
                return "varchar(#DataLength#)";
            }
        }
        #endregion
        #endregion
    }
    /// <summary>
    /// 临时表的创建，插入，删除语句
    /// </summary>
    public class TempTableStings
    {
        public string CreatString;
        public string DataString;
        public string DropString;
    }
}
