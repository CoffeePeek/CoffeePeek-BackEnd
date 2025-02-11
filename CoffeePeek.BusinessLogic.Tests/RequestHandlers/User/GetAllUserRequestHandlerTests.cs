using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePeek.BusinessLogic.RequestHandlers;
using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using JetBrains.Annotations;
using MapsterMapper;
using Moq;
using Xunit;

namespace CoffeePeek.BusinessLogic.Tests.RequestHandlers;

using UserModel = Data.Models.Users.User;

[TestSubject(typeof(GetAllUserRequestHandler))]
public class GetAllUserRequestHandlerTests
{
    private readonly Mock<IRepository<UserModel>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllUserRequestHandler _handler;

    public GetAllUserRequestHandlerTests()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork<CoffeePeekDbContext>>();
        _repositoryMock = new Mock<IRepository<UserModel>>();
        _mapperMock = new Mock<IMapper>();

        unitOfWorkMock
            .Setup(uow => uow.GetRepository<UserModel>(false))
            .Returns(_repositoryMock.Object);

        _handler = new GetAllUserRequestHandler(unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<UserModel>
        {
            new() { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new() { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com" }
        }.AsQueryable();

        var userDtos = new[]
        {
            new UserDto { FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new UserDto { FirstName = "Jane", LastName = "Doe", Email = "jane@example.com" }
        };

        _repositoryMock.Setup(repo => repo.GetAll()).Returns(users);

        _mapperMock
            .Setup(mapper => mapper.Map<UserDto[]>(users))
            .Returns(userDtos);

        var request = new GetAllUsersRequest();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        Assert.Equal(2, response.Data.Length);
        Assert.Equal("John", response.Data[0].FirstName);
        Assert.Equal("Jane", response.Data[1].FirstName);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyArray_WhenNoUsersExist()
    {
        // Arrange
        var users = new List<UserModel>();
        var userDtos = Array.Empty<UserDto>();

        _repositoryMock
            .Setup(repo => repo.GetAll())
            .Returns(users.AsQueryable);

        _mapperMock
            .Setup(mapper => mapper.Map<UserDto[]>(users))
            .Returns(userDtos);

        var request = new GetAllUsersRequest();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        Assert.Empty(response.Data);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenRepositoryThrows()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.GetAll())
            .Throws(new Exception("Database error"));

        var request = new GetAllUsersRequest();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await _handler.Handle(request, CancellationToken.None));
        Assert.Equal("Database error", exception.Message);
    }
}