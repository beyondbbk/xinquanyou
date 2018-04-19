using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateSalary.Models
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }

        public string Msg { get; set; }

        public string ErrMsg { get; set; }

        public Dictionary<string,double> WorkerSalary  = new Dictionary<string, double>();
    }
}
