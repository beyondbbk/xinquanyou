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

//专门用于照片墙效果
function typeImgWord(elementId, strArr, imgArr) {
    var ob = $("#" + elementId);
    if (ob == null) {
        console.log("没有元素：" + elementId);
        return;
    }

    var height = parseFloat(ob.height());
    var width = parseFloat(ob.width());
    console.log("元素宽度：" + width + " 高度：" + height);

    var strHeight = strArr.length;
    var strWidth = strArr[0].length;

    var rowHeight = height / strHeight;
    var rowWidth = width / strWidth;

    //调整为方形
    var finalSize = 0;
    if (rowHeight > rowWidth) {

        finalSize = rowWidth;
        //上下填充
        var adjustTopMargin = (rowHeight - finalSize) * strHeight / 2;
        $(ob).css("padding-top", adjustTopMargin);
        $(ob).css("padding-bottom", adjustTopMargin);
    } else {
        finalSize = rowHeight;
        //左右填充
        var adjustLeftMargin = (rowWidth - finalSize) * strWidth / 2;
        $(ob).css("padding-left", adjustLeftMargin);
        $(ob).css("padding-right", adjustLeftMargin);
    }

    var tempClassName = elementId + "Temp";

    for (var i = 0; i < strHeight; i++) {
        var row = $(
            '<div class="am-g" style="height: ' + finalSize + 'px"></div>');
        for (var j = 0; j < strWidth; j++) {
            var col = $('<div class="am-u-sm-1 ' + tempClassName+' " style="height:' + finalSize + 'px;width:' + finalSize + 'px;padding-left:0px;padding-right:0px;"></div>');
            row.append(col);
        }
        ob.append(row);
    }

    var arr = [];
    strArr.forEach(function (v, i) {
        for (var j = 1; j < v.length; j++) {
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
        $(divEles[showNumber]).css("background-color","#F40");
        tempList.push((divEles[showNumber]));
        //imgEles[showNumber].src = imgArr[Math.floor(Math.random() * imgArr.length)];
        //$(imgEles[showNumber]).css("height", '100%');
        //$(imgEles[showNumber]).css("width", '100%');



        if (!textArr.length) {
            clearInterval(timer);
            showImg();
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
            }
        }, 0);
    }


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


function ajaxTypeImg(str, fontSize,fontName,outPutId,imgList) {
    var ob = new Object();
    ob.Text = str;
    ob.FontSize = fontSize;
    ob.fontName = fontName;

    AjaxHelper('weeding/Home/GetStrArr',
        ob,
        function (result) {
            console.log(result.data);
            typeImgWord(outPutId, result.data, imgList);
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
            console.log("服务器异常");
        }
    });
}