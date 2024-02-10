﻿using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class Question
{
    public long QuestionId { get; set; }

    public string Topic { get; set; } = null!;

    public string Explanation { get; set; } = null!;

    public long? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual User? User { get; set; }
}
