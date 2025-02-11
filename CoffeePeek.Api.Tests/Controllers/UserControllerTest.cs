using System;
using System.Threading;
using System.Threading.Tasks;
using CoffeePeek.Api.Controllers;
using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using FluentAssertions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sentry;
using Xunit;

namespace CoffeePeek.Api.Tests.Controllers;

[TestSubject(typeof(UserController))]
public class UserControllerTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserController _controller;

    public UserControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();
        var hubMock = new Mock<IHub>();
        _controller = new UserController(_mediatorMock.Object, hubMock.Object);
    }

    [Fact]
    public async Task GetUser_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        var userId = 1;
        var user = new UserDto
        {
            FirstName = "test",
            LastName = "testovich",
            UserName = "testovich",
            Email = "testing@test.com",
            Password = "password",
        };
        var response = new Response<UserDto> { Success = true, Data = user };
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.GetUser(userId, CancellationToken.None);

        // Assert
        var actionResult = new OkObjectResult(result);
        actionResult.Should().NotBeNull();
        actionResult!.StatusCode.Should().Be(200);
        var responseData = actionResult.Value as Response<UserDto>;
        responseData.Should().NotBeNull();
    }

    [Fact]
    public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 333333333;
        var response = new Response<UserDto> { Success = false };
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.GetUser(userId, CancellationToken.None);

        // Assert
        var actionResult = new NotFoundResult();
        actionResult.Should().NotBeNull();
        actionResult!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnOk_WhenUserIsCreated()
    {
        // Arrange
        var request = new CreateUserRequest { UserName = "New User", Password = "password123" };
        var response = new CreateEntityResponse<UserDto>
            { Success = true, Data = new UserDto { UserName = "New User" } };

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.CreateUser(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.UserName.Should().Be("New User");
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnOk_WhenUserIsDeleted()
    {
        // Arrange
        var userId = 1;
        var response = new Response<bool> { Success = true, Data = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteUserRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.DeleteUser(userId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnServerError_WhenExceptionOccurs()
    {
        // Arrange
        var userId = 1;

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteUserRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        Func<Task> act = async () => await _controller.DeleteUser(userId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnOk()
    {
        // Arrange
        var users = new[]
        {
            new UserDto
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "john.doe@test.com"
            },
            new UserDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                UserName = "janedoe",
                Email = "jane.doe@test.com"
            }
        };

        var response = new Response<UserDto[]>
        {
            Success = true,
            Data = users
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllUsersRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.GetAllUsers(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().Be(true);
        result.Data.Length.Should().Be(users.Length);
        result.Data[0].Email.Should().Be(users[0].Email);
        result.Data[1].UserName.Should().Be(users[1].UserName);
    }
}