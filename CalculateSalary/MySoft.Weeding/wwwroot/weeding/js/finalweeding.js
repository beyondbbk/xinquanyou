"use strict";

var _promise = require("babel-runtime/core-js/promise");

var _promise2 = _interopRequireDefault(_promise);

var _regenerator = require("babel-runtime/regenerator");

var _regenerator2 = _interopRequireDefault(_regenerator);

var _asyncToGenerator2 = require("babel-runtime/helpers/asyncToGenerator");

var _asyncToGenerator3 = _interopRequireDefault(_asyncToGenerator2);

//2.1
var SecondOne = function () {
    var _ref = (0, _asyncToGenerator3.default)( /*#__PURE__*/_regenerator2.default.mark(function _callee() {
        return _regenerator2.default.wrap(function _callee$(_context) {
            while (1) {
                switch (_context.prev = _context.next) {
                case 0:
                    $("#secondimg").addClass('animated rollIn');
                    typed("secondimgtemp", "", "secondimg", SecondTwo);
                    _context.next = 4;
                    return sleep(2000);

                case 4:
                    $("#secondimg").addClass('animated rollOut');

                case 5:
                case "end":
                    return _context.stop();
                }
            }
        }, _callee, this);
    }));

    return function SecondOne() {
        return _ref.apply(this, arguments);
    };
}();

//2.2


function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

$(function () {

    SecondAction();
});

function FirstAction() {
    var temp = [];
    temp.push($("#temp").html().replace(/\s{2,1000}/g, ""));
    temp.push($("#temp").html().replace(/\s{2,1000}/g, "").replace("概不", "可以"));
    typed("", temp, "whatToSay", SecondAction);
}

//第二步
function SecondAction() {
    $("#mainbody").html($("#secondbody").html());
    typed("secondtemp", "", "secondman", SecondOne);
} function SecondTwo() { }

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
        typeSpeed: 100, //打字延迟
        backSpeed: 100, //删除延迟
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
        onComplete: function onComplete(self) {
            if (nextfun != null) {
                nextfun();
            }
        }
    });
}

function sleep(ms) {
    return new _promise2.default(function (resolve) {
        return setTimeout(resolve, ms);
    });
}