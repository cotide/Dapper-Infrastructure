using System;

namespace BH.Domain.Dtos.Base
{
    /// <summary>
    /// 基类Dto
    /// </summary>
    public class BaseDto<T>  
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseDto()
        {           
            CreateDateTime = DateTime.Now;
            LastUpdateDateTime = DateTime.Now;
        } 
  

        /// <summary>
        /// 主键
        /// </summary> 
        public virtual T Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>  
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>  
        public DateTime? LastUpdateDateTime { get; set; }

    }
}
