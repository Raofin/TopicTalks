﻿function formatTopics(topics) {
    if (Array.isArray(topics)) {
        return topics.map(topic => `<p onclick="searchTag('${topic}')">${topic}</p>`).join('')
    } else {
        const topicArray = topics.split(',').map(topic => topic.trim())
        return topicArray.map(topic => `<p onclick="searchTag('${topic}')">${topic}</p>`).join('')
    }
}

function currentDate() {
    let timeZone = timeZoneCookie()

    const options = {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        timeZone: timeZone ? timeZone : 'UTC'
    }

    return new Date().toLocaleDateString('en-US', options)
}

function logout() {
    $.get(`/logout`)
        .done(() => {
            toastMessageNext('You are logged out!')
            window.location.href = '/login'
        })
}

tippy('.user-preview', {
    content: (ref) => $(ref).children('.user-preview').html(),
    theme: 'profile',
    allowHTML: true
})

tippy('.profile-preview', {
    content: (ref) => $(ref).children('.profile-preview').html(),
    theme: 'profile',
    allowHTML: true,
    trigger: 'click',
    interactive: true,
    onShown: () => $('.fi').css('display', 'inline'),
    popperOptions: { strategy: 'fixed' },
    appendTo: document.body
})