namespace TopicTalks.Application.Dtos;

public record QuestionDtos(
    long QuestionId,
    string Topic,
    string Explanation,
    long? UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
