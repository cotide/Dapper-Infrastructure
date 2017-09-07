using System;
using DapperInfrastructure.Extensions.Collections;

namespace DapperInfrastructure.DapperWrapper.Pagination
{
	public static class PagedListExtension
	{ 

		public static PageList<T> GetPage<T>(this Page<T> list, int pageIndex, int pageSize)
		{
			var totalCount = list.TotalItems;
			double pageCount = Math.Ceiling((double)totalCount / (double)pageSize);
			var totalPage = (int)pageCount;

			if (pageIndex < 1)	//前端约定第一页从1开始
			{
				pageIndex = 1;
			}
			if (pageIndex >= totalPage)
			{
				pageIndex = totalPage;
			}

			//封装Page对象返回
			var page = new PageList<T>();
			page.PageIndex = pageIndex;
			page.PageSize = pageSize;
			page.TotalCount = totalCount;
			page.TotalPage = totalPage;
			page.Items = list.Items;

			return page;
		}

	} 
 
}
