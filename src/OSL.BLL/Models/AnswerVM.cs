using OSL.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace OSL.BLL.Models;

public class AnswerVM
{
    public long AnswerId { get; set; }

    public long? ParentAnswerId { get; set; }

    [Required]
    public long QuestionId { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(int.MaxValue)]
    public string Explanation { get; set; } = null!;

    public long? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }

    public List<AnswerVM>? Answers { get; set; }
}