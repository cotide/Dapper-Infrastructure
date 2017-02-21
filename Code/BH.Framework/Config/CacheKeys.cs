namespace BH.Framework.Config
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public sealed class CacheKeys
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
        public static readonly string EcloginPasswordSecret = "CotideShop";

    }
}
