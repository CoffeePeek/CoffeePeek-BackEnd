using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Entities.Users;
using CoffeePeek.Data.Models.Users;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.BusinessLogic.RequestHandlers;

public class GetAllUserRequestHandler(IUnitOfWork<CoffeePeekDbContext> unitOfWork, 
    IMapper mapper)
    : IRequestHandler<GetAllUsersRequest, Response<UserDto[]>>
{
    public async Task<Response<UserDto[]>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<User>();

        var usersQuery = repository.GetAll();
        
        var users = await usersQuery.ToListAsync(cancellationToken);
        
        var result = mapper.Map<UserDto[]>(users);
        
        return Response.SuccessResponse<Response<UserDto[]>>(result);
    }
}