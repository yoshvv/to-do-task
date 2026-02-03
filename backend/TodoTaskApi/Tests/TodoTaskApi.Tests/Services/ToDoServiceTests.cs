using System;
using System.Threading.Tasks;
using TodoTaskApi.Models;
using TodoTaskApi.Tests.TodoTaskApi.Tests.Base;
using Xunit;

namespace TodoTaskApi.Tests.Services;

public class ToDoServiceTests : ServiceTestBase
{
    [Fact]
    public async Task CreateAsync_Should_Adds_And_Fetch_Item()
    {
        var service = CreateService(out var db);

        var item = new ToDoItem { Title = "Test", Description = "desc" };
        var created = await service.CreateAsync(item);

        Assert.True(created.Id > 0);

        var fetched = await service.GetByIdAsync(created.Id);
        Assert.NotNull(fetched);
        Assert.Equal("Test", fetched!.Title);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Item()
    {
        var service = CreateService(out var db);
        var item = new ToDoItem { Title = "Initial" };
        var created = await service.CreateAsync(item);

        created.Title = "Updated";
        var updated = await service.UpdateAsync(created);
        Assert.True(updated);

        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal("Updated", fetched!.Title);
    }

    [Fact]
    public async Task DeleteAsync_Should_Removes_Item()
    {
        var service = CreateService(out var db);
        var item = new ToDoItem { Title = "ToDelete" };
        var created = await service.CreateAsync(item);

        var deleted = await service.DeleteAsync(created.Id);
        Assert.True(deleted);

        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Null(fetched);
    }
}
