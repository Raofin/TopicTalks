// Bootstrap toast message
const ToastColor = {
    Red: "danger",
    Green: "success",
    Yellow: "warning",
    Blue: "primary"
}

function toastMessage(message, type = ToastColor.Red, timer = 6000) {
    var container = $('#toast-container');
    if (container.length === 0) {
        container = $('<div>').attr('id', 'toast-container').css({
            'position': 'fixed',
            'z-index': 11,
            'bottom': '40px',
            'right': '0',
            'margin-right': '0.5rem'
        }).appendTo('body');
    }

    var toastDiv = $('<div>').addClass('toast align-items-center text-bg-' + type + ' border-0 mb-2')
        .attr({
            'role': 'alert',
            'aria-live': 'assertive',
            'aria-atomic': 'true',
            'data-bs-delay': timer
        })
        .html(`
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        `);

    container.append(toastDiv);
    new bootstrap.Toast(toastDiv[0]).show();
}

function toastMessageNext(message, type = ToastColor.Red, timer = 6000) {
    var toastMessage = {
        message: message,
        type: type,
        timer: timer
    };

    sessionStorage.setItem('toastMessage', JSON.stringify(toastMessage));
}

$(() => {
    var message = sessionStorage.getItem('toastMessage');

    if (message) {
        var toastData = JSON.parse(message);
        toastMessage(toastData.message, toastData.type);

        sessionStorage.removeItem('toastMessage');
    }
});


// Override the default email validation method in jQuery Validate
var emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

$.validator.addMethod("email", function (value, element) {
    return this.optional(element) || emailRegex.test(value);
}, "Please enter a valid email address");


// Load icon fonts with FontFace Observer
new FontFaceObserver('RawfinIcons')
    .load()
    .then(() => $('.fi').css('display', 'inline'))


// Initialize tippy.js
tippy.setDefaultProps({ delayanimation: 'perspective-subtle' });

function setTippyContent() {
    tippy('[pop]', {
        content: (reference) => reference.getAttribute('pop')
    });
}

setTippyContent()