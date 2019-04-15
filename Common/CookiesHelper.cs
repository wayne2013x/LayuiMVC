using System;
using System.Web;

namespace Common
{
    public class CookiesHelper
    {
        public static void AddCookie(string cookieName, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Expires = expires
            };
            AddCookie(cookie, null);
        }

        public static void AddCookie(string key, string value)
        {
            AddCookie(new HttpCookie(key, value), null);
        }

        public static void AddCookie(HttpCookie cookie, string Domain)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                cookie.HttpOnly = true;
                cookie.Path = "/";
                if (!string.IsNullOrEmpty(Domain))
                {
                    cookie.Domain = Domain;
                }
                response.AppendCookie(cookie);
            }
        }

        public static void AddCookie(string cookieName, DateTime expires, string Domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Expires = expires
            };
            AddCookie(cookie, Domain);
        }

        public static void AddCookie(string key, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(key, value)
            {
                Expires = expires
            };
            AddCookie(cookie, null);
        }

        public static void AddCookie(string cookieName, string key, string value)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Add(key, value);
            AddCookie(cookie, null);
        }

        public static void AddCookie(string key, string value, bool withDomain, string Domain)
        {
            if (withDomain)
            {
                AddCookie(new HttpCookie(key, value), Domain);
            }
            else
            {
                AddCookie(new HttpCookie(key, value), null);
            }
        }

        public static void AddCookie(string key, string value, DateTime expires, string Domain)
        {
            HttpCookie cookie = new HttpCookie(key, value)
            {
                Expires = expires
            };
            AddCookie(cookie, Domain);
        }

        public static void AddCookie(string cookieName, string key, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Expires = expires
            };
            cookie.Values.Add(key, value);
            AddCookie(cookie, null);
        }

        public static void AddCookie(string cookieName, string key, string value, string Domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Add(key, value);
            AddCookie(cookie, Domain);
        }

        public static void AddCookie(string cookieName, string key, string value, DateTime expires, string Domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Expires = expires
            };
            cookie.Values.Add(key, value);
            AddCookie(cookie, Domain);
        }

        public static void AddDomainCookie(string key, string value, string Domain)
        {
            AddCookie(new HttpCookie(key, value), Domain);
        }

        public static HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
            {
                if (request.Cookies[cookieName] != null)
                {
                    return request.Cookies[cookieName];
                }
                if (request.Cookies[", " + cookieName] != null)
                {
                    return request.Cookies[", " + cookieName];
                }
            }
            return null;
        }

        public static string GetCookieValue(string cookieName)
        {
            return GetCookieValue(cookieName, null);
        }

        public static string GetCookieValue(string cookieName, string key)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request == null)
            {
                return "";
            }
            if (request.Cookies[cookieName] != null)
            {
                if (!string.IsNullOrEmpty(key) && request.Cookies[cookieName].HasKeys)
                {
                    return request.Cookies[cookieName].Values[key];
                }
                return request.Cookies[cookieName].Value;
            }
            string str = ", " + cookieName;
            if (request.Cookies[str] == null)
            {
                return "";
            }
            if (!string.IsNullOrEmpty(key) && request.Cookies[str].HasKeys)
            {
                return request.Cookies[str].Values[key];
            }
            return request.Cookies[str].Value;
        }

        public static string GetCookieValue(HttpCookie cookie, string key)
        {
            if (cookie == null)
            {
                return "";
            }
            if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
            {
                return cookie.Values[key];
            }
            return cookie.Value;
        }

        public static void RemoveCookie(string cookieName)
        {
            RemoveCookie(cookieName, null);
        }

        public static void RemoveCookie(string cookieName, string key)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                    {
                        cookie.Values.Remove(key);
                    }
                    else
                    {
                        response.Cookies.Remove(cookieName);
                    }
                }
            }
        }

        public static void SetCookie(string cookieName, DateTime expires)
        {
            SetCookie(cookieName, null, null, expires, null);
        }

        public static void SetCookie(string key, string value)
        {
            SetCookie(key, null, value, null, null);
        }

        public static void SetCookie(string cookieName, DateTime expires, string Domain)
        {
            SetCookie(cookieName, null, null, expires, Domain);
        }

        public static void SetCookie(string key, string value, DateTime expires)
        {
            SetCookie(key, null, value, expires, null);
        }

        public static void SetCookie(string cookieName, string key, string value)
        {
            SetCookie(cookieName, key, value, null, null);
        }

        public static void SetCookie(string key, string value, bool withDomain, string Domain)
        {
            if (withDomain)
            {
                SetCookie(key, null, value, null, Domain);
            }
            else
            {
                SetCookie(key, null, value, null, null);
            }
        }

        public static void SetCookie(string key, string value, DateTime expires, string Domain)
        {
            SetCookie(key, null, value, expires, Domain);
        }

        public static void SetCookie(string cookieName, string key, string value, string Domain)
        {
            SetCookie(cookieName, key, value, null, Domain);
        }

        public static void SetCookie(string cookieName, string key, string value, DateTime? expires, string Domain)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Path = "/";
                    if (!string.IsNullOrEmpty(Domain))
                    {
                        cookie.Domain = Domain;
                    }
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                    {
                        cookie.Values.Set(key, value);
                    }
                    else if (!string.IsNullOrEmpty(value))
                    {
                        cookie.Value = value;
                    }
                    if (expires.HasValue)
                    {
                        cookie.Expires = expires.Value;
                    }
                    response.SetCookie(cookie);
                }
            }
        }
        /// <summary>
        /// 设置域的cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="Domain"></param>
        public static void SetDomainCookie(string key, string value, string Domain)
        {
            SetCookie(key, null, value, null, Domain);
        }
    }
}
