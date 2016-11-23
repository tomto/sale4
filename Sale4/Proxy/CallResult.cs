using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class CallResult
    {
        public string VMsg { get; set; }
        public string BMsg { get; set; }
        public bool State { get; set; }
        public int Tag { get; set; }
        public object ResultObj { get; set; }
    }


    public class PageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 返回分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T>
    {
        public PageResult()
        {
            List = new List<T>();
        }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 符合条件的记录总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize <= 0) return 0;
                return Count % PageSize == 0 ? Count / PageSize : Count / PageSize + 1;
            }
        }

        /// <summary>
        /// 当前页的记录
        /// </summary>
        public List<T> List { get; set; }

        public static implicit operator PageResult<T>(PageRequest pageRequest)
        {
            return new PageResult<T>()
            {
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize
            };
        }
    }
}
