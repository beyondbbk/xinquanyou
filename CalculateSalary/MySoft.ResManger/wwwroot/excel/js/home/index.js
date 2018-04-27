
$(function() {
    initFileInput("input-id");
});

function initFileInput(ctrlName) {  
    var control = $('#' + ctrlName);
    control.fileinput({
        language: 'zh', //设置语言
        uploadUrl: "/excel/home/upload?companyname=yh", //上传的地址
        allowedFileExtensions: ['xlsx'], //接收的文件后缀
        maxFilesNum: 1, //上传最大的文件数量
        //uploadExtraData:{"id": 1, "fileName":'123.mp3'},
        uploadAsync: true, //默认异步上传
        showUpload: true, //是否显示上传按钮
        showRemove: true, //显示移除按钮
        //showPreview: true, //是否显示预览
        //showCaption: false, //是否显示标题
        browseClass: "btn btn-primary", //按钮样式
        //dropZoneEnabled: true,//是否显示拖拽区域
        //minImageWidth: 50, //图片的最小宽度
        //minImageHeight: 50,//图片的最小高度
        //maxImageWidth: 1000,//图片的最大宽度
        //maxImageHeight: 1000,//图片的最大高度
        maxFileSize: 1000, //单位为kb，如果为0表示不限制文件大小
        //minFileCount: 0,
        //maxFileCount: 10, //表示允许同时上传的最大文件个数
        enctype: 'multipart/form-data',
        validateInitialCount: true,
        //previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",

    }).on('filepreupload',
        function(event, data, previewId, index) { //上传中  
            var form = data.form,
                files = data.files,
                extra = data.extra,
                response = data.response,
                reader = data.reader;
        }).on("fileuploaded",
        function (event, data, previewId, index) { //一个文件上传成功
            GlobalShowInfo("正在拼命解析数据中，请稍候...", "ok");
            window.location.href = "/excel/home/analyexcelfile?companyname=yh";
        }).on('fileerror',
        function(event, data, msg) { //一个文件上传失败  
            GlobalShowInfo("文件上传失败了，请重试...", "fail");
            GlobalAutoHide();
        });
}

