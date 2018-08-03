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
function typeImgWord(elementId, strArr, imgArr,nextFunc) {
    var ob = $("#" + elementId);
    if (ob == null) {
        console.log("没有元素：" + elementId);
        return;
    }
    ob.html("");
    ob.css("padding", "0");
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    console.log("元素宽度：" + width + " 高度：" + height);

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
    if (rowHeight > rowWidth) {

        finalSize = Math.floor(rowWidth);
        if (finalSize == 0) finalSize = rowWidth;

        //上下填充
        var adjustTopMargin = Math.floor((rowHeight - finalSize) * strHeight / 2);
        $(ob).css("padding-top", adjustTopMargin);
        $(ob).css("padding-bottom", adjustTopMargin);
        obfinalHeight = adjustTopMargin * 2 + finalSize * strHeight;
        obfinalWidth = finalSize * strWidth;
        finalAdjustWidth = (width - obfinalWidth) / 2;
        $(ob).css("padding-left", finalAdjustWidth);
        $(ob).css("padding-right", finalAdjustWidth);

    } else {
        finalSize = Math.floor(rowHeight);
        if (finalSize == 0) finalSize = rowHeight;

        //左右填充
        var adjustLeftMargin = Math.floor((rowWidth - finalSize) * strWidth / 2);
        $(ob).css("padding-left", adjustLeftMargin);
        $(ob).css("padding-right", adjustLeftMargin);
        obfinalHeight = finalSize * strHeight;
        obfinalWidth = adjustLeftMargin * 2 + finalSize * strWidth;
        finalAdjustHeight = (height - obfinalHeight) / 2;
        $(ob).css("padding-top", finalAdjustHeight);
        $(ob).css("padding-bottom", finalAdjustHeight);
    }
    //ob.css("height", obfinalHeight + "px").css("width", obfinalWidth + "px");

    var tempClassName = elementId + "Temp";

    for (var i = 0; i < strHeight; i++) {
        var row = $(
            '<div class="am-g" style="height: ' + finalSize + 'px;margin-left:0px;margin-right:0px;"></div>');
        for (var j = 0; j < strWidth; j++) {
            var col = $('<div class="am-u-sm-1 ' + tempClassName+' " style="height:' + finalSize + 'px;width:' + finalSize + 'px;padding-left:0px;padding-right:0px;"></div>');
            row.append(col);
        }
        ob.append(row);//这里添加会导致背景图变化
    }



    var arr = [];
    strArr.forEach(function (v, i) {
        for (var j = 0; j < v.length; j++) {
            if (v[j] === '0') {
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
        $(divEles[showNumber]).css("background-color","#ebece5");
        tempList.push((divEles[showNumber]));
        //imgEles[showNumber].src = imgArr[Math.floor(Math.random() * imgArr.length)];
        //$(imgEles[showNumber]).css("height", '100%');
        //$(imgEles[showNumber]).css("width", '100%');



        if (!textArr.length) {
            clearInterval(timer);
            if (imgArr != null) {
                showImg();
            } else {
                if (nextFunc != null) {
                    awaitFunc(function () { nextFunc(); }, 1000);
                }
            }
           

            //awaitFunc(function () {
            //    $(tempList).css("background", "url(" + imgArr[Math.floor(Math.random() * imgArr.length)] + ") no-repeat");
            //    $(tempList).css("background-size", "cover");
            //}, 1000);

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
                    awaitFunc(function() { nextFunc(); }, 1000);
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
        console.log("没有元素：" + elementId);
        return;
    }
    ob.html("");
    ob.css("padding", "0");
    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    console.log("元素宽度：" + width + " 高度：" + height);

    var strHeight = strArr.length;
    var strWidth = strArr[0].split('#').length;
    console.log("字符长度：" + strHeight + "字符宽度：" + strWidth);
    var rowHeight = height / strHeight;
    var rowWidth = width / strWidth;

    //调整为方形
    var finalSize = 0;
    var obfinalHeight = 0;
    var obfinalWidth = 0;
    var finalAdjustWidth = 0;
    var finalAdjustHeight = 0;
    if (rowHeight > rowWidth) {

        finalSize = Math.floor(rowWidth);
        if (finalSize == 0) finalSize = rowWidth;

        //上下填充
        var adjustTopMargin = Math.floor((rowHeight - finalSize) * strHeight / 2);
        $(ob).css("padding-top", adjustTopMargin);
        $(ob).css("padding-bottom", adjustTopMargin);
        obfinalHeight = adjustTopMargin * 2 + finalSize * strHeight;
        obfinalWidth = finalSize * strWidth;
        finalAdjustWidth = (width - obfinalWidth) / 2;
        $(ob).css("padding-left", finalAdjustWidth);
        $(ob).css("padding-right", finalAdjustWidth);

    } else {
        finalSize = Math.floor(rowHeight);
        if (finalSize == 0) finalSize = rowHeight;

        //左右填充
        var adjustLeftMargin = Math.floor((rowWidth - finalSize) * strWidth / 2);
        $(ob).css("padding-left", adjustLeftMargin);
        $(ob).css("padding-right", adjustLeftMargin);
        obfinalHeight = finalSize * strHeight;
        obfinalWidth = adjustLeftMargin * 2 + finalSize * strWidth;
        finalAdjustHeight = (height - obfinalHeight) / 2;
        $(ob).css("padding-top", finalAdjustHeight);
        $(ob).css("padding-bottom", finalAdjustHeight);
    }

    var tempClassName = elementId + "Temp";

    for (var i = 0; i < strHeight; i++) {
        var row = $(
            '<div class="am-g" style="height: ' + finalSize + 'px;margin-left:0px;margin-right:0px;"></div>');
        for (var j = 0; j < strWidth; j++) {
            var col = $('<div class="am-u-sm-1 ' + tempClassName + '  " style="height:' + finalSize + 'px;width:' + finalSize + 'px;padding-left:0px;padding-right:0px;background-color:'+strArr[i].split("#")[j]+';"></div>');
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
function typedWord(sourceId, strarray, outputId, nextfun) {
    var desstr = [];
    if (sourceId != "") {
        //剔除多余空格，但保留样式之间的空格
        desstr.push($("#" + sourceId).html().replace(/\s{2,1000}/g, ""));
    } else {
        desstr = strarray;
    };
    var temp1 = new Typed("#" + outputId, {
        //strings: ['<img class="am-radius" alt="140*140" src="http://s.amazeui.org/media/i/demos/bw-2014-06-19.jpg?imageView/1/w/1000/h/1000/q/80" width="140" height="140" />'],
        strings: desstr,
        //stringsElement: '#typed-strings',
        typeSpeed: 100,//打字延迟
        backSpeed: 100,//删除延迟
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
        //onStringTyped: (arrayPos, self) => {
        //    self.options.showCursor = false;
        //    self.showCursor = false;
        //},
        onComplete: function (self){
            if (nextfun != null) {
                nextfun();
            }

        }
    });
}


function ajaxTypeWord(str, fontSize,fontName,outPutId,imgList,nextFunc) {
    var ob = new Object();
    ob.Text = str;
    ob.FontSize = fontSize;
    ob.fontName = fontName;

    AjaxHelper('weeding/Home/GetWordArr',
        ob,
        function (result) {
            var jsonResult = jQuery.parseJSON(result);
            console.log(jsonResult.result);
            typeImgWord(outPutId, jsonResult.result, imgList,nextFunc);
        });
}


function ajaxTypeImg(imgName,outPutId, width,height,nextFunc) {
    var ob = new Object();
    ob.ImgName = imgName;
    ob.Width = width;
    ob.Height = height;

    AjaxHelper('weeding/Home/GetImgArr',
        ob,
        function (result) {
            var jsonResult = jQuery.parseJSON(result);
            console.log(jsonResult.result);
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