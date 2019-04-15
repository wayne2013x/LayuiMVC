using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Common
{
    public class Utility
    {
        public static void Alert(string message)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\");</script>");
            HttpContext.Current.Response.End();
        }

        public static void Alert(Page page, string message)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">alert(\"" + message + "\");</script>");
        }

        public static void Alert(string message, string url, bool bTop)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            if (bTop)
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\"); window.top.location=\"" + url + "\";</script>");
            }
            else
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\"); this.window.location=\"" + url + "\";</script>");
            }
            HttpContext.Current.Response.End();
        }

        public static void Alert(string message, string url, int iTime)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\"); window.setTimeout(\"this.window.location='" + url + "';\", " + iTime + ");</script>");
            HttpContext.Current.Response.End();
        }

        public static void Alert(Page page, string message, string url, bool bTop)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            if (bTop)
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">alert('" + message + "'); window.top.location='" + url + "';</script>");
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">alert(\"'" + message + "'); this.window.location='" + url + "';</script>");
            }
        }

        public static void Alert(Page page, string message, string url, int iTime)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">alert(\"" + message + "\"); window.setTimeout(\"this.window.location='" + url + "';\", " + iTime + ");</script>");
        }

        public static void AlertAsk(string message, string okurl)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">if(confirm(\"" + message + "\")){window.location=\"" + okurl + "\";}else{window.history.back();}</script>");
            HttpContext.Current.Response.End();
        }

        public static void AlertAsk(string message, string okurl, string cancelurl)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">if(confirm(\"" + message + "\")){window.location=\"" + okurl + "\";}else{window.location=\"" + cancelurl + "\";}</script>");
            HttpContext.Current.Response.End();
        }

        public static void AlertAsk(Page page, string message, string okurl)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">if(confirm(\"" + message + "\")){window.location=\"" + okurl + "\";}else{window.history.back();}</script>");
        }

        public static void AlertAsk(Page page, string message, string okurl, string cancelurl)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">if(confirm(\"" + message + "\")){window.location=\"" + okurl + "\";}else{window.location=\"" + cancelurl + "\";}</script>");
        }

        public static void AlertBack(string message)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\"); window.history.back();</script>");
            HttpContext.Current.Response.End();
        }

        public static void AlertBack(Page page, string message)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">alert(\"" + message + "\"); window.history.back();</script>");
        }

        public static void AlertClose(string message)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\"); self.close();</script>");
            HttpContext.Current.Response.End();
        }

        public static void AlertClose(Page page, string message)
        {
            if (message == null)
            {
                message = "";
            }
            message = message.Replace("\"", "\\\"");
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">alert(\"" + message + "\"); self.close();<</script>");
        }

        public static bool CheckMaxLen(string str, int maxLength)
        {
            string input = str;
            return (Regex.Replace(input, @"[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= maxLength);
        }

        public static string DecodeText(string src)
        {
            return OutputEncode(src, 0);
        }

        public static string DecodeTitleText(string src)
        {
            return OutputEncode(src, 1);
        }

        public static string DecodeUrlText(string src)
        {
            return OutputEncode(src, 2);
        }

        public static string DeUnicode(string srcText)
        {
            return HttpUtility.UrlDecode(srcText);
        }

        public static string DobuleNum(int iNum)
        {
            if (iNum < 10)
            {
                return ("0" + iNum);
            }
            return iNum.ToString();
        }

        public static string Drop(string src, string pattern)
        {
            return Replace(src, pattern, "");
        }

        public static string Drop(string src, string pattern, RegexOptions options)
        {
            return Replace(src, pattern, "", options);
        }

        public static string DropHtml(string src)
        {
            if (string.IsNullOrEmpty(src))
            {
                return "";
            }
            return DropIgnoreCase(DropScript(src), "<[^>]*>");
        }

        public static string DropHtmlTag(string src, string tagName)
        {
            return DropIgnoreCase(src, "<[/]{0,1}" + tagName + "[^>]*>");
        }

        public static string DropIgnoreCase(string src, string pattern)
        {
            return ReplaceIgnoreCase(src, pattern, "");
        }

        public static string DropScript(string src)
        {
            return DropIgnoreCase(src, @"<\s*script[^>]*>([\s\S](?!<script))*?</\s*script\s*>");
        }

        public static string EncodeText(string src)
        {
            return InputEncode(src, 0);
        }

        public static string EncodeTitleText(string src)
        {
            return InputEncode(src, 1);
        }

        public static string EncodeUrlText(string src)
        {
            return InputEncode(src, 2);
        }

        public static string EnUnicode(string srcText)
        {
            return HttpUtility.UrlEncode(srcText);
        }

        public static string EscapeHtml(string src)
        {
            if (src == null)
            {
                return null;
            }
            string str = src;
            return Replace(Replace(str, "<", "&lt;"), ">", "&gt;");
        }

        public static string FormatFileSize(long fSize)
        {
            if (fSize == 0L)
            {
                return "0";
            }
            string[] strArray = { "B", "KB", "MB", "GB", "TB", "PB" };
            int index = (int)Math.Floor(Math.Log(fSize, 1024.0));
            return (Math.Round(fSize / Math.Pow(1024.0, index), 2) + strArray[index]);
        }

        public static string GetAppSettingsKeyValue(string keyName)
        {
            return DataConvert.ToString(ConfigurationManager.AppSettings[keyName]);
        }

        public static string GetCatchMsg(string sErrMsg, string sDefault)
        {
            if (sDefault == "")
            {
                sDefault = "数据操作失败！";
            }
            if (sErrMsg.IndexOf("无法访问服务器") > 0)
            {
                return "数据库连接失败！";
            }
            if (sErrMsg.IndexOf("登录失败") > 0)
            {
                return "数据库连接失败！请检查数据库连接字符串是否正确。";
            }
            return sDefault;
        }

        public static string GetConnectionString(string name)
        {
            if (ConfigurationManager.ConnectionStrings[name] != null)
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            return "";
        }

        public static string GetCurrentUrl(bool withParam)
        {
            if (withParam)
            {
                return HttpContext.Current.Request.Url.AbsoluteUri;
            }
            return HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];
        }

        public static string GetCurrentUrl(string NPList, bool FullUrl)
        {
            HttpRequest request = HttpContext.Current.Request;
            NPList = NPList.ToLower();
            string str = "";
            for (int i = 0; i < request.QueryString.Count; i++)
            {
                if (NPList.IndexOf("," + request.QueryString.Keys[i].ToLower() + ",") < 0)
                {
                    string str2 = str;
                    str = str2 + "&" + request.QueryString.Keys[i] + "=" + request.QueryString[i];
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                if (FullUrl)
                {
                    return GetCurrentUrl(false);
                }
                return request.Url.AbsolutePath;
            }
            if (FullUrl)
            {
                return (GetCurrentUrl(false) + "?" + str.Substring(1));
            }
            return (request.Url.AbsolutePath + "?" + str.Substring(1));
        }

        public static string GetCurrentUrl(string NPList, bool FullUrl, bool ForLink)
        {
            if (ForLink)
            {
                return ToLinkUrl(GetCurrentUrl(NPList, FullUrl));
            }
            return GetCurrentUrl(NPList, FullUrl);
        }

        public static string GetIndustryName(string IndustryName)
        {
            if (IndustryName.IndexOf('(') != -1)
            {
                IndustryName = IndustryName.Substring(0, IndustryName.IndexOf('('));
            }
            return IndustryName;
        }

        public static string GetStr(string inputStr, int getLengt, string endStr)
        {
            if (!string.IsNullOrEmpty(inputStr))
            {
                string input = inputStr.Substring(0, (inputStr.Length < getLengt) ? inputStr.Length : getLengt);
                if (Regex.Replace(input, "[一-龥]", "zz", RegexOptions.IgnoreCase).Length <= getLengt)
                {
                    return input;
                }
                for (int i = input.Length; i >= 0; i--)
                {
                    input = input.Substring(0, i);
                    if (Regex.Replace(input, "[一-龥]", "zz", RegexOptions.IgnoreCase).Length <= (getLengt - endStr.Length))
                    {
                        return (input + endStr);
                    }
                }
            }
            return endStr;
        }

        public static string GetUrlParam(string NPList)
        {
            HttpRequest request = HttpContext.Current.Request;
            NPList = NPList.ToLower();
            string str = "";
            for (int i = 0; i < request.QueryString.Count; i++)
            {
                if (NPList.IndexOf("," + request.QueryString.Keys[i].ToLower() + ",") < 0)
                {
                    string str2 = str;
                    str = str2 + "&" + request.QueryString.Keys[i] + "=" + request.QueryString[i];
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(1);
            }
            return str;
        }

        public static void GoBack()
        {
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.history.back();</script>");
            HttpContext.Current.Response.End();
        }

        public static string HighLightString(string str, string keywords)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(keywords))
            {
                return DataConvert.ToString(str);
            }
            string str2 = str;
            string[] strArray = keywords.ToUpper().Split(',');
            for (int i = 0; i < strArray.Length; i++)
            {
                str2 = str2.Replace(strArray[i], @"#\#" + strArray[i] + "$/$");
            }
            return str2.Replace(@"#\#", "<font color=\"#ff0000\">").Replace("$/$", "</font>");
        }

        public static string HtmlDecode(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src);
            builder.Replace("&amp;", "&");
            builder.Replace("&lt;", "<");
            builder.Replace("&gt;", ">");
            builder.Replace("&#40;", "(");
            builder.Replace("&#41;", ")");
            builder.Replace("&#45;&#45;", "--");
            builder.Replace("&#39;", "'");
            builder.Replace("&quot;", "\"");
            builder.Replace("&nbsp;", " ");
            builder.Replace("<br/>", "\n");
            return builder.ToString();
        }

        public static string HtmlDecode2(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src);
            builder.Replace("&amp;", "&");
            builder.Replace("&lt;", "＜");
            builder.Replace("&gt;", "＞");
            builder.Replace("&#40;", "(");
            builder.Replace("&#41;", ")");
            builder.Replace("&#45;&#45;", "--");
            builder.Replace("&#39;", "'");
            builder.Replace("&quot;", "\"");
            builder.Replace("&nbsp;", " ");
            builder.Replace("<br/>", "\n");
            return builder.ToString();
        }

        public static string HtmlEncode(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src);
            builder.Replace(Convert.ToChar(0).ToString(), "");
            builder.Replace("&", "&amp;");
            builder.Replace("<", "&lt;");
            builder.Replace(">", "&gt;");
            builder.Replace("(", "&#40;");
            builder.Replace(")", "&#41;");
            builder.Replace("--", "&#45;&#45;");
            builder.Replace("'", "&#39;");
            builder.Replace("\"", "&quot;");
            builder.Replace("\r\n", "<br/>");
            builder.Replace("\n", "<br/>");
            builder.Replace("\t", "    ");
            builder.Replace(" ", "&nbsp;");
            return builder.ToString();
        }

        public static string HtmlEncodeWithOutNbsp(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src);
            builder.Replace(Convert.ToChar(0).ToString(), "");
            builder.Replace("&", "&amp;");
            builder.Replace("<", "&lt;");
            builder.Replace(">", "&gt;");
            builder.Replace("(", "&#40;");
            builder.Replace(")", "&#41;");
            builder.Replace("--", "&#45;&#45;");
            builder.Replace("'", "&#39;");
            builder.Replace("\"", "&quot;");
            builder.Replace("script", "");
            builder.Replace("\r\n", "");
            builder.Replace("\n", "");
            builder.Replace("\t", "");
            return builder.ToString();
        }

        public static bool IDsIsNumber(string IDs)
        {
            string[] strArray = IDs.Split(',');
            List<int> list = new List<int>();
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    list.Add(int.Parse(strArray[i]));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string InputEncode(string src, int type)
        {
            string str2 = src.ToLower();
            string[] strArray = "onabort,onblur,onchange,onclick,ondblclick,onerror,onfocus,onkeydown,onkeypress,onkeyup,onload,onmousedown,onmousemove,onmouseout,onmouseover,onmouseup,onreset,onresize,onselect,onsubmit,onunload".Split(',');
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str2.IndexOf(strArray[i]) >= 0)
                {
                    src = ReplaceIgnoreCase(src, strArray[i], "");
                    str2 = src.ToLower();
                }
            }
            switch (type)
            {
                case 1:
                    return HtmlEncodeWithOutNbsp(src);

                case 2:
                    return UrlTextEncode(src);
            }
            return HtmlEncode(src);
        }

        public static bool IsEmail(string email)
        {
            return RegTest(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        public static bool IsFromMobile()
        {
            return RegExec(DataConvert.ToString(HttpContext.Current.Request.UserAgent).Trim().ToLower(), "(iphone|ipod|android|ios)");
        }

        public static bool IsIPAddress(string ip)
        {
            return (((!string.IsNullOrEmpty(ip) && (ip.Length >= 7)) && (ip.Length <= 15)) && Regex.IsMatch(ip, @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$", RegexOptions.IgnoreCase));
        }

        public static bool IsMobile(string mobile)
        {
            return IsPhone(mobile, 2);
        }

        public static bool IsNumeric(string str)
        {
            return RegTestIgnoreCase(str, @"^[+-]?\d*[.]?\d*$");
        }

        public static bool IsPhone(string phone, int checkType)
        {
            string pattern = @"(^((\(\d{3,4}\))|((\([+]{0,1}\d{2,3}\))?\d{3,4}(\-|[ ]?)))?(\d{7,8}(\-\d{1,4})?)$)";
            string str2 = @"(^((0{0,1})|(\([+]{0,1}\d{2,3}\))|([+]{0,1}\d{2,3}(\-|[ ]?)))?((13\d{9})|(14\d{9})|(15\d{9})|(17\d{9})|(18\d{9}))$)";
            switch (checkType)
            {
                case 1:
                    return RegTestIgnoreCase(phone, pattern);

                case 2:
                    return RegTestIgnoreCase(phone, str2);
            }
            return RegTestIgnoreCase(phone, pattern + "|" + str2);
        }

        public static bool IsUrl(string url)
        {
            return RegTest(url, @"^http(s)?://([\w-]+\.)+[\w-]+[\w\?\./%&=-]*$");
        }

        public static string Left(string sSource, int iLength)
        {
            if (string.IsNullOrEmpty(sSource))
            {
                return "";
            }
            return sSource.Substring(0, (iLength > sSource.Length) ? sSource.Length : iLength);
        }

        public static List<string> Matches(string src, string pattern)
        {
            List<string> list = new List<string>();
            MatchCollection matchs = Regex.Matches(src, pattern);
            for (int i = 0; i < matchs.Count; i++)
            {
                list.Add(matchs[i].Value);
            }
            return list;
        }

        public static List<string[]> MatchesGroup(string src, string pattern)
        {
            List<string[]> list = new List<string[]>();
            List<string> list2 = new List<string>();
            foreach (Match match in Regex.Matches(src, pattern))
            {
                list2.Clear();
                list2.Add(match.Value);
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    list2.Add(match.Groups[i].Value);
                }
                list.Add(list2.ToArray());
            }
            return list;
        }

        public static List<string[]> MatchesGroupIgnoreCase(string src, string pattern)
        {
            List<string[]> list = new List<string[]>();
            List<string> list2 = new List<string>();
            foreach (Match match in Regex.Matches(src, pattern, RegexOptions.IgnoreCase))
            {
                list2.Clear();
                list2.Add(match.Value);
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    list2.Add(match.Groups[i].Value);
                }
                list.Add(list2.ToArray());
            }
            return list;
        }

        public static List<string> MatchesIgnoreCase(string src, string pattern)
        {
            List<string> list = new List<string>();
            MatchCollection matchs = Regex.Matches(src, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                list.Add(matchs[i].Value);
            }
            return list;
        }

        public static string Md5(string src)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(src, "MD5");
        }

        public static string Md516(string src)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(src, "MD5").Substring(8, 0x10);
        }

        public static string OutputEncode(string src, int type)
        {
            switch (type)
            {
                case 1:
                    return HtmlDecode(src);

                case 2:
                    return src;
            }
            return HtmlDecode(src);
        }

        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode, out int httpStatusCode)
        {
            string str = string.Empty;
            httpStatusCode = 200;
            try
            {
                byte[] bytes = dataEncode.GetBytes(paramData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                httpStatusCode = (int)response.StatusCode;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
                requestStream.Close();
            }
            catch (WebException exception)
            {
                HttpWebResponse response2 = exception.Response as HttpWebResponse;
                if (response2 != null)
                {
                    httpStatusCode = (int)response2.StatusCode;
                }
            }
            return str;
        }

        public static void Redirect(string url, bool bTop)
        {
            if ((url == null) || (url.Length < 1))
            {
                Alert("重定向地址不能为空");
            }
            else if (bTop)
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.top.location=\"" + url + "\";</script>");
            }
            else
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">this.window.location=\"" + url + "\";</script>");
            }
            HttpContext.Current.Response.End();
        }

        public static void Redirect(string url, bool bTop, int iTime)
        {
            if ((url == null) || (url.Length < 1))
            {
                Alert("重定向地址不能为空");
            }
            else if (bTop)
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.setTimeout(\"window.top.location='" + url + "';\", " + iTime + ");</script>");
            }
            else
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.setTimeout(\"this.window.location='" + url + "';\", " + iTime + ");</script>");
            }
            HttpContext.Current.Response.End();
        }

        public static void Redirect(Page page, string url, bool bTop)
        {
            if ((url == null) || (url.Length < 1))
            {
                Alert("重定向地址不能为空");
            }
            else if (bTop)
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">window.top.location=\"" + url + "\";</script>");
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">this.window.location=\"" + url + "\";</script>");
            }
        }

        public static void Redirect(Page page, string url, bool bTop, int iTime)
        {
            if ((url == null) || (url.Length < 1))
            {
                Alert("重定向地址不能为空");
            }
            else if (bTop)
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">window.setTimeout(\"window.top.location='" + url + "';\", " + iTime + ");</script>");
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">window.setTimeout(\"this.window.location='" + url + "';\", " + iTime + ");</script>");
            }
        }

        public static void RedirectMobile(string mobileUrl)
        {
            if (RegExec(DataConvert.ToString(HttpContext.Current.Request.UserAgent).Trim().ToLower(), "(iphone|ipod|android|ios)") && (CookiesHelper.GetCookieValue("nomobile") == ""))
            {
                HttpContext.Current.Response.Redirect(mobileUrl, true);
            }
        }

        public static bool RegExec(string src, string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.None);
            return regex.Match(src).Success;
        }

        public static bool RegExecIgnoreCase(string src, string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.Match(src).Success;
        }

        public static bool RegTest(string src, string pattern)
        {
            return Regex.IsMatch(src, pattern, RegexOptions.None);
        }

        public static bool RegTestIgnoreCase(string src, string pattern)
        {
            return Regex.IsMatch(src, pattern, RegexOptions.IgnoreCase);
        }

        public static string Replace(string src, string pattern, string replacement)
        {
            return Replace(src, pattern, replacement, RegexOptions.None);
        }

        public static string Replace(string src, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(src, pattern, replacement, options | RegexOptions.Compiled);
        }

        public static string ReplaceIgnoreCase(string src, string pattern, string replacement)
        {
            return Replace(src, pattern, replacement, RegexOptions.IgnoreCase);
        }

        public static string Right(string sSource, int iLength)
        {
            if (string.IsNullOrEmpty(sSource))
            {
                return "";
            }
            return sSource.Substring((iLength > sSource.Length) ? 0 : (sSource.Length - iLength));
        }

        public static string[] Split(string src, string pattern)
        {
            return Regex.Split(src, pattern);
        }

        public static string SysTrim(string src)
        {
            char[] trimChars = { ' ', '\r', '\n', '\t' };
            return src.Trim(trimChars);
        }

        public static string TextFromHtml(string src)
        {
            return DropIgnoreCase(DropScript(src), "</?(?!br|/?p|img)[^>]*>");
        }

        public static string ToLinkUrl(string Url)
        {
            return Url.Replace("&", "&amp;");
        }

        public static string ToSQL(string src)
        {
            if (src == null)
            {
                return null;
            }
            return src.Replace("'", "''").Replace("%", @"\%");
        }

        public static string Trim(string src)
        {
            if (string.IsNullOrEmpty(src))
            {
                return "";
            }
            return DropIgnoreCase(src, @"^[\s\n\r\t]*|[\s\n\r\t]*$");
        }

        public static string UrlTextEncode(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src.ToLower());
            builder.Replace(Convert.ToChar(0).ToString(), "");
            builder.Replace("'", "");
            builder.Replace("%27", "");
            builder.Replace("\"", "");
            builder.Replace("%22", "");
            builder.Replace(" ", "");
            builder.Replace("<", "");
            builder.Replace("%3c", "");
            builder.Replace(">", "");
            builder.Replace("%3e", "");
            builder.Replace("&#", "");
            builder.Replace("script", "");
            builder.Replace("*", "");
            builder.Replace("\r\n", "");
            builder.Replace("\n", "");
            builder.Replace("\t", "");
            return builder.ToString();
        }

        public static string WapDecode(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src);
            builder.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "　　");
            builder.Replace("&nbsp;", " ");
            builder.Replace("&amp;", "&");
            builder.Replace("<br />", "\r\n");
            builder.Replace("<br/>", "\r\n");
            return builder.ToString();
        }

        public static string WapEncode(string src)
        {
            if ((src == null) || src.Equals(""))
            {
                return src;
            }
            StringBuilder builder = new StringBuilder(src);
            builder.Replace(Convert.ToChar(0).ToString(), "");
            builder.Replace("'", "‘");
            builder.Replace("\"", "“");
            builder.Replace(",", "，");
            builder.Replace(";", "；");
            builder.Replace("(", "（");
            builder.Replace(")", "）");
            builder.Replace("<", "＜");
            builder.Replace(">", "＞");
            builder.Replace("--", "——");
            builder.Replace("$", "＄");
            builder.Replace("?", "？");
            builder.Replace("&", "&amp;");
            builder.Replace("    ", "　　");
            builder.Replace(" ", "&nbsp;");
            builder.Replace("\r", "");
            builder.Replace("\n\n", "<br/>");
            builder.Replace("\n", "<br/>");
            return builder.ToString();
        }

        public static void WriteJs(string message, bool endResponse)
        {
            if (!string.IsNullOrEmpty(message))
            {
                message = message.Replace("\"", "\\\"");
                HttpContext.Current.Response.Write("<script type=\"text/javascript\">" + message + "</script>");
                if (endResponse)
                {
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static void WriteJs(Page page, string js)
        {
            if (!string.IsNullOrEmpty(js))
            {
                js = js.Replace("\"", "\\\"");
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type=\"text/javascript\">" + js + "</script>");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="state">没有权限返回:nopermission，正确则不输出。没有登录返回nologin，正常消息按true和false返回消息</param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public static void WriteString(string state, string msg)
        {
            WriteString(state, msg, "");
        }
        /// <summary>
        /// 权限放回的消息
        /// cza
        /// 2015年12月1日 16:05:07
        /// </summary>
        /// <param name="context"></param>
        /// <param name="state">没有权限返回:nopermission，没有登录返回nologin，正常消息按true和false返回消息</param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public static void WriteString(string state, string msg, string data)
        {
            HttpContext.Current.Response.Clear();
            if (string.IsNullOrEmpty(data))
            {
                HttpContext.Current.Response.Write("{\"state\":\"" + state + "\",\"msg\":\"" + msg + "\",\"data\":\"\"}");
            }
            else
            {
                HttpContext.Current.Response.Write("{\"state\":\"" + state + "\",\"msg\":\"" + msg + "\",\"data\":" + data + "}");
            }
            //context.Response.End();
        }

        public static void Write(HttpContext context, bool state, string msg, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                context.Response.Write("{\"state\":\"" + state.ToString().ToLower() + "\",\"msg\":\"" + msg + "\",\"data\":\"\"}");
            }
            else
            {
                context.Response.Write("{\"state\":\"" + state.ToString().ToLower() + "\",\"msg\":\"" + msg + "\",\"data\":" + data + "}");
            }
            //context.Response.End();
        }
    }
}
