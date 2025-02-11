using System;
using System.Threading;
using System.Threading.Tasks;
using CoffeePeek.BusinessLogic.Abstractions;
using CoffeePeek.BusinessLogic.RequestHandlers;
using CoffeePeek.BusinessLogic.Services;
using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using JetBrains.Annotations;
using MapsterMapper;
using Moq;
using Xunit;

namespace CoffeePeek.BusinessLogic.Tests.RequestHandlers;

[TestSubject(typeof(CreateUserRequestHandler))]
public class CreateUserRequestHandlerTests
{
    private readonly Mock<IUnitOfWork<CoffeePeekDbContext>> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidationStrategy<UserDto>> _validationStrategyMock;
    private readonly CreateUserRequestHandler _handler;
    private readonly Mock<IUserPasswordService> _passwordServiceMock;

    public CreateUserRequestHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork<CoffeePeekDbContext>>();
        _mapperMock = new Mock<IMapper>();
        _validationStrategyMock = new Mock<IValidationStrategy<UserDto>>();
        _passwordServiceMock = new Mock<IUserPasswordService>();

        _handler = new CreateUserRequestHandler(
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _validationStrategyMock.Object,
            _passwordServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenValidationSucceeds()
    {
        // Arrange
        var password = "Password123";
        var hashPassword = _passwordServiceMock.Object.HashPassword(password);
        var cancellationToken = CancellationToken.None;
    
        var request = new CreateUserRequest
        {
            FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = password,
            UserName = "JohnDoe"
        };
        var userDto = new UserDto
        {
            FullName = "John Doe", FirstName = "John", LastName = "Doe", Email = "john@example.com",
            UserName = "JohnDoe", Password = hashPassword
        };

        var user = new Data.Models.Users.User
        {
            Id = 1, FirstName = "John", LastName = "Doe", UserName = "JohnDoe", Email = "john@example.com",
            Password = hashPassword
        };

        _mapperMock.Setup(m => m.Map<UserDto>(request)).Returns(userDto);
        _validationStrategyMock.Setup(v => v.Validate(userDto)).Returns(ValidationResult.Valid);
        _mapperMock.Setup(m => m.Map<Data.Models.Users.User>(userDto)).Returns(user);
        _unitOfWorkMock.Setup(u => u.DbContext.Add(user));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken, false)).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(user.Id, result.EntityId);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(cancellationToken, false), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrorResponse_WhenValidationFails()
    {
        // Arrange
        var password = "Password123";
        var hashPassword = _passwordServiceMock.Object.HashPassword(password);
        
        var request = new CreateUserRequest
        {
            FirstName = "Invalid", LastName = "User", Email = "john@example.com", Password = password,
            UserName = "InvalidUser"
        };
        var userDto = new UserDto
        {
            FullName = "Invalid User", FirstName = "Invalid", LastName = "User", Email = "john@example.com",
            UserName = "InvalidUser", Password = hashPassword
        };

        _mapperMock.Setup(m => m.Map<UserDto>(request)).Returns(userDto);
        _validationStrategyMock.Setup(v => v.Validate(userDto))
            .Returns(ValidationResult.Invalid("Invalid email address"));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid request: Invalid email address", result.Message);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>(), false), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnitOfWorkFails()
    {
        // Arrange
        var password = "Password123";
        var hashPassword = _passwordServiceMock.Object.HashPassword(password);

        var request = new CreateUserRequest
        {
            FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = password,
            UserName = "JohnDoe"
        };
        var userDto = new UserDto
        {
            FullName = "John Doe", FirstName = "John", LastName = "Doe", Email = "john@example.com",
            UserName = "JohnDoe", Password = hashPassword
        };

        var user = new Data.Models.Users.User
        {
            Id = 1, FirstName = "John", LastName = "Doe", UserName = "JohnDoe", Email = "john@example.com",
            Password = hashPassword
        };

        
        _mapperMock.Setup(m => m.Map<UserDto>(request)).Returns(userDto);
        _validationStrategyMock.Setup(v => v.Validate(userDto)).Returns(ValidationResult.Valid);
        _mapperMock.Setup(m => m.Map<Data.Models.Users.User>(userDto)).Returns(user);
        _unitOfWorkMock.Setup(u => u.DbContext.Add(user));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>(), false)).ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
        Assert.Equal("Database error", exception.Message);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>(), false), Times.Once);
    }
}