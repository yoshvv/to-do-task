using System;
using Microsoft.EntityFrameworkCore;
using TodoTaskApi.Data;
using TodoTaskApi.Services;

namespace TodoTaskApi.Tests.TodoTaskApi.Tests.Base
{
    public abstract class ServiceTestBase
    {
        protected ToDoDbContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ToDoDbContext(options);
        }

        protected ToDoService CreateService(out ToDoDbContext db)
        {
            db = CreateInMemoryDb();
            return new ToDoService(db);
        }
    }
}
