using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoList.Server.Controllers;
using TodoList.Server.Models;
using TodoList.Server.Repositories;
using TodoList.Shared.Dto;

namespace TodoList.Tests.UnitTests.Builders
{
    public class TodosControllerBuilder
    {
        private Mock<ITodosRepository> _todosRepositoryMock;
        private Mock<ITodoListsRepository> _todoListsRepositoryMock;
        private Mock<IDbRepository> _dbRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<HttpContext> _httpContextMock;

        public TodosControllerBuilder()
        {
            _todosRepositoryMock = new Mock<ITodosRepository>();
            _todoListsRepositoryMock = new Mock<ITodoListsRepository>();
            _dbRepositoryMock = new Mock<IDbRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextMock = new Mock<HttpContext>();
        }

        public TodosController Build()
        {
            return new TodosController(_todosRepositoryMock.Object, _todoListsRepositoryMock.Object, _dbRepositoryMock.Object, _mapperMock.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        public TodosControllerBuilder WithListOfTodosExists(bool returnValue)
        {
            _todoListsRepositoryMock.Setup(x => x.ListOfTodosExists(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(returnValue);
            return this;
        }

        public TodosControllerBuilder WithTodoExists(bool returnValue)
        {
            _todosRepositoryMock.Setup(x => x.TodoExists(It.IsAny<int>(), It.IsAny<string>(), null))
                .ReturnsAsync(returnValue);
            return this;
        }

        public TodosControllerBuilder WithSaveChangesAsync(bool returnValue)
        {
            _dbRepositoryMock.Setup(x => x.SaveChangesAsync())
                .Returns(Task.FromResult(returnValue));
            return this;
        }

        public TodosControllerBuilder WithMapReturnTodo(Todo returnValue)
        {
            _mapperMock.Setup(x => x.Map<Todo>(It.IsAny<TodoForCreationDto>()))
                .Returns(returnValue);
            return this;
        }

        public TodosControllerBuilder WithMapReturnTodoDto(TodoDto returnValue)
        {
            _mapperMock.Setup(x => x.Map<TodoDto>(It.IsAny<Todo>()))
                .Returns(returnValue);
            return this;
        }

        public TodosControllerBuilder WithHttpContext(Claim claim)
        {
            _httpContextMock.Setup(x => x.User.FindFirst(It.IsAny<string>()))
                .Returns(claim);
            return this;
        }

        public void VerifyShouldCheckExistingList()
        {
            _todoListsRepositoryMock.Verify(x => x.ListOfTodosExists(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        public void VerifyShouldTodoExists()
        {
            _todosRepositoryMock.Verify(x => x.TodoExists(It.IsAny<int>(), It.IsAny<string>(), null), Times.Once);
        }

        public void VerifyShouldSaveList()
        {
            _dbRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        public void VerifyShouldMapObject()
        {
            _mapperMock.Verify(x => x.Map<Todo>(It.IsAny<TodoForCreationDto>()), Times.Once);
        }
    }
}