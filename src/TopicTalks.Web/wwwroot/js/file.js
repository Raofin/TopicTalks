function Upload(imageElement) {
    if (validateImage($(imageElement), mbToBytes(2))) {
        previewImage(imageElement)

        var formData = new FormData();
        formData.append('file', $(imageElement)[0].files[0]);

        $.ajax({
            url: '/file',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: (response) => {
                console.table(response);
                return response;
                toastMessage('Image uploaded successfully.', ToastType.Success);
            },
            error: () => {
                toastMessage('Error uploading image', ToastType.Error);
            }
        });
    } else {
        $(imageElement).val('');
        removePreviewImage();
        toastMessage('Please select a valid image.', ToastType.Error);
    }
}

function validateImage(imageElement, maxSize = 2097152) {
    var file = imageElement[0].files[0];

    if (!file) {
        appendFileError(imageElement, 'Please select an image file.');
    } else if (!file.type.startsWith('image/')) {
        appendFileError(imageElement, 'Please select a valid image.');
    } else if (file.size > maxSize) {
        appendFileError(imageElement, `Max size allowed is ${bytesToMB(maxSize)} MB.`);
    } else {
        removeFileError();
        return true;
    }

    return false;
}

function previewImage(imageElement) {
    var file = $(imageElement)[0].files[0];

    if (file) {
        var previewImage = $('#preview-image');
        previewImage.attr('src', URL.createObjectURL(file)).show().on('load', function () {
            URL.revokeObjectURL(previewImage.attr('src'));
        });
    }
}

function removePreviewImage() {
    $('#preview-image').hide();
    $('#preview-image').attr('src', '');
}

function appendFileError(selector, message) {
    $(selector).after(`<label id="file-error" class="error" for="${$(selector).attr('id')}">${message}</label>`);
}

function removeFileError() {
    $('#file-error').remove();
}

function bytesToMB(bytes) {
    return (bytes / (1024 * 1024)).toFixed(2);
}

function mbToBytes(mb) {
    return mb * 1024 * 1024;
}