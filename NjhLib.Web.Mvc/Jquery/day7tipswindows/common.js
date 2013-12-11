
//对比时间
function compareDate(date1, date2) {

    var d1 = new Date(date1.replace(/-/g, "/"));
    var d2 = new Date(date2.replace(/-/g, "/"));
    if (Date.parse(d2) - Date.parse(d1) > 0) {
        return true;
    } else return false;
    var a, ass, aD, aS;
    var b, bss, bD, bS;

    a = date1; //得到现在时间；
    b = date2;     //得到选择时间；

    ass = a.split("-");        //以"-"分割字符串，返回数组；
    aD = new Date(ass[0], ass[1], ass[2]); //格式化为Date对像;
    aS = aD.getTime(); //得到从 1970 年 1 月 1 日开始计算到 Date 对象中的时间之间的毫秒数
    bss = b.split("-");

    bD = new Date(bss[0], bss[1], bss[2]);
    bS = bD.getTime();
    if (bS < aS) return false;
    else return true;
}
function getBrowser() {
    var bString = navigator.appName + navigator.appVersion;
    if (/MSIE/i.test(bString)) return 'MSIE';
    if (/FireFox/i.test(bString)) return 'FireFox';
    if (/NetScape/i.test(bString)) return 'NetScape';
    if (/Opera/i.test(bString)) return 'Opera';
};

/*
*弹出本页指定ID的内容于窗口
*id 指定的元素的id
*title:	window弹出窗的标题
*width:	窗口的宽,height:窗口的高
*/
function showTipsWindown(title, id, width, height) {
    tipsWindown(title, "id:" + id, width, height, "true", "", "true", id);
}

/*
*弹出本页指定URL内容于窗口
*URL 指定的URL
*title:	window弹出窗的标题
*width:	窗口的宽,height:窗口的高
*/
function showTipsWindownByUrl(title, url, width, height, backcall) {
    tipsWindown(title, "url:get?" + url, width, height, "true", "", "true", "text", backcall);
}

//关闭弹出窗口
function closeWindown() {
    $("#windownbg").remove();
    $("#windown-box").fadeOut("slow", function () { $(this).remove(); });
}
//页面跳转
function urlChange(myurl) {
    location.href = myurl;
}

/*
highlight display obj on mouse event eventName,
also accept the third param which is an array has
three elements. value is RGB or predefined color.
*/
function hightlightOnEvent(obj, eventName, org) {
    var orgColor = "#FFFFFF";
    var actColor = "#FFFF99";
    var sltColor = "#EDCD78";
    if (arguments.length == 3) {
        if (typeof (arguments[2]) == "object") {
            orgColor = arguments[2][0] ? arguments[2][0] : orgColor;
            actColor = arguments[2][1] ? arguments[2][1] : actColor;
            sltColor = arguments[2][2] ? arguments[2][2] : sltColor;
        }
    }

    if (eventName == 'OVER') {
        if (obj.bgColor.toUpperCase() != sltColor) obj.bgColor = actColor;
    }
    if (eventName == 'OUT') {
        if (obj.bgColor.toUpperCase() != sltColor) obj.bgColor = orgColor;
    }
    if (eventName == 'CLICK') {
        if (obj.bgColor.toUpperCase() == sltColor) obj.bgColor = actColor;
        else obj.bgColor = sltColor;
    }
}


//检查是否是合法的日期
function checkdate(month, day, year) {
    if (!(/^[1-9][0-9]{3,3}$/.test(year))) {
        alert("年份错误（年份只能由4个数字组成）。");
        return false;
    }
    if (!(/^[0-9]{1,2}$/.test(month))) {
        alert("月份只能1-2个由数字组成。");
        return false;
    }
    if (!(/^[0-9]{1,2}$/.test(day))) {
        alert("月的天数只能由1－2个数字组成。");
        return false;
    }
    var imonth = parseInt(month);
    var iday = parseInt(day);
    var iyear = parseInt(year);
    if (iyear < 1000 || iyear > 9999) {
        alert("年份必须在1000-9999之间。");
        return false;
    }
    if (imonth < 1 || imonth > 12) {
        alert("月份必须在1-12之间。");
        return false;
    }
    var maxDay;
    switch (imonth) {
        case 4:
        case 6:
        case 9:
        case 11:
            maxDay = 30;
            break;
        case 2:
            if (year % 4 == 0) {
                maxDay = 29;
            } else {
                if (iyear % 100 == 0 && iyear % 400 == 0) maxDay = 29;
                else maxDay = 28;
            }
            break;
        default:
            maxDay = 31;
    }
    if (iday > maxDay) {
        alert(year + "-" + month + "-" + day + "不是合法的日期。");
        return false;
    }
    return true;
}


/*--------------------------------------------------------------------------------------
以下是信息模块显示功能函数及初始化参数。
通过收集页面相关信息，在特定的区域显示特定的内容
Haisong Zheng
hszheng@gmail.com
2005-06-15
---------------------------------------------------------------------------------------*/

/*---------------------------------------------------------------------------------------*/

/*显示内容入口
参数顺序为：width, height, type, keyword, style
width : 内容块的宽，以像素为单位
height:　内容块的高，以像素为单位
type:内容类型
qkey : Query string关键词
style : 为内容块的风格，可以为空。目前有效值如下：
bulletin_default
customer_service_default
provider_customer_service_default
*/
function showIndividuationInfo() {
    var a = showIndividuationInfo.arguments;

    var params = new Array();
    var keyword = a[0] ? a[0] : 0;
    var type = a[1] ? a[1] : 0;
    var width = a[2] ? a[2] : '';
    var height = a[3] ? a[3] : '';
    //	var style = a[4]?a[4]:'';
    params[0] = 'keyword=' + keyword;
    params[1] = 'type=' + type;
    params[2] = 'width=' + width;
    params[3] = 'height=' + height;


    //	params[4] = 'style='+style;
    var info_src = individuationInfoURL + params.join('&');
    outputIndividuationInfo(width, height, info_src);
}

function outputIndividuationInfo(width, height, src) {
    //alert(src);
    document.write('<iframe' +
				   ' name="individuationInfo"' +
				   ' frameborder="0"' +
				   ' marginwidth="0"' +
				   ' marginheight="0"' +
				   ' vspace="0"' +
				   ' hspace="0"' +
				   ' allowtransparency="true"' +
				   ' scrolling="no"' +
				   ' width=' + quote(width) +
				   ' height=' + quote(height) +
				   ' src=' + quote(src) +
				   '></iframe>'
				   );
}


function popWin(url, width, height) {
    window.open(url, "popWindow", "menubar=no, location=no, resizable=no,scrollbars=no, tollbar=no, status=no, width=" + width + ", height=" + height + ", left=80, top=80");
}

function CloseWin() {
    var ua = navigator.userAgent
    var ie = navigator.appName == "Microsoft Internet Explorer" ? true : false
    if (ie) {
        var IEversion = parseFloat(ua.substring(ua.indexOf("MSIE ") + 5, ua.indexOf(";", ua.indexOf("MSIE "))))
        if (IEversion < 5.5) {
            var str = '<object id=noTipClose classid="clsid:ADB880A6-D8FF-11CF-9377-00AA003B7A11">'
            str += '<param name="Command" value="Close"></object>';
            document.body.insertAdjacentHTML("beforeEnd", str);
            document.all.noTipClose.Click();
        }
        else {
            window.opener = null;
            window.close();
        }
    }
    else {
        window.close()
    }
}