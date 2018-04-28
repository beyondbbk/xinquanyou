var edittitlecount = 0;
var currentrow = 0;
var currentcol = 0;
var currentsheetname = "";
var currentob = null;

function GetView(companyName, sheetName, pageNum) {
    var searchWords = $("#searchinput").val();
    console.log("搜索关键字：" + searchWords);
    GlobalShowInfo("正在拉取数据中，请稍候...", "ok");
    window.location.href = "/excel/home/analyexcelfile?companyname=yh&sheetname=" + sheetName + "&pagenum=" + pageNum + "&searchWords=" + searchWords;
}

function ShowTitle() {
    edittitlecount++;
    if (edittitlecount < 3) {
        GlobalShowInfo("单击该数据可进行编辑...", "ok");
        GlobalAutoHide();
    }
}

function EditData(ob,sheetname,  row, col) {
    currentsheetname = sheetname;
    currentrow = row;
    currentcol = col;
    //显示旧值，填充数据
    $("#prevalue").html($(ob).html());
    currentob = $(ob);
    $("#editmodal").modal("show");
}

function BtnYes() {
    console.log("row-" + currentrow);
    console.log("col-" + currentcol);
    var successfun = function (data) {
        GlobalShowInfo("数据修改成功...", "ok");
        GlobalAutoHide();
        $("#editmodal").modal("hide");
        currentob.html($("#newvalue").val());
        $("#newvalue").val("");
        console.log(data.IsSuccess);
    };
    AjaxHelper("/excel/home/setexcelvalue","companyname=yh&sheetname=" +
        currentsheetname +
        "&row=" +
        currentrow +
        "&col=" +
        currentcol +
        "&newData=" +
        $("#newvalue").val(),successfun);
}

