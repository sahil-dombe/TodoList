using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoList.Server.Controllers;
using TodoList.Server.Models;
using TodoList.Shared.Dto;
using TodoList.Tests.UnitTests.Builders;
using Xunit;

namespace TodoList.Tests.UnitTests
{
    public class TodosControllerTests
    {
        private TodosController _sut;
        private TodosControllerBuilder _sutBuilder;

        [Fact]
        public async Task NewTodoShouldCreateTodo()
        {
            // Arrange
            var todo = new Todo()
            {
                Title = "title",
            };

            var todoForCreationDto = new TodoForCreationDto
            {
                Title = "title"
            };

            var todoDto = new TodoDto
            {
                Title = "title"
            };

            var userIdClaim = new Claim("id", "1");

            _sutBuilder = new TodosControllerBuilder();
            _sutBuilder.WithListOfTodosExists(true)
                    .WithTodoExists(false)
                    .WithSaveChangesAsync(true)
                    .WithMapReturnTodo(todo)
                    .WithMapReturnTodoDto(todoDto)
                    .WithHttpContext(userIdClaim);

            _sut = _sutBuilder.Build();

            // Act
            var result = await _sut.NewTodo(1, todoForCreationDto);

            // Assert
            _sutBuilder.VerifyShouldCheckExistingList();
            _sutBuilder.VerifyShouldMapObject();
            _sutBuilder.VerifyShouldSaveList();
            var actionResult = Assert.IsType<ActionResult<TodoDto>>(result);
            var model = Assert.IsAssignableFrom<TodoDto>(((CreatedAtActionResult)actionResult.Result).Value);
            Assert.Equal("title", model.Title);
        }
    }
}
