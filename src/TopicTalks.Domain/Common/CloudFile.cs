namespace TopicTalks.Domain.Common;

public record CloudFile(
    string Id,
    string Name,
    string ContentType,
    long Size,
    string WebContentLink,
    string WebViewLink,
    string DirectLink,
    DateTime CreatedTime
);

public record CloudFileDownload(
    string Id,
    string Name,
    string ContentType,
    long Size,
    byte[] Bytes,
    DateTime CreatedTime
);