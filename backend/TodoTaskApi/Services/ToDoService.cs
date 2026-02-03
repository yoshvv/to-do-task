using Microsoft.EntityFrameworkCore;
using TodoTaskApi.Data;
using TodoTaskApi.Models;

namespace TodoTaskApi.Services;

public class ToDoService : IToDoService
{
    private readonly ToDoDbContext _db;

    public ToDoService(ToDoDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ToDoItem>> GetAllAsync()
    {
        return await _db.ToDoItems.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<ToDoItem?> GetByIdAsync(int id)
    {
        return await _db.ToDoItems.FindAsync(id);
    }

    public async Task<ToDoItem> CreateAsync(ToDoItem item)
    {
        _db.ToDoItems.Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task<bool> UpdateAsync(ToDoItem item)
    {
        var existing = await _db.ToDoItems.FindAsync(item.Id);
        if (existing == null) return false;
        existing.Title = item.Title;
        existing.Description = item.Description;
        existing.IsCompleted = item.IsCompleted;
        existing.DueDate = item.DueDate;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.ToDoItems.FindAsync(id);
        if (existing == null) return false;
        _db.ToDoItems.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
