using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(string name, string email, CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task InsertAsync(User user, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
