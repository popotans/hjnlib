/******************************************************************
*	通用不间断滚动JS封装类
*	深蓝蝴蝶
*	使用方法
*			new Marquee("marquee",0,1,760,52,50,5000,3000)
*			new Marquee("marquee",0,1,760,104,50,5000,3000,52)
*	
*		参数说明:
*			参数1	marquee	容器ID
*			参数2	0		向上滚动(0-向上 1-向下 2-向左 3-向右)
*			参数3	1		滚动的步长(数值越大,滚动越快)
*			参数4	760		容器可视宽度
*			参数5	52		容器可视高度
*			参数6	50		定时器(数值越小,滚动的速度越快 1000=1秒,建议不小于20)
*			参数7	5000		间歇停顿延迟时间(0为不停顿,1000=1秒)
*			参数8	3000		开始时的等待时间(0为不等待,1000=1秒)
*			参数9	(52)		间歇滚动间距(可选,该数值与延迟均为0则为鼠标悬停控制)
*	　使用建议:
*		1、建议直接赋予容器的显示区域的宽度和高度，
*			如(<div id="marquee" style="width:760px;height:52px;">......</div>)
*		2、建议为容器添加样式overflow=auto，
*			如(<div id="marquee" style="width:760px;height:52px;overflow:auto;">......</div>)
*		3、为了更准确的获取滚动区域的宽度和高度，请尽可能将各滚动单位直接赋予正确宽高度
*		4、对于TABLE标记的横向滚动，需要对TABLE添加样式display=inline，
*			如(<div id="marquee" style="width:760px;height:52px;overflow:auto;"><table style="display:inline">......</table></div>)
*		5、对于翻屏滚动或间歇滚动，要注意各滚动单位间的间距，
*			同时需要对容器的可视高度和可视宽度做好准确的设置，对于各滚动单位间的间距可以通过设置行间距或者单元格的高宽度来进行调整
*		6、对于LI自动换行的问题暂时没有更好的解决办法，建议将其转换成表格(TABLE)的形式来达到同等的效果
*	
*	注意：仅用div的时候需要用<nobr>来包括div
*******************************************************************/

var Marquee10 = function () { this.ID = document.getElementById(arguments[0]); this.Direction = arguments[1]; this.Step = arguments[2]; this.BakStep = arguments[2]; this.Width = arguments[3]; this.HalfWidth = Math.round(arguments[3] / 2); this.Height = arguments[4]; this.Timer = arguments[5]; this.DelayTime = arguments[6]; this.WaitTime = arguments[7]; if (arguments[8] || arguments[8] == 0) { this.ScrollStep = arguments[8]; } else { this.ScrollStep = this.Direction > 1 ? this.Width : this.Height; } this.Correct = 0; this.CTL = 0; this.StartID = 0; this.Stop = 0; this.MouseOver = 0; this.ID.style.overflow = "hidden"; this.ID.style.overflowX = "hidden"; this.ID.style.overflowY = "hidden"; this.ID.noWrap = true; this.ID.style.width = this.Width + "px"; this.ID.style.height = this.Height + "px"; this.ClientScroll = this.Direction > 1 ? parseInt(this.ID.scrollWidth) : parseInt(this.ID.scrollHeight); this.ID.innerHTML += this.ID.innerHTML; this.IsNotOpera = (navigator.userAgent.toLowerCase().indexOf("opera") == -1); if (arguments.length >= 8) { this.Start(this, this.Timer, this.DelayTime, this.WaitTime); } }
Marquee10.prototype.Start = function (msobj, timer, delaytime, waittime) {
    msobj.StartID = function () { msobj.Scroll(); };
    msobj.Continue = function () { if (msobj.MouseOver == 1) { setTimeout(msobj.Continue, delaytime); } else { clearInterval(msobj.TimerID); msobj.CTL = 0; msobj.Stop = 0; msobj.TimerID = setInterval(msobj.StartID, timer); } };
    msobj.Pause = function () { msobj.Stop = 1; clearInterval(msobj.TimerID); setTimeout(msobj.Continue, delaytime); };
    msobj.Begin = function () {
        msobj.ID.onmousemove = function (evt) { if (msobj.ScrollStep == 0 && msobj.Direction > 1) { var event = null; if (window.event) { event = window.event; if (msobj.IsNotOpera) { msobj.EventLeft = event.srcElement.id == msobj.ID.id ? parseInt(event.offsetX) - parseInt(msobj.ID.scrollLeft) : parseInt(event.srcElement.offsetLeft) - parseInt(msobj.ID.scrollLeft) + parseInt(event.offsetX); } else { msobj.ScrollStep = null; return; } } else { event = evt; msobj.EventLeft = parseInt(event.layerX) - parseInt(msobj.ID.scrollLeft); } msobj.Direction = msobj.EventLeft > msobj.HalfWidth ? 3 : 2; msobj.AbsCenter = Math.abs(msobj.HalfWidth - msobj.EventLeft); msobj.Step = Math.round(msobj.AbsCenter * (msobj.BakStep * 2) / msobj.HalfWidth); } };
        msobj.ID.onmouseover = function () { if (msobj.ScrollStep == 0) { return; } msobj.MouseOver = 1; clearInterval(msobj.TimerID); };
        msobj.ID.onmouseout = function () { if (msobj.ScrollStep == 0) { if (msobj.Step == 0) { msobj.Step = 1; } return; } msobj.MouseOver = 0; if (msobj.Stop == 0) { clearInterval(msobj.TimerID); msobj.TimerID = setInterval(msobj.StartID, timer); } }; msobj.TimerID = setInterval(msobj.StartID, timer);
    }; setTimeout(msobj.Begin, waittime);
}
Marquee10.prototype.Scroll = function () { if (this.Correct == 0 && this.CTL > this.ClientScroll) { this.ClientScroll = (this.Direction > 1) ? Math.round(parseInt(this.ID.scrollWidth) / 2) : Math.round(parseInt(this.ID.scrollHeight) / 2); this.Correct = 1; } switch (this.Direction) { case 0: { this.CTL += this.Step; if (this.CTL >= this.ScrollStep && this.DelayTime > 0) { this.ID.scrollTop += (this.ScrollStep + this.Step - this.CTL); this.Pause(); return; } else { if (this.ID.scrollTop >= this.ClientScroll) { this.ID.scrollTop -= this.ClientScroll; } this.ID.scrollTop += this.Step; } break; } case 1: { this.CTL += this.Step; if (this.CTL >= this.ScrollStep && this.DelayTime > 0) { this.ID.scrollTop -= (this.ScrollStep + this.Step - this.CTL); this.Pause(); return; } else { if (this.ID.scrollTop <= 0) { this.ID.scrollTop += this.ClientScroll; } this.ID.scrollTop -= this.Step; } break; } case 2: { this.CTL += this.Step; if (this.CTL >= this.ScrollStep && this.DelayTime > 0) { this.ID.scrollLeft += (this.ScrollStep + this.Step - this.CTL); this.Pause(); return; } else { if (this.ID.scrollLeft >= this.ClientScroll) { this.ID.scrollLeft -= this.ClientScroll; } this.ID.scrollLeft += this.Step; } break; } case 3: { this.CTL += this.Step; if (this.CTL >= this.ScrollStep && this.DelayTime > 0) { this.ID.scrollLeft -= (this.ScrollStep + this.Step - this.CTL); this.Pause(); return; } else { if (this.ID.scrollLeft <= 0) { this.ID.scrollLeft += this.ClientScroll; } this.ID.scrollLeft -= this.Step; } break; } this.ID.scrollTop += "px"; this.ID.scrollLeft += "px"; } }


function liRoll() {
    var liNum = document.getElementById('mycarousel').getElementsByTagName('li').length;
    if (liNum > 10) {
        new Marquee10("hotjob", 0, 1, 180, 220, 60, 0, 1000);
    }
    else {
        if (liNum == 0) {
            document.getElementById("HotJobList").style.display = "none";
        }
    }
}


