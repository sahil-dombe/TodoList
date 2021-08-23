using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Server.Controllers;
using TodoList.Shared.Auth;
using TodoList.Tests.UnitTests.Builders;
using Xunit;

namespace TodoList.Tests.UnitTests
{
    public class UsersControllerTests
    {
        private UsersController _sut;
        private UsersControllerBuilder _sutBuilder;

        [Fact]
        public async Task AuthenticateShouldAuthenticateUser()
        {
            // Arrange
            var authenticationRequest = new AuthenticateRequest
            {
                Username = "username",
                Password = "password"
            };

            var authenticationResponse = new AuthenticateResponse
            {
                Username = "username",
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
            };

            _sutBuilder = new UsersControllerBuilder();
            _sut = _sutBuilder.WithAuthenticate(authenticationResponse)
                .Build();

            // Act

            var result = await _sut.Authenticate(authenticationRequest);

            // Assert

            _sutBuilder.VerifyShouldAuthenticateUser();

            var actionResult = Assert.IsType<ActionResult<AuthenticateResponse>>(result);
            var model = Assert.IsAssignableFrom<AuthenticateResponse>(((OkObjectResult)actionResult.Result).Value);
            Assert.Equal(authenticationResponse.Token, model.Token);
        }
    }
}
