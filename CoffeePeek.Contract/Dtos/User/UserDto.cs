using System.ComponentModel.DataAnnotations;

namespace CoffeePeek.Contract.Dtos.User;

public class UserDto
{
    public int? Id { get; set; }
    [Required] public string UserName { get; set; }
    [Required] public string FullName { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }

    public string Token { get; set; }

    public string[]? Roles { get; set; }
}