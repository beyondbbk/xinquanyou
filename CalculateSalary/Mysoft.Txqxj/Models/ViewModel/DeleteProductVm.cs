using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysoft.Tjqxj.Models.Db;

namespace Mysoft.Tjqxj.Models.ViewModel
{
    public class DeleteProductVm
    {
        public string ProductType { get; set; }

        public string SearchWord { get; set; }

        public List<Product> Products { get; set; }

        public SignVm SignVm { get; set; }

        public  string CurrentHost { get; set; }
    }
}
