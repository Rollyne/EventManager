/**
 * Created by princ on 18.9.2016 г..
 */

//Changing Background Images( too heavy )
/*    $(function ($) {
 var images = [
 '../images/background0.jpg',
 '../images/background1.jpg',
 '../images/background2.jpg',
 '../images/background3.jpg'
 ],
 amountOfImages = images.length,
 tempR = 0,
 $bg = $('#bg');

 for (let i = 0; i < amountOfImages; i++) {
 var img = new Image(),src = images[i];
 }

 function fader(){
 let r = Math.floor(Math.random()*amountOfImages);
 $('#bg').fadeOut(500, function(){
 $bg.attr('src', images[r]); //TODO: Animate the transition
 $('#bg').fadeIn(500);
 tempR = r;
 })
 }
 fader();

 setInterval(fader, 5000);
 });*/

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
//TODO: FIX: While the sidebar is full page and you resize the page the main content breaks
//TODO: Make animation on the main show/hide

function Dropdowner(idOfWrapper, idOfDropdowner, idOfContent) {
    let $userDrop = $('#' + idOfDropdowner),
        $userWrapper = $('#' + idOfWrapper),
        $dropdownMenu = $('#' + idOfContent),
        windowSizeBig = theWindow.width() + 17 >= 600;
    console.log(windowSizeBig, theWindow.width());

    if ($('.dropdownToggle').length == 0) {
        if (windowSizeBig) {
            $('main').css({"margin-left": "auto"});
        } else {
            $('main').fadeIn('fast');
        }
        $dropdownMenu.animate({opacity: 0}, 300, function () {
            $userDrop.hide('slow');
        });
        $userWrapper.attr('class', 'dropdownToggle');

    } else {
        if (windowSizeBig) {
            $('main').css({"margin-left": "300px"});
        }
        $userDrop.show(function () {
            $dropdownMenu.animate({opacity: 1}, 200);
            $dropdownMenu.show('slow');
        });
        $userWrapper.removeAttr('class', 'dropdownToggle');
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


//Calendar Settings
jQuery(document).ready(function () {

    var dateDetails = (function () {
            let json = null;
            $.ajax({
                'async': false,
                'global': false,
                'url': "../json/Dates.json",
                'dataType': "json",
                'success': function (data) {
                    json = data;
                }
            });
            return json;
        }()),
        allAttenders = dateDetails.MaxPeople,
        $cal = $('#calendar');

    function datesFromJSONArray(json) {
        let array = [];
        $.each(json.Dates, function () {
            array.push(new Date(this.Year, this.Month - 1, this.Day).getTime());
        });
        return array;
    }

    var datesArray = datesFromJSONArray(dateDetails);
    $cal.datepicker(
        {
            inline: true,
            firstDay: 1,
            showOtherMonths: true,
            dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            beforeShowDay: function (date) {
                let highlight = $.inArray(date.getTime(), datesArray) >= 0,
                    elIndex = $.inArray(date.getTime(), datesArray);
                if(elIndex >= 0){
                percentAttenders = parseInt(dateDetails.Dates[elIndex].FreePeopleCount / allAttenders * 10);
                }
                if (highlight) {
                    switch (percentAttenders) {
                        case 1 :
                            return [true, "calendarHighlight1", ''];
                            break;
                        case 2 :
                            return [true, "calendarHighlight2", ''];
                            break;
                        case 3 :
                            return [true, "calendarHighlight3", ''];
                            break;
                        case 4 :
                            return [true, "calendarHighlight4", ''];
                            break;
                        case 5 :
                            return [true, "calendarHighlight5", ''];
                            break;
                        case 6 :
                            return [true, "calendarHighlight6", ''];
                            break;
                        case 7 :
                            return [true, "calendarHighlight7", ''];
                            break;
                        case 8 :
                            return [true, "calendarHighlight8", ''];
                            break;
                        case 9 :
                            return [true, "calendarHighlight9", ''];
                            break;
                        case 10 :
                            return [true, "calendarHighlight10", ''];
                            break;
                        default :
                            return [true, 'calendarHighlight0', ''];
                            break;
                    }
                } else {
                    return [true, '', ''];
                }
            }
        });
});
