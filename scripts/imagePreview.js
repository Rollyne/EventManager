function ImagePreview(picker,dropTarget ,inputId, previewId, titleId, remover) {
    var $dropzone = $(picker),
        $droptarget = $(dropTarget),
        $dropinput = $(inputId),
        $dropimg = $(previewId),
        $remover = $('[data-action=' + remover + ']');
    if(titleId!=""){
        var $imgtitleinput = $(titleId + ' input');
    } else {
        $imgtitleinput = "";
    }


    $dropzone.on('dragover', function () {
        $droptarget.addClass('dropping');
        return false;
    });

    $dropzone.on('dragend dragleave', function () {
        $droptarget.removeClass('dropping');
        return false;
    });

    $dropzone.on('drop', function (e) {
        $droptarget.removeClass('dropping');
        $droptarget.addClass('dropped');
        $remover.removeClass('disabled');
        e.preventDefault();

        var file = e.originalEvent.dataTransfer.files[0],
            reader = new FileReader();

        reader.onload = function (event) {
            $dropimg.css('background-image', 'url(' + event.target.result + ')');
        };

        console.log(file);
        reader.readAsDataURL(file);

        return false;
    });

    $dropinput.change(function (e) {
        $droptarget.addClass('dropped');
        $remover.removeClass('disabled');
        $($imgtitleinput).val('');
        $(titleId).parent().addClass('image_details_active');

        var file = $dropinput.get(0).files[0],
            reader = new FileReader();

        reader.onload = function (event) {
            $dropimg.css('background-image', 'url(' + event.target.result + ')');
        };

        reader.readAsDataURL(file);
    });

    $remover.on('click', function () {
        $dropimg.css('background-image', '');
        console.log($dropimg.css('background-image', ''));
        $droptarget.removeClass('dropped');
        $remover.addClass('disabled');
        $(titleId).parent().removeClass('image_details_active');
        $($imgtitleinput).val('');
    });

    $($imgtitleinput).blur(function () {
        if ($(this).val() != '') {
            $droptarget.removeClass('dropped');
            $(titleId).parent().removeClass('image_details_active');
        }
    });
}



function addImage() {
    for(let i = 1; i<=3; i++){
        let $elId = '#image_picker' + i;
        if($($elId + ':visible').length == 0){
            $($elId).show('slow');
            if(i==3) {
                $('#addImage').hide('slow');
            }
            break;
        }
    }
}