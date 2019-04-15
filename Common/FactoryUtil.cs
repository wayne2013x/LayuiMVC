using System;
using System.Configuration;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// 实例化DAL和BLL的工厂类
    /// </summary>
    public class FactoryUtil
    {
        /// <summary>
        /// 实例化DAL对象
        /// </summary>
        /// <param name="className">DAL配置节点</param>
        /// <returns>DAL实例对象</returns>
        public static object CreateDal(string className)
        {
            try
            {
                className = ConfigurationManager.AppSettings[className].Split(',')[1];
                string assemblyName = ConfigurationManager.AppSettings["Namespace"];
                return Assembly.GetCallingAssembly().CreateInstance(assemblyName + "." + className);
            }
            catch (Exception)
            {
                throw new Exception("我猜你是写错了DAL类名");
            }
        }

        /// <summary>
        /// 实例化BLL对象
        /// </summary>
        /// <param name="className">BLL配置节点</param>
        /// <returns>BLL实例对象</returns>
        public static object CreateBll(string className)
        {
            try
            {
                className = ConfigurationManager.AppSettings[className].Split(',')[0];
                string assemblyName = ConfigurationManager.AppSettings["Namespace"];
                return Assembly.GetCallingAssembly().CreateInstance(assemblyName + "." + className);
            }
            catch (Exception)
            {
                throw new Exception("我猜你是写错了BLL类名");
            }
        }
    }
}
