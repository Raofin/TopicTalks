
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

function timeAgo(datetime) {
    const SECOND = 1;
    const MINUTE = 60 * SECOND;
    const HOUR = 60 * MINUTE;
    const DAY = 24 * HOUR;
    const MONTH = 30 * DAY;

    const now = new Date();
    const timestamp = new Date(datetime);
    const delta = Math.abs(now - timestamp) / 1000;

    if (delta < 1 * MINUTE)
        return timestamp.getSeconds() === 1 ? 'one second ago' : timestamp.getSeconds() + ' seconds ago';

    if (delta < 2 * MINUTE)
        return 'a minute ago';

    if (delta < 45 * MINUTE)
        return timestamp.getMinutes() + ' minutes ago';

    if (delta < 90 * MINUTE)
        return 'an hour ago';

    if (delta < 24 * HOUR)
        return timestamp.getHours() + ' hours ago';

    if (delta < 48 * HOUR)
        return 'yesterday';

    if (delta < 30 * DAY)
        return timestamp.getDate() + ' days ago';

    if (delta < 12 * MONTH) {
        const months = Math.floor(delta / 30);
        return months <= 1 ? 'one month ago' : months + ' months ago';
    } else {
        const years = Math.floor(delta / 365);
        return years <= 1 ? 'one year ago' : years + ' years ago';
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


