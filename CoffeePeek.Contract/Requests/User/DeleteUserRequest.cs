using CoffeePeek.Contract.Response;
using MediatR;

namespace CoffeePeek.Contract.Requests.User;

public class DeleteUserRequest : IRequest<Response<bool>>
{
    public int UserId { get; }

    public DeleteUserRequest(int userId)
    {
        UserId = userId;
    }
}