using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PruebaTecnicaTandemSoftware.Domain.Entities;

namespace PruebaTecnicaTandemSoftware.Infraestructure.Repository;
public class AuthRepository : IAuthRepository
{
    private readonly string _connectionString;

    public AuthRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE email = @Email", new { Email = email });
    }

    public async Task<User> CreateUserAsync(User user)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "INSERT INTO Users (Email, Password) VALUES (@Email, @Password); SELECT Id, Email FROM Users WHERE Id = SCOPE_IDENTITY();";
        var createdProduct = await connection.QuerySingleAsync<User>(query, user);
        return createdProduct;
    }

}