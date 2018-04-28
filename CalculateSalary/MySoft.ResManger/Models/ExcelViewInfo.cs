using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySoft.ResManger.Models
{
    public class ExcelViewInfo
    {
        public bool IsSuccess { get; set; } = true;//默认成功

        public List<SheetInfo> SheetInfos { get; set; }

        public string ErrMsg { get; set; }

        public string Info { get; set; }

        public string CurrentSheetName { get; set; }

        public int CurrentRowsCount { get; set; }

        public int CurrentColsCount { get; set; }

        public int PageNum { get; set; }

        public string SearchWords { get; set; }

        public bool IsFirstPage { get; set; }//是否是第一页

        public bool IsEndPage { get; set; }

        public List<string> Headers { get; set; }

        public Dictionary<int,List<string>> RowInfos { get; set; }
    }

    public class SheetInfo
    {
        public string SheetName { get; set; }

        public int RowCount { get; set; }

        public int ColCount { get; set; }
    }
}
