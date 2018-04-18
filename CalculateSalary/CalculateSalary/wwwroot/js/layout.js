$(function() {

    navWidth();
});

function navWidth() {
    var btnwidth = $("#navbtngroup").css("width");
    console.log(btnwidth);
    $("#navdropmemu").css("min-width", btnwidth);
    var tempwidth = $("#navdropmemu").css("width");
    console.log(tempwidth);
}

function loadRes() {
    GlobalShowInfo("正在导入物料，请稍候...", "");
    GlobalAutoHide();
}


//顶端显示全局消息
function GlobalShowInfo(msg, msgtype)
{
    var oldtimeid = $("#globaltitletimeid").attr("tag");
    if (oldtimeid) {
        clearTimeout(oldtimeid);
    };
    $("#globaltitle").stop();
    if (msgtype === "ok") {
        $("#globaltitle").removeClass('btn-danger').addClass("btn-success");
    } else if (msgtype === "fail") {
        $("#globaltitle").removeClass('btn-success').addClass("btn-danger");
    }
    $("#globaltitle").html(msg);
    var thiswidth = ($("#globaltitle").prop("offsetWidth"));
    $("#globaltitle").css("margin-top", "0px").css("margin-left", -thiswidth / 2);
}

function GlobalAutoHide() {
    var globaltitletimeid = setTimeout(
        function () {
            $("#globaltitle").stop();
            $("#globaltitle").css("margin-top", "0px");
            $("#globaltitle").animate({ marginTop: "-=35px" }, 3000, "linear");
        }, 10000);
    $("#globaltitletimeid").attr("tag", globaltitletimeid);
}