<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paging.aspx.cs" Inherits="NjhLib.Web.Mvc.web.paging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
</head>
<body>
    <%
    
        int pno = 0;
        int.TryParse(Request["pno"], out pno);
        if (pno < 1) pno = 1;
        int totalpage = 55555;
        string str = NjhLib.Utils.StringUtil.showPage(pno, totalpage, 12, null, "#1564");

        //   Response.Write(str);
        
    %>
   
    <%=str %>
   
    <%--  
    对下面样式进行重写 可以覆盖默认样式
    <style type="text/css">
        #fenye{ font-weight:normal}
        #fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}
        #fenye a:hover{ background-color:red}
        #fenye .active { color:#990000; font-style:italic}
        #fenye .nob{ }
    </style>--%>
</body>
</html>
