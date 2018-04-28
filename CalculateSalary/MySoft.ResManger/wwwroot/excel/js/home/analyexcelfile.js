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
                GetView("","");
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

function GetView(sheetName,pageNum) {
    var searchWords = $("#searchinput").val();
    if (sheetName == "") {sheetName = $("#sheetname").val();}
    if (pageNum == "") {pageNum = $("#pagenum").val();} 
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

