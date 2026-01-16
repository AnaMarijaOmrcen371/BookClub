using BookClub.Tests.Strategies;
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
    public class NewestFirstSortingStrategyTests
    {
        [Fact]
        public void Apply_OrdersDiscussionsByCreatedAtDescending()
        {
            // Arrange
            var discussions = new List<Discussion>
            {
                new Discussion
                {
                    Id = 1,
                    Title = "Old discussion",
                    CreatedAt = new DateTime(2023, 1, 1)
                },
                new Discussion
                {
                    Id = 2,
                    Title = "Newest discussion",
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Discussion
                {
                    Id = 3,
                    Title = "Middle discussion",
                    CreatedAt = new DateTime(2023, 6, 1)
                }
            }.AsQueryable(); 

            var strategy = new NewestFirstSortingStrategy();

            // Act
            var result = strategy.Apply(discussions).ToList();

            // Assert
            Assert.Equal(2, result[0].Id); 
            Assert.Equal(3, result[1].Id);
            Assert.Equal(1, result[2].Id); 
        }
    }
}