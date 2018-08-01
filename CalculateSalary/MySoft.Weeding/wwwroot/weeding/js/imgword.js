////var $ = selectors => document.querySelector(selectors);
//var $all = selectors => document.querySelectorAll(selectors);

//用于展示的图片
var imgList1 = Array.from({ length: 11 }, (v, i) => `weeding/images/face${i+1}.jpg`);
//var imgList2 = Array.from({ length: 11 }, (v, i) => `weeding/images/face${i+1}.jpg`);
//var imgList3 = Array.from({ length: 11 }, (v, i) => `weeding/images/face${i+1}.jpg`);
//var imgList = imgList1.concat(imgList2, imgList3);

////126=7*18
//var faceList = Array.from({ length: 180 }, () => {
//  var face = document.createElement("span");
//  var img = document.createElement("img");
//  var i = document.createElement("i");
//  i.style.left = '-100%';
//  face.append(img);
//  face.append(i);
//  return face;
//});

//var box = document.createElement("div");
//box.classList.add('eoi-box');
//faceList.forEach(ele => {
//  box.append(ele);
//});

//document.body.appendChild(box);
//以上是创建dom的过程

//var EOITextArr = 
//    [
//        '             ',
//        '   xxxxxxx    ',
//        '   x     x    ',
//        '   x     x    ',
//        '   xxxxxxx    ',
//        '         x    ',
//        '         x    ',
//        '   xxxxxxx    ',
//        '             ',
//        '             '

//];

var EOITextArr =
[
    '              ',
    '   xxxxxxx    ',
    '   x     x    ',
    '   x     x    ',
    '   xxxxxxx    ',
    '         x    ',
    '         x    ',
    '   xxxxxxx    ',
    '              ',
    '              '

];

typeImgWord("mainPage", EOITextArr, imgList1);

