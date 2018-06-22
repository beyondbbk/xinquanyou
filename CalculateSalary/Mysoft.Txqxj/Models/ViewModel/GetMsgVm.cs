using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysoft.Tjqxj.Models.Db;

namespace Mysoft.Tjqxj.Models.ViewModel
{
    public class GetMsgVm
    {
        
        public int ID { get; set; }

        public DateTime UploadTime { get; set; }

        public string Msg { get; set; }

        public int IsReply { get; set; }

        public string ReplyMsg { get; set; }

        public string OpenId { get; set; }

        public List<GetMsgVm> FormDb(List<Useradvise> lists)
        {
            var getMsgVms = new List<GetMsgVm>();
            foreach (var temp in lists)
            {
                getMsgVms.Add(new GetMsgVm()
                {
                    ID = temp.ID,
                    UploadTime = temp.UploadTime,
                    Msg = temp.Msg,
                    IsReply = temp.IsReply,
                    ReplyMsg = temp.ReplyMsg,
                    OpenId = temp.OpenId
                });
            }
            return getMsgVms;
        }
    }
}
