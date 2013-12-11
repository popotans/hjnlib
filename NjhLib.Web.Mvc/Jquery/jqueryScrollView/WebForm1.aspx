<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.jqueryScrollView.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/Jquery/jquery.js"></script>
    <script src="ui.core.js"></script>
    <script src="../../Scripts/Jquery/jquery.cookie.js"></script>
    <script src="jquery.scroll-follow.js"></script>
    <style>
        #d{ width:120px; height:420px; background-color:#999;position: relative;}
    </style>
    <script>
        $(document).ready(function () {
            $("#d").scrollFollow(
            {
                speed:1110,
                offset:60,
                container:'wrap'
            }
            );

            $("#a").click(function(){
                $("#d").slideUp(1000);
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrap" style=" height:600px; overflow:hidden; background:red; width:400px;">
    <div id="d">
    sdfsdfsd
    <a href="javascript:" id="a">close</a>
    </div>
    <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
     <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
      <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
       <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
        <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
         <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
          <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
           <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
            <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
             <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
             </div>
              <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
     <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
      <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
       <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
        <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
         <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
          <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
           <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
            <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
             <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />  <br />
    </form>
    <h3>Parameters</h3>
			
			<dl class="params">
				<dt>&bull; speed</dt>
					<dd>
						<em>int - default: 500</em>
						<br />The duration of the sliding animation (in milliseconds). The smaller the value, the faster
						the animation.
					</dd>
				
				<dt>&bull; easing</dt>
					<dd>
						<em>string - default: 'linear'</em>
						<br />If included, use any one of the easing options from the <a href="http://gsgd.co.uk/sandbox/jquery/easing/">easing plugin</a>.
						 Uses 'linear' by default which provides no easing.
					</dd>
					
				<dt>&bull; offset</dt>
					<dd>
						<em>int - default: 0</em>
						<br />Number of pixels the Scroll Follow object should remain from top of viewport.
					</dd>
					
				<dt>&bull; container</dt>
					<dd>
						<em>string - default: object's immediate parent</em>
						<br />ID of the containing div.
					</dd>
					
				<dt>&bull; killSwitch</dt>
					<dd>
						<em>string - default: 'killSwitch'</em>
						<br />ID of the On/Off toggle element. Requires the 
						<a href="http://www.stilbuero.de/2006/09/17/cookie-plugin-for-jquery/">jQuery Cookie plugin</a>.
					</dd>
				
				<dt>&bull; onText</dt>
					<dd>
						<em>string - default: 'Turn Slide Off'</em>
						<br />killSwitch text to be displayed if sliding is enabled.
					</dd>
				
				<dt>&bull; offText</dt>
					<dd>
						<em>string - default: 'Turn Slide On'</em>
						<br />killSwitch text to be displayed if sliding is disabled.
					</dd>
					
				<dt>&bull; relativeTo</dt>
					<dd>
						<em>string - default: 'top'</em>
						<br />Scroll animation can be relative to either the 'top' or 'bottom' of the viewport.
					</dd>
					
				<dt>&bull; delay</dt>
					<dd>
						<em>int - default: 0</em>
						<br />Time between the end of the scroll and the beginning of the animation in milliseconds.
					</dd>
			</dl>
</body>
</html>
