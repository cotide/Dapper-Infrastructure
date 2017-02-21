namespace BH.Framework.Utility
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public sealed class GlobalKeys
    {
        /// <summary>
        /// 当前登录用户缓存Key
        /// </summary>
        public static readonly string UserKey = "User_";

        /// <summary>
        /// 当前访问用户缓存Key
        /// </summary>
        public static readonly string HistoryUserKey = "HistoryUser_";

        /// <summary>
        /// 明文密码公钥
        /// </summary>
        public static readonly string ECLOGIN_PASSWORD_SECRET = "CotideShop";

    }
}
