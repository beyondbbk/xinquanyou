using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySoft.Weeding.Models.Respone
{
    public class HomeRes
    {
        public int TogetherDays { get; set; }

        public int PrepareDays { get; set; }

        public FriendInfo FriendInfo { get; set; }

        public bool IsReload { get; set; }

        public string TimeId = DateTime.Now.ToString("yyyyMMddhhmmssfff");
    }
}
