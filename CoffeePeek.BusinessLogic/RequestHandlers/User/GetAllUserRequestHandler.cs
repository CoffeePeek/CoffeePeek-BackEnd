using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Users;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.BusinessLogic.RequestHandlers;

public class GetAllUserRequestHandler : IRequestHandler<GetAllUsersRequest, Response<UserDto[]>>
{
    private readonly IUnitOfWork<CoffeePeekDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUserRequestHandler(IUnitOfWork<CoffeePeekDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<UserDto[]>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<User>();

        var usersQuery = repository.GetAll();
        
        var users = await usersQuery.ToListAsync(cancellationToken);
        
        var result = _mapper.Map<UserDto[]>(users);
        
        return Response.SuccessResponse<Response<UserDto[]>>(result);
    }
}