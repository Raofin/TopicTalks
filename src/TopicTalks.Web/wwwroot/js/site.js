
function formToJson(formElement) {
    const formDataArray = Array.from(new FormData(formElement));
    const formDataObject = {};

    formDataArray.forEach(([key, value]) => {
        formDataObject[key] = value;
    });

    return JSON.stringify(formDataObject);
}


function formatDate(dateString) {
    const options = { year: 'numeric', month: 'short', day: '2-digit', hour: 'numeric', minute: '2-digit', hour12: true };
    return new Date(dateString).toLocaleDateString('en-US', options);
}

// Function to handle string or array of topics
function formatTopics(topics) {
    if (Array.isArray(topics)) {
        return topics.map(topic => `<p onclick="searchTag('${topic}')">${topic}</p>`).join('');
    } else {
        // Assuming topics is a string, split and trim it
        const topicArray = topics.split(',').map(topic => topic.trim());
        return topicArray.map(topic => `<p onclick="searchTag('${topic}')">${topic}</p>`).join('');
    }
}

const ToastType = {
    Error: "danger",
    Success: "success",
    Warning: "warning",
    Primary: "primary"
}

function toastMessage(message, type = ToastType.Error, timer = 6000) {
    const container = document.getElementById('toast-container') || document.createElement('div');
    container.id = 'toast-container';
    container.style.cssText = 'position: fixed; z-index: 11; bottom: 40px; right: 0; margin-right: 0.5rem;';
    document.body.appendChild(container);

    const toastDiv = document.createElement('div');
    toastDiv.classList.add('toast', 'align-items-center', `text-bg-${type}`, 'border-0', 'mb-2');
    toastDiv.setAttribute('role', 'alert');
    toastDiv.setAttribute('aria-live', 'assertive');
    toastDiv.setAttribute('aria-atomic', 'true');
    toastDiv.setAttribute('data-bs-delay', timer);

    toastDiv.innerHTML = `
        <div class="d-flex">
            <div class="toast-body">
                ${message}
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>`;

    container.appendChild(toastDiv);
    new bootstrap.Toast(toastDiv).show();
}

function toastMessageNext(message, type = ToastType.Error, timer = 6000) {
    var toastData = {
        message: message,
        type: type,
        timer: timer
    };

    sessionStorage.setItem('toastData', JSON.stringify(toastData));
}

document.addEventListener('DOMContentLoaded', function () {
    var toastDataString = sessionStorage.getItem('toastData');

    if (toastDataString) {
        var toastData = JSON.parse(toastDataString);
        toastMessage(toastData.message, toastData.type);

        sessionStorage.removeItem('toastData');
    }
});

function getFormattedDate() {
    const timeZone = 'Asia/Dhaka';
    const options = {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        timeZone: timeZone
    };

    return new Date().toLocaleDateString('en-US', options);
}

function logout() {
    $.get(`/logout`)
        .done(() => {
            toastMessageNext("You are logged out!", ToastType.Error)
            window.location.href = '/login';
        });
}

// Override the default email validation method
var emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

$.validator.addMethod("email", function (value, element) {
    return this.optional(element) || emailRegex.test(value);
}, "Please enter a valid email address");


// Load RawfinIcons font
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