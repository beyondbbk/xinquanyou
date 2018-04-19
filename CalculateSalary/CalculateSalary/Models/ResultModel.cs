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

        public Dictionary<string,TeacherInfo> TeacherDetails = new Dictionary<string, TeacherInfo>();
    }

    public class TeacherInfo
    {
        public string TeacherName { get; set; }

        public HashSet<string> Students { get; set; } = new HashSet<string>();

        public HashSet<string> ClassNames { get; set; } = new HashSet<string>();

        //详细描述
        public string Detail
        {
            get
            {
                var detail = "";
                foreach (var rowSalaryInfo in RowSalaryInfos)
                {
                    detail += "<tr>";
                    detail += $"<td>{rowSalaryInfo.StudentName}</td>" +
                              $"<td>{rowSalaryInfo.ClassName}</td>" +
                              $"<td>{rowSalaryInfo.TotalHour}</td>" +
                              $"<td>{rowSalaryInfo.CurrentHour}</td>" +
                              $"<td>{rowSalaryInfo.Step}</td>" +
                              $"<td>{(string.IsNullOrEmpty(rowSalaryInfo.CalDetail)?"该生当月无课时": rowSalaryInfo.CalDetail.Replace("*","&nbsp;"))}</td>" +
                              $"<td>{rowSalaryInfo.FinalSalay}</td>";
                    detail += "</tr>";
                }
                return detail;
            }
        }

        public List<RowSalaryInfo> RowSalaryInfos = new List<RowSalaryInfo>();
    }

    /// <summary>
    /// 每一行的收入信息
    /// </summary>
    public class RowSalaryInfo
    {
        //指示行信息是否解析成功
        public bool IsSuccess { get; set; } = true;
        //姓名
        public string TeacherName { get; set; }

        //学生姓名
        public string StudentName { get; set; }

        //起步课时费
        public int StartMoney { get; set; }

        //班级
        public string ClassName { get; set; }

        //科目信息
        public string Subject { get; set; }

        //总课时
        public double TotalHour { get; set; }

        //当月课时
        public double CurrentHour { get; set; }

        //跨了几个阶梯
        public int Step { get; set; }

        //最终收入
        public double FinalSalay { get; set; }

        //计算过程
        public string CalDetail { get; set; }

        //错误的信息
        public string ErrMsg { get; set; }
    }
}
