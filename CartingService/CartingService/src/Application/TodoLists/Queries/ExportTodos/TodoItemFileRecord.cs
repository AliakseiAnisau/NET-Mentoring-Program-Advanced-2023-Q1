using CartingService.Application.Common.Mappings;
using CartingService.Domain.Entities;

namespace CartingService.Application.TodoLists.Queries.ExportTodos;
public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
