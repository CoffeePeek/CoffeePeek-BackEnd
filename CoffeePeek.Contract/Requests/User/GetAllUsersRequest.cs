using CoffeePeek.Contract.Dtos.User;
using MediatR;

namespace CoffeePeek.Contract.Requests.User;

public class GetAllUsersRequest : IRequest<Response.Response<UserDto[]>>;