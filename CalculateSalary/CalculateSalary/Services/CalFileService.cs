using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CalculateSalary.Models;
using NPOI.SS.UserModel;
using MySoft.Common;
using Newtonsoft.Json;

namespace MySoft.CalculateSalary.Services
{
    public class CalFileService
    {
        private static ISheet _sheet;
        private static Dictionary<List<string>, int> _dic = new Dictionary<List<string>, int>();//每个年级的起始薪资
        private static int _stepMoney = 5;//每个梯度涨5块钱
        private static int _stepHour=20;


        static CalFileService()
        {
            _dic.Add(new List<string>(){ "小一", "一年级", "小二", "二年级", "小三", "三年级", "小四", "四年级", "小五", "五年级", "小六", "六年级"}, 50);
            _dic.Add(new List<string>() { "初一", "初二", "初三" }, 65);
            _dic.Add(new List<string>() { "高一", "高二", "高三" }, 70);
            _dic.Add(new List<string>() { "初三新" }, 70);
            _dic.Add(new List<string>() { "高三新" }, 80);
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
                var currentMonthCol = GetCurrentMonthCol();
                //2 开始解析，记录无法识别的年级
                for (var i = 2; i <= ((_sheet.LastRowNum>200)?200:_sheet.LastRowNum+1); i++)
                {
                    var rowSalaryInfo = AnalyRow(i, currentMonthCol);
                    LogHelper.Debug($"第{i}行的解析结果：{JsonConvert.SerializeObject(rowSalaryInfo)}");
                    if (!rowSalaryInfo.IsSuccess)//如果该行解析不成功，将无法识别的信息记录下来，继续解析下面的信息
                    {
                        if(!string.IsNullOrEmpty(rowSalaryInfo.ErrMsg)) resultDto.Msg += rowSalaryInfo.ErrMsg;
                        continue;
                    }
                    if (!resultDto.TeacherDetails.ContainsKey(rowSalaryInfo.TeacherName)) resultDto.TeacherDetails.Add(rowSalaryInfo.TeacherName,new TeacherInfo());
                    resultDto.TeacherDetails[rowSalaryInfo.TeacherName].RowSalaryInfos.Add(rowSalaryInfo);
                    resultDto.TeacherDetails[rowSalaryInfo.TeacherName].ClassNames.Add(rowSalaryInfo.ClassName);
                    resultDto.TeacherDetails[rowSalaryInfo.TeacherName].Students.Add(rowSalaryInfo.StudentName);
                }
                return resultDto;
            }
        }


        /// <summary>
        /// 获取已经合算了几个月的薪资了
        /// </summary>
        /// <returns></returns>
        private static int GetCurrentMonthCol()
        {
            for (int col = 7; col < 24; col++)//24是12月份，最后一列
            {
                Debug.WriteLine(col);
                var hasData = false;//假设每列都是空的
                for (int row = 2; row <= ((_sheet.LastRowNum > 200) ? 200 : _sheet.LastRowNum+1); row++)
                {
                    //不为空表示该列有数据，就不是最新的一个月
                    if (!string.IsNullOrEmpty(GetCellValue(row, col)))
                    {
                        hasData = true;
                        break;
                    };
                    
                }
                if (!hasData)
                {
                    return col - 1;//该列为最新一个月所在的列 
                }
                
            }
            return 0;
        }

        /// <summary>
        /// 解析每一行的薪资信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static RowSalaryInfo AnalyRow(int row,int currentMonthCol)
        {
            Debug.WriteLine(row);
            var rowSalaryInfo = new RowSalaryInfo();
            rowSalaryInfo.TeacherName = GetCellValue(row, 5);
            if (string.IsNullOrEmpty(rowSalaryInfo.TeacherName))
            {
                rowSalaryInfo.IsSuccess = false;
                return rowSalaryInfo;
            }
            rowSalaryInfo.ClassName = GetCellValue(row, 3);
            if (string.IsNullOrEmpty(rowSalaryInfo.ClassName))
            {
                rowSalaryInfo.IsSuccess = false;
                return rowSalaryInfo;
            }
            rowSalaryInfo.StudentName = GetCellValue(row, 2);
            rowSalaryInfo.StartMoney = GetStartSalary(rowSalaryInfo.ClassName);//获取初始薪水
            if (rowSalaryInfo.StartMoney==0)//没有拿到起始薪资，证明这个年级是不支持的
            {
                rowSalaryInfo.IsSuccess = false;
                rowSalaryInfo.ErrMsg = "年级[" + rowSalaryInfo.ClassName + "]暂不支持；";
                return rowSalaryInfo;
            }
            rowSalaryInfo.Subject = GetCellValue(row, 7);
            var hourlist = GetHourList(row);
            rowSalaryInfo.TotalHour = hourlist.Sum();
            //如果课时记录的数目符合月份数，证明学生一直在上课
            //如果不想等，证明学生已经停课了
            rowSalaryInfo.CurrentHour = string.IsNullOrEmpty(GetCellValue(row,currentMonthCol))? 0: hourlist.LastOrDefault();
            rowSalaryInfo.Step = (int)Math.Ceiling((double)rowSalaryInfo.TotalHour / _stepHour);
            rowSalaryInfo.FinalSalay = GetFinalyMoney(rowSalaryInfo);
            return rowSalaryInfo;
        }

        private static int GetStartSalary(string className)
        {
            foreach (var temp in _dic)
            {
                if (temp.Key.Contains(className)) return temp.Value;
            }
            return 0;
        }

        /// <summary>
        /// 计算最终的收入
        /// </summary>
        /// <param name="rowSalaryInfo"></param>
        /// <param name="calDetail">输出计算详情</param>
        /// <returns></returns>
        private static double GetFinalyMoney(RowSalaryInfo rowSalaryInfo)
        {
           
            var tempFinalySalay = 0.0;
            var tempcurrentHour = rowSalaryInfo.CurrentHour;
            var tempTotalHour = rowSalaryInfo.TotalHour;
            var tempStep = rowSalaryInfo.Step;
            while (tempcurrentHour != 0)
            {
                //时段对应工资
                var stepHourMoney = rowSalaryInfo.StartMoney+ _stepMoney * tempStep;
                var stepHour = tempTotalHour - (tempStep - 1) * _stepHour;//待计算的最高时段小时总数
         
                if (tempcurrentHour < stepHour) stepHour = tempcurrentHour;
                tempFinalySalay += stepHourMoney * stepHour;
                rowSalaryInfo.CalDetail += $"梯度{tempStep}：({rowSalaryInfo.StartMoney.ToString().PadRight(3,'*')} + {(_stepMoney * tempStep).ToString().PadLeft(3,'*')}) x {stepHour.ToString().PadRight(4, '*')} = {stepHourMoney * stepHour}<br/>";
                tempTotalHour -= stepHour;
                tempcurrentHour -= stepHour;
                tempStep--;
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
            var list = new List<double>();
            for (var i = 8; i < ((_sheet.GetRow(row-1).LastCellNum>30)?30: _sheet.GetRow(row-1).LastCellNum); i++)
            {
                var hour = GetCellValue(row, i);
                if (string.IsNullOrWhiteSpace(hour?.Trim()))
                {
                    continue;
                }
                //规避总计列
                if (GetCellValue(1, i) == "总计")
                {
                    continue;
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
                resultDto.ErrMsg += "表格第 1 行第 3 列错误，该列应为[年级]列；";
            }
            //第一行第五列是教师姓名
            if (GetCellValue(1, 5) != "教师姓名")
            {
                resultDto.ErrMsg += "表格第 1 行第 5 列错误，该列应为[教师姓名]列；";
            }
            //第一行第六列是科目
            if (GetCellValue(1, 7) != "科目")
            {
                resultDto.ErrMsg += "表格第 1 行第 6 列错误，该列应为[科目]列；";
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
            //LogHelper.Error("读取数据时类型不在候选之列，类型为"+cell.CellType.ToString());

            return null;
        }
    }

    
}
