using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Entities;
using BookClub.Strategies;
using Xunit;

namespace BookClub.Tests.Strategies
{
    public class OldestFirstSortingStrategyTests
    {
        [Fact]
        public void Apply_ShouldSortDiscussionsByCreatedAt_Ascending()
        {
            // Arrange
            var discussions = new List<Discussion>
            {
                new Discussion { Id = 1, CreatedAt = new DateTime(2024, 5, 10) },
                new Discussion { Id = 2, CreatedAt = new DateTime(2023, 1, 15) },
                new Discussion { Id = 3, CreatedAt = new DateTime(2025, 3, 1) }
            }.AsQueryable();

            var strategy = new OldestFirstSortingStrategy();

            // Act
            var result = strategy.Apply(discussions).ToList();

            // Assert
            Assert.Equal(2, result[0].Id); 
            Assert.Equal(1, result[1].Id);
            Assert.Equal(3, result[2].Id); 
        }
    }
}