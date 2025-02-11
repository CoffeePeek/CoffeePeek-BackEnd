using CoffeePeek.BusinessLogic.Abstractions;
using CoffeePeek.Contract.Dtos.User;
using FluentAssertions;
using Xunit;

namespace CoffeePeek.BusinessLogic.Tests.Abstractions;

public class UserCreateValidationStrategyTests
{
    private readonly UserCreateValidationStrategy _validationStrategy;

    public UserCreateValidationStrategyTests()
    {
        _validationStrategy = new UserCreateValidationStrategy();
    }

    [Fact]
    public void Validate_ShouldReturnValid_WhenUserDtoIsCorrect()
    {
        // Arrange
        var user = new UserDto
        {
            Email = "valid.email@example.com",
            Password = "StrongPassword123"
        };

        // Act
        var result = _validationStrategy.Validate(user);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("", "Password must be between 6 and 30 characters")]
    [InlineData("short", "Password must be between 6 and 30 characters")]
    [InlineData("thispasswordiswaytoolongtobeaccepted123456789", "Password must be between 6 and 30 characters")]
    public void Validate_ShouldReturnInvalid_WhenPasswordIsInvalid(string password, string expectedError)
    {
        // Arrange
        var user = new UserDto
        {
            Email = "valid.email@example.com",
            Password = password
        };

        // Act
        var result = _validationStrategy.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ErrorMessage.Should().Be(expectedError);
    }

    [Theory]
    [InlineData("", "Invalid email address")]
    [InlineData("plainaddress", "Invalid email address")]
    [InlineData("missing.domain@", "Invalid email address")]
    [InlineData("@missing.localpart.com", "Invalid email address")]
    [InlineData("user@.invalid.com", "Invalid email address")]
    public void Validate_ShouldReturnInvalid_WhenEmailIsInvalid(string email, string expectedError)
    {
        // Arrange
        var user = new UserDto
        {
            Email = email,
            Password = "StrongPassword123"
        };

        // Act
        var result = _validationStrategy.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ErrorMessage.Should().Be(expectedError);
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenBothEmailAndPasswordAreInvalid()
    {
        // Arrange
        var user = new UserDto
        {
            Email = "invalid.email",
            Password = "short"
        };

        // Act
        var result = _validationStrategy.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ErrorMessage.Should().Be("Password must be between 6 and 30 characters");
    }
}