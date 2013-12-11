<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="NjhLib.Web.Mvc.myAdmin.index" %>

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
<table class="Common Intro index" id="Intro_index">
<tr class="hxbt">
<td colspan="4" >LUM首页</td>
</tr>
<tr>
<td colspan="6" id="hd_info" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">基本信息</span>
</td>
</tr>
<tr>
<th>软件名称</th>
<td>
LuManager 2.0		(<font title="LuManager当前使用的是mysql数据库">mysql</font>, <font title="发布日期">20120103</font>)
<strong class="pointer" id="lum_update_check">检查更新</strong>
</td>
<th>您的机器码</th>
<td>d2dd6392a5fc48d166</td>
</tr>
<tr>
<th>系统信息</th>
<td>Linux(Debian) &nbsp; 2.6.32-274.el5.028stab093.2
&nbsp; 32位</td>
<th>服务器主机名</th>
<td>266706</td>
</tr>
<tr>
<th>服务器IP</th>
<td>184.82.15.124</td>
<th>您的IP</th>
<td>202.85.212.2</td>
</tr>
<tr>
<th>服务器运行时间</th>
<td>0天1小时21分钟</td>
<th>服务器时间</th>
<td>
2012-01-05 14:18:48		&nbsp;北京时间：2012-01-05 14:18:48	</td>
</tr>
<tr>
<th>CPU个数</th>
<td>1 (1核)</td>
<th>CPU型号</th>
<td>Intel(R) Xeon(R) CPU           X3220  @ 2.40GHz</td>
</tr>
<tr>
<td colspan="6" id="hd_info" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">产品信息</span>
</td>
</tr>
<tr>
<th>可用套餐个数</th>
<td>
1		<a href="/index.php?m=Solutions&a=index"><img title="查看详细" src="_res/view2.gif" /></a>
</td>
<th>网站个数</th>
<td>
21 (活动: 21)
<a href="/index.php?m=Hosts&a=index"><img title="查看详细" src="_res/view2.gif" /></a>
</td>
</tr>
<tr>
<th>FTP个数</th>
<td>
25 (活动: 25)
<a href="/index.php?m=Ftps&a=index"><img title="查看详细" src="_res/view2.gif" /></a>
</td>
<th>MySQL个数</th>
<td>
30 (活动: 30)
<a href="/index.php?m=Mysqls&a=index"><img title="查看详细" src="_res/view2.gif" /></a>
</td>
</tr>
<tr>
<th>智能DNS域名</th>
<td>
0 (活动: 0)
<a href="/index.php?m=Zones&a=index"><img title="查看详细" src="_res/view2.gif" /></a>
</td>
<th></th>
<td></td>
</tr>
<tr>
<td colspan="4" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">资源占用详情</span>
</td>
</tr>
<tr>
<th>内存使用</th>
<td>256.7 MB</td>
<th>可用内存</th>
<td>767.52 MB（空闲+可回收）</td>
</tr>
<tr>
<th>Swap虚拟内存总大小</th>
<td>0 B</td>
<th>Swap虚拟内存剩余</th>
<td>0 B</td>
</tr>
<tr>
<th>网站所在分区剩余空间</th>
<td>48 GB</td>
<th>日志所在分区剩余空间</th>
<td>
48 GB		<span title="未统计的访问日志和错误日志大小之和，在/home/hosts_log/目录下">（新日志: 40 KB）</span>
</td>
</tr>
<tr id="hardware_test">
<td colspan="4" id="hardware_test" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">服务器性能测试</span>
</td>
</tr>
<tr>
<th>整数运算能力得分</th>
<td>
<a href="/index.php?m=Intro&a=index&hardware_test_int=1">测试</a>
</td>
<th>浮点数运算能力得分</th>
<td>
<a href="/index.php?m=Intro&a=index&hardware_test_float=1">测试</a>
</td>
</tr>
<tr>
<th>读数据能力得分</th>
<td>
<a href="/index.php?m=Intro&a=index&hardware_test_read=1">测试</a>
</td>
<th>写数据能力得分</th>
<td>
<a href="/index.php?m=Intro&a=index&hardware_test_write=1">测试</a>
</td>
</tr>
<tr>
<th>参考值</th>
<td colspan="3">
A机器：Pentium4 CPU(3.06GHz)；80G硬盘；2G内存；<a href="http://www.zijidelu.org" target="_blank" class="red">HttpOS系统</a> --> 整数:15分、浮点:17分、读:25分、写:24分<br />
B机器：T4300 CPU；500G硬盘；2G内存；Debian Linux系统 --> 整数:28分、浮点:24分、读:32分、写:34分<br />
C机器：AMD3800+ CPU；160G硬盘；2G内存；FreeBSD系统 --> 整数:27分、浮点:22分、读:29分、写:31分<br />
</td>
</tr>
<tr>
<td colspan="4" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">组件进程数</span>
</td>
</tr>
<tr>
<th>SSH进程数</th>
<td>4</span></td>
<th>FastCGI进程数</th>
<td>3</span></td>
</tr>
<tr>
<th>Pure-ftpd进程数</th>
<td>
1		
</td>
<th>MySQL进程数</th>
<td>1</span></td>
</tr>
<tr>
<th>Nginx进程数</th>
<td>8</span></td>
<th>Apache进程数</th>
<td><font class="orange" title="如无任何网站使用Apache，则属正常现象">未启动</font>（正常）</span></td>
</tr>
<tr>
<th>Bind(DNS)进程数</th>
<td><font class="orange">未启动</font>
（正常）	</span></td>
<th>LUM进程数</th>
<td>7</span></td>
</tr>
<tr>
<td colspan="4">
<p>当没有网站使用Apache时，Apache没有启动是正常的。同理，当没有FTP时，Pure-ftpd没有启动是正常的。当没有智能DNS时，Bind没有启动是正常的
<p>如果某程序没有启动成功，请尝试重启相应程序。<a href="/index.php?m=Settings&a=system_action">现在就去尝试</a>
<p>如果从未修改过MySQL或PostgreSQL的密码（只能在LUM中修改），是不能启动Pure-ftpd的。<a href="/index.php?m=Safes&a=index">现在就去修改</a>
</td>
</tr>
<tr>
<td colspan="4" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">组件版本信息</span>
</td>
</tr>
<tr>
<th>Nginx</th>
<td>
1.0.10			</td>
<th>Apache</th>
<td>2.2.21</td>
</tr>
<tr>
<th>MySQL</th>
<td>5.5.19</td>
<th>PHP</th>
<td>5.2.17</td>
</tr>
<tr>
<th>Pure-ftpd</th>
<td>1.0.32</td>
<th>Zend Optimizer</th>
<td>3.3.3</td>
</tr>
<tr>
<th>eAccelerator</th>
<td>0.9.5.3</td>
<th>Debian(系统)</th>
<td>6.0</td>
</tr>
</table>
<table class="Common Intro index" id="Intro_index">
<tr>
<td colspan="6" id="hd_info" class="hxbt2"><img src="_res/plus.gif" /> 
<span class="title">磁盘信息</span>
</td>
</tr>
<tr>
<th>挂载点</th>
<th>文件系统</th>
<th>大小</th>
<th>已使用</th>
<th>剩余</th>
<th>使用百分比</th>
</tr>
<tr>
<td>/</td>
<td>/dev/simfs</td>
<td>50G</td>
<td>2.1G</td>
<td>48G</td>
<td>5%</td>
</tr></table>
<table class="Common Intro index" id="Intro_index">
<tr>
<th id="first_info">使用提醒</th>
<td>
* 首次使用请更改<font class="red">帐户密码</font>和<font class="red">MySQL的root用户密码</font><br />
* 请务必在此软件中更改Mysql的root用户密码，而不是在phpMyAdmin中更改，否则将有可能导致不能登陆后台<br />
<a href="http://www.zijidelu.org" target="_blank">忘记密码怎么办？</a>
</td>
</tr>
<tr>
<th>鸣谢</th>
<td>
* LuManager运行于开源的类unix系统上，感谢那些为开源软件做出贡献的人们！<br />
* 在软件开发和调试过程中得到了很多朋友的支持，在此深表感谢！<br />
* 感谢
Sparrow(qq:8500096)、
<a target="_blank" href="http://www.safefake.com">safefake(qq:342996)</a>、
小猴(qq:694568)、
<a target="_blank" href="http://jerrychen.me">Jerry Chen</a>、
风影(qq:355568052)、
琥珀(qq:16171830)、
等朋友的无私帮助！
</td>
</tr>
<tr>
<th>关于我们</th>
<td>
官方微博: http://www.zijidelu.org/thread-1915-1-1.html
</td>
</tr>
<tr>
<th>赞助我们</th>
<td>
<a href="/index.php?m=Public&a=rand_flinks" target="_blank">友情链接赞助</a>
&nbsp; &nbsp;
<a href="http://www.zijidelu.org/article-1-1.html" target="_blank">商业服务及捐赠</a>
</td>
</tr>
<tr id="license">
<th>使用协议</th>
<td>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<style>
.red{color:red;}
</style>
本软件版权归作者（爱洞特漏）所有，官方网站是<a href="http://www.zijidelu.com" target="_blank">自己的路</a>，欢迎使用！请您在使用本软件前仔细阅读如下条款。您的安装使用行为将视为对本《协议》的接受，并同意接受本《协议》各项条款的约束。<br />
<font class="red">*</font> 您可以复制、分发和传播无限制数量的软件产品，但您必须保证每一份复制、分发和传播都必须是完整和未经修改的，包括所有有关本软件产品的软件、电子文档，版权和商标宣言，亦包括本协议。 <br />
<font class="red">*</font> 我们欢迎服务器厂商、主机或虚拟机提供商等企业将本程序集成到你们的产品中，方便用户使用，但不得对本软件进行收费或修改。如使用该软件对外提供服务，我们建议您向我们<a href="http://www.zijidelu.com" target="_blank">购买授权</a>，因为LuManager正在帮助您或您的企业。<br />
<font class="red">*</font> 禁止对本软件产品进行反向工程、反向编译和反向汇编，不得删除本软件及其他副本上一切关于版权的信息，不得制作和提供该软件的注册机及破解程序。除非适用法律明文允许上述活动，否则您必须遵守此协议限制。<br />
<font class="red">*</font> 本软件产品是被当成一个单一产品而被授予许可使用，不得将各个部分分开用于任何目的行动。<br />
<font class="red">*</font> 您可以对模板文件进行更改供单位（只限于内部）或自己使用，但必须保留登陆页面的官方版权信息，否则属于侵权行为。由于模板文件中可以执行PHP原始代码，为了保证其他用户的数据安全，未经官方同意，不得将被修改过的模板供其他用户使用。<br />
<font class="red">*</font> 本软件并无附带任何形式的明示的或暗示的保证，包括任何关于本软件的适用性, 无侵犯知识产权或适合作某一特定用途的保证。<br />
<font class="red">*</font> 在任何情况下，对于因使用本软件或无法使用本软件而导致的任何损害赔偿，作者均无须承担法律责任，也不保证无故障产生，即使作者曾经被告知有可能出现该等损害赔偿。作者不保证本软件所包含的资料、文字、图形、链接或其它事项的准确性或完整性。作者可随时更改本软件，无须另作通知。<br />
<font class="red">*</font> 出于某些原因，本软件现在只提供信息显示界面，所有由用户自己制作、下载、使用的第三方信息数据插件所引起的一切版权问题或纠纷，本软件概不承担任何责任，也不提供任何明确的或暗示的保证。<br />
<font class="red">*</font> 如您未遵守本《协议》的各项条件，在不损害其它权利的情况下，版权人可将本《协议》终止。如发生此种情况，则您必须销毁“软件产品”及其各部分的所有副本。<br />			</td>
</tr>
</table>
<table id="footer">
<tr>
<td>
Powered by <a href="http://www.zijidelu.org" target="_blank">LuManager 2.0</a>
&nbsp; &nbsp; &copy;<a href="http://www.zijidelu.org" target="_blank">zijidelu.org</a>
<br /> 
<a href="http://www.zijidelu.org" target="_blank">自己的路</a> 版权所有
(run time: 0.364 s)
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
