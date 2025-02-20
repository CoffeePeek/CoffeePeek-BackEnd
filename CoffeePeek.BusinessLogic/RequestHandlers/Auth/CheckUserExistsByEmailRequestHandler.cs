using CoffeePeek.Contract.Requests.Auth;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoffeePeek.BusinessLogic.RequestHandlers;

public class CheckUserExistsByEmailRequestHandler(UserManager<User> userManager) : IRequestHandler<CheckUserExistsByEmailRequest, Response>
{
    public async Task<Response> Handle(CheckUserExistsByEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        return user == null ? Response.ErrorResponse<Response>("User not found") : Response.SuccessResponse<Response>();
    }
}