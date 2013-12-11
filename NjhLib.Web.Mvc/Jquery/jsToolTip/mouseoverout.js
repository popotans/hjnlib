var $$ = function (id) {
    return "string" == typeof id ? document.getElementById(id) : id;
};

var isIE = navigator.userAgent.indexOf('MSIE') != -1;
var isIE6 = isIE && ([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1] == "6");
var isChrome = navigator.userAgent.indexOf('Chrome') != -1;
var isSafari = navigator.userAgent.indexOf('AppleWebKit') != -1;

// with input from Tino Zijdel, Matthias Miller, Diego Perini 
// http://dean.edwards.name/weblog/2005/10/add-event/ 
function addEvent(element, type, handler) {
    if (element.addEventListener) {
        element.addEventListener(type, handler, false);
    } else {
        if (!handler.$$guid) handler.$$guid = addEvent.guid++;
        if (!element.events) element.events = {};
        var handlers = element.events[type];
        if (!handlers) {
            handlers = element.events[type] = {};
            if (element["on" + type]) {
                handlers[0] = element["on" + type];
            }
        }
        handlers[handler.$$guid] = handler;
        element["on" + type] = handleEvent;
    }
};
addEvent.guid = 1;
function removeEvent(element, type, handler) {
    if (element.removeEventListener) {
        element.removeEventListener(type, handler, false);
    } else {
        if (element.events && element.events[type]) {
            delete element.events[type][handler.$$guid];
        }
    }
};
function handleEvent(event) {
    var returnValue = true;
    event = event || fixEvent(((this.ownerDocument || this.document || this).parentWindow || window).event);
    var handlers = this.events[event.type];
    for (var i in handlers) {
        this.$$handleEvent = handlers[i];
        if (this.$$handleEvent(event) === false) {
            returnValue = false;
        }
    }
    return returnValue;
};
function fixEvent(event) {
    event.target = event.srcElement;
    if (event.type == "mouseout") {
        event.relatedTarget = event.toElement;
    } else if (event.type == "mouseover") {
        event.relatedTarget = event.fromElement;
    }
    return event;
};

var Extend = function (destination, source) {
    for (var property in source) {
        destination[property] = source[property];
    }
    return destination;
}

var Contains = function (a, b) {
    return a.contains ? a != b && a.contains(b) : !!(a.compareDocumentPosition(b) & 16);
}

var Bind = function (object, fun) {
    var args = Array.prototype.slice.call(arguments, 2);
    return function () {
        return fun.apply(object, args.concat(Array.prototype.slice.call(arguments)));
    }
}

var BindAsEventListener = function (object, fun) {
    var args = Array.prototype.slice.call(arguments, 2);
    return function (event) {
        return fun.apply(object, [event].concat(args));
    }
}

function GetRelative(relElem, fixedElem, options) {
    //默认值
    options = Extend({
        Align: "clientleft", //水平方向定位
        vAlign: "clienttop", //垂直方向定位
        CustomLeft: 0, //自定义left定位
        CustomTop: 0, //自定义top定位
        PercentLeft: 0, //自定义left百分比定位
        PercentTop: 0, //自定义top百分比定位
        Adaptive: false, //是否自适应定位
        Reset: false//自适应定位时是否重新定位
    }, options || {});
    //定义参数
    var rect = relElem.getBoundingClientRect(),
		scrolldoc = isChrome || isSafari ? document.body : document.documentElement,
		iLeft = GetRelative.Left(options.Align, rect, fixedElem) + options.CustomLeft,
		iTop = GetRelative.Top(options.vAlign, rect, fixedElem) + options.CustomTop;
    //自定义百分比定位
    if (options.PercentLeft) { iLeft += .01 * options.PercentLeft * relElem.offsetWidth; };
    if (options.PercentTop) { iTop += .01 * options.PercentTop * relElem.offsetHeight; };
    //自适应视窗定位
    if (options.Adaptive) {
        //修正定位参数
        var maxLeft = document.documentElement.clientWidth - fixedElem.offsetWidth,
			maxTop = document.documentElement.clientHeight - fixedElem.offsetHeight;
        if (options.Reset) {
            //自动重新定位
            if (iLeft > maxLeft || iLeft < 0) {
                iLeft = GetRelative.Left(2 * iLeft > maxLeft ? "left" : "right") + options.CustomLeft;
            };
            if (iTop > maxTop || iTop < 0) {
                iTop = GetRelative.Top(2 * iTop > maxTop ? "top" : "bottom") + options.CustomTop;
            };
        } else {
            //修正到适合位置
            iLeft = Math.max(Math.min(iLeft, maxLeft), 0);
            iTop = Math.max(Math.min(iTop, maxTop), 0);
        };
    };
    //加上滚动条
    iLeft += scrolldoc.scrollLeft; iTop += scrolldoc.scrollTop;
    //返回定位参数
    return { Left: iLeft, Top: iTop };
};
GetRelative.Left = function (align, rect, fixedElem) {
    var iLeft = 0;
    switch (align.toLowerCase()) {
        case "left":
            return rect.left - fixedElem.offsetWidth;
        case "clientleft":
            return rect.left;
        case "center":
            return (rect.left + rect.right - fixedElem.offsetWidth) / 2;
        case "clientright":
            return rect.right - fixedElem.offsetWidth;
        case "right":
        default:
            return rect.right;
    };
};
GetRelative.Top = function (valign, rect, fixedElem) {
    var iTop = 0;
    switch (valign.toLowerCase()) {
        case "top":
            return rect.top - fixedElem.offsetHeight;
        case "clienttop":
            return rect.top;
        case "center":
            return (rect.top + rect.bottom - fixedElem.offsetHeight) / 2;
        case "clientbottom":
            return rect.bottom - fixedElem.offsetHeight;
        case "bottom":
        default:
            return rect.bottom;
    };
};


var FixedTips = function (tip, options) {
    this.Tip = $$(tip); //提示框

    this._trigger = null; //触发对象
    this._timer = null; //定时器
    this._cssTip = this.Tip.style; //简化代码
    this._onshow = false; //记录当前显示状态

    this.SetOptions(options);
    //处理Tip对象
    var css = this._cssTip;
    css.margin = 0; //避免定位问题
    css.position = "absolute"; css.visibility = "hidden";
    css.display = "block"; css.zIndex = 99;
    css.left = this._cssTip.top = "-9999px"; //避免占位出现滚动条
    //offset修正参数
    var iLeft = 0, iTop = 0, p = this.Tip;
    while (p.offsetParent) {
        p = p.offsetParent; iLeft += p.offsetLeft; iTop += p.offsetTop;
    };
    this._offsetleft = iLeft;
    this._offsettop = iTop;
    //移入Tip对象时保持显示状态
    addEvent(this.Tip, "mouseover", BindAsEventListener(this, function (e) {
        //如果是外部元素进入，说明当前是隐藏延时阶段，那么清除定时器取消隐藏
        this.Check(e.relatedTarget) && clearTimeout(this._timer);
    }));
    //ie6处理select
    if (isIE6) {
        var iframe = document.createElement("<iframe style='position:absolute;filter:alpha(opacity=0);display:none;'>");
        document.body.insertBefore(iframe, document.body.childNodes[0]);
        this._cssiframe = iframe.style;
    };
    //用于点击方式隐藏
    this._fCH = BindAsEventListener(this, function (e) {
        if (this.Check(e.target) && this.CheckHide()) {
            this.ReadyHide(this.IsClick(this._trigger.HideDelayType));
        };
    });
    //用于触发方式隐藏
    this._fTH = BindAsEventListener(this, function (e) {
        if (this.Check(e.relatedTarget) && this.CheckHide()) {
            this.ReadyHide(this.IsTouch(this._trigger.HideDelayType));
        };
    });
};
FixedTips.prototype = {
    //设置默认属性
    SetOptions: function (options) {
        this.options = {//默认值
            ShowType: "both", //显示方式
            HideType: "both", //隐藏方式
            ShowDelayType: "touch", //显示延迟方式
            HideDelayType: "touch", //隐藏延迟方式
            //"click":只用点击方式,"touch":只用触发方式,"both":两个都使用,"none":都不使用
            ShowDelay: 300, //显示延时时间
            HideDelay: 300, //隐藏延时时间
            Fixed: {}, //定位对象
            onShow: function () { }, //显示时执行
            onHide: function () { } //隐藏时执行
        };
        Extend(this.options, options || {});
    },
    //检查触发元素
    Check: function (elem) {
        //返回是否外部元素（即触发元素和Tip对象本身及其内部元素以外的元素对象）
        return !this._trigger ||
		!(
			this.Tip === elem || this._trigger.Elem === elem ||
			Contains(this.Tip, elem) || Contains(this._trigger.Elem, elem)
		);
    },
    //准备显示
    ReadyShow: function (delay) {
        clearTimeout(this._timer);
        var trigger = this._trigger;
        //触发方式隐藏
        this.IsTouch(trigger.HideType) && addEvent(this._trigger.Elem, "mouseout", this._fTH);
        //点击方式隐藏
        this.IsClick(trigger.HideType) && addEvent(document, "click", this._fCH);
        //显示
        if (delay) {
            this._timer = setTimeout(Bind(this, this.Show), trigger.ShowDelay);
        } else { this.Show(); };
    },
    //显示
    Show: function () {
        clearTimeout(this._timer);
        this._trigger.onShow(); //放在前面方便修改属性
        //根据预设定位和自定义定位计算left和top
        var trigger = this._trigger,
		pos = GetRelative(trigger.Elem, this.Tip, trigger.Fixed),
		iLeft = pos.Left, iTop = pos.Top;
        //设置位置并显示
        this._cssTip.left = iLeft - this._offsetleft + "px";
        this._cssTip.top = iTop - this._offsettop + "px";
        this._cssTip.visibility = "visible";
        //ie6处理select
        if (isIE6) {
            var css = this._cssiframe;
            css.width = this.Tip.offsetWidth + "px";
            css.height = this.Tip.offsetHeight + "px";
            css.left = iLeft + "px"; css.top = iTop + "px"; css.display = "";
        };
        //触发方式隐藏
        this.IsTouch(trigger.HideType) && addEvent(this.Tip, "mouseout", this._fTH);
    },
    //准备隐藏
    ReadyHide: function (delay) {
        clearTimeout(this._timer);
        if (delay) {
            this._timer = setTimeout(Bind(this, this.Hide), this._trigger.HideDelay);
        } else { this.Hide(); };
    },
    //隐藏
    Hide: function () {
        clearTimeout(this._timer);
        //设置隐藏
        this._cssTip.visibility = "hidden";
        this._cssTip.left = this._cssTip.top = "-9999px";
        //ie6处理select
        if (isIE6) { this._cssiframe.display = "none"; };
        //处理触发对象
        if (!!this._trigger) {
            this._trigger.onHide();
            removeEvent(this._trigger.Elem, "mouseout", this._fTH);
        }
        this._trigger = null;
        //移除事件
        removeEvent(this.Tip, "mouseout", this._fTH);
        removeEvent(document, "click", this._fCH);
    },
    //添加触发对象
    Add: function (elem, options) {
        //创建一个触发对象
        var elem = $$(elem), trigger = Extend(Extend({ Elem: elem }, this.options), options || {});
        //点击方式显示
        addEvent(elem, "click", BindAsEventListener(this, function (e) {
            if (this.IsClick(trigger.ShowType)) {
                if (this.CheckShow(trigger)) {
                    this.ReadyShow(this.IsClick(trigger.ShowDelayType));
                } else {
                    clearTimeout(this._timer);
                };
            };
        }));
        //触发方式显示
        addEvent(elem, "mouseover", BindAsEventListener(this, function (e) {
            if (this.IsTouch(trigger.ShowType)) {
                if (this.CheckShow(trigger)) {
                    this.ReadyShow(this.IsTouch(trigger.ShowDelayType));
                } else if (this.Check(e.relatedTarget)) {
                    clearTimeout(this._timer);
                };
            };
        }));
        //返回触发对象
        return trigger;
    },
    //显示检查
    CheckShow: function (trigger) {
        if (trigger !== this._trigger) {
            //不是同一个触发对象就先执行Hide防止冲突
            this.Hide(); this._trigger = trigger; return true;
        } else { return false; };
    },
    //隐藏检查
    CheckHide: function () {
        if (this._cssTip.visibility === "hidden") {
            //本来就是隐藏状态，不需要再执行Hide
            clearTimeout(this._timer);
            removeEvent(this._trigger.Elem, "mouseout", this._fTH);
            this._trigger = null;
            removeEvent(document, "click", this._fCH);
            return false;
        } else { return true; };
    },
    //是否点击方式
    IsClick: function (type) {
        type = type.toLowerCase();
        return type === "both" || type === "click";
    },
    //是否触发方式
    IsTouch: function (type) {
        type = type.toLowerCase();
        return type === "both" || type === "touch";
    }
};
