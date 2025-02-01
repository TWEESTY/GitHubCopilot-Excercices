using System;
using Copilot.Application.TodoItems.Commands.CreateTodoItem;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Entities;
using Copilot.Domain.Enums;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Copilot.Application.UnitTests.TodoItems.Commands;

public class CreateTodoItemCommandHandlerTests
{
    private Mock<ITodoItemRepository> _mockTodoItemRepository;

    [SetUp]
    public void Setup(){
        _mockTodoItemRepository = new Mock<ITodoItemRepository>();
    }

    [Test]
    public void Handle_RepositoryThrowsException_ShouldThrowsException(){
        // Arrange
        var handler = new CreateTodoItemCommandHandler(_mockTodoItemRepository.Object);
        CreateTodoItemCommand createTodoItemCommand = new(){Title = "Test"};
        _mockTodoItemRepository.Setup(x => x.Create(It.IsAny<TodoItem>())).Throws(new Exception());

        // Act
        Func<Task<int>> act = () => handler.Handle(createTodoItemCommand, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task Handle_GoodData_ShouldReturnId(){
        // Arrange
        const string title = "Test";
        TodoItem returnedTodoItem = new TodoItem(){
            Done = true,
            Id = 1,
            Note = "Note",
            Priority = PriorityLevel.Medium,
            Reminder = DateTime.Now,
            Title = title,
        };

        var handler = new CreateTodoItemCommandHandler(_mockTodoItemRepository.Object);
        CreateTodoItemCommand createTodoItemCommand = new(){Title = title};
        _mockTodoItemRepository.Setup(x => x.Create(It.IsAny<TodoItem>())).Returns(returnedTodoItem);

        // Act
        int result = await handler.Handle(createTodoItemCommand, CancellationToken.None);

        // Assert
        result.Should().Be(returnedTodoItem.Id);
        _mockTodoItemRepository.Verify(x => x.Create(It.Is<TodoItem>(x => x.Title == title)), Times.Once);
    }
}
