function getUserTimeZone() {
    return Intl.DateTimeFormat().resolvedOptions().timeZone;
}

function setTimeZoneCookie(timeZoneId) {
    document.cookie = "TimeZone=" + timeZoneId + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

document.addEventListener('DOMContentLoaded', function () {
    var timeZoneId = getUserTimeZone();
    var existingTimeZone = getCookie("TimeZone");

    if (!existingTimeZone) {
        setTimeZoneCookie(timeZoneId);
        location.reload();
    }
});