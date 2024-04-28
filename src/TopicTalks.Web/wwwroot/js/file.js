function Upload(imageElement) {
    return new Promise((resolve, reject) => {
        if (validateImage($(imageElement), mbToBytes(2))) {
            $('#profile-pic-loading').show();

            previewImage(imageElement);

            var formData = new FormData();
            formData.append('file', $(imageElement)[0].files[0]);

            $.ajax({
                url: '/file',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: (response) => {
                    resolve(response);
                },
                error: () => {
                    toastMessage('Error uploading image');
                    reject('Error uploading image');
                }
            });
        } else {
            $(imageElement).val('');
            removePreviewImage();
            reject('Please select a valid image.');
        }
    });
}

function validateImage(imageElement) {
    var file = imageElement[0].files[0];

    switch (true) {
        case !file:
            toastMessage('Please select an image.');
            break;
        case !file.type.startsWith('image/'):
            toastMessage('Please select a valid image.');
            break;
        case file.size > mbToBytes(2):
            toastMessage('Max image size allowed is 2 MB.');
            break;
        default:
            removeFileError();
            return true;
    }

    return false;
}

function previewImage(imageElement) {
    var file = $(imageElement)[0].files[0];

    if (file) {
        var previewImage = $('#preview-image');
        previewImage.attr('src', URL.createObjectURL(file)).show().on('load', () => {
            URL.revokeObjectURL(previewImage.attr('src'));
        });
        $('#image-label').hide()
        $('.image-input').addClass('no-padding');
    }
}

function removePreviewImage() {
    $('#image-label').show()
    $('.image-input').removeClass('no-padding');
    $('#preview-image').hide();
    $('#preview-image').attr('src', '');
}

function appendLabel(selector) {
    $(selector).after(`<label id="message" style="color: #595c5f" for="${$(selector).attr('id')}">Choose an image (Max 2 MB)</label>`);
}

function removeFileError() {
    $('#message').remove();
}

function bytesToMB(bytes) {
    return (bytes / (1024 * 1024)).toFixed(2);
}

function mbToBytes(mb) {
    return mb * 1024 * 1024;
}