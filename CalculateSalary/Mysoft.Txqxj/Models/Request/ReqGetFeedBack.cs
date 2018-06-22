using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.Request
{
    public class ReqGetFeedBack
    {
        //public int PageNum { get; set; } = 1;

        //public int PageCount { get; set; } = 10; 

        /// <summary>
        /// 期望获得什么数据
        /// 0-未处理
        /// 1-已处理
        /// </summary>
        public int Type { get; set; } = 0;
    }
}
