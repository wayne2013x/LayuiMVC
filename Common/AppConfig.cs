using System;

namespace Common
{
    public class AppConfig
    {

        /// <summary>
        /// 日志 log4net Repository 名称
        /// </summary>
        public static string Log4NETRepositoryName = "NETRepository";

        /// <summary>
        /// 超级管理员的 角色ID
        /// </summary>
        public static string Admin_RoleID { get { return "18fa4771-6f58-46a3-80d2-6f0f5e9972e3"; } }

        /// <summary>
        /// 错误页面地址
        /// </summary>
        public static string ErrorPageUrl = "~/Views/Error/Index.cshtml";

        /// <summary>
        /// 登录地址
        /// </summary>
        public static string LoginPageUrl = "/Login/Index/";

        /// <summary>
        /// 首页地址
        /// </summary>
        public static string HomePageUrl = "/Admin/Home/";

    }
}
