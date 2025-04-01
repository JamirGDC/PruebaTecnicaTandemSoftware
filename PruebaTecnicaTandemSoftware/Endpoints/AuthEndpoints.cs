using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTandemSoftware.Application.Common.Filters;
using PruebaTecnicaTandemSoftware.Application.Common.Result;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;
using PruebaTecnicaTandemSoftware.Application.Intefaces;

namespace PruebaTecnicaTandemSoftware.Endpoints;

public static class AuthEndpoints
{

    public static RouteGroupBuilder MapAuth(this RouteGroupBuilder group)
    {
        group.MapPost("/login", Login).AddEndpointFilter(new ResultHttpCodeFilter());
        group.MapPost("/register", Signup).AddEndpointFilter(new ResultHttpCodeFilter());
        
        return group;
    }
 
    public static async Task<Result<AuthLoginResponseDto>> Login (AuthLoginRequestDto loginRequest, IAuthService authService)
    {
        return await authService.LoginUserAsync(loginRequest);
    }

    public static async Task<Result<AuthRegisterResponseDto>> Signup(AuthRegisterRequestDto registerRequest, IAuthService authService)
    {
        return await authService.SignUpUserAsync(registerRequest);
    }






}


