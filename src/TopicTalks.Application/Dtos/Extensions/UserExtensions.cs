using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Dtos.Extensions;

public static class UserExtensions
{
    public static UserDetailDto? ToDto(this UserDetail? userDetail)
    {
        return userDetail == null
            ? null
            : new UserDetailDto(
                Name: userDetail.Name,
                InstituteName: userDetail.InstituteName,
                IdCardNumber: userDetail.IdCardNumber
            );
    }
}