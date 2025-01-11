using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Response;
using MediatR;

namespace CoffeePeek.Contract.Requests.User;

public class GetUserRequest : IRequest<Response<UserDto>>
{
    public int UserId { get;}

    public GetUserRequest(int userId)
    {
        UserId = userId;
    }
}