using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySoft.ResManger.Models;
using OfficeOpenXml;

namespace MySoft.ResManger.Services
{
    public class AnalyExcelService
    {
        public static ExcelInfo GetView(string excelpath,string currentSheetName="",int pageNum=1)
        {
            var excelInfo = new ExcelInfo();
            using (var package = new ExcelPackage(new FileStream(excelpath, FileMode.Open)))
            {
                excelInfo.SheetInfos = GetSheetInfos(package.Workbook.Worksheets);
                var currentSheet = package.Workbook.Worksheets[currentSheetName] ?? package.Workbook.Worksheets[0];
                currentSheetName = currentSheet.Name;
                excelInfo.CurrentSheetName = currentSheetName;
                excelInfo.CurrentRowsCount = excelInfo.SheetInfos.FirstOrDefault(u => u.SheetName == currentSheetName).RowCount;
                excelInfo.CurrentColsCount = excelInfo.SheetInfos.FirstOrDefault(u => u.SheetName == currentSheetName).ColCount;
                excelInfo.Headers = GetHeaders(currentSheet);
                excelInfo.RowInfos = GetRowInfos(currentSheet, pageNum);
            }
            return excelInfo;
        }


        private static Dictionary<int,List<string>> GetRowInfos(ExcelWorksheet sheet,int pageNum)
        {
            var dic = new Dictionary<int,List<string>>();
            var step = 20;//默认每页显示20条
            var start = ((pageNum - 1) * step) != 0 ? ((pageNum - 1) * step) : 1;
            for (var i = start+1; i < pageNum*step; i++)
            {
                dic.Add(i,new List<string>());
                for (var j = 1; j < sheet.Dimension.Columns; j++)
                {
                    dic[i].Add(sheet.GetValue(i,j)==null?"": sheet.GetValue(i, j).ToString());
                }
            }
            return dic;
        }

        private static List<string> GetHeaders(ExcelWorksheet sheet)
        {
            var headers = new List<string>();
            for (int i = 1; i < sheet.Dimension.Columns; i++)
            {
                headers.Add(sheet.GetValue(1,i).ToString());
            }
            return headers;
        }


        private static List<SheetInfo> GetSheetInfos(ExcelWorksheets excelWorksheets)
        {
            var sheetInfos = new List<SheetInfo>();
            foreach (var excelWorksheet in excelWorksheets)
            {
                var sheetInfo = new SheetInfo();
                sheetInfo.SheetName = excelWorksheet.Name;
                sheetInfo.RowCount = excelWorksheet.Dimension?.End.Row ?? 0;
                sheetInfo.ColCount = excelWorksheet.Dimension?.End.Column ?? 0;
                sheetInfos.Add(sheetInfo);
            }
            return sheetInfos;
        }
    }
}
