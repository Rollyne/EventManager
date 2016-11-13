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

var inputIds = [
    ["#image_picker0","#drop_target0","#inputFile0", "#image_preview0", "#image_title0","remove_current_image0"],
    ["#image_picker","#drop_target","#inputFile", "#image_preview", "#image_title","remove_current_image"],
    ["#image_picker1","#drop_target1","#inputFile1", "#image_preview1", "#image_title1","remove_current_image1"],
    ["#image_picker2","#drop_target2","#inputFile2", "#image_preview2", "#image_title2","remove_current_image2"],
    ["#image_picker3","#drop_target3","#inputFile3", "#image_preview3", "#image_title3","remove_current_image3"]
];

for (var i = 0; i < inputIds.length; i++) {
    ImagePreview(inputIds[i][0], inputIds[i][1],inputIds[i][2],inputIds[i][3],inputIds[i][4],inputIds[i][5]);
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