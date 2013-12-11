var _ua = navigator.userAgent.toLowerCase();
var $IE = /msie/.test(_ua);
var $moz = /gecko/.test(_ua);
var $Safari = /safari/.test(_ua);
var inputField;
var adapter = 2;
function aaavmpcomplete(id, url) {
    var divcid = id + 'list_box_complete_vmp';
    var keywords = $('#' + id).val();
    var data = 'query=' + keywords;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (html) {
            $('.' + divcid).show();
            html = "<li>niejunhua</li><li>niejunfang</li><li>nieting</li><li>nieai</li><li>niehuan</li>";
            $('#_' + divcid + ' .keywords_list').html(html); //<li>内容内容</li>拼接的字符串
            $('.keywords_list li').css("padding-left", "4px");
            $('.keywords_list li').hover(function () {
                $(this).addClass('hover');
            }, function () {
                $(this).removeClass('hover');
            });
            $('.keywords_list li').click(function () {
                $('#' + id).val($(this).text());
                $('.' + divcid).hide();
            });
        }
    });
    return false;
}


function aaavmpcompleteinit(id, url) {
    $("#" + id).attr("AUTOCOMPLETE", "off");
    var divcid = id + 'list_box_complete_vmp';
    var cls = "asdfghjkl";
    // $(".asdfghjkl").css("display", "none");
    var $nowDiv = $("#_" + divcid);
    if ($nowDiv.length > 0) {
        $nowDiv.css("display", "block");
    } else {
        var $div = $('<div id="_' + divcid + '" class="' + divcid + ' asdfghjkl" style="display: none"> <div class="keywords_list"> </div></div>');
        $div.appendTo("body");
        $nowDiv = $("#_" + divcid);
        $nowDiv.css("display", "block");
    }
    var s = ' <style type="text/css" id="completestyle"> .tips{ font-size: 12px;}.' + divcid + '{ position: absolute;cursor:pointer;width: 280px;overflow:hidden;background: #f3f3f3;border: 1px solid #CCC;}.keywords_list{margin: 0;padding: 0;list-style: none;}.hover{background: #33CCFF;color: #333333;}</style>';
    if ($("#completestylecss").length == 0) {
        $(s).appendTo("body");
    }

    var div = $("#_" + divcid)[0];
    var curHeight = $("#" + id).height();
    inputField = $("#" + id)[0];
    var top = calculateOffsetTop();
    var left = calculateOffsetLeft();
    div.style.left = left + "px";
    div.style.top = top + curHeight + adapter + "px";

    $("." + cls).hide();
    $("." + divcid).mouseleave(function () {
        $(this).hide();
    });

    $("#" + id).click(function () {
        hidden();
        aaavmpcomplete(id, url);
    });
    $("#" + id).keyup(function () {
        hidden();
        aaavmpcomplete(id, url);
    });
    $(document).click(function () {
        hidden();
    });
}

function hidden() {
    $(".asdfghjkl").hide();
}

function myComplete(id, url) {
    aaavmpcompleteinit(id, url);
}


function calculateOffsetLeft() {
    return calculateOffset("offsetLeft");
}

function calculateOffsetTop() {
    return calculateOffset("offsetTop");
}

function calculateOffset(attr) {
    var offset = 0;
    var tmpField = inputField;
    while (tmpField) {
        offset += tmpField[attr];
        tmpField = tmpField.offsetParent;
    }
    return offset;
}
