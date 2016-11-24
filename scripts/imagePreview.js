function ImagePreview(picker, dropTarget, inputId, previewId, titleId, remover) {
    var $dropzone = $(picker),
        $droptarget = $(dropTarget),
        $dropinput = $(inputId),
        $dropimg = $(previewId),
        $remover = $('[data-action=' + remover + ']'),
        validFileTypes = ['jpg', 'jpeg', 'png', 'gif'];
    if (titleId != "") {
        var $imgtitleinput = $(titleId + ' input');
    } else {
        $imgtitleinput = "";
    }
    function validImageFile(file) {
        var valid = false;
        try {
            var type = file.type;
        }
        catch(err) {
            return valid=false;

        }
        for (i = 0; i < validFileTypes.length; i++) {
            if (type == 'image/' + validFileTypes[i]) {
                valid = true;
                break;
            }
        }
        return valid;
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
        if (!validImageFile(file)) {
            try {
                var errorMsg = '"' + file.name + '"' + ' is not a valid image file';
            }
            catch(err) {
                errorMsg = '';
            }
            $dropimg.css('background-image', '');
            $droptarget.removeClass('dropped');
            $remover.addClass('disabled');
            $(titleId).parent().removeClass('image_details_active');
            $($imgtitleinput).val('');
            if (isThereError) {
                $(".inputErrMsg").remove();
            }
            if(errorMsg != ''){
                errorMessages.inputErrorMessage(picker,errorMsg);
            }
            isThereError = true;
        } else {
            if (isThereError) {
                $(".inputErrMsg").hide().remove();
            }
            reader.onload =  function (event) {
                $dropimg.css('background-image', 'url(' + event.target.result + ')');
            };

            reader.readAsDataURL(file);
        }
        return false;
    });
    var isThereError = false;
    $dropinput.change(function (e) {
        $droptarget.addClass('dropped');
        $remover.removeClass('disabled');
        $($imgtitleinput).val('');
        $(titleId).parent().addClass('image_details_active');


        //if (Number($dropinput.get(0).files[0].size / 1024 / 1024).toFixed(2) > 5) {
        //    alert("File too large!");
        //    var control = $("#" + $(this).attr('id'));//get the id
        //    console.log(control.val());
        //    control.replaceWith(control.clone().val(''));//replace with clone
        //    $dropimg.css('background-image', '');
        //    console.log($dropimg.css('background-image', ''));
        //    $droptarget.removeClass('dropped');
        //    $remover.addClass('disabled');
        //    $(titleId).parent().removeClass('image_details_active');
        //    $($imgtitleinput).val('');
        //    console.log("i think i did it");
        //} else {


        var file = $dropinput.get(0).files[0],
            reader = new FileReader();

        if (!validImageFile(file)) {
            try {
            var errorMsg = '"' + file.name + '"' + ' is not a valid image file';
            }
            catch(err) {
                errorMsg = '';
            }
            $dropimg.css('background-image', '');
            $droptarget.removeClass('dropped');
            $remover.addClass('disabled');
            $(titleId).parent().removeClass('image_details_active');
            $($imgtitleinput).val('');
            if (isThereError) {
                $(".inputErrMsg").remove();
            }
            if(errorMsg != ''){
                errorMessages.inputErrorMessage(picker,errorMsg);
            }
            isThereError = true;
        } else {
            if (isThereError) {
                $(".inputErrMsg").hide().remove();
            }
            reader.onload =  function (event) {
                $dropimg.css('background-image', 'url(' + event.target.result + ')');
            };
            reader.readAsDataURL(file);
        }
        //}
    });

    $remover.on('click', function () {
        if (!$remover.hasClass('disabled')) {
            $dropimg.css('background-image', '');
            $droptarget.removeClass('dropped');
            $remover.addClass('disabled');
            $(titleId).parent().removeClass('image_details_active');
            $($imgtitleinput).val('');
        }
    });


    $($imgtitleinput).blur(function () {
        if ($(this).val() != '') {
            $droptarget.removeClass('dropped');
            $(titleId).parent().removeClass('image_details_active');
        }
    });
}


function addImage() {
    for (let i = 1; i <= 3; i++) {
        let $elId = '#image_picker' + i;
        if ($($elId + ':visible').length == 0) {
            $($elId).show('slow');
            if (i == 3) {
                $('#addImage').hide('slow');
            }
            break;
        }
    }
}