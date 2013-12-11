<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.blockuiv2.WebForm1" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
<title>jQuery BlockUI Plugin (v2)</title>
<%--<link rel="stylesheet" type="text/css" media="screen" href="ui/jquery-ui-1.7.2.redmond.css" />	
<link rel="stylesheet" type="text/css" media="screen" href="../jq.css" />--%>
<link rel="stylesheet" type="text/css" media="screen" href="block.css?v3" />
<script type="text/javascript" src="../../Scripts/Jquery/jquery.js"></script>
<script type="text/javascript" src="jquery.blockUI.js?v2.38"></script>
<script type="text/javascript" src="jquery.tabs.pack.js"></script>
<%--<script type="text/javascript" src="jquery.history.min.js"></script>--%>
<%--<script type="text/javascript" src="chili-1.7.pack.js"></script>--%>
<script type="text/javascript">
    //   http://malsup.com/jquery/jquery.history.min.js
    $(function ($) {

        function test() {
            $.ajax({ url: 'wait2.php', cache: false });
        }

        // global hook - unblock UI when ajax request completes
        $(document).ajaxStop($.unblockUI);

        $('#main').tabs();

        //
        // page blocking example
        //
        $('#pageDemo1').click(function () {
            $.blockUI();
            test();
        });
        $('#pageDemo2').click(function () {
            $.blockUI({ message: '<h1><img src="busy.gif" /> Just a moment...</h1>' });
            test();
        });
        $('#pageDemo3').click(function () {
            $.blockUI({ css: { backgroundColor: '#f00', color: '#fff'} });
            test();
        });
        $('#pageDemo4').click(function () {
            $.blockUI({ message: $('#domMessage') });
            test();
        });

        //
        // element blocking example
        //
        $('#blockButton').click(function () {
            $('div.blockMe').block({ message: null });
        });
        $('#blockButton2').click(function () {
            $('div.blockMe').block({ message: '<h1>Processing...</h1>', css: { border: '3px solid #a00'} });
        });
        $('#unblockButton').click(function () {
            $('div.blockMe').unblock();
        });
        $('a.test').click(function () {
            alert('link clicked');
            return false;
        });

        //
        // dialog example
        //
        $('#showDialog').click(function () {
            $.blockUI({ message: $('#question'), css: { width: '275px'} });
        });

        $('#yes').click(function () {
            // update the block message
            $.unblockUI({ fadeOut: 200 });
            $.blockUI({ message: '<h1>Remote call in progress...</h1>' });

            $.ajax({
                url: 'wait2.php',
                cache: false,
                complete: function () {
                    // unblock when remote call returns
                    $.unblockUI();
                }
            });
        });

        $('#no').bind('click', $.unblockUI);

        // wire up all the demo code
        $('#demoTable pre code').each(function () {
            eval($(this).text());
        });
    });
</script>
<style type="text/css">
#overview h2 { border: 1px dashed gray; padding: 10px; background-color: #ffc;
    color: black; font-size: medium; margin: 10px 0;
}
#footer { font-family: sans-serif; color: #888 }
#domMessage { padding: 10px; }
div.blockMe { padding: 30px; margin: 30px; border: 10px solid #ccc; background-color: #ffd }
#question { background-color: #ffc; padding: 10px; }
#question input { width: 4em }

#demoTable { border: 1px solid #ddd }
#demoTable tr.odd { background-color: #efe }
#demoTable th { padding: 15px; background-color: #ffa }
#demoTable td { padding: 15px; vertical-align: top }
#tallContent h1 { margin: 15px }

#twitter { float:right; right: 20px; margin: 0 15px 15px 15px }
#twitter a { text-decoration: none; font-family: arial }
#twitter img { border: none; float: left }
</style>
</head>
<body><div><a href="http://$.com"><img src="../images/jq.png" class="logo" alt="Powered By jQuery" width="110" height="31" /></a></div>
<h1 id="banner"><a id="backnav" href="..">&lsaquo;&lsaquo; home</a>jQuery BlockUI Plugin (v2)</h1>
<div id="main">
    <ul id="nav" class="anchors">
    <li><a href="#overview">Overview</a></li>
    <li><a href="#page">Page Blocking</a></li>
    <li><a href="#element">Element Blocking</a></li>
    <li><a href="#dialog">Modal Dialogs</a></li>
    <li><a href="#demos">Demos</a></li>
    <li><a href="#options">Options</a></li>
    <li><a href="#faq">FAQ</a></li>
    <li><a href="#download">Download and Support</a></li>
    </ul>
    <div id="overview" class="tabContent">
<div id="twitter">
<a href="http://twitter.com/malsup">
<img src="../images/twitter.jpg" alt="Twitter.com"/>
<em>Updates posted<br>on twitter</em>
</a>
</div>
        <h1>Overview</h1>
        <h2><img src="info.gif" width="36" height="34" style="float:left;padding-right:10px"/>
        This documentation is for the 2.X versions of the plugin.
		If you need documentation for the old 1.X versions, you can find it
        <a href="index-old.html">here</a>.
        <span style="color:#a00;background-color:#ff0">This version of BlockUI requires jQuery v1.2.3 or later!</span>
        </h2>
        <h2><img src="info.gif" width="36" height="34" style="float:left;padding-right:10px"/>
        <p>
			17-JAN-09: New options: <code>fadeIn</code>, <code>timeout</code> and <code>showOverlay</code><br>
			New function: <code>$.growlUI</code> (see the Demos tab for more info)
		</p>
        <p>
		</p>
        </h2>
        The jQuery BlockUI Plugin lets you simulate synchronous behavior when using
        <abbr title="Asynchronous JavaScript and XML">AJAX</abbr>, <em>without locking the browser</em><sup>[1]</sup>.
        When activated, it will prevent user activity with the page (or part of the page) until
        it is deactivated.  BlockUI adds elements to the <abbr title="Document Object Model">DOM</abbr>
        to give it both the appearance and behavior of blocking user interaction.
        <p />
        Usage is very simple; to block user activity for the page:
        <pre><code class="mix">$.blockUI();</code></pre>
        Blocking with a custom message:
        <pre><code class="mix">$.blockUI({ message: '&lt;h1&gt;&lt;img src="busy.gif" /&gt; Just a moment...&lt;/h1&gt;' });</code></pre>
        Blocking with custom style:
        <pre><code class="mix">$.blockUI({ css: { backgroundColor: '#f00', color: '#fff'} });</code></pre>
        To unblock the page:
        <pre><code class="mix">$.unblockUI();</code></pre>
        If you want to use the default settings and have the UI blocked for all ajax requests,
        it's as easy as this:
        <pre><code class="mix">$(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);</code></pre>
        <p />
        <div class="footer">
        [1] Using the <code class="inline">XMLHttpRequest</code> object in <em>synchronous</em> mode causes
        the entire browser to lock until the remote call completes.  This is usually not a desirable behavior.
        </div>

    </div>

    <div id="page" class="tabContent">
        <h1>Page Blocking Examples</h1>
        This page demonstrates several ways to block the page.  Each button below activates
        blockUI and then makes a remote call to the server.
        <p />
        <input id="pageDemo1" class="demo" type="submit" value="Default Message" />
        <input id="pageDemo2" class="demo" type="submit" value="Custom Message" />
        <input id="pageDemo3" class="demo" type="submit" value="Custom Style" />
        <input id="pageDemo4" class="demo" type="submit" value="DOM Element as Message" />
        <p />
        The following code is used on this page:
        <pre><code class="mix">&lt;script type="text/javascript"&gt;

    // unblock when ajax activity stops
    $(document).ajaxStop($.unblockUI);

    function test() {
        $.ajax({ url: 'wait.php', cache: false });
    }

    $(document).ready(function() {
        $('#pageDemo1').click(function() {
            $.blockUI();
            test();
        });
        $('#pageDemo2').click(function() {
            $.blockUI({ message: '&lt;h1&gt;&lt;img src="busy.gif" /&gt; Just a moment...&lt;/h1&gt;' });
            test();
        });
        $('#pageDemo3').click(function() {
            $.blockUI({ css: { backgroundColor: '#f00', color: '#fff' } });
            test();
        });

        $('#pageDemo4').click(function() {
            $.blockUI({ message: $('#domMessage') });
            test();
        });
    });

&lt;/script&gt;

...

&lt;div id="domMessage" style="display:none;"&gt;
    &lt;h1&gt;We are processing your request.  Please be patient.&lt;/h1&gt;
&lt;/div&gt;
</code></pre>

        <div id="domMessage" style="display:none;">
            <h1>We are processing your request.  Please be patient.</h1>
        </div>

    </div>

    <div id="element" class="tabContent">
        <h1>Element Blocking Examples</h1>
        This page demonstrates how to block selected elements on the page rather than the entire page.
        The buttons below will block/unblock access to the bordered area beneath them.
        <p />
            <p>
                <input id="blockButton"   type="submit" value="Block" />
                <input id="blockButton2"  type="submit" value="Block with Message" />
                <input id="unblockButton" type="submit" value="Unblock" />
            </p>

        <div class="blockMe">
            <a href="#" class="test">Test link - click me!</a>
            <p />
            <select><option>Option 1</option><option>Option 2</option></select>
            lorem ipsum dolor sit amet
            consectetuer adipiscing elit
            sed lorem leo
            lorem leo consectetuer adipiscing elit
            sed lorem leo
            rhoncus sit amet
            <select><option>Option 1</option><option>Option 2</option></select>

            lorem ipsum dolor sit amet
            consectetuer adipiscing elit
            sed lorem leo
            <a href="#" class="test">Test link - click me!</a>
            lorem leo consectetuer adipiscing elit
            sed lorem leo
            rhoncus sit amet<br />
            <textarea rows="2" cols="20">test textarea</textarea>

        </div>
        <div>
            This text will not be blocked.
            <a href="#" class="test">Test link - click me!</a>
        </div>
        <p />
        The following code is used on this page:
        <pre><code class="mix">&lt;script type="text/javascript"&gt;
    $(document).ready(function() {

        $('#blockButton').click(function() {
            $('div.test').block({ message: null });
        });

        $('#blockButton2').click(function() {
            $('div.test').block({
                message: '&lt;h1&gt;Processing&lt;/h1&gt;',
                css: { border: '3px solid #a00' }
            });
        });

        $('#unblockButton').click(function() {
            $('div.test').unblock();
        });

        $('a.test').click(function() {
            alert('link clicked');
            return false;
        });
    });
&lt;/script&gt;
</code></pre>

    </div>

    <div id="dialog" class="tabContent">
        <h1>Simple Modal Dialog Example</h1>
        This page demonstrates how to display a simple modal dialog.  The button below will invoke <code class="inline">blockUI</code>
        with a custom message.  Depending upon the user response (yes or no) an ajax call will be made
        while keeping the UI blocked.
        <p />
        <input id="showDialog" type="submit" value="Show Dialog" />

        <p />
        The following code is used on this page:
<pre><code class="mix">&lt;script type="text/javascript"&gt;
    $(document).ready(function() {

        $('#test').click(function() {
            $.blockUI({ message: $('#question'), css: { width: '275px' } });
        });

        $('#yes').click(function() {
            // update the block message
            $.blockUI({ message: "&lt;h1&gt;Remote call in progress...&lt;/h1&gt;" });

            $.ajax({
                url: 'wait.php',
                cache: false,
                complete: function() {
                    // unblock when remote call returns
                    $.unblockUI();
                }
            });
        });

        $('#no').click(function() {
            $.unblockUI();
            return false;
        });

    });
&lt;/script&gt;

...

&lt;input id="test" type="submit" value="Show Dialog" /&gt;

...

&lt;div id="question" style="display:none; cursor: default"&gt;
        &lt;h1&gt;Would you like to contine?.&lt;/h1&gt;
        &lt;input type="button" id="yes" value="Yes" /&gt;
        &lt;input type="button" id="no" value="No" /&gt;
&lt;/div&gt;
</code></pre>

        For full-featured modal dialog support, check out
		<a class="external" href="http://www.ericmmartin.com/projects/simplemodal/">Simple Modal</a> by Eric Martin or
        <a class="external" href="http://dev.iceburg.net/jquery/jqModal/">jqModal</a>
        by Brice Burgess.

        <div id="question" style="display:none; cursor: default">
            <h1>Would you like to contine?</h1>
            <input type="button" id="yes" value="Yes" />
            <input type="button" id="no" value="No" />
        </div>

    </div>

    <div id="demos" class="tabContent">
        <h1>Demos</h1>
        <p>Most of the demos below will display for 2 seconds</p>
        <table id="demoTable">

        <tr class="odd"><td><button id="demo1">Run</button></td><td>Login Form</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo1').click(function() {
        $.blockUI({ message: $('#loginForm') });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>

        <tr><td><button id="demo2">Run</button><td>iPhoto (ish)</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo2').click(function() {
        $.blockUI({ css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        } });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>

        <tr class="odd"><td><button id="demo3">Run</button><td>Blue Overlay</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo3').click(function() {
        $.blockUI({ overlayCSS: { backgroundColor: '#00f' } });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>

        <tr><td><button id="demo4">Run</button><td>Tall Content</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo4').click(function() {
        $.blockUI({
            message: $('#tallContent'),
            css: { top: '20%' }
        });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>

        <tr class="odd"><td><button id="demo5">Run</button><td>Image Box</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo5').click(function() {
        $.blockUI({
            message: $('#displayBox'),
            css: {
                top:  ($(window).height() - 400) /2 + 'px',
                left: ($(window).width() - 400) /2 + 'px',
                width: '400px'
            }
        });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>

        <tr><td><button id="demo6">Run</button><td>Non-centered message</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo6').click(function() {
        $.blockUI({
            centerY: 0,
            css: { top: '10px', left: '', right: '10px' }
        });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>


        <tr class="odd"><td><button id="demo7">Run</button><td>Blocking without a message
            <br />(pass null as message)
        </td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo7').click(function() {
        $.blockUI({ message: null });

        setTimeout($.unblockUI, 2000);
    });
});
        </code></pre></td></tr>


        <tr><td><button id="demo8">Run</button><td>onUnblock callback<br />
        (useful when using fadeOut option<br /> as it is invoked when all<br />the blocking elements have been removed)
        </td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo8').click(function() {
        $.blockUI();

        setTimeout(function() {
            $.unblockUI({
                onUnblock: function(){ alert('onUnblock'); }
            });
        }, 2000);
    });
});
        </code></pre></td></tr>

        <tr class="odd"><td><button id="demo9">Run</button><td>Click overlay to unblock<br />
            (This demo will not automatically unblock, you  must click the overlay.)
        </td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo9').click(function() {
        $.blockUI();
        $('.blockOverlay').attr('title','Click to unblock').click($.unblockUI);
    });
});
        </code></pre></td></tr>

        <tr><td><button id="demo10">Run</button><td>Auto-Unblock<br />
        Sets a timer to unblock after a specified timeout.
        </td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo10').click(function() {
        $.blockUI({
			message: '&lt;h1&gt;Auto-Unblock!&lt;/h1&gt;',
			timeout: 2000
		});
	});
});
        </code></pre></td></tr>

        <tr class="odd"><td><button id="demo11">Run</button><td>Growl (the hard way)</td>
        <td><pre><code class="mix">$(document).ready(function() {
    $('#demo11').click(function() {
        $.blockUI({
			message: $('div.growlUI'),
			fadeIn: 700,
			fadeOut: 700,
			timeout: 2000,
			showOverlay: false,
            centerY: false,
			css: {
				width: '350px',
				top: '10px',
				left: '',
				right: '10px',
			    border: 'none',
			    padding: '5px',
			    backgroundColor: '#000',
			    '-webkit-border-radius': '10px',
			    '-moz-border-radius': '10px',
			    opacity: .6,
			    color: '#fff'
			}
        });
    });
});
        </code></pre>
</td></tr>

       <tr><td><button id="demo12">Run</button><td>Growl (the easy way)</td>
       <td><pre><code class="mix">$(document).ready(function() {
   $('#demo12').click(function() {
       $.growlUI('Growl Notification', 'Have a nice day!');
   });
});
       </code></pre>
		<p>The two growl examples above also make use of the following external CSS:</p>
		<pre>
div.growlUI { background: url(check48.png) no-repeat 10px 10px }
div.growlUI h1, div.growlUI h2 {
	color: white; padding: 5px 5px 5px 75px; text-align: left
}
	</pre>
	<p style="padding:15px;border:1px dashed #ccc;background:#ffe">Note: For a more full-featured "growl" implementation, check out the excellent
		<a href="http://stanlemon.net/projects/jgrowl.html">jGrowl plugin</a> by Stan Lemon.
</td></tr>

       <tr class="odd"><td><button id="demo13">Run</button><td>jQuery UI Theme<br/>Use jQuery UI themes to style the messaege</td>
       <td><pre><code class="mix">$(document).ready(function() {
	$('#demo13').click(function() {
		$.blockUI({
			theme:     true,
			title:    'This is your title',
			message:  '&lt;p&gt;This is your message.&lt;/p&gt;',
			timeout:   2000
		});
	});   
});
       </code></pre>
		<p>For more details on using this feature see <a href="theme.html">theme.html</a></p>
</td></tr>

       <tr><td><button id="demo14">Run</button><td>onBlock callback</td>
       <td><pre><code class="mix">$(document).ready(function() {
	$('#demo14').click(function() {
		$.blockUI({
			fadeIn: 1000,
			timeout:   2000,
			onBlock: function() {
				alert('Page is now blocked; fadeIn complete');
			}
		});
	});   
});
       </code></pre>
</td></tr>

        </table>
        <div class="growlUI" style="display:none">
            <h1>Growl Notification</h1>
            <h2>Have a nice day!</h2>
        </div>

        <div id="loginForm" style="display:none">
            <p><label>Username:</label><input type="text" name="demo1" /></p>
            <p><label>Password:</label><input type="text" name="demo1" /></p>
        </div>

        <div id="tallContent" style="display:none">
            <h1>Why</h1>
            <h1>am</h1>
            <h1>I</h1>
            <h1>so</h1>
            <h1>darn</h1>
            <h1>tall?</h1>
        </div>

        <img id="displayBox" src="gecko.jpg" width="400" height="400" style="display:none" />

    </div>

    <div id="options" class="tabContent">
    <h1>Options</h1>
    BlockUI's default options look (exactly) like this:
<pre><code class="mix">// override these in your code to change the default behavior and style
$.blockUI.defaults = {
    // message displayed when blocking (use null for no message)
    message:  '&lt;h1&gt;Please wait...&lt;/h1&gt;',

    // styles for the message when blocking; if you wish to disable
    // these and use an external stylesheet then do this in your code:
    // $.blockUI.defaults.css = {};
    css: {
        padding:        0,
        margin:         0,
        width:          '30%',
        top:            '40%',
        left:           '35%',
        textAlign:      'center',
        color:          '#000',
        border:         '3px solid #aaa',
        backgroundColor:'#fff',
        cursor:         'wait'
    },

    // styles for the overlay
    overlayCSS:  {
        backgroundColor: '#000',
        opacity:         0.6
    },

	// styles applied when using $.growlUI
	growlCSS: {
		width:    '350px',
		top:      '10px',
		left:     '',
		right:    '10px',
	    border:   'none',
	    padding:  '5px',
	    opacity:   0.6,
		cursor:    null,
	    color:    '#fff',
	    backgroundColor: '#000',
	    '-webkit-border-radius': '10px',
	    '-moz-border-radius':    '10px'
	},
	
	// IE issues: 'about:blank' fails on HTTPS and javascript:false is s-l-o-w
	// (hat tip to Jorge H. N. de Vasconcelos)
	iframeSrc: /^https/i.test(window.location.href || '') ? 'javascript:false' : 'about:blank',

	// force usage of iframe in non-IE browsers (handy for blocking applets)
	forceIframe: false,

    // z-index for the blocking overlay
    baseZ: 1000,

    // set these to true to have the message automatically centered
    centerX: true, // <-- only effects element blocking (page block controlled via css above)
    centerY: true,

    // allow body element to be stetched in ie6; this makes blocking look better
    // on "short" pages.  disable if you wish to prevent changes to the body height
    allowBodyStretch: true,

	// enable if you want key and mouse events to be disabled for content that is blocked
	bindEvents: true,

    // be default blockUI will supress tab navigation from leaving blocking content
    // (if bindEvents is true)
    constrainTabKey: true,

    // fadeIn time in millis; set to 0 to disable fadeIn on block
    fadeIn:  200,

    // fadeOut time in millis; set to 0 to disable fadeOut on unblock
    fadeOut:  400,

	// time in millis to wait before auto-unblocking; set to 0 to disable auto-unblock
	timeout: 0,

	// disable if you don't want to show the overlay
	showOverlay: true,

    // if true, focus will be placed in the first available input field when
    // page blocking
    focusInput: true,

    // suppresses the use of overlay styles on FF/Linux (due to performance issues with opacity)
    applyPlatformOpacityRules: true,

    // callback method invoked when unblocking has completed; the callback is
    // passed the element that has been unblocked (which is the window object for page
    // blocks) and the options that were passed to the unblock call:
    //     onUnblock(element, options)
    onUnblock: null,

    // don't ask; if you really must know: http://groups.google.com/group/jquery-en/browse_thread/thread/36640a8730503595/2f6a79a77a78e493#2f6a79a77a78e493
    quirksmodeOffsetHack: 4
};
</code></pre>
     Changing the blockUI options is simple and can be done in one of two ways:
     <ol>
        <li>Globally, by directly overriding the values in the
            <code class="inline">$.blockUI.defaults</code> object</li>
        <li>Locally, by passing an options object to the <code class="inline">blockUI</code>
            (or <code class="inline">block</code>) function.
     </ol>
     <h1>Global Overrides</h1>
     You can change the default options by simply declaring different values for them.  For example:
    <pre><code class="mix">// change message border
$.blockUI.defaults.css.border = '5px solid red';

// make fadeOut effect shorter
$.blockUI.defaults.fadeOut = 200;
</code></pre>

     <h1>Local Overrides</h1>
     Local overrides are achieved by passing an object to the <code class="inline">blockUI</code>,
     <code class="inline">unblockUI</code>, <code class="inline">block</code> or <code class="inline">unblock</code>
     functions.  The <strong>exact same options are available</strong> to
     the local options object as are available in the global object.  For example:
    <pre><code class="mix">// change message border
$.blockUI({ css: { border = '5px solid red'} });

...

// make fadeOut effect shorter
$.unblockUI({ fadeOut: 200 });

...

// use a different message
$.blockUI({ message: 'Hold on!' });

...

// use a different message
$('#myDiv').block({ message: 'Processing...' });
</code></pre>
    </div>

    <div id="faq" class="tabContent">
        <h1>Frequently Asked Questions</h1>
        <dl>
        <dt>What version of jQuery does the BlockUI plugin require?</dt>
        <dd>BlockUI is compatible with jQuery v1.2.3 and later.</dd>
        <dt>What has changed in version 2 of the BlockUI plugin?</dt>
        <dd>
        <ul>
            <li>Elements are no longer removed from the DOM when unblocking</li>
            <li>The default overlay color is now black instead of white</li>
            <li>The available options have been consolidated and sanitized</li>
            <li>The way in which options are passed to the plugin has changed</li>
            <li>Support for Opera 8 has been dropped</li>
            <li>The internals have been restructured for improved readability</li>
            <li><code>displayBox</code> functionality removed
            (<a href="http://www.shadowbox-js.com/">other plugins</a> do this better)</li>
        </ul>
        </dd>
        <dt>Is my code that used the old blockUI plugin compatible with the new 2.0x version?</dt>
        <dd>No, not if that code was passing options to blockUI.  The manner in which options
            are passed has changed slightly.  See the <a href="#options">Options page</a> for details on how
            to pass options in the new version.</dd>
        <dt>Does the BlockUI Plugin have any dependencies on other plugins?</dt>
        <dd>No.</dd>
        <dt>How do I use an external stylesheet to style the blocking message?</dt>
        <dd>See <a href="stylesheet.html">this demo page</a>.</dd>
        <dt>How do I use an external stylesheet to style the blocking overlay?</dt>
        <dd>See <a href="overlay.html">this demo page</a>.</dd>
        <dt>Can I change the default message text used when blocking the page?</dt>
        <dd>Yes.  The default message is stored in <code class="inline">$.blockUI.defaults.message</code>.
        You can change it simply by assigning a new value, like this:
        <pre><code class="mix">$.blockUI.defaults.message = "Please be patient...";</code></pre>
        </dd>
        <dt>Can I change the color or transparency of the overlay?</dt>
        <dd>Yes.  The default overlay CSS is stored in <code class="inline">$.blockUI.defaults.overlayCSS</code>.
        You can choose a different default overlay color and transparency value like this:
        <pre><code class="mix">// use yellow overlay
$.blockUI.defaults.overlayCSS.backgroundColor = '#ff0';

// make overlay more transparent
$.blockUI.defaults.overlayCSS.opacity = .2;
</code></pre>
        </dd>
        <dt>Does BlockUI support Opera 8?</dt>
        <dd>No.</dd>
        <dt>Why don't I see overlays in FF on Linux?</dt>
        <dd>Several people informed me that full page opacity rendering in FF/Linux is crazy slow, so by default it's
        disabled for that platform.  You can enable it by overriding the <code class="inline">applyPlatformOpacityRules</code>
        property like this:
        <pre><code class="mix">// enable transparent overlay on FF/Linux
$.blockUI.defaults.applyPlatformOpacityRules = false;</code></pre>
        </dd>
        <dt>I don't want the message content to be centered.  How do override the default position?</dt>
        <dd>See <a href="position.html">this demo page</a>.</dd>
        </dl>
    </div>

    <div id="download" class="tabContent">
        <h1>Download</h1>
        blockUI is available for download right here:
        <a href="http://github.com/malsup/blockui/raw/master/jquery.blockUI.js?v2.31">jquery.blockUI.js</a>.
        <p />
        It is also available from the project's github repository:
        <a class="external" href="http://github.com/malsup/blockui/">http://github.com/malsup/blockui/</a>.
        <p />
        The last version in the 1.x line, v1.33, is still available here:
        <a href="jquery.blockUI.1.33.js">jquery.blockUI.1.33.js</a>.
        <br />The old 1.x docs can be found <a href="index-old.html">here</a>.
        <p />
        <h1>Support</h1>
        Support for the BlockUI Plugin is available through the
        <a class="external" href="http://groups.google.com/group/jquery-en">jQuery Google Group</a>.
        <p />
        <h1>Change Log</h1>
        <a href="changes.txt?v2.31">changes.txt</a>
        <p />
        <div class="footer">
        The BlockUI Plugin and this documentation are the work of Mike Alsup.
        Send comments to jquery at malsup dot com.
        </div>
    </div>


</div>
<script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
<script type="text/javascript">
    _uacct = "UA-850242-2";
    urchinTracker();
</script>
</body></html>

