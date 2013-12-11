<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsLIb.aspx.cs" Inherits="NjhLib.Web.Mvc.Test.JsLIb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JSLIB</title>
    <script type="text/javascript">
        //防止SQl注入
        function checkSqlStr(text) {
            var repWord = "|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|set|;|from";
            var repWords = repWord.split('|');
            var appIndex;
            for (var i = 0; i < repWords.length; i++) {
                appIndex = text.indexOf(repWords[i]);
                if (appIndex != -1) {
                    text = text.replace(repWords[i], "");
                }
            }
            return text;
        }
        //防止脚本攻击
        function checkScriptStr(text) {
            var flag = true;
            var scriptWord = "<|>|script|alert|{|}|(|)|#|$|'|\"|:|;|&|*|@@|%|^|?";
            var words = scriptWord.split('|');
            for (var i = 0; i < words.length; i++) {
                if (text.indexOf(words[i]) != -1) {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
