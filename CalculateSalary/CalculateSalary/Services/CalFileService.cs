using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CalculateSalary.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CalculateSalary.Services
{
    public class CalFileService
    {
        private static ISheet _sheet;

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

                var resultDto = new ResultDto();

                //1 识别文件是否符合预设规范

                //2 开始解析，记录无法识别的年级

                //3 汇总结果
                return resultDto;
            }


        }

        private static string CheckFile(int rowNum, int colNum)
        {


            var row = _sheet.GetRow(rowNum);

                var cell = row.GetCell(colNum);
                //根据单元格的数据类型来判断怎样提取数据
                if (cell.CellType == CellType.String)
                    return (cell.StringCellValue);
                if (cell.CellType == CellType.Numeric)
                    return (cell.NumericCellValue.ToString());
                if (cell.CellType == CellType.Boolean)
                    return cell.BooleanCellValue.ToString();

            return null;
        }
    }
}
