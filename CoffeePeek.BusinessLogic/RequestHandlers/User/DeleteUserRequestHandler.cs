using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.BusinessLogic.RequestHandlers;

public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Response<bool>>
{
    private readonly IUnitOfWork<CoffeePeekDbContext> _unitOfWork;

    public DeleteUserRequestHandler(IUnitOfWork<CoffeePeekDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.DbContext.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Response.ErrorResponse<Response<bool>>("User not found");
        }
        
        user.IsSoftDeleted = true;
        
        await _unitOfWork.DbContext.SaveChangesAsync(cancellationToken);

        return Response.SuccessResponse<Response<bool>>(true);
    }
}