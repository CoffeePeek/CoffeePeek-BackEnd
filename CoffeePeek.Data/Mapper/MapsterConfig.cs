using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Data.Models.Users;
using Mapster;

namespace CoffeePeek.Data.Mapper;

public static class MapsterConfig
{
    public static void MapperConfigure()
    {
        TypeAdapterConfig<User, UserDto>
           .NewConfig()
           .Map(d => d.FullName, s => $"{s.FirstName} {s.LastName}");
    }
}