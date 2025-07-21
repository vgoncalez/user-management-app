using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Auth.Queries;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserQuery query)
    {
        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
