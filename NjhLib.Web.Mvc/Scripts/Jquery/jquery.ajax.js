function $ajax(url, type, data, dataType, suc, err, cache) {
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
/*get*/
function $ajaxGet(url, data, dataType, suc, err) {
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
function $ajaxGetHTML(url, data, suc, err) {
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
function $ajaxGetText(url, data, suc, err) {
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
function $ajaxGetJson(url, data, suc, err) {
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

/*post*/
function $ajaxPost(url, data, dataType, suc, err) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: dataType,
        data: data,
        success: suc,
        error: err,
        cache: false
    });
}
function $ajaxPostHTML(url, data, suc, err) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: "html",
        data: data,
        success: suc,
        error: err,
        cache: false
    });
}
function $ajaxPostText(url, data, suc, err) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: "text",
        data: data,
        success: suc,
        error: err,
        cache: false
    });
}
function $ajaxPostJson(url, data, suc, err) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: data,
        success: suc,
        error: err,
        cache: false
    });
}