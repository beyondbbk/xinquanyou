using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateSalary.Models
{
    public class CalFileViewModel
    {
        //string 为filename，list为结果集
        public Dictionary<string,List<RowInfo>> Results=new Dictionary<string, List<RowInfo>>();
    }
    public class RowInfo
    {
        public string Name { get; set; }

        public double Salary { get; set; }
    }
}
