<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.NbspAutoComplete10.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script src="jquery.NbspAutoComplete.1.0.min.js"></script>
    <script>

        //   $(function () {
        var a = { data: ['Ädams, Egbert', 'Altman, Alisha', 'Archibald, Janna', 'Auman, Cody', 'Bagley, Sheree', 'Ballou, Wilmot', 'Bard, Cassian', 'Bash, Latanya', 'Beail, May', 'Black, Lux', 'Bloise, India', 'Blyant, Nora', 'Bollinger, Carter', 'Burns, Jaycob', 'Carden, Preston', 'Carter, Merrilyn', 'Christner, Addie', 'Churchill, Mirabelle', 'Conkle, Erin', 'Countryman, Abner', 'Courtney, Edgar', 'Cowher, Antony', 'Craig, Charlie', 'Cram, Zacharias', 'Cressman, Ted', 'Crissman, Annie', 'Davis, Palmer', 'Downing, Casimir', 'Earl, Missie', 'Eckert, Janele', 'Eisenman, Briar', 'Fitzgerald, Love', 'Fleming, Sidney', 'Fuchs, Bridger', 'Fulton, Rosalynne', 'Fye, Webster', 'Geyer, Rylan', 'Greene, Charis', 'Greif, Jem', 'Guest, Sarahjeanne', 'Harper, Phyllida', 'Hildyard, Erskine', 'Hoenshell, Eulalia', 'Isaman, Lalo', 'James, Diamond', 'Jenkins, Merrill', 'Jube, Bennett', 'Kava, Marianne', 'Kern, Linda', 'Klockman, Jenifer', 'Lacon, Quincy', 'Laurenzi, Leland', 'Leichter, Jeane', 'Leslie, Kerrie', 'Lester, Noah', 'Llora, Roxana', 'Lombardi, Polly', 'Lowstetter, Louisa', 'Mays, Emery', 'Mccullough, Bernadine', 'Mckinnon, Kristie', 'Meyers, Hector', 'Monahan, Penelope', 'Mull, Kaelea', 'Newbiggin, Osmond', 'Nickolson, Alfreda', 'Pawle, Jacki', 'Paynter, Nerissa', 'Pinney, Wilkie', 'Pratt, Ricky', 'Putnam, Stephanie', 'Ream, Terrence', 'Rumbaugh, Noelle', 'Ryals, Titania', 'Saylor, Lenora', 'Schofield, Denice', 'Schuck, John', 'Scott, Clover', 'Smith, Estella', 'Smothers, Matthew', 'Stainforth, Maurene', 'Stephenson, Phillipa', 'Stewart, Hyram', 'Stough, Gussie', 'Strickland, Temple', 'Sullivan, Gertie', 'Swink, Stefanie', 'Tavoularis, Terance', 'Taylor, Kizzy', 'Thigpen, Alwyn', 'Treeby, Jim', 'Trevithick, Jayme', 'Waldron, Ashley', 'Wheeler, Bysshe', 'Whishaw, Dodie', 'Whitehead, Jericho', 'Wilks, Debby', 'Wire, Tallulah', 'Woodworth, Alexandria', 'Zaun, Jillie', '徐建涛', '徐先生', '徐女士', '徐静蕾', '徐怀钰', '徐熙媛', '张三', '张栋梁', '张可可', '张杰', '张柏芝', '李四'] };
       // a = eval("(" + a + ")");
       // alert(a.data);
        // });
        $(function() {
            $(function () {
                $(".nbsptext").NbspAutoComplete({
                     location:'data.aspx',
                     datatype: "json",
                     selectcount: 55555,
                     liheight: "30px"
                });
            });
        });
    </script>
</head>
<body>
    <input type="text" id="inp1" class="nbsptext"/>

</body>
</html>
