using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace MySoft.ResManger.Services
{
    public class ExcelHandler
    {
        public string GetCell(string path, int row, int col)
        {
            using (ExcelPackage package = new ExcelPackage(new FileStream(path, FileMode.Open)))
            {
                var sheet = package.Workbook.Worksheets[0];
                return sheet.GetValue(row, col).ToString();
            }
            
        }

        public void SetValue()
        {

        }
    }
}
