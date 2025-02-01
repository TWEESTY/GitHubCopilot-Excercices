using Copilot.Application.Common.Exceptions;
using Copilot.Application.TodoItems.Commands.CreateTodoItem;
using Copilot.Application.TodoLists.Commands.CreateTodoList;
using Copilot.Domain.Entities;

namespace Copilot.Application.FunctionalTests.TodoItems.Commands;

using static Testing;

public class CreateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoItem()
    {
        var command = new CreateTodoItemCommand
        {
            Title = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item.Title.Should().Be(command.Title);
    }
}
