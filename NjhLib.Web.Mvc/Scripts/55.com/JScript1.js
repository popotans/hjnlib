/*设置cookie,以天为过期时间*/
function setCookie(c_name, value, expiredays) {
    //设置主域 var Cookie_Domain='t.com';
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
	((expiredays == null) ? "" : ";expires=" + exdate.toUTCString()) + '; path=/';
}
/*获得cookie*/
function getCookie(name) {
    if (document.cookie.length > 0) {
        var start = document.cookie.indexOf(name + "=");
        var len = start + name.length + 1;
        if ((!start) && (name != document.cookie.substring(0, name.length))) {
            return null;
        }
        if (start == -1)
            return null;
        var end = document.cookie.indexOf(';', len);
        if (end == -1)
            end = document.cookie.length;

        return unescape(document.cookie.substring(len, end));
    }
    return "";
}
