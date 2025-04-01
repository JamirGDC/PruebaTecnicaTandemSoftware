using Azure;
using Microsoft.AspNetCore.Authentication.BearerToken;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;
using PruebaTecnicaTandemSoftware.Domain.Entities;

namespace PruebaTecnicaTandemSoftware.Application.Utilities.Mappers;

public static class ProductMappers
{
    public static IEnumerable<GetProductsResponseDto> ToResponse(this IEnumerable<Product> products)
    {
        return products.Select(product => new GetProductsResponseDto()
        {
            Name = product.Name,
            Price = product.Price
        }).ToList();
    }

    public static GetProductByIdResponseDto ToResponse(this Product product)
    {
        return new GetProductByIdResponseDto
        {
            Name = product.Name,
            Price = product.Price
        };
    }

    public static Product ToModel(this PostProductRequestDto productRequest)
    {
        return new Product
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
        };
    }

    public static PostProductResponseDto ToPostResponse(this Product product)
    {
        return new PostProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
        };
    }

    public static void  UpdateFromDto (this Product product, PatchProductRequestDto patchProductRequest)
    {
        product.Name = patchProductRequest.Name ?? product.Name;
        product.Price = patchProductRequest.Price ?? product.Price;
    }

    public static PatchProductResponseDto ToPatchResponse(this Product product)
    {
        return new PatchProductResponseDto
        {
            Name = product.Name,
            Price = product.Price,
        };
    }
}

