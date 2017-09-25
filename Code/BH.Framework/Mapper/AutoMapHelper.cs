 

namespace BH.Framework.Mapper
{
    /// <summary>
    /// 自动配置辅助类
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：Gorson Ng</para>
    ///     <para>CreatedTime：2013/8/9 17:33:19</para>
    /// </remarks>
    public static class AutoMapHelper
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TDestination"></typeparam> 
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return source == null 
                ? default(TDestination)
                : AutoMapper.Mapper.Map<TDestination>(source);
        }
         
    }
}
