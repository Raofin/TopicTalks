using System.ComponentModel.DataAnnotations;

namespace OSL.BLL.Models;

public class QuestionVM
{
    public long QuestionId { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(4)]
    public required string Topic { get; set; }

    [Required]
    [MaxLength(500)]
    [MinLength(50)]
    public required string Description { get; set; }

    [Required]
    [MinLength(100)]
    [MaxLength(int.MaxValue)]
    public required string Explanation { get; set; }

    [Required]
    public long UserId { get; set; }
}
