﻿var bodywidth;
var bodyheight;
window.onload = function () {
    bodywidth = $("body").width();
    bodyheight = $("body").height();

    console.log("所有资源都加载完毕，开始执行动画");
    //播放背景音乐
    var audio = new Audio();
    audio.src = './weeding/music/Building%20A%20Family.mp3';
    audio.play();

    //适配字体
    console.log(0.1 * bodyheight);
    $(".bigword").css("font-size", 0.06 * bodyheight + "px");
    $(".midword").css("font-size", 0.05 * bodyheight + "px");
    $(".smallword").css("font-size", 0.03 * bodyheight + "px");

    //首屏动画
    console.log("FirstSreen-Start" + new Date());
    firstAction(firstActionDisappear);
    
    //第二屏
    awaitFunc(secondAction, 26000);

};

function secondAction() {
    console.log("执行secondAction");

    var mainDiv = $("#mainDiv");
    mainDiv.css("visibility", "hidden");
    mainDiv.html($("#secondScreen").html());
    //让头像垂直居中
    centerOb("zhangyuimg");
    $("#zhangyuimg").css("animation", "fadeInLeft 1000ms ease 1000ms 1 both");
    //先飞入，再转圈
    awaitFunc(function() {
            $("#zhangyuimg").css("animation", "spin 15s infinite linear");
        },
        2000);
    

    {
        var temp = $("#secondtopflower");
        temp.css("width", "70%"); //宽度70%
        temp.css("height", "20%"); //高度20%
        temp.css("left", "15%");
        temp.css("top", "2%");
        temp.css("animation", "fadeInUp 2000ms ease 2000ms 1 both");
    }

    {
        $("#secondbottomflower div").css("transform", "rotateZ(180deg)");
        var temp = $("#secondbottomflower");
        temp.css("width", "70%"); //宽度70%
        temp.css("height", "20%"); //高度20%
        temp.css("left", "15%");
        temp.css("top", "38%");
        temp.css("animation", "fadeInDown 2000ms ease 2000ms 1 both");
    }

    mainDiv.css("visibility", "visible");
}

function firstActionDisappear() {
    console.log("执行firstActionDisappear");

    $("#topflower").css("animation", "fadeOutUp 1000ms ease 1500ms 1 both");
    $("#topleftbesideflower").css("animation", "fadeOutLeft 1000ms ease 2000ms 1 both");
    $("#toprightbesideflower").css("animation", "fadeOutRight 1000ms ease 2000ms 1 both");

    $("#left").css("animation", "fadeOutLeft 1000ms ease 2500ms 1 both");
    $("#right").css("animation", "fadeOutRight 1000ms ease 2500ms 1 both");

    $("#bottomleft").css("animation", "fadeOutLeft 1000ms ease 3000ms 1 both");
    $("#bottomright").css("animation", "fadeOutRight 1000ms ease 3000ms 1 both");

    $("#bottom").css("animation", "fadeOutDown 1000ms ease 3500ms 1 both");

    $("#yaoqing").css("animation", "bounceOut 1000ms ease 4500ms 1 both");
    $("#name").css("animation", "hinge 2500ms ease 5500ms 1 both");
    $("#hunli").css("animation", "bounceOutRight 2500ms ease 8500ms 1 both");

   
        awaitFunc(function() {
            console.log("FirstSreen-End" + new Date());
        }, 11000);
    
}


function firstAction(nextAction) {

    var mainDiv = $("#mainDiv");
    //mainDiv.css("visibility", "hidden");
    mainDiv.html($("#firstScreen").html());

    //调整花束位置
    //右边花需要翻转
    $(".rightflower div").css("transform", "rotateY(180deg)");
    {
        var temp = $("#topflower");

        temp.css("width", "70%"); //宽度70%
        temp.css("height", "20%"); //高度20%
        temp.css("left", "15%");
        temp.css("top", "-10%");
        temp.css("animation", "fadeInDown 2000ms ease 1500ms 1 both");
    }
    {
        var temp = $("#topleftbesideflower");
        temp.css("width", "40%");
        temp.css("height", "30%");
        temp.css("left", "-15%");
        temp.css("top", "0%");
        temp.css("animation", "fadeInLeft 1000ms ease 2000ms 1 both");
    }
    {
        var temp = $("#toprightbesideflower");
        temp.css("width", "40%");
        temp.css("height", "30%");
        temp.css("right", "-15%");
        temp.css("top", "0%");
        temp.css("animation", "fadeInRight 1000ms ease 2000ms 1 both");
    }
    {
        var temp = $("#left");
        temp.css("width", "30%");
        temp.css("height", "40%");
        temp.css("left", "-15%");
        temp.css("top", "30%");
        temp.css("animation", "fadeInLeft 1000ms ease 2500ms 1 both");
    }
    {
        var temp = $("#right");
        temp.css("width", "30%");
        temp.css("height", "40%");
        temp.css("right", "-15%");
        temp.css("top", "30%");
        temp.css("animation", "fadeInRight 1000ms ease 2500ms 1 both");
    }
    {
        var temp = $("#bottomleft");
        temp.css("width", "30%");
        temp.css("height", "30%");
        temp.css("left", "-15%");
        temp.css("bottom", "0%");
        temp.css("animation", " fadeInLeft 1000ms ease 3000ms 1 both");
    }
    {
        var temp = $("#bottomright");
        temp.css("width", "30%");
        temp.css("height", "30%");
        temp.css("right", "-15%");
        temp.css("bottom", "0%");
        temp.css("animation", " fadeInRight 1000ms ease 3000ms 1 both");
    }
    {
        var temp = $("#bottom");
        temp.css("width", "70%");
        temp.css("height", "30%");
        temp.css("left", "15%");
        temp.css("bottom", "-15%");
        temp.css("animation", "fadeInUp 1000ms ease 3500ms 1 both");
    }
    {
        $("#yaoqing").css("animation", "fadeIn 1000ms ease 4500ms 1 both");
        $("#hunli").css("visibility", "hidden");
        var nextFunc = function () {
            console.log("执行了");
            $("#hunli").css("animation", "fadeIn 2000ms ease 1000ms 1 both");
            $("#hunli").css("visibility", "visible");
            $("#remark").css("visibility", "visible");
            if (nextAction != null) {
                awaitFunc(nextAction, 3500);
            }

            //awaitFunc(function () {
            //    console.log("开始执行备注")
            //    typedWord(null, ["请携款前往，概不赊账", "请携款前往，可以赊账"], "replaceRemark", null,150,100);
            //    },
            //    3500);
        }
        awaitFunc(function () {
            typedWord(null, ["李涛", "大涛哥", "大涛姥爷"], "relaceName", nextFunc, 200, 100);
        },
            5500);
        mainDiv.css("visibility", "visible");
    }

    //$(function() {
    //    //per10Height = $("#mainPage").height();//640
    //    //console.log(per10Height);
    //    //console.log($("#mainPage").width());//360
    //    //用于展示的图片
    //    //var imgList1 = Array.from({ length: 6 }, function (v, i) {
    //    //    return "weeding/images/bgfont" + (i) + ".png";
    //    //});
    //    var imgList1 = Array.from({ length: 10 },
    //        function(v, i) {
    //            return "weeding/images/face" + (i + 1) + ".jpg";
    //        });

    //    //SeqTypeImgWord("邀请", [30], "隶书", "yaoqing", imgList1);
    //    //SeqTypeImgWord("鹏哥", [25], "隶书", "shui", imgList1);
    //    //var fun3 = function () {
    //    //    ajaxTypeWord("参\n\r加\n\r我\n\r们\n\r的\n\r婚\n\r礼", 20, "隶书", "hunli", imgList1, null);
    //    //}
    //    //var fun2 = function () { ajaxTypeWord("大涛哥", 30, "隶书", "shui", imgList1, fun3); }
    //    //var fun1 = function () { ajaxTypeWord("邀请", 30, "隶书", "yaoqing", imgList1, fun2); }

    //    //typeImgWord("zhangyu", zhangyuarr, imgList1, null);
    //    //SeqTypeImgWord("邀请", [30], "隶书", "yaoqing", imgList1);
    //    //SeqTypeImgWord("旭大爷", [25], "隶书", "shui", imgList1);
    //    //SeqTypeImgWord("参\n\r加\n\r我\n\r们\n\r的\n\r婚\n\r礼", [20], "隶书", "hunli", imgList1);

    //    //ajaxTypeWord("张玉❤杨健", 25, "微软雅黑", "zhangyu", imgList1, fun1);

    //    //SeqTypeImgWord("张玉❤杨健", [25], "隶书", "zhangyu", imgList1);
    //    //SeqTypeImgWord("参\n\r加\n\r我\n\r们\n\r的\n\r婚\n\r礼", [20], "隶书", "hunli", imgList1);
    //    //SeqTypeImgWord("杨健", [40], "隶书", "yangjian", imgList1);
    //    //ajaxTypeImg("1", "name", 60, 60, null);


    //    //typedWord("wanttosay", "", "word", null);

    //    //$("#bgmusic").play();
    //});


    function firstAction1() {
        var temp = [];
        temp.push($("#temp").html().replace(/\s{2,1000}/g, ""));
        temp.push($("#temp").html().replace(/\s{2,1000}/g, "").replace("概不", "可以"));
        typed(null, temp, "whatToSay", secondAction);
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
        awaitFunc(function () {
            $("#secondimg").addClass('animated rollOut');
        },
            2000);

    }

    //2.2
    function secondTwo() {

    }
}

