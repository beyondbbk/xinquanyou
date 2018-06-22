using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.Db
{
    public class Useradvise
    {
        public int ID { get; set; }

        public DateTime UploadTime { get; set; }

        public string Msg { get; set; }

        public int IsReply { get; set; }

        public string ReplyMsg { get; set; }

        public string OpenId { get; set; }
    }
}
