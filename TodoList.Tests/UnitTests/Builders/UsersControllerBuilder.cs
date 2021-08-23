using Moq;
using TodoList.Server.Controllers;
using TodoList.Server.Repositories;
using TodoList.Shared.Auth;

namespace TodoList.Tests.UnitTests.Builders
{
    public class UsersControllerBuilder
    {
        private readonly Mock<IUsersRepository> _userRepositoryMcok;

        public UsersControllerBuilder()
        {
            _userRepositoryMcok = new Mock<IUsersRepository>();
        }

        public UsersController Build()
        {
            return new UsersController(_userRepositoryMcok.Object, null, null);
        }

        public UsersControllerBuilder WithAuthenticate(AuthenticateResponse authenticateResponse)
        {
            _userRepositoryMcok
                .Setup(x => x.Authenticate(It.IsAny<AuthenticateRequest>()))
                .ReturnsAsync(authenticateResponse);

            return this;
        }

        public void VerifyShouldAuthenticateUser()
        {
            _userRepositoryMcok.Verify(x => x.Authenticate(It.IsAny<AuthenticateRequest>()), Times.Once);
        }
    }
}
