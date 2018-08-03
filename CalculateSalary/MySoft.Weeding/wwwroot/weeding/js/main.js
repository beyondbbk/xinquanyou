$(function () {
    //per10Height = $("#mainPage").height();//640
    //console.log(per10Height);
    //console.log($("#mainPage").width());//360
    //用于展示的图片
    var imgList1 = Array.from({ length: 11 }, function (v, i) {
        return "weeding/images/face" + (i + 1) + ".jpg";
    });
    //隶书最佳
    //var strArr = '["xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx…xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxxxxxyyyxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx…xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyyxxxxxxxxxx", "xxxxyyxxxxyxxxyyyxxxxxxyxxxxxxxxxxxxxxxxxyxxxxxxxx…yxxxxyyyxxxxxxxxxxxxxxxxxxxyxxxxxxxxyyyxxxxxxxxxx", "xxxyyyyyyyyyxxyyyxxxxyyyyxxxxxxxxxxxxxxyyyyyyyyyyy…yyyyyyyyxxxxxxxxxxxxxxxxxyyyxxxxxxxxyyyyyyyxxxxxx", "xxxyyyxxxyyyxxyyyxxxyyyyyxxxxxxxxxxxxxyyyyyyyyyyyy…yyyyyyyyxxxxxxxxxxxxxxxyyyyxxxxxxyyyyyyyyyyxxxxxx", "xxxxxxxxxyyxxxyyyxyyyyyxxxxxxxxxxxxxxxxyxxxxxxxxyy…xxxyyyxxxxxxxxxxxxxxyyyyyyxxyyyyxxxxyyyxxxyxxxxxx", "xxxxxxxxxyyxxxyyyyyyyxxxxxxxxxxxxxxxxxxxxxxxxxxxyy…xyyyyxxxxxxxxxxxxxyyyyyyyxxyyyyyyyyyyyyyyyyyyyxxx", "xxxxxyyyyyyxxxxyyyxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyy…yyyyxxxxxxxxxxxxxxxyyyyyyxxxxxyxxxxxyyyxxxyxxxxxx", "xxxxyyyyyyyxxxxyyxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyy…yyyyyyyyyyyyyyxxxxxxyxyyyxxxxyyxxyyyyyyyyyyxxxxxx", "xxxxyyxxxxxyyyyyyyyyyyyyyyyyxxxxxxxxxxxxyyyyyyyyyy…xxyyxxyyyyyyyyxxxxxxxxyyyxxxxyyyxxxxyyyxxxxxxxxxx", "xxxxyyxxxxyyyyyyyyyyyyyyyyxxxxxxxxxxxxxyyyyyyyyyyy…xyyyxxxyyxxyyyxxxxxxxxyyyxxxxxxyyyyyyyyyyyyxxxxxx", "xxxxyyyyyyxxxxxyyxyyxxxxxxxxxxxxxxxxxxxyyxxxxxxxyy…yyyxxxyyxxxyyyxxxxxxxxyyyxyyyxxyyxxxxyyxxxxxxxxxx", "xxxxyyyyyyyxxxxyyxyyyxxxxxxxxxxxxxxxxxxxxxxxxxxxyy…yyxxxyyxxxxyyyxxxxxxxxyyyxxxyyyyyyyyyyyyyyyyxxxxx", "xxxxxxxxyyyxxxyyyxxxyyyxxxxxxxxxxxxxxxxxxxxxxxxxyy…xxxxyyyxxxyyyxxxxxxxxxyyyxxxxyyyyxxxxyyxxxxxxxxxx", "xxxxxxxxyyyxxxyyyxxxxyyyxxxxxxxxxxxxxxxxxxxxxxxyyy…xxyyyyxxxxyyyxxxxxxxxxyyyxxxxyyyyyxxxyyxxxxxxxxxx", "xxxxxxxxyyyxxxyyyxxxxxyyyyxxxxxxxxxxxxxxxxxxxxxyyy…xyyyxxxxxyyyxxxxxxxxxxyyyxxxyyyxxyyyyyxxxxxxxxxxx", "xxxxxxxxyyyxxxyyyxxxyyyyyyyyxxxxxxxxyyyyyyyyyyyyyy…yyyxxxxxyyyxxxxxxxxxxxyyyyyyyyxxxxyyyyyyxxxxxxxxx", "xxxxxxxyyyxxxxyyyxxyyxxxyyyyyyxxxxxyyyyyyyyyyyyyyy…yxxxxxxyyyyxxxxxxxxxxxyyyxyxxxxxxxxxyyyyyyyyxxxxx", "xyyxxxyyyyxxxyyyyyyyxxxxyyyyyyyxxxyyyyyyyyxxxxxxxx…xxyyyyyyyyxxxxxxxxxxxxyyyxxxxxxxxxxxxyyyyyyyyyyyy", "xxyyyyyyyyxxxyyyyyyxxxxxxyyyyyxxxxyyyxxxxxxxxxxxxx…xxxyyyyyyxxxxxxxxxxxxxyyyxxxxxxxxxxxxxyyyyyyyyyyy", "xxxyyyyyyxxxxyyyyyxxxxxxxxyyxxxxxxxxxxxxxxxxxxxxxx…xxxxyyyyxxxxxxxxxxxxxxyyyxxxxxxxxxxxxxxxyyyyyyyyx", "xxxxxyyxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx…xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyyyyxxx"]';

    //SeqTypeImgWord("邀请", [30], "隶书", "yaoqing", imgList1);
    //SeqTypeImgWord("鹏哥", [25], "隶书", "shui", imgList1);
    var fun3 = function () {
        ajaxTypeWord("参\n\r加\n\r我\n\r们\n\r的\n\r婚\n\r礼", [25], "隶书", "hunli", imgList1,null); }
    var fun2 = function () { ajaxTypeWord("鹏哥", 25, "隶书", "shui", imgList1, fun3); }
    var fun1 = function () { ajaxTypeWord("邀请", 25, "隶书", "yaoqing", imgList1, fun2); }
    //typeImgWord("zhangyu", strArr, imgList1, fun1);
    ajaxTypeWord("张玉\n\r杨健", 25, "隶书", "zhangyu", imgList1, fun1);
    //SeqTypeImgWord("张玉❤杨健", [25], "隶书", "zhangyu", imgList1);
    //SeqTypeImgWord("参\n\r加\n\r我\n\r们\n\r的\n\r婚\n\r礼", [20], "隶书", "hunli", imgList1);
    //SeqTypeImgWord("杨健", [40], "隶书", "yangjian", imgList1);
    //ajaxTypeImg("1", "name", 60, 60, null);
    

    //typedWord("wanttosay", "", "word", null);
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
    awaitFunc(function () {
        $("#secondimg").addClass('animated rollOut');
    }, 2000);

}

//2.2
function secondTwo() {

}

