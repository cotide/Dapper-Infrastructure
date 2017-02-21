using System;

namespace BH.Framework.Exceptions
{
    /// <summary>
    /// 用户权限异常
    /// </summary>
    [Serializable]
    public class UserPowerException : Exception
    {
 
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public UserPowerException(string message)
            : base(message) { }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在BllException内部的异常实例</param>
        public UserPowerException(string message, Exception inner)
            : base(message, inner) { }
 
    }
}
