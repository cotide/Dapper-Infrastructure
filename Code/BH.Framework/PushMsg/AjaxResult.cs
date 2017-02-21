using System;

namespace BH.Framework.PushMsg
{
    /// <summary>
    /// Ajax返回结果对象
    /// </summary>
    public class AjaxResult<T> 
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AjaxResult()
        {
            IsSuccess = true;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public String ErrCode { get; set; }
    }
}
