using System;
using System.Data;

namespace Common
{
    public class DbCommonHelper
    {
        /// <summary>
        /// 分页获取数据，手动排序
        /// </summary>
        /// <param name="fieldList">要查询的字段名</param>
        /// <param name="where">where条件，条件之前要加 and</param>
        /// <param name="colsKey">通过该字段分页排序，一般为主键</param>
        /// <param name="orderKey">通过该字段排序</param>
        /// <param name="orderSort">排序方式，ASC或DESC</param>
        /// <param name="tableName">查询的表名，设计关联表一起写上，如table1 t1 INNER table2 t2 ON t1.Id = t2.Id，或者table1, table2</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示的条数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string fieldList, string where, string colsKey, string orderKey, string orderSort, string tableName, int pageIndex, int pageSize, string whereTemp = "")
        {
            string sql = "select * from (select " + fieldList + ",cols = row_number() over(order by " + colsKey + " " + orderSort + ") from " + tableName + " where 1=1 " + where + ") as temp where cols>" + ((pageIndex - 1) * pageSize) + " and cols<=" + (pageIndex * pageSize) + whereTemp + " " + (!string.IsNullOrEmpty(orderKey) ? " order by " + orderKey + " " + orderSort : "");
            return BaseClass.ReadTable(sql);
        }


        /// <summary>
        /// 获取数据(不分页)，手动排序
        /// </summary>
        /// <param name="fieldList">要查询的字段名</param>
        /// <param name="where">where条件，条件之前要加 and</param>
        /// <param name="orderKey">通过该字段排序</param>
        /// <param name="orderSort">排序方式，ASC或DESC</param>
        /// <param name="tableName">查询的表名，设计关联表一起写上，如table1 t1 INNER table2 t2 ON t1.Id = t2.Id，或者table1, table2</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTableNoPage(string fieldList, string where, string orderKey, string orderSort, string tableName, string whereTemp = "")
        {
            string sql = "select " + fieldList + " from " + tableName + " where 1=1 " + where  + whereTemp + " " + (!string.IsNullOrEmpty(orderKey) ? " order by " + orderKey + " " + orderSort : "");
            return BaseClass.ReadTable(sql);
        }

        /// <summary>
        /// 分页获取数据，降序排列
        /// </summary>
        /// <param name="fieldList">要查询的字段名</param>
        /// <param name="where">where条件，条件之前要加 and</param>
        /// <param name="orderKey">通过该字段排序</param>
        /// <param name="tableName">查询的表名，设计关联表一起写上，如table1 t1 INNER table2 t2 ON t1.Id = t2.Id，或者table1, table2</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示的条数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTableDesc(string fieldList, string where, string orderKey, string tableName, int pageIndex, int pageSize)
        {
            string sql = "select * from (select " + fieldList + ",cols = row_number() over(order by " + orderKey + " desc) from " + tableName + " where 1=1 " + where + ") as temp where cols>" + ((pageIndex - 1) * pageSize) + " and cols<=" + (pageIndex * pageSize);
            return BaseClass.ReadTable(sql);
        }

        /// <summary>
        /// 分页获取数据，顺序排列
        /// </summary>
        /// <param name="fieldList">要查询的字段名</param>
        /// <param name="where">查询条件，条件之前要加 and</param>
        /// <param name="orderKey">通过该字段排序</param>
        /// <param name="tableName">查询的表名，设计关联表一起写上，如table1 t1 INNER table2 t2 ON t1.Id = t2.Id，或者table1, table2</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示的条数</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTableAsc(string fieldList, string where, string orderKey, string tableName, int pageIndex, int pageSize)
        {
            string sql = "select * from (select " + fieldList + ",cols = row_number() over(order by " + orderKey + ") from " + tableName + " where 1=1 " + where + ") as temp where cols>" + ((pageIndex - 1) * pageSize) + " and cols<=" + (pageIndex * pageSize);
            return BaseClass.ReadTable(sql);
        }

        /// <summary>
        /// 获取指定表的总记录数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="countFlag">通过指定统计的列名</param>
        /// <param name="where">查询条件，条件之前要加 and</param>
        /// <returns>总记录数</returns>
        public static int GetTableCount(string tableName, string where, string countFlag = "Id")
        {
            string sql = "select count(" + countFlag + ") from " + tableName + " where 1=1 " + where + ";";
            return Convert.ToInt32(BaseClass.ReadScalar(sql));
        }

        /// <summary>
        /// 指定表的主键Id加1的值（int型主键）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>int型主键Id加1后的值</returns>
        public static int GetMaxId(string tableName)
        {
            string sql = "select IDENT_CURRENT('" + tableName + "');";
            return Convert.ToInt32(BaseClass.ReadScalar(sql));
        }

        /// <summary>
        /// 自定义查询sql语句
        /// </summary>
        /// <param name="table">表名，设计关联表一起写上，如table1 t1 INNER table2 t2 ON t1.Id = t2.Id，或者table1, table2</param>
        /// <param name="field">字段，设计关联表别忘记别名+字段名</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <param name="orderKey">排序字段</param>
        /// <param name="orderSort">排序，默认降序DESC，升序ASC</param>
        /// <returns>数据集，无数据为null</returns>
        public static DataSet Select(string table, string field, string where, string orderKey = "", string orderSort = "DESC")
        {
            string sql = "SELECT " + field;
            sql += " FROM " + table;
            sql += " WHERE 1 = 1" + where;
            if (!string.IsNullOrEmpty(orderKey))
            {
                sql += " ORDER BY " + orderKey + " " + orderSort + ";";
            }
            else
            {
                sql += ";";
            }
            try
            {
                return BaseClass.ReadDataSet(sql);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 返回自定义查询sql语句
        /// </summary>
        /// <param name="table">表名，设计关联表一起写上，如table1 t1 INNER table2 t2 ON t1.Id = t2.Id，或者table1, table2</param>
        /// <param name="field">字段，设计关联表别忘记别名+字段名</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <param name="orderKey">排序字段</param>
        /// <param name="orderSort">排序，默认降序DESC，升序ASC</param>
        /// <returns>sql</returns>
        public static string SelectSql(string table, string field, string where, string orderKey = "", string orderSort = "DESC")
        {
            string sql = "SELECT " + field;
            sql = sql.TrimEnd(',');
            sql += " FROM " + table;
            sql += " WHERE 1 = 1" + where;
            if (!string.IsNullOrEmpty(orderKey))
            {
                sql += " ORDER BY " + orderKey + " " + orderSort + ";";
            }
            else
            {
                sql += ";";
            }
            return sql;
        }

        /// <summary>
        /// 自定义插入sql语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">insert语句into字段，用英文逗号分隔</param>
        /// <param name="value">insert语句values值，对应into字段，用英文逗号分隔</param>
        /// <returns>插入成功true，失败false</returns>
        public static int Insert(string table, string field, string value)
        {
            string sql = "INSERT INTO " + table;
            sql += " (" + field + ")";
            sql += " VALUES(" + value + ");";
            try
            {
                return BaseClass.ExecSql(sql);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 返回自定义插入sql语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">insert语句into字段，用英文逗号分隔</param>
        /// <param name="value">insert语句values值，对应into字段，用英文逗号分隔</param>
        /// <returns></returns>
        public static string InsertSql(string table, string field, string value)
        {
            string sql = "INSERT INTO " + table;
            sql += " (" + field + ")";
            sql += " VALUES(" + value + ");";
            return sql;
        }

        /// <summary>
        /// 自定义编辑sql语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">update语句field，"字段1=值1,字段2=值2"，别忘记字符串两边的英文单引号</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <returns>编辑成功true，失败false</returns>
        public static int Update(string table, string field, string where)
        {
            string sql = "UPDATE " + table;
            sql += " SET " + field;
            sql = sql.TrimEnd(',');
            sql += " WHERE 1 = 1" + where + ";";
            try
            {
                return BaseClass.ExecSql(sql);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 返回自定义编辑sql语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">update语句field，"字段1=值1,字段2=值2"，别忘记字符串两边的英文单引号</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <returns></returns>
        public static string UpdateSql(string table, string field, string where)
        {
            string sql = "UPDATE " + table;
            sql += " SET " + field;
            sql = sql.TrimEnd(',');
            sql += " WHERE 1 = 1" + where + ";";
            return sql;
        }

        /// <summary>
        /// 根据主键Id（int型）删除指定表的记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">int型的主键Id值</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        public static bool Delete(string tableName, string id, string where = "")
        {
            string sql = "delete from " + tableName + " where 1=1 " + where + " Id = " + id + ";";
            int row = BaseClass.ExecSql(sql);
            return row > 0;
        }

        /// <summary>
        /// 返回根据主键Id（int型）删除指定表的记录sql
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">int型的主键Id值</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <returns>sql</returns>
        public static string DeleteSql(string tableName, string id, string where = "")
        {
            string sql = "delete from " + tableName + " where 1=1 " + where + " Id = " + id + ";";
            return sql;
        }

        /// <summary>
        /// 根据主键Id（int型）删除指定表的模糊记录，IN关键字
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">要删除的主键序列</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <returns>是否删除成功，true成功，false失败</returns>
        public static bool DeleteIn(string tableName, string id, string where = "")
        {
            string sql = "delete from " + tableName + " where 1=1 " + where + " Id IN (" + id + ");";
            int row = BaseClass.ExecSql(sql);
            return row > 0;
        }

        /// <summary>
        /// 返回根据主键Id（int型）删除指定表的模糊记录，IN关键字sql
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">要删除的主键序列</param>
        /// <param name="where">查询条件，" AND 条件1=值2 AND 条件2=值2"</param>
        /// <returns>sql</returns>
        public static string DeleteInSql(string tableName, string id, string where = "")
        {
            string sql = "delete from " + tableName + " where 1=1 " + where + " Id IN (" + id + ");";
            return sql;
        }
    }
}
