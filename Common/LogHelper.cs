using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    using log4net;
    using log4net.Config;
    using log4net.Repository;
    using System.IO;
    public class LogHelper
    {

        public ILog _ILog;

        public static ILoggerRepository _ILoggerRepository { get; set; }

        public LogHelper()
        {
            if (_ILog == null) _ILog = LogManager.GetLogger(AppConfig.Log4NETRepositoryName, "\r\n-------------------------------------------------------------\r\n");
        }

        /// <summary>
        /// 初始化 Log4Net
        /// </summary>
        /// <returns></returns>
        public static void CreateRepository(string FilePath)
        {
            _ILoggerRepository = LogManager.CreateRepository(AppConfig.Log4NETRepositoryName);
            XmlConfigurator.Configure(_ILoggerRepository, new FileInfo(FilePath));
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="Text"></param>
        public void WriteLog(string Text)
        {
            _ILog.Info(Text);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="_Exception"></param>
        public void WriteLog(string Text, Exception _Exception)
        {
            if (_ILog.IsErrorEnabled)
            {
                _ILog.Error(Text, _Exception);
            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="_Exception"></param>
        /// <param name="UserHostAddress"></param>
        /// <param name="CallBack"></param>
        public void WriteLog(Exception _Exception, string UserHostAddress, Action<StringBuilder> CallBack = null)
        {
            var sb = new StringBuilder();
            var _Message = "异常信息: " + _Exception.Message;
            var _Source = "错误源:" + _Exception.Source;
            var _StackTrace = "堆栈信息:" + _Exception.StackTrace;

            sb.Append("\r\n" + UserHostAddress + "\r\n" + _Message + "\r\n" + _Source + "\r\n" + _StackTrace + "\r\n");

            if (CallBack != null)
            {
                CallBack(sb);
            }

            this.WriteLog(sb.ToString(), _Exception);
        }

    }
}
