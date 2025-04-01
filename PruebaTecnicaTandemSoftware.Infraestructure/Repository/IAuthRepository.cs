using PruebaTecnicaTandemSoftware.Domain.Entities;

namespace PruebaTecnicaTandemSoftware.Infraestructure.Repository;

public interface IAuthRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
}