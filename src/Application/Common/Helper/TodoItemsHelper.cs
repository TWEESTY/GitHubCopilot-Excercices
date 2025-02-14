using System.Net.Http.Headers;
using System.Text;
using Copilot.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Copilot.Application.Common.Helper;

public static class TodoItemsHelper
{
    public static string ConcatenateFirstTitles(int numberOfElements, List<TodoItem> todoItems)
    {
        StringBuilder concatenatedTitles = new StringBuilder();

        for (int i = 0; i < numberOfElements && i < todoItems.Count; i++)
        {
            concatenatedTitles.Append(" ").Append(todoItems[i].Title);
        }

        return concatenatedTitles.ToString();
    }
}
