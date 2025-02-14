using System.Net.Http.Headers;
using Copilot.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Copilot.Application.Common.Helper;

public static class TodoItemsHelper
{
    public static string ConcatenateFirstTitles(int numberOfElements, List<TodoItem> todoItems){
        string concatenatedTitles = string.Empty;
        int i = 0;

        foreach(TodoItem todoItem in todoItems){
            if(i < numberOfElements)
                concatenatedTitles += " " + todoItem.Title;
            i++;
        }

        return concatenatedTitles;
    }
}
