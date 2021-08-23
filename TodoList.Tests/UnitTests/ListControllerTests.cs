using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using TodoList.Server.Models;
using TodoList.Server.Controllers;
using TodoList.Server.Repositories;
using TodoList.Shared.Dto;
using Xunit;
using System.Collections.Generic;
using TodoList.Tests.UnitTests.Builders;

namespace TodoList.Tests.UnitTests
{
    public class ListControllerTests
    {
        private ListsController _sut;
        private ListsControllerBuilder _sutBuilder;

        [Fact]
        public async Task NewListOfTodosShouldCreateNewList()
        {
            // Arrange
            var listOfTodos = new ListOfTodos()
            {
                Title = "title",
            };

            var listOfTodosForCreationDto = new ListOfTodosForCreationDto
            {
                Title = "title"
            };

            var listOfTodosDto = new ListOfTodosDto
            {
                Title = "title"
            };

            var userIdClaim = new Claim("id", "1");

            _sutBuilder = new ListsControllerBuilder();
            _sutBuilder.WithListOfTodosExists(false)
                    .WithSaveChangesAsync(true)
                    .WithMapReturnListOfTodos(listOfTodos)
                    .WithMapReturnListOfTodosDto(listOfTodosDto)
                    .WithHttpContext(userIdClaim);

            _sut = _sutBuilder.Build();

            // Act
            var result = await _sut.NewListOfTodos(listOfTodosForCreationDto);

            // Assert
            _sutBuilder.VerifyShouldCheckExistingList();
            _sutBuilder.VerifyShouldMapObject();
            _sutBuilder.VerifyShouldSaveList();
            var actionResult = Assert.IsType<ActionResult<ListOfTodosDto>>(result);
            var model = Assert.IsAssignableFrom<ListOfTodosDto>(((CreatedAtActionResult)actionResult.Result).Value);
            Assert.Equal("title", model.Title);
        }

    }
}