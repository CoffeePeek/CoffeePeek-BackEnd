using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Utilities.Exceptions;

namespace CoffeePeek.BusinessLogic.RequestHandlers;

public class GetUserRequestHandler : IRequestHandler<GetUserRequest, Response<UserDto>>
{
    private readonly IUnitOfWork<CoffeePeekDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserRequestHandler(IMapper mapper, IUnitOfWork<CoffeePeekDbContext> unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.DbContext.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        
        var result = _mapper.Map<UserDto>(user);
        
        return Response.SuccessResponse<Response<UserDto>>(result);
    }
}