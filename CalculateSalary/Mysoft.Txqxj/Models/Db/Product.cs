using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.Db
{
    public class Product
    {
        public int ID { get; set; }

        public string ProductType { get; set; }

        public string ProductTitle { get; set; }

        public string ImgPath { get; set; }

        public DateTime UploadTime { get; set; }
    }
}
