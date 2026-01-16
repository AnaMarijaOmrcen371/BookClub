using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Dtos;
using Xunit;

public class DiscussionDtoTests
{
    [Fact]
    public void DiscussionDto_Should_Assign_And_Return_Properties_Correctly()
    {
        // Arrange
        var createdAt = new DateTime(2024, 1, 1);

        // Act
        var dto = new DiscussionDto
        {
            Id = 1,
            Title = "Test Title",
            Description = "Test Description",
            CreatedAt = createdAt
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Title", dto.Title);
        Assert.Equal("Test Description", dto.Description);
        Assert.Equal(createdAt, dto.CreatedAt);
    }
}