using System.ComponentModel.DataAnnotations;
using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Response;
using MediatR;

namespace CoffeePeek.Contract.Requests.User;

public class CreateUserRequest : IRequest<CreateEntityResponse<UserDto>>
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
}