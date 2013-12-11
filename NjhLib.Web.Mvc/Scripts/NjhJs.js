//prptotype,from qq.com
String.prototype.trim = function () {
    return this.replace(/^\s*|\s*$/g, "");
};
String.prototype.realLength = function () {
    return this.replace(/[^\x00-\xff]/g, "**").length;
};
String.prototype.replaceTags = function () {
    var t = this.replace(/</g, '&lt;');
    t = t.replace(/>/g, '&gt;');
    return t;
};
String.prototype.toDocTitle = function () {
    return this.replace(/&acute;/g, "'")
		.replace(/&quot;/g, '"')
		.replace(/&lt;/g, '<')
		.replace(/&gt;/g, '>')
		.replace(/&amp;/g, '&');
};
String.prototype.left = function (n) {
    if (this.length > n) return this.substr(0, n) + '...';
    else return this;
};
String.prototype.stripTags = function () {
    return this.replace(/<\/?[^>]+>/gi, '');
};
Array.prototype.each = function (iterator) {
    for (var i = 0; i < this.length; ++i) iterator(this[i]);
}
Array.prototype.remove = function (n) {
    if (n < 0) return this;
    else return this.slice(0, n).concat(this.slice(n + 1, this.length));
};
window.onerror = function () { return true; }




/******EMAILY验证******/
function isEmail(strEmail) {
    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
        return true;
    else
        return false;
}
/******校验是否全由数字组成******/
function isNum(s) {
    var patrn = /^[0-9]{1,}$/;
    if (!patrn.exec(s)) return false
    return true
}
/******是否url地址*****/
function isUrl(str) {
    regExp = /(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/i;
    if (str.match(regExp)) return true;
    else return false;
}

/******复制链接*****/
function copyStr(str) {
    try {
        var cText = str;
        //alert(cText);
        if (window.clipboardData) {
            window.clipboardData.setData("Text", cText);
            alert("复制完成!");
        } else if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("您的浏览器设置为不允许复制！\n如果需要此操作，请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true',再重试复制操作!");
                return false;
            }
            var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
            if (!clip)
                return;
            var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
            if (!trans) {
                return;
            }
            trans.addDataFlavor('text/unicode');
            var str = new Object();
            var len = new Object();
            var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);

            str.data = cText;
            trans.setTransferData("text/unicode", str, cText.length * 2);
            var clipid = Components.interfaces.nsIClipboard;
            if (!clip)
                return false;
            clip.setData(trans, null, clipid.kGlobalClipboard);
            alert("复制完成!");
        }
    }
    catch (e) {
    }
}
/******等比缩放图片******/
function DrawImage(ImgD, iwidth, iheight) {
    //参数(图片,允许的宽度,允许的高度)    
    var image = new Image();
    image.src = ImgD.src;
    //alert(33);
    if (image.width > 0 && image.height > 0) {
        if (image.width / image.height >= iwidth / iheight) {
            if (image.width > iwidth) {
                ImgD.width = iwidth;
                ImgD.height = (image.height * iwidth) / image.width;
            } else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        } else {
            if (image.height > iheight) {
                ImgD.height = iheight;
                ImgD.width = (image.width * iheight) / image.height;
            } else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
        }

    }
}
/*加入收藏夹*/
function addFav(url, title) { // 加入收藏夹
    if (document.all) {
        window.external.addFavorite(url, title);
    } else if (window.sidebar) {
        window.sidebar.addPanel(title, url, "");
    }
}
/*设定为首页*/
function setHomepage(url) { // 设置首页
    if (document.all) {
        document.body.style.behavior = 'url(#default#homepage)';
        document.body.setHomePage(url);

    } else if (window.sidebar) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true");
            }
        }
        var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
        prefs.setCharPref('设置首页', url);
    }
}
/***********************************无间断滚动js代码************************************************************************/

// 附带函数
var 
// 用ID获取元素
$ = function (element) {
    return typeof (element) == 'object' ? element : document.getElementById(element);
},
// 生成随机数
RandStr = function (n, u) {
    var tmStr = "abcdefghijklmnopqrstuvwxyz0123456789";
    var Len = tmStr.length;
    var Str = "";
    for (i = 1; i < n + 1; i++) {
        Str += tmStr.charAt(Math.random() * Len);
    }
    return (u ? Str.toUpperCase() : Str);
}


/*******************************************
- Marquee 替代 -- 无间滚动
- By Mudoo 2008.8
- http://hi.baidu.com/mt20
********************************************
new Marquee({
obj			: 'myMarquee',		// 滚动对象（必须）
name		: 'MyMQ_1',			// 实例名（可选，不指定则随机）
mode		: 'x',				// 滚动模式（可选，x=水平, y=垂直，默认x）
speed		: 10,				// 滚动速度（可选，越小速度越快，默认10）
autoStart 	: true,				// 自动开始（可选，默认True）
movePause	: true				// 鼠标经过是否暂停（可选，默认True）
});
********************************************/
var MyMarquees = new Array();
// 获取检测实例名
function getMyMQName(mName) {
    var name = mName == undefined ? RandStr(5) : mName;
    var myNames = ',' + MyMarquees.join(',') + ',';

    while (myNames.indexOf(',' + name + ',') != -1) {
        name = RandStr(5);
    }
    return name;
}
function Marquee(inits) {
    var _o = this;
    var _i = inits;

    if (_i.obj == undefined) return;
    _o.mode = _i.mode == undefined ? 'x' : _i.mode; 		// 滚动模式(x:横向, y:纵向)
    _o.mName = getMyMQName(_i.name); 						// 实例名
    _o.mObj = $(_i.obj); 								// 滚动对象
    _o.speed = _i.speed == undefined ? 10 : _i.speed; 		// 滚动速度
    _o.autoStart = _i.autoStart == undefined ? true : _i.autoStart; // 自动开始
    _o.movePause = _i.movePause == undefined ? true : _i.movePause; // 鼠标经过是否暂停

    _o.mDo = null; 										// 计时器
    _o.pause = false; 									// 暂停状态

    // 无间滚动初始化
    _o.init = function () {
        if ((_o.mObj.scrollWidth <= _o.mObj.offsetWidth && _o.mode == 'x') || (_o.mObj.scrollHeight <= _o.mObj.offsetHeight && _o.mode == 'y'))
            return;

        MyMarquees.push(_o.mName);

        // 克隆滚动内容
        _o.mObj.innerHTML = _o.mode == 'x' ? (
			'<table width="100%" border="0" align="left" cellpadding="0" cellspace="0">' +
			'	<tr>' +
			'		<td id="MYMQ_' + _o.mName + '_1">' + _o.mObj.innerHTML + '</td>' +
			'		<td id="MYMQ_' + _o.mName + '_2">' + _o.mObj.innerHTML + '</td>' +
			'	</tr>' +
			'</table>'
		) : (
			'<div id="MYMQ_' + _o.mName + '_1">' + _o.mObj.innerHTML + '</div>' +
			'<div id="MYMQ_' + _o.mName + '_2">' + _o.mObj.innerHTML + '</div>'
		);

        // 获取对象、高宽
        _o.mObj1 = $('MYMQ_' + _o.mName + '_1');
        _o.mObj2 = $('MYMQ_' + _o.mName + '_2');
        _o.mo1Width = _o.mObj1.scrollWidth;
        _o.mo1Height = _o.mObj1.scrollHeight;

        // 初始滚动
        if (_o.autoStart) _o.start();
    };

    // 开始滚动
    _o.start = function () {
        _o.mDo = setInterval((_o.mode == 'x' ? _o.moveX : _o.moveY), _o.speed);
        if (_o.movePause) {
            _o.mObj.onmouseover = function () { _o.pause = true; }
            _o.mObj.onmouseout = function () { _o.pause = false; }
        }
    }

    // 停止滚动
    _o.stop = function () {
        clearInterval(_o.mDo)
        _o.mObj.onmouseover = function () { }
        _o.mObj.onmouseout = function () { }
    }

    // 水平滚动
    _o.moveX = function () {
        if (_o.pause) return;
        var left = _o.mObj.scrollLeft;
        if (left == _o.mo1Width) {
            _o.mObj.scrollLeft = 0;
        } else if (left > _o.mo1Width) {
            _o.mObj.scrollLeft = left - _o.mo1Width;
        } else {
            _o.mObj.scrollLeft++;
        }
    };

    // 垂直滚动
    _o.moveY = function () {
        if (_o.pause) return;
        var top = _o.mObj.scrollTop;
        if (top == _o.mo1Height) {
            _o.mObj.scrollTop = 0;
        } else if (top > _o.mo1Height) {
            _o.mObj.scrollTop = top - _o.mo1Height;
        } else {
            _o.mObj.scrollTop++;
        }
    };
    _o.init();
}

/******取到过期时间******/
function getExpires(a) {//a:day
    var expires = new Date(new Date().getTime() + (a ? a : 1) * 24 * 3600 * 1000);
    return expires;
}

/******设置cookie*****/
function setCookie(name, value, expires, path, domain) {
    if (typeof expires == "undefined") {
        expires = new Date(new Date().getTime() + 24 * 3600 * 1000);
    }
    expires = getExpires(expires);
    document.cookie = name + "=" + escape(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "; path=/") + ((domain) ? ";domain=" + domain : "");
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return unescape(arr[2]);
    }
    return null;
}
function delCookie(sName) {
    var date = new Date();
    document.cookie = sName + "=;expires=" + date.toGMTString();
}

//****************腾讯js模块***********************//
var MiniSite = new Object();
MiniSite.Browser = {
    ie: /msie/.test(window.navigator.userAgent.toLowerCase()),
    moz: /gecko/.test(window.navigator.userAgent.toLowerCase()),
    opera: /opera/.test(window.navigator.userAgent.toLowerCase()),
    safari: /safari/.test(window.navigator.userAgent.toLowerCase())
};
MiniSite.JsLoader = {
    load: function (sUrl, fCallback) {
        var _script = document.createElement('script');

        _script.setAttribute('type', 'text/javascript');
        _script.setAttribute('src', sUrl + '?=' + Math.random());
        _script.setAttribute('charset', 'gb2312');
        document.getElementsByTagName('head')[0].appendChild(_script);
        if (MiniSite.Browser.ie) {
            _script.onreadystatechange = function () {
                if (this.readyState == 'loaded' || this.readyStaate == 'complete') {
                    fCallback();
                }
            };
        } else if (MiniSite.Browser.moz) {
            _script.onload = function () {
                fCallback();
            };
        } else {
            fCallback();
        }
    }
};
MiniSite.Cookie = {
    set: function (name, value, expires, path, domain) {
        if (typeof expires == "undefined") {
            expires = new Date(new Date().getTime() + 24 * 3600 * 1000);
        }
        document.cookie = name + "=" + escape(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "; path=/") + ((domain) ? ";domain=" + domain : "");
    },
    get: function (name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|n)"));
        if (arr != null) {
            return unescape(arr[2]);
        }
        return null;
    },
    clear: function (name, path, domain) {
        if (this.get(name)) {
            document.cookie = name + "=" + ((path) ? "; path=" + path : "; path=/") + ((domain) ? "; domain=" + domain : "") + ";expires=Fri, 02-Jan-1970 00:00:00 GMT";
        }
    }
};
function loadJS(url, load) {
    var _script = document.createElement('script');
    _script.setAttribute('type', 'text/javascript');
    _script.setAttribute('src', url);
    document.getElementsByTagName('head')[0].appendChild(_script);
    if (!load) return;
    if (document.all) {
        _script.onreadystatechange = function () {
            if (this.readyState == 'loaded' || this.readyState == 'complete') {
                load();
            }
        };
    } else {
        _script.onload = function () {
            load();
        };
    }
}
function correctPNG() // correctly handle PNG transparency in Win IE 5.5 & 6. 
{
    var arVersion = navigator.appVersion.split("MSIE")
    var version = parseFloat(arVersion[1])
    if ((version >= 5.5) && (document.body.filters)) {
        for (var j = 0; j < document.images.length; j++) {
            var img = document.images[j]
            var imgName = img.src.toUpperCase()
            if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                var imgID = (img.id) ? "id='" + img.id + "' " : ""
                var imgClass = (img.className) ? "class='" + img.className + "' " : ""
                var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' "
                var imgStyle = "display:inline-block;" + img.style.cssText
                if (img.align == "left") imgStyle = "float:left;" + imgStyle
                if (img.align == "right") imgStyle = "float:right;" + imgStyle
                if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
                var strNewHTML = "<span " + imgID + imgClass + imgTitle
             + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
             + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
             + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>"
                img.outerHTML = strNewHTML
                j = j - 1
            }
        }
    }
}
//window.attachEvent("onload", correctPNG);
function loadPng(o) {
    if (MiniSite.Browser.ie) {
        try {
            var img = o;
            var imgName = o.src.toUpperCase();
            if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                var imgID = (img.id) ? "id='" + img.id + "' " : "";
                var imgClass = (img.className) ? "class='" + img.className + "' " : "";
                var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' ";
                var imgStyle = "display:inline-block;" + img.style.cssText;
                if (img.align == "left") imgStyle = "float:left;" + imgStyle;
                if (img.align == "right") imgStyle = "float:right;" + imgStyle;
                if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle;
                var strNewHTML = "<span " + imgID + imgClass + imgTitle + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";" + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader" + "(src=\'" + img.src + "\', sizingMethod='image');\"></span>";
                img.outerHTML = strNewHTML;
            }
        } catch (e) { }
    }
}

//******************腾讯js模块end***************************//
//************************qq.comment.com js*********************************//
var Browser = new Object();
Browser.ua = window.navigator.userAgent.toLowerCase();
Browser.ie = /msie/.test(Browser.ua);
Browser.moz = /gecko/.test(Browser.ua);
Browser.opera = /opera/.test(Browser.ua);
Browser.safari = /safari/.test(Browser.ua);
//XmlHttp object
var XmlHttp = function () {
    if (Browser.ie) {
        var msxmls = ["MSXML3", "MSXML2", "Microsoft"];
        for (var i = 0; i < msxmls.length; i++) {
            try {
                return new ActiveXObject(msxmls[i] + ".XmlHttp");
            } catch (e) { }
        }
    } else {
        return new XMLHttpRequest();
    }
};
//AsynLoader
var AsynLoader = {
    config: {
        queueCount: 20, //最大并发数
        curQueue: 0		//当前并发数
    },
    load: function (sUrl, oOption) {
        AsynLoader.initOption(oOption);

        if (AsynLoader.config.curQueue >= AsynLoader.config.queueCount) {
            if (typeof oOption.onQueue == "function") {
                oOption.onQueue();
            }
            function fn(sUrl, oOption) {
                return function () {
                    AsynLoader.load(sUrl, oOption);
                }
            } (sUrl, oOption);

            window.setTimeout(fn(sUrl, oOption), 100);
            return;
        } else {
            AsynLoader.config.curQueue++;
        }

        var xmlHttp = new XmlHttp();
        xmlHttp.open(oOption.method, sUrl, true);
        var _loadCount = 0;

        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4) {
                if (_loadCount == 0) {
                    _loadCount++;
                    AsynLoader.config.curQueue--;

                    if (AsynLoader.isSuccess(xmlHttp)) {
                        var _xmlHttp = {
                            status: xmlHttp.status,
                            responseXML: xmlHttp.responseXML,
                            responseText: xmlHttp.responseText
                            //responseJS: xmlHttp.responseText.parseJSON()
                        };

                        oOption.onSuccess(_xmlHttp);
                    } else {
                        if (--oOption.decay) {
                            AsynLoader.load(sUrl, oOption);
                        }
                        else {
                            if (typeof oOption.onFailure == "function") {
                                oOption.onFailure(_xmlHttp);
                            }
                        }
                    }
                }
            }
        }

        xmlHttp.send(oOption.data);
    },

    initOption: function (oOption) {
        oOption.method = (typeof oOption.data == "undefined" || oOption.data == null) ? "get" : "post";
        oOption.asyn = oOption.asyn || true;
        oOption.decay = oOption.decay || 1;

        if (typeof oOption.data != "string" && oOption.data != null) {
            //oOption.data = oOption.data.toJSONString();
        } else if (typeof oOption.data == "undefined") {
            oOption.data = null;
        }
    },

    isSuccess: function (oXmlHttp) {
        return oXmlHttp.status == undefined
			|| oXmlHttp.status == 0
			|| (oXmlHttp.status >= 200 && oXmlHttp.status < 300);
    }
};

var Ajax = {};
Ajax.Request = function (url, option) {
    AsynLoader.load(url, {
        method: option.method,
        asyn: option.asynchronous,
        onSuccess: option.onSuccess,
        onFailure: option.onFailure
    });
};
function addEvent(obj, type, fn) {
    if (obj.attachEvent) obj.attachEvent('on' + type, fn);
    else obj.addEventListener(type, fn, false);
}
var JsLoader = {
    load: function (sId, sUrl, fCallback) {
        Element.remove(sId);

        var _script = document.createElement("script");
        _script.setAttribute("id", sId);
        _script.setAttribute("type", "text/javascript");
        _script.setAttribute("src", sUrl);
        document.getElementsByTagName("head")[0].appendChild(_script);

        if (!!(window.attachEvent && !window.opera)) {
            _script.onreadystatechange = function () {
                if (this.readyState == "loaded" || this.readyState == "complete") {
                    Element.remove(_script);
                    fCallback(this);
                }
            };
        }
        else {
            _script.onload = function () {
                Element.remove(_script);
                fCallback(this);
            };
        }
    }
};

//************************qq.comment.com js end*********************************//
//*********************************************//
//验证日期格式
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
//	input : obj		表单对象
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
function isDate(obj) { //1900-1-1 ~ 2099-12-31,分隔符必须是"-"、"."、"/"之一
    // var item = document.forms[0].dateText; //检测的字段,修改这里就可以了
    var item = obj;
    var param = item.value;
    //var filter=/^\d{4}((-\d{1,2}-)|(\.\d{1,2}\.)|(\/\d{1,2}\/))\d{1,2}$/;
    var filter = /^\d{4}(-\d{1,2}-)\d{1,2}$/;
    if (!filter.test(param)) {
        alert("请重新输入日期！");
        item.focus();
        return false;
    }
    if (param.indexOf(".") != -1) separator = "."
    if (param.indexOf("/") != -1) separator = "/"
    if (param.indexOf("-") != -1) separator = "-"
    var arrayOfDate = param.split(separator)
    var yYear = arrayOfDate[0]
    var mMonth = arrayOfDate[1]
    var dDay = arrayOfDate[2]
    filter = /^(20|19)\d{2}$/; //修改此字符串可修改年份
    if (!filter.test(yYear)) {
        alert("年份有误，请重新输入！");
        item.focus();
        return false;
    }
    filter = /^((1[0-2])|(0[1-9])|[1-9])$/;
    if (!filter.test(mMonth)) {
        alert("月份有误，请重新输入！");
        item.focus();
        return false;
    }

    isLeapYear = (yYear % 4 == 0 && yYear % 100 != 0) || (yYear % 400 == 0)//是否闰年

    filter = /^((1[02])|(0[13578])|[13578])$/;
    var filter1 = /^((3[01])|([0-2][1-9])|[1-9]|10|20)$/;
    if (!(filter.test(mMonth) && filter1.test(dDay))) {
        filter = /^(11|(0[469])|[469])$/;
        filter1 = /^(30|([0-2][1-9])|[1-9]|10|20)$/;
        if (!(filter.test(mMonth) && filter1.test(dDay))) {
            filter = /^(02|2)$/;
            if (isLeapYear)
            { filter1 = /^(([0-2][1-9])|[1-9]|10|20)$/; }
            else
            { filter1 = /^(([01]9)|([0-2][1-8])|[1-9]|10|20)$/; }
            if (!(filter.test(mMonth) && filter1.test(dDay))) {
                alert("日期有误，请重新输入！");
                item.focus();
                return false;
            }
        }
    }
    return true;
}
// ************************************************************//
//验证电话格式
function checkTel(str) {
    return str.match(/^[+]{0,1}\d{0,3}[-]{0,1}\d{0,6}[-]{0,1}\d+[-]{0,1}\d+$/) != null;
}
//***********************************************************//
//页面自动刷新
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
//	input : setsecond		刷新的间隔时间
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
function autorefresh(setsecond) {
    setTimeout("this.location.reload();", setsecond);
}
//************************************************************//
//表格排序
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
//	input : obj		表格对象
//			sCount	单元格数
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
function sortTable(obj, sCount) {
    var start = new Date;
    var theTable = new Array();
    var i;
    for (i = 1; i < obj.rows.length; i++) {
        theTable[i - 1] = new Array(obj.rows(i).cells(sCount).innerText.toLowerCase(), obj.rows(i));
    }
    theTable = theTable.sort();
    for (i = 1; i < theTable.length; i++) {
        obj.lastChild.appendChild(theTable[i][1]);
    }
    window.status = (new Date - start) + ' ms';
}
//**************************fanke凡客********************************//
//防止SQl注入
function checkSqlStr(text) {
    var repWord = "|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|set|;|from";
    var repWords = repWord.split('|');
    var appIndex;
    for (var i = 0; i < repWords.length; i++) {
        appIndex = text.indexOf(repWords[i]);
        if (appIndex != -1) {
            text = text.replace(repWords[i], "");
        }
    }
    return text;
}
//防止脚本攻击
function checkScriptStr(text) {
    var flag = true;
    var scriptWord = "<|>|script|alert|{|}|(|)|#|$|'|\"|:|;|&|*|@@|%|^|?";
    var words = scriptWord.split('|');
    for (var i = 0; i < words.length; i++) {
        if (text.indexOf(words[i]) != -1) {
            flag = false;
            break;
        }
    }
    return flag;
}
//设置对象背景颜色
function setBgColor(element) {
    element.bgColor = '#ffffcc';
}
//取消背景颜色
function restoreBgColor(element) {
    element.bgColor = '';
}
//根据ID设置对象背景颜色
function SetBgColor(elementID) {
    document.getElementById(elementID).bgColor = '#ffffcc';
}
//还原某个id对象背景色
function RestoreBgColor(elementID) {
    document.getElementById(elementID).bgColor = '';
}
//判断chekbox是否有选中
function chkCheckBoxChoice(objNam) {
    var obj = document.getElementsByName(objNam);
    var objLen = obj.length;
    var objYN;
    var i;

    objYN = false;
    for (i = 0; i < objLen; i++) {
        if (obj[i].checked == true) {
            objYN = true;
            break;
        }
    }
    return objYN;
}
//获取url地址参数中某name的值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function getCookie2(name) {
    var cookieValue = "";
    var search = name + "=";
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search);
        if (offset != -1) {
            offset += search.length;
            end = document.cookie.indexOf(";", offset);
            if (end == -1) end = document.cookie.length;
            cookieValue = unescape(document.cookie.substring(offset, end))
        }
    }
    return cookieValue;
}

function setCookie2(cookieName, cookieValue, DayValue) {
    var expire = "";
    var day_value = 1;
    if (DayValue != null) {
        day_value = DayValue;
    }
    expire = new Date((new Date()).getTime() + day_value * 86400000);
    expire = "; expires=" + expire.toGMTString();
    document.cookie = cookieName + "=" + escape(cookieValue) + ";path=/" + expire;
}

function delCookie2(cookieName) {
    var expire = "";
    expire = new Date((new Date()).getTime() - 1);
    expire = "; expires=" + expire.toGMTString();
    document.cookie = cookieName + "=" + escape("") + ";path=/" + expire;
    //path=/
}
//全角转换半角
function CtoH(strs) {
    var str = strs;
    var result = "";
    for (var i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) == 12288) {
            result += String.fromCharCode(str.charCodeAt(i) - 12256);
            continue;
        }
        if (str.charCodeAt(i) > 65280 && str.charCodeAt(i) < 65375) result += String.fromCharCode(str.charCodeAt(i) - 65248);
        else result += String.fromCharCode(str.charCodeAt(i));
    }
    return result;
}
function DBC2SBC(str) {
    var i;
    var result = '';
    for (i = 0; i < str.length; i++) {
        code = str.charCodeAt(i);

        if (code == 12290) {
            result += String.fromCharCode(46);
        }
        else if (code == 183) {
            result += String.fromCharCode(64);
        }
        else if (code >= 65281 && code < 65373) {
            result += String.fromCharCode(str.charCodeAt(i) - 65248);
        }
        else {
            result += str.charAt(i);
        }
    }
    return result;
}

// 手机号码验证，验证13系列和150-159(154除外)、180、185、186、187、188、189几种号码，长度11位 
function is_mobile(value) {
    if (value.match(/^13\d{9}$/g) || value.match(/^15[0-35-9]\d{8}$/g) || value.match(/^18[0-9]\d{8}$/g)) {
        return true;
    } else {
        return false;
    }
}

//全角半角字符转换
function convertValue(input) {
    var isIE = (document.uniqueID) ? 1 : 0;
    if (isIE) {
        try {
            var cuRange = document.selection.createRange();
            var tbRange = input.createTextRange();
            tbRange.collapse(true);
            tbRange.select();
            var headRange = document.selection.createRange();
            headRange.setEndPoint("EndToEnd", cuRange);
            var position = headRange.text.length;
            cuRange.select();

            var originValue = input.value;
            input.value = DBC2SBC(originValue);

            //var r = input.createTextRange();
            tbRange.moveStart("character", position);
            if (position == originValue.length) {
                tbRange.collapse(false);
            } else {
                tbRange.collapse(true);
            }
        } catch (e) { }
        tbRange.select();
    } else {
        var originValue = input.value;
        input.value = DBC2SBC(originValue);
    }
}
function getBrowser() {
    var bString = navigator.appName + navigator.appVersion;
    if (/MSIE/i.test(bString)) return 'MSIE';
    if (/FireFox/i.test(bString)) return 'FireFox';
    if (/NetScape/i.test(bString)) return 'NetScape';
    if (/Opera/i.test(bString)) return 'Opera';
};
/*取得页面信息，文件名
*/
function getKeyword() {
    var info = this.location.toString();
    var kstr = info.substr(info.lastIndexOf('/') + 1);
    return (encodeURIComponent(kstr));
};



