using System;
using System.Collections.Generic;
using System.Configuration;
using SqlSugar;
using System.Linq.Expressions;

namespace Application
{
    public class BaseLogic<T> where T : class, new()
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public SqlSugarClient db;
        /// <summary>
        /// ORM实例
        /// </summary>
        public SimpleClient<T> clientdb { get { return new SimpleClient<T>(db); } }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseLogic()
        {
            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
            });
        }
        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>T</returns>
        public virtual T Get(dynamic id)
        {
            return clientdb.GetById(id);
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>List<T></returns>
        public virtual List<T> GetAllList()
        {
            return clientdb.GetList();
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="where">linq查询</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>List<T></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> where, int pageIndex, int pageSize)
        {
            return clientdb.GetPageList(where, new PageModel() { PageIndex = pageIndex, PageSize = pageSize });
        }
        /// <summary>
        /// 根据Id删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return clientdb.Delete(id);
        }
        /// <summary>
        /// 更新(为NULL的列不更新)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Update(T obj)
        {
            return clientdb.AsUpdateable(obj).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 新增obj
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns>obj.Id</returns>
        public virtual int Add(T obj)
        {
            var result = clientdb.InsertReturnIdentity(obj);
            return result.ObjToInt();
        }
    }
}
