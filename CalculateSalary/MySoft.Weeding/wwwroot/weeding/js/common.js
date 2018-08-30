//通用方法

//延时方法，不阻塞主线程
function awaitFunc(func, timeout) {
    var timeId = setTimeout(function () {
        if (func != null) {
            func();
        }

        clearTimeout(timeId);
    },
        timeout);
}

//用于文字墙效果
function typeImgWord(elementId, strArr, imgArr, nextFunc) {
    var ob = $("#" + elementId);
    if (ob == null) {
        SaveWebLog("没有元素：" + elementId);
        return;
    }
    ob.html("");
    ob.css("padding", "0");
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    SaveWebLog("元素宽度：" + width + " 高度：" + height);

    var strHeight = strArr.length;
    var strWidth = strArr[0].length;

    var rowHeight = height / strHeight;
    var rowWidth = width / strWidth;


    //调整为方形
    var finalSize = 0;
    var obfinalHeight = 0;
    var obfinalWidth = 0;
    var finalAdjustWidth = 0;
    var finalAdjustHeight = 0;
    var setWidth = 0;
    var setHeight = 0;
    if (rowHeight > rowWidth) {

        finalSize = rowWidth.toFixed(1);

        //上下填充
        var adjustTopMargin = ((rowHeight - finalSize) * strHeight / 2).toFixed(1);
        $(ob).css("padding-top", adjustTopMargin + "px");
        $(ob).css("padding-bottom", adjustTopMargin + "px");
        obfinalHeight = finalSize * strHeight;
        obfinalWidth = finalSize * strWidth;

        setWidth = obfinalWidth;
        setHeight = obfinalHeight + adjustTopMargin * 2;

    } else {

        finalSize = rowHeight.toFixed(1);
        //左右填充
        var adjustLeftMargin = ((rowWidth - finalSize) * strWidth / 2).toFixed(1);
        $(ob).css("padding-left", adjustLeftMargin + "px");
        $(ob).css("padding-right", adjustLeftMargin + "px");
        obfinalHeight = finalSize * strHeight;
        obfinalWidth = finalSize * strWidth;
        setWidth = obfinalWidth + adjustLeftMargin * 2;
        setHeight = obfinalHeight;
    }
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    SaveWebLog("元素改前的宽度：" + width + " 改前的高度：" + height);
    $(ob).css("height", setHeight + "px");
    $(ob).css("width", setWidth + "px");
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    SaveWebLog("元素改后的宽度：" + width + " 改后的高度：" + height);

    var tempClassName = elementId + "Temp";

    for (var i = 0; i < strHeight; i++) {
        var row = $(
            '<div class="am-g" style="height: ' + finalSize + 'px;margin-left:0px;margin-right:0px;"></div>');
        for (var j = 0; j < strWidth; j++) {
            var col = $('<div class="am-u-sm-1 ' + tempClassName + ' " style="height:' + finalSize + 'px;width:' + finalSize + 'px;padding-left:0px;padding-right:0px;float:left"></div>');
            row.append(col);
        }
        ob.append(row);//这里添加会导致背景图变化
    }



    var arr = [];
    strArr.forEach(function (v, i) {
        for (var j = 0; j < v.length; j++) {
            if (v[j] === 'y') {
                arr.push(i * strWidth + j);
            }
        }
    });

    var textArr = [].concat(arr);
    var imgArrTemp = [].concat(arr);

    var divEles = $('.' + tempClassName);
    var imgEles = $('#' + elementId + ' img');




    var tempList = [];
    //先替换为红色背景块
    var timer = setInterval(function () {


        var length = textArr.length;
        var showNumber = textArr.splice(Math.random() * length, 1);
        //直接替换为背景图
        $(divEles[showNumber]).css("background-color", "#DC143C");

        //$(divEles[showNumber]).css("background", "url(" + imgArr[Math.floor(Math.random() * imgArr.length)] + ") no-repeat");
        $(divEles[showNumber]).css("background-size", "cover");
        //tempList.push((divEles[showNumber]));

        if (!textArr.length) {
            clearInterval(timer);
            //if (imgArr != null) {
            //    showImg();
            //} else {
            //    if (nextFunc != null) {
            //        awaitFunc(function () { nextFunc(); }, 1000);
            //    }
            //}
            if (nextFunc != null) {
                awaitFunc(function () { nextFunc(); }, 1000);
            }
        }
    }, 0);

    //var imgSrc = Array.from({ length: 33 }, (v, i) => i);

    //再显示为图片
    function showImg() {
        var imgTimer = setInterval(function () {
            var length = imgArrTemp.length;
            var showNumber = imgArrTemp.splice(Math.random() * length, 1);

            $(divEles[showNumber]).css("background", "url(" + imgArr[Math.floor(Math.random() * imgArr.length)] + ") no-repeat");
            $(divEles[showNumber]).css("background-size", "cover");

            if (imgArrTemp.length === 0) {
                clearInterval(imgTimer);
                //$(".testcolor").addClass('animated rollOut');
                if (nextFunc != null) {
                    awaitFunc(function () { nextFunc(); }, 1000);
                }
            }
        }, 0);
    }


    //


}


//用于图片墙效果
function typeImg(elementId, strArr, nextFunc) {
    var ob = $("#" + elementId);
    if (ob == null) {
        SaveWebLog("没有元素：" + elementId);
        return;
    }
    ob.html("");
    ob.css("padding", "0");
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    SaveWebLog("元素宽度：" + width + " 高度：" + height);

    var strHeight = strArr.length;
    var strWidth = strArr[0].split('#').length;
    SaveWebLog("字符长度：" + strHeight + "字符宽度：" + strWidth);
    var rowHeight = height / strHeight;
    var rowWidth = width / strWidth;

    //调整为方形
    var finalSize = 0;
    var obfinalHeight = 0;
    var obfinalWidth = 0;
    var finalAdjustWidth = 0;
    var finalAdjustHeight = 0;
    var setWidth = 0;
    var setHeight = 0;
    if (rowHeight > rowWidth) {

        //finalSize = Math.ceil(rowWidth);
        //if (finalSize == 0) finalSize = rowWidth;
        finalSize = rowWidth.toFixed(1);

        //上下填充
        var adjustTopMargin = ((rowHeight - finalSize) * strHeight / 2).toFixed(1);
        $(ob).css("padding-top", adjustTopMargin + "px");
        $(ob).css("padding-bottom", adjustTopMargin + "px");
        obfinalHeight = finalSize * strHeight;
        obfinalWidth = finalSize * strWidth;
        //finalAdjustWidth = ((width - obfinalWidth) / 2).toFixed(1);
        //$(ob).css("padding-left", finalAdjustWidth + "px");
        //$(ob).css("padding-right", finalAdjustWidth + "px");
        setWidth = obfinalWidth;
        setHeight = obfinalHeight + adjustTopMargin * 2;

    } else {
        //finalSize = Math.ceil(rowHeight);
        //if (finalSize == 0) finalSize = rowHeight;
        finalSize = rowHeight.toFixed(1);
        //左右填充
        var adjustLeftMargin = ((rowWidth - finalSize) * strWidth / 2).toFixed(1);
        $(ob).css("padding-left", adjustLeftMargin + "px");
        $(ob).css("padding-right", adjustLeftMargin + "px");
        obfinalHeight = finalSize * strHeight;
        obfinalWidth = finalSize * strWidth;

        //finalAdjustHeight = ((height - obfinalHeight) / 2).toFixed(1);
        ////if (temp < 0) {
        ////    finalAdjustHeight = Math.floor(temp);
        ////} else {
        ////    finalAdjustHeight = Math.ceil(temp);
        ////}

        //$(ob).css("padding-top", finalAdjustHeight + "px");
        //$(ob).css("padding-bottom", finalAdjustHeight + "px");
        setWidth = obfinalWidth + adjustLeftMargin * 2;
        setHeight = obfinalHeight;
    }
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    SaveWebLog("元素改前的宽度：" + width + " 改前的高度：" + height);
    $(ob).css("height", setHeight + "px");
    $(ob).css("width", setWidth + "px");
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    SaveWebLog("元素改后的宽度：" + width + " 改后的高度：" + height);

    var tempClassName = elementId + "Temp";

    for (var i = 0; i < strHeight; i++) {
        var row = $(
            '<div class="am-g" style="height: ' + finalSize + 'px;margin-left:0px;margin-right:0px;float:left"></div>');
        for (var j = 0; j < strWidth; j++) {
            var col = $('<div class="am-u-sm-1 ' + tempClassName + '  " style="height:' + finalSize + 'px;width:' + finalSize + 'px;padding-left:0px;padding-right:0px;background-color:' + strArr[i].split("#")[j] + ';"></div>');
            row.append(col);
        }
        ob.append(row);
    }

    if (nextFunc != null) {
        awaitFunc(function () { nextFunc(); }, 1000);
    }

    //var arr = [];
    //strArr.forEach(function (v, i) {
    //    for (var j = 0; j < v.length; j++) {
    //        if (v[j] === '0') {
    //            arr.push(i * strWidth + j);
    //        }
    //    }
    //});

    //var textArr = [].concat(arr);
    //var imgArrTemp = [].concat(arr);

    //var divEles = $('.' + tempClassName);
    //var imgEles = $('#' + elementId + ' img');

    //var tempList = [];
    ////先替换为红色背景块
    //var timer = setInterval(function () {


    //    var length = textArr.length;
    //    var showNumber = textArr.splice(Math.random() * length, 1);
    //    $(divEles[showNumber]).css("background-color", "lightslategray");
    //    tempList.push((divEles[showNumber]));
    //    //imgEles[showNumber].src = imgArr[Math.floor(Math.random() * imgArr.length)];
    //    //$(imgEles[showNumber]).css("height", '100%');
    //    //$(imgEles[showNumber]).css("width", '100%');



    //    if (!textArr.length) {
    //        clearInterval(timer);
    //        if (imgArr != null) {
    //            showImg();
    //        } else {
    //            if (nextFunc != null) {
    //                awaitFunc(function () { nextFunc(); }, 1000);
    //            }
    //        }


    //        //awaitFunc(function () {
    //        //    $(tempList).css("background", "url(" + imgArr[Math.floor(Math.random() * imgArr.length)] + ") no-repeat");
    //        //    $(tempList).css("background-size", "cover");
    //        //}, 1000);

    //    }
    //}, 0);

    ////var imgSrc = Array.from({ length: 33 }, (v, i) => i);

    ////再显示为图片
    //function showImg() {
    //    var imgTimer = setInterval(function () {
    //        var length = imgArrTemp.length;
    //        var showNumber = imgArrTemp.splice(Math.random() * length, 1);

    //        $(divEles[showNumber]).css("background", "url(" + imgArr[Math.floor(Math.random() * imgArr.length)] + ") no-repeat");
    //        $(divEles[showNumber]).css("background-size", "cover");

    //        if (imgArrTemp.length === 0) {
    //            clearInterval(imgTimer);
    //            //$(".testcolor").addClass('animated rollOut');
    //            if (nextFunc != null) {
    //                awaitFunc(function () { nextFunc(); }, 1000);
    //            }
    //        }
    //    }, 0);
    //}


    //


}

//动态输出文字
//sourceId-来源id strarray-如果没有sourceId未提供，就提供拼接好的文本数组 
//output-目标输出id nextfun-后续动作
function typedWord(sourceId, strarray, outputId, nextfun, typeSpeed, delSpeed, typedFunc) {
    if (typeSpeed == null) {
        typeSpeed = 500;
    }
    if (delSpeed == null) {
        delSpeed = 300;
    }
    var desstr = [];
    if (sourceId != null) {
        //剔除多余空格，但保留样式之间的空格
        desstr.push($("#" + sourceId).html().replace(/\s{2,1000}/g, ""));
    } else {
        desstr = strarray;
    };
    var temp1 = new Typed("#" + outputId, {
        //strings: ['<img class="am-radius" alt="140*140" src="http://s.amazeui.org/media/i/demos/bw-2014-06-19.jpg?imageView/1/w/1000/h/1000/q/80" width="140" height="140" />'],
        strings: desstr,
        //stringsElement: '#typed-strings',
        typeSpeed: typeSpeed,//打字延迟
        backSpeed: delSpeed,//删除延迟
        //混序，会使句子以乱序输出
        //shuffle: true,

        //控制删除的效果
        //fadeOut: true,
        //fadeOutClass: 'typed-fade-out',
        //fadeOutDelay: 500,

        //光标
        //cursorChar: '|',
        showCursor: false,
        //arrayPos是string数组的Index
        onStringTyped: (arrayPos, self) => {
            if (typedFunc != null) {
                typedFunc(arrayPos, self);
            }
        },
        onComplete: function (self) {
            if (nextfun != null) {
                nextfun();
            }

        }
    });
}


function ajaxTypeWord(str, fontSize, fontName, outPutId, imgList, nextFunc) {
    var ob = new Object();
    ob.Text = str;
    ob.FontSize = fontSize;
    ob.fontName = fontName;

    AjaxHelper('weeding/Home/GetWordArr',
        ob,
        function (result) {
            var jsonResult = jQuery.parseJSON(result);
            SaveWebLog(jsonResult.result);
            typeImgWord(outPutId, jsonResult.result, imgList, nextFunc);
        });
}

function SaveWebLog(str) {
    var ob = new Object();
    ob.Name = nameInfos[0];
    ob.LogMsg = str;
    ob.Time = GetTimeStr();
    console.log(str);
    ajaxSendLog(ob);
}

function ajaxSendLog(ob) {
    AjaxHelper('weeding/Home/WebLog',
        ob,
        function (result) {
            console.log(result);
        });
}

function GetTimeStr() {
    var date = new Date();//实例一个时间对象；
    var year = date.getFullYear();//获取系统的年；
    var month = date.getMonth() + 1;//获取系统月份，由于月份是从0开始计算，所以要加1
    var day = date.getDate(); //获取系统日
    var hour = date.getHours();//获取系统时间
    var minute = date.getMinutes(); //分
    var second = date.getSeconds();//秒
    return (year + '年' + month + '月' + day + '日 ' + hour + ':' + minute + ':' + second);
}


function ajaxTypeImg(imgName, outPutId, width, height, nextFunc) {
    var ob = new Object();
    ob.ImgName = imgName;
    ob.Width = width;
    ob.Height = height;

    AjaxHelper('weeding/Home/GetImgArr',
        ob,
        function (result) {
            var jsonResult = jQuery.parseJSON(result);
            SaveWebLog(jsonResult.result);
            typeImg(outPutId, jsonResult.result, nextFunc);
        });
}

//Ajax通用
function AjaxHelper(url, data, successfun) {
    $.ajax({
        type: "post",
        url: url,
        data: data,
        success: successfun,
        error: function () {
            console.log("服务器请求异常");
        }
    });
}

//动态展示文字字模-蛋疼，只能这么写了
function SeqTypeImgWord(str, fontSizeArr, fontName, elementId, imgList) {
    var fun1, fun2, fun3, fun4, fun5, fun6, fun7, fun8, fun9, fun10;
    if (fontSizeArr.length > 9) {
        var fun9 = function () {
            ajaxTypeWord(str, fontSizeArr[9], fontName, elementId, imgList, null);
        }
    }
    if (fontSizeArr.length > 8) {
        var fun8 = function () {
            ajaxTypeWord(str, fontSizeArr[8], fontName, elementId, imgList, fun9);
        }
    }
    if (fontSizeArr.length > 7) {
        var fun7 = function () {
            ajaxTypeWord(str, fontSizeArr[7], fontName, elementId, imgList, fun8);
        }
    }
    if (fontSizeArr.length > 6) {
        var fun6 = function () {
            ajaxTypeWord(str, fontSizeArr[6], fontName, elementId, imgList, fun7);
        }
    }
    if (fontSizeArr.length > 5) {
        var fun5 = function () {
            ajaxTypeWord(str, fontSizeArr[5], fontName, elementId, imgList, fun6);
        }
    }
    if (fontSizeArr.length > 4) {
        var fun4 = function () {
            ajaxTypeWord(str, fontSizeArr[4], fontName, elementId, imgList, fun5);
        }
    }

    if (fontSizeArr.length > 3) {
        var fun3 = function () {
            ajaxTypeWord(str, fontSizeArr[3], fontName, elementId, imgList, fun4);
        }
    }

    if (fontSizeArr.length > 2) {
        var fun2 = function () {
            ajaxTypeWord(str, fontSizeArr[2], fontName, elementId, imgList, fun3);
        }
    }
    if (fontSizeArr.length > 1) {
        var fun1 = function () {
            ajaxTypeWord(str, fontSizeArr[1], fontName, elementId, imgList, fun2);
        }
    }

    ajaxTypeWord(str, fontSizeArr[0], fontName, elementId, imgList, fun1);

}

function centerOb(elementId) {
    //让元素左右垂直居中
    var ob = $("#" + elementId);
    if (ob == null) return;
    parent = ob.parent();
    margintop = ((parent.height() - ob.height()) / 2).toFixed(2);
    marginleft = ((parent.width() - ob.width()) / 2).toFixed(2);
    if (margintop < 0) {
        ob.css("height", parent.height());
    } else {
        ob.css("margin-top", margintop + "px");
        ob.css("margin-bottom", margintop + "px");
    }

    if (marginleft < 0) {
        ob.css("width", parent.width());
    } else {
        ob.css("margin-left", marginleft + "px");
        ob.css("margin-right", marginleft + "px");
    }

}