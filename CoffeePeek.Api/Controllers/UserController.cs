using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : Controller
{
    [HttpGet("Users")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Response<UserDto[]>> GetAllUsers(CancellationToken cancellationToken)
    {
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
    
    [HttpPost("Create")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<CreateEntityResponse<UserDto>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        return mediator.Send(request, cancellationToken);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<Response<bool>> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest(id);
        return mediator.Send(request, cancellationToken);
    }
}