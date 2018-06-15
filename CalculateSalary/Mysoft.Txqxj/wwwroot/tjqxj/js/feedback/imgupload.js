//图片压缩
var blobPics = [];//存放压缩后的图片二进制数据
{
    //传入单图片文件进行压缩
    function CompressPic(file,picNum) {
        photoCompress(file, {
            quality: 0.2//压缩质量
        }, function (base64Codes) {
            var blob = convertBase64UrlToBlob(base64Codes);
            choosedPicBlobs[picNum] = blob;
            var url = window.URL || window.webkitURL || window.mozURL;
            choosedPicUrls[picNum] = url.createObjectURL(blob);
        });
    }

    /*
    file：一个是文件(类型是图片格式)，
    w：一个是文件压缩的后宽度，宽度越小，字节越小
    objDiv：一个是容器或者回调函数
     */
    function photoCompress(file, w, objDiv) {
        var ready = new FileReader();
        /*开始读取指定的Blob对象或File对象中的内容. 当读取操作完成时,readyState属性的值会成为DONE,如果设置了onloadend事件处理程序,则调用之.同时,result属性中将包含一个data: URL格式的字符串以表示所读取文件的内容.*/
        ready.readAsDataURL(file);
        ready.onload = function () {
            var re = this.result;
            canvasDataURL(re, w, objDiv);
        }
    }
    function canvasDataURL(path, obj, callback) {
        var img = new Image();
        img.src = path;
        img.onload = function () {
            var that = this;
            // 默认按比例压缩
            var w = that.width,
                h = that.height,
                scale = w / h;
            w = obj.width || w;
            h = obj.height || (w / scale);
            //生成canvas
            var canvas = document.createElement('canvas');
            var ctx = canvas.getContext('2d');
            // 创建属性节点
            var anw = document.createAttribute("width");
            anw.nodeValue = w;
            var anh = document.createAttribute("height");
            anh.nodeValue = h;
            canvas.setAttributeNode(anw);
            canvas.setAttributeNode(anh);
            ctx.drawImage(that, 0, 0, w, h);
            // 图像质量
            if (obj.quality && obj.quality <= 1 && obj.quality > 0) {
                quality = obj.quality;
            }
            // quality值越小，所绘制出的图像越模糊
            var base64 = canvas.toDataURL('image/jpeg', quality);
            // 回调函数返回base64的值
            callback(base64);
        }
    }
    //将以base64的图片url数据转换为Blob
    function convertBase64UrlToBlob(urlData) {
        var arr = urlData.split(','), mime = arr[0].match(/:(.*?);/)[1],
            bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
        while (n--) {
            u8arr[n] = bstr.charCodeAt(n);
        }
        return new Blob([u8arr], { type: mime });
    }
}

var unicodeId = Date.parse(new Date()) + RndNum(5);//唯一标识
var imgCompressDoneNum = 0;
var isSuccess = true;
//开始上传
{
    //文件信息存放于choosedPics数组
    function SumbitInfo() {
        $(".weui-btn_primary").addClass("weui-btn_loading");
        $(".weui-btn_primary").append($("<i class=\"weui-loading\"></i>"));

        var tmpl = '<div class="weui-uploader__file-content">@temp</div>';
        $(".weui-uploader__file").addClass("weui-uploader__file_status");
        $(".weui-uploader__file").append($(tmpl.replace("@temp", "上传")));

        //先上传图片
        for (var picNum = 0; picNum < choosedPicBlobs.length;picNum++) {
            PostImgInfo(choosedPicBlobs[picNum], picNum);
        };

        //再上传其它信息
        var timeId = setInterval(function() {
            //等待图片先上传完毕
            if (imgCompressDoneNum==choosedPicBlobs.length) {
                //上传其他信息
                clearInterval(timeId);
                var ob = new Object();
                ob.id = unicodeId;
                ob.address = $("#addressDetail").html();
                ob.calamity = choosedCalamity;
                ob.remark = $("#remark").val();
                PostTextInfo(JSON.stringify(ob));
            } 
        }, 500);
    }

    var xhr;
    //开始上传
    function PostImgInfo(blob,fileNum) {
        var url = "/tjqxj/home/upload"; //接收上传文件的后台地址
        var form = new FormData(); // FormData 对象
        form.append("file", blob, unicodeId + "-" + choosedPicNames[fileNum]); //文件对象
        xhr = new XMLHttpRequest();  // XMLHttpRequest 对象
        xhr.open("post", url, true); //true异步处理
        xhr.timeout = 20000;
        xhr.upload.addEventListener("progress", function (evt) {
            uploadProgress(evt, fileNum);
        }, false);
        xhr.addEventListener("load", function (evt) {
            uploadComplete(evt, fileNum);
        }, false);
        xhr.onerror = function() {
            clearStatus();
        };
        xhr.ontimeout = function () {
            clearStatus();
        };
        xhr.send(form); //开始上传，发送form数据
    }

    //上传响应
    function uploadComplete(evt, fileNum) {
        if (!isSuccess) {
            return;
        }
        if (evt.currentTarget.response == "success") {
            $(".weui-uploader__file-content").eq(fileNum).html(100);
            imgCompressDoneNum++;
            console.debug("图片" + fileNum + "上传完成");
        } else {
            clearStatus();
        }
        
    }

    //上传进度实现
    function uploadProgress(evt, fileNum) {
        // event.total是需要传输的总字节，event.loaded是已经传输的字节。如果event.lengthComputable不为真，则event.total等于0
        if (evt.lengthComputable) {//
            var percent = Math.round(evt.loaded / evt.total * 100);
            $(".weui-uploader__file-content").eq(fileNum).html(percent);
        }
    }

    //开始上传其它信息
    function PostTextInfo(jsonData) {
        var url = "/tjqxj/home/upload"; //接收上传文件的后台地址
        var form = new FormData(); // FormData 对象
        form.append("json", jsonData); //文件对象
        xhr = new XMLHttpRequest();  // XMLHttpRequest 对象
        xhr.open("post", url, true); //true异步处理
        xhr.timeout = 20000;
        xhr.onload = uploadTextComplete;
        xhr.onerror = function () {
            clearStatus();
        };
        xhr.ontimeout = function () {
            clearStatus();
        };
        xhr.send(form); //开始上传，发送form数据
    }

    function uploadTextComplete(evt) {
        if (evt.currentTarget.response == "success") {
            window.location.href = "/tjqxj/home/success";
        } else {
            clearStatus();
        }
    }

    function clearStatus() {
        isSuccess = false;
        clearInterval(timeId);
        //恢复状态
        $(".weui-btn_primary").removeClass("weui-btn_loading");
        $(".weui-btn_primary").children("i").remove();

        $(".weui-uploader__file").removeClass("weui-uploader__file_status");
        $(".weui-uploader__file").children("div").remove();

        unicodeId = Date.parse(new Date()) + RndNum(5);//唯一标识
        ShowMsg("请重试", "抱歉，上传出现了问题");
    }
}