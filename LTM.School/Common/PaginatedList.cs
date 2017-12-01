using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LTM.School.Common
{
    public class PaginatedList<T>:List<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
         public int PageIndex { get; set; }
        //总页面数
        public int TotalPages { get; set; }


        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;

            TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);

            this.AddRange(items);
        }
        /// <summary>
        /// 判断是否有上一页
        /// </summary>
        public bool HasPreViousPage => (PageIndex > 1);
        /// <summary>
        /// 判断是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;
        /// <summary>
        /// 创建分页
        /// </summary>
        /// <param name="source">实体信息</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页数据条数</param>
        /// <returns></returns>
        public static async Task<PaginatedList<T>> CreatepagingAsync(IQueryable<T> source,int pageIndex,int pageSize)
        {
            //返回实体总条数
            var count = await source.CountAsync();

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

      var dtos=new PaginatedList<T>(items,count,pageIndex,pageSize);
            return dtos;
        }


    }
}