using Xunit;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        
        public UserControllerTests()
        {
            _controller = new UserController();
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
        }

        [Fact]
        public void Details_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var testUser = new User { Id = 1, Name = "Test User", Email = "test@test.com" };
            _controller.Create(testUser);

            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Details_WithInvalidId_ReturnsNotFound()
        {
            // Act
            var result = _controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_GET_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_POST_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var newUser = new User { Name = "New User", Email = "new@test.com" };

            // Act
            var result = _controller.Create(newUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Create_POST_InvalidModel_ReturnsView()
        {
            // Arrange
            var controller = new UserController();
            controller.ModelState.AddModelError("Name", "Name is required");
            var invalidUser = new User();

            // Act
            var result = controller.Create(invalidUser);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<User>(viewResult.Model);
        }

        [Fact]
        public void Edit_GET_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var testUser = new User { Id = 1, Name = "Test User", Email = "test@test.com" };
            _controller.Create(testUser);

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Edit_GET_WithInvalidId_ReturnsNotFound()
        {
            // Act
            var result = _controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_POST_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var testUser = new User { Id = 1, Name = "Test User", Email = "test@test.com" };
            _controller.Create(testUser);
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@test.com" };

            // Act
            var result = _controller.Edit(1, updatedUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_GET_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var testUser = new User { Id = 1, Name = "Test User", Email = "test@test.com" };
            _controller.Create(testUser);

            // Act
            var result = _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Delete_POST_WithValidId_RedirectsToIndex()
        {
            // Arrange
            var testUser = new User { Id = 1, Name = "Test User", Email = "test@test.com" };
            _controller.Create(testUser);

            // Act
            var result = _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}