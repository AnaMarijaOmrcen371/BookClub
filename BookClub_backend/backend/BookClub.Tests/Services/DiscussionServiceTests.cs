using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Entities;
using BookClub.Models.Requests;
using BookClub.Repositories.Interfaces;
using BookClub.Services;
using BookClub.Services.Interfaces;
using BookClub.Factories.Interfaces;
using Moq;
using Xunit;


namespace BookClub.Tests.Services
{
    public class DiscussionServiceTests
    {
        private readonly Mock<IDiscussionRepository> _repositoryMock;
        private readonly Mock<IDiscussionFactory> _factoryMock;
        private readonly IDiscussionService _service;

        public DiscussionServiceTests()
        {
            _repositoryMock = new Mock<IDiscussionRepository>();
            _factoryMock = new Mock<IDiscussionFactory>();

            _service = new DiscussionService(
                _repositoryMock.Object,
                _factoryMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnDiscussionDto_WhenRequestIsValid()
        {
            // Arrange
            var userId = 1;
            var request = new CreateDiscussionRequest
            {
                Title = "Test discussion",
                Description = "Test description"
            };

            var discussion = new Discussion
            {
                Id = 1,
                Title = request.Title,
                Description = request.Description!,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = 1
            };

            _factoryMock
     .Setup(f => f.Create(request, userId))
     .Returns(discussion);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test discussion", result.Title);

            _factoryMock.Verify(f => f.Create(request, userId), Times.Once);

            _repositoryMock.Verify(r => r.AddAsync(discussion), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}