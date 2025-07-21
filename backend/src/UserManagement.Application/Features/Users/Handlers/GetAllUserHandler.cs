using MediatR;
using UserManagement.Application.Features.Users.Queries;
using UserManagement.Application.Features.Users.Responses;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.Users.Handlers;

public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserResponse>>
{
    private readonly IUserRepository _repository;

    public GetAllUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync(request.Name, request.Email, cancellationToken);

        return users
            .Select(user => new UserResponse(user))
            .ToList();
    }
}
