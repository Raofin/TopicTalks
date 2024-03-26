﻿using TopicTalks.Contracts.Common;

namespace TopicTalks.Application.Dtos;

public record LoginRequest(
    string Email,
    string Password,
    RoleName Role
);

public record LoginResponse(
    long UserId,
    string Token,
    string Email,
    UserDetailDto? UserDetails,
    List<RoleName?> Role
);