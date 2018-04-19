using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CalculateSalary.Models;
using NPOI.SS.UserModel;
using MySoft.Common;

namespace MySoft.CalculateSalary.Services
{
    public class CalFileService
    {
        private static ISheet _sheet;
        private static Dictionary<string, int> _dic = new Dictionary<string, int>();//每个年级的起始薪资
        private static int _stepMoney = 5;//每个梯度涨5块钱
        private static int _stepHour=20;
        private static int maxStepHour = 140;//大于140的都按照140梯度计算


        static CalFileService()
        {
            _dic.Add("初一",65);
            _dic.Add("初二", 65);
            _dic.Add("初三", 70);
        }


        /// <summary>
        /// 传入Excel文件路径，将每个人的工资计算出来
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static ResultDto GetCalResult(string excelPath)
        {
            using (var input = new FileStream(excelPath, FileMode.Open))
            {
                var workBook = WorkbookFactory.Create(input);
                //再获取工作表，0编号对应第一个工作表
                _sheet = workBook.GetSheetAt(0);
                //1 识别文件是否符合预设规范
                var resultDto = Check();
                if (!resultDto.IsSuccess) return resultDto;
                //2 开始解析，记录无法识别的年级
                for (var i = 2; i < _sheet.LastRowNum; i++)
                {
                    var rowSalaryInfo = AnalyRow(i);
                    if (!rowSalaryInfo.IsSuccess)//如果改行解析不成功，将无法识别的信息记录下来
                    {
                        if(!string.IsNullOrEmpty(rowSalaryInfo.ErrMsg)) resultDto.Msg += rowSalaryInfo.ErrMsg;
                        continue;
                    }
                    if (!resultDto.WorkerSalary.ContainsKey(rowSalaryInfo.Name)) resultDto.WorkerSalary.Add(rowSalaryInfo.Name,0);
                    resultDto.WorkerSalary[rowSalaryInfo.Name] += rowSalaryInfo.FinalSalay;//累计工资
                }
                return resultDto;
            }
        }


        /// <summary>
        /// 解析每一行的薪资信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static RowSalaryInfo AnalyRow(int row)
        {
            var rowSalaryInfo = new RowSalaryInfo();
            rowSalaryInfo.Name = GetCellValue(row, 5);
            if (string.IsNullOrEmpty(rowSalaryInfo.Name))
            {
                rowSalaryInfo.IsSuccess = false;
                return rowSalaryInfo;
            }
            rowSalaryInfo.Class = GetCellValue(row, 3);
            if (string.IsNullOrEmpty(rowSalaryInfo.Class))
            {
                rowSalaryInfo.IsSuccess = false;
                return rowSalaryInfo;
            }
            if (!_dic.ContainsKey(rowSalaryInfo.Class))
            {
                rowSalaryInfo.IsSuccess = false;
                rowSalaryInfo.ErrMsg = "年级 " + rowSalaryInfo.Class + " 暂不支持";
                return rowSalaryInfo;
            }
            rowSalaryInfo.StartMoney = _dic[rowSalaryInfo.Class];//获取初始薪水
            rowSalaryInfo.Subject = GetCellValue(row, 7);
            var hourlist = GetHourList(row);
            rowSalaryInfo.TotalHour = hourlist.Sum();
            rowSalaryInfo.CurrentHour = hourlist.LastOrDefault();
            rowSalaryInfo.Step = (int)Math.Floor((double)rowSalaryInfo.TotalHour / _stepHour);
            rowSalaryInfo.FinalSalay = GetFinalyMoney(rowSalaryInfo);
            return rowSalaryInfo;
        }

        /// <summary>
        /// 计算最终的收入
        /// </summary>
        /// <param name="rowSalaryInfo"></param>
        /// <returns></returns>
        private static double GetFinalyMoney(RowSalaryInfo rowSalaryInfo)
        {
            var tempFinalySalay = 0.0;
            while (rowSalaryInfo.CurrentHour!=0)
            {
                //时段对应工资
                var stepHourMoney = rowSalaryInfo.StartMoney + _stepMoney * rowSalaryInfo.Step;
                var stepHour = rowSalaryInfo.TotalHour - (rowSalaryInfo.Step - 1) * _stepHour;//待计算的最高时段小时总数
         
                if (rowSalaryInfo.CurrentHour < stepHour) stepHour = rowSalaryInfo.CurrentHour;
                tempFinalySalay += stepHourMoney * stepHour;

                rowSalaryInfo.TotalHour -= stepHour;
                rowSalaryInfo.CurrentHour -= stepHour;
                rowSalaryInfo.Step--;
            }
            return tempFinalySalay;
        }


        /// <summary>
        /// 获取总额度
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static List<double> GetHourList(int row)
        {
            Debug.WriteLine("解析行:"+row);
            var list = new List<double>();
            for (var i = 8; i < _sheet.GetRow(row).LastCellNum; i++)
            {
                var hour = GetCellValue(row, i);
                if (string.IsNullOrEmpty(hour))
                {
                    list.Add(0);continue;
                }
                list.Add(Convert.ToDouble(hour));
            }
            return list;
        }


        /// <summary>
        /// 检查文件是否符合规范
        /// </summary>
        /// <returns></returns>
        private static ResultDto Check()
        {
            var resultDto = new ResultDto();
            //第一行第三列是年级
            if (GetCellValue(1, 3) != "年级")
            {
                resultDto.ErrMsg += "表格第 1 行第 3 列错误，该列应为[年级]列;";
            }
            //第一行第五列是教师姓名
            if (GetCellValue(1, 5) != "教师姓名")
            {
                resultDto.ErrMsg += "表格第 1 行第 5 列错误，该列应为[教师姓名]列;";
            }
            //第一行第六列是科目
            if (GetCellValue(1, 7) != "科目")
            {
                resultDto.ErrMsg += "表格第 1 行第 6 列错误，该列应为[科目]列;";
            }
            resultDto.IsSuccess = string.IsNullOrEmpty(resultDto.ErrMsg);
            return resultDto;
        }


        private static string GetCellValue(int rowNum, int colNum)
        {
            var row = _sheet.GetRow(rowNum-1);
            var cell = row?.GetCell(colNum-1);//列是从0开始的，修正一下
            if (cell == null) return null;
            //根据单元格的数据类型来判断怎样提取数据
            if (cell.CellType == CellType.String)
                return (cell.StringCellValue);
            if (cell.CellType == CellType.Numeric)
                return (cell.NumericCellValue.ToString());
            //if (cell.CellType == CellType.Boolean)
            //    return cell.BooleanCellValue.ToString();
            MyLogHelper.Error("读取数据时类型不在候选之列，类型为"+cell.CellType.ToString());

            return null;
        }
    }

    /// <summary>
    /// 每一行的收入信息
    /// </summary>
    public class RowSalaryInfo
    {
        //指示行信息是否解析成功
        public bool IsSuccess { get; set; } = true;
        //姓名
        public string Name { get; set; }

        //起步课时费
        public int StartMoney { get; set; }

        //班级
        public string Class { get; set; }
        
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

        //额外的奖励
        public int ExtraAward { get; set; }

        //错误的信息
        public string ErrMsg { get; set; }
    }
}
