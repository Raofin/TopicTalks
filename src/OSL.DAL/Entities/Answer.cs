﻿using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class Answer
{
    public long AnswerId { get; set; }

    public long? ParentAnswerId { get; set; }

    public long? QuestionId { get; set; }

    public string Explanation { get; set; } = null!;

    public long? UserId { get; set; }

    public virtual ICollection<Answer> InverseParentAnswer { get; set; } = new List<Answer>();

    public virtual Answer? ParentAnswer { get; set; }

    public virtual Question? Question { get; set; }

    public virtual User? User { get; set; }
}
