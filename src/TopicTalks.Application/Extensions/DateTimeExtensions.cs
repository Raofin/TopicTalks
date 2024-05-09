namespace TopicTalks.Application.Extensions;

public static class DateTimeExtensions
{
    public static string ToTimeAgo(this DateTime datetime)
    {
        const int second = 1;
        const int minute = 60 * second;
        const int hour = 60 * minute;
        const int day = 24 * hour;
        const int month = 30 * day;

        var ts = new TimeSpan(DateTime.UtcNow.Ticks - datetime.Ticks);
        var delta = Math.Abs(ts.TotalSeconds);
        var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
        var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));

        return delta switch
        {
            < 10 * second => "just now",
            < 1 * minute => ts.Seconds + " seconds ago",
            < 2 * minute => "a minute ago",
            < 45 * minute => ts.Minutes + " minutes ago",
            < 90 * minute => "an hour ago",
            < 24 * hour => ts.Hours + " hours ago",
            < 48 * hour => "yesterday",
            < 30 * day => ts.Days + " days ago",
            < 12 * month => months <= 1 ? "a month ago" : months + " months ago",
            _ => years <= 1 ? "one year ago" : years + " years ago"
        };
    }
    
    public static string Format1(this DateTime dateTime)
    {
        return dateTime.ToString("hh:mm tt");
    }
    
    public static string Format2(this DateTime dateTime)
    {
        return dateTime.ToString("dd-MMM-yy");
    }

    public static string Format3(this DateTime dateTime)
    {
        return dateTime.ToString("MMM dd, yyyy");
    }

    public static string Format4(this DateTime dateTime)
    {
        return dateTime.ToString("MMM dd, yyyy | hh:mm tt");
    }
}
