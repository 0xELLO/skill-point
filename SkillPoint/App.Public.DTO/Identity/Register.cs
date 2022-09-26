﻿using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.Identity;

public class Register
{
    [StringLength(maximumLength:128, MinimumLength = 5, ErrorMessage = "Wrong length on email")]
    public string Email { get; set; } = default!;
    [StringLength(maximumLength:128, MinimumLength = 1, ErrorMessage = "Wrong length on password")]
    public string Password { get; set; } = default!;
}