using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoTaskApi.Tests.Base
{
    public abstract class BaseControllerTest
    {
        protected readonly WebApplicationFactory<Program> Factory;

        protected BaseControllerTest(WebApplicationFactory<Program> factory)
        {
            Factory = factory;
        }

        protected WebApplicationFactory<Program> CreateFactoryWithContentRoot()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "TodoTaskApi"));
            return Factory.WithWebHostBuilder(builder => { builder.UseContentRoot(projectRoot); });
        }
    }
}
