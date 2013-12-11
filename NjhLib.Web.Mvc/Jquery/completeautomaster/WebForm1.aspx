<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.completeautomaster.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <%--  <script src="../../Scripts/jquery-1.4.1.min.js"></script>--%>
   <script type="text/javascript" src="scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.mockjax.js"></script>
    <script type="text/javascript" src="src/jquery.autocomplete.js"></script>
    <link href="content/styles.css" rel="stylesheet" />
    <script>
        $(function () {
            var a = "";
                        a = [{ value: 'Ädams, Egbert', data: '' }, { value: 'Altman, Alisha', data: '' }, { value: 'Archibald, Janna', data: '' }, { value: 'Auman, Cody', data: '' }, { value: 'Bagley, Sheree', data: '' }, { value: 'Ballou, Wilmot', data: '' }, { value: 'Bard, Cassian', data: '' }, { value: 'Bash, Latanya', data: '' }, { value: 'Beail, May', data: '' }, { value: 'Black, Lux', data: '' }, { value: 'Bloise, India', data: '' }, { value: 'Blyant, Nora', data: '' }, { value: 'Bollinger, Carter', data: '' }, { value: 'Burns, Jaycob', data: '' }, { value: 'Carden, Preston', data: '' }, { value: 'Carter, Merrilyn', data: '' }, { value: 'Christner, Addie', data: '' }, { value: 'Churchill, Mirabelle', data: '' }, { value: 'Conkle, Erin', data: '' }, { value: 'Countryman, Abner', data: '' }, { value: 'Courtney, Edgar', data: '' }, { value: 'Cowher, Antony', data: '' }, { value: 'Craig, Charlie', data: '' }, { value: 'Cram, Zacharias', data: '' }, { value: 'Cressman, Ted', data: '' }, { value: 'Crissman, Annie', data: '' }, { value: 'Davis, Palmer', data: '' }, { value: 'Downing, Casimir', data: '' }, { value: 'Earl, Missie', data: '' }, { value: 'Eckert, Janele', data: '' }, { value: 'Eisenman, Briar', data: '' }, { value: 'Fitzgerald, Love', data: '' }, { value: 'Fleming, Sidney', data: '' }, { value: 'Fuchs, Bridger', data: '' }, { value: 'Fulton, Rosalynne', data: '' }, { value: 'Fye, Webster', data: '' }, { value: 'Geyer, Rylan', data: '' }, { value: 'Greene, Charis', data: '' }, { value: 'Greif, Jem', data: '' }, { value: 'Guest, Sarahjeanne', data: '' }, { value: 'Harper, Phyllida', data: '' }, { value: 'Hildyard, Erskine', data: '' }, { value: 'Hoenshell, Eulalia', data: '' }, { value: 'Isaman, Lalo', data: '' }, { value: 'James, Diamond', data: '' }, { value: 'Jenkins, Merrill', data: '' }, { value: 'Jube, Bennett', data: '' }, { value: 'Kava, Marianne', data: '' }, { value: 'Kern, Linda', data: '' }, { value: 'Klockman, Jenifer', data: '' }, { value: 'Lacon, Quincy', data: '' }, { value: 'Laurenzi, Leland', data: '' }, { value: 'Leichter, Jeane', data: '' }, { value: 'Leslie, Kerrie', data: '' }, { value: 'Lester, Noah', data: '' }, { value: 'Llora, Roxana', data: '' }, { value: 'Lombardi, Polly', data: '' }, { value: 'Lowstetter, Louisa', data: '' }, { value: 'Mays, Emery', data: '' }, { value: 'Mccullough, Bernadine', data: '' }, { value: 'Mckinnon, Kristie', data: '' }, { value: 'Meyers, Hector', data: '' }, { value: 'Monahan, Penelope', data: '' }, { value: 'Mull, Kaelea', data: '' }, { value: 'Newbiggin, Osmond', data: '' }, { value: 'Nickolson, Alfreda', data: '' }, { value: 'Pawle, Jacki', data: '' }, { value: 'Paynter, Nerissa', data: '' }, { value: 'Pinney, Wilkie', data: '' }, { value: 'Pratt, Ricky', data: '' }, { value: 'Putnam, Stephanie', data: '' }, { value: 'Ream, Terrence', data: '' }, { value: 'Rumbaugh, Noelle', data: '' }, { value: 'Ryals, Titania', data: '' }, { value: 'Saylor, Lenora', data: '' }, { value: 'Schofield, Denice', data: '' }, { value: 'Schuck, John', data: '' }, { value: 'Scott, Clover', data: '' }, { value: 'Smith, Estella', data: '' }, { value: 'Smothers, Matthew', data: '' }, { value: 'Stainforth, Maurene', data: '' }, { value: 'Stephenson, Phillipa', data: '' }, { value: 'Stewart, Hyram', data: '' }, { value: 'Stough, Gussie', data: '' }, { value: 'Strickland, Temple', data: '' }, { value: 'Sullivan, Gertie', data: '' }, { value: 'Swink, Stefanie', data: '' }, { value: 'Tavoularis, Terance', data: '' }, { value: 'Taylor, Kizzy', data: '' }, { value: 'Thigpen, Alwyn', data: '' }, { value: 'Treeby, Jim', data: '' }, { value: 'Trevithick, Jayme', data: '' }, { value: 'Waldron, Ashley', data: '' }, { value: 'Wheeler, Bysshe', data: '' }, { value: 'Whishaw, Dodie', data: '' }, { value: 'Whitehead, Jericho', data: '' }, { value: 'Wilks, Debby', data: '' }, { value: 'Wire, Tallulah', data: '' }, { value: 'Woodworth, Alexandria', data: '' }, { value: 'Zaun, Jillie', data: '' }, { value: '徐建涛', data: '' }, { value: '徐先生', data: '' }, { value: '徐女士', data: '' }, { value: '徐静蕾', data: '' }, { value: '徐怀钰', data: '' }, { value: '徐熙媛', data: '' }, { value: '张三', data: '' }, { value: '张栋梁', data: '' }, { value: '张可可', data: '' }, { value: '张杰', data: '' }, { value: '张柏芝', data: '' }, { value: '李四', data: ''}];
            //            //  alert(a);


            $.get("http://localhost:5405/Jquery/combogrid/data.aspx", function (result) {

             //   a = result;
                alert(result);
            });

            window.setTimeout(function () {
                alert(a);

                $('#autocomplete').autocomplete({
                    lookup: a,
                     serviceUrl: '/Jquery/combogrid/data.aspx',
                    onSelect: function (suggestion) {
                        $('#selection').html('You selected: ' + suggestion.value + ', ' + suggestion.data);
                    }
                });
            }, 1000);


//            $('#autocomplete').autocomplete({
//                serviceUrl: '/Jquery/combogrid/data.aspx',
//                onSelect: function (suggestion) {
//                    //  alert(suggestion);
//                    $('#selection').html('You selected: ' + suggestion.value + ', ' + suggestion.data);
//                }
//            });
        });
    </script>
</head>
<body>
    <div>
        <input type="text" name="country" id="autocomplete"/>
        <span id="selction"></span>
    </div>
</body>
</html>
