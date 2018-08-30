var musics = [];

//资源预加载
var loader = new resLoader({
    resources: [
        '/weeding/music/bg.mp3',
        '/weeding/images/bg.src',
        '/weeding/images/top.src',
        '/weeding/images/topbeside.src',
        '/weeding/images/leftandright.src',
        '/weeding/images/bottomleftandright.src',
        '/weeding/images/bottom.src',
        '/weeding/images/zhangyuimgtopandbottom.src',
        '/weeding/images/zhangyu.png',
        '/weeding/images/2011.gif',
        '/weeding/images/2012.gif',
        '/weeding/images/2013.gif',
        '/weeding/images/2014.gif',
        '/weeding/images/2015.gif',
        '/weeding/images/2016.gif',
        '/weeding/images/2017.gif',

        '/weeding/images/fourbottomleftflower.src',
        '/weeding/images/fourbottommidflower.src',
        '/weeding/images/fourbottomrightflower.src',

        '/weeding/images/fiveleft.src',
        '/weeding/images/fivemid.src',
        '/weeding/images/fiveright.src',
        '/weeding/images/sixleft.src',
        '/weeding/images/sixmid.src',
        '/weeding/images/sixright.src',
    ],
    onStart: function (total) {
        
    },
    onProgress: function (current, total) {
        var pro =document.getElementById("progress");//通过id获取标签
        var elements =pro.getElementsByTagName("div"); //根据标签名获取页面元素
  
        var rate = total / elements.length;
        elements[Math.floor(current / rate)].style.backgroundColor = "darkred";
        //SaveWebLog("资源加载进度" + current + "-" + total);
        document.title = "正在加载资源[" + Math.floor((current/total)*100)+"%]";
    },
    onComplete: function (total) {
        SaveWebLog("加载完毕"+total);
    }
});

loader.start();

////不知道为什么必须要设置audio为autoplay才会加载，蛋疼啊
////$("#bgmusic").removeAttr("autoplay");

////if (!('performance' in window) ||
////    !('getEntriesByType' in window.performance) ||
////    !(window.performance.getEntriesByType('resource') instanceof Array)
////) {
////    SaveWebLog("不支持");
////} else {
////    SaveWebLog("支持");
////    var timeId = setInterval(function() {
////        var resources = window.performance.getEntriesByType('resource');
////        SaveWebLog("元素个数：" + resources.length);
////        for (var i = 0; i < resources.length; i++) {
////            var res = resources[i];
////            SaveWebLog("资源名称：" + res.name + "\n\r资源请求持续时间：" + (res.responseEnd - res.startTime));
////        }
////    },100);


////}

