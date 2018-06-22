using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysoft.Tjqxj.Models.Db;

namespace Mysoft.Tjqxj.Models.ViewModel
{
    public class GetFeedBackVm
    {
        public SignVm SignVm { get; set; }

        public List<FeedBack> FeedBacks { get; set; }

        public int Type { get; set; }

        public string CurrentHost { get; set; }
    }
}
