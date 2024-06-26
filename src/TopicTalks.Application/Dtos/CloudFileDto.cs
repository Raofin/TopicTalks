﻿namespace TopicTalks.Application.Dtos;

public record CloudFileDto(
    string CloudFileId,
    string Name,
    string ContentType,
    long Size,
    string WebContentLink,
    string WebViewLink,
    string DirectLink,
    DateTime CreatedAt
);

public record FileUploadDto(
    string FileName, 
    Stream Stream, 
    string ContentType
);