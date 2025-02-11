using System;
using System.Linq;
using System.Threading.Tasks;
using CoffeePeek.Data.Databases;
using CoffeePeek.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CoffeePeek.Data.Tests.UnitOfWork;

public class UnitOfWorkTests
{
    private readonly CoffeePeekDbContext _dbContext;
    private readonly UnitOfWork<CoffeePeekDbContext> _unitOfWork;

    public UnitOfWorkTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<CoffeePeekDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new CoffeePeekDbContext(dbContextOptions);
        _unitOfWork = new UnitOfWork<CoffeePeekDbContext>(_dbContext);
    }

    [Fact]
    public async Task GetRepository_ShouldReturnRepositoryInstance()
    {
        // Act
        var repository = _unitOfWork.GetRepository<User>();

        // Assert
        Assert.NotNull(repository);
        Assert.IsType<Repository<User>>(repository);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldPersistChangesToDatabase()
    {
        // Arrange
        var repository = _unitOfWork.GetRepository<User>();
        repository.Add(new User
        {
            Id = 1,
            UserName = "test",
            FirstName = "Test User",
            LastName = "test",
            Email = "test@test.com",
            Password = Guid.NewGuid().ToString(),
            IsSoftDeleted = false
        });

        // Act
        var changes = await _unitOfWork.SaveChangesAsync();

        // Assert
        Assert.Equal(1, changes);
        var user = await _dbContext.Set<User>().FindAsync(1);
        Assert.NotNull(user);
        Assert.Equal("Test User", user.FirstName);
    }

    [Fact]
    public void Dispose_ShouldReleaseResources()
    {
        // Arrange
        var unitOfWork = new UnitOfWork<CoffeePeekDbContext>(_dbContext);

        // Act
        unitOfWork.Dispose();

        // Assert
        Assert.Throws<ObjectDisposedException>(() => _dbContext.Users.ToList());
    }

    [Fact]
    public async Task GetRepository_WithCustomRepository_ShouldUseCustomRepository()
    {
        // Arrange
        var customRepoMock = new Mock<IRepository<User>>();
        
        // Act
        var repository = _unitOfWork.GetRepository<User>(hasCustomRepository: true);

        // Assert
        Assert.Equal(customRepoMock.Object, repository);
    }
}