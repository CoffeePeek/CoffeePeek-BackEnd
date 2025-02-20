using CoffeePeek.Contract.Dtos.CoffeeShop;
using CoffeePeek.Contract.Requests.CoffeeShop;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.CoffeeShop;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeePeek.BusinessLogic.RequestHandlers.CoffeeShop;

public class GetCoffeeShopsRequestHandler : IRequestHandler<GetCoffeeShopsRequest, Response<GetCoffeeShopsResponse>>
{
    private readonly CoffeePeekDbContext _unitOfWork;
    private readonly IMapper _mapper;

    public GetCoffeeShopsRequestHandler(IUnitOfWork<CoffeePeekDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork.DbContext;
        _mapper = mapper;
    }

    public async Task<Response<GetCoffeeShopsResponse>> Handle(GetCoffeeShopsRequest request, CancellationToken cancellationToken)
    {
        //TODO add validation pagination
        
        var totalCount = _unitOfWork.Shops.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
        
        var pageNumber = request.PageNumber;

        if (pageNumber > totalPages && totalPages > 0)
        {
            pageNumber = totalPages;
        }

        //TODO add sorting
        var coffeeShops = await _unitOfWork.Shops
            .AsNoTracking().AsSplitQuery()
            .Include(cs => cs.Address).ThenInclude(a => a.City)
            .Include(cs => cs.Address).ThenInclude(a => a.Street)
            .Include(x => x.Schedules)
            .Include(x => x.ScheduleExceptions)
            .Include(x => x.ShopPhotos)
            .OrderBy(cs => cs.Name) 
            .Skip((pageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var coffeeShopDtos = _mapper.Map<CoffeeShopDto[]>(coffeeShops);

        var response = new GetCoffeeShopsResponse
        {
            CoffeeShopDtos = coffeeShopDtos,
            CurrentPage = pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalCount,
            TotalPages = totalPages
        };
        
        return Response.SuccessResponse<Response<GetCoffeeShopsResponse>>(response);
    }
}