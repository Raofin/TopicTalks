using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Common;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Extensions;

public static class EntityExtensions
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(
            UserId: user.UserId,
            Username: user.Username,
            Email: user.Email,
            IsVerified: user.IsVerified,
            UserDetails: user.UserDetails.ToDto(),
            Roles: user.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList(),
            CreatedAt: user.CreatedAt,
            ImageFile: user.ImageFile?.ToDto()
        );
    }

    public static UserBasicInfoDto ToBasicInfoDto(this User user)
    {
        return new UserBasicInfoDto(
            UserId: user.UserId,
            Username: user.Username,
            Email: user.Email,
            ProfileImageUrl: user.ImageFile?.DirectLink,
            CreatedAt: user.CreatedAt
        );
    }

    public static QuestionResponseDto ToDto(this Question question)
    {
        return new QuestionResponseDto(
            QuestionId: question.QuestionId,
            Topic: question.Topic,
            Explanation: question.Explanation,
            UserInfo: question.User?.ToBasicInfoDto(),
            CreatedAt: question.CreatedAt,
            UpdatedAt: question.UpdatedAt,
            ImageFile: question.ImageFile?.ToDto()
        );
    }

    public static UserDetailDto? ToDto(this UserDetail? userDetail)
    {
        return userDetail is null ? null
            : new UserDetailDto(
                Name: userDetail.FullName,
                InstituteName: userDetail.InstituteName,
                IdCardNumber: userDetail.IdCardNumber
            );
    }

    public static AnswerResponseDto ToDto(this Answer answer)
    {
        return new AnswerResponseDto(
            AnswerId: answer.AnswerId,
            ParentAnswerId: answer.ParentAnswerId,
            QuestionId: answer.QuestionId,
            Explanation: answer.Explanation,
            CreatedAt: answer.CreatedAt,
            UserInfo: answer.User?.ToBasicInfoDto()
        );
    }

    public static CloudFile ToCloudFile(this GoogleFile googleFile, long? userId = null)
    {
        return new CloudFile {
            CloudFileId = googleFile.CloudFileId,
            Name = googleFile.Name,
            ContentType = googleFile.ContentType,
            Size = googleFile.Size,
            WebContentLink = googleFile.WebContentLink,
            WebViewLink = googleFile.WebViewLink,
            DirectLink = googleFile.DirectLink,
            CreatedAt = googleFile.CreatedAt,
            UserId = userId
        };
    }

    public static CloudFileDto? ToDto(this CloudFile? imageFile)
    {
        return imageFile is null ? null
            : new CloudFileDto(
                CloudFileId: imageFile.CloudFileId,
                Name: imageFile.Name,
                ContentType: imageFile.ContentType,
                Size: imageFile.Size,
                WebContentLink: imageFile.WebContentLink,
                WebViewLink: imageFile.WebViewLink,
                DirectLink: imageFile.DirectLink,
                CreatedAt: imageFile.CreatedAt
            );
    }
}