using Microsoft.AspNetCore.Razor.TagHelpers;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.TagHelpers;

[HtmlTargetElement("local-time", Attributes = "utc-datetime, format")]
public class LocalTimeTagHelper(ITimeZoneService timeZoneService) : TagHelper
{
    private readonly ITimeZoneService _timeZoneService = timeZoneService;

    [HtmlAttributeName("utc-datetime")]
    public DateTime UtcDateTime { get; set; }

    [HtmlAttributeName("format")]
    public string? Format { get; set; }

    [HtmlAttributeName("time-ago")]
    public bool TimeAgo { get; set; } = false;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var localDateTime = _timeZoneService.ConvertUtcToUserLocalTime(UtcDateTime);

        var datetimeFormatted = "";

        if (Format != "")
        {
            datetimeFormatted = Format switch
            {
                "1" => localDateTime.ToString("dd-MMM-yy "),
                "2" => localDateTime.ToString("MMM dd, yyyy "),
                "3" => localDateTime.ToString("MMM dd, yyyy | hh:mm tt "),
                _ => localDateTime.ToString(Format)
            };
        }

        if (TimeAgo)
        {
            datetimeFormatted = $"{datetimeFormatted}({UtcDateTime.TimeAgo()})";
        }

        output.TagName = "span";
        output.Content.SetContent(datetimeFormatted);
    }
}
