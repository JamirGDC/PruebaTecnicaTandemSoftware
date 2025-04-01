using PruebaTecnicaTandemSoftware.Application.Common.Result;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;

namespace PruebaTecnicaTandemSoftware.Application.Intefaces;

public interface IProductService
{
    Task<Result<IEnumerable<GetProductsResponseDto>>> GetAllProductsAsync();
    Task<Result<GetProductByIdResponseDto>> GetProductByIdAsync(int id);
    Task<Result<PostProductResponseDto>> PostProductAsync(PostProductRequestDto postProductRequestDto);
    Task<Result<PatchProductResponseDto>> PatchProductAsync(int id, PatchProductRequestDto patchProductRequest);
    Task<Result<string>> DeleteProductByIdAsync(int id);
}