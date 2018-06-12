﻿
//打开网页后，自动获取实时地理位置
{
    //获取地理位置
    function getLocation() {
        //var geolocation = new BMap.Geolocation();
        //geolocation.getCurrentPosition(function(r) {
        //        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
        //            var mk = new BMap.Marker(r.point);
        //            //map.addOverlay(mk);
        //            //map.panTo(r.point);
        //            alert('您的位置：' + r.point.lng + ',' + r.point.lat);
        //            baiduLocation(r.point.lng, r.point.lat);
        //        } else {
        //            alert('failed' + this.getStatus());
        //        }
        //    },
        //    { enableHighAccuracy: true });


        wx.getLocation({
            type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
            success: function (res) {
                console.debug(res.latitude);
                baiduLocation(res.longitude, res.latitude);
            }
        });
    }

    //根据微信经纬度去百度获取详细位置
    function baiduLocation(longitude, latitude) {
        //增加坐标转换
        var convertor = new BMap.Convertor();
        var pointArr = [];
        var ggPoint = new BMap.Point(longitude, latitude);
        pointArr.push(ggPoint);
        convertor.translate(pointArr, 1, 5, translateCallback);
    };

    //百度经纬度转换之后的回调
    function translateCallback(data) {

        var url =
            'http://api.map.baidu.com/geocoder/v2/?callback=renderReverse&location=' + data.points[0].lat + ',' + data.points[0].lng + '&output=json&pois=1&ak=ZKLhoXcO2o1DvaKLa9CqpTBoj7S8Ygnb';
        $.ajax(url,
            {
                dataType: 'jsonp',
                crossDomain: true,
                success: function (data) {
                    if (data.status == '0') {
                        $("#addressDetail").html(data.result.formatted_address);
                        return;
                    }
                    $("#addressDetail").html("定位失败，请重试...");
                }
            });
    }
}

//灾害类型，点击后变色
{
    $(function() {
        $(".weui-grid").bind("click",
            function () {
                $(".weui-grid").css("background", "");
                $(this).css("background", "#ECECEC");
                var temp = $(this).children("p");
                $("#kindName").html("灾害类型" + "(" + temp.html().replace(/(^\s*)|(\s*$)/g, "")+ ")");
            });
    });
}

//存储已选择的图片File
var choosedPics = [];
//存储已选择的图片路径名
var choosedPicPaths = [];

//添加图片后动态加载缩略图，缩略图预览，图片数目限定，去重等
{
    //动态添加缩略图，weui-uploader__file_status
    $(function () {
        var tmpl = '<li class="weui-uploader__file " style="background-image:url(#url#)"></li>',
            $gallery = $("#gallery"), $galleryImg = $("#galleryImg"),
            $uploaderInput = $("#uploaderInput"),
            $uploaderFiles = $("#uploaderFiles")
            ;

        $uploaderInput.on("change", function (e) {
            var src, url = window.URL || window.webkitURL || window.mozURL, files = e.target.files;

            alert("数量2：" + files.length);
            alert("对象属性"+getStr(files[0]));
            //开始添加图片
            for (var i = 0, len = files.length; i < len; ++i) {
                //更新图片数，检查是否超限
                var num = $("#uploaderFiles li").length+1;
                alert("总数：" + num);
                $(".weui-uploader__info").html(num + "/8");
                if (num >8) {
                    ShowMsg('图片超限', '最多只能上传8张图片', 400);
                    return;
                }
                var file = files[i];
                alert("文件名"+file.name);
                //判定图片是否重复，不重复就将图片文件名记录下来
                var index = $.inArray(file.name, choosedPicPaths);
                if (index!=-1) {
                    continue;
                }
                choosedPics.push(file);
                choosedPicPaths.push(file.name);
                if (url) {
                    src = url.createObjectURL(file);
                } else {
                    src = e.target.result;
                }

                $uploaderFiles.append($(tmpl.replace('#url#', src)));

            }
        });
        $uploaderFiles.on("click", "li", function () {
            $galleryImg.attr("style", this.getAttribute("style"));
            $gallery.fadeIn(300);
        });
        $gallery.on("click", function () {
            $gallery.fadeOut(300);
        });
    });
}

var setTimeId = 0;
var isShow = false;
//文本框字数限定符
{
    $(function() {
        $(".weui-textarea").on("propertychange input",
            function () {
                clearTimeout(setTimeId);
                setTimeId = setTimeout(function() {
                    var length = $(".weui-textarea").val().length;
                    if (parseInt(length) > 100) {
                        if (!isShow) {
                            ShowMsg('文字超限', '请尽量简洁的描述现场情况', 300);
                        }
                       
                        isShow = true;
                        $(".weui-textarea-counter").css("color", "#f43530");
                    } else {
                        isShow = false;
                        $(".weui-textarea-counter").css("color", "#b2b2b2");
                    }
                    $("#inputNum").html(length);
                }, 1500);
                
            });
    });
}

//通用弹窗
{
    function ShowMsg(title, msg, time) {
        //图片超限对话框关闭事件
        $("#msg").on("click",
            function () {
                $(this).fadeOut(300);
            });

        $(".weui-dialog__title").html(title);
        $(".weui-dialog__bd").html(msg);
        $("#msg").fadeIn(time);
    }
}

//辅助调试方法
{
    function getStr(obj) {
        var temp = "";
        for (var p in obj) { temp += (p+":"+obj[p] + '\n\r'); }
        return temp;
    }
}