String.prototype.endsWith2 = function (str) {
    var reg = new RegExp(str + "$");
    return reg.test(this);
}

String.prototype.endsWith = function(str) {
return this.substr(this.length-str.length) == str;
}
// 判断字符串是否以指定的字符串开始
String.prototype.startsWith=function(str) {
return this.substr(0,str.length) == str;
}

String.prototype.realLength = function () {
    return this.replace(/[^\x00-\xff]/g, "**").length;
};

String.prototype.left = function (n) {
    if (this.length > n) return this.substr(0, n) + '...';
    else return this;
};

String.prototype.stripTags = function () {
    return this.replace(/<\/?[^>]+>/gi, '');
};

String.prototype.trim = function () {
    return this.replace(/^\s*|\s*$/g, "");
};

String.prototype.leftTrim = function() {
return this.replace(/(^[\s]*)/g,"");
}

String.prototype.rightTrim = function() {
return this .replace(/([\s]*$)/g, "" );
}

String.prototype.replace = function(oldValue, newValue) {
var reg = new RegExp(oldValue, "g" );
return this.replace(reg, newValue);
}

//得到有汉字字符串的长度
String.prototype.chLength = function () {
    var strLen = 0;
    for (i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 255) { strLen += 2; }
        else { strLen++; }
    }
    return strLen;
};
//去除敏感字符
String.prototype.trimBadWords = function (str) {
    var reg = new RegExp(str, "gi");
    return this.replace(reg, function (str_bad) { return str_bad.replace(/./g, "*") });
};


//去除字符串首尾空格
String.prototype.trimSpaces = function () {
    var reg = /^\s*(.*?)\s*$/gim;
    return this.replace(reg, "$1");
};

//转化<>标签为实体字符
String.prototype.trimTab = function () {
    var reg = /<|>/g;
    return this.replace(reg, function (s) { if (s == "<") { return "&lt;"; } else { return "&gt;"; } })
};

//去除任意HTML标签
String.prototype.trimHtml = function (tag) {//不写标签名代表所有标签
    tag ? reg = new RegExp("<\/?" + tag + "(?:(.|\s)*?)>", "gi") : reg = /<(?:.|\s)*?>/gi;
    return this.replace(reg, "");
};
//js浮点数精确计算函数(加，减，乘，除)//浮点数加法运算  
function FloatAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    return (arg1 * m + arg2 * m) / m;
};

//浮点数减法运算  
function FloatSub(arg1, arg2) {
    var r1, r2, m, n;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    //动态控制精度长度  
    n = (r1 >= r2) ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
};

//浮点数乘法运算  
function FloatMul(arg1, arg2) {
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try { m += s1.split(".")[1].length } catch (e) { }
    try { m += s2.split(".")[1].length } catch (e) { }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
};


//浮点数除法运算  
function FloatDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""));
        r2 = Number(arg2.toString().replace(".", ""));
        return (r1 / r2) * pow(10, t2 - t1);
    }
};

//测试字符串是否是Email地址
String.prototype.isEmail = function () {
    return /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9])+$/.test(this);
};

//测试字符串是否是URL
String.prototype.isURL = function () {
    return /^[hf]t{1,2}p:\/\/(\w+:\w+\@)?(?:[0-9a-z-]+\.)+[a-z]{2,4}(?:(\/?)|(\/.*))$/i.test(this);
};
String.prototype.byteLength = function () {
    var len = 0;
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) >= 0x80) len += 3;
        else len += 1;
    }
    return len;
};



