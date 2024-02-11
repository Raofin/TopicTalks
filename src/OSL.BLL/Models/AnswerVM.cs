using OSL.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace OSL.BLL.Models;

public class AnswerVM
{
    public long AnswerId { get; set; }

    public long? ParentAnswerId { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(4)]
    public long QuestionId { get; set; }

    [Required]
    [MinLength(30)]
    [MaxLength(int.MaxValue)]
    public string Explanation { get; set; } = null!;

    [Required]
    public long? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }

    public List<AnswerVM>? Answers { get; set; }
}