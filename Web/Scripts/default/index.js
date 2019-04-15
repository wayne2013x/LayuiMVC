$(function () {
    //判断是否点击了收缩菜单
    var check_shrink = 0;

    //悬浮侧边一级菜单
    $("#LAY-system-side-menu .layui-nav-item").hover(function () {
        if (check_shrink == 0) {
            $("#side_menu_bar").css({ "top": ($(this).offset().top - 45) + "px", "opacity": "1" });
        } else {
            $("#side_menu_bar").css({ "top": ($(this).offset().top + 5) + "px", "opacity": "1" });
        }
    }, function () {
        if (check_shrink == 0) {
            $("#side_menu_bar").css({ "top": ($(this).offset().top - 45) + "px", "opacity": "0" });
        } else {
            $("#side_menu_bar").css({ "top": ($(this).offset().top + 5) + "px", "opacity": "0" });
        }
    });

    //悬浮顶部一级菜单
    $("#top_menu .layui-nav-item").hover(function () {
        $("#top_menu_bar").css({ "left": ($(this).offset().left + 10) + "px", "opacity": "1", "width": $(this).width() - 15 });
        $(this).find(".two_menu_list").css({ "display": "flex" });
    }, function () {
        $("#top_menu_bar").css({ "left": ($(this).offset().left + 10) + "px", "opacity": "0", "width": $(this).width() - 15 });
        $(this).find(".two_menu_list").css({ "display": "none" });
    });

    $(".two_menu_list").hover(function () {
        $(this).css({ "display": "flex" });
    }, function () {
        $(this).css({ "display": "none" });
    });

    //点击侧边一级菜单
    $("#LAY-system-side-menu .layui-nav-item").click(function () {
        if ($(this).hasClass("layui-nav-itemed")) {
            $(this).removeClass("layui-nav-itemed");
        } else {
            $("#LAY-system-side-menu .layui-nav-item").removeClass("layui-nav-itemed");
            $(this).addClass("layui-nav-itemed");
        }
    });

    //点击侧边二级级菜单
    $(".two_menu_name").click(function () {
        if ($(this).parent().hasClass("layui-nav-itemed")) {
            $(this).parent().removeClass("layui-nav-itemed");
        } else {
            $(".two_menu_name").parent().removeClass("layui-nav-itemed");
            $(this).parent().addClass("layui-nav-itemed");
        }
    });

    //点击侧边三级级菜单
    $(".three_menu_name").click(function () {
        $(".three_menu_name").parent().removeClass("layui-this");
        $(this).parent().addClass("layui-this");

        var menu_name = $(this).text();
        var menu_href = $(this).attr("lay-href");
        var menu_id = $(this).attr("data-id");

        //判断是否已经存在该选项卡
        if ($("#tab_" + menu_id).length == 0) {
            //不存在 新建选项卡

            //选项卡
            $("#LAY_app_tabsheader li").removeClass("layui-this");
            var tab_html = '<li id="tab_' + menu_id + '" data-id="' + menu_id + '" lay-attr="' + menu_href + '" class="layui-this tab"><span>' + menu_name + '</span><i class="layui-icon layui-unselect layui-tab-close">ဆ</i></li>';
            $("#LAY_app_tabsheader").append(tab_html);

            //iframe
            $("#LAY_app_body div").removeClass("layui-show");
            var iframe_html = '<div id="iframe_' + menu_id + '" class="layadmin-tabsbody-item layui-show"><iframe src="' + menu_href + '" frameborder="0" class="layadmin-iframe"></iframe></div>';
            $("#LAY_app_body").append(iframe_html);
        } else {
            //已经存在 刷新当前选项卡
            $(".tab").removeClass("layui-this");
            $("#tab_" + menu_id).addClass("layui-this");

            $("#LAY_app_body div").removeClass("layui-show");
            $("#ifame_" + menu_id).addClass("layui-show");
            $("#ifame_" + menu_id).remove();
            var iframe_html = '<div id="iframe_' + menu_id + '" class="layadmin-tabsbody-item layui-show"><iframe src="' + menu_href + '" frameborder="0" class="layadmin-iframe"></iframe></div>';
            $("#LAY_app_body").append(iframe_html);
        }
    });

    //点击头部三级级菜单
    $(".top_three_menu_name").click(function () {
        var menu_name = $(this).text();
        var menu_href = $(this).attr("lay-href");
        var menu_id = $(this).attr("data-id");

        //判断是否已经存在该选项卡
        if ($("#tab_" + menu_id).length == 0) {
            //不存在 新建选项卡

            //选项卡
            $("#LAY_app_tabsheader li").removeClass("layui-this");
            var tab_html = '<li id="tab_' + menu_id + '" data-id="' + menu_id + '" lay-attr="' + menu_href + '" class="layui-this tab"><span>' + menu_name + '</span><i class="layui-icon layui-unselect layui-tab-close">ဆ</i></li>';
            $("#LAY_app_tabsheader").append(tab_html);

            //iframe
            $("#LAY_app_body div").removeClass("layui-show");
            var iframe_html = '<div id="iframe_' + menu_id + '" class="layadmin-tabsbody-item layui-show"><iframe src="' + menu_href + '" frameborder="0" class="layadmin-iframe"></iframe></div>';
            $("#LAY_app_body").append(iframe_html);
        } else {
            //已经存在 刷新当前选项卡
            $(".tab").removeClass("layui-this");
            $("#tab_" + menu_id).addClass("layui-this");

            $("#LAY_app_body div").removeClass("layui-show");
            $("#ifame_" + menu_id).addClass("layui-show");
            $("#ifame_" + menu_id).remove();
            var iframe_html = '<div id="iframe_' + menu_id + '" class="layadmin-tabsbody-item layui-show"><iframe src="' + menu_href + '" frameborder="0" class="layadmin-iframe"></iframe></div>';
            $("#LAY_app_body").append(iframe_html);
        }
    });

    //点击选项卡
    $('body').delegate('.tab', 'click', function () {
        $(".tab").removeClass("layui-this");
        $(this).addClass("layui-this");

        var menu_id = $(this).attr("data-id");
        $("#LAY_app_body div").removeClass("layui-show");
        $("#iframe_" + menu_id).addClass("layui-show");

        $(".three_menu_name").parent().removeClass("layui-this");
        $("#menu_id_" + menu_id).parent().addClass("layui-this");
    });

    //关闭选项卡
    $('body').delegate('.layui-tab-close', 'click', function (e) {
        e.stopPropagation();

        $(".tab").removeClass("layui-this");

        $("#LAY_app_body div").removeClass("layui-show");

        var this_parent = $(this).parent();
        var menu_id = this_parent.attr("data-id");
        $("#iframe_" + menu_id).remove();
        $("#tab_" + menu_id).remove();

        $(".three_menu_name").parent().removeClass("layui-this");

        $("#tab_0").addClass("layui-this");
        $("#iframe_0").addClass("layui-show");
    });

    var check_count = 0;

    //收缩侧边菜单
    $("#shrink").click(function () {
        if (check_count == 0) {
            if ($("#LAY_app").hasClass("layadmin-side-shrink")) {
                check_shrink = 0;
                $("#LAY_app").removeClass("layadmin-side-shrink");

                //侧边栏展开
                $(".layui-side-menu").css("width", "210px");
                //logo展开
                $(".layui-logo").css("width", "210px");
                //iframe展开
                $("#LAY_app_body").css({ "left": "210px" });
                //选项卡展开
                $("#LAY_app_tabs").css({ "left": "210px" });
                //头部展开
                $(".layui-layout-left").css("left", "210px");

                $("#LAY_app_flexible").removeClass("layui-icon-spread-left");
                $("#LAY_app_flexible").addClass("layui-icon-shrink-right");
            } else {
                check_shrink = 1;
                $("#LAY_app").addClass("layadmin-side-shrink");

                //侧边栏收缩
                $(".layui-side-menu").css("width", "60px");
                //logo收缩
                $(".layui-logo").css("width", "60px");
                $(".layui-logo").css("background-color", "#324157");
                //iframe收缩
                $("#LAY_app_body").css({ "left": "60px" });
                //选项卡收缩
                $("#LAY_app_tabs").css({ "left": "60px" });
                //头部收缩
                $(".layui-layout-left").css("left", "60px");

                $("#LAY_app_flexible").removeClass("layui-icon-shrink-right");
                $("#LAY_app_flexible").addClass("layui-icon-spread-left");
            }
        }
    });


    //打开顶部菜单
    $("#top_menu_btn").click(function () {
        if (check_count == 0) {
            //侧边栏收缩
            $(".layui-side-menu").css("width", "0px");
            //logo收缩
            $(".layui-logo").css("width", "0px");
            //iframe收缩
            $("#LAY_app_body").css({ "left": "0px", "top": "140px" });
            //选项卡收缩
            $("#LAY_app_tabs").css({ "left": "0px", "top": "100px" });
            //头部收缩
            $(".layui-header").css({ "top": "50px" });
            $(".layui-layout-left").css("left", "0px");

            check_count = 1;
        } else {
            $("#LAY_app").removeClass("layadmin-side-shrink");
            $("#LAY_app_flexible").removeClass("layui-icon-spread-left");
            $("#LAY_app_flexible").addClass("layui-icon-shrink-right");

            //侧边栏展开
            $(".layui-side-menu").css("width", "220px");
            //logo展开
            $(".layui-logo").css("width", "220px");
            //iframe展开
            $("#LAY_app_body").css({ "left": "220px", "top": "90px" });
            //选项卡展开
            $("#LAY_app_tabs").css({ "left": "220px", "top": "50px" });
            //头部展开
            $(".layui-header").css({ "top": "0px" });
            $(".layui-layout-left").css("left", "220px");

            check_count = 0;
        }
    });

    //刷新
    $("#reload").click(function () {
        window.location.reload();
    });

    //开启全屏
    $("#full_screen").click(function () {
        var el = document.documentElement;
        var rfs = el.requestFullScreen || el.webkitRequestFullScreen ||
            el.mozRequestFullScreen || el.msRequestFullScreen;
        if (typeof rfs != "undefined" && rfs) {
            rfs.call(el);
        } else if (typeof window.ActiveXObject != "undefined") {
            var wscript = new ActiveXObject("WScript.Shell");
            if (wscript != null) {
                wscript.SendKeys("{F11}");
            }
        }
    });

    //取消冒泡事件
    $("#LAY-system-side-menu dl a").click(function (e) {
        e.stopPropagation();
    });

    //点击左侧选项卡
    $("#tab_left").click(function () {
        tab_click = 0;
        $("#tab_main ul").css("left", "0px");
    });

    //记录点击了几次
    var tab_click = 0;
    //点击右侧选项卡
    $("#tab_right").click(function () {
        tab_click++;
        var width = $("#tab_main").width() - 45;
        $("#tab_main ul").css("left", "-" + width * tab_click + "px");
    });
});