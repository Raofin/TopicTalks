namespace TopicTalks.Web.ViewModels;

public record UploadedFile(
    string CloudFileId,
    string Name,
    string ContentType,
    long Size,
    string WebContentLink,
    string WebViewLink,
    string DirectLink,
    DateTime CreatedAt,
    long? UserId
);