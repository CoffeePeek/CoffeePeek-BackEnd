using System.Collections.Generic;
using System.Linq;
using CoffeePeek.Data.Models.Review;
using CoffeePeek.Data.Models.Users;
using CoffeePeek.Data.Tests.UnitOfWork;

namespace CoffeePeek.Data.Tests;

public static class InMemoryContextUtil
{
    public static InMemoryContext CreateInMemoryContext()
    {
        var context = new InMemoryContext();
        
        if (context.Users.Any())
        {
            return context;
        }
        
        context.AddRange(TestUsers);
        context.AddRange(TestReviews);
        context.SaveChanges();
        return context;
    }
    
    private static List<User> TestUsers=>
    [
        new() { Id = 1, FirstName = "A", LastName = "B", Email = "a@b.com", Password = "strongPassword"},
        new() { Id = 2, FirstName = "B" }
    ];

    private static List<Review> TestReviews =>
    [
        new() { Id = 1, Header = "A", Comment = "B", UserId = 1 },
        new() { Id = 2, Header = "B", Comment = "C", UserId = 2 },
        new() { Id = 3, Header = "C", Comment = "D", UserId = 1 },
        new() { Id = 4, Header = "D", Comment = "F", UserId = 2 },
        new() { Id = 5, Header = "E", Comment = "E", UserId = 1 },
        new() { Id = 6, Header = "F", Comment = "J", UserId = 2 }
    ]; 
}