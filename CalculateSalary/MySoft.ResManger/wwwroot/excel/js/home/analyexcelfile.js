var edittitlecount = 0;
var currentrow = 0;
var currentcol = 0;

function GetView(companyName, sheetName, pageNum) {
    if (companyName == "") {
        companyName = "yh";
    }
    if (pageNum == "") {
        pageNum = 1;
    }
    GlobalShowInfo("正在拉取数据中，请稍候...", "ok");
    window.location.href = "/excel/home/analyexcelfile?companyname=yh&sheetname=" + sheetName + "&pagenum=" + pageNum;
}

function ShowTitle() {
    edittitlecount++;
    if (edittitlecount < 5) {
        GlobalShowInfo("单击该数据可进行编辑...", "ok");
        GlobalAutoHide();
    }
}

function EditData(prevalue, row, col) {
    currentrow = row;
    currentcol = col;
    //显示旧值，填充数据
    $("#prevalue").html(prevalue);
    $("#editmodal").modal("show");
}