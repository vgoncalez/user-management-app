using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface ITokenProvider
{
    public string Create(User user);
}
