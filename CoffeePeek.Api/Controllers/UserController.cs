using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

public class UserController(IMediator mediator) : Controller
{
    [HttpPost("create")]
    public Task<CreateEntityResponse<UserDto>> CreateUser(CreateUserRequest request)
    {
        return mediator.Send(request);
    }
}