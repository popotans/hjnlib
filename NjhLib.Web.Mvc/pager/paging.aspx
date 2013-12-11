<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paging.aspx.cs" Inherits="NjhLib.Web.Mvc.pager.paging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
       .pagination {
    list-style: none outside none;
    padding: 2px;
}

.pagination li {
    float: left;
}
.pagination a {
    color: #016EC3;
    text-decoration: none;
}

    
    </style>
</head>
<body>
   <%
   
       Dictionary<string, string> dic = new Dictionary<string, string>();
       dic.Add("id", "1");
       dic.Add("name", "2");
       string s = NjhLib.Utils.StringUtil.Paging(1, 15, 500, "paging.aspx", dic, false);
       Response.Write(s);
        %>
</body>
</html>
