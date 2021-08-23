using Moq;
using System;
using System.Text;
using TodoList.Server.Helpers;
using TodoList.Shared.Dto;
using Xunit;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace TodoList.Tests.UnitTests
{
    public class HashTests
    {

        [Fact]
        public void GetHashShouldReturnCorrectHash()
        {

            // Arrange
            var password = "strongpassword";

            // Act
            var actual = Hash.GetHash(password);

            // Assert
            Assert.Equal("05926FD3E6EC8C13C5DA5205B546037BDCF861528E0BDB22E9CECE29E567A1BC", actual);
        }

    }
}
