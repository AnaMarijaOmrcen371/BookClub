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

namespace BookClub.Tests.Integration.Strategies
{
    public class OldestFirstSortingStrategyIntegrationTests
    {
        private static DbContextOptions<ApplicationDbContext> CreateOptions()
            => new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public void Apply_ShouldSortDiscussionsByCreatedAt_Ascending_FromDatabase()
        {
            // Arrange
            var options = CreateOptions();

            using (var context = new ApplicationDbContext(options))
            {
                context.Discussions.AddRange(
                    new Discussion
                    {
                        Id = 1,
                        CreatedAt = new DateTime(2024, 5, 10)
                    },
                    new Discussion
                    {
                        Id = 2,
                        CreatedAt = new DateTime(2023, 1, 15)
                    },
                    new Discussion
                    {
                        Id = 3,
                        CreatedAt = new DateTime(2025, 3, 1)
                    }
                );

                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var strategy = new OldestFirstSortingStrategy();

                // Act
                var result = strategy
                    .Apply(context.Discussions)
                    .ToList();

                // Assert
                Assert.Equal(new[] { 2, 1, 3 }, result.Select(d => d.Id));
            }
        }
    }
}