declare var $;
declare var Typed;
declare var Promise;
declare var per10Height;//页面10%高度

$(() => {
    per10Height = $("#mainPage").height();//640
    console.log(per10Height);
    console.log($("#mainPage").width());//360
    //secondAction();

});

function firstAction() {
    var temp = [];
    temp.push($("#temp").html().replace(/\s{2,1000}/g, ""));
    temp.push($("#temp").html().replace(/\s{2,1000}/g, "").replace("概不", "可以"));
    typed("", temp, "whatToSay", secondAction);
}

//第二步
function secondAction() {
    $("#mainPage").html($("#secondPage").html());
    typed("secondtemp", "", "secondman", secondOne);
}

//2.1
async function secondOne() {
    $("#secondimg").addClass('animated rollIn');
    typed("secondimgtemp", "", "secondimg", secondTwo);
    await sleep(2000);
    $("#secondimg").addClass('animated rollOut');
}

//2.2
function secondTwo() {

}



//source-来源id strarray-如果没有目标来源，就提供拼接好的文本数组 output-目标输出id nextfun-后续动作
function typed(source, strarray, output, nextfun) {
    var desstr = [];
    if (source != "") {
        //剔除多余空格，但保留元素之间的空格
        desstr.push($("#" + source).html().replace(/\s{2,1000}/g, ""));
    } else {
        desstr = strarray;
    };
    console.log(desstr);
    var temp1 = new Typed("#" + output, {
        //strings: ['<img class="am-radius" alt="140*140" src="http://s.amazeui.org/media/i/demos/bw-2014-06-19.jpg?imageView/1/w/1000/h/1000/q/80" width="140" height="140" />'],
        strings: desstr,
        //stringsElement: '#typed-strings',
        typeSpeed: 100,//打字延迟
        backSpeed: 100,//删除延迟
        //混序，会使句子以乱序输出
        //shuffle: true,

        //控制删除的效果
        //fadeOut: true,
        //fadeOutClass: 'typed-fade-out',
        //fadeOutDelay: 500,

        //光标
        //cursorChar: '|',
        showCursor: false,
        //arrayPos是string数组的Index
        //onStringTyped: (arrayPos, self) => {
        //    self.options.showCursor = false;
        //    self.showCursor = false;
        //},
        onComplete: (self) => {
            if (nextfun != null) {
                nextfun();
            }

        }
    });
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}