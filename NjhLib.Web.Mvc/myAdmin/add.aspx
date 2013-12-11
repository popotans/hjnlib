<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="NjhLib.Web.Mvc.myAdmin.add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LuManager2.0服务器管理系统</title>
<link rel="stylesheet" type="text/css" href="_res/common.css" />
<script>
    var LU_DEBUG = "0"; //调试模式
    var IS_DEMO = "1"; //演示模式
    var COOKIE_PREFIX = "lum_e9583ad3_"; //cookie前缀
    var COOKIE_EXPIRE = "900"; //cookie过期时间
    var CHECK_PASSWORD_STRING = "1"; //检查密码字串
    var ALLOW_SAFE_PASSWORD = ""; //允许保护密码
    var MODULE_NAME = "Intro";
    var ACTION_NAME = "index";
    var uid = "1520";
    var user = "zijidelu";
    var USER_FTP_ROOT = "/home/ftp/1520/";
    var TPL_PATH = ".//Tpl/User_1520/"; //后面需加/
    var CSS_PATH = "/Tpl/User_1520/Public/css/";
    var IMG_PATH = "_res/";
    var JS_PATH = "_res/";
    var FLASH_PATH = "/Tpl/User_1520/Public/flash/";
    //Hosts
    if (MODULE_NAME == 'Hosts') {
        var $host_type = "";
    }
</script>
<script type="text/javascript" src="_res/md5-min.js"></script>
<script type="text/javascript" src="_res/jquery.min.js"></script>
<script type="text/javascript" src="_res/jquery.cookie.js"></script>
<script type="text/javascript" src="_res/common.js"></script>
<script type="text/javascript" src="_res/Intro.js"></script>
</head>
<body>
<!--frameset cols="25%,75%">
<frame src="_footer.html">
<frame src="_footer.html">
</frameset-->
<div id="html_cache" class="hide"></div>
<div id="background" class="hide"></div>
<div id="loading" class="hide"></div>
<div id="show_msg" class="hide"></div>
<div id="text_contents_info" class="hide">
<div class="close pointer t_a_r"><img src="_res/del.gif" /></div>
<div id="contens"></div>
</div>
<div id="dialog" class="hide">
<div class="title">
<span class="close pointer"><img src="_res/del.gif" /></span>
<font class="title_contens">LUM提示</font>
</div>
<div class="contents">内容</div>
</div>
<div id="Top">
<font>
<a href="http://www.zijidelu.org" target="_blank">使用帮助</a>
<a href="/index.php?m=Intro&a=index">LUM首页</a>
<a href="/index.php?m=Public&a=quit">退出LUM</a>
</font>
<span class="f_w_b" style="padding-left:168px;">zijidelu</span>
您好，您属于<b>系统管理员</b>(具有最高权限)，感谢您使用LuManager！
</div>
<div id="main_menu">
<div class="top"></div>
<div class="middle">
<div class="t_a_c pointer">
<img class="hide_all" title="收缩所有" src="_res/uparrow0.png" />
&nbsp;
<img class="close" title="隐藏菜单" src="_res/disabled.png" />
&nbsp;
<img class="show_all" title="展开所有" src="_res/downarrow0.png" />
</div>
<div class="title">网站管理</div>
<div id="lum_main_menu_1" class="menu_text">
<a href="/index.php?m=Hosts&a=quick_add_vhost" class="f_w_b" title="包括虚拟主机，MySQL数据库，FTP空间">快速建站</a>
<a href="/index.php?m=Hosts&a=index">网站(虚拟主机)</a>
<a href="/index.php?m=Hosts&a=add">添加网站</a>
<a href="/index.php?m=Hosts&a=backup_list">网站还原</a>
<a href="/index.php?m=Hosts&a=index&check_vhost_error=1">检查网站配置错误</a>
<a href="/index.php?m=Hosts&a=update_vhosts">更新网站</a>
</div>
<div class="title">数据库管理</div>
<div id="lum_main_menu_3" class="menu_text">
<a href="/index.php?m=Mysqls&a=index">MySQL数据库</a>
<a href="/index.php?m=Mysqls&a=add">添加MySQL数据库</a>
<a href="/index.php?m=Mysqls&a=backup_list">MySQL数据库还原</a>
<a href=".//Runtime/pm/1e/pm_1520_e22ecc3dd9c42122e6daec1236031616" target="_blank" title="MySQL管理工具">PhpMyAdmin</a>
<a href=".//Runtime/pp/d5/pp_1520_f34941bd29895c359fbb9be32f91431c" target="_blank" title="PgSQL管理工具">PhpPgAdmin</a>
</div>
<div class="title">FTP管理</div>
<div id="lum_main_menu_4" class="menu_text">
<a href="/index.php?m=Ftps&a=index">FTP空间</a>
<a href="/index.php?m=Ftps&a=add">添加FTP</a>
</div>
<div class="title">
套餐定制			</div>
<div id="lum_main_menu_6" class="menu_text">
<a href="/index.php?m=Solutions&a=index">产品套餐(解决方案)</a>
<a href="/index.php?m=Solutions&a=revise_errors">修正套餐错误</a>
</div>
<div class="title">用户和密码</div>
<div id="lum_main_menu_7" class="menu_text">
<a href="/index.php?m=Users&a=index">用户管理</a>
<a href="/index.php?m=Groups&a=index" title="用户权限配置">用户组(用户授权)</a>
<a href="/index.php?m=Safes&a=index">密码管理</a>
</div>
<div class="title">网站相关</div>
<div id="lum_main_menu_2" class="menu_text">
<a href="/index.php?m=Flows&a=index">流量统计</a>
<a href="/index.php?m=Files&a=index">文件管理</a>
<a href="/index.php?m=Files&a=upload&upload_dir=/home/ftp/1520/">文件上传</a>
<a href="/index.php?m=Settings&a=memcached">Memcached缓存</a>
</div>
<div class="title">系统管理</div>
<div id="lum_main_menu_8" class="menu_text">
<a href="/index.php?m=Settings&a=system_action" title="系统关机和重启，组件关闭或重启等操作">关机&启动 等行为</a>
<a href="/index.php?m=Settings&a=config_lum">配置LuManager</a>
<a href="/index.php?m=Settings&a=config_system">配置Linux系统</a>
<a href="/index.php?m=Settings&a=config_softwares" title="PHP,MySQL等配置">优化和修改组件</a>
<a href="/index.php?m=Settings&a=config_file_edit">修改配置文件</a>
<a href="/index.php?m=Settings&a=crontab">自动化配置(任务计划)</a>
<a href="/index.php?m=Settings&a=email">邮件发送设置</a>
<a href="/index.php?m=Logs&a=clear">清理垃圾</a>
<a href="/index.php?m=Settings&a=connections">禁止访客IP</a>
<a href="/index.php?m=Settings&a=tools">工具箱</a>
<a href="/index.php?m=Guestbook&a=email" title="给系统管理员留言">留言反馈</a>
<a href="/index.php?m=Logs&a=index">操作日志</a>
</div>
<div class="title" id="menu_title_other">其它</div>
<div id="lum_main_menu_9">
<a href="/index.php?m=Intro&a=index">管理面板首页</a>
<a id="flush_cache" class="pointer">清除缓存</a>
<a href="#Top">↑返回顶部</a>
<!-- &nbsp; ↓底部 -->
<a href="/index.php?m=Public&a=quit">退出LuManager</a>
</div>
</div>
<div class="bottom">
<a href="/index.php?m=Intro&a=index"><img src="_res/logo_60_120.gif" /></a>	</div>
</div>

<table class="Common Mysqls add" id="Mysqls_add">
<tr class="hxbt">
<td colspan="3">
<span><a href="/index.php?m=Mysqls&a=index"><font class="red">&raquo;</font> 数据库列表</a></span>
添加MySQL数据库	</td>
</tr>
<form action="/index.php?m=Mysqls&a=add" method="post" id="master_form">
<tr>
<th>数据库用户名<br />数据库名</th>
<td class="input_td"><input autocomplete="off" name="name" id="name" class="mysql_name" value=""  /></td>
<td>由1-15位的字母，数字，下划线组成</td>
</tr>
<tr id="next_show">
<th>数据库密码</th>
<td><input autocomplete="off" name="password" type="password" class="no_check_password" maxlength="32" /></td>
<td>
如不更改，请留空<br />
<label><input autocomplete="off" type="button" class="rand_string" value="生成密码" /></label>
<font class="show_password"></font>
</td>
</tr>
<tr>
<th>所属套餐</th>
<td>
<select name="sid" id="sid">
<option value="1" >旗舰套餐</option>			</select>
</td>
<td></td>
</tr>
<tr class="extend_line">
<td colspan="3">选填(请点击)</td>
</tr>
<tbody id="extend_contents">
<tr>
<th>允许连接的IP</th>
<td><input autocomplete="off" name="host" value="localhost" /></td>
<td>
留空则允许任何IP连接，可填入localhost表示只允许本地连接
<div class="gray">如果您打算用LuManager自带的phpMyAdmin来管理该数据库，请将此项设置为localhost或%，否则将无法登陆</div>
</td>
</tr>
<tr>
<th>编码</th>
<td colspan="2">
<label><input autocomplete="off" name="character" value="utf8" type="radio" checked />utf8</label> &nbsp;
<label><input autocomplete="off" name="character" value="gbk" type="radio" />gbk</label> &nbsp;
<label><input autocomplete="off" name="character" value="big5" type="radio" />big5</label> &nbsp;
<label><input autocomplete="off" name="character" value="latin1" type="radio" />latin1</label> &nbsp;
<label><input autocomplete="off" name="character" value="latin2" type="radio" />latin2</label> &nbsp;
</td>
</tr>		
<tr>
<th>排序</th>
<td><input autocomplete="off" name="sort" value="2000" /></td>
<td></td>
</tr>
<tr>
<th>简要说明</th>
<td><textarea name="description"></textarea></td>
<td></td>
</tr>
<tr>
<th>状态</th>
<td colspan="2">
<label><input autocomplete="off" type="radio" name="status" value="1" checked />开</label> &nbsp;
<label><input autocomplete="off" type="radio" name="status" value="0"  />关</label> &nbsp;
</td>
</tr>
</tbody>
<tr>
<td colspan="3" class="t_a_r">
<label><input autocomplete="off" type="reset" value="重置" /></label>
<label><input autocomplete="off" type="submit" id="add_send" value="确认" /></label>
</td>
</tr>
<input autocomplete="off" name="uid" type="hidden" value="1520" />
<!--eq name="Think.ACTION_NAME" value="edit">
<tr class="hxbt2">
<td colspan="3"><img src="/Tpl/User_1520/Public/images/plus.gif" /> 禁止或还原非法字符</td>
</tr>
<tr>
<th>禁止不和谐的内容</th>
<td colspan="2">
<div class="line_box">
<textarea name="replace_db_string" id="replace_db_string"></textarea>
<br />
<label><input autocomplete="off" id="replace_db_string_send" id="replace_db_string" value="提交" type="button" /></label>
</div>
<p>多个字符请用##隔开，如：自己的路##自己作主##我爱你们，涛哥宝哥
</td>
</tr>
<tr>
<th>还原不和谐的内容</th>
<td colspan="2">
<div class="line_box">
<textarea name="restore_db_string" id="restore_db_string"></textarea>
<br />
<label><input autocomplete="off" id="restore_db_string_send" value="提交" type="button" /></label>
</div>
<p>多个字符请用##隔开，如：自己的路##我爱你们，涛哥宝哥##自己作主
</td>
</tr>
</eq-->
<input autocomplete="off" type="hidden" name="__hash__" value="70a09ed8606f09b5b49db2f9b9a0296d" /></form>
</table>
<table id="footer">
<tr>
<td>
Powered by <a href="http://www.zijidelu.org" target="_blank">LuManager 2.0</a>
&nbsp; &nbsp; &copy;<a href="http://www.zijidelu.org" target="_blank">zijidelu.org</a>
<br /> 
<a href="http://www.zijidelu.org" target="_blank">自己的路</a> 版权所有
(run time: 0.837 s)
<br />
<br />
</td>
</tr>
</table>
<script>
    $(function () {
        $("#loading").ajaxStart(function () {
            $(this).show();
        });
        $("#loading").ajaxStop(function () {
            $(this).hide();
        });
    });
</script>
</body>
</html>
<!-- LuManager: website:http://www.zijidelu.org -->