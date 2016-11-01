/**
 * Created by Dimitar Tonchev on 18.9.2016 г..
 */


//Auto Resizing Text Area

var theWindow = $(window);

//Background Shrink Properties
$(window).on("load", function () {
    let $bg = $("#bg"),
        aspectRatio = $bg.width() / $bg.height();

    function resizeBg() {
        if ((theWindow.width() / theWindow.height()) < aspectRatio) {
            $bg.removeClass()
                .addClass('bgheight');
        } else {
            $bg.removeClass()
                .addClass('bgwidth');
        }
    }

    theWindow.resize(resizeBg).trigger("resize");
});

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



function Dropdowner(idOfWrapper, idOfDropdowner, idOfContent) {
    let $userDrop = $('#' + idOfDropdowner),
        $userWrapper = $('#' + idOfWrapper),
        $dropdownMenu = $('#' + idOfContent),
        windowSizeBig = theWindow.width() >= 600;
    if (!windowSizeBig) {
        if ($('.dropdownToggle').length == 0) {
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

//Home Page Views
// TODO: Shorten the code by making the views in functions and using "this" as a tool
// TODO: Make escaping and add regular expressions
function loginFormView() {
    let $homeBtn = $('#homeButton'),
        $loginBtn = $('#loginButton'),
        $registerBtn = $('#registerButton'),
        $loginForm = $('#loginForm'),
        $registerForm = $('#registerForm');
    if ($homeBtn.hasClass('selectedHeaderButton')) {
        $homeBtn.removeAttr('class', 'selectedHeaderButton');
        $homeBtn.attr('class', 'buttonHeader');
        $loginBtn.attr('class', 'selectedHeaderButton buttonHeader');
        $('#mainCenterText').hide('slow');
        $loginForm.show('slow');
    } else if ($registerBtn.hasClass('selectedHeaderButton')) {
        $registerBtn.removeAttr('class', 'selectedHeaderButton');
        $registerBtn.attr('class', 'buttonHeader');
        $loginBtn.attr('class', 'selectedHeaderButton buttonHeader');
        $('#registerFormContent').animate({opacity: 0}, 200, function () {
            $registerForm.hide('slow', function () {
                $loginForm.show('slow');
                $('#registerFormContent').css({opacity: 1});

            });
        });
    }
}

function registerFormView() {
    let $homeBtn = $('#homeButton'),
        $loginBtn = $('#loginButton'),
        $registerBtn = $('#registerButton'),
        $loginForm = $('#loginForm'),
        $registerForm = $('#registerForm');
    if ($homeBtn.hasClass('selectedHeaderButton')) {
        $homeBtn.removeAttr('class', 'selectedHeaderButton');
        $homeBtn.attr('class', 'buttonHeader');
        $registerBtn.attr('class', 'selectedHeaderButton buttonHeader');
        $('#mainCenterText').hide('slow');
        $registerForm.show('slow');
    } else if ($loginBtn.hasClass('selectedHeaderButton')) {
        $loginBtn.removeAttr('class', 'selectedHeaderButton');
        $loginBtn.attr('class', 'buttonHeader');
        $registerBtn.attr('class', 'selectedHeaderButton buttonHeader');
        $('#loginFormContent').animate({opacity: 0}, 200, function () {
            $loginForm.hide('slow', function () {
                $registerForm.show('slow');
                $('#loginFormContent').css({opacity: 1});
            });
        });
    }
}

function homeView() {

    let $homeBtn = $('#homeButton'),
        $loginBtn = $('#loginButton'),
        $registerBtn = $('#registerButton'),
        $loginForm = $('#loginForm'),
        $registerForm = $('#registerForm');
    if ($loginBtn.hasClass('selectedHeaderButton')) {
        $loginBtn.removeAttr('class', 'selectedHeaderButton');
        $loginBtn.attr('class', 'buttonHeader');
        $homeBtn.attr('class', 'selectedHeaderButton buttonHeader');
        $('#loginFormContent').animate({opacity: 0}, 200, function () {
            $loginForm.hide('slow', function () {
                $('#mainCenterText').show('slow');
                $('#loginFormContent').css({opacity: 1});
            });
        });
    } else if ($registerBtn.hasClass('selectedHeaderButton')) {
        $registerBtn.removeAttr('class', 'selectedHeaderButton');
        $registerBtn.attr('class', 'buttonHeader');
        $homeBtn.attr('class', 'selectedHeaderButton buttonHeader');
        $('#registerFormContent').animate({opacity: 0}, 200, function () {
            $registerForm.hide('slow', function () {
                $('#mainCenterText').show('slow');
                $('#registerFormContent').css({opacity: 1});

            });
        });
    }
}
