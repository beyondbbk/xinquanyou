//资源预加载
var loader = new resLoader({
    resources: [

        'weeding/music/Building%20A%20Family.mp3',
        'weeding/images/bg.src',
        'weeding/images/top.src',
        'weeding/images/topbeside.src',
        'weeding/images/leftandright.src',
        'weeding/images/bottomleftandright.src',
        'weeding/images/bottom.src',
        'weeding/images/zhangyuimgtopandbottom.src',
        'weeding/images/zhangyu.png',
        'weeding/images/huge.gif',

    ],
    onStart: function (total) {
        
    },
    onProgress: function (current, total) {
        
    },
    onComplete: function (total) {
        console.log("加载完毕"+total);
    }
});

loader.start();

////不知道为什么必须要设置audio为autoplay才会加载，蛋疼啊
////$("#bgmusic").removeAttr("autoplay");

////if (!('performance' in window) ||
////    !('getEntriesByType' in window.performance) ||
////    !(window.performance.getEntriesByType('resource') instanceof Array)
////) {
////    console.log("不支持");
////} else {
////    console.log("支持");
////    var timeId = setInterval(function() {
////        var resources = window.performance.getEntriesByType('resource');
////        console.log("元素个数：" + resources.length);
////        for (var i = 0; i < resources.length; i++) {
////            var res = resources[i];
////            console.log("资源名称：" + res.name + "\n\r资源请求持续时间：" + (res.responseEnd - res.startTime));
////        }
////    },100);


////}

