var bodywidth;
var bodyheight;
//动画运行状态，默认正在运行
var obRunState = new Object();
obRunState.firstScreenIsRun = true;
obRunState.secondScreenIsRun = true;
obRunState.thirdScreenIsRun = true;
obRunState.fourScreenIsRun = true;
obRunState.fiveScreenIsRun = true;
obRunState.sixScreenIsRun = true;

//动画定时器
var secondTimeId = 0;
var thirdTimeId = 0;
var fourTimeId = 0;
var fiveTimeId = 0;
var sixTimeId = 0;

//日志信息
//var webLogs = [];


window.onload = function () {
    bodywidth = $("body").width();
    bodyheight = $("body").height();

    document.title = "To " + nameInfos[0];

    SaveWebLog("所有资源都加载完毕，开始执行动画");
    //播放背景音乐
    var audio = new Audio();
    audio.src = '/weeding/music/bg.mp3';
    musics[0].pause();
    audio.play();
    //根据屏幕高度来适配字体
    //SaveWebLog(0.1 * bodyheight);
    $(".bigwordbyheight").css("font-size", 0.067 * bodyheight + "px");
    $(".midwordbyheight").css("font-size", 0.046 * bodyheight + "px");
    $(".midbtnbyheight").css("font-size", 0.0325 * bodyheight + "px");
    $(".smallwordbyheight").css("font-size", 0.033 * bodyheight + "px");
    $(".verysmallwordbyheight").css("font-size", 0.024 * bodyheight + "px");
    $(".verysmallbtnbyheight").css("font-size", 0.017 * bodyheight + "px");

    //根据屏幕宽度来适配字体
    $(".bigword").css("font-size", 0.125 * bodywidth + "px");
    $(".midword").css("font-size", 0.10 * bodywidth + "px");
    $(".smallword").css("font-size", 0.065 * bodywidth + "px");
    $(".verysmallword").css("font-size", 0.05 * bodywidth + "px");
    ////首屏动画
    //firstAction(firstActionDisappear);
    //firstAction(null);

    //////第二屏
    //obRunState.firstScreenIsRun = true;
    //secondAction(secondActionDisapear);

    //////第三屏
    //thirdAction(thirdActionDisapear);

    //第四屏
    obRunState.thirdScreenIsRun = false;
    fourAction(fourDisapear);

    //第五屏
    //obRunState.fourScreenIsRun = false;
    fiveAction(fiveActionDisapear);

    //第六屏
    //obRunState.fiveScreenIsRun = false;
    sixAction(sixActionDisapear);

    //测试代码
    //typedWord(null, ["wo", "321", "321", "321", "321"], "test", null, 1000, 500);
};

function sixAction(nextAction) {
    sixTimeId = setInterval(function () {
        if (obRunState.fiveScreenIsRun) {
            return;
        }
        SaveWebLog("执行sixAction");
        clearInterval(sixTimeId);

        var mainDiv = $("#mainDiv");
        mainDiv.css("visibility", "hidden");
        mainDiv.html($("#sixScreen").html());

        {
            var temp = $("#sixleftflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "30%"); //高度20%
            temp.css("left", "-20%");
            temp.css("bottom", "-10%");
            temp.css("animation", "zoomIn 1000ms ease 0ms 1 both");
        }
        {
            var temp = $("#sixmidflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "25%");
            temp.css("bottom", "-10%");
            temp.css("animation", "zoomIn 1000ms ease 0ms 1 both");
        }
        {
            var temp = $("#sixrightflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "30%"); //高度20%
            temp.css("right", "-20%");
            temp.css("bottom", "-10%");
            temp.css("animation", "zoomIn 1000ms ease 0ms 1 both");
        }
        awaitFunc(function () {
            typedWord("sixtimesource", null, "sixtime", null, 70, 50, null);
        },
            1000);
        awaitFunc(function () {
            typedWord("sixaddresssource", null, "sixaddress", null, 70, 50, null);
        },
            6800);//原本是6000
        awaitFunc(function () {
            typedWord("trafficremarksource", null, "trafficremark", null, 70, 50, null);
        },
            10500);
        //$("#sixtime").css("animation", "fadeIn 2000ms ease 1000ms 1 both");
        //$("#sixaddress").css("animation", "fadeIn 2000ms ease 4000ms 1 both");
        //$("#trafficremark").css("animation", "fadeIn 2000ms ease 7000ms 1 both");

        awaitFunc(function () {
            //$("#sixleftflower").css("animation", "zoomOut 1000ms ease 0ms 1 both");
            $("#sixmidflower").css("animation", "zoomOut 2000ms ease 0ms 1 both");
            //$("#sixrightflower").css("animation", "zoomOut 1000ms ease 0ms 1 both");

        }, 29000);

        $("#about").css("animation", "fadeIn 4000ms ease 32000ms 1 both");
        mainDiv.css("visibility", "visible");
        awaitFunc(function () {
            $("#viewnavigate").html("查看导航");
            obRunState.sixScreenIsRun = false;
            if (nextAction != null) {
                nextAction();
            } else {
                obRunState.sixScreenIsRun = false;
            }
        },
            33000);//原值33000
    },
        700);
}

//最后一屏，花束文案均保留
function sixActionDisapear() {
    SaveWebLog("执行了sixActionDisapear");
}

function fiveAction(nextAction) {
    fiveTimeId = setInterval(function () {
        if (obRunState.fourScreenIsRun) {
            return;
        }
        SaveWebLog("执行fiveAction");
        clearInterval(fiveTimeId);

        var mainDiv = $("#mainDiv");
        mainDiv.css("visibility", "hidden");
        mainDiv.html($("#fiveScreen").html());

        {
            var temp = $("#fiveleftflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "30%"); //高度20%
            temp.css("left", "-10%");
            temp.css("bottom", "-5%");
            temp.css("animation", "zoomIn 1000ms ease 0ms 1 both");
        }
        {
            var temp = $("#fivemidflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "25%");
            temp.css("bottom", "-5%");
            temp.css("animation", "zoomIn 1000ms ease 0ms 1 both");
        }
        {
            var temp = $("#fiverightflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "30%"); //高度20%
            temp.css("right", "-10%");
            temp.css("bottom", "-5%");
            temp.css("animation", "zoomIn 1000ms ease 0ms 1 both");
        }

        $("#fivetitletxt div").html("我们一起走过了<span class='num'>7</span>年<span class='num'>"+day+"</span>天");
        //$("#fivetitleyear div").html("7年209天");
        $("#fivetitletxt").css("animation", "fadeIn 2000ms ease 1000ms 1 both");
        //$("#fivetitleyear").css("animation", "fadeIn 1000ms ease 4000ms 1 both");

        awaitFunc(function () {
            var typedFunc = function (arr, self) {
                if (arr == 0) {
                    self.typeSpeed = 200;
                }
                if (arr == 1) {
                    self.typeSpeed = 400;
                }
            };
            typedWord(null, [
                "如今<br>水到渠成<span class='symbol'>，</span>木已成舟<br>生米煮成了熟饭<br>而后<span class='symbol'>，</span>又熬成了稀饭<br>眼看着<span class='symbol'>，</span>快要炸锅了<br>我们决定",
                "如今<br>水到渠成<span class='symbol'>，</span>木已成舟<br>生米煮成了熟饭<br>而后<span class='symbol'>，</span>又熬成了稀饭<br>眼看着<span class='symbol'>，</span>快要炸锅了<br>我们决定 ",
                "如今<br>水到渠成<span class='symbol'>，</span>木已成舟<br>生米煮成了熟饭<br>而后<span class='symbol'>，</span>又熬成了稀饭<br>眼看着<span class='symbol'>，</span>快要炸锅了<br>我们决定<p class='fiveweed'>还是先把婚结了吧</p>"
            ], "typeOut", null, 200, 30, typedFunc);
        },
            3000);
        mainDiv.css("visibility", "visible");
        awaitFunc(function () {
            if (nextAction != null) {
                nextAction();
            } else {
                obRunState.fiveScreenIsRun = false;
            }
        },
            28000);
    },
        700);
}

function fiveActionDisapear() {
    SaveWebLog("执行fiveDisapear");

    $("#fiveleftflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");
    $("#fivemidflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");
    $("#fiverightflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");

    $("#fivetitletxt").css("animation", "fadeOut 1500ms ease 2500ms 1 both");
    $("#fivetxt").css("animation", "fadeOut 1500ms ease 2500ms 1 both");

    awaitFunc(function () {
        obRunState.fiveScreenIsRun = false;
        SaveWebLog("fiveAction结束");
    }, 4000);
}

function fourAction(nextAction) {
    fourTimeId = setInterval(function () {
        if (obRunState.thirdScreenIsRun) {
            return;
        }
        SaveWebLog("执行fourAction");
        clearInterval(fourTimeId);

        var mainDiv = $("#mainDiv");
        mainDiv.css("visibility", "hidden");
        mainDiv.html($("#fourScreen").html());

        {
            var temp = $("#fourbottomleftflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "-10%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 2000ms 1 both");
        }
        {
            var temp = $("#fourbottommidflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "25%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 2000ms 1 both");
        }
        {
            var temp = $("#fourbottomrightflower");
            temp.css("width", "50%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("right", "-10%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 2000ms 1 both");
        }
        //结缘
        $("#fourtitle div").html("结缘");
        $("#fourtitle").css("animation", "fadeInDown 1000ms ease 1000ms 1 both");
        $("#gifImg").attr("src", "weeding/images/2011.gif");
        $("#gifImg").css("animation", "rollIn 1500ms ease 3000ms 1 both");
        $("#fourremark div").html("缘<span class='symbol'>，</span>起于<span class='num'>2009</span><span class='symbol'>，</span>成于<span class='num'>2011</span>");
        $("#fourremark").css("animation", "rollIn 1500ms ease 3000ms 1 both");
        awaitFunc(function () {
            $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
            $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
        }, 7000);
        awaitFunc(function () {
            $("#fourtitle").css("animation", "fadeOutUp 1000ms ease 0ms 1 both");
        }, 9500);

        //相识
        awaitFunc(function () {
            //2012-好年华
            $("#gifImg").attr("src", "weeding/images/2012.gif");
            $("#fourtitle div").html("相识");
            $("#fourtitle").css("animation", "fadeInDown 1000ms ease 500ms 1 both");
            $("#gifImg").css("animation", "rollIn 1500ms ease 2000ms 1 both");
            $("#fourremark div").html("<span class='num'>2012</span><span class='symbol'>，</span>一起走过了大学年华");
            $("#fourremark").css("animation", "rollIn 1500ms ease 2000ms 1 both");
            awaitFunc(function () {
                $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
            }, 6500);

            //2013-新郎毕业
            awaitFunc(function () {
                $("#gifImg").attr("src", "weeding/images/2013.gif");
                $("#gifImg").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                $("#fourremark div").html("<span class='num'>2013</span><span class='symbol'>，</span>新郎大学毕业了");
                $("#fourremark").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                awaitFunc(function () {
                    $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                    $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                }, 6500);
            }, 8500);

            //2014-新娘也毕业了
            awaitFunc(function () {
                $("#gifImg").attr("src", "weeding/images/2014.gif");
                $("#gifImg").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                $("#fourremark div").html("<span class='num'>2014</span><span class='symbol'>，</span>新娘也大学毕业了");
                $("#fourremark").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                awaitFunc(function () {
                    $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                    $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                }, 6500);
            }, 17500);


            awaitFunc(function () {
                $("#fourtitle").css("animation", "fadeOutUp 1000ms ease 1000ms 1 both");
            }, 25500);

        }, 10500);

        //相知
        awaitFunc(function () {
            //2015-初入职场，也辛苦也快乐
            $("#fourtitle div").html("相知");
            $("#fourtitle").css("animation", "fadeInDown 1000ms ease 1000ms 1 both");
            $("#gifImg").attr("src", "weeding/images/2015.gif");
            $("#gifImg").css("animation", "rollIn 1500ms ease 2000ms 1 both");
            $("#fourremark div").html("<span class='num'>2015</span><span class='symbol'>，</span>初入职场<span class='symbol'>，</span>我们辛苦也快乐");
            $("#fourremark").css("animation", "rollIn 1500ms ease 2000ms 1 both");
            awaitFunc(function () {
                $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
            }, 6000);

            //2016-努力学习，共同进步
            awaitFunc(function () {
                $("#gifImg").attr("src", "weeding/images/2016.gif");
                $("#gifImg").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                $("#fourremark div").html("<span class='num'>2016</span><span class='symbol'>，</span>努力学习<span class='symbol'>，</span>我们一起进步");
                $("#fourremark").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                awaitFunc(function () {
                    $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                    $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                }, 6500);
            }, 8500);

            //2017-有目标，有期望
            awaitFunc(function () {
                $("#gifImg").attr("src", "weeding/images/2017.gif");
                $("#gifImg").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                $("#fourremark div").html("<span class='num'>2017</span><span class='symbol'>，</span>新的目标<span class='symbol'>，</span>新的期望");
                $("#fourremark").css("animation", "rollIn 1500ms ease 1000ms 1 both");
                awaitFunc(function () {
                    $("#gifImg").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                    $("#fourremark").css("animation", "rollOut 1500ms ease 1000ms 1 both");
                }, 6500);
            }, 17500);


            //awaitFunc(function () {
            //    $("#fourtitle").css("animation", "fadeOutUp 1000ms ease 1000ms 1 both");
            //}, 26500);

        }, 38500);

        //汇总-图片墙
        awaitFunc(function () {
            var imgWall = $("#imgWall");
            var imgWallWidth = imgWall.width();
            var imgWallHeight = imgWall.height();
            var rate = (imgWallHeight / 2) / (imgWallWidth * 0.3);
            var imgWidth = 0;
            var imgHeight = 0;
            if (rate == 1.5 || rate > 1.5) {
                imgWidth = imgWallWidth * 0.3;
                imgHeight = imgWidth * 1.5;
            } else if (rate < 1.5) {
                imgHeight = imgWallHeight / 2;
                imgWidth = imgHeight * 2 / 3;
            }
            SaveWebLog("计算结果：rate-" + rate + " imgwallheight:" + imgWallHeight + " imgwallwidth:" + imgWallWidth + "imgheight:" + imgHeight + " imgwidth:" + imgWidth);

            imgWall.html("");//清空元素
            var img2011 =
                $('<img id="img2011" class="am-circle am-img-thumbnail" style="width: ' + imgWidth + 'px;height:' + imgHeight + 'px" src="weeding/images/2011.gif" />');
            var img2012 =
                $('<img id="img2012" class="am-circle am-img-thumbnail" style="width: ' + imgWidth + 'px;height:' + imgHeight + 'px" src="weeding/images/2012.gif" />');
            var img2013 =
                $('<img id="img2013" class="am-circle am-img-thumbnail" style="width: ' + imgWidth + 'px;height:' + imgHeight + 'px" src="weeding/images/2013.gif" />');
            var img2014 =
                $('<img id="img2014" class="am-circle am-img-thumbnail" style="width: ' + imgWidth + 'px;height:' + imgHeight + 'px" src="weeding/images/2014.gif" />');
            var img2015 =
                $('<img id="img2015" class="am-circle am-img-thumbnail" style="width: ' + imgWidth + 'px;height:' + imgHeight + 'px" src="weeding/images/2015.gif" />');
            var img2016 =
                $('<img id="img2016" class="am-circle am-img-thumbnail" style="width: ' + imgWidth + 'px;height:' + imgHeight + 'px" src="weeding/images/2016.gif" />');
            imgWall.append(img2011);
            imgWall.append(img2012);
            imgWall.append(img2013);
            imgWall.append(img2014);
            imgWall.append(img2015);
            imgWall.append(img2016);
            $("#img2011").css("animation", "flipInX 1500ms ease 1000ms 1 both");
            $("#img2012").css("animation", "flipInX 1500ms ease 1300ms 1 both");
            $("#img2013").css("animation", "flipInX 1500ms ease 1600ms 1 both");
            $("#img2014").css("animation", "flipInX 1500ms ease 1900ms 1 both");
            $("#img2015").css("animation", "flipInX 1500ms ease 2200ms 1 both");
            $("#img2016").css("animation", "flipInX 1500ms ease 2500ms 1 both");
            $("#fourremark div").html("一生<span class='symbol'>，</span>有你<span class='symbol'>，</span>太短<br>一生<span class='symbol'>，</span>没你<span class='symbol'>，</span>太长");
            $("#fourremark").css("animation", "fadeIn 5000ms ease 4000ms 1 both");
            awaitFunc(function () {
                $("#fourtitle").css("animation", "fadeOutUp 1000ms ease 1000ms 1 both");
                $("#img2011").css("animation", "flipOutX 1500ms ease 1000ms 1 both");
                $("#img2012").css("animation", "flipOutX 1500ms ease 1300ms 1 both");
                $("#img2013").css("animation", "flipOutX 1500ms ease 1600ms 1 both");
                $("#img2014").css("animation", "flipOutX 1500ms ease 1900ms 1 both");
                $("#img2015").css("animation", "flipOutX 1500ms ease 2200ms 1 both");
                $("#img2016").css("animation", "flipOutX 1500ms ease 2500ms 1 both");
                $("#fourremark").css("animation", "fadeOut 1500ms ease 4000ms 1 both");
            }, 11000);
        }, 65000);


        mainDiv.css("visibility", "visible");
        awaitFunc(function () {
            if (nextAction != null) {
                nextAction();
            } else {
                obRunState.fourScreenIsRun = false;
            }
        },
            81500);
    },
        700);
}

function fourDisapear() {
    SaveWebLog("执行fourDisapear");

    $("#fourbottomleftflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");
    $("#fourbottommidflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");
    $("#fourbottomrightflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");


    awaitFunc(function () {
        obRunState.fourScreenIsRun = false;
        SaveWebLog("fourAction结束");
    }, 2500);
}

function thirdAction(nextAction) {

    thirdTimeId = setInterval(function () {
        if (obRunState.secondScreenIsRun) {
            return;
        }
        SaveWebLog("执行thirdAction");
        clearInterval(thirdTimeId);

        var mainDiv = $("#mainDiv");
        mainDiv.css("visibility", "hidden");
        mainDiv.html($("#thirdScreen").html());
        //让头像垂直居中
        centerOb("yangjianimg");
        $("#yangjianimg").css("animation", "fadeInLeft 1000ms ease 1000ms 1 both");
        //先飞入，再转圈
        awaitFunc(function () {
            $("#yangjianimg").css("animation", "spin 13s infinite linear");
        },
            2000);
        {
            var temp = $("#thirdtopflower");
            temp.css("width", "70%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "15%");
            temp.css("top", "2%");
            temp.css("animation", "fadeInUp 2000ms ease 2000ms 1 both");
        }
        {
            $("#thirdbottomflower div").css("transform", "rotateZ(180deg)");
            var temp = $("#thirdbottomflower");
            temp.css("width", "70%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "15%");
            temp.css("top", "38%");
            temp.css("animation", "fadeInDown 2000ms ease 2000ms 1 both");
        }

        $("#zuanqianyangjian").css("animation", "fadeIn 1000ms ease 4000ms 1 both");

        {
            var temp = $("#thirdbottomleftflower");
            temp.css("width", "60%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "-10%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 5000ms 1 both");
        }

        {
            var temp = $("#thirdbottomrightflower");
            temp.css("width", "60%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "45%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 5000ms 1 both");
        }

        mainDiv.css("visibility", "visible");
        awaitFunc(function () {
            if (nextAction != null) {
                nextAction();
            } else {
                obRunState.thirdScreenIsRun = false;
            }
        },
            6980);
    },
        700);
}

function thirdActionDisapear() {
    SaveWebLog("执行thirdActionDisapear");

    $("#thirdtopflower").css("animation", "fadeOutUp 1500ms ease 1000ms 1 both");
    $("#thirdbottomflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");

    $("#thirdbottomleftflower").css("animation", "fadeOutDown 1500ms ease 2500ms 1 both");
    $("#thirdbottomrightflower").css("animation", "fadeOutDown 1500ms ease 2500ms 1 both");

    $("#zuanqianyangjian").css("animation", "bounceOut 2000ms ease 4000ms 1 both");

    awaitFunc(function () {
        $("#yangjianimg").css("animation", "zoomOut 2000ms ease 0ms 1 both");
    }, 8000);

    awaitFunc(function () {
        obRunState.thirdScreenIsRun = false;
        SaveWebLog("thirdAction结束");
    }, 8000);
}

function secondActionDisapear() {
    SaveWebLog("执行secondActionDisapear");

    $("#secondtopflower").css("animation", "fadeOutUp 1500ms ease 1000ms 1 both");
    $("#secondbottomflower").css("animation", "fadeOutDown 1500ms ease 1000ms 1 both");

    $("#secondbottomleftflower").css("animation", "fadeOutDown 1500ms ease 2500ms 1 both");
    $("#secondbottomrightflower").css("animation", "fadeOutDown 1500ms ease 2500ms 1 both");

    $("#maomeiruhua").css("animation", "bounceOut 2000ms ease 4000ms 1 both");

    awaitFunc(function () {
        $("#zhangyuimg").css("animation", "zoomOut 2000ms ease 0ms 1 both");
    }, 8000);

    awaitFunc(function () {
        obRunState.secondScreenIsRun = false;
        SaveWebLog("secondAction结束");
    }, 9000);
}

function secondAction(nextAction) {

    secondTimeId = setInterval(function () {
        if (obRunState.firstScreenIsRun) {
            return;
        }
        clearInterval(secondTimeId);
        SaveWebLog("执行secondAction");
        var mainDiv = $("#mainDiv");
        mainDiv.css("visibility", "hidden");
        mainDiv.html($("#secondScreen").html());
        //让头像垂直居中
        centerOb("zhangyuimg");
        $("#zhangyuimg").css("animation", "fadeInLeft 1000ms ease 1000ms 1 both");
        //先飞入，再转圈
        awaitFunc(function () {
            $("#zhangyuimg").css("animation", "spin 13s infinite linear");
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

        $("#maomeiruhua").css("animation", "fadeIn 1000ms ease 4000ms 1 both");

        {
            var temp = $("#secondbottomleftflower");
            temp.css("width", "60%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "-10%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 5000ms 1 both");
        }

        {
            var temp = $("#secondbottomrightflower");
            temp.css("width", "60%"); //宽度70%
            temp.css("height", "20%"); //高度20%
            temp.css("left", "45%");
            temp.css("bottom", "-5%");
            temp.css("animation", "fadeInUp 1000ms ease 5000ms 1 both");
        }

        mainDiv.css("visibility", "visible");
        awaitFunc(function () {
            if (nextAction != null) {
                nextAction();
            } else {
                obRunState.secondScreenIsRun = false;
            }
        },
            6980);
    },
        500);

}

function firstActionDisappear() {
    
    SaveWebLog("执行firstActionDisappear");

    $("#topflower").css("animation", "fadeOutUp 1000ms ease 1500ms 1 both");
    $("#topleftbesideflower").css("animation", "fadeOutLeft 1000ms ease 1700ms 1 both");
    $("#toprightbesideflower").css("animation", "fadeOutRight 1000ms ease 1700ms 1 both");

    $("#left").css("animation", "fadeOutLeft 1000ms ease 1900ms 1 both");
    $("#right").css("animation", "fadeOutRight 1000ms ease 1900ms 1 both");

    $("#leftbottom").css("animation", "fadeOutLeft 1000ms ease 2100ms 1 both");
    $("#rightbottom").css("animation", "fadeOutRight 1000ms ease 2100ms 1 both");

    $("#bottomleft").css("animation", "fadeOutLeft 1000ms ease 2300ms 1 both");
    $("#bottomright").css("animation", "fadeOutRight 1000ms ease 2300ms 1 both");

    $("#bottom").css("animation", "fadeOutDown 1000ms ease 2500ms 1 both");

    $("#yaoqing").css("animation", "bounceOut 1000ms ease 3500ms 1 both");
    $("#name").css("animation", "hinge 2000ms ease 4500ms 1 both");
    $("#hunli").css("animation", "bounceOutRight 2000ms ease 6500ms 1 both");
    $("#firstimg").css("animation", "bounceOutRight 2000ms ease 6700ms 1 both");
    $("#time").css("animation", "bounceOutRight 2000ms ease 6900ms 1 both");
    $("#address").css("animation", "bounceOutRight 2000ms ease 7000ms 1 both");

    awaitFunc(function () {
        //标识动画已经结束
        obRunState.firstScreenIsRun = false;
        SaveWebLog("firstAction结束");
    }, 8000);
}

function firstAction(nextAction) {
    SaveWebLog("执行firstAction");
    obRunState.firstScreenIsRun = true;
    var mainDiv = $("#mainDiv");
    mainDiv.html($("#firstScreen").html());


    centerOb("firstimg");

    //调整花束位置
    //右边花需要翻转
    $(".rightflower div").css("transform", "rotateY(180deg)");
    {
        var temp = $("#topflower");

        temp.css("width", "70%"); //宽度70%
        temp.css("height", "20%"); //高度20%
        temp.css("left", "15%");
        temp.css("top", "-5%");
        temp.css("animation", "zoomIn 1000ms ease 1000ms 1 both");
    }
    {
        var temp = $("#topleftbesideflower");
        temp.css("width", "40%");
        temp.css("height", "30%");
        temp.css("left", "-15%");
        temp.css("top", "-5%");
        temp.css("animation", "fadeInLeft 1000ms ease 1300ms 1 both");
    }
    {
        var temp = $("#toprightbesideflower");
        temp.css("width", "40%");
        temp.css("height", "30%");
        temp.css("right", "-15%");
        temp.css("top", "-5%");
        temp.css("animation", "fadeInRight 1000ms ease 1300ms 1 both");
    }
    {
        var temp = $("#left");
        temp.css("width", "30%");
        temp.css("height", "40%");
        temp.css("left", "-15%");
        temp.css("top", "20%");
        temp.css("animation", "fadeInLeft 1000ms ease 1600ms 1 both");
    }

    {
        var temp = $("#right");
        temp.css("width", "30%");
        temp.css("height", "40%");
        temp.css("right", "-15%");
        temp.css("top", "20%");
        temp.css("animation", "fadeInRight 1000ms ease 1600ms 1 both");
    }
    {
        var temp = $("#leftbottom");
        temp.css("width", "30%");
        temp.css("height", "30%");
        temp.css("left", "-15%");
        temp.css("top", "55%");
        temp.css("animation", "fadeInLeft 1000ms ease 2000ms 1 both");
    }

    {
        var temp = $("#rightbottom");
        temp.css("width", "30%");
        temp.css("height", "30%");
        temp.css("right", "-15%");
        temp.css("top", "55%");
        temp.css("animation", "fadeInRight 1000ms ease 2000ms 1 both");
    }
    {
        var temp = $("#bottomleft");
        temp.css("width", "30%");
        temp.css("height", "30%");
        temp.css("left", "-15%");
        temp.css("bottom", "-5%");
        temp.css("animation", " fadeInLeft 1000ms ease 2300ms 1 both");
    }
    {
        var temp = $("#bottomright");
        temp.css("width", "30%");
        temp.css("height", "30%");
        temp.css("right", "-15%");
        temp.css("bottom", "-5%");
        temp.css("animation", " fadeInRight 1000ms ease 2300ms 1 both");
    }
    {
        var temp = $("#bottom");
        temp.css("width", "70%");
        temp.css("height", "30%");
        temp.css("left", "15%");
        temp.css("bottom", "-15%");
        temp.css("animation", "zoomIn 1000ms ease 2500ms 1 both");
    }
    {
        $("#yaoqing").css("animation", "fadeIn 1000ms ease 4000ms 1 both");
        $("#hunli").css("visibility", "hidden");
        $("#address").css("visibility", "hidden");
        $("#time").css("visibility", "hidden");
        $("#firstimg").css("visibility", "hidden");

        var nextFunc = function () {
            console.log("结束打字" + new Date());
            $("#hunli").css("animation", "fadeIn 1000ms ease 1000ms 1 both");
            $("#firstimg").css("animation", "zoomIn 1000ms ease 2000ms 1 both");
            $("#time").css("animation", "fadeIn 1000ms ease 3000ms 1 both");
            $("#address").css("animation", "fadeIn 1000ms ease 4000ms 1 both");

            $("#hunli").css("visibility", "visible");
            $("#time").css("visibility", "visible");
            $("#address").css("visibility", "visible");
            $("#firstimg").css("visibility", "visible");
            if (nextAction != null) {
                awaitFunc(nextAction, 5000);
            }
        }

        awaitFunc(function () {
            console.log("开始打字" + new Date() + "速度：" + firstScreenTypeSpeed);
            typedWord(null, nameInfos, "relaceName", nextFunc, firstScreenTypeSpeed, firstScreenTypeSpeed);
        },
            5000);
        mainDiv.css("visibility", "visible");
    }

    //$(function() {
    //    //per10Height = $("#mainPage").height();//640
    //    //SaveWebLog(per10Height);
    //    //SaveWebLog($("#mainPage").width());//360
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

}

//再次播放
function Replay() {
    SaveWebLog("再次播放");
    location.reload();
}

var smsStatus = false;
//短信发送
function SendSms(name) {
    if (smsStatus) {
        return;
    }
    smsStatus = true;
    $("#sendsms").html("正在设置");
    var ob = new Object();
    ob.Name = name;
    AjaxHelper('weeding/Home/SendSms', ob,
        function (result) {
            console.log(result);
            if (result == "success") {
                $("#sendsms").html("提醒成功");
            } else {
                $("#sendsms").html("设置失败");
                smsStatus = false;
            }
        });
}

//本地的经纬度信息
var locallng = 0;
var locallat = 0;

//查看导航
function ViewNavigate() {
    if (obRunState.sixScreenIsRun) {
        $("#viewnavigate").html("稍等一下");
        return;
    }

    var baiduMap = $("#baiduMap");
    var navigatemodal = $("#navigatemodal");
    var navigatecontent = $("#navigatecontent");
    var baiduresult = $("#baiduresult");
    //console.log("navigatemodalWidth:" + navigatemodal.width() + "navigatemodalheight:" + navigatemodal.height());

    baiduMap.css("height", navigatemodal.width()*0.8+"px");
    baiduMap.css("width", navigatemodal.width() * 0.8 + "px");
    //baiduresult.css("height", navigatemodal.width() * 0.4 + "px");
    baiduresult.css("width", navigatemodal.width() * 0.8 + "px");
    navigatecontent.css("margin-left", navigatemodal.width() * 0.1 + "px");
    navigatecontent.css("margin-right", navigatemodal.width() * 0.1 + "px");

    var geolocation = new BMap.Geolocation();
    geolocation.getCurrentPosition(function(r) {
            if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                var mk = new BMap.Marker(r.point);
                var map = new BMap.Map("baiduMap");
                map.addOverlay(mk);
                map.panTo(r.point);
                locallng = r.point.lng;
                locallat = r.point.lat;
                console.log("定位信息：" + locallng);
                var p1 = new BMap.Point(locallng, locallat);
                var p2 = new BMap.Point(103.63899, 29.419018);
                
                var searchComplete = function (results) {
                    if (driving.getStatus() != BMAP_STATUS_SUCCESS) {
                        return;
                    }
                    var plan = results.getPlan(0);
                    $("#duartion").html(plan.getDuration(true));
                    $("#distance").html(plan.getDistance(true));
                    //output += plan.getDuration(true) + "\n";                //获取时间
                    //output += "总路程为：";
                    //output += plan.getDistance(true) + "\n";             //获取距离
                }
                var driving = new BMap.DrivingRoute(map,
                    {
                        renderOptions: { map: map, autoViewport: true },
                        onSearchComplete: searchComplete,
                        //onPolylinesSet: function () {
                        //    setTimeout(function () { alert(output) }, "1000");
                        //}
                    }
                );
                driving.search(p1, p2); 
                
                var navigate = $("#navigate");
                navigate.modal();
            } else {
                $("#baiduresult").html("很抱歉，定位失败了，无法发起导航");
                console.log('百度定图定位失败' + this.getStatus());
            }
        },
        { enableHighAccuracy: true });
}

