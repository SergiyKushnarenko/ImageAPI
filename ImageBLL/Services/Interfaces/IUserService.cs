﻿using System.Data;

namespace ImageBLL.Services.Interfaces;

public interface IUserService
{
    int UserId { get; }
    string Email { get; }
}