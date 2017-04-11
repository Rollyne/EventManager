/**
 * Created by princ on 11.11.2016 г..
 */
//Dropdown settings
$(function () {
    let windowSizeBig = theWindow.width() >= 600;
    if (windowSizeBig) {
        $('#mainHeader').show();
        $('main').css({"margin-left": "300px"});
    } else {
        $('#mainHeader').hide();
        $('#sidebarToggler').attr('class', 'dropdownToggle');
    }
});
//TODO: It has few bugs while resizing
$(window).on("load", function () {
    let $userDrop = $('#mainHeader'),
        $userWrapper = $('#sidebarToggler'),
        $dropdownMenu = $('#headerContent'),
        windowSizeBig = theWindow.width() + 17 >= 600;

    function windowToggle() {
        if (theWindow.width() >= 600) {
            $userDrop.show(function () {
                $dropdownMenu.animate({opacity: 1}, 200);
                $dropdownMenu.show('slow');
            });
            $('main').css({"margin-left": "300px"});
            $userWrapper.css({"cursor": "default"});
            if ($userWrapper.hasClass('dropdownToggle')) {
                $userWrapper.removeAttr('class', 'dropdownToggle');
            }
        } else {
            $('main').css({"margin-left": "auto"});
            if ($userWrapper.hasClass('dropdownToggle')) {
                $userWrapper.attr('class', 'dropdownToggle');
            }
        }
    }

    theWindow.resize(windowToggle).trigger("resize");
});


var $icon = $('#notificationIcon'),
    $content = $('#notificationList *'),
    $wrapper = $('#notificationList');
function Dropdowner(idOfWrapper, idOfDropdowner, idOfContent) {
    let $userDrop = $('#' + idOfDropdowner),
        $userWrapper = $('#' + idOfWrapper),
        $dropdownMenu = $('#' + idOfContent),
        windowSizeBig = theWindow.width() >= 600;
    var $icon = $('#notificationIcon'),
        $content = $('#notificationList *'),
        $wrapper = $('#notificationList');
    if (!windowSizeBig) {
        if ($('.dropdownToggle').length == 0) {
            $('.searchDrop').slideUp('slow');
            $icon.removeClass('notificationClicked');
            $content.animate({opacity: 0},300, function (){$wrapper.slideUp('fast');});
            $dropdownMenu.animate({opacity: 0}, 300, function () {
                $userDrop.hide('slow');
            });
            $userWrapper.attr('class', 'dropdownToggle');
        } else {
            $userDrop.show(function () {
                $dropdownMenu.animate({opacity: 1}, 200);
                $dropdownMenu.show('slow');
            });
            $userWrapper.removeAttr('class', 'dropdownToggle');
        }
    }
}

/*SEARCH COLOR*/
$(function () {

    $('#searchContent').focus(function () {
        $('#searchWrapper').css('background-color', 'rgba(0,0,0,0.9)');
        var keyword = $("#searchContent").val();
        if (keyword.length >= 3) {
            $("#ajaxContent").val(keyword);
            $("#ajaxSearch").click();
        }
        else {
            $("#ajax").children().remove();
        }
    });
    //$(document).mouseup(function (e) {
    //    var container = $("#searchWrapper");

    //    if (!container.is(e.target) // if the target of the click isn't the container...
    //        && container.has(e.target).length === 0) // ... nor a descendant of the container
    //    {
    //        container.hide();
    //    }
    //});

    //$('#searchContent').blur(function () {
    //    $('#searchWrapper').css('background-color', 'rgba(0,0,0,0.4)');
    //    $('#ajax').children().remove();
    //});

    $('html').click(function () {
        $('#searchWrapper').css('background-color', 'rgba(0,0,0,0.4)');
        $('#ajax').children().remove();
    });
    $('#searchWrapper').click(function (event) {
        event.stopPropagation();
    });
});
//AJAX ON KEYUP
$(function () {
    $("#searchContent").keyup(function () {
        var keyword = $("#searchContent").val();
        if (keyword.length >= 3) {
            $("#ajaxContent").val(keyword);
            $("#ajaxSearch").click();
        }
        else {
            $("#ajax").children().remove();
        }
    });
});

//USELESS HEADERS
//$(function () {
//    if ($('#searchUser').length == 0) {
//        $('#searchUserHead').hide();
//    }
//    if ($('#searchEvent').length == 0) {
//        $('#searchEventHead').hide();
//    }
//});
/*NOTIFICATION DISPLAY*/
$(function () {
    let $icon = $('#notificationIcon'),
        $content = $('#notificationList *'),
        $wrapper = $('#notificationList');
    $icon.on('click', function () {
        if ($icon.hasClass('notificationClicked')) {
            $icon.removeClass('notificationClicked');
            $wrapper.slideUp('fast');

        } else {
            if ($('.searchDrop').hasClass('dropShow')) {
                $('#ajax').children().remove();
            }
            $icon.addClass('notificationClicked');
            $.when($wrapper.slideDown('slow')).done(function(){
                $content.css('opacity','1');
            });
        }
    });
});