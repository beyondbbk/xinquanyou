
//打开网页后，自动获取实时地理位置
{
    var timeId = 0;
    //获取地理位置
    function getLocation() {
        timeId = setInterval(function () {
            wx.getLocation({
                type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                success: function (res) {
                    console.debug(res.latitude);
                    //偶尔会有坐标获取不到的问题，增加重试机制
                    if (res.latitude == "0.0" || res.longitude == "0.0") {
                        $("#addressDetail").html("定位失败，正发起重试...");
                    } else {
                        baiduLocation(res.longitude, res.latitude);
                    }
                }
            });
        },
            2000);//2秒后重试
    }

    //根据微信经纬度去百度获取详细位置
    function baiduLocation(longitude, latitude) {
        clearInterval(timeId);
        //增加百度坐标转换（腾讯坐标转为百度坐标）
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


var choosedClimate = "";
//灾害类型图片，点击后变色
{
    $(function () {
        $("#chooseclimate .weui-grid").bind("click",
            function () {
                $("#chooseclimate .weui-grid").css("background", "");
                $(this).css("background", "#ECECEC");
                var temp = $(this).children("p");
                choosedClimate = temp.html().replace(/(^\s*)|(\s*$)/g, "");
                
            });
    });
}

//存储已选择的图片路径名
var choosedPicNames = [];
//存储已经压缩的Blob数据
var choosedPicBlobs = [];
//图片url
var choosedPicUrls = [];
var currentChoosedPicNum = 0;

//图片数目限定，去重，添加图片后，压缩图片，加载缩略图
var tempTimeId = 0;
{

    function Test() {
        if (currentChoosedPicNum == 3) {
            //阻止后续事件
            event.stopPropagation();
            event.preventDefault();
            ShowMsg("图片超限", "最多只能上3张图片");
            return false;
        }
    };

    //动态添加缩略图，weui-uploader__file_status
    $(function () {
        var tmpl = '<li class="weui-uploader__file " style="background-image:url(/tjqxj/images/imgpre.png)"></li>',
            $gallery = $("#gallery"), $galleryImg = $("#galleryImg"),
            $uploaderInput = $("#uploaderInput"),
            $uploaderFiles = $("#uploaderFiles")
            ;

        $uploaderInput.on("change", function (e) {
            var files = e.target.files;
            var currentNum = $("#uploaderFiles li").length;
            var addNum = files.length;
            if ((currentNum + addNum) > 3) {
                ShowMsg('图片超限', '最多只能上传3张图片');
                return;
            }

            //开始添加图片
            for (var i = 0, len = files.length; i < len; ++i) {
                //}
                var file = files[i];
                //判定图片是否重复，不重复就将图片文件名记录下来
                var index = $.inArray(file.name, choosedPicNames);
                if (index != -1) {
                    continue;
                }
                $uploaderFiles.append($(tmpl));
                choosedPicNames.push(file.name);
                //压缩图片
                var fileNum = currentNum + i;
                setTimeout(CompressPic(file, fileNum),500+(i*300));
            };
            //更新已选图片数量提示
            currentChoosedPicNum = $("#uploaderFiles li").length;
            $("#imgchoosednum").html(currentChoosedPicNum + "/3");
            tempTimeId = setInterval(function () {
                isCompleted = true;
                for (var fileNum = 0; fileNum < choosedPicNames.length; fileNum++) {
                    if (typeof (choosedPicUrls[fileNum]) != "undefined") {
                        $("#uploaderFiles li").eq(fileNum)
                            .css("background-image", "url(" + choosedPicUrls[fileNum] + ")");
                    } else {
                        isCompleted = false;
                    }
                };
                if (isCompleted) {
                    clearInterval(tempTimeId);
                }
            }, 300);
        });
        $uploaderFiles.on("click", "li", function () {
            $galleryImg.attr("style", this.getAttribute("style"));
            $gallery.fadeIn(0);
        });
        $gallery.on("click", function () {
            $gallery.fadeOut(0);
        });
        
    });
}

var setTimeId = 0;
var isShow = false;
//文本框字数限定符
{
    $(function () {
        $("#remark").on("propertychange input",
            function () {
                clearTimeout(setTimeId);
                setTimeId = setTimeout(function () {
                    var length = $("#remark").val().length;
                    if (parseInt(length) > 100) {
                        if (!isShow) {
                            ShowMsg('文字超限', '请尽量简洁的描述现场情况');
                        }
                        isShow = true;
                        $("#inputword").css("color", "#f43530");
                    } else {
                        isShow = false;
                        $("#inputword").css("color", "#b2b2b2");
                    }
                    $("#inputNum").html(length);
                }, 1000);

            });
    });
}

//通用弹窗
{
    function ShowMsg(title, msg) {
        //图片超限对话框关闭事件
        $("#msg").on("click",
            function () {
                $(this).fadeOut(0);
            });

        $("#msgtitle").html(title);
        $("#msgtxt").html(msg);
        $("#msg").fadeIn(0);
    }
}

//辅助调试方法
{
    function getStr(obj) {
        var temp = "";
        for (var p in obj) { temp += (p + ":" + obj[p] + '\n\r'); }
        return temp;
    }
}