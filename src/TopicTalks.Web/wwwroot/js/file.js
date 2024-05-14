function Upload(imageElement, previewElement = '#preview-image') {
    return new Promise((resolve, reject) => {
        if (validateImage($(imageElement), mbToBytes(2))) {
            previewImage(imageElement, previewElement)
            $('#profile-pic-loading').show()

            let formData = new FormData()
            formData.append('file', $(imageElement)[0].files[0])

            $.ajax({
                url: '/file',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: response => resolve(response),
                error: () => {
                    toastMessage('Error uploading image')
                    $(imageElement).val('')
                    removePreviewImage(previewElement)
                    reject('Error uploading image')
                }
            })
        } else {
            $(imageElement).val('')
            removePreviewImage(previewElement)
            reject('Please select a valid image.')
        }
    })
}

function validateImage(imageElement) {
    let file = imageElement[0].files[0]

    switch (true) {
        case !file:
            toastMessage('Please select an image.')
            break
        case !file.type.startsWith('image/'):
            toastMessage('Please select a valid image.')
            break
        case file.size > mbToBytes(2):
            toastMessage('Max image size allowed is 2 MB.')
            break
        default:
            removeFileError()
            return true
    }

    return false
}

function previewImage(imageElement, previewElement) {
    let file = $(imageElement)[0].files[0]

    if (file) {
        let previewImage = $(previewElement)
        previewImage.attr('src', URL.createObjectURL(file)).show().on('load', () => {
            URL.revokeObjectURL(previewImage.attr('src'))
        })

        $('#image-label').hide()
        $('.image-input').addClass('no-padding')
    }
}

function removePreviewImage(previewElement) {
    $('.image-input').removeClass('no-padding')
    $(previewElement).hide()
    $(previewElement).attr('src', '')
    $('#image-label').show()
}

function appendLabel(selector) {
    $(selector).after(`<label id="message" style="color: #595c5f" for="${$(selector).attr('id')}">Choose an image (Max 2 MB)</label>`)
}

function removeFileError() {
    $('#message').remove()
}

function bytesToMB(bytes) {
    return (bytes / (1024 * 1024)).toFixed(2)
}

function mbToBytes(mb) {
    return mb * 1024 * 1024
}