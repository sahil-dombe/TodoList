using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TodoList.Server;
using TodoList.Server.Models;
using TodoList.Shared.Dto;
using Xunit;

namespace TodoList.Tests.IntegrationTests
{
    public class ListControllerItgTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly TodoContext _todoContext;

        public ListControllerItgTests(CustomWebApplicationFactory<Startup> factory)
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
        public async Task NewListOfTodosShouldCreateNewList()
        {
            // Arrange
            var listOfTodosForCreationDto = new ListOfTodosForCreationDto
            {
                Title = "list1"
            };
            var content = new StringContent(JsonConvert.SerializeObject(listOfTodosForCreationDto),Encoding.UTF8,"application/json");
            
            //Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJ1c2VybmFtZSI6InNpbW8iLCJuYmYiOjE2Mjk2NTA1MTMsImV4cCI6MTYzMDI1NTMxMywiaWF0IjoxNjI5NjUwNTEzfQ.grZ2VmikIrjUCttunEGuMkTMSHcBMdKpxwWaz1q6CBQ");
            var response = await _client.PostAsync("/api/Lists",content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var listOfTodosDto = JsonConvert.DeserializeObject<ListOfTodosDto>(responseContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(listOfTodosForCreationDto.Title, listOfTodosDto.Title);
        }
    }
}
