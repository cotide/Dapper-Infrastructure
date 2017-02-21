#region using

using System.Collections.Generic;
using System.Linq;

#endregion

namespace DapperInfrastructure.Extensions.Collections
{
    /// <summary>
    /// 分页列表接口
    /// </summary>
    public interface IPagerList
    {
        int TotalCount { get; set; }
        int TotalPages { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool IsPreviousPage { get; }
        bool IsNextPage { get; }
    }

    /// <summary>
    /// 分页列表接口实现类
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class PagerList<T> : List<T>, IPagerList 
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="index">页码</param>
        /// <param name="pageSize">页面大小</param>
        public PagerList(IQueryable<T> source, int index, int pageSize)
        {
            int total = source.Count();
            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = index; 
            AddRange(source.Skip((index - 1) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="totalRecords">总记录数</param>
        /// <param name="index">页码</param>
        /// <param name="pageSize">页面大小</param>
        public PagerList(IEnumerable<T> source, int totalRecords, int index, int pageSize)
        {
            TotalCount = totalRecords;
            TotalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = index;
            AddRange(source.ToList());
        }
        #region IPagerList Members
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool IsPreviousPage
        {
            get { return ((PageIndex - 1) > 0); }
        }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool IsNextPage
        {
            get { return (PageIndex * PageSize) <= TotalCount; }
        }

        #endregion
    }

    /// <summary>
    /// 分页列表类扩展方法类
    /// </summary>
    public static class Pagination 
    {
        #region IQueryable<T> extensions
        /// <summary>
        /// 数据源转化为分页列表类
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="index">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>分页列表</returns>
        public static PagerList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)  
        {
            return new PagerList<T>(source, index, pageSize);
        }

        /// <summary>
        /// 数据源转化分页列表(页面大小默认为10)
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="index">页码</param>
        /// <returns>分页列表</returns>
        public static PagerList<T> ToPagedList<T>(this IQueryable<T> source, int index) 
        {
            return new PagerList<T>(source, index, 10);
        }

        #endregion

        #region IEnumerable<T> extensions
        /// <summary>
        /// 数据源转化为分页列表类
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>分页列表</returns>
        public static PagerList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize) 
        {
            return new PagerList<T>(source.AsQueryable(), pageIndex, pageSize);
        }

        /// <summary>
        /// 数据源转化为分页列表类
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>分页列表</returns>
        public static PagerList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount) 
        {
            return new PagerList<T>(source, totalCount, pageIndex, pageSize);
        }

        #endregion
    }
}