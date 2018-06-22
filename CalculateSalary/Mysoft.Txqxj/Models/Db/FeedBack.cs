using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.Db
{
    public class FeedBack
    {
        /// <summary>
        /// 就把本地上传的ID作为此项的值
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; } 

        public string Address { get; set; }

        public string Climate { get; set; }

        public string Images { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// 0-未处理
        /// 1-已处理
        /// </summary>
        public int IsHandled { get; set; }

        /// <summary>
        /// 感谢标记
        /// 0-未感谢
        /// 1-已感谢
        /// </summary>
        public int IsThank { get; set; }

        public string OpenId { get; set; }
    }
}
