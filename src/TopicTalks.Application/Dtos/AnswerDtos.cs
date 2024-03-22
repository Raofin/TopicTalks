namespace TopicTalks.Application.Dtos;

public record AnswerDtos(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    long? UserId,
    DateTime CreatedAt
);