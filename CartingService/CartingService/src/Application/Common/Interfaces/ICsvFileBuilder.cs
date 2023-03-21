using CartingService.Application.TodoLists.Queries.ExportTodos;

namespace CartingService.Application.Common.Interfaces;
public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
