<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="$ajaxJson.aspx.cs" Inherits="NjhLib.Web.Mvc.json._ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.min-vsdoc.js"></script>
    <script src="../Scripts/jquery-1.4.1.min.js"></script>
    <script>
        function ajax(url, type, data, dataType, suc, err, cache) {
            $.ajax({
                url: url,
                type: type,
                dataType: dataType,
                data: data,
                success: suc,
                error: err,
                cache: cache
            });
        }
        function ajaxGet(url, data, dataType, suc, err) {
            $.ajax({
                url: url,
                type: "GET",
                dataType: dataType,
                data: data,
                success: suc,
                error: err,
                cache: false
            });
        }
        function ajaxGetHTML(url, data, suc, err) {
            $.ajax({
                url: url,
                type: "GET",
                dataType: "html",
                data: data,
                success: suc,
                error: err,
                cache: false
            });
        }
        function ajaxGetText(url, data, suc, err) {
            $.ajax({
                url: url,
                type: "GET",
                dataType: "text",
                data: data,
                success: suc,
                error: err,
                cache: false
            });
        }
        function ajaxGetJson(url, data, suc, err) {
            $.ajax({
                url: url,
                type: "GET",
                dataType: "json",
                data: data,
                success: suc,
                error: err,
                cache: false
            });
        }
    </script>
    <script>
        function ajax1() {
            $.ajax({
                url: "_jsonstr.aspx",
                type: "GET",
                dataType: "json",
                data: "",
                success: function (data) {
                    $(p).html(data.list[0].LeaderName);
                    alert(data);
                },
                error: function (data) {
                    alert("error");
                },
                cache: false
            });
        } // $(p).html(data.list[0].LeaderName);
        $(function () {


            var suc = function (data) {
                // $(p).html(data.list[0].LeaderName);
                alert(data);
            }
            var err = function () { alert("errpr"); }
            $("#btn").click(function () {
                var url = "_jsonstr.aspx";
                //ajax("_jsonstr.aspx", "get", "", "json", suc, err, false);
                // ajaxGetJson("_jsonstr.aspx", "", suc, err);
                ajaxGetHTML(url, "", suc, err);
              
            });
        });
    </script>
</head>
<body>
    <p id="p">
    </p>
    <input type="button" id="btn" value="click" />
</body>
</html>
