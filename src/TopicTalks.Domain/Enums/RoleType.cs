using System.Text.Json.Serialization;

namespace TopicTalks.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoleType
{
    Student = 1,
    Teacher = 2,
    Moderator = 3
}