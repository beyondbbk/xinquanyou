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
        #region 获取Excel数据
        public static ExcelInfo GetView(string excelpath,string sheetName="",int pageNum=1,string searchWords="")
        {
            var excelInfo = new ExcelInfo();
            using (var fs =  new FileStream(excelpath, FileMode.Open))
            {
                using (var package = new ExcelPackage(fs))
                {
                    excelInfo.SheetInfos = GetSheetInfos(package.Workbook.Worksheets);
                    var currentSheet = package.Workbook.Worksheets[sheetName] ?? package.Workbook.Worksheets[0];
                    sheetName = currentSheet.Name;
                    excelInfo.CurrentSheetName = sheetName;
                    excelInfo.CurrentRowsCount = excelInfo.SheetInfos.FirstOrDefault(u => u.SheetName == sheetName).RowCount;
                    excelInfo.CurrentColsCount = excelInfo.SheetInfos.FirstOrDefault(u => u.SheetName == sheetName).ColCount;
                    excelInfo.Headers = GetHeaders(currentSheet);
                    excelInfo.RowInfos = GetRowInfos(currentSheet, pageNum,searchWords);
                    package.Dispose();
                }
            }
           
            return excelInfo;
        }


        private static Dictionary<int,List<string>> GetRowInfos(ExcelWorksheet sheet,int pageNum,string searchWords)
        {
            var dic = new Dictionary<int,List<string>>();
            var step = 200;//默认每页显示20条
            var start = ((pageNum - 1) * step) != 0 ? ((pageNum - 1) * step) : 1;
            var searchList = string.IsNullOrEmpty(searchWords)?new string[0]: searchWords.Split(' ', StringSplitOptions.RemoveEmptyEntries);//搜索关键字切割
            for (var i = start+1; i < pageNum*step; i++)
            {
                if(i>sheet.Dimension.Rows)break;
                dic.Add(i,new List<string>());
                for (var j = 1; j < sheet.Dimension.Columns; j++)
                {
                    if(!ContainKeyWord(sheet,i,searchList)) continue;
                    dic[i].Add(sheet.GetValue(i,j)==null?"": sheet.GetValue(i, j).ToString());
                }
            }
            return dic;
        }

        private static bool ContainKeyWord(ExcelWorksheet sheet,int row,string[] searchList)
        {
            if (searchList.Length == 0) return true;//没有限制关键字

            var rowUnionStr = "";
            for (int i = 1; i < sheet.Dimension.Columns; i++)
            {
                rowUnionStr += (sheet.GetValue(row, i) == null ? "" : sheet.GetValue(row, i).ToString());
            }
            foreach (var search in searchList)
            {
                if (!rowUnionStr.Contains(search)) return false;//有任何一个关键字不包括就丢掉
            }
            return true;
        }

        private static List<string> GetHeaders(ExcelWorksheet sheet)
        {
            var headers = new List<string>();
            for (int i = 1; i < sheet.Dimension.Columns; i++)
            {
                headers.Add((sheet.GetValue(1,i)==null)?"": (sheet.GetValue(1, i).ToString()));
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
        #endregion

        #region 设置Excel数据

        public static bool SetCellValue(string excelPath, string sheetName, int row, int col,string newData)
        {
            var fileinfo = new FileInfo(excelPath);
           
                using (var package = new ExcelPackage(fileinfo))
                {
                    var sheet = package.Workbook.Worksheets[sheetName];
                    if (sheet == null) return false;
                    sheet.SetValue(row,col, newData);
                    package.Save();
                    return true;
                }
            
        }


        #endregion
    }
}
