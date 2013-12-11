<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="NjhLib.Web.Mvc.myAdmin.list" %>


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
</div><table class="Common Hosts index" id="Hosts_index">
<tr class="hxbt">
<td colspan="15">
<span>
<a href="/index.php?m=Hosts&a=quick_add_vhost"><font class="red">&raquo;</font> 快速建站</a>
<a href="/index.php?m=Hosts&a=add"><font class="red">&raquo;</font> 添加</a>
<a href="/index.php?m=Hosts&a=backup_list"><font class="red">&raquo;</font> 备份列表</a>
<font class="pointer" id="check_vhost_error" 
url="/index.php?m=Hosts&a=index&check_vhost_error=1"><font class="red">&raquo;</font> 检查错误</font>
<font id="search" class="pointer"><font class="red">&raquo;</font> 搜索</font>
</span>
网站列表	</td>
</tr>
<tr id="search_tr" class="hide">
<td colspan="15" class="t_a_c">
<input autocomplete="off" name="host_domain" id="host_domain" value="请填入域名" class="gray" />
<label><input autocomplete="off" id="host_domain_search" type="button" value="搜索网站" /></label>
</td>
</tr>
<tr>
<th>ID</th>
<th>域名</th>
<th>网站类型</th>
<th>所属FTP</th>
<th>目录</th>
<th>所属用户</th>
<th>使用Apache</th>
<th>相关端口</th>
<th>CGI进程数</th>
<th colspan="6">管理</th>
</tr>	
<tr class=" listing_tr " id="tr_1" k="1">
<td k="1" class="id pointer">
<div class="hide show_detail" id="show_detail_1">
<b>ID为49的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/hailider_com-20120105-iTC/hailider.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 12:18				<p>修改时间：2012-01-05 12:18			</div>
49		</td>
<td title="hailider_com">
<a href="http://hailider.com" title="http://hailider.com" target="_blank">hailider.com</a> <br /><a href="http://www.hailider.com" title="http://www.hailider.com" target="_blank">www.hailider.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/hailider_com-20120105-iTC/">hailider_com</td>
<td title="网站位置：/home/ftp/1520/hailider_com-20120105-iTC/hailider.com/">hailider.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td width="16" ><a href="/index.php?m=Hosts&a=edit&id=49" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td width="16" 
class="delete pointer" 
module_name="Hosts" 
pro_id="49" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td width="16" >
<span title="查看网站状态" class="nginx_status pointer" domain="hailider.com" host_id="49"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td width="16" >
<a href="/index.php?m=Hosts&a=backup_one&id=49" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td width="16" class="flows pointer" k="1" hostID="49" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td width="16" class="view pointer" k="1" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_2" k="2">
<td k="2" class="id pointer">
<div class="hide show_detail" id="show_detail_2">
<b>ID为48的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/hailide_com-20120105-MRh/hailide.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 12:16				<p>修改时间：2012-01-05 12:16			</div>
48		</td>
<td title="hailide_com">
<a href="http://hailide.com" title="http://hailide.com" target="_blank">hailide.com</a> <br /><a href="http://www.hailide.com" title="http://www.hailide.com" target="_blank">www.hailide.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/hailide_com-20120105-MRh/">hailide_com</td>
<td title="网站位置：/home/ftp/1520/hailide_com-20120105-MRh/hailide.com/">hailide.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=48" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="48" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="hailide.com" host_id="48"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=48" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="2" hostID="48" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="2" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class=" listing_tr " id="tr_3" k="3">
<td k="3" class="id pointer">
<div class="hide show_detail" id="show_detail_3">
<b>ID为47的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/zhms100-20120105-CMr/zhms100/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 11:05				<p>修改时间：2012-01-05 13:06			</div>
47		</td>
<td title="zhms100">
<a href="http://zhms100.com" title="http://zhms100.com" target="_blank">zhms100.com</a> <br /><a href="http://www.zhms100.com" title="http://www.zhms100.com" target="_blank">www.zhms100.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/zhms100-20120105-CMr/">zhms100</td>
<td title="网站位置：/home/ftp/1520/zhms100-20120105-CMr/zhms100/">zhms100</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=47" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="47" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="zhms100.com" host_id="47"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=47" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="3" hostID="47" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="3" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_4" k="4">
<td k="4" class="id pointer">
<div class="hide show_detail" id="show_detail_4">
<b>ID为46的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/iisct_62652_com-20120105-ztR/iisct.62652.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:55				<p>修改时间：2012-01-05 10:55			</div>
46		</td>
<td title="iisct_62652_com">
<a href="http://iisct.62652.com" title="http://iisct.62652.com" target="_blank">iisct.62652.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/iisct_62652_com-20120105-ztR/">iisct_62652_com</td>
<td title="网站位置：/home/ftp/1520/iisct_62652_com-20120105-ztR/iisct.62652.com/">iisct.62652.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=46" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="46" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="iisct.62652.com" host_id="46"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=46" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="4" hostID="46" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="4" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class=" listing_tr " id="tr_5" k="5">
<td k="5" class="id pointer">
<div class="hide show_detail" id="show_detail_5">
<b>ID为45的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/72e_62652_com-20120105-EQT/72e.62652.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:55				<p>修改时间：2012-01-05 10:55			</div>
45		</td>
<td title="72e_62652_com">
<a href="http://72e.62652.com" title="http://72e.62652.com" target="_blank">72e.62652.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/72e_62652_com-20120105-EQT/">72e_62652_com</td>
<td title="网站位置：/home/ftp/1520/72e_62652_com-20120105-EQT/72e.62652.com/">72e.62652.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=45" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="45" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="72e.62652.com" host_id="45"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=45" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="5" hostID="45" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="5" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_6" k="6">
<td k="6" class="id pointer">
<div class="hide show_detail" id="show_detail_6">
<b>ID为44的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/3vhost_com-20120105-wZF/3vhost.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:55				<p>修改时间：2012-01-05 10:55			</div>
44		</td>
<td title="3vhost_com">
<a href="http://3vhost.com" title="http://3vhost.com" target="_blank">3vhost.com</a> <br /><a href="http://www.3vhost.com" title="http://www.3vhost.com" target="_blank">www.3vhost.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/3vhost_com-20120105-wZF/">3vhost_com</td>
<td title="网站位置：/home/ftp/1520/3vhost_com-20120105-wZF/3vhost.com/">3vhost.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=44" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="44" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="3vhost.com" host_id="44"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=44" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="6" hostID="44" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="6" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class=" listing_tr " id="tr_7" k="7">
<td k="7" class="id pointer">
<div class="hide show_detail" id="show_detail_7">
<b>ID为43的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/5vhost_com-20120105-PHI/5vhost.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:54				<p>修改时间：2012-01-05 10:54			</div>
43		</td>
<td title="5vhost_com">
<a href="http://5vhost.com" title="http://5vhost.com" target="_blank">5vhost.com</a> <br /><a href="http://www.5vhost.com" title="http://www.5vhost.com" target="_blank">www.5vhost.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/5vhost_com-20120105-PHI/">5vhost_com</td>
<td title="网站位置：/home/ftp/1520/5vhost_com-20120105-PHI/5vhost.com/">5vhost.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=43" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="43" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="5vhost.com" host_id="43"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=43" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="7" hostID="43" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="7" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_8" k="8">
<td k="8" class="id pointer">
<div class="hide show_detail" id="show_detail_8">
<b>ID为42的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/2vhost_com-20120105-YAn/2vhost.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:51				<p>修改时间：2012-01-05 10:51			</div>
42		</td>
<td title="2vhost_com">
<a href="http://2vhost.com" title="http://2vhost.com" target="_blank">2vhost.com</a> <br /><a href="http://www.2vhost.com" title="http://www.2vhost.com" target="_blank">www.2vhost.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/2vhost_com-20120105-YAn/">2vhost_com</td>
<td title="网站位置：/home/ftp/1520/2vhost_com-20120105-YAn/2vhost.com/">2vhost.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=42" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="42" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="2vhost.com" host_id="42"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=42" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="8" hostID="42" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="8" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class=" listing_tr " id="tr_9" k="9">
<td k="9" class="id pointer">
<div class="hide show_detail" id="show_detail_9">
<b>ID为40的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/hanxing123-20120105-CBG/www.hanxing123.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:32				<p>修改时间：2012-01-05 10:32			</div>
40		</td>
<td title="hanxing123">
<a href="http://www.hanxing123.com" title="http://www.hanxing123.com" target="_blank">www.hanxing123.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/hanxing123-20120105-CBG/">hanxing123</td>
<td title="网站位置：/home/ftp/1520/hanxing123-20120105-CBG/www.hanxing123.com/">www.hanxing123.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=40" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="40" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="www.hanxing123.com" host_id="40"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=40" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="9" hostID="40" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="9" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_10" k="10">
<td k="10" class="id pointer">
<div class="hide show_detail" id="show_detail_10">
<b>ID为39的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/shiyan168-20120105-EFG/shiyan168/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 10:01				<p>修改时间：2012-01-05 10:01			</div>
39		</td>
<td title="shiyan168">
<a href="http://shiyan168" title="http://shiyan168" target="_blank">shiyan168</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/shiyan168-20120105-EFG/">shiyan168</td>
<td title="网站位置：/home/ftp/1520/shiyan168-20120105-EFG/shiyan168/">shiyan168</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=39" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="39" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="shiyan168" host_id="39"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=39" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="10" hostID="39" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="10" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr><tr>
<th>ID</th>
<th>域名</th>
<th>网站类型</th>
<th>所属FTP</th>
<th>目录</th>
<th>所属用户</th>
<th>使用Apache</th>
<th>相关端口</th>
<th>CGI进程数</th>
<th colspan="6">管理</th>
</tr>	
<tr class=" listing_tr " id="tr_11" k="11">
<td k="11" class="id pointer">
<div class="hide show_detail" id="show_detail_11">
<b>ID为38的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/baidu_com-20120105-jXK/baidu.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-05 09:45				<p>修改时间：2012-01-05 09:45			</div>
38		</td>
<td title="baidu_com">
<a href="http://baidu.com" title="http://baidu.com" target="_blank">baidu.com</a> <br /><a href="http://www.baidu.com" title="http://www.baidu.com" target="_blank">www.baidu.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/baidu_com-20120105-jXK/">baidu_com</td>
<td title="网站位置：/home/ftp/1520/baidu_com-20120105-jXK/baidu.com/">baidu.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=38" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="38" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="baidu.com" host_id="38"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=38" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="11" hostID="38" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="11" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_12" k="12">
<td k="12" class="id pointer">
<div class="hide show_detail" id="show_detail_12">
<b>ID为37的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/pic_rt22_net-20120104-ybF/pic.rt22.net/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-04 23:33				<p>修改时间：2012-01-04 23:33			</div>
37		</td>
<td title="pic_rt22_net">
<a href="http://pic.rt22.net" title="http://pic.rt22.net" target="_blank">pic.rt22.net</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/pic_rt22_net-20120104-ybF/">pic_rt22_net</td>
<td title="网站位置：/home/ftp/1520/pic_rt22_net-20120104-ybF/pic.rt22.net/">pic.rt22.net</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=37" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="37" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="pic.rt22.net" host_id="37"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=37" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="12" hostID="37" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="12" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class=" listing_tr " id="tr_13" k="13">
<td k="13" class="id pointer">
<div class="hide show_detail" id="show_detail_13">
<b>ID为35的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/jianfeng-20120104-EJO/guo.cn/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-04 14:11				<p>修改时间：2012-01-04 14:11			</div>
35		</td>
<td title="jianfeng">
<a href="http://guo.cn" title="http://guo.cn" target="_blank">guo.cn</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/jianfeng-20120104-EJO/">jianfeng</td>
<td title="网站位置：/home/ftp/1520/jianfeng-20120104-EJO/guo.cn/">guo.cn</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=35" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="35" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="guo.cn" host_id="35"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=35" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="13" hostID="35" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="13" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class="highlight_blue listing_tr " id="tr_14" k="14">
<td k="14" class="id pointer">
<div class="hide show_detail" id="show_detail_14">
<b>ID为34的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/cyjt-20120104-JgP/cyjtco.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-04 13:39				<p>修改时间：2012-01-04 13:39			</div>
34		</td>
<td title="cyjt">
<a href="http://cyjtco.com" title="http://cyjtco.com" target="_blank">cyjtco.com</a> <br /><a href="http://www.cyjtco.com" title="http://www.cyjtco.com" target="_blank">www.cyjtco.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/cyjt-20120104-JgP/">cyjt</td>
<td title="网站位置：/home/ftp/1520/cyjt-20120104-JgP/cyjtco.com/">cyjtco.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=34" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="34" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="cyjtco.com" host_id="34"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=34" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="14" hostID="34" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="14" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>	
<tr class=" listing_tr " id="tr_15" k="15">
<td k="15" class="id pointer">
<div class="hide show_detail" id="show_detail_15">
<b>ID为33的网站的详细信息：</b>
<div class="line"></div>
<p>网站目录：/home/ftp/1520/aaa-20120103-yMs/fuckyou.com/
<p>网站类型：普通				<p>所属套餐：旗舰套餐				<p>排序：2000				<p>状态：开启				<p>开通时间：2012-01-04 11:32				<p>修改时间：2012-01-04 11:32			</div>
33		</td>
<td title="fuckyou.com">
<a href="http://fuckyou.com" title="http://fuckyou.com" target="_blank">fuckyou.com</a> <br /><a href="http://www.fuckyou.com" title="http://www.fuckyou.com" target="_blank">www.fuckyou.com</a> <br />		</td>
<td>普通</td>
<td title="/home/ftp/1520/aaa-20120103-yMs/">aaa</td>
<td title="网站位置：/home/ftp/1520/aaa-20120103-yMs/fuckyou.com/">fuckyou.com</td>
<td><a href="/index.php?m=Users&a=edit&id=1520" target="_blank">zijidelu</a></td>
<td>
否		</td>
<td>
<span title="网站端口">80</span>,
<span title="FastCGI端口">
9000				</span>		</td>
<td>
默认
</td>
<td  ><a href="/index.php?m=Hosts&a=edit&id=33" title="修改"><img src="/Tpl/User_1520/Public/images/edit4.gif" /></a></td>
<td  
class="delete pointer" 
module_name="Hosts" 
pro_id="33" title="删除"><img src="/Tpl/User_1520/Public/images/drop.png" /></td>
<td  >
<span title="查看网站状态" class="nginx_status pointer" domain="fuckyou.com" host_id="33"><img src="/Tpl/User_1520/Public/images/flows.png" /></span>
</td>
<td  >
<a href="/index.php?m=Hosts&a=backup_one&id=33" title="备份"><img src="/Tpl/User_1520/Public/images/backup.gif" /></a>
</td>
<td  class="flows pointer" k="15" hostID="33" title="流量统计"><img src="/Tpl/User_1520/Public/images/tongji.gif" /></td>
<td  class="view pointer" k="15" title="查看详情"><img src="/Tpl/User_1520/Public/images/view.gif" /></td>
</tr>
<tr>
<td colspan="15">
<div class="line_box f_w_b">提示：</div>
<p>如提示“Nginx启动失败”，请执行“检查错误”操作，确保没有错误后，再重启Nginx
</td>
</tr>
<tr>
<td colspan="15" class="page">21 条记录 1/2 页    &nbsp;<span class='current'>1</span>&nbsp;<a href='/index.php?m=Hosts&a=index&p=2'>&nbsp;2&nbsp;</a> <a href='/index.php?m=Hosts&a=index&p=2'>下一页  &raquo;</a>  </td>
</tr>
</table>
<table id="footer">
<tr>
<td>
Powered by <a href="http://www.zijidelu.org" target="_blank">LuManager 2.0</a>
&nbsp; &nbsp; &copy;<a href="http://www.zijidelu.org" target="_blank">zijidelu.org</a>
<br /> 
<a href="http://www.zijidelu.org" target="_blank">自己的路</a> 版权所有
(run time: 6.156 s)
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