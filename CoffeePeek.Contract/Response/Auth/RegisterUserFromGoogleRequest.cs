using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CoffeePeek.Contract.Response.Auth;

public class RegisterUserFromGoogleRequest : IRequest
{
    [Required]
    public string TokenId { get; set; }
}