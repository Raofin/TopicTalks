using System.Text.Json.Serialization;

namespace TopicTalks.Contracts.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoleName
{
    Student = 1,
    Teacher = 2,
    Moderator = 3
}