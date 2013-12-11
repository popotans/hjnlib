<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoComplete.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.AutoComplete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/autocomplete/jquery.autocomplete.js"></script>
    <link rel="stylesheet" type="text/css" href="~/Scripts/autocomplete/jquery.autocomplete.css" />
    <script>
        var emails = [
{ name: "Peter Pan", to: "peter@pan.de" },
{ name: "Molly", to: "molly@yahoo.com" },
{ name: "Forneria Marconi", to: "live@japan.jp" },
{ name: "Master <em>Sync</em>", to: "205bw@samsung.com" },
{ name: "Dr. <strong>Tech</strong> de Log", to: "g15@logitech.com" },
{ name: "Don Corleone", to: "don@vegas.com" },
{ name: "Mc Chick", to: "info@donalds.org" },
{ name: "Donnie Darko", to: "dd@timeshift.info" },
{ name: "Quake The Net", to: "webmaster@quakenet.org" },
{ name: "Dr. Write", to: "write@writable.com" },
{ name: "GG Bond", to: "Bond@qq.com" },
{ name: "Zhuzhu Xia", to: "zhuzhu@qq.com" }
];
        var persons = [
            { name: "聂军华", id: "niejunhua" },
             { name: "王世宇", id: "wangshiyu" },
             { name: "彭乐飞", id: "penglefei" },
             { name: "黄金", id: "huangjin" },
             { name: "徐建平", id: "xujianping" },
            { name: "聂军华", id: "niejunhua02" }
        ];
             $(function () {
                 $('#keyword').autocomplete(emails, {
                     max: 12, //列表里的条目数
                     minChars: 0, //自动完成激活之前填入的最小字符
                     width: 400, //提示的宽度，溢出隐藏
                     scrollHeight: 300, //提示的高度，溢出显示滚动条
                     matchContains: true, //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                     autoFill: false, //自动填充
                     formatItem: function (row, i, max) {
                         return i + '/' + max + ':"' + row.name + '"[' + row.to + ']';
                     },
                     formatMatch: function (row, i, max) {
                         return row.name + row.to;
                     },
                     formatResult: function (row) {
                         return row.to;
                     }
                 }).result(function (event, row, formatted) {
                     alert(row.to);
                 });

                 $("#p").autocomplete(persons, {
                     matchContains: true,
                     formatItem: function (row, i, max) {
                         return row.name + '>' + row.id;
                     },
                     formatResult: function (row) {
                         return row.id;
                     }
                 }).result(function (event, row, formatted) {
                     $('#uid').val(row.id);
                 });


                 $("#getValue").click(function () {
                     alert($('#uid').val());
                 });
             });

             $(function () {
                 var datas = [{ value: 'niejunhua' }, { value: 'nieting'}];
                 $("#Text1").autocomplete(datas, {
                     max: 12, //列表里的条目数
                     minChars: 0, //自动完成激活之前填入的最小字符
                     width: 400, //提示的宽度，溢出隐藏
                     scrollHeight: 300, //提示的高度，溢出显示滚动条
                     matchContains: true, //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                     autoFill: false, //自动填充
                     formatItem: function (row, i, max) {
                         return row.value;
                     },
                     formatMatch: function (row, i, max) {
                         return row.value;
                     },
                     formatResult: function (row) {
                         return row.value;
                     }
                 }).result(function (event, row, formatted) {
                     $("#Text1").val(row.value);
                 });

                 var sdata = "";
                 var ajaxVersion = new Object();
                 function autoComplete(targetId, dataurl) {
                     var datas;
                     $.ajax({
                         url: dataurl,
                         dataType: 'html',
                         type: 'POST',
                         success: function (data) {
                             datas = data;
                             ajaxVersion.data = data;
                             $("<input type='hidden' id='ajaxVersion' value=\"" + data + "\"/>").appendTo("body");
                         }
                     });

                     //                     datas = [{ value: 'Ädams, Egbert' }, { value: 'Altman, Alisha' }, { value: 'Archibald, Janna' }, { value: 'Auman, Cody' }, { value: 'Bagley, Sheree' }, { value: 'Ballou, Wilmot' }, { value: 'Bard, Cassian' }, { value: 'Bash, Latanya' }, { value: 'Beail, May' }, { value: 'Black, Lux' }, { value: 'Bloise, India' }, { value: 'Blyant, Nora' }, { value: 'Bollinger, Carter' }, { value: 'Burns, Jaycob' }, { value: 'Carden, Preston' }, { value: 'Carter, Merrilyn' }, { value: 'Christner, Addie' }, { value: 'Churchill, Mirabelle' }, { value: 'Conkle, Erin' }, { value: 'Countryman, Abner' }, { value: 'Courtney, Edgar' }, { value: 'Cowher, Antony' }, { value: 'Craig, Charlie' }, { value: 'Cram, Zacharias' }, { value: 'Cressman, Ted' }, { value: 'Crissman, Annie' }, { value: 'Davis, Palmer' }, { value: 'Downing, Casimir' }, { value: 'Earl, Missie' }, { value: 'Eckert, Janele' }, { value: 'Eisenman, Briar' }, { value: 'Fitzgerald, Love' }, { value: 'Fleming, Sidney' }, { value: 'Fuchs, Bridger' }, { value: 'Fulton, Rosalynne' }, { value: 'Fye, Webster' }, { value: 'Geyer, Rylan' }, { value: 'Greene, Charis' }, { value: 'Greif, Jem' }, { value: 'Guest, Sarahjeanne' }, { value: 'Harper, Phyllida' }, { value: 'Hildyard, Erskine' }, { value: 'Hoenshell, Eulalia' }, { value: 'Isaman, Lalo' }, { value: 'James, Diamond' }, { value: 'Jenkins, Merrill' }, { value: 'Jube, Bennett' }, { value: 'Kava, Marianne' }, { value: 'Kern, Linda' }, { value: 'Klockman, Jenifer' }, { value: 'Lacon, Quincy' }, { value: 'Laurenzi, Leland' }, { value: 'Leichter, Jeane' }, { value: 'Leslie, Kerrie' }, { value: 'Lester, Noah' }, { value: 'Llora, Roxana' }, { value: 'Lombardi, Polly' }, { value: 'Lowstetter, Louisa' }, { value: 'Mays, Emery' }, { value: 'Mccullough, Bernadine' }, { value: 'Mckinnon, Kristie' }, { value: 'Meyers, Hector' }, { value: 'Monahan, Penelope' }, { value: 'Mull, Kaelea' }, { value: 'Newbiggin, Osmond' }, { value: 'Nickolson, Alfreda' }, { value: 'Pawle, Jacki' }, { value: 'Paynter, Nerissa' }, { value: 'Pinney, Wilkie' }, { value: 'Pratt, Ricky' }, { value: 'Putnam, Stephanie' }, { value: 'Ream, Terrence' }, { value: 'Rumbaugh, Noelle' }, { value: 'Ryals, Titania' }, { value: 'Saylor, Lenora' }, { value: 'Schofield, Denice' }, { value: 'Schuck, John' }, { value: 'Scott, Clover' }, { value: 'Smith, Estella' }, { value: 'Smothers, Matthew' }, { value: 'Stainforth, Maurene' }, { value: 'Stephenson, Phillipa' }, { value: 'Stewart, Hyram' }, { value: 'Stough, Gussie' }, { value: 'Strickland, Temple' }, { value: 'Sullivan, Gertie' }, { value: 'Swink, Stefanie' }, { value: 'Tavoularis, Terance' }, { value: 'Taylor, Kizzy' }, { value: 'Thigpen, Alwyn' }, { value: 'Treeby, Jim' }, { value: 'Trevithick, Jayme' }, { value: 'Waldron, Ashley' }, { value: 'Wheeler, Bysshe' }, { value: 'Whishaw, Dodie' }, { value: 'Whitehead, Jericho' }, { value: 'Wilks, Debby' }, { value: 'Wire, Tallulah' }, { value: 'Woodworth, Alexandria' }, { value: 'Zaun, Jillie' }, { value: '徐建涛' }, { value: '徐先生' }, { value: '徐女士' }, { value: '徐静蕾' }, { value: '徐怀钰' }, { value: '徐熙媛' }, { value: '张三' }, { value: '张栋梁' }, { value: '张可可' }, { value: '张杰' }, { value: '张柏芝' }, { value: '李四'}];
                     //                     ajaxVersion.data = datas;
                     window.setTimeout(function () {
                         alert($("#ajaxVersion").val());
                         var sss = $("#ajaxVersion").val();
                        sss = [{ value: 'Ädams, Egbert' }, { value: 'Altman, Alisha' }, { value: 'Archibald, Janna' }, { value: 'Auman, Cody' }, { value: 'Bagley, Sheree' }, { value: 'Ballou, Wilmot' }, { value: 'Bard, Cassian' }, { value: 'Bash, Latanya' }, { value: 'Beail, May' }, { value: 'Black, Lux' }, { value: 'Bloise, India' }, { value: 'Blyant, Nora' }, { value: 'Bollinger, Carter' }, { value: 'Burns, Jaycob' }, { value: 'Carden, Preston' }, { value: 'Carter, Merrilyn' }, { value: 'Christner, Addie' }, { value: 'Churchill, Mirabelle' }, { value: 'Conkle, Erin' }, { value: 'Countryman, Abner' }, { value: 'Courtney, Edgar' }, { value: 'Cowher, Antony' }, { value: 'Craig, Charlie' }, { value: 'Cram, Zacharias' }, { value: 'Cressman, Ted' }, { value: 'Crissman, Annie' }, { value: 'Davis, Palmer' }, { value: 'Downing, Casimir' }, { value: 'Earl, Missie' }, { value: 'Eckert, Janele' }, { value: 'Eisenman, Briar' }, { value: 'Fitzgerald, Love' }, { value: 'Fleming, Sidney' }, { value: 'Fuchs, Bridger' }, { value: 'Fulton, Rosalynne' }, { value: 'Fye, Webster' }, { value: 'Geyer, Rylan' }, { value: 'Greene, Charis' }, { value: 'Greif, Jem' }, { value: 'Guest, Sarahjeanne' }, { value: 'Harper, Phyllida' }, { value: 'Hildyard, Erskine' }, { value: 'Hoenshell, Eulalia' }, { value: 'Isaman, Lalo' }, { value: 'James, Diamond' }, { value: 'Jenkins, Merrill' }, { value: 'Jube, Bennett' }, { value: 'Kava, Marianne' }, { value: 'Kern, Linda' }, { value: 'Klockman, Jenifer' }, { value: 'Lacon, Quincy' }, { value: 'Laurenzi, Leland' }, { value: 'Leichter, Jeane' }, { value: 'Leslie, Kerrie' }, { value: 'Lester, Noah' }, { value: 'Llora, Roxana' }, { value: 'Lombardi, Polly' }, { value: 'Lowstetter, Louisa' }, { value: 'Mays, Emery' }, { value: 'Mccullough, Bernadine' }, { value: 'Mckinnon, Kristie' }, { value: 'Meyers, Hector' }, { value: 'Monahan, Penelope' }, { value: 'Mull, Kaelea' }, { value: 'Newbiggin, Osmond' }, { value: 'Nickolson, Alfreda' }, { value: 'Pawle, Jacki' }, { value: 'Paynter, Nerissa' }, { value: 'Pinney, Wilkie' }, { value: 'Pratt, Ricky' }, { value: 'Putnam, Stephanie' }, { value: 'Ream, Terrence' }, { value: 'Rumbaugh, Noelle' }, { value: 'Ryals, Titania' }, { value: 'Saylor, Lenora' }, { value: 'Schofield, Denice' }, { value: 'Schuck, John' }, { value: 'Scott, Clover' }, { value: 'Smith, Estella' }, { value: 'Smothers, Matthew' }, { value: 'Stainforth, Maurene' }, { value: 'Stephenson, Phillipa' }, { value: 'Stewart, Hyram' }, { value: 'Stough, Gussie' }, { value: 'Strickland, Temple' }, { value: 'Sullivan, Gertie' }, { value: 'Swink, Stefanie' }, { value: 'Tavoularis, Terance' }, { value: 'Taylor, Kizzy' }, { value: 'Thigpen, Alwyn' }, { value: 'Treeby, Jim' }, { value: 'Trevithick, Jayme' }, { value: 'Waldron, Ashley' }, { value: 'Wheeler, Bysshe' }, { value: 'Whishaw, Dodie' }, { value: 'Whitehead, Jericho' }, { value: 'Wilks, Debby' }, { value: 'Wire, Tallulah' }, { value: 'Woodworth, Alexandria' }, { value: 'Zaun, Jillie' }, { value: '徐建涛' }, { value: '徐先生' }, { value: '徐女士' }, { value: '徐静蕾' }, { value: '徐怀钰' }, { value: '徐熙媛' }, { value: '张三' }, { value: '张栋梁' }, { value: '张可可' }, { value: '张杰' }, { value: '张柏芝' }, { value: '李四' }];
                         $("#" + targetId).autocomplete(sss, {
                             max: 5, //列表里的条目数
                             minChars: 0, //自动完成激活之前填入的最小字符
                             width: 400, //提示的宽度，溢出隐藏
                             scrollHeight: 300, //提示的高度，溢出显示滚动条
                             matchContains: true, //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                             autoFill: false, //自动填充
                             formatItem: function (row, i, max) {
                                 return row.value;
                             },
                             formatMatch: function (row, i, max) {
                                 return row.value;
                             },
                             formatResult: function (row) {
                                 return row.value;
                             }
                         }).result(function (event, row, formatted) {
                             $("#" + targetId).val(row.value);
                         });
                     }, 1000);
                 }

                 autoComplete("Text2", 'http://localhost:5405/Jquery/combogrid/data.aspx');


             });
             


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="keyword" />
        <input id="getValue" value="GetValue" type="button" />
        <input id="p" /><input type="hidden" id="uid"/><br/>
         <input id="Text1"  /><br/>
         <input id="Text2" />
        <asp:Button ID="Button1"  runat="server" Text="Button" />
    </div>
    </form>
</body>
</html>
