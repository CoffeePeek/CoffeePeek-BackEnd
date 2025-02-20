using CoffeePeek.Contract.Dtos.Internal;
using CoffeePeek.Contract.Requests.Internal;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.Internal;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.BusinessLogic.RequestHandlers.Internal;

public class GetCitiesRequestHandler : IRequestHandler<GetCitiesRequest, Response<GetCitiesResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<CoffeePeekDbContext> _unitOfWork;

    public GetCitiesRequestHandler(IUnitOfWork<CoffeePeekDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<GetCitiesResponse>> Handle(GetCitiesRequest request, CancellationToken cancellationToken)
    {
        var cities = await _unitOfWork.DbContext.Cities.AsNoTracking().ToListAsync(cancellationToken);
        
        var cityDtos = _mapper.Map<CityDto[]>(cities);

        return Response.SuccessResponse<Response<GetCitiesResponse>>(cityDtos);
    }
}