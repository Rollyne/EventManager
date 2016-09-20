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

//Background Shrink Properties
$(window).on("load" , function () {
    let theWindow = $(window),
        $bg = $("#bg"),
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
function Dropdowner() {
    let $userDrop = $('#userDropdown'),
        $userWrapper = $('#UserDetailsWrapper'),
        $dropdownMenu = $('#dropdownMenu');

    if($('.dropdownToggle').length == 0) {
        $userDrop.show('slow', function () {
            $dropdownMenu.animate({opacity:1}, 300);
            $dropdownMenu.show('slow');
        });
        $userWrapper.attr('class', 'buttonHeader selectedHeaderButton');
        $userDrop.attr('class' , 'dropdownToggle' );

    } else {
        $dropdownMenu.animate({opacity: 0}, 300, function() {
            $userDrop.hide('slow');
        });
        $userDrop.removeAttr('class', 'dropdownToggle');
        $userWrapper.removeAttr('class', 'selectedHeaderButton');
        $userWrapper.attr('class', 'buttonHeader');
    }
}
