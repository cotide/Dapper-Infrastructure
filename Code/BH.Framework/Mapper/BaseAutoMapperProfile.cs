using AutoMapper;

namespace BH.Framework.Mapper
{
    /// <summary>
    ///  AutoMapper Profile基类
    /// </summary>
    public  class BaseAutoMapperProfile<TPut, TOut> : Profile
    {
        /// <summary>
        /// 配置初始化
        /// </summary>
        protected override void Configure()
        {
            CreateMap<TPut, TOut>(); 
        }
    }
}
