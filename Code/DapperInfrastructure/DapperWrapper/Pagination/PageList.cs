using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperInfrastructure.DapperWrapper.Pagination
{
    /// <summary>
    /// 分页对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T>
    {
        /// <summary>
        /// 开始页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalPage"></param>
        /// <param name="totalCount"></param>
        public PageList(IEnumerable<T> list, int pageIndex, int pageSize, int totalPage, int totalCount)
        {
            Items = list;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = totalPage;
            TotalCount = totalCount;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalPage"></param>
        /// <param name="totalCount"></param>
        public PageList(int pageIndex, int pageSize, int totalPage, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = totalPage;
            TotalCount = totalCount;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PageList(IEnumerable<T> list, int pageIndex, int pageSize)
        {
            Items = list;
            PageIndex = pageIndex;
            PageSize = pageSize;

            TotalCount = list.Count();
            TotalPage = (int)Math.Ceiling((double)TotalCount / (double)PageSize);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageList()
        {

        }
    }
}
