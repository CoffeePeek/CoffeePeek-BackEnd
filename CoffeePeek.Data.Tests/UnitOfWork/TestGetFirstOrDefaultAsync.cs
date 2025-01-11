using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeePeek.Data.Models.Review;
using CoffeePeek.Data.Models.Users;
using Xunit;

namespace CoffeePeek.Data.Tests.UnitOfWork;

public class TestGetFirstOrDefaultAsync
{
    [Fact]
    public async Task TestGetFirstOrDefaultAsyncGetsCorrectItem()
    {
        await using var db = InMemoryContextUtil.CreateInMemoryContext();
        
        var repository = new Repository<User>(db);
        var city = await repository.FirstOrDefaultAsync(predicate: t => t.FirstName == "A");
        Assert.NotNull(city);
        Assert.Equal(1, city.Id);            
    }

    [Fact]
    public async Task TestGetFirstOrDefaultAsyncReturnsNullValue()
    {
        await using var db = InMemoryContextUtil.CreateInMemoryContext();
        
        var repository = new Repository<User>(db);
        var city = await repository.FirstOrDefaultAsync(t => t.FirstName == "Easy-E");
        Assert.Null(city);            
    }

    [Fact]
    public async Task TestGetFirstOrDefaultAsyncCanInclude()
    {
        await using var db = InMemoryContextUtil.CreateInMemoryContext();
        
        var repository = new Repository<User>(db);
        var user = await repository.FirstOrDefaultAsync(
            predicate: c => c.FirstName == "A");
        Assert.NotNull(user);
        Assert.NotNull(user.Reviews);
    }
}