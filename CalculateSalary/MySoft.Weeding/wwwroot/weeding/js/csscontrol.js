$(function () {

    var bodywidth = $("body").width();
    var bodyheight = $("body").height();

    //调整顶部花束位置
    {
        var temp = $("#topflower");
        //temp.css("width", 0.7 * bodywidth + "px");//宽度70%
        //temp.css("height", 0.2 * bodyheight + "px");//高度20%
        //temp.css("left", 0.15 * bodywidth + "px");
        //temp.css("top", -0.08 * bodyheight + "px");

        temp.css("width", "70%");//宽度70%
        temp.css("height", "20%");//高度20%
        temp.css("left", "15%");
        temp.css("top", "-8%");

}
    {
        var temp = $("#topleftbesideflower");
        //temp.css("width", 0.4 * bodywidth + "px");
        //temp.css("height", 0.4 * bodyheight + "px");
        //temp.css("left", -0.1 * bodywidth + "px");
        //temp.css("top", -0.1 * bodyheight + "px");

        temp.css("width", "40%");
        temp.css("height", "40%");
        temp.css("left", "-10%");
        temp.css("top", "-10%");
    }

    //这里会撑破body
    {
        var temp = $("#toprightbesideflower");
        //temp.css("width", 0.4 * bodywidth + "px");
        //temp.css("height", 0.4 * bodyheight + "px");
        //temp.css("right", -0.1 * bodywidth + "px");
        //temp.css("top", -0.1 * bodyheight + "px");

        temp.css("width", "40%");
        temp.css("height", "40%");
        temp.css("right","-10%");
        temp.css("top", "-10%");
    }


    //以下代码全部废弃
    //设置圆形图片的位置
    //var imgTemp = $("#imgtemp");
    //var height = imgTemp.height();
    //var width = imgTemp.width();

    //console.log("图片宽度：" + width + "图片高度：" + height);
    //var temp;
    //if (height > width) {
    //    temp = width;
    //} else {
    //    temp = height;
    //}

    //var imgbg = $("#imgbg");
    //imgbg.attr("height", temp * 0.55);
    //imgbg.attr("width", temp * 0.55);

    ////设置动态花枝的位置
    //var huazhidiv = $("#huazhidiv");
    //huazhidiv.css("left", width * 0.63);
    //huazhidiv.css("top", height * 0.31); 


    //var huazhi = $("#huazhi");
    //huazhi.css("height", temp * 0.22);

    ////设置白色花枝位置
    //var baihuazhidiv = $("#baihuazhidiv");
    //baihuazhidiv.css("left", width * 0.26);
    //baihuazhidiv.css("top", height * 0.61);
    //var baihuazhi = $("#baihuazhi");
    //baihuazhi.css("height", temp * 0.22);


    //{
    //    //设置白色动态花枝定时
    //    var currentdeg = 0;
    //    var clockwise = true;

    //    var huazhiTimeId = setInterval(function () {
    //        baihuazhi.css("transform-origin", "100% 0");
    //        baihuazhi.css("transform", "rotate(" + currentdeg + "deg)");
    //        //var temp = (10 - Math.abs(currentdeg)) / 20;
    //        var temp = Math.random() / 2;
    //        //console.log("temp值："+temp);
    //        if (temp < 0.15) { temp = 0.15 }

    //        if (clockwise) {
    //            currentdeg = currentdeg + temp;
    //        } else {
    //            currentdeg = currentdeg - temp;
    //        }
    //        if (currentdeg > 10) {
    //            clockwise = false;
    //        }
    //        if (currentdeg < -10) {
    //            clockwise = true;
    //        }
    //    }, 70);
    //}

    ////设置动态花枝定时
    //{
    //    var currentdeg = 0;
    //    var clockwise = true;

    //    var huazhiTimeId = setInterval(function () {
    //        huazhi.css("transform-origin", "0 0");
    //        huazhi.css("transform", "rotate(" + currentdeg + "deg)");
    //        //var temp = (10 - Math.abs(currentdeg)) / 20;
    //        var temp = Math.random() / 2;
    //        //console.log("temp值："+temp);
    //        if (temp < 0.15) { temp = 0.15 }

    //        if (clockwise) {
    //            currentdeg = currentdeg + temp;
    //        } else {
    //            currentdeg = currentdeg - temp;
    //        }
    //        if (currentdeg > 10) {
    //            clockwise = false;
    //        }
    //        if (currentdeg < -10) {
    //            clockwise = true;
    //        }
    //    }, 50);
    //}





    //让元素左右垂直居中
    var obs = $(".mycenterdiv");
    if (obs.length != 0) {
        var ob;
        var parent;
        var margintop;
        var marginleft;
        for (var temp = 0; temp < obs.length; temp++) {
            ob = $(obs[temp]);
            parent = ob.parent();
            margintop = ((parent.height() - ob.height()) / 2).toFixed(2);
            marginleft = ((parent.width() - ob.width()) / 2).toFixed(2);
            ob.css("margin-top", margintop + "px");
            ob.css("margin-bottom", margintop + "px");
            ob.css("margin-left", marginleft + "px");
            ob.css("margin-right", marginleft + "px");
        }

    }



    awaitFunc(function() {
            //元素调整完毕后再展示
            $("body").css("visibility", "visible");
        },
        2000);

});