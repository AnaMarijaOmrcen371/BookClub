using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models.Entities;
using BookClub.Strategies;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class NewestFirstSortingStrategyIntegrationTests
{
    private ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public void Apply_ShouldSortDiscussions_ByCreatedAtDescending()
    {
        // Arrange
        using var context = CreateDbContext();

        var d1 = new Discussion
        {
            CreatedAt = DateTime.UtcNow.AddDays(-2)
        };

        var d2 = new Discussion
        {
            CreatedAt = DateTime.UtcNow
        };

        var d3 = new Discussion
        {
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };

        context.Discussions.AddRange(d1, d2, d3);
        context.SaveChanges();

        var strategy = new NewestFirstSortingStrategy();

        // Act
        var result = strategy
            .Apply(context.Discussions)
            .ToList();

        // Assert
        Assert.Equal(d2, result[0]);
        Assert.Equal(d3, result[1]);
        Assert.Equal(d1, result[2]);
    }
}