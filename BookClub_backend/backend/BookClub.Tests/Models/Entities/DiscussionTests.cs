using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Entities;
using Xunit;

namespace BookClub.Tests.Models.Entities
{
    public class DiscussionTests
    {
        [Fact]
        public void Discussion_Should_Set_And_Get_Properties_Correctly()
        {
            // Arrange
            var createdAt = DateTime.UtcNow;

            var discussion = new Discussion
            {
                Id = 1,
                Title = "Test Discussion",
                Description = "This is a test description",
                CreatedByUserId = 42,
                CreatedAt = createdAt
            };

            // Assert
            Assert.Equal(1, discussion.Id);
            Assert.Equal("Test Discussion", discussion.Title);
            Assert.Equal("This is a test description", discussion.Description);
            Assert.Equal(42, discussion.CreatedByUserId);
            Assert.Equal(createdAt, discussion.CreatedAt);
        }
    }
}