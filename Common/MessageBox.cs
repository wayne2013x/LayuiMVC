using System;

namespace Common
{
    /// <summary>
    /// 配合Web页面使用纯ajax调用函数，后端弹出提示框的 类   AJAX 全局拦截消息
    /// </summary>
    [Serializable]
    public class MessageBox : Exception
    {
        /// <summary>
        /// 异常模型
        /// </summary>
        public static ErrorModel errorModel { set; get; }

        /// <summary>
        /// 初始化 WebException 类的新实例。
        /// </summary>
        //public MsgBox()
        //{
        //    errorModel = new ErrorModel();
        //}

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        public MessageBox(string Messager)
            : base(Messager)
        {
            errorModel = new ErrorModel(Messager);
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        public MessageBox(string Messager, EMsgStatus msgstatus)
            : base(Messager)
        {
            errorModel = new ErrorModel(Messager, msgstatus);
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用</param>
        public MessageBox(string Messager, Exception InnerException)
            : base(Messager, InnerException)
        {
            errorModel = new ErrorModel(Messager);
        }

    }
    
    public class ErrorModel
    {

        public ErrorModel()
        {

        }

        public ErrorModel(string msg)
        {
            this.EM(msg, EMsgStatus.信息提示10);
        }

        public ErrorModel(string msg, EMsgStatus msgStatus)
        {
            this.EM(msg, msgStatus);
        }

        public ErrorModel(Exception exception)
        {
            this.msg = exception.Message;
            this.status = (int)EMsgStatus.程序异常50;
            this.StackTrace = exception.StackTrace;
            this.TargetSite = exception.TargetSite != null ? exception.TargetSite.Name : null;
            this.Source = exception.Source;
            this.Data = exception.Data != null ? exception.Data.ToString() : "";
        }

        private void EM(string msg, EMsgStatus msgStatus)
        {
            this.msg = msg;
            this.status = (int)msgStatus;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { set; get; }

        /// <summary>
        /// 错误状态码
        /// </summary>
        public int status { set; get; }

        /// <summary>
        /// 堆栈
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 异常引发方法
        /// </summary>
        public string TargetSite { get; set; }

        /// <summary>
        /// 异常对象或应用程序
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 异常信息键值对集合
        /// </summary>
        public object Data { get; set; }

    }

    /// <summary>
    /// 消息状态
    /// </summary>
    public enum EMsgStatus
    {
        信息提示10 = 10,
        登录超时20 = 20,
        手动处理30 = 30,
        程序异常50 = 50
    }


}

