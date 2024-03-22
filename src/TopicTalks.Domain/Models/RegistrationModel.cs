﻿using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Models;

public class RegistrationModel
{
    public User User { get; set; } = null!;
    public UserDetail UserDetail { get; set; } = null!;
}
