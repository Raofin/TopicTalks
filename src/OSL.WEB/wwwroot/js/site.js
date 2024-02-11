
function formatDate(dateString) {
    const options = { year: 'numeric', month: 'short', day: '2-digit', hour: 'numeric', minute: '2-digit', hour12: true };
    return new Date(dateString).toLocaleDateString('en-US', options);
}

// Function to handle string or array of topics
function formatTopics(topics) {
    if (Array.isArray(topics)) {
        return topics.map(topic => `<p>${topic}</p>`).join('');
    } else {
        // Assuming topics is a string, split and trim it
        const topicArray = topics.split(',').map(topic => topic.trim());
        return topicArray.map(topic => `<p>${topic}</p>`).join('');
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