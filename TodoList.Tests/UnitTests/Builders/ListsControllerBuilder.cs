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
    public class ListsControllerBuilder
    {
        private Mock<ITodoListsRepository> _todoListsRepositoryMock;
        private Mock<IDbRepository> _dbRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<HttpContext> _httpContextMock;

        public ListsControllerBuilder()
        {
            _todoListsRepositoryMock = new Mock<ITodoListsRepository>();
            _dbRepositoryMock = new Mock<IDbRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextMock = new Mock<HttpContext>();
        }

        public ListsController Build()
        {
            return new ListsController(_todoListsRepositoryMock.Object, _dbRepositoryMock.Object, _mapperMock.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContextMock.Object
                }
            };
        }

        public ListsControllerBuilder WithListOfTodosExists(bool returnValue)
        {
            _todoListsRepositoryMock.Setup(x => x.ListOfTodosExists(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(returnValue);
            return this;
        }

        public ListsControllerBuilder WithSaveChangesAsync(bool returnValue)
        {
            _dbRepositoryMock.Setup(x => x.SaveChangesAsync())
                .Returns(Task.FromResult(returnValue));
            return this;
        }

        public ListsControllerBuilder WithMapReturnListOfTodos(ListOfTodos returnValue)
        {
            _mapperMock.Setup(x => x.Map<ListOfTodos>(It.IsAny<ListOfTodosForCreationDto>()))
                .Returns(returnValue);
            return this;
        }

        public ListsControllerBuilder WithMapReturnListOfTodosDto(ListOfTodosDto returnValue)
        {
            _mapperMock.Setup(x => x.Map<ListOfTodosDto>(It.IsAny<ListOfTodos>()))
                .Returns(returnValue);
            return this;
        }

        public ListsControllerBuilder WithHttpContext(Claim claim)
        {
            _httpContextMock.Setup(x => x.User.FindFirst(It.IsAny<string>()))
                .Returns(claim);
            return this;
        }

        public void VerifyShouldCheckExistingList()
        {
            _todoListsRepositoryMock.Verify(x => x.ListOfTodosExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        public void VerifyShouldSaveList()
        {
            _dbRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        public void VerifyShouldMapObject()
        {
            _mapperMock.Verify(x => x.Map<ListOfTodos>(It.IsAny<ListOfTodosForCreationDto>()), Times.Once);
        }
    }
}
