using Ardalis.GuardClauses;
using Copilot.Application.TodoItems.Commands.UpdateTodoItem;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Entities;
using Copilot.Domain.Enums;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Copilot.Application.UnitTests.TodoItems.Commands;

public class UpdateTodoItemCommandHandlerTests
{
    private Mock<ITodoItemRepository> _mockTodoItemRepository;

    [SetUp]
    public void Setup(){
        _mockTodoItemRepository = new Mock<ITodoItemRepository>();
    }

    [Test]
    public void Handle_RepositoryThrowsException_ShouldThrowsException(){
        // Arrange
        var handler = new UpdateTodoItemCommandHandler(_mockTodoItemRepository.Object);
        UpdateTodoItemCommand updateTodoItemCommand = new(){Id = 1, Title = "Test"};
        _mockTodoItemRepository.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
        _mockTodoItemRepository.Setup(x => x.Update(It.IsAny<TodoItem>()));

        // Act
        Func<Task> act = () => handler.Handle(updateTodoItemCommand, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
        _mockTodoItemRepository.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        _mockTodoItemRepository.Verify(x => x.Update(It.IsAny<TodoItem>()), Times.Never);
    }

    [Test]
    public void Handle_IdNotExists_ShouldReturnsId(){
        // Arrange
        const int idNotExits = 10;
        var handler = new UpdateTodoItemCommandHandler(_mockTodoItemRepository.Object);
        UpdateTodoItemCommand updateTodoItemCommand = new(){Id = idNotExits, Title = "Test"};
        _mockTodoItemRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(() => null);
        _mockTodoItemRepository.Setup(x => x.Update(It.IsAny<TodoItem>()));

        // Act
        Func<Task> act = () => handler.Handle(updateTodoItemCommand, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
        _mockTodoItemRepository.Verify(x => x.Get(It.Is<int>(x => x == idNotExits)), Times.Once);
        _mockTodoItemRepository.Verify(x => x.Update(It.IsAny<TodoItem>()), Times.Never);
    }

    [Test]
    public void Handle_IdExists_ShouldBeSuccessful(){
        // Arrange
        var handler = new UpdateTodoItemCommandHandler(_mockTodoItemRepository.Object);
        UpdateTodoItemCommand updateTodoItemCommand = new(){Id = 10, Title = "Test", Done = true};
        TodoItem returnedByGetRepository = new TodoItem { Id = 10, Title = "Old title", Done = false};
        
        _mockTodoItemRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(returnedByGetRepository);
        _mockTodoItemRepository.Setup(x => x.Update(It.IsAny<TodoItem>()));

        // Act
        Func<Task> act = () => handler.Handle(updateTodoItemCommand, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
        _mockTodoItemRepository.Verify(x => x.Get(It.Is<int>(x => x == updateTodoItemCommand.Id)), Times.Once);
        _mockTodoItemRepository.Verify(x => x.Update(It.Is<TodoItem>(x => 
            x.Id == updateTodoItemCommand.Id
            && x.Done == updateTodoItemCommand.Done
            && x.Title == updateTodoItemCommand.Title
            && x.Note == returnedByGetRepository.Note
            && x.Priority == returnedByGetRepository.Priority
            && x.Reminder == returnedByGetRepository.Reminder
        )), Times.Once);
    }
}
