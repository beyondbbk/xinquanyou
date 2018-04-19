using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateSalary.Models
{
    public class CalFileViewModel
    {
        public string ExcelName { get; set; }

        public bool IsSuccess { get; set; }

        public string Msg { get; set; }

        public string ErrMsg { get; set; }

        //string 为filename，list为结果集
        public Dictionary<string, TeacherInfo> TeacherDetails = new Dictionary<string, TeacherInfo>();
    }
}
