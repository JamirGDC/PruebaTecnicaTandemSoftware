using System.Security.AccessControl;

namespace PruebaTecnicaTandemSoftware.Application.Dtos.Response;

public record GetProductByIdResponseDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}