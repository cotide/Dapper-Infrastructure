using System.Collections.Generic;

namespace DapperInfrastructure.DapperWrapper.Pagination
{
    /// <summary>
    /// 分页对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Items { get; set; }
         
    }
}
