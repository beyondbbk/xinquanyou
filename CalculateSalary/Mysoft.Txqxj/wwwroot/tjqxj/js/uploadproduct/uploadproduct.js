
var chooseProduct = "";
//产品类型图片，点击后变色
{
    $(function () {
        $("#chooseproduct .weui-grid").bind("click",
            function () {
                $("#chooseproduct .weui-grid").css("background", "");
                $(this).css("background", "#ECECEC");
                var temp = $(this).children("p");
                chooseProduct = temp.html().replace(/(^\s*)|(\s*$)/g, "");
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