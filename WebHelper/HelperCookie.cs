using System;
using System.Web;
using System.Collections;

namespace WebHelper
{
    /// <summary>
    /// Cookie
    /// 2015-10-24 14:09 hemajun
    /// </summary>
    public class HelperCookie
    {
        private const String Cookies_Name = "UserInfo";
        /// <summary>
        /// 添加用户相关信息到Cookie
        /// </summary>
        /// <param name="ht">哈希表ht，ht的key是cookie名，ht的value是cookie的值</param>
        public static void SetCookies(Hashtable ht)
        {
            HttpCookie cookie = new HttpCookie(Cookies_Name, "hemajun");
            foreach (DictionaryEntry de in ht)
            {
                cookie.Values.Add(de.Key.ToString(), HttpUtility.UrlEncode(de.Value.ToString()));   //从安全上考虑，Cookie需要编码传送
            }
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="sCookieName">要删除的cookie的名字</param>
        public static void DelCookies(string sCookieName)
        {
            if (HttpContext.Current.Response.Cookies[sCookieName] != null)
            {
                HttpCookie cookie = new HttpCookie(sCookieName) { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 删除所有cookie
        /// </summary>
        public static void DelCookies()
        {
            string[] sCookieNames = HttpContext.Current.Request.Cookies.AllKeys;
            foreach (string sCookieName in sCookieNames)
            {
                DelCookies(sCookieName);
            }
        }

        /// <summary>
        /// 获取Cookie里的数据
        /// </summary>
        public static Object GetCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[Cookies_Name] == null || HttpContext.Current.Request.Cookies[Cookies_Name].Values[key] == null)
                return null;
            else
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[Cookies_Name].Values[key]);
        }
    }
}
