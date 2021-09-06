using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TodoList.Server;
using TodoList.Server.Models;
using TodoList.Shared.Dto;
using TodoList.Tests.IntegrationTests.Helper;
using Xunit;

namespace TodoList.Tests.IntegrationTests
{
    public class TodosControllerItgTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private TodoContext _todoContext;

        public TodosControllerItgTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            var scope = _factory.Services.CreateScope();
            _todoContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
        }

        [Fact]
        public async Task NewTodoShouldCreateTodo()
        {
            // Arrange
            var todoForCreationDto = new TodoForCreationDto
            {
                Title = "todo1"
            };

            var content = new StringContent(JsonConvert.SerializeObject(todoForCreationDto),Encoding.UTF8,"application/json");
            var todoList = new ListOfTodos {
                Title = "list2",
                UserId = 1
            };

            _todoContext.ListsOfTodos.Add(todoList);
            _todoContext.SaveChanges();
            var token = await Utilities.GetNewToken(_client);

            //Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync("/api/lists/1/Todos", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var TodosDto = JsonConvert.DeserializeObject<ListOfTodosDto>(responseContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(todoForCreationDto.Title, TodosDto.Title);
        }
    }
}
