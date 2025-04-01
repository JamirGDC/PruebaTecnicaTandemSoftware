﻿using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaTandemSoftware.Application.Dtos.Request;

public class AuthLoginRequestDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}