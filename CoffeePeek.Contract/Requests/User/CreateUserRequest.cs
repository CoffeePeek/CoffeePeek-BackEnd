using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Response;
using MediatR;

namespace CoffeePeek.Contract.Requests.User;

public class CreateUserRequest : IRequest<CreateEntityResponse<UserDto>>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}