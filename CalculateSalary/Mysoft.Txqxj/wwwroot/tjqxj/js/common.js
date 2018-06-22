//工具类
{
    //产生随机数函数
    function RndNum(n) {
        var rnd = "";
        for (var i = 0; i < n; i++)
            rnd += Math.floor(Math.random() * 10);
        return rnd;
    }
}
//通用Post方法
{
    var isHandling = false;
    var successTxtTemp = "";
    var failTxtTemp = "";
    var $currentOb;
    var funcExtraSuccessTemp;
    //改变数据类型
    function CommonPost(postOb,postUrl, currentOb,waitTxt,successTxt,failTxt,funcExtraSuccess) {
        if (isHandling) return;
        isHandling = true;
        $currentOb = $(currentOb);
        $(currentOb).html(waitTxt);
        successTxtTemp = successTxt;
        funcExtraSuccessTemp = funcExtraSuccess;
        failTxtTemp = failTxt;
        PostInfo(JSON.stringify(postOb),postUrl);
    }

    function PostInfo(jsonData, postUrl) {
        var url = postUrl; //接收上传文件的后台地址
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
            $currentOb.html(successTxtTemp);
            isHandling = false;
            if (typeof funcExtraSuccessTemp != "undefined") {
                funcExtraSuccessTemp();
            }
        } else {
            clearStatus();
        }
    }

    function clearStatus() {
        isHandling = false;
        $currentOb.html(failTxtTemp);
    }

}