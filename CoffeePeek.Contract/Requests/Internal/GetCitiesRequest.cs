using CoffeePeek.Contract.Response.Internal;
using MediatR;

namespace CoffeePeek.Contract.Requests.Internal;

public class GetCitiesRequest : IRequest<Response.Response<GetCitiesResponse>>
{
    
}