using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Extensions;

public static class EntityExtensions
{
    public static QuestionResponseDto ToDto(this Question question)
    {
        return new QuestionResponseDto(
            QuestionId: question.QuestionId,
            Topic: question.Topic,
            Explanation: question.Explanation,
            UserInfo: question.User is null ? null : new UserBasicInfo(
                UserId: question.User.UserId,
                Email: question.User.Email
            ),
            CreatedAt: question.CreatedAt,
            UpdatedAt: question.UpdatedAt
        );
    }
}