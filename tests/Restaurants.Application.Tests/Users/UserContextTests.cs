using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // Arrange
            var dateOfBirth = new DateOnly(1990, 1, 1);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email,"test@test.com"),
                new Claim(ClaimTypes.Role,UserRoles.Admin),
                new Claim(ClaimTypes.Role,UserRoles.User),
                new Claim("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd")),
                new Claim("Nationality","US"),

            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // Act

            var currentUser = userContext.GetCurrentUser();


            // Asset

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().Contain(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("US");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);

        }

        [Fact()]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowInvalidOperationException()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // Act

            Action action = () => userContext.GetCurrentUser();

            // Assert

            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}