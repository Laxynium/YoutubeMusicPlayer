﻿$(document).ready(function ()
{
    function e(e) {
        var r = "";
        return e = $.trim(e), -1 < e.indexOf("youtube.com/")
            ? (r =
                /v\=[a-zA-Z0-9\-\_]{11}/.exec(e)) &&
            (r = r.toString().substr(
                2))
            : -1 < e.indexOf("youtu.be/") &&
            (r =
                /\/[a-zA-Z0-9\-\_]{11}/.exec(e)) &&
            (r = r.toString().substr(
                1)), null != r && 11 == r.length && r;
    }

    function r(e)
    {
        $("#converter_wrapper").before('<div id="error"><span>' +
            e +
            '</span><span><a href="">Please try to convert another YouTube video by clicking here</a></span></div><div id="pa_download"><a href="https://ytmp3.cc/d/" rel="nofollow" target="_blank">Download</a></div>'
        ), $("#converter_wrapper").remove(), $("#error").show(), $(
            "#pa_download").show();
    }
    //Set link to download
    function o(e, r, o, t)
    {
        s && $("#dropbox").show(), $("#progress").hide(), $(
            "#download #file").attr("href",
            "https://" +
            h[o] +
            ".ymcdn.cc/" +
            t +
            "/" +
            e), $("#download").show(), $(
            "#pa_download").show();
    }

    function t(e, a, n) {
        $.ajax({
            url: "https://d.ymcdn.cc/progress.php",
            data: {
                id: n
            },
            dataType: "jsonp",
            success: function(s) {
                if ($.each(s,
                    function(e, r) {
                        s[e] = "hash" == e ? r : parseInt(r);
                    }), 0 < s.error)
                    return r(f.ct[s.error]),
                        $.ajax({
                            url: "error.php",
                            async: !1,
                            cache: !1,
                            data: {
                                f: 2,
                                e: s.error,
                                s: s.sid,
                                v: e,
                                h: n
                            },
                            type: "POST"
                        }), !1;
                switch (s.progress) {
                case 0:
                case 1:
                case 2:
                    $("#progress span").html(l[s.progress]);
                    break;
                case 3:
                    c = !0, o(e, a, s.sid, n);
                }
                c ||
                    window.setTimeout(function() {
                            t(e, a, n);
                        },
                        3e3);
            }
        });
    }

    function a(e, a) {
        $.ajax({
            url: "https://d.ymcdn.cc/check.php",
            data: {
                v: e,
                f: a
            },
            dataType: "jsonp",
            success: function(n) {
                if ($.each(n,
                    function(e, r) {
                        n[e] = "hash" == e ||
                            "title" ==
                            e
                            ? r
                            : parseInt(r);
                    }), 0 < n.error)
                    return r(f.ck[n.error]),
                        $.ajax({
                            url: "error.php",
                            async: !1,
                            cache: !1,
                            data: {
                                f: 1,
                                e: n.error,
                                s: "/",
                                v: e,
                                h: n.hash
                            },
                            type: "POST"
                        }), !1;
                0 < n.title.length ? $("#title").html(n.title) : $("#title").html("no title"), 0 < n.ce
                    ? o(e, a, n.sid, n.hash)
                    : t(e, a, n.hash);
            }
        });
    }

    var n = !1,
        c = !1,
        s = !1,
        i = [
            "Please insert a YouTube video URL",
            "A parameter error occurred. The conversion was cancelled. Please delete your browser cache and try it again.",
            "A balancer error occurred. The conversion was cancelled. Please refresh the page and try it again.",
            "The youtube video you try to convert is blacklisted due a DMCA request. Please try to convert another video.",
            "A conversion error occurred. The conversion was cancelled. Please refresh the page and try it again.",
            "A download error occurred. The conversion was cancelled. Please refresh the page and try it again.",
            "The video could not be downloaded because of the age restriction. We are not able to convert or download any videos with age restriction. Please try to convert another video.",
            "We are sorry. The selected video is longer than 2 hours. We are only able to convert videos with a length up to 2 hours (120 minutes).",
            "A database error occurred. The conversion was cancelled. Please refresh the page and try it again."
        ],
        d = "mp3",
        p = !1,
        l = ["checking video", "loading video", "converting video"],
        h = {
            1: "odg",
            2: "ado",
            3: "jld",
            4: "tzg",
            5: "uuj",
            6: "bkl",
            7: "fnw",
            8: "eeq",
            9: "ebr",
            10: "asx",
            11: "ghn",
            12: "eal",
            13: "hrh",
            14: "quq",
            15: "zki",
            16: "tff",
            17: "aol",
            18: "eeu",
            19: "kkr",
            20: "yui",
            21: "yyd",
            22: "hdi",
            23: "ddb",
            24: "iir",
            25: "ihi",
            26: "heh",
            27: "xaa",
            28: "nim",
            29: "omp",
            30: "eez"
        },
        u = "",
        f = {
            ck: {
                1: i[1],
                2: i[2],
                3: i[3]
            },
            ct: {
                1: i[4],
                3: i[5],
                7: i[4],
                9: i[6],
                11: i[7],
                100: i[8]
            }
        };
    $.ajax({
        url: "p.php",
        data: {
            c: 1
        },
        dataType: "jsonp",
        success: function(e) {
            e.p && (p = !0);
        }
    });
    var b = document.createElement("script");
    b.setAttribute("id", "dropboxjs"), b.setAttribute("type",
        "text/javascript"), b.setAttribute("src",
        "https://www.dropbox.com/static/api/2/dropins.js"), b.setAttribute(
        "data-app-key",
        "nlg9bp1z4vdpbvm"), b.onload = function() {
        return "object" == typeof Dropbox &&
        (!!Dropbox.isBrowserSupported() &&
            void(s = !0));
    }, document.body.appendChild(b), $("#input").focus(), $(
        "#dropbox").click(function() {
        $("#dropbox_wrapper").length && $("#dropbox_wrapper").remove(),
            $("#formats").after(
                '<div id="dropbox_wrapper"></div>');
        var e = {
            cancel: function() {
                $("#dropbox_wrapper").html(
                    "Upload to Dropbox failed.");
            },
            error: function(e) {
                $("#dropbox_wrapper").html(
                    "An error occured. Upload failed."
                );
            },
            progress: function(e) {
                $("#dropbox_wrapper").html(
                    'Uploading file to dropbox <i class="fa fa-cog fa-spin"></i>'
                );
            },
            success: function() {
                $("#dropbox_wrapper").html(
                    "Success! File was saved to your Dropbox."
                );
            }
        };
        return Dropbox.save($("#file").attr("href"),
            $("#title")
            .html() +
            "." +
            d.toLowerCase(),
            e), !1;
    }), $("#file").click(function() {
        return p && (window.open("https://ytmp3.cc/p/"), p = !1),
            document.location.href = $(this).attr("href"), !1;
        }), $("#formats a").click(function ()
        {
        if (!n)
            switch ($(this).attr("id")) {
            case "mp3":
                d = "mp3", $("#mp3").css("background-color",
                    "#007cbe"), $("#mp4").css(
                    "background-color",
                    "#0087cf");
                break;
            case "mp4":
                d = "mp4", $("#mp4").css("background-color",
                    "#007cbe"), $("#mp3").css(
                    "background-color",
                    "#0087cf");
            }
        return !1;
        }), $("form").submit(function ()
        {
        return (u = e($("#input").val()))
            ? (n = !0, $("form").hide(),
                $("#progress").show(), a(u, d), !1)
            : ($(
                "#input").val(i[0]), !1);
        });
});