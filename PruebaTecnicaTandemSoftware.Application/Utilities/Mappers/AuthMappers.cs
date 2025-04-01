using Azure;
using Microsoft.AspNetCore.Authentication.BearerToken;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;
using PruebaTecnicaTandemSoftware.Domain.Entities;

namespace PruebaTecnicaTandemSoftware.Application.Utilities.Mappers;

public static class AuthMappers
{

    public static AuthRegisterResponseDto ToResponse(this User user)
    {
        return new AuthRegisterResponseDto
        {
           Id = user.Id
        };
    }

    public static User ToModel(this AuthLoginRequestDto loginRequest)
    {
        return new User
        {
            Email = loginRequest.Email,
            Password = loginRequest.Password,
        };
    }

    public static User ToModel(this AuthRegisterRequestDto registerRequest)
    {
        return new User
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password,
        };
    }

    public static AuthLoginResponseDto ToResponse(string token)
    {
        return new AuthLoginResponseDto
        {
            Token = token,
        };
    }

}

