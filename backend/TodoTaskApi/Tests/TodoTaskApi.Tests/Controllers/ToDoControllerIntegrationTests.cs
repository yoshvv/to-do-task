using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoTaskApi.Models;
using Xunit;
using TodoTaskApi.Tests.Base;

namespace TodoTaskApi.Tests.Controllers;

public class ToDoControllerIntegrationTests : BaseControllerTest, IClassFixture<WebApplicationFactory<Program>>
{
    public ToDoControllerIntegrationTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    private async Task<ToDoItem> CreateItemAsync(HttpClient client, ToDoItem newItem)
    {
        var postResp = await client.PostAsJsonAsync("/api/todo", newItem);
        postResp.EnsureSuccessStatusCode();
        var created = await postResp.Content.ReadFromJsonAsync<ToDoItem>();
        return created!;
    }

    [Fact]
    public async Task Post_Should_Create_Item()
    {
        var factory = CreateFactoryWithContentRoot();
        var client = factory.CreateClient();

        var newItem = new ToDoItem { Title = "Post Test", Description = "desc" };
        var postResp = await client.PostAsJsonAsync("/api/todo", newItem);

        Assert.Equal(HttpStatusCode.Created, postResp.StatusCode);

        var created = await postResp.Content.ReadFromJsonAsync<ToDoItem>();
        Assert.NotNull(created);
        Assert.True(created!.Id > 0);
        Assert.Equal(newItem.Title, created.Title);
    }

    [Fact]
    public async Task Get_Should_Return_Item()
    {
        var factory = CreateFactoryWithContentRoot();
        var client = factory.CreateClient();

        var newItem = new ToDoItem { Title = "Get Test", Description = "desc" };
        var created = await CreateItemAsync(client, newItem);

        var getResp = await client.GetAsync($"/api/todo/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResp.StatusCode);

        var fetched = await getResp.Content.ReadFromJsonAsync<ToDoItem>();
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
        Assert.Equal("Get Test", fetched.Title);
    }

    [Fact]
    public async Task Put_Should_Update_Item()
    {
        var factory = CreateFactoryWithContentRoot();
        var client = factory.CreateClient();

        var newItem = new ToDoItem { Title = "Put Test", Description = "desc" };
        var created = await CreateItemAsync(client, newItem);

        created.Title = "Put Updated";
        var putResp = await client.PutAsJsonAsync($"/api/todo/{created.Id}", created);
        Assert.Equal(HttpStatusCode.NoContent, putResp.StatusCode);

        var getResp = await client.GetAsync($"/api/todo/{created.Id}");
        var fetched = await getResp.Content.ReadFromJsonAsync<ToDoItem>();
        Assert.Equal("Put Updated", fetched!.Title);
    }

    [Fact]
    public async Task Delete_Should_Remove_Item()
    {
        var factory = CreateFactoryWithContentRoot();
        var client = factory.CreateClient();

        var newItem = new ToDoItem { Title = "Delete Test", Description = "desc" };
        var created = await CreateItemAsync(client, newItem);

        var delResp = await client.DeleteAsync($"/api/todo/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delResp.StatusCode);

        var getAfterDel = await client.GetAsync($"/api/todo/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getAfterDel.StatusCode);
    }
}
