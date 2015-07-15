//Ipad方向転換対応
window.onorientationchange = detectIPadOrientation;
function detectIPadOrientation() {

    var goResize = false;
    var gap = 60;

    if ($.browser.isLandscape) {
        gap = 60;
        goResize = true;
    } else if ($.browser.isPortrait) {
        gap = 10;
        goResize = true;
    }

    if (goResize) {
        var position = $("#_dialogDiv").position();
        var h = $(window).height() - gap;
        position.top = gap / 2;
        $("#_dialogDiv").dialog("option", "height", h);
        $("#_dialogDiv").dialog("option", "position", "center");

        $("#_dialogChildDiv").dialog("option", "height", h);
        $("#_dialogChildDiv").dialog("option", "position", "center");

        $("#_dialogChild2Div").dialog("option", "height", h);
        $("#_dialogChild2Div").dialog("option", "position", "center");

        $("#_dialogChild3Div").dialog("option", "height", h);
        $("#_dialogChild3Div").dialog("option", "position", "center");
    }
}

//ポップ画面表示
function showDialogEx(url, title, h, callback, args) {
  
    var isPad = window.top.$.browser.IPad;
    var w = isPad ? 735 : 740;

    showDialogInner(url, title, w, h, callback, args);
}

function showDialog(url, title, callback, args) {

    var isPad = window.top.$.browser.IPad;
    var w = isPad ? 735 : 740;
    var h;
    if (isPad) {
        h = 500;
        if (window.top.$.browser.isLandscape) {
            h = $(window.top).height() - 60;
        }
        else if (window.top.$.browser.isPortrait) {
            h = $(window.top).height() - 10;
        }
    } else {
        h = 700;        
        h = $(window.top).height() - 60;
    }

    showDialogInner(url, title, w, h, callback, args);
}

function showDialogWithWidth(url, title, w, callback, args) {
    
    var isPad = window.top.$.browser.IPad;
    var h;
    if (isPad) {
        h = 500;
        if (window.top.$.browser.isLandscape) {
            h = $(window.top).height() - 60;
        }
        else if (window.top.$.browser.isPortrait) {
            h = $(window.top).height() - 10;
        }
    } else {
        h = 700;
        h = $(window.top).height() - 60;
    }

    showDialogInner(url, title, w, h, callback, args);
}

function showDialogInner(url, title, w, h, callback, args) {

    var vDialogId;

    if (window.frameElement != null && window.frameElement.parentElement !== undefined && window.frameElement.parentElement !== null) {
        vDialogId = window.frameElement.parentElement.id;
    } else {
        vDialogId = "_top";
    }

    var dialogDiv;
    var dialogMsg;
    var dialogFrame;

    if (vDialogId == '_top') {
        dialogDiv = '_dialogDiv';
        dialogMsg = '_dialogMsg';
        dialogFrame = '_iframeContent';
    } else if (vDialogId == '_dialogDiv') {
        dialogDiv = '_dialogChildDiv';
        dialogMsg = '_dialogChildMsg';
        dialogFrame = '_iframeChildContent';
    } else if (vDialogId == '_dialogChildDiv') {
        dialogDiv = '_dialogChild2Div';
        dialogMsg = '_dialogChild2Msg';
        dialogFrame = '_iframeChild2Content';
    } else if (vDialogId == '_dialogChild2Div') {
        dialogDiv = '_dialogChild3Div';
        dialogMsg = '_dialogChild3Msg';
        dialogFrame = '_iframeChild3Content';
    }

    window.top.$("#" + dialogDiv).dialog({
        title: title,
        autoOpen: true,
        width: w,
        height: h,
        modal: true,
        buttons: {
            "キャンセル": function () {
                window.top.$('#' + dialogDiv).dialog("close");
            }
        },
        close: function () {
            if (callback !== undefined && callback !== null)
                callback(args);
        }
    });

    window.top.$("#" + dialogDiv).html("");
    window.top.$("#" + dialogDiv).append('<div id="' + dialogMsg + '" title="Loading..."><br/><b>画面ロード中です、暫くお待ちください...</b></div>');
    window.top.$("#" + dialogMsg).dialog({
        modal: true,
        autoOpen: true,
        draggable: true,
        resizable: false,
        dialogClass: 'no-close',
        width: 360,
        height: 125
    });
    
    window.top.$("#" + dialogDiv).append('<iframe id="' + dialogFrame + '" src="' + url + '" width="' + (w - 18) + '"'
               + ' height="100%" frameborder="0" scrolling="yes"></iframe>');

    window.top.$("#" + dialogFrame).load(function () {
        window.top.$("#" + dialogMsg).dialog("destroy").remove();
        //window.top.$("#" + dialogMsg).dialog("close");
    });
}