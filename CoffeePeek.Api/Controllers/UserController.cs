using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator, IHub hub) : Controller
{
    [Authorize]
    [HttpGet("test")] 

    public IActionResult Test()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }

    [HttpGet("Users")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto[]>> GetAllUsers(CancellationToken cancellationToken)
    {
        hub.GetSpan()?.StartChild("additional-work");
        
        var request = new GetAllUsersRequest();
        var result = await mediator.Send(request, cancellationToken);
        
        return result;
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto>> GetUser(int id, CancellationToken cancellationToken)
    {
        var request = new GetUserRequest(id);
        var result = await mediator.Send(request, cancellationToken);

        return result;
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<Response<bool>> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest(id);
        return mediator.Send(request, cancellationToken);
    }
}