using System.Text;

namespace Common
{
    /// <summary>
    /// 客户端json数据扩展类
    /// </summary>
    public class JsonHelperEx
    {
        public static string GetAjaxMsg(bool flag, string msg = "", string data = "{}")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"result\":" + flag.ToString().ToLower() + ",");
            if (msg == "")
            {
                msg = flag ? "成功" : "失败";
            }
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"data\":" + data);
            sb.Append("}");
            return sb.ToString();
        }

        public static string GetLayuiTable(string json, int count, string msg = "")
        {
            if (string.IsNullOrEmpty(json))
            {
                return GetAjaxMsg(false, "无数据");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"code\":0,");
                sb.Append("\"msg\":\"" + msg + "\",");
                sb.Append("\"count\":" + count + ",");
                sb.Append("\"data\":" + json);
                sb.Append("}");
                return sb.ToString();
            }
            
        }

        public static string GetLayuiUpload(string json, int code = 0, string msg = "")
        {
            if (!string.IsNullOrEmpty(json))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"code\":" + code + ",");
                sb.Append("\"msg\":\"" + msg + "\",");
                sb.Append("\"data\":" + json);
                sb.Append("}");
                return sb.ToString();
            }
            return json;
        }
    }
}