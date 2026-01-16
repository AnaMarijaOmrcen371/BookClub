using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Requests;
using Xunit;

namespace BookClub.Tests.Models.Requests
{
    public class CreateDiscussionRequestTests
    {
        [Fact]
        public void Constructor_ShouldInitializeTitleWithEmptyString()
        {
            // Act
            var request = new CreateDiscussionRequest();

            // Assert
            Assert.NotNull(request.Title);
            Assert.Equal(string.Empty, request.Title);
        }

        [Fact]
        public void Constructor_ShouldInitializeDescriptionAsNull()
        {
            // Act
            var request = new CreateDiscussionRequest();

            // Assert
            Assert.Null(request.Description);
        }

        [Fact]
        public void CanSetTitle()
        {
            // Arrange
            var request = new CreateDiscussionRequest();
            var title = "Test Discussion";

            // Act
            request.Title = title;

            // Assert
            Assert.Equal(title, request.Title);
        }

        [Fact]
        public void CanSetDescription()
        {
            // Arrange
            var request = new CreateDiscussionRequest();
            var description = "This is a test description.";

            // Act
            request.Description = description;

            // Assert
            Assert.Equal(description, request.Description);
        }
    }
}