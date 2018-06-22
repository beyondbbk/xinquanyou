using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.ViewModel
{
    public class SignVm
    { 
        public string AppId { get; set; }

        public string TimeStamp { get; set; }

        public string NonceStr { get; set; }

        public string Signature { get; set; }

        public string OpenId { get; set; }
    }
}
