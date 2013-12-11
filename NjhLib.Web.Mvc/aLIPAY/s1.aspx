<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="s1.aspx.cs" Inherits="NjhLib.Web.Mvc.aLIPAY.s1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" action="s1.aspx"  method="post">
    <input type="hidden" name="action" value="do" />
    <div>
        <table>
            <tr>
                <td>标题
                </td>
                <td> <input type="text" name="TxtSubject"  value="一分钱测试"/>
                </td>
            </tr>
            <tr>
                <td>金额
                </td>
                <td><input type="text" name="TxtTotal_fee" value="0.01" />
                </td>
            </tr>
            <tr>
                <td>备注
                </td>
                <td><input type="text" name="TxtBody" value="备注，开发一分钱的测试程序" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td><input type="submit" name="subject" value="submit" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
