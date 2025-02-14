using System;
using Copilot.Application.TodoItems.Commands.CreateTodoItem;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;

namespace Copilot.Application.UnitTests.TodoItems.Commands;

public class CreateTodoItemCommandValidatorTests
{
    [Test]
    public async Task ValidateAsync_EmptyTitle_ShouldReturnNotValid(){
        // Arrange
        CreateTodoItemCommand createTodoItemCommand = new(){Title = string.Empty};
        CreateTodoItemCommandValidator validator = new();

        // Act
        ValidationResult validationResult = await validator.ValidateAsync(createTodoItemCommand);
    
        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(x => x.PropertyName == nameof(CreateTodoItemCommand.Title));
    }

    [Test]
    public async Task ValidateAsync_TooLongTitle_ShouldReturnNotValid(){
        // Arrange
        const string title = "Title which is very long!!!!!!!!!!!";
        CreateTodoItemCommand createTodoItemCommand = new(){Title = title};
        CreateTodoItemCommandValidator validator = new();

        // Act
        ValidationResult validationResult = await validator.ValidateAsync(createTodoItemCommand);
    
        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(x => x.PropertyName == nameof(CreateTodoItemCommand.Title));
    }

    [Test]
    public async Task ValidateAsync_CorrectTitle_ShouldReturnValid(){
        // Arrange
        const string title = "Correct title";
        CreateTodoItemCommand createTodoItemCommand = new(){Title = title};
        CreateTodoItemCommandValidator validator = new();

        // Act
        ValidationResult validationResult = await validator.ValidateAsync(createTodoItemCommand);
    
        // Assert
        validationResult.IsValid.Should().BeTrue();
    }
}
