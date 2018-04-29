var edittitlecount = 0;
var currentrow = 0;
var currentcol = 0;
var currentob = null;

$(
    //监听搜索功能
    $('#searchinput').bind('keypress',
        function(event) {
            if (event.keyCode == "13") {
                //需要处理的事情
                GetSearchView("", "");
            }
        }
    )

);

$(
    //监听修改功能
    $('#newvalue').bind('keypress',
        function (event) {
            if (event.keyCode == "13") {
                //需要处理的事情
                BtnYes();
            }
        }
    )
);


function getContextPath() {
    var hostName = document.location.host;
    return hostName+"/";
}

//页码触发

function GetPageView(pageNum, type) {
    if (type == 'pre') {
        if ($("#isfirstpage").val() == "True") {
            GlobalShowInfo("当前页已经是第一页...", "fail");
            GlobalAutoHide();
            return;
        }
    }
    if (type == 'next') {
        if ($("#isendpage").val() == "True") {
            GlobalShowInfo("当前页已经是最后一页...", "fail");
            GlobalAutoHide();
            return;
        }
    }
    $("#pagenum").val(pageNum);
    CommonGetView();
}

//点击工作表触发
//页码初始化，关键词初始化
function GetSheetView(sheetName) {
    $("#sheetname").val(sheetName);
    $("#pagenum").val("1");
    $("#searchinput").val("");
    CommonGetView();
}

//搜索触发
//页码初始化
function GetSearchView() {
    var searchWords = $("#searchinput").val();
    $("#pagenum").val("1");
    CommonGetView();

}

function CommonGetView() {
    var searchWords = $("#searchinput").val();
    var sheetName = $("#sheetname").val();
    var pageNum = $("#pagenum").val();
    GlobalShowInfo("正在拉取数据中，请稍候...", "ok");

    window.location = "/excel/home/analyexcelfile?companyname=yh&sheetname=" + encodeURIComponent(sheetName) + "&pagenum=" + pageNum + "&searchWords=" + searchWords;

}

function ShowTitle() {
    edittitlecount++;
    if (edittitlecount < 2) {
        GlobalShowInfo("单击该数据可进行编辑...", "ok");
        GlobalAutoHide();
    }
}

function EditData(ob,  row, col) {
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

    var sheetName = $("#sheetname").val();

    AjaxHelper("/excel/home/setexcelvalue","companyname=yh&sheetname=" +
        sheetName +
        "&row=" +
        currentrow +
        "&col=" +
        currentcol +
        "&newData=" +
        $("#newvalue").val(),successfun);
}

