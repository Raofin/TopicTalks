namespace TopicTalks.Domain.Common;

public record GoogleFile(
    string CloudFileId,
    string Name,
    string ContentType,
    long Size,
    string WebContentLink,
    string WebViewLink,
    string DirectLink,
    DateTime CreatedAt
);

public record GoogleFileDownload(
    string CloudFileId,
    string Name,
    string ContentType,
    long Size,
    byte[] Bytes,
    DateTime CreatedAt
);