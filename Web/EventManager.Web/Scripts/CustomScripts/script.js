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

/*Function that focuses next input hour in Create Event*/
$(function () {
    $('.inputHour').keyup(function (e) {
        if ($(this).val().length == $(this).attr('maxlength'))
            $(this).next(':input').focus()
    })
});
/*Invite box dropdown function*/
function InviteBox(button, form) {
    if ($(form).hasClass('inviteUserDisabled')) {
        $(button).addClass('inviteBtnAct');
        $.when($(form).css('display', 'flex')).done(function () {
            $(form).css('opacity');
            $(form).removeClass('inviteUserDisabled').css('opacity', '1');
        });
    } else {
        $(button).removeClass('inviteBtnAct');
        $.when($(form).css('opacity', '0')).done(function () {
            $(form).addClass('inviteUserDisabled');
        });
    }
}
/*List limit function*/
/*$(function () {
    var attendersAmount = $('#attendersList').children().length,
        moreAttenders = attendersAmount - 4,
        overflower = '<div class="attenderName">' + moreAttenders + ' more attenders</div>',
        nthChildSafe = $('.listAttender:nth-child(5) a .attenderName'),
        listed = false;

    if (attendersAmount > 5) {
        attendersAmount--;
        $('.listAttender:lt(5)').show();
        $('.listAttender:nth-child(5)').addClass('toggler');
        $('.listAttender:nth-child(5) a .attenderPic').hide();
        $('.listAttender:nth-child(5) a .attenderName').replaceWith(overflower);
    } else {
        $('.listAttender').show();
    }
    $('.listAttender:nth-child(5) a').on('click', function () {
        if (!listed) {

            $('.listAttender:nth-child(5) a .attenderPic').fadeIn();
            $('.listAttender:nth-child(5) a .attenderName').replaceWith(nthChildSafe);
            $('.listAttender:lt(' + attendersAmount + ')').show('slow');
            $('.listAttender:last-child').show('slow');
            $('.listAttender:nth-child(5)').removeClass('toggler');
            //$(liElement).appendTo('#attendersList');
            listed = true;
        }
    });
    $('#hider').on('click', function () {
        let remove = moreAttenders + 1;
        if (listed) {
            $('.listAttender:last-child').hide('slow');
            $('.listAttender:gt(' + remove + ')').hide('slow');
            $('.listAttender:nth-child(5) a .attenderPic').hide();
            $('.listAttender:nth-child(5) a .attenderName').replaceWith(overflower);
            $('.listAttender:nth-child(5)').addClass('toggler');
            listed = false;
        }
    });
});*/
//SCROLL TO ATTENDERS FUNCTION
function scrollTo(dirId) {
    $('html, body').animate({
        scrollTop: $('#'+dirId).offset().top
    }, 1000);
}

/*ERROR MESSAGE FUNCTION*/
var errorMessages = {
    errmsgClass: 'inputErrMsg',
    inputErrorMessage: function (box, message) {
        let error = '<div class="' + this.errmsgClass + ' Msg">' + message + '</div>',
            $errorSelector = $(box + ':has(.' + this.errmsgClass + ')');
        if ($errorSelector[0] != undefined) {
            $('.' + this.errmsgClass).remove();
            $(box).prepend(error);
        } else {
            $(box).prepend(error);
        }
    },

    removeErrorMessage: function (box) {
        let $errorSelector = $(box + ':has(.' + this.errmsgClass + ')');
        if ($errorSelector[0] != undefined) {
            $('.' + this.errmsgClass).remove();
        }
    }
};

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


/*INFORMATION EDIT TOGGLE FUNCTION*/
function abToggler(id) {
    let $inputBox = $('#' + id);
    if ($inputBox.is(':disabled')) {
        $inputBox.prop('disabled', false);
        if (id != 'aboutUser') {
            $inputBox.focus().select();
        }
    }
}


