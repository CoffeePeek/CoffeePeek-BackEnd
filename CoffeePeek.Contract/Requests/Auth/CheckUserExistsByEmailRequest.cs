using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CoffeePeek.Contract.Requests.Auth;

public class CheckUserExistsByEmailRequest(string email) : IRequest<Response.Response>
{
    [Required]
    public string Email { get; } = email;
}