using PruebaTecnicaTandemSoftware.Application.Common.Result;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;

namespace PruebaTecnicaTandemSoftware.Application.Intefaces;

public interface IAuthService
{
    string GenerateToken(string username);
    Task<Result<AuthRegisterResponseDto>> SignUpUserAsync(AuthRegisterRequestDto authRegisterRequest);
    Task<Result<AuthLoginResponseDto>> LoginUserAsync(AuthLoginRequestDto authLoginRequest);
}