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
    //Ĭ��ֵ
    options = Extend({
        Align: "clientleft", //ˮƽ����λ
        vAlign: "clienttop", //��ֱ����λ
        CustomLeft: 0, //�Զ���left��λ
        CustomTop: 0, //�Զ���top��λ
        PercentLeft: 0, //�Զ���left�ٷֱȶ�λ
        PercentTop: 0, //�Զ���top�ٷֱȶ�λ
        Adaptive: false, //�Ƿ�����Ӧ��λ
        Reset: false//����Ӧ��λʱ�Ƿ����¶�λ
    }, options || {});
    //�������
    var rect = relElem.getBoundingClientRect(),
		scrolldoc = isChrome || isSafari ? document.body : document.documentElement,
		iLeft = GetRelative.Left(options.Align, rect, fixedElem) + options.CustomLeft,
		iTop = GetRelative.Top(options.vAlign, rect, fixedElem) + options.CustomTop;
    //�Զ���ٷֱȶ�λ
    if (options.PercentLeft) { iLeft += .01 * options.PercentLeft * relElem.offsetWidth; };
    if (options.PercentTop) { iTop += .01 * options.PercentTop * relElem.offsetHeight; };
    //����Ӧ�Ӵ���λ
    if (options.Adaptive) {
        //������λ����
        var maxLeft = document.documentElement.clientWidth - fixedElem.offsetWidth,
			maxTop = document.documentElement.clientHeight - fixedElem.offsetHeight;
        if (options.Reset) {
            //�Զ����¶�λ
            if (iLeft > maxLeft || iLeft < 0) {
                iLeft = GetRelative.Left(2 * iLeft > maxLeft ? "left" : "right") + options.CustomLeft;
            };
            if (iTop > maxTop || iTop < 0) {
                iTop = GetRelative.Top(2 * iTop > maxTop ? "top" : "bottom") + options.CustomTop;
            };
        } else {
            //�������ʺ�λ��
            iLeft = Math.max(Math.min(iLeft, maxLeft), 0);
            iTop = Math.max(Math.min(iTop, maxTop), 0);
        };
    };
    //���Ϲ�����
    iLeft += scrolldoc.scrollLeft; iTop += scrolldoc.scrollTop;
    //���ض�λ����
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
    this.Tip = $$(tip); //��ʾ��

    this._trigger = null; //��������
    this._timer = null; //��ʱ��
    this._cssTip = this.Tip.style; //�򻯴���
    this._onshow = false; //��¼��ǰ��ʾ״̬

    this.SetOptions(options);
    //����Tip����
    var css = this._cssTip;
    css.margin = 0; //���ⶨλ����
    css.position = "absolute"; css.visibility = "hidden";
    css.display = "block"; css.zIndex = 99;
    css.left = this._cssTip.top = "-9999px"; //����ռλ���ֹ�����
    //offset��������
    var iLeft = 0, iTop = 0, p = this.Tip;
    while (p.offsetParent) {
        p = p.offsetParent; iLeft += p.offsetLeft; iTop += p.offsetTop;
    };
    this._offsetleft = iLeft;
    this._offsettop = iTop;
    //����Tip����ʱ������ʾ״̬
    addEvent(this.Tip, "mouseover", BindAsEventListener(this, function (e) {
        //������ⲿԪ�ؽ��룬˵����ǰ��������ʱ�׶Σ���ô�����ʱ��ȡ������
        this.Check(e.relatedTarget) && clearTimeout(this._timer);
    }));
    //ie6����select
    if (isIE6) {
        var iframe = document.createElement("<iframe style='position:absolute;filter:alpha(opacity=0);display:none;'>");
        document.body.insertBefore(iframe, document.body.childNodes[0]);
        this._cssiframe = iframe.style;
    };
    //���ڵ����ʽ����
    this._fCH = BindAsEventListener(this, function (e) {
        if (this.Check(e.target) && this.CheckHide()) {
            this.ReadyHide(this.IsClick(this._trigger.HideDelayType));
        };
    });
    //���ڴ�����ʽ����
    this._fTH = BindAsEventListener(this, function (e) {
        if (this.Check(e.relatedTarget) && this.CheckHide()) {
            this.ReadyHide(this.IsTouch(this._trigger.HideDelayType));
        };
    });
};
FixedTips.prototype = {
    //����Ĭ������
    SetOptions: function (options) {
        this.options = {//Ĭ��ֵ
            ShowType: "both", //��ʾ��ʽ
            HideType: "both", //���ط�ʽ
            ShowDelayType: "touch", //��ʾ�ӳٷ�ʽ
            HideDelayType: "touch", //�����ӳٷ�ʽ
            //"click":ֻ�õ����ʽ,"touch":ֻ�ô�����ʽ,"both":������ʹ��,"none":����ʹ��
            ShowDelay: 300, //��ʾ��ʱʱ��
            HideDelay: 300, //������ʱʱ��
            Fixed: {}, //��λ����
            onShow: function () { }, //��ʾʱִ��
            onHide: function () { } //����ʱִ��
        };
        Extend(this.options, options || {});
    },
    //��鴥��Ԫ��
    Check: function (elem) {
        //�����Ƿ��ⲿԪ�أ�������Ԫ�غ�Tip���������ڲ�Ԫ�������Ԫ�ض���
        return !this._trigger ||
		!(
			this.Tip === elem || this._trigger.Elem === elem ||
			Contains(this.Tip, elem) || Contains(this._trigger.Elem, elem)
		);
    },
    //׼����ʾ
    ReadyShow: function (delay) {
        clearTimeout(this._timer);
        var trigger = this._trigger;
        //������ʽ����
        this.IsTouch(trigger.HideType) && addEvent(this._trigger.Elem, "mouseout", this._fTH);
        //�����ʽ����
        this.IsClick(trigger.HideType) && addEvent(document, "click", this._fCH);
        //��ʾ
        if (delay) {
            this._timer = setTimeout(Bind(this, this.Show), trigger.ShowDelay);
        } else { this.Show(); };
    },
    //��ʾ
    Show: function () {
        clearTimeout(this._timer);
        this._trigger.onShow(); //����ǰ�淽���޸�����
        //����Ԥ�趨λ���Զ��嶨λ����left��top
        var trigger = this._trigger,
		pos = GetRelative(trigger.Elem, this.Tip, trigger.Fixed),
		iLeft = pos.Left, iTop = pos.Top;
        //����λ�ò���ʾ
        this._cssTip.left = iLeft - this._offsetleft + "px";
        this._cssTip.top = iTop - this._offsettop + "px";
        this._cssTip.visibility = "visible";
        //ie6����select
        if (isIE6) {
            var css = this._cssiframe;
            css.width = this.Tip.offsetWidth + "px";
            css.height = this.Tip.offsetHeight + "px";
            css.left = iLeft + "px"; css.top = iTop + "px"; css.display = "";
        };
        //������ʽ����
        this.IsTouch(trigger.HideType) && addEvent(this.Tip, "mouseout", this._fTH);
    },
    //׼������
    ReadyHide: function (delay) {
        clearTimeout(this._timer);
        if (delay) {
            this._timer = setTimeout(Bind(this, this.Hide), this._trigger.HideDelay);
        } else { this.Hide(); };
    },
    //����
    Hide: function () {
        clearTimeout(this._timer);
        //��������
        this._cssTip.visibility = "hidden";
        this._cssTip.left = this._cssTip.top = "-9999px";
        //ie6����select
        if (isIE6) { this._cssiframe.display = "none"; };
        //����������
        if (!!this._trigger) {
            this._trigger.onHide();
            removeEvent(this._trigger.Elem, "mouseout", this._fTH);
        }
        this._trigger = null;
        //�Ƴ��¼�
        removeEvent(this.Tip, "mouseout", this._fTH);
        removeEvent(document, "click", this._fCH);
    },
    //��Ӵ�������
    Add: function (elem, options) {
        //����һ����������
        var elem = $$(elem), trigger = Extend(Extend({ Elem: elem }, this.options), options || {});
        //�����ʽ��ʾ
        addEvent(elem, "click", BindAsEventListener(this, function (e) {
            if (this.IsClick(trigger.ShowType)) {
                if (this.CheckShow(trigger)) {
                    this.ReadyShow(this.IsClick(trigger.ShowDelayType));
                } else {
                    clearTimeout(this._timer);
                };
            };
        }));
        //������ʽ��ʾ
        addEvent(elem, "mouseover", BindAsEventListener(this, function (e) {
            if (this.IsTouch(trigger.ShowType)) {
                if (this.CheckShow(trigger)) {
                    this.ReadyShow(this.IsTouch(trigger.ShowDelayType));
                } else if (this.Check(e.relatedTarget)) {
                    clearTimeout(this._timer);
                };
            };
        }));
        //���ش�������
        return trigger;
    },
    //��ʾ���
    CheckShow: function (trigger) {
        if (trigger !== this._trigger) {
            //����ͬһ�������������ִ��Hide��ֹ��ͻ
            this.Hide(); this._trigger = trigger; return true;
        } else { return false; };
    },
    //���ؼ��
    CheckHide: function () {
        if (this._cssTip.visibility === "hidden") {
            //������������״̬������Ҫ��ִ��Hide
            clearTimeout(this._timer);
            removeEvent(this._trigger.Elem, "mouseout", this._fTH);
            this._trigger = null;
            removeEvent(document, "click", this._fCH);
            return false;
        } else { return true; };
    },
    //�Ƿ�����ʽ
    IsClick: function (type) {
        type = type.toLowerCase();
        return type === "both" || type === "click";
    },
    //�Ƿ񴥷���ʽ
    IsTouch: function (type) {
        type = type.toLowerCase();
        return type === "both" || type === "touch";
    }
};
