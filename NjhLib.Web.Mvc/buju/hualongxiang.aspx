<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hualongxiang.aspx.cs" Inherits="NjhLib.Web.Mvc.buju.hualongxiang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /* CSS Document */
body,h1,h2,h3,h4,h5,h6,p,ul,ol,li,form,img,dl,dt,dd,table,th,td,blockquote,fieldset,div,strong,label,em{margin:0;padding:0;border:0;}
ul,ol,li{list-style:none;}
input,button{margin:0;font-size:12px;vertical-align:middle;}
body{font-size:12px;color:#333;font-family:simSun;}
em{font-style:normal;}
li{word-wrap:break-word;}
.blank10{clear:both;height:10px;overflow:hidden;}
a{text-decoration:none;color:#333;}
a:hover{text-decoration:underline;}
.clear{clear:both;}
.fl{float:left;}
.fr{float:right;}
.wall {width: 960px;margin: 0 auto;}
/*颜色*/
.color_333 {color: #333333;}
.color_666 {color: #666;}
.color_333 a:hover { text-decoration: none;}
.color_c81{ color:#C8171D;}
.color_blue{ color:#005EAC;}
.color_red{ color:#FF0000;}
.org {color:#FF6600;}
.m4{ margin: 0 4px;}
.xl{position:relative;top:-2px;left:2px;border:0;}
.x2{position:relative;top:2px;border:0;}
.x3{position:relative;left:2px;top:-5px;}
.x4{position:relative;top:4px;border:0;}
.x5 { position:relative; top: -7px;_top:-8px; left: 4px;}
.x6{position: relative;left: 2px;top: -1px;}
.x7 {position: relative;left: 4px;top: -3px;}
.x8 {position: relative;left: -4px;top: 1px; border:none;}
.mt{ *margin-top: -2px;margin-top: -2px\0;}
.mr10{margin-right:10px;}
.blank5 { clear:both; height:5px; overflow:hidden;}
.p9{ position:relative; left: 2px; top: -2px; border:0; _margin-top:18px;}
.f8{font-size:8px;}
.t_mes {position:relative;top:-1px;*top:-3px;_top:0px;top: -2px\0;}
@media screen and (-webkit-min-device-pixel-ratio:0) {.t_mes{top: 0\0;}} 
*html .t3{position:relative;top: 8px;}
.small_nav_bg{position:relative;z-index:999;width:100%;height:36px;line-height:36px;background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) 0 -631px repeat-x;}
/*导航*/
ul.small_nav{position:relative;float:left;font-size:14px;overflow:hidden;margin:0;padding:0;}
ul.small_nav li{float:left;display:inline;text-align:center;overflow:hidden;margin:0 1px 0 0;padding:0 5px;}
ul.small_nav li a{color:#005eac;}
/*右侧登录部分*/
#top-b{color:#999;float:left;margin:8px 0 0;}
ul.loginform{line-height:20px;color:#999;}
ul.loginform li{float:left;height:20px;line-height:20px;}
.name_frame {position:relative;float:left;width: 100px; height: 20px; border: 1px solid #ccc;  margin: 0 10px 0 0;_margin: 0 5px 0 0;background: #fff;}
.input_name{position:absolute;width:75px;height:15px;border:0;overflow:hidden;color:#CCC;padding:2px 3px;}
.input_pw{float:left;width:92px;height:16px;line-height:16px;border:1px solid #ccc;overflow:hidden;color:#CCC;margin:0 10px 0 0;padding:2px 4px;}
a.jiantou_img{float:right;width:13px;height:15px;cursor:pointer;overflow:hidden;background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) right -570px no-repeat;margin:2px 4px 0 0;}
a.jiantou_img:hover{background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) right -595px no-repeat;}
.log_in{display:inline-block;width:40px;height:22px;cursor:pointer;border:none;background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) 0 -212px no-repeat;}
.type_style {position:relative; top:2px; left: 2px;*left:0;}
/*同步登录*/
.loginWays{ float: left;margin: 0 10px;position: relative;text-align: left;width: 122px;}
.loginWays img{float:left;margin-top:4px;margin-right:4px;}
.loginWays .firstWay,.loginWays .text{display:block; padding-left:5px; color:#666; line-height:20px;}
.loginWays .firstWay{height:24px; line-height:25px; overflow:hidden;background:url(bg_loginways.png) no-repeat 0 0}
.loginWays .firstWay span{float:left; color:#333;}
.loginWays .firstWay span.iconDown{display:none}
.wb_menu{z-index: 1011;border:1px solid #d4d4d4; position:absolute;}
.wb_menuList{background:#fff;padding:1px;width:100px;}
.wb_menuList li{width:100%;}
.wb_menuList a{line-height:25px;height:25px;padding:0 10px;display:block;overflow:hidden;}
.wb_menuList a:hover{background:#e6f2fe url(menubg.png) repeat-x; text-decoration:none;color:#666;}
.wb_menuList img{ vertical-align:middle;white-space:nowrap;margin-left:-5px;_margin-top:4px;margin-right:5px;}
/*用户名选择框*/
.btn_xiala{position:relative;z-index:9999;margin:20px 0 0 -1px;}
ul.xiala{position:absolute;width:100px;background:#FFF;border:1px solid #ccc;font-size:14px;z-index:9999; padding:0;}
ul.xiala li{width:100px;height:22px;line-height:22px;text-indent:5px;overflow:hidden;margin:0;padding:0;}
ul.xiala li a{display:block;color:#333;}
ul.xiala li a:hover{background:#999;color:#FFF;text-decoration:none;}
/*密码隐藏部分内容*/
.pw_frame{position:relative;float:left;top:21px;left:-112px;}
.pw_hidd{position:absolute;width:174px;background:#FFF;overflow:hidden;border:1px solid #CCC;z-index:100;}
.pw_hidd p{padding:10px;}
.type_02{float:left;color:#333;height:18px;line-height:18px;font-size:12px;font-weight:700;}
.closeds{float:right;width:18px;height:18px;background:url(http://static.hualongxiang.com/style/cssV2/closed.gif) no-repeat;cursor:pointer;}
.pw_hidd select{width:154px;height:22px;border:1px solid #ccc;overflow:hidden;margin:10px 10px 5px;}
.pw_hidd input{width:152px;height:20px;border-top:1px solid #666;border-left:1px solid #666;border-right:1px solid #ccc;border-bottom:1px solid #ccc;margin:5px 10px;}
/*用户详细信息*/
.nav_right {float: right;margin: 0 10px 0 0 ;font-size: 12px;line-height: 22px;}
div.customer_mes {color:#666;float:left; margin:7px 0 0 10px;}
/*登陆之后的样式*/
.user_info{float:left;width:500px;padding:7px 0 0;}
.user_info dt{float:right;overflow:hidden;margin:-3px 0 0;}
.user_info dd{float:right;padding-top:0;text-align:left;}
.user_info dd a{color:#369;margin-left:2px;}
.user_info_level{float:left;margin-left:4px;padding:0;}
.user_info_level li{float:left;display:inline;margin:0 1px;}
*html .user_info_level li.mt8{margin-top:-8px;}
.img_28{background:url(http://static.hualongxiang.com/style/cssV2/face_28.gif) no-repeat scroll 0 0 transparent;display:block;height:24px;width:24px;margin:0 5px;padding:2px;}
/*全站首页弹出框样式nav_listbg 列表背景*/
.weie {	position:relative;display: block;margin: -20px 0 0 0;*margin: -30px 0 0 -1px;_margin: -10px 0 0 1px;}
@media screen and (-webkit-min-device-pixel-ratio:0) {.weie{margin: 0 0 0 0px; }} 
.tanchu_home {position:relative;height:33px;line-height:30px;background:#fff;text-align:center;font-size:14px;overflow:hidden;color:#005eac;cursor:pointer;z-index:1000;border:1px solid #d2d2d2;border-width:1px 1px 0;margin:-4px 0 0 -6px;padding:0 8px 0 5px;}
*html .tanchu_home{ width: 38px;margin:-14px 0 0 -7px;}
*html .big_tit { width: 68px;}
*html .mob_tit { width: 58px;}
.tanchu_home a { color:#005EAC;}
@media screen and (-webkit-min-device-pixel-ratio:0) {.tanchu_home{	margin:-23px 0 0 -6px;}} 
.nav_listbg {position:absolute;line-height: 22px;font-size: 14px;left: -6px;_left: -7px;border: 1px solid #d2d2d2;border-width: 0 1px 1px;}
ul.list_cc {float: left;width: 90px;padding: 5px;z-index: 10000;letter-spacing: 0;word-wrap:break-word;background:#fff;}
ul.list_cc li {float: left;display:block;padding: 3px;*padding: 3px 2px;color:#ccc;border-bottom: 1px dotted #ccc;}
ul.list_cc li.b_none { border:none; padding-bottom:0;}
ul.list_cc li span{float:left;margin:0 0 0 3px;}
ul.list_cc li a{display:block;word-wrap:break-word;white-space:nowrap;color:#005EAC;}
ul.list_cc li a span{padding:0 3px;}
ul.list_cc li a:hover{background:#005EAC;color:#fff;text-decoration:none;}
ul.list_cc li a:hover span{background:#005EAC;color:#fff;text-decoration:none;padding:0 3px;}
/*menu*/
.menu{position:absolute;}
/*下拉框*/
.menu-post {position:absolute;border: 1px solid #cccccc;font-size: 12px;margin: -19px 0 0 4px;*margin: -19px 0 0 3px;_margin: -19px 0 0 4px;background: #fff;}
@media screen and (-webkit-min-device-pixel-ratio:0) {.menu-post{ margin: -18px 0 0 4px;}}
.menu-post a {display:block;color: #005eac;padding:4px 8px;}
.menu-post a: hover {text-decoration: underline;}
.myhome_weizhi {display:block;margin: -19px 0 0 0px; *margin: -19px 0 0 0px;_margin: -19px 0 0 2px;margin: -19px 0 0 2px\0;font-size: 12px;}
@media screen and (-webkit-min-device-pixel-ratio:0) {.myhome_weizhi{ margin: -18px 0 0 0px;}}
.menu_topup2{position:absolute;border:1px solid #ccc;background:#fff;border-width:0 1px 1px;}
.menu_topup2 li{overflow:hidden;}
.menu_topup2 a {color: #333333;padding: 4px 6px;_padding: 6px 6px 2px;display: block;line-height: 18px;height: 18px;overflow: hidden;}
.menu_topup2 a:hover{background:#e6f2fe;text-decoration:none;}
.menu_topup2 img{vertical-align:middle;margin-right:5px;}
.menu_topup2 b{margin-right:5px;padding:0 8px;}
.menu_topup3{position:absolute;border:1px solid #ccc;background:#fff;border-width:0 1px 1px;margin:0;}
.menu_topup3 li{overflow:hidden;}
.menu_topup3 a {color: #005eac;padding: 4px 6px;_padding: 6px 6px 2px;display: block;line-height: 18px;height: 18px;overflow: hidden;}
.menu_topup3 a: hover{text-decoration:none;}
.menu_topup3 img{vertical-align:middle;margin-right:5px;}
.menu_topup3 b{margin-right:5px;padding:0 8px;}
.tacs{text-align:center;display:block;width:40px;height:25px;border:1px solid #ccc;color:#005eac;background:#fff;border-width:1px 1px 0;padding:4px 2px 0 0;}
.mymes_weizhi {display:block;margin: -19px 0 0 0;*margin: -20px 0 0 0;_margin: -19px 0 0 -2px;}
@media screen and (-webkit-min-device-pixel-ratio:0) {.mymes_weizhi{margin: -18px 0 0 0; }} 
.m_num{height:12px;background:#F90;color:#FFF;margin:0 0 0 4px;padding:0 3px;}
/*大头里面*/
.teshu_header{position:relative;z-index:0;width:100%;margin:0 auto;}
.teshu_header h1{float:left;width:160px;text-indent:-10000px;}
.teshu_header h1 a{display:block;width:160px;height:34px;cursor:pointer;background:url(http://static.hualongxiang.com/style/cssV2/logo.gif) no-repeat;margin:15px 15px 15px 0;}
/*分站样式*/
.subSite{padding-left:10px;padding-top:15px;white-space:nowrap;margin:0 10px 0 0;}
.currCity{font-size:16px;font-weight:700;}
.citySel{margin-top:5px;}
.cityLink{color:#005EAC;}
.citys{position:relative;width:120px;margin:-19px 0 0 -5px;}
.subSite_t{background:#FFF;color:#005EAC;cursor:pointer;display:inline-block;height:20px;line-height:20px;margin-bottom:-1px;text-align:center;width:80px;border-color:#CCC;border-style:solid solid none;border-width:1px 1px 0;}
.citys ul{background:#FFF;border:1px solid #CCC;padding:10px;}
.citys ul li{display:inline;}
.citys ul li a{color:#005EAC;display:inline-block;margin:0 3px 3px;padding:2px 3px;}
.citys ul li a:hover{background:#005EAC;color:#FFF;}
.contactNumber{padding-top:10px;width:150px;line-height:22px;}
.contactNumber em{color:#f60;font-weight:700;}
.biz_qq{ float:left; margin:26px 10px 0; width:102px; height:24px; overflow:hidden;}
/*搜索*/
.header_search_box{position:relative;float:right;width:400px;height:40px;background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) 0 -260px no-repeat;margin:15px 0 0;}
.search_frame {float: left;width: 240px;height: 18px;*height: 16px;padding: 3px 2px;margin: 7px 0 0 28px;*margin: 8px 0 0 28px;margin: 7px 0 0 28px\0;border: 0;color:#999;}
.search_select {position:relative;float: left;left:5px;top:11px;border-left: 1px solid #ccc;}
.search_select .box{ position:relative; width:40px; height:16px; background: url(bbs_header_bg.png) no-repeat #ffffff right -548px; cursor:pointer; padding: 0 0 0 10px;*padding: 2px 0 0 10px;
padding: 2px 0 0 10px\0;color: #999;}
.search_select ul{position:absolute;list-style:none;top:16px;left:-1px;width:54px;border:1px solid #ccc;background:#fff;text-indent:6px;border-width:1px 1px 0;}
.search_select ul li{ border-bottom: 1px solid #ccc;}
.search_select ul li a { display:block;height: 22px; line-height:22px; color:#333333;}
.search_select ul li a:hover{ background: #E6F2FE; text-decoration:none;}
.search_select .show{ display:none;}
.btn_search{float:left;width:50px;height:38px;margin-left:18px;overflow:hidden;cursor:pointer;border:none;background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) 0 -322px no-repeat;}
/*头部*/
.bbs_nav_left,.bbs_nav_right,.bbs_nav_wrap,.bbs_nav li.current,.bbs_nav li.current a,.bbs_nav li a:hover,.bbs_nav li a:hover span,.bbs_nav li{background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) repeat-x;}
.bbs_nav_wrap{ height:36px;background-color:#668ec1; background-position:0 0;}
.bbs_nav_left{background-position:1px -90px; width:4px; height:36px}
.bbs_nav_right{background-position:right -90px; width:4px; height:36px;}
.bbs_nav{display:block; height:36px; width:98%;}
.bbs_nav li{float:left;font-size:14px; display:block; background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) right -144px no-repeat; margin:1px 3px 0; line-height:35px;}
.bbs_nav li a{color:#fff;font-weight:700; float:left;}
.bbs_nav li a:hover{text-decoration:none;}
.bbs_nav li a:visited{color:#f7fbff;}
.bbs_nav li a span{padding:0 15px;cursor:pointer;}
.bbs_nav li a span img{ padding:15px 0 16px;}
.bbs_nav li.current{background-position:left -39px;}
.bbs_nav li.current a{background-position:right -39px;color:#000;}
.bbs_nav li.current a:hover{background-position:left -39px;color:#000;}
.bbs_nav li.current a:visited{color:#333;}
.bbs_nav li.current a:hover span{background-position:right -39px; float:left; display:inline;}
.bbs_nav li a:hover{background-position:left -39px;color:#000;}
.bbs_nav li a:hover span{background-position:right -39px; float:left; display:inline;}
.bbs_nav li.back a:hover{background-position:left -423px;text-decoration:none;}
.bbs_nav li.back a:hover span{background-position:right -423px; float:left; display:inline;}
.bbs_nav .down{margin:15px 0 0 5px;}
.bbs_nav_b{background:url(http://static.hualongxiang.com/style/cssV2/bbs_nav_listbg.gif) no-repeat 0 bottom;padding-left:5px;}
.bbs_nav_bot{height:1px;overflow:hidden;display:block;background:#d5d5d5;}
.bbs_nav_pd{overflow:hidden;width:953px;}
.bbs_nav_b ul{padding:2px 0 0;margin-left:-1px;}
.bbs_nav_b li{float:left; background-position:left -338px;line-height:25px;padding:0 10px 5px; display:block;white-space:nowrap;}
.bbs_nav_b li a{display:block;color:#333333;font-size:14px;height:25px;overflow:hidden;float:left;}
.bbs_nav_b li a:hover{text-decoration:none;}
.bbs_nav_b li.current a{text-decoration:none;}
.nav_home_down{margin:7px 0 0 10px;*margin: 7px 10px 0 0;overflow:hidden;line-height:24px;color:#ffffff;width:100px; background:url(http://static.hualongxiang.com/style/cssV2/gohome.gif) 0 1px no-repeat; overflow:hidden;float:right;}
.nav_home_down i{ font-style:normal;cursor:pointer;padding:0 5px 0 15px;display:block;float:left;}
.nav_home_down .d_white_up{margin:9px 10px 0 0;}
.nav_home_down:hover{ text-decoration:none;}
/*down*/
.down_top{display:block;width:0px;height:0px;overflow:hidden;float:right;padding:0;margin:14px 8px 0 0;}
.d_white_on,.d_white_up{border-left:4px transparent dotted;border-right:4px transparent dotted;}
.d_white_on{border-bottom:0px transparent dotted;border-top:4px #000 solid;}
.d_white_up{border-bottom:0px transparent dotted;border-top:4px #ffffff solid;}
.nav_topup{border:1px solid #7ca4d1;background:#fff;margin-top:-25px;border-top:1px solid #7ca4d1;}
.nav_topup li{overflow:hidden;}
.nav_topup a{color:#333333;padding:4px 6px;_padding:6px 6px 2px;display:block;line-height:18px;height:18px;overflow:hidden;}
.nav_topup a:hover{background-color:#e6f2fe; text-decoration:none;}
.nav_topup img{ vertical-align:middle;margin-right:5px;}
.nav_topup b{padding:0 8px;margin-right:5px;}
.nav_tac{text-align:center;border-bottom:1px solid #c0d6df;color:#369;margin:0 3px;padding:5px 0;}
/*类别*/
.category p{width:16px;font-size:14px;color:#0058B0;position:relative;top:5px; font-weight:700; line-height: 20px;margin:0 0 0 12px;}
.category_left{background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) 0 -388px no-repeat;width:45px;height:52px;float:left;}
.category ul.mr0{margin-right:0;}
.category ul{height:49px;float:left;background:url(http://static.hualongxiang.com/style/cssV2/bbs_header_bg.png) right -459px no-repeat;margin-right:18px;padding:3px 0 0;}
.category ul.width220{width:220px;}
.category ul.width100{width:104px;}
.category ul.width130{width:130px;}
.category ul li{float:left;font-size:14px;display:inline;height:23px;line-height:23px;padding:0 7px;*padding:0 4px;}
.category ul li a{color:#666;}
.category ul li a:hover{color:#005EAC;text-decoration:none;}
.category ul li.h_icon{position:relative;}
.category ul li img.new_icon{position:absolute;top:4px;}
.category ul li.h_icon a,.category ul li.reds a{color:#F30;}
/*--返回顶部--*/
#scrollBar a{background:url(http://static.hualongxiang.com/style/cssV2/top.png) no-repeat scroll 0 0 transparent;cursor:pointer;display:block;height:57px;overflow:hidden;text-indent:-2000em;width:19px;}
#scrollBar a:hover{background-position:right 0;}


#f_tips{ position:relative; width:960px; font-size:12px;color:#e10000; margin: 0 auto; padding:5px;font-weight:700; border:1px solid #F27C05; background:#fff;}
#f_tips a.color_e1{color:#e10000;}
#f_tips a.guanbi{ position: absolute; display:block; cursor: pointer;width: 8px;height: 7px;background:url(http://static.hualongxiang.com/images/wind/login_icon.png) no-repeat right -198px;right: 8px;top: 7px;text-indent: -9999px;overflow: hidden;}

    /*网站首页*/

/*广告大小*/

a:hover{text-decoration:underline;}

.ad_430{width:430px;height:40px;overflow:hidden;background:#CCCCCC;}

.ad_310_80 ul li.ad-0{margin-bottom:-2px;*+margin-bottom:3px;margin-bottom:5px\0;}

/*顶部广告*/

#topad{text-align:center;}

/*左侧主体*/

#major{width:700px;float:left;margin:0;overflow:hidden;}

#major li{margin:0 0 4px 0;}

/*右侧广告*/

#minor{float:left;width:255px;margin:0 0 0 5px;text-align:center;}

#minor li{margin:0 0 4px 0;}

#bbs_ads{width:960px;margin-bottom:6px;overflow:hidden;}

#bbs_ads li{margin:0 0 4px 0;}

/*焦点*/

#focus{height:240px;clear:both;}

#focus-play{width:500px;height:230px;overflow:hidden;float:left;position:relative;}

#play-bg{position:absolute;z-index:90;height:60px;width:400px;left:0;top:170px;background:#333;filter:alpha(opacity=80);-moz-opacity:0.8;opacity:0.8;}

#play-box{width:400px;float:left;height:230px;}

#play-box .banner{width:100%;height:100%;}

#play-box img{position:absolute;z-index:1;width:400px;height:230px;}

#play-box h1{font:18px/28px "黑体";font-family:"Microsoft YaHei","黑体";color:#FFF;position:absolute;left:8px;top:170px!important;*top:175px!important;top:175px;z-index:100;text-indent:0;width:380px;}

#play-box h1 a:link,#play-box h1 a:visited{font:18px/28px "黑体";color:#ccc}

#play-box h1 a:hover,#play-box h1 a:active{color:#FFF;text-decoration:underline}

#play-box h2{width:380px;font:12px/20px "宋体";font-family:"Microsoft YaHei","黑体";color:#aaa;padding-right:10px;position:absolute;left:8px;top:200px!important;*top:200px!important;top:200px;z-index:100}

#play-box h2 a:link,#play-box h2 a:visited{color:#77C04B}

#play-box h2 a:hover,#play-box h2 a:active{color:#77C04B}

#play-sel{width:92px;float:left;margin-left:8px;}

#play-sel a{display:block;width:100%;height:58px;text-align:center;}

#play-sel a.active{background:url(sel.jpg);}

#play-sel img{width:74px;height:46px;margin:5px 5px 5px 10px;}

#focus-news{margin-left:510px;height:230px;border-left:#ccc dashed 1px;padding-left:15px!important;padding-left:12px;overflow:hidden;width:430px;}

#focus-news h2{font-size:18px;padding:5px 0;margin:0;line-height:1.2em;font-family:"Microsoft YaHei","黑体";font-weight:bold;}

#focus-news h2 a{color:#000;}

#focus-news p{line-height:20px;color:#666;padding:0 0 9px 0;margin:0 0 12px 0;}

#focus-news p.border{border-bottom:#ccc solid 1px;}

#focus-news p a{color:#CF0C12;}

#focus-news .banner{height:45px;}

/*左侧导航*/

#nav{width:118px;float:left;clear:both;overflow:hidden;background:#efefef;}

/*标签式标题*/

#nav h4{font-weight:bold;font-size:12px;margin:0;padding:0;line-height:20px;background:url(n.gif) 0 -15px repeat-x;display:block;text-indent:1em;}

#nav h4 a{color:#FFFF00;}

#nav div{border:#CDCDCD solid 1px;background:url(nav.gif);}

#nav span{background:url(icon.gif) repeat-x;height:1px;overflow:hidden;display:block;margin:0 5px;}

#nav dl{margin:0 5px;}

#nav dt{margin:0px 5px;background:url(n.gif) -125px -27px no-repeat;text-indent:18px;line-height:25px;color:#000;cursor:pointer;}

#nav dt.active{background:url(n.gif) -125px -7px no-repeat;}

#nav dd{margin:5px;}

#nav dd li{line-height:20px;background:url(n.gif) -125px -173px no-repeat;text-indent:20px;}

#nav dd li.last{background:url(n.gif) -125px -153px no-repeat;}

#nav dd li a{*margin:0 0 0 -2px;}

#nav dd li a:link,#nav dd li a:visited{color:#575757;}

#nav dd li a:hover{color:#333;}

/*右侧主体*/

#channel{margin-left:12px;float:left;width:830px;background_:#ccc;overflow:hidden;}

/*焦点访·坛*/

#channel-focus{width:510px;float:left;background_:red;overflow:hidden;background_:blue;}

#channel-focus .title{line-height:22px;background:url(icon.jpg) 0 0 repeat-x;padding:0 8px;margin-bottom:10px;}

#channel-focus .title strong{color:#CC0001;background:url(icon.jpg) -440px -582px no-repeat;padding-left:15px;font-size:13px;}

#channel-focus .title span{float:right;}

#channel-focus .title img,#channel-focus .title embed,#channel-focus .title iframe{vertical-align:middle;}

#channel-focus .main{line-height:20px;}

.jd_content{border:1px solid #DCDDDD;border-top:0;padding:10px 0 5px 0;}

#jd_title{background:url(jd_bg.png) repeat-x 0 -51px;height:29px;border:1px solid #DCDDDD;border-width:1px 1px 0 1px;}

#jd_tab{float:left;width:310px;}

#jd_tab li{float:left;padding:0 11px;border-right:1px solid #DCDDDD;height:29px;line-height:28px;font-size:14px;cursor:pointer;}

#jd_tab li.current{background:url(jd_bg.png) repeat-x;color:#CC0001;font-weight:bold;}

.jd_gg{margin:6px 0 0 0;float:left;width:190px;}

.jd_gg img{float:right;}

/*帖子列表*/

#channel-focus .main ul{padding-bottom:5px;margin:0 10px 5px;}

#channel-focus .main ul.h24{border-bottom:#ccc dashed 1px;}

#channel-focus .main ul.h48 li span{color:#999;margin:0;}

#channel-focus .main ul.h48 li span.org{font-family:Arial;font-size:14px;color:#f60;margin-right:10px;}

#channel-focus .main ul.h48 li span.org a{color:#f60;}

#channel-focus .main ul.h48 li a{padding:0;font-size:14px;color:#333;background:none;}

#channel-focus .main ul.none{margin:0 5px;padding:0;border:none;}

#channel-focus .main li{line-height:25px;height:25px;overflow:hidden;}

#channel-focus .main li span{font-size:12px;color:#004475;margin-left:10px;}

#channel-focus .main li a{background:url(icon.jpg) -438px -648px no-repeat;padding-left:20px;font-size:14px;color:#333;}

#channel-focus .main .hr{height:0;border-bottom:#ccc dashed 1px;margin:5px 0;overflow:hidden;}

#channel-focus .main .fc{background:#FDF4E3;color:#CB0100;padding:2px 10px;line-height:auto;margin:8px 0;}

#channel-focus .main .fc span{float:right;}

#channel-focus .main .st{background:#F9F9F9 url(icon.jpg) -380px -748px no-repeat;color:#CB0100;border:#C8C8C8 solid 1px;font-size:13px;padding:5px 10px 5px 80px;height:45px;line-height:23px;margin-top:8px;+margin-top:5px;}

#channel-focus .main .st a{margin:0 4px;display:inline-block;}

/*分类信息*/

#channel-slide{float:left;margin-left:10px;width:310px;overflow:hidden;line-height:22px;background_:red; *margin-bottom:5px;margin-bottom:8px\0;}
#channel-slide .mr15{ margin-right:15px;*margin-right:13px;}
#channel-slide .box1{width:308px; height:300px; overflow:hidden; background:#fff;}
#channel-slide ul.frame_hd{ height:38px;}
#channel-slide ul.frame_hd li{ position:relative; float:left; display:inline-block; width:98px; height:32px; line-height:32px; margin-right:7px; text-align:center; font-weight:700; z-index:2;}
#channel-slide ul.frame_hd li.pu{ background:#7C88C5;}
#channel-slide ul.frame_hd li.org{ background:#F27B04;}
#channel-slide ul.frame_hd li.qgre{ background:#6CB028;}
#channel-slide ul.frame_hd li.qblue{ background:#09c; margin-right:0;}
#channel-slide ul.frame_hd li.blue{ background:#1E68BA;}
#channel-slide ul.frame_hd li.green{ background:#019454; margin-right:0;}

#channel-slide ul.frame_hd li a{ display:block;color:#fff; font-size:16px;}
#channel-slide ul.frame_hd li a:hover{ text-decoration:none;}
#channel-slide ul.frame_hd li.on{ width:96px; height:37px;_margin-bottom:-1px; border:1px solid #dfdfdf; border-bottom:1px solid #fff; background:#fff;}
#channel-slide ul.frame_hd li.on a{ color:#09c;}

#channel-slide .frame_search{  position:relative;display: inline-block;width:50px; height:24px; text-align:center; margin-left:5px; background:url(search.png) no-repeat; border:none; cursor:pointer; text-indent:-999px; }
#channel-slide .frame_item{position:relative;  border:1px solid #dfdfdf; padding:10px; height:198px;z-index:1;}
#channel-slide .frame_item ul{ margin: 0;}

#channel-slide .frame_item ul li.search{ margin-bottom:5px;}

#channel-slide .frame_item ul li.search em,#channel-slide .frame_item ul li.search input{ float:left;}
#channel-slide .frame_item ul li.search input.txt{ width:160px;_width:150px; height:18px;}
#channel-slide .frame_item ul li{ line-height:27px;}
#channel-slide .frame_item ul li em{ float:left; color:#666;}
#channel-slide .frame_item ul li p{ float:right; width:238px;}
#channel-slide .frame_item ul li p a{ float: left; display:block;margin-right: 4px;padding: 2px; white-space: nowrap;}
#channel-slide .frame_item ul li.bot_bor{ float:left; display:block; padding-bottom:4px; margin-bottom:4px; border-bottom:1px dashed #ccc;line-height:16px;}
#channel-slide .frame_item ul li.none{ border:none;}

/*分类信息*/

#channel-slide_hd{margin-left:0px!important;margin-left:0px;width:310px;overflow:hidden;line-height:22px;background_:red;}

/*商家活动促销*/

#channel-biz{clear:both;}

#channel-biz dt{height:32px;background:url(icon.jpg) 0 -530px no-repeat;}

#channel-biz dd{border:#CCCCCC solid 1px;border-top:none;padding:10px;overflow:hidden;}

#channel-biz p{padding:5px;height:70px;margin:0;line-height:22px;color:#666;}

#channel-biz p img{width:70px;height:70px;float:left;margin-right:10px;}

#channel-biz p a{color:#BA2636;}

#channel-biz p strong a{color:#2B2B2B;}

#channel-biz ul{margin:0 5px;}

#channel-biz ul.br{border-bottom:#DDDDDD dashed 1px;padding-bottom:5px;margin-bottom:5px;}

#channel-biz ul li{line-height:22px;}

#channel-biz ol{margin:0;padding:0;}

#channel-biz ol li{margin:3px 0;padding:0;text-align:center;}

/*帖子热点*/

#channel-hot{border:#ccc solid 1px;margin:0 0 5px 0;background:url(icon.jpg) 0 -460px no-repeat;}

#channel-hot strong{color:#0061B8;margin:5px 0 0 10px;display:block;}

#channel-hot ul{margin:0;padding:0 5px 4px 5px;line-height:22px;background:#fff url(icon.jpg) -420px -168px no-repeat;}

#channel-hot ul li{margin:0 5px;padding-left:25px;color:#004475;}

/*县区资讯*/

#channel-area{border:#ccc solid 1px;margin:0 0 5px 0;background:url(icon.jpg) 0 -460px no-repeat;overflow:hidden;}

#channel-area strong{color:#0061B8;margin:5px 0 0 10px;display:block;}

#channel-area p{color:#666;line-height:20px;margin:0;padding:10px;height:60px;background:#fff;width:288px;}

#channel-area p img{width:90px;height:65px;float:left;margin-right:10px;}

#channel-area p b a{color:#333333;font-size:13px;}

#channel-area p a{color:#BA2636;}

#channel-area ul{margin:0;padding:5px;line-height:17px;}

#channel-area ul li{margin:5px;color:#004475;}

/*活动月历*/

#channel-calendar{clear:both;padding-top:35px;background:url(icon.jpg) 0 -530px no-repeat;height:207px;}

#channel-calendar div.title{margin:5px 25px;text-align:center;}

#channel-calendar div.title span.prev{float:left;}

#channel-calendar div.title span.next{float:right;}

#channel-calendar div.page{margin:5px 10px;}

#channel-calendar div.page span{float:right;}

#channel-calendar div.page a{color:red;text-decoration:underline;}

#channel-calendar strong{color:#FE6402;}

#channel-calendar ul{margin:5px;}

#channel-calendar li{border-bottom:#ccc dashed 1px;padding-left:5px;line-height:23px;}

#channel-calendar li span{float:right;}

/*专题活动*/

#channel-event{margin:10px 0;clear:both;overflow:hidden;background_:red;}

#event-show{line-height:22px;width:520px;margin:0;float:left;border-top:#F7F7F7 solid 2px;padding-top:5px;background:none;height:auto}

#event-show .item{width:172px;text-align:center;float:left; height:180px;}

#event-show .item img{width:156px;height:140px;border:#DADADA solid 1px;padding:4px;}

#event-show .item input{background:url(icon.jpg) -140px -270px;width:114px;height:21px;margin-top:6px;cursor:hand;border:none;}

#event-show p{margin:0 0 0 10px;width:290px;float:left;}

#event-show ul{float_:left;}

#event-show ul li{float:left;width:90px;margin:5px;text-align:center;height:100px}

#event-show ul li img{width:80px;height:60px;border:#ccc solid 1px;padding:2px;}

#event-note{margin-left:535px;}

#event-note dl{}

#event-note dl dt{line-height:15px;padding-left:20px;background:#DADADA url(icon.jpg) 0 -500px;font-weight:bold;color:#fff;}

#event-note dl dd{overflow:hidden;}

#event-note p{margin:11px 0;padding:0;color:#333333;height:70px;line-height:18px;}

#event-note img{width:88px;height:66px;float:left;margin-right:10px;border:#ccc solid 1px;padding:2px;}

/*滑动内容*/

#channel-scroll{margin:10px 1px;clear:both;overflow:hidden;}

#channel-scroll .title{line-height:30px;border-bottom:1px dashed #cccccc;}

#channel-scroll .title strong{color:#CC0001;font-size:16px;}

#channel-scroll ul{padding:10px 0;margin:5px 0;line-height:20px;overflow:hidden;position:relative;width:100%;/*height:150px;*/

 text-align:center;}

#channel-scroll ul li{float:left;display:inline-block;width:180px;margin:0 25px 0 0;overflow:hidden;}

#channel-scroll ul li img{display:block;width:174px;height:93px;padding:2px;border:#ccc solid 1px;margin:0 0 5px 0;}



/*精选内容*/

#channel-board{margin:10px 0;clear:both;overflow:hidden;}

#channel-board div.clear{height:1px;overflow:hidden;font-size:0;line-height:0;}

#channel-board dl{width:408px;margin_:0 5px 10px 5px;margin:0 10px 10px 0;padding:0;float:left;overflow:hidden;background:#fff;border:#DEDEDE solid 1px;}

#channel-board dl.right{margin-right:0;}

#channel-board dt{font-size:14px;line-height:20px;font-weight:bold;background:url(icon.jpg) 0 -80px repeat-x;padding:5px;margin-bottom:5px;}

#channel-board dt strong{color:#CC0001;background:url(icon.jpg) -425px -540px no-repeat;padding-left:25px;font-size:13px;}

#channel-board dt span{float:right;}

#channel-board dt img{height:20px;}

#channel-board dt a{color:#10599D;}

#channel-board dd{padding:10px;margin:0;line-height:25px;}

#channel-board dd p{padding:10px;margin:0;border:#d5d5d5 solid 1px;background:#F8F8F8;overflow:hidden;line-height:20px;}

#channel-board dd p strong{font-size:14px;display:block;}

#channel-board dd p a{color:#CD0100;}

#channel-board dd ul{margin:10px 0;padding:5px;padding:0;}

#channel-board dd ul li{background:url(icon.gif) -442px -674px no-repeat;color:#004475;text-indent:12px;}

#channel-board dd ul li a{font-size:14px;}

#channel-board dd ul li span{margin-left:20px;}

#channel-board dd ol{padding:10px;border-top:#E8E8E8 solid 1px;overflow:hidden;clear:both;}

#channel-board dd ol li{float:left;width:116px;margin:0 5px 0 0;text-align:center;}

#channel-board dd ol li img{width:110px;height:80px;border:#ccc solid 1px;padding:2px;display:block;}

/*ad*/

.channel-ad{ clear:both; text-align:center;}

.channel-ad div{margin-bottom:5px;}	

/*好房网api 样式 开始  改动*/

.houseinfo_content{padding:5px 8px;}

#channel-slide .houseinfo_content ul{clear:both;overflow:hidden;font-size:14px;line-height:30px;}

.houseinfo_content li{ float:left;height:30px;line-height:30px;width:290px;overflow:hidden;border-bottom:1px dashed #DADADA;}

.houseinfo_content li a{ float:left;margin:0 10px 0 0;color:#0449BE;display:inline-block;width:100px;height:30px; overflow:hidden;}

.houseinfo_content li span{float:left;margin:0 5px 0 0;color:#666;width:52px;display:inline-block;font-family:Arial; height:30px; overflow:hidden;}

.houseinfo_content li span.color_3e{color:#3E3E3E}

.houseinfo_content li span.color_3e em{ position:relative;top:-5px; left:2px; font-size:8px;}

.houseinfo_content li strong{float:left;color:#FF6600;display:inline-block;text-align:right;width:66px;font-family:Arial;height:30px; overflow:hidden;}

.houseinfo_content li strong.new_h_a{width:100px;}

.houseinfo_content li a.new_h_ad{width:116px;}

.api_box{border:1px solid #DADADA;float:left;height:221px;overflow:hidden;width:308px;}

.api_title{background:url(http://static.hualongxiang.com/style/indexv2/style/api_title.gif) repeat-x scroll 0 0 transparent;height:31px;line-height:31px;overflow:hidden;padding:0 0 0 10px;}

.api_title span{float:left;font-family:Microsoft YaHei,SimHei;font-size:18px;}

.api_title span a.haofang{color:#669900;}

.api_title ul{border-left:1px solid #DADADA;display:block;float:left;margin-left:10px;}

.api_title ul li{border-right:1px solid #DADADA;color:#666666;float:left;font-size:14px;font-weight:bold;width:68px;text-align:center; overflow:hidden;}

.api_title ul li a{text-decoration:none;color:#666; width:68px; display:inline-block;}

.api_title ul li.current{background:none repeat scroll 0 0 #FFFFFF;color:#000000;cursor:pointer;}

.api_title ul li.current a{color:#000;}

.bbs_gg{ width:310px; height:80px; background-color:#F0F0F0; }

	
    </style>
</head>
<body>
 <!--左侧导航_开始-->
  <div id="nav">
    <h4><a target="_blank" href="http://bbs.hualongxiang.com/index.php" title="论坛列表导航" class="strong">论坛列表导航</a></h4>
    <div><dl rel="block">
	<dt class="active" title="互动常州">互动常州</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=103" target="_blank" title="龙城茶座">龙城茶座</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=104" target="_blank" title="谈天说地">谈天说地</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=100" target="_blank" title="交友征婚">交友征婚</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=101" target="_blank" title="情感天空">情感天空</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=502" target="_blank" title="化龙真我SHOW">化龙真我SHOW</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1054" target="_blank" title="娱乐八卦">娱乐八卦</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=107" target="_blank" title="影视部落">影视部落</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1603" target="_blank" title="通讯运营商">通讯运营商</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="生活娱乐">生活娱乐</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=203" target="_blank" title="龙城美食">龙城美食</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=2122" target="_blank" title="爱晒主义">爱晒主义</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1041" target="_blank" title="潮流前线">潮流前线</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=136" target="_blank" title="美颜修身">美颜修身</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=306" target="_blank" title="游山玩水">游山玩水</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1068" target="_blank" title="上班这点事">上班这点事</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=707" target="_blank" title="家有宠物">家有宠物</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=305" target="_blank" title="体育健身">体育健身</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=2097" target="_blank" title="玩乐一族">玩乐一族</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="网购乐翻天">网购乐翻天</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1311" target="_blank" title="服饰区">服饰区</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1313" target="_blank" title="食品区">食品区</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1312" target="_blank" title="美容区">美容区</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1310" target="_blank" title="箱包鞋帽">箱包鞋帽</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1314" target="_blank" title="饰品天地">饰品天地</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1315" target="_blank" title="家居家饰">家居家饰</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1316" target="_blank" title="杂货区">杂货区</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1022" target="_blank" title="跳蚤市场">跳蚤市场</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="信息发布">信息发布</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1090" target="_blank" title="商业信息广场">商业信息广场</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=704" target="_blank" title="招聘求职">招聘求职</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=300" target="_blank" title="教育培训">教育培训</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=706" target="_blank" title="房屋信息">房屋信息</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1067" target="_blank" title="二手车租车">二手车租车</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1618" target="_blank" title="家教信息">家教信息</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="住在常州">住在常州</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1549" target="_blank" title="楼市速递">楼市速递</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=202" target="_blank" title="我要买房">我要买房</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1261" target="_blank" title="业主小区">业主小区</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="家居生活">家居生活</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=206" target="_blank" title="装修讨论">装修讨论</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1253" target="_blank" title="家装建材">家装建材</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1212" target="_blank" title="家具软装">家具软装</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1014" target="_blank" title="家用电器">家用电器</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1210" target="_blank" title="家装日记">家装日记</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1528" target="_blank" title="家装设计会所">家装设计会所</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1331" target="_blank" title="商家直通车">商家直通车</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="龙城车友">龙城车友</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1902" target="_blank" title="学车俱乐部">学车俱乐部</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1903" target="_blank" title="网上4S店">网上4S店</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1218" target="_blank" title="我要买车">我要买车</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=201" target="_blank" title="龙城车友">龙城车友</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1619" target="_blank" title="汽车维修、保养、改装">汽车维修</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=2123" target="_blank" title="车友俱乐部">车友俱乐部</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="我要结婚">我要结婚</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=98" target="_blank" title="谈婚论嫁">谈婚论嫁</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1620" target="_blank" title="咔嚓瞬间">咔嚓瞬间</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1031" target="_blank" title="婚庆喜铺">婚庆喜铺</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1546" target="_blank" title="婚嫁品牌商家">婚嫁品牌商家</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1038" target="_blank" title="婚姻生活">婚姻生活</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="妈妈宝宝">妈妈宝宝</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1225" target="_blank" title="准妈俱乐部">准妈俱乐部</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=99" target="_blank" title="亲子俱乐部">亲子俱乐部</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1217" target="_blank" title="无敌宝宝秀">无敌宝宝秀</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1939" target="_blank" title="母婴用品">母婴用品</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="电脑·数码">电脑·数码</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=204" target="_blank" title="电脑·本本">电脑·本本</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1076" target="_blank" title="数码玩家">手机数码玩家</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1025" target="_blank" title="电脑数码配件交易">电脑数码配件</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1948" target="_blank" title="Android智能专区">Android智能</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1945" target="_blank" title="苹果粉丝俱乐部">苹果粉丝俱乐</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1026" target="_blank" title="手机交易">手机交易</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1434" target="_blank" title="手机号码交易区">手机号码交易</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="职业生活">职业生活</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1617" target="_blank" title="理财学院">理财学院</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1502" target="_blank" title="谈股论金">谈股论金</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=212" target="_blank" title="求医问药">求医问药</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1087" target="_blank" title="法律天地">法律天地</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1061" target="_blank" title="外语课堂">外语课堂</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="兴趣爱好">兴趣爱好</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=105" target="_blank" title="原创文字">原创文字</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1960" target="_blank" title="业余无线电">业余无线电</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=108" target="_blank" title="极品音乐">极品音乐</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1611" target="_blank" title="花鸟鱼虫">花鸟鱼虫</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1628" target="_blank" title="常州摩托社区">常州摩托社区</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1638" target="_blank" title="动漫吧">动漫吧</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1639" target="_blank" title="兰若无界">兰若无界</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1765" target="_blank" title="龙城桌游">龙城桌游</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=303" target="_blank" title="星月童话">星月童话</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1461" target="_blank" title="来摄">来摄</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=503" target="_blank" title="常州FB公摄">常州FB公摄</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1039" target="_blank" title="团购 打折卡">化龙巷打折卡</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1250" target="_blank" title="Ｋ歌ＳＨＯＷ">Ｋ歌ＳＨＯＷ</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1055" target="_blank" title="书香门第">书香门第</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1300" target="_blank" title="异乡人在常州">异乡人在常州</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=111" target="_blank" title="我的母校">我的母校</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1613" target="_blank" title="龙城蓝翼">龙城蓝翼</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1607" target="_blank" title="扬子晚饭团">扬子晚饭团</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=1956" target="_blank" title="龙城驴友">龙城驴友</a></li></ul>
	</dd>
</dl><dl rel="block">
	<dt class="active" title="站务讨论">站务讨论</dt>
	<dd>
		<ul><li><a href="http://bbs.hualongxiang.com/thread.php?fid=2140" target="_blank" title="信息版">信息版</a></li><li><a href="http://bbs.hualongxiang.com/thread.php?fid=400" target="_blank" title="意见与建议">意见与建议</a></li></ul>
	</dd>
</dl></div>
  </div>
</body>
</html>
