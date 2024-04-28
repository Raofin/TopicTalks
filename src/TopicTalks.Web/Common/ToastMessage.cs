using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TopicTalks.Web.Common;

public record Toast(
    string Message,
    string Type = ToastColor.Red,
    int Timer = 6000
);

public static class ToastColor
{
    public const string Red = "danger";
    public const string Green = "success";
    public const string Yellow = "warning";
    public const string Blue = "primary";
}

public static class ViewDataExtension
{
    public static void SetToastMessage(
        this ViewDataDictionary viewData,
        string message,
        string type = ToastColor.Red,
        int timer = 6000)
    {
        viewData[nameof(Toast)] = new Toast(message, type, timer);
    }
}