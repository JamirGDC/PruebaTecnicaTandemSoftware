using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PruebaTecnicaTandemSoftware.Application.Intefaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PruebaTecnicaTandemSoftware.Application.Common.Result;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;
using PruebaTecnicaTandemSoftware.Domain.Entities;
using System.Net;
using PruebaTecnicaTandemSoftware.Application.Utilities.Mappers;
using PruebaTecnicaTandemSoftware.Infraestructure.Repository;

namespace PruebaTecnicaTandemSoftware.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IAuthRepository _authRepository;

    public AuthService(IConfiguration config, IAuthRepository authRepository)
    {
        _config = config;
        _authRepository = authRepository;
    }

    public string GenerateToken(string email)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Claim [] claims = new[] { new Claim(ClaimTypes.Email, email) };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(int.Parse(_config["Jwt:ExpireDays"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    public async Task<Result<AuthRegisterResponseDto>> SignUpUserAsync(AuthRegisterRequestDto authRegisterRequest)
    {
        User userExists = await _authRepository.GetUserByEmailAsync(authRegisterRequest.Email);
        if (userExists != null)
        {
            return Result<AuthRegisterResponseDto>.Failure(HttpStatusCode.BadRequest).WithDescription("Invalid Credentials, try again.");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(authRegisterRequest.Password);

        User userToRegister = authRegisterRequest.ToModel();
        userToRegister.Password = hashedPassword;

        User createdUser = await _authRepository.CreateUserAsync(userToRegister);
        AuthRegisterResponseDto postRegisterResponse = createdUser.ToResponse();
        return Result<AuthRegisterResponseDto>.Success(HttpStatusCode.Created).WithPayload(postRegisterResponse);
    }

    public async Task<Result<AuthLoginResponseDto>> LoginUserAsync(AuthLoginRequestDto authLoginRequest)
    {
        User user = await _authRepository.GetUserByEmailAsync(authLoginRequest.Email);
        if (user == null)
        {
            return Result<AuthLoginResponseDto>.Failure(HttpStatusCode.BadRequest).WithDescription("Invalid Credentials, try again.");
        }


        if (BCrypt.Net.BCrypt.Verify(authLoginRequest.Password, user.Password))
        {
            return Result<AuthLoginResponseDto>.Failure(HttpStatusCode.BadRequest)
                .WithDescription("Invalid Credentials, try again.");
        }
        string token = GenerateToken(user.Email);

        AuthLoginResponseDto loginResponse = AuthMappers.ToResponse(token);

        return Result<AuthLoginResponseDto>.Success(HttpStatusCode.OK).WithPayload(loginResponse);

    }
}