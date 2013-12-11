<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        div
        {
           /* height: 100%;*/
        }
        #d1
        {
            width: 400px;
            border: #000 solid 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="d1">
        这个不兼容的问题，一想起来就很火大的。
在网上找来资料学习。
IE6，IE7，Firefox兼容的css hack
第一种办法：
body
{
background:red;
*background:blue !important;  
*background: green;
}

第一排给Firefox以及其他浏览器看；
第二排给IE7,IE7既能能识别*号，也能识别important；
第三排给IE6也能识别*号；
第二种办法，使用_来区分IE6：
body
{
background:red;
*background:blue;  
_background: green;
}

第一排给Firefox以及其他浏览器看；
第二排给IE7,IE7既能能识别*号；
第三排给IE6能识别下划线；
CSS对浏览器器的兼容性具有很高的价值，通常情况下IE和Firefox存在很大的解析差异，这里介绍一下兼容要点。
常见兼容问题：
　　1.DOCTYPE 影响 CSS 处理(但这个声明对于WEB标准的验证是非常重要的)
2.FF: div 设置 margin-left, margin-right 为 auto 时已经居中, IE 不行
3.FF: body 设置 text-align 时, div 需要设置 margin: auto(主要是 margin-left,margin-right) 方可居中
4.FF: 设置 padding 后, div 会增加 height 和 width, 但 IE 不会, 故需要用 !important 多设一个 height 和 width
5.FF: 支持 !important, IE 则忽略, 可用 !important 为 FF 特别设置样式
6.div 的垂直居中问题: vertical-align:middle; 将行距增加到和整个DIV一样高 line-height:200px; 然后插入文字，就垂直居中了。缺点是要控制内容不要换行
7.cursor: pointer 可以同时在 IE FF 中显示游标手指状， hand 仅 IE 可以
8.FF: 链接加边框和背景色，需设置 display: block, 同时设置 float: left 保证不换行。参照 menubar, 给 a 和 menubar 设置高度是为了避免底边显示错位, 若不设 height, 可以在 menubar 中插入一个空格。
9.在mozilla firefox和IE中的BOX模型解释不一致导致相差2px解决方法：
div{margin:30px!important;margin:28px;}
　　注意这两个margin的顺序一定不能写反，据阿捷的说法!important这个属性IE不能识别，但别的浏览器可以识别。所以在IE下其实解释成这样：
div{maring:30px;margin:28px}
　　重复定义的话按照最后一个来执行，所以不可以只写margin:XXpx!important;
10.IE5 和IE6的BOX解释不一致
IE5下
div{width:300px;margin:0 10px 0 10px;}
　　div的宽度会被解释为300px-10px(右填充)-10px(左填充)最终div的宽度为280px，而在IE6和其他浏览器上宽度则是以300px+10px(右填充)+10px(左填充)=320px来计算的。这时我们可以做如下修改
div{width:300px!important;width /**/:340px;margin:0 10px 0 10px}
　　关于这个/**/是什么我也不太明白，只知道IE5和firefox都支持但IE6不支持，如果有人理解的话，请告诉我一声，谢了！：）
11.ul标签在Mozilla中默认是有padding值的,而在IE中只有margin有值所以先定义
ul{margin:0;padding:0;}
　　就能解决大部分问题
注意事项：
1、float的div一定要闭合。
例如：(其中floatA、floatB的属性已经设置为float:left;)

　　这里的NOTfloatC并不希望继续平移，而是希望往下排。
这段代码在IE中毫无问题，问题出在FF。原因是NOTfloatC并非float标签，必须将float标签闭合。
    </div>
    </div>
    </form>
</body>
</html>
