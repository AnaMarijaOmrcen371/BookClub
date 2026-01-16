using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Factories;
using BookClub.Models.Requests;
using Xunit;

namespace BookClub.Tests.Factories
{
    public class DiscussionFactoryTests
    {
        [Fact]
        public void Create_ValidRequest_ReturnsDiscussionEntity()
        {
            // Arrange
            var factory = new DiscussionFactory();

            var request = new CreateDiscussionRequest
            {
                Title = "Factory test",
                Description = "Testing factory pattern"
            };

            var userId = 5;

            // Act
            var discussion = factory.Create(request, userId);

            // Assert
            Assert.NotNull(discussion);
            Assert.Equal("Factory test", discussion.Title);
            Assert.Equal("Testing factory pattern", discussion.Description);
            Assert.Equal(userId, discussion.CreatedByUserId);
            Assert.True(discussion.CreatedAt <= DateTime.UtcNow);
        }
    }
}
