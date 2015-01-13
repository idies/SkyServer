﻿var search = false;
function gotochart(ra, dec) {
    var s = "../chart/chart.aspx?ra=" + ra + "&dec=" + dec;
    var w = window.open(s, "_blank");
    w.focus();
}
function gotonavi(ra, dec) {
    var s = "../chart/navi.aspx?ra=" + ra + "&dec=" + dec;
    var w = window.open(s, "_top");
    w.focus();
}

function setSDSS(id) {
    var s = "setSDSS.aspx?id=" + id;
    var w = window.open(s, "POPUP", "width=240,height=220");
    w.focus();
}
function setEq(ra, dec) {
    var s = "setEq.aspx?ra=" + ra + "&dec=" + dec;
    var w = window.open(s, "POPUP", "width=240,height=220");
    w.focus();
}
function setSid(sid) {
    var s = "setSid.aspx?sid=" + sid;
    var w = window.open(s, "POPUP", "width=240,height=220");
    w.focus();
}
function setId(id) {
    var s = "setId.aspx?id=" + id;
    var w = window.open(s, "POPUP", "width=240,height=220");
    w.focus();
}
function setPlfib(sid) {
    var s = "setPlfib.aspx?sid=" + sid;
    var w = window.open(s, "POPUP", "width=240,height=220");
    w.focus();
}
function loadSummary(id) {
    parent._top.document.location = "summary.aspx?id=" + id;
}
function showNotes() {
    var s = "../chart/book.aspx";
    var w = window.open(s, "POPUP");
    w.focus();
}
function framePrint() {
    window.print();
    /*
    var pr = (window.print) ? 1 : 0;
    var da = (document.all) ? 1 : 0;
    var mac = (navigator.userAgent.indexOf("Mac") != -1);
    if (pr && da) {		// IE5
        parent._top.focus();
        window.print();
    } else if (pr) {	// NS4
        parent._top.print();
    } else {
        alert("Sorry, your browser does not support this feature");
    }
    */
}
function saveBook(id) {
    var url = "../chart/book.aspx?add=" + id;
    var frame = document.getElementById("test");
    frame.src = url;
    //	w = window.open(url, "BOOK","width=100,height=100");
    //	w.close();
}

function padHex(a, m) {
    var h = Number(a).toString(16);
    var s = "";
    if (h.length < m) {
        for (var i = 0; i < m - h.length; i++)
            s += "0";
    }
    s += h;
    return s;
}

function callNameResolver() {
    $.ajax({
        type: "GET",
        url: "http://vaodev.stsci.edu/portal/Mashup/Mashup.asmx/invoke",
        dataType: ($.browser.msie) ? "text" : "xml",
        data: 'request={"service":"Mast.Name.Lookup","format":"xml","params":{"input":"' + document.getElementById('searchName').value + '"},"timeout":10,"page":1,"pagesize":100}',
        success: function (temp) {
            //document.getElementById('getImageId').disabled = false;
            if (typeof temp == "plain") {
                alert(temp);
            } else {
                xml = temp;
                $(xml).find('resolvedCoordinate').each(function () {
                    var ra1 = $(this).find('ra').text();
                    var dec1 = $(this).find('dec').text();
                    ra = ra1;
                    dec = dec1;
                    $('#searchRA').val(ra1);
                    $('#searchDec').val(dec1);
                    //resubmit();
                });
            }
        },
        error: function () {
            if (jQuery.trim(objtext).length > 0) {
                alert("Could not resolve name: " + objtext);
            }
        }
    });
}

function resolveName() {
    var name = $('#searchName').val();
    $.ajax({
        type: "GET",
        url: "../Resolver.ashx?name=" + name,
        success: function (response) {
            if (response.indexOf("Error:") == 0) {
                alert(response);
            }
            else {
                var s = response.split('\n');
                $('#searchName').val(s[0].substring(6));
                $('#searchRA').val(s[1].substring(4));
                $('#searchDec').val(s[2].substring(5));
            }
        },
        error: function () {
            alert("Error: Could not resolve name.");
        }
    });
}

function resolveCoords() {
    var ra = $('#searchRA').val();
    var dec = $('#searchDec').val();
    $.ajax({
        type: "GET",
        url: "../Resolver.ashx?ra=" + ra + "&dec=" + dec,
        success: function (response) {
            if (response.indexOf("Error:") == 0) {
                alert(response);
            }
            else {
                var s = response.split('\n');
                $('#searchName').val(s[0].substring(6));
                $('#searchRA').val(s[1].substring(4));
                $('#searchDec').val(s[2].substring(5));
            }
        },
        error: function () {
            alert("Error: Could not resolve coordinates.");
        }
    });
}

function press_ok(kind) {
    var windowPage = "summary.aspx";
    var f = (document.layers) ? document.ctrl.document.forms[0] : document.forms[0];
    switch (kind) {
        case 'name':
            //callNameResolver();
            break;
        case "objid":
            window.location = windowPage+'?id=' + f.searchObjID.value;
            break;
        case "radec":
            window.location = windowPage+'?ra=' + f.searchRA.value + '&dec=' + f.searchDec.value;
            break;
        case "sdss":
            var a = String(f.searchSDSS.value).split("-");
            if (a.length != 5) {
                alert('The SDSS Id has 5 parts,\n Run-Rerun-Camcol-Field-Obj\n');
                return false;
            }
            var rerun = Number(a[1]) + 2048;  // skyversion=1
            var run = Number(a[0]);
            var camcol = Number(a[2]);
            var field = Number(a[3]);
            var obj = Number(a[4]);
            var cf = 8192 * camcol + field;

            var s = "0x";
            s += padHex(rerun, 4);
            s += padHex(run, 4);
            s += padHex(cf, 4);
            s += padHex(obj, 4);

            window.location = windowPage+'?id=' + s;
            break;
        case "specid":
            window.location = windowPage+'?sid=' + encodeURIComponent(f.searchSpecID.value);
            break;
        case "plfib":
            window.location = windowPage+'?plate=' + f.searchPlate.value + '&mjd=' + f.searchMJD.value + '&fiber=' + f.searchFiber.value;
            break;
        default:
            alert('Not supported');
            break;
    }

    return false;
}

function toggleSearch() {
    if (search) {
        document.getElementById("search").style.display = "none";
        document.getElementById("content").style.position = "absolute";
        document.getElementById("content").style.top = "0px";
        document.getElementById("content").style.left = "135px";
        search = false;
    }
    else {
        document.getElementById("content").style.position = "absolute";
        document.getElementById("content").style.top = "100px";
        document.getElementById("content").style.left = "135px";
        document.getElementById("search").style.display = "block";
        search = true;
    }
}


