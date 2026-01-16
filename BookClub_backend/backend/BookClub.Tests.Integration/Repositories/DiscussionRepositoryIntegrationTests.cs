using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Models.Entities;
using BookClub.Repositories;
using BookClub.Strategies.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BookClub.IntegrationTests.Repositories
{
    public class DiscussionRepositoryIntegrationTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DiscussionRepositoryIntegrationTests(
            CustomWebApplicationFactory factory)
        {
            _scopeFactory = factory.Services
                .GetRequiredService<IServiceScopeFactory>();
        }

        [Fact]
        public async Task AddAndGetById_WorksAgainstRealDatabase()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var repository = new DiscussionRepository(context);

            var discussion = new Discussion
            {
                Title = "Repo integration test",
                Description = "Persisted in SQL Server",
                CreatedByUserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(discussion);
            await repository.SaveChangesAsync();

            var fromDb = await repository.GetByIdAsync(discussion.Id);

            Assert.NotNull(fromDb);
            Assert.Equal(discussion.Title, fromDb!.Title);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOrderedByCreatedAtDesc()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            context.Discussions.RemoveRange(context.Discussions);
            await context.SaveChangesAsync();

            var repository = new DiscussionRepository(context);

            var older = new Discussion
            {
                Title = "Older",
                Description = "Old",
                CreatedByUserId = 1,
                CreatedAt = DateTime.UtcNow.AddMinutes(-10)
            };

            var newer = new Discussion
            {
                Title = "Newer",
                Description = "New",
                CreatedByUserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(older);
            await repository.AddAsync(newer);
            await repository.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.Equal("Newer", result.First().Title);
        }

        [Fact]
        public async Task GetAllAsync_WithSortingStrategy_AppliesStrategy()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            context.Discussions.RemoveRange(context.Discussions);
            await context.SaveChangesAsync();

            var repository = new DiscussionRepository(context);

            var d1 = new Discussion
            {
                Title = "B title",
                Description = "B",
                CreatedByUserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            var d2 = new Discussion
            {
                Title = "A title",
                Description = "A",
                CreatedByUserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(d1);
            await repository.AddAsync(d2);
            await repository.SaveChangesAsync();

            var strategy = new TitleAscendingStrategy();

            var result = await repository.GetAllAsync(strategy);

            Assert.Equal("A title", result.First().Title);
        }
    }

    // Test-only strategy
    internal class TitleAscendingStrategy
        : IDiscussionSortingStrategy
    {
        public IQueryable<Discussion> Apply(
            IQueryable<Discussion> query)
            => query.OrderBy(d => d.Title);
    }
}
