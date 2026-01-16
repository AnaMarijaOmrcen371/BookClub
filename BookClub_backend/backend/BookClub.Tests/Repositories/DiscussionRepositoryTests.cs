using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Entities;
using BookClub.Repositories;
using Xunit;
using BookClub.Tests.Helpers;
using BookClub.Tests.Strategies;

public class DiscussionRepositoryTests
{
    [Fact]
    public async Task AddAsync_AddsDiscussionToDatabase()
    {
        // Arrange
        using var context = DbContextHelper.CreateContext();
        var repository = new DiscussionRepository(context);

        var discussion = new Discussion
        {
            Title = "Test discussion",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        await repository.AddAsync(discussion);
        await repository.SaveChangesAsync();

        // Assert
        Assert.Single(context.Discussions);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsDiscussionsOrderedByCreatedAtDescending()
    {
        // Arrange
        using var context = DbContextHelper.CreateContext();
        context.Discussions.AddRange(
            new Discussion { Title = "Old", CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new Discussion { Title = "New", CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        var repository = new DiscussionRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal("New", result.First().Title);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectDiscussion()
    {
        // Arrange
        using var context = DbContextHelper.CreateContext();
        var discussion = new Discussion
        {
            Title = "Find me",
            CreatedAt = DateTime.UtcNow
        };

        context.Discussions.Add(discussion);
        await context.SaveChangesAsync();

        var repository = new DiscussionRepository(context);

        // Act
        var result = await repository.GetByIdAsync(discussion.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Find me", result!.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        using var context = DbContextHelper.CreateContext();
        var repository = new DiscussionRepository(context);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_WithSortingStrategy_AppliesStrategy()
    {
        // Arrange
        using var context = DbContextHelper.CreateContext();
        context.Discussions.AddRange(
            new Discussion { Title = "Second", CreatedAt = DateTime.UtcNow.AddDays(1) },
            new Discussion { Title = "First", CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        var repository = new DiscussionRepository(context);
        var strategy = new CreatedAtAscendingStrategy();

        // Act
        var result = await repository.GetAllAsync(strategy);

        // Assert
        Assert.Equal("First", result.First().Title);
    }
}