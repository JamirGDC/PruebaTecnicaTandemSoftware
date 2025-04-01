using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTandemSoftware.Application.Common.Filters;
using PruebaTecnicaTandemSoftware.Application.Common.Result;
using PruebaTecnicaTandemSoftware.Application.Dtos.Request;
using PruebaTecnicaTandemSoftware.Application.Dtos.Response;
using PruebaTecnicaTandemSoftware.Application.Intefaces;

namespace PruebaTecnicaTandemSoftware.Endpoints;

public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProducts(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllProducts)
            .AddEndpointFilter(new ResultHttpCodeFilter())
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetProducById)
            .AddEndpointFilter(new ResultHttpCodeFilter())
            .RequireAuthorization();

        group.MapPost("/", PostProduct)
            .AddEndpointFilter(new ResultHttpCodeFilter())
            .RequireAuthorization();

        group.MapPatch("/", PatchProduct)
            .AddEndpointFilter(new ResultHttpCodeFilter())
            .RequireAuthorization();

        group.MapDelete("/{id:int}", DeleteProductById)
            .AddEndpointFilter(new ResultHttpCodeFilter())
            .RequireAuthorization();

        return group;
    }

    public static async Task<Result<IEnumerable<GetProductsResponseDto>>> GetAllProducts(IProductService productService)
    {
        return await productService.GetAllProductsAsync();
    }

    public static async Task<Result<GetProductByIdResponseDto>> GetProducById([FromRoute] int id, IProductService productService)
    {
        return await productService.GetProductByIdAsync(id);
    }

    public static async Task<Result<PostProductResponseDto>> PostProduct (PostProductRequestDto postProductRequestDto, IProductService productService)
    {
        return await productService.PostProductAsync(postProductRequestDto);
    }

    public static async Task<Result<PatchProductResponseDto>> PatchProduct(int id, PatchProductRequestDto patchProductRequestDto, IProductService productService)
    {
        return await productService.PatchProductAsync(id, patchProductRequestDto);
    }

    public static async Task<Result<string>> DeleteProductById (int id, IProductService productService)
    {
        return await productService.DeleteProductByIdAsync(id);
    }






}


