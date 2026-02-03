using TodoTaskApi.Models;

namespace TodoTaskApi.Services;

public interface IToDoService
{
    Task<IEnumerable<ToDoItem>> GetAllAsync();
    Task<ToDoItem?> GetByIdAsync(int id);
    Task<ToDoItem> CreateAsync(ToDoItem item);
    Task<bool> UpdateAsync(ToDoItem item);
    Task<bool> DeleteAsync(int id);
}
