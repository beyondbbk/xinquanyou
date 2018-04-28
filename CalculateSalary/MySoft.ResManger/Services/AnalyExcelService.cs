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
        public static ExcelViewInfo GetView(string excelpath,string sheetName="",int pageNum=1,string searchWords="")
        {
            var excelInfo = new ExcelViewInfo();
            excelInfo.PageNum = pageNum;
            if (pageNum == 1) excelInfo.IsFirstPage = true;
            excelInfo.SearchWords = searchWords;
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
                    excelInfo.RowInfos = GetRowInfos(currentSheet, excelInfo);
                }
            }
           
            return excelInfo;
        }


        private static Dictionary<int,List<string>> GetRowInfos(ExcelWorksheet sheet,ExcelViewInfo excelInfo)
        {
            var dic = new Dictionary<int,List<string>>();
            var step = 18;//默认每页显示条数
            var start = ((excelInfo.PageNum - 1) * step) != 0 ? ((excelInfo.PageNum - 1) * step) : 1;
            var searchList = string.IsNullOrEmpty(excelInfo.SearchWords) ?new string[0]: excelInfo.SearchWords.Split(' ', StringSplitOptions.RemoveEmptyEntries);//搜索关键字切割
            if (searchList.Length == 0)//有无关键字需要采取不同的策略
            {
                for (var i = start + 1; i <= excelInfo.PageNum * step; i++)
                {
                    if (i == sheet.Dimension.Rows) excelInfo.IsEndPage = true;
                    dic.Add(i, new List<string>());
                    for (var j = 1; j < sheet.Dimension.Columns; j++)
                    {
                        if (!ContainKeyWord(sheet, i, searchList)) continue;
                        dic[i].Add(sheet.GetValue(i, j) == null ? "" : sheet.GetValue(i, j).ToString());
                    }
                }
            }
            else
            {
                int rowCount = 1;//记录符合关键字的行数，第一行列头
                for (var i = 1; i <= sheet.Dimension.Rows; i++)
                {
                    if (i == sheet.Dimension.Rows){excelInfo.IsEndPage = true;}
                    if (!ContainKeyWord(sheet, i, searchList)) continue;
                    rowCount++;
                    if (rowCount > excelInfo.PageNum * step) break;
                    if (rowCount >= start + 1 && rowCount <= excelInfo.PageNum * step)
                    {
                        dic.Add(i, new List<string>());
                        for (var j = 1; j < sheet.Dimension.Columns; j++)
                        {
                            var temp = sheet.GetValue(i, j) == null ? "" : sheet.GetValue(i, j).ToString();
                            temp = HighLight(temp, searchList);
                            dic[i].Add(temp);
                        }
                    }
                }
            }
            return dic;
        }

        private static string HighLight(string txt,string[] searchWords)
        {
            foreach (var searchWord in searchWords)
            {
                txt = txt.Replace(searchWord, "<mark>"+ searchWord + "</mark>");
            }
            return txt;
        }

        private static bool ContainKeyWord(ExcelWorksheet sheet,int row,string[] searchList)
        {
            if (searchList.Length == 0) return true;//没有限制关键字

            var rowUnionStr = "";
            for (int i = 1; i < sheet.Dimension.Columns; i++)
            {
                rowUnionStr += (sheet.GetValue(row, i) == null ? "" : sheet.GetValue(row, i).ToString())+" ";
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
