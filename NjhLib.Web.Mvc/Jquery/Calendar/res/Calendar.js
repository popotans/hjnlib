    var DTChar = "-";
    var cal;
    var isFocus = false; //是否为焦点
    //function SelectDate(obj,strFormat) //两个参数改为只传一个
    function showCalendar(obj, temp) {
        var date = new Date();
        var by = date.getFullYear() - 40; //最小值 → 10 年前
        var ey = date.getFullYear() + 13; //最大值 → 0 年后
        cal = (cal == null) ? new Calendar(by, ey, 0) : cal;    //初始化为中文版，1为英文版
        //cal.dateFormatStyle = strFormat; // 默认显示格式为:yyyy-MM-dd ,还可显示 yyyy/MM/dd
        cal.show(obj);
    }
    /**//* 返回日期 */
    String.prototype.toDate = function(style) {
        var y = this.substring(style.indexOf('y'), style.lastIndexOf('y') + 1); //年
        var m = this.substring(style.indexOf('M'), style.lastIndexOf('M') + 1); //月
        var d = this.substring(style.indexOf('d'), style.lastIndexOf('d') + 1); //日
        if (isNaN(y)) y = new Date().getFullYear();
        if (isNaN(m)) m = new Date().getMonth();
        if (isNaN(d)) d = new Date().getDate();
        var dt;
        eval("dt = new Date('" + y + "', '" + (m - 1) + "','" + d + "')");
        return dt;
    }
    /**//* 格式化日期 */
    Date.prototype.format = function(style) {
        var o = {
            "M+": this.getMonth() + 1, //month
            "d+": this.getDate(),      //day
            "h+": this.getHours(),     //hour
            "m+": this.getMinutes(),   //minute
            "s+": this.getSeconds(),   //second
            "w+": "天一二三四五六".charAt(this.getDay()),   //week
            "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
            "S": this.getMilliseconds() //millisecond
        }
        if (/(y+)/.test(style)) {
            style = style.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(style)) {
                style = style.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return style;
    };

    /**//*
* 日历类
* @param   beginYear 1990
* @param   endYear   2010
* @param   lang      0(中文)|1(英语) 可自由扩充
* @param   dateFormatStyle "yyyy-MM-dd";
*/
    function Calendar(beginYear, endYear, lang, dateFormatStyle) {
        this.beginYear = 1990;
        this.endYear = 2010;
        this.lang = 0;            //0(中文) | 1(英文)
        this.dateFormatStyle = "yyyy-MM-dd";

        if (beginYear != null && endYear != null) {
            this.beginYear = beginYear;
            this.endYear = endYear;
        }
        if (lang != null) {
            this.lang = lang
        }

        if (dateFormatStyle != null) {
            this.dateFormatStyle = dateFormatStyle
        }

        this.dateControl = null;
        this.panel = this.getElementById("calendarPanel");
        this.container = this.getElementById("ContainerPanel");
        this.form = null;

        this.date = new Date();
        this.year = this.date.getFullYear();
        this.month = this.date.getMonth();


        this.colors = {
            "cur_word": "#FFFFFF", //当日日期文字颜色
            "cur_bg": "#D6E6EF", //当日日期单元格背影色
            "sel_bg": "#F5C59F", //已被选择的日期单元格背影色
            "sun_word": "#FF0000", //星期天文字颜色
            "sat_word": "#0000FF", //星期六文字颜色
            "td_word_light": "#333333", //单元格文字颜色
            "td_word_dark": "#CCCCCC", //单元格文字暗色
            "td_bg_out": "#EFEFEF", //单元格背影色
            "td_bg_over": "#E86902", //单元格背影色
            "tr_word": "#FFFFFF", //日历头文字颜色
            "tr_bg": "#666666", //日历头背影色
            "input_border": "#CCCCCC", //input控件的边框颜色
            "input_bg": "#EFEFEF"   //input控件的背影色
        }

        this.draw();
        this.bindYear();
        this.bindMonth();
        this.changeSelect();
        this.bindData();
    }

    /**//*
* 日历类属性（语言包，可自由扩展）
*/
    Calendar.language = {
        "year": [[""], [""]],
        "months": [["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]
         ],
        "weeks": [["日", "一", "二", "三", "四", "五", "六"],
        ["SUN", "MON", "TUR", "WED", "THU", "FRI", "SAT"]
         ],
        "clear": [["清空"], ["CLS"]],
        "today": [["今天"], ["TODAY"]],
        "close": [["关闭"], ["CLOSE"]]
    }

    Calendar.prototype.draw = function() {
        calendar = this;



        var mvAry = [];
        mvAry[mvAry.length] = '<div class="data">';
        mvAry[mvAry.length] = '<table width="100%" border="0" cellspacing="0" cellpadding="0">';
        mvAry[mvAry.length] = '<tr>';
        mvAry[mvAry.length] = '<td colspan="3">';
        mvAry[mvAry.length] = '<div class="year">';
        mvAry[mvAry.length] = '<div class="data_btn">';
        mvAry[mvAry.length] = '<span> <a href="#M" name="prevYear" id="prevYear"> <img src="/jquery/calendar/res/pic_yeaL.gif" /> </a> </span>          ';
        mvAry[mvAry.length] = '<span> <a href="#M"  name="prevMonth"  id="prevMonth"> <img src="/jquery/calendar/res/pic_monthL.gif" /> </a>  </span>';
        mvAry[mvAry.length] = '</div>';
        mvAry[mvAry.length] = '<div class="data_select">';
        mvAry[mvAry.length] = '<select name="calendarYear" id="calendarYear" class="select_year"></select><select name="calendarMonth" id="calendarMonth" class="select_month"></select>';
        mvAry[mvAry.length] = '</div>';
        mvAry[mvAry.length] = '<div class="data_btn data_btn01">';
        mvAry[mvAry.length] = '<span> <a href="#M" name="nextMonth" id="nextMonth"> <img src="/jquery/calendar/res/pic_monthR.gif" /> </a> </span>    ';
        mvAry[mvAry.length] = '<span> <a href="#M"   name="nextYear"  id="nextYear"> <img src="/jquery/calendar/res/pic_yearR.gif" /> </a>  </span>';
        mvAry[mvAry.length] = '</div>';
        mvAry[mvAry.length] = '</div>';
        mvAry[mvAry.length] = '</td>';
        mvAry[mvAry.length] = '</tr>';
        mvAry[mvAry.length] = '<tr> <td colspan="3"> ';
        mvAry[mvAry.length] = '<table id="calendarTable" border="0" cellpadding="0" cellspacing="1" class="data_table">';
        mvAry[mvAry.length] = '<tr>';
        for (var i = 0; i < 7; i++) {
            mvAry[mvAry.length] = '<th  width="25">' + Calendar.language["weeks"][this.lang][i] + '</th>';
        }
        mvAry[mvAry.length] = '</tr>';
        for (var i = 0; i < 6; i++) {
            mvAry[mvAry.length] = '<tr>';
            for (var j = 0; j < 7; j++) {
                if (j == 0) {
                    mvAry[mvAry.length] = ' <td class="data_red" style="cursor:pointer;"></td>';
                } else {
                    mvAry[mvAry.length] = ' <td style="cursor:pointer;" class="data_on"></td>';
                }
            }
            mvAry[mvAry.length] = '</tr>';
        }
        mvAry[mvAry.length] = '<table>';
        mvAry[mvAry.length] = ' </td> </tr> <tr class="bg_table">';
        mvAry[mvAry.length] = '<td width="50" align="center"><span class="qingkong"><a href="#M" id="calendarClear" >清空</a> </span></td>';
        mvAry[mvAry.length] = '<td width="83" align="center"><span class="jintian"><a href="#M" id="calendarToday" >今 天</a> </span></td>';
        mvAry[mvAry.length] = '<td width="50" align="center"><span class="qingkong"><a href="#M" id="calendarClose" >关闭</a> </span></td>';
        mvAry[mvAry.length] = '</tr>';
        mvAry[mvAry.length] = '</table>';
        mvAry[mvAry.length] = '</div>';
        this.panel.innerHTML = mvAry.join("");

        var obj = this.getElementById("prevMonth");
        obj.onclick = function() { calendar.goPrevMonth(calendar); }
        obj.onblur = function() { calendar.onblur(); }
        this.prevMonth = obj;

        obj = this.getElementById("nextMonth");
        obj.onclick = function() { calendar.goNextMonth(calendar); }
        obj.onblur = function() { calendar.onblur(); }
        this.nextMonth = obj;

        obj = this.getElementById("prevYear");
        obj.onclick = function() { calendar.goPrevYear(calendar); }
        obj.onblur = function() { calendar.onblur(); }
        this.prevMonth = obj;

        obj = this.getElementById("nextYear");
        obj.onclick = function() { calendar.goNextYear(calendar); }
        obj.onblur = function() { calendar.onblur(); }
        this.nextMonth = obj;


        obj = this.getElementById("calendarClear");
        obj.onclick = function() { calendar.dateControl.value = ""; calendar.hide(); }
        this.calendarClear = obj;

        obj = this.getElementById("calendarClose");
        obj.onclick = function() { calendar.hide(); }
        this.calendarClose = obj;

        obj = this.getElementById("calendarYear");
        obj.onchange = function() { calendar.update(calendar); }
        obj.onblur = function() { calendar.onblur(); }
        this.calendarYear = obj;

        obj = this.getElementById("calendarMonth");
        with (obj) {
            onchange = function() { calendar.update(calendar); }
            onblur = function() { calendar.onblur(); }
        } this.calendarMonth = obj;

        obj = this.getElementById("calendarToday");
        obj.onclick = function() {
            var today = new Date();
            calendar.date = today;
            calendar.year = today.getFullYear();
            calendar.month = today.getMonth();
            calendar.changeSelect();
            calendar.bindData();
            calendar.dateControl.value = today.format(calendar.dateFormatStyle);
            calendar.hide();
        }
        this.calendarToday = obj;
    }

    //年份下拉框绑定数据
    Calendar.prototype.bindYear = function() {
        var cy = this.calendarYear;
        cy.length = 0;
        for (var i = this.beginYear; i <= this.endYear; i++) {
            cy.options[cy.length] = new Option(i + Calendar.language["year"][this.lang], i);
        }
    }

    //月份下拉框绑定数据
    Calendar.prototype.bindMonth = function() {
        var cm = this.calendarMonth;
        cm.length = 0;
        for (var i = 0; i < 12; i++) {
            cm.options[cm.length] = new Option(Calendar.language["months"][this.lang][i], i);
        }
    }

    //向前一月
    Calendar.prototype.goPrevMonth = function(e) {
        if (this.year == this.beginYear && this.month == 0) { return; }
        this.month--;
        if (this.month == -1) {
            this.year--;
            this.month = 11;
        }
        this.date = new Date(this.year, this.month, 1);
        this.changeSelect();
        this.bindData();
    }

    //向后一月
    Calendar.prototype.goNextMonth = function(e) {
        if (this.year == this.endYear && this.month == 11) { return; }
        this.month++;
        if (this.month == 12) {
            this.year++;
            this.month = 0;
        }
        this.date = new Date(this.year, this.month, 1);
        this.changeSelect();
        this.bindData();
    }

    //向前一年
    Calendar.prototype.goPrevYear = function(e) {
        if (this.year == this.beginYear) { return; }
        this.year--;
        this.date = new Date(this.year, this.month, 1);
        this.changeSelect();
        this.bindData();
    }

    //向后一年
    Calendar.prototype.goNextYear = function(e) {
        if (this.year == this.endYear) { return; }
        this.year++;
        this.date = new Date(this.year, this.month, 1);
        this.changeSelect();
        this.bindData();
    }

    //改变SELECT选中状态
    Calendar.prototype.changeSelect = function() {
        var cy = this.calendarYear;
        var cm = this.calendarMonth;
        for (var i = 0; i < cy.length; i++) {
            if (cy.options[i].value == this.date.getFullYear()) {
                cy[i].selected = true;
                break;
            }
        }
        for (var i = 0; i < cm.length; i++) {
            if (cm.options[i].value == this.date.getMonth()) {
                cm[i].selected = true;
                break;
            }
        }
    }

    //更新年、月
    Calendar.prototype.update = function(e) {
        this.year = e.calendarYear.options[e.calendarYear.selectedIndex].value;
        this.month = e.calendarMonth.options[e.calendarMonth.selectedIndex].value;
        this.date = new Date(this.year, this.month, 1);
        this.changeSelect();
        this.bindData();
    }

    //绑定数据到月视图
    Calendar.prototype.bindData = function() {
        var calendar = this;
        var dateArray = this.getMonthViewArray(this.date.getFullYear(), this.date.getMonth());
        var tds = this.getElementById("calendarTable").getElementsByTagName("td");
        for (var i = 0; i < tds.length; i++) {
            tds[i].style.backgroundColor = calendar.colors["td_bg_out"];
            tds[i].onclick = function() { return; }
            tds[i].onmouseover = function() { return; }
            tds[i].onmouseout = function() { return; }
            if (i > dateArray.length - 1) break;
            tds[i].innerHTML = dateArray[i];
            if (dateArray[i] != "&nbsp;") {
                tds[i].onclick = function() {
                    if (calendar.dateControl != null) {
                        calendar.dateControl.value = new Date(calendar.date.getFullYear(),
                                                calendar.date.getMonth(),
                                                this.innerHTML).format(calendar.dateFormatStyle);
                    }
                    calendar.hide();
                }
                tds[i].onmouseover = function() {
                    this.style.backgroundColor = calendar.colors["td_bg_over"];
                }
                tds[i].onmouseout = function() {
                    this.style.backgroundColor = calendar.colors["td_bg_out"];
                }
                if (new Date().format(calendar.dateFormatStyle) ==
          new Date(calendar.date.getFullYear(),
                   calendar.date.getMonth(),
                   dateArray[i]).format(calendar.dateFormatStyle)) {
                    tds[i].style.backgroundColor = calendar.colors["cur_bg"];
                    tds[i].onmouseover = function() {
                        this.style.backgroundColor = calendar.colors["td_bg_over"];
                    }
                    tds[i].onmouseout = function() {
                        this.style.backgroundColor = calendar.colors["cur_bg"];
                    }
                } //end if

                //设置已被选择的日期单元格背影色
                if (calendar.dateControl != null && calendar.dateControl.value == new Date(calendar.date.getFullYear(),
                   calendar.date.getMonth(),
                   dateArray[i]).format(calendar.dateFormatStyle)) {
                    tds[i].style.backgroundColor = calendar.colors["sel_bg"];
                    tds[i].onmouseover = function() {
                        this.style.backgroundColor = calendar.colors["td_bg_over"];
                    }
                    tds[i].onmouseout = function() {
                        this.style.backgroundColor = calendar.colors["sel_bg"];
                    }
                }
            }
        }
    }

    //根据年、月得到月视图数据(数组形式)
    Calendar.prototype.getMonthViewArray = function(y, m) {
        var mvArray = [];
        var dayOfFirstDay = new Date(y, m, 1).getDay();
        var daysOfMonth = new Date(y, m + 1, 0).getDate();
        for (var i = 0; i < 42; i++) {
            mvArray[i] = "&nbsp;";
        }
        for (var i = 0; i < daysOfMonth; i++) {
            mvArray[i + dayOfFirstDay] = i + 1;
        }
        return mvArray;
    }

    //扩展 document.getElementById(id) 多浏览器兼容性 from meizz tree source
    Calendar.prototype.getElementById = function(id) {
        if (typeof (id) != "string" || id == "") return null;
        if (document.getElementById) return document.getElementById(id);
        if (document.all) return document.all(id);
        try { return eval(id); } catch (e) { return null; }
    }

    //扩展 object.getElementsByTagName(tagName)
    Calendar.prototype.getElementsByTagName = function(object, tagName) {
        if (document.getElementsByTagName) return document.getElementsByTagName(tagName);
        if (document.all) return document.all.tags(tagName);
    }

    //取得HTML控件绝对位置
    Calendar.prototype.getAbsPoint = function(e) {
        var x = e.offsetLeft;
        var y = e.offsetTop;
        while (e = e.offsetParent) {
            x += e.offsetLeft;
            y += e.offsetTop;
        }
        return { "x": x, "y": y };
    }

    //显示日历
    Calendar.prototype.show = function(dateObj, popControl) {
        if (dateObj == null) {
            throw new Error("arguments[0] is necessary")
        }
        dateObj.focus();
        this.dateControl = dateObj;

        this.date = (dateObj.value.length > 0) ? new Date(dateObj.value.toDate(this.dateFormatStyle)) : new Date(); //若为空则显示当前月份
        this.year = this.date.getFullYear();
        this.month = this.date.getMonth();
        this.changeSelect();
        this.bindData();
        if (popControl == null) {
            popControl = dateObj;
        }
        var xy = this.getAbsPoint(popControl);
        this.panel.style.left = xy.x - 25 + "px";
        this.panel.style.top = (xy.y + dateObj.offsetHeight) + "px";

        this.panel.style.display = "";
        this.container.style.display = "";

        dateObj.onblur = function() { calendar.onblur(); }
        this.container.onmouseover = function() { isFocus = true; }
        this.container.onmouseout = function() { isFocus = false; }
    }

    //隐藏日历
    Calendar.prototype.hide = function() {
        this.panel.style.display = "none";
        this.container.style.display = "none";
        isFocus = false;
    }

    //焦点转移时隐藏日历
    Calendar.prototype.onblur = function() {
        if (!isFocus) { this.hide(); }
    }
    document.write('<div id="ContainerPanel" style="display:none;"><div id="calendarPanel" style="position: absolute;display: none;z-index: 9999;');
    document.write('background-color: #FFFFFF;border: 1px solid #CCCCCC;width:185px;font-size:12px;margin-left:25px;"></div>');
    if (document.all) {
        document.write('<iframe style="position:absolute;z-index:2000;width:expression(this.previousSibling.offsetWidth);');
        document.write('height:expression(this.previousSibling.offsetHeight);');
        document.write('left:expression(this.previousSibling.offsetLeft);top:expression(this.previousSibling.offsetTop);');
        document.write('display:expression(this.previousSibling.style.display);" scrolling="no" frameborder="no"></iframe>');
    }
    document.write('</div>');
    //-->



    String.prototype.trim = function() {
        return this.replace(/(^\s*)|(\s*$)/g, '');
    }

    function gChangeDate(vobjName, vintType) {
        var strDay, strMonth, strYear, strCYear, strTimeAry
        var strDate, StandDate, strWeekName, datDate, intNum, strChangeDate
        var resStr = "";
        if (vintType == "0" || vintType == "3" || vintType == "5" || vintType == "7" || vintType == "10")
            ChinaDateFlg = false;
        else
            ChinaDateFlg = true;

        strDate = vobjName.value.trim();
        if (strDate == "") return; //若为空值, 则不做
        //转换日期为标准模式 "1997-5-6"
        //XXXX-XX-XX
        if (strDate.indexOf(DTChar) != 0) {
            resStr = "";
            for (var i = 0; i < strDate.length; i++)
                resStr = resStr + ((strDate.substring(i, i + 1)).replace('/', DTChar));
            strDate = resStr;
        }
        //XXXX.XX.XX
        if (strDate.indexOf(".") != 0) {
            resStr = "";
            for (var i = 0; i < strDate.length; i++)
                resStr = resStr + ((strDate.substring(i, i + 1)).replace('.', DTChar));
            strDate = resStr;
        }
        //XXXXXXXX
        if (strDate.indexOf(DTChar) <= 0)
            strDate = strDate.substring(0, strDate.length - 4) + DTChar + strDate.substring(strDate.length - 4, strDate.length - 2) + DTChar + strDate.substring(strDate.length - 2, strDate.length);

        //若输入日期为12-23-23 应该是不合理的情况 ->先转成西元年格式再做判断
        var strTimeAry1 = strDate.split(DTChar);
        if (strTimeAry1.length != 3) {
            alert("请输入日期,格式为:年-月-日");
            vobjName.value = "";
            vobjName.focus();
            return;
        }
        if (strTimeAry1[0].length != 4) {
            strTimeAry1[0] = strTimeAry1[0] * 1;
            strDate = strTimeAry1[0] + DTChar + strTimeAry1[1] + DTChar + strTimeAry1[2];
        }
        //判断日期是否合法
        if (!ChinaDateFlg)
            var idDate = IsCDate(strDate, 1, 0);
        else
            var idDate = IsCDate(strDate, 1, 1);
        if (!idDate) {
            alert("请输入日期, 格式为:年-月-日");
            vobjName.value = "";
            vobjName.focus();
            return;
        }
        strYear = strTimeAry1[0];
        strCYear = strYear.toString();
        strMonth = strTimeAry1[1];
        strDay = strTimeAry1[2];
        datDate = strYear + DTChar + strMonth + DTChar + strDay;

        //OK 现在有标准的日期格式
        //找出星期几
        if (!ChinaDateFlg)
            var dateNew = new Date(datDate)
        else
            var dateNew = new Date((strCYear * 1 + 1911) + DTChar + strMonth + DTChar + strDay)
        var strWeekName = "";
        switch (dateNew.getDay()) {
            case 0:
                strWeekName = "星期日";
                break;
            case 1:
                strWeekName = "星期一";
                break;
            case 2:
                strWeekName = "星期二";
                break;
            case 3:
                strWeekName = "星期三";
                break;
            case 4:
                strWeekName = "星期四";
                break;
            case 5:
                strWeekName = "星期五";
                break;
            case 6:
                strWeekName = "星期六";
                break;
        }
        switch (vintType) {
            case 0:  //1997-08-05'
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strYear + DTChar + strMonth + DTChar + strDay;
                break;
            case 1:  //85-08-05'
                strChangeDate = strCYear + DTChar + strMonth + DTChar + strDay;
                break;
            case 2:  //085-08-05'
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                if (strCYear.length == 2)
                    strCYear = "0" + strCYear;
                strChangeDate = strCYear + DTChar + strMonth + DTChar + strDay;
                break;
            case 3:  //1997年08月05日
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strYear + "年" + strMonth + "月" + strDay + "日";
                break;
            case 4:  //86年08月05日
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strCYear + "年" + strMonth + "月" + strDay + "日";
                break;
            case 5:  //1997年08月05日星期一
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strYear + "年" + strMonth + "月" + strDay + "日" + strWeekName;
                break;
            case 6:  //86年08月05日星期一
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strCYear + "年" + strMonth + "月" + strDay + "日" + strWeekName;
                break;
            case 7:  //19970805
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strYear + strMonth + strDay;
                break;
            case 8:  //860805
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                strChangeDate = strCYear + strMonth + strDay;
                break;
            case 9:   //0860805
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                if (strCYear.length == 2)
                    strCYear = "0" + strCYear;
                strChangeDate = strCYear + strMonth + strDay;
                break;
            case 10:  //星期一
                strChangeDate = strWeekName;
                break;
            case 11:   // 860805
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                if (strCYear.length == 2)
                    strCYear = " " + strCYear;
                strChangeDate = strCYear + strMonth + strDay;
                break;
            case 12:  // 86/08/05
                if (strMonth.length == 1)
                    strMonth = "0" + strMonth;
                if (strDay.length == 1)
                    strDay = "0" + strDay;
                if (strCYear.length == 2)
                    strCYear = " " + strCYear;
                strChangeDate = strCYear + DTChar + strMonth + DTChar + strDay;
                break;
            case 13:  // 94/1/1
                if (strCYear.length == 2)
                    strCYear = " " + strCYear;
                strMonth = strMonth * 1
                strDay = strDay * 1
                strCYear = strCYear * 1
                strChangeDate = strCYear + DTChar + strMonth + DTChar + strDay;
                break;
            default:
                strChangeDate = vobjName.value; // "没有指定输出形态";
                break;
        }
        vobjName.value = strChangeDate;
    }

    function IsCDate(objV, sType, CDate) {
        var objV = objV.trim();
        var sY;
        var sM;
        var sD;
        var str;
        if (sType == 0)//非标准日期
        {
            sY = objV.substring(0, objV.length - 4);
            sM = objV.substring(objV.length - 4, objV.length - 2);
            sD = objV.substring(objV.length - 2, objV.length);
        }
        if (sType == 1)//标准日期
        {
            var strTimeAry1 = objV.split(DTChar);
            sY = strTimeAry1[0];
            sM = strTimeAry1[1];
            sD = strTimeAry1[2];
        }
        //if(CDate=1)
        //	sY=sY*1+1911;

        str = sY + DTChar + sM + DTChar + sD
        var r = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
        if (r == null) return false;
        var d = new Date(r[1], r[3] - 1, r[4]);
        return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
    }
