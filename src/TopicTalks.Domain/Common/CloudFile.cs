namespace TopicTalks.Domain.Common;

public record CloudFile(
    string CloudFileId,
    string Name,
    string ContentType,
    long Size,
    string WebContentLink,
    string WebViewLink,
    string DirectLink,
    DateTime CreatedAt
);

public record CloudFileDownload(
    string CloudFileId,
    string Name,
    string ContentType,
    long Size,
    byte[] Bytes,
    DateTime CreatedAt
);