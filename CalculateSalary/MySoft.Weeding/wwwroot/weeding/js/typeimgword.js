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

    for (var i = 0; i < strHeight; i++) {
        var row = $(
            '<div class="am-g" style="height: ' + finalSize + 'px"></div>');
        for (var j = 0; j < strWidth; j++) {
            var col = $('<div class="am-u-sm-1 testcolor" style="height:' + finalSize + 'px;width:' + finalSize + 'px;padding-left:0px;padding-right:0px;"></div>');
            row.append(col);
        }
        ob.append(row);
    }

    var arr = [];
    strArr.forEach((v, i) => {
        for (var j = 1; j < v.length; j++) {
            if (v[j] === 'x') {
                arr.push(i * strWidth + j);
            }
        }
    });

    var textArr = [].concat(arr);
    var imgArrTemp = [].concat(arr);

    var divEles = $('.testcolor');
    var imgEles = $('#' + elementId + ' img');

    //先替换为红色背景块
    var timer = setInterval(() => {
        var length = textArr.length;
        var showNumber = textArr.splice(Math.random() * length, 1);
        divEles[showNumber].style.backgroundColor = '#F40';

        //imgEles[showNumber].src = imgArr[Math.floor(Math.random() * imgArr.length)];
        //$(imgEles[showNumber]).css("height", '100%');
        //$(imgEles[showNumber]).css("width", '100%');

        if (!textArr.length) {
            clearInterval(timer);
            showImg();
        }
    }, 50);

    //var imgSrc = Array.from({ length: 33 }, (v, i) => i);

    //再显示为图片
    var showImg = () => {
        var imgTimer = setInterval(() => {
            var length = imgArrTemp.length;
            var showNumber = imgArrTemp.splice(Math.random() * length, 1);

            $(divEles[showNumber]).css("background", "url(" + imgArr[Math.floor(Math.random() * imgArr.length)] +") no-repeat");
            $(divEles[showNumber]).css("background-size", "cover");

            if (imgArrTemp.length === 0) {
                clearInterval(imgTimer);
                $(".testcolor").addClass('animated rollOut');
            }
        }, 50);
    }


    //

    
}