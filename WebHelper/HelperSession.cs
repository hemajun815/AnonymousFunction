using System;

namespace WebHelper
{
    /// <summary>
    /// Session
    /// 2015-10-24 11:05 hemajun
    /// </summary>
    public class HelperSession
    {
        #region 自定义Session
        /// <summary>
        /// 获取Session
        /// </summary>
        public static Object GetSession(String key)
        {
            return System.Web.HttpContext.Current.Session[key];
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        public static void SetSession(String key, Object value)
        {
            System.Web.HttpContext.Current.Session[key] = value;
        }
        #endregion

        #region 常用Session
        /// <summary>
        /// 用户ID
        /// </summary>
        public static Object UserId
        {
            set { HelperSession.SetSession("HelperSession_UserID", value); }
            get { return HelperSession.GetSession("HelperSession_UserID"); }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public static Object UserName
        {
            set { HelperSession.SetSession("HelperSession_UserName", value); }
            get { return HelperSession.GetSession("HelperSession_UserName"); }
        }
        /// <summary>
        /// 验证码
        /// </summary>
        public static Object ValidateKey
        {
            set { HelperSession.SetSession("HelperSession_ValidateKey", value); }
            get { return HelperSession.GetSession("HelperSession_ValidateKey"); }
        }
        /// <summary>
        /// 用户级别
        /// </summary>
        public static Object UserLevel
        {
            set { HelperSession.SetSession("HelperSession_UserLevel", value); }
            get { return HelperSession.GetSession("HelperSession_UserLevel"); }
        }
        /// <summary>
        /// 用户登录时间
        /// </summary>
        public static DateTime UserLoginDatetime
        {
            set { HelperSession.SetSession("HelperSession_UserLoginDatetime", value); }
            get { return (DateTime)HelperSession.GetSession("HelperSession_UserLoginDatetime"); }
        }
        #endregion
    }
}
