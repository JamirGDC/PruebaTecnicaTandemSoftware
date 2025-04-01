using System.Net;
using PruebaTecnicaTandemSoftware.Application.Common.Result;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;
using PruebaTecnicaTandemSoftware.Application.Intefaces;
using PruebaTecnicaTandemSoftware.Application.Utilities.Mappers;
using PruebaTecnicaTandemSoftware.Domain.Entities;
using PruebaTecnicaTandemSoftware.Infraestructure.Repository;

namespace PruebaTecnicaTandemSoftware.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<GetProductsResponseDto>>> GetAllProductsAsync()
    {
        IEnumerable<Product> products = await _productRepository.GetAllProductsAsync();
        IEnumerable<GetProductsResponseDto> getProductsResponse = products.ToResponse();
        return Result<IEnumerable<GetProductsResponseDto>>.Success(HttpStatusCode.OK).WithPayload(getProductsResponse);
    }

    public async Task<Result<GetProductByIdResponseDto>> GetProductByIdAsync(int id )
    {
        Product product = await _productRepository.GetByIdAsync(id);
        if(product == null) return Result<GetProductByIdResponseDto>.Failure(HttpStatusCode.NotFound).WithDescription("Product not found");
        GetProductByIdResponseDto getProductResponse = product.ToResponse();
        return Result<GetProductByIdResponseDto>.Success(HttpStatusCode.OK).WithPayload(getProductResponse);
    }


    public async Task<Result<PostProductResponseDto>> PostProductAsync(PostProductRequestDto postProductRequestDto)
    {
        Product product = await _productRepository.CreateProductAsync(postProductRequestDto.ToModel());
        PostProductResponseDto postProductResponse = product.ToPostResponse();
        return Result<PostProductResponseDto>.Success(HttpStatusCode.Created).WithPayload(postProductResponse);
    }

    //JGDC: Se hizo un patch porque considero que se puede querer actualizar el nombre o el precio y no todo el producto
    public async Task<Result<PatchProductResponseDto>> PatchProductAsync(int id, PatchProductRequestDto patchProductRequest)
    {
        Product product = await _productRepository.GetByIdAsync(id);
        if (product == null) return Result<PatchProductResponseDto>.Failure(HttpStatusCode.NotFound).WithDescription("Product to update not found");

        product.UpdateFromDto(patchProductRequest);

        Product productUpdated = await _productRepository.UpdateProductAsync(product);

        PatchProductResponseDto patchProducResponse = productUpdated.ToPatchResponse();

        return Result<PatchProductResponseDto>.Success(HttpStatusCode.OK).WithPayload(patchProducResponse);
    }


    public async Task<Result<string>> DeleteProductByIdAsync(int id)
    {
        Product product = await _productRepository.GetByIdAsync(id);
        if (product == null) return Result<string>.Failure(HttpStatusCode.NotFound).WithDescription("Product to delete not found");

        await _productRepository.DeleteProductAsync(id);

        return Result<string>.Success(HttpStatusCode.OK).WithDescription($"Product with Id: {id} correctly deleted");
    }



}