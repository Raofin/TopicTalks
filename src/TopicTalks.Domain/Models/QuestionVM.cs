using System.ComponentModel.DataAnnotations;

namespace TopicTalks.Domain.Models;

public class QuestionVM
{
    public long QuestionId { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(4)]
    public required string Topic { get; set; }

    [Required]
    [MinLength(30)]
    [MaxLength(int.MaxValue)]
    public required string Explanation { get; set; }

    public long? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
