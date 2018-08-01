$(function () {
    //per10Height = $("#mainPage").height();//640
    //console.log(per10Height);
    //console.log($("#mainPage").width());//360
    //用于展示的图片
    var imgList1 = Array.from({ length: 11 }, function (v, i) {
        return "weeding/images/face" + (i + 1) + ".jpg";
    });
    ajaxTypeImg("国家\n\r哈\n\r骄", 20, "楷体", "welcome", imgList1);
    typedWord("wanttosay", "", "word", null);
});

function firstAction() {
    var temp = [];
    temp.push($("#temp").html().replace(/\s{2,1000}/g, ""));
    temp.push($("#temp").html().replace(/\s{2,1000}/g, "").replace("概不", "可以"));
    typed("", temp, "whatToSay", secondAction);
}

//第二步
function secondAction() {
    $("#mainPage").html($("#secondPage").html());
    typed("secondtemp", "", "secondman", secondOne);
}

//2.1
function secondOne() {
    $("#secondimg").addClass('animated rollIn');
    typed("secondimgtemp", "", "secondimg", secondTwo);
    awaitFunc(function() {
        $("#secondimg").addClass('animated rollOut');
    }, 2000);
    
}

//2.2
function secondTwo() {

}

