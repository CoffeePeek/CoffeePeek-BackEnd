namespace CoffeePeek.Contract.Response.Auth;

public class RegisterUserResponse 
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
    public string FullName { get; set; }
}