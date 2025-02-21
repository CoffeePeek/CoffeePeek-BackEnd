using System.ComponentModel.DataAnnotations;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.Auth;
using MediatR;

namespace CoffeePeek.Contract.Requests.Auth;

public class RegisterUserRequest : IRequest<Response<RegisterUserResponse>>
{
    [Required] public string UserName { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}