using MediatR;
using UserManagement.Application.Features.Users.Commands;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserHandler(
        IUserRepository repository,
        IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByEmailAsync(request.Email, cancellationToken);

        if (existing != null)
            throw new Exception("Email já cadastrado.");

        var user = new User(request.Name, request.Email);

        user.SetPasswordHash(_passwordHasher.HashPassword(request.Password));

        await _repository.InsertAsync(user, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
