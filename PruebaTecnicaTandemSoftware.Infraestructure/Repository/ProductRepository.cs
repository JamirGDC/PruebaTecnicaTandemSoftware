using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaTandemSoftware.Domain.Entities;

namespace PruebaTecnicaTandemSoftware.Infraestructure.Repository;
public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Product>("SELECT * FROM Products");
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price); SELECT Id, Name, Price FROM Products WHERE Id = SCOPE_IDENTITY();";
        var createdProduct = await connection.QuerySingleAsync<Product>(query, product);
        return createdProduct;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", product);

        var updatedProduct = await connection.QueryFirstOrDefaultAsync<Product>(
            "SELECT * FROM Products WHERE Id = @Id", new { product.Id });

        return updatedProduct!;
    }


    public async Task DeleteProductAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = id });
    }

}