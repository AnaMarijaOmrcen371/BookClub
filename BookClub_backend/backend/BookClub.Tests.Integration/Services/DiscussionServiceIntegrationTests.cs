using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Data;
using BookClub.Factories;
using BookClub.Models.Requests;
using BookClub.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BookClub.IntegrationTests.Services
{
    public class DiscussionServiceIntegrationTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DiscussionServiceIntegrationTests(
            CustomWebApplicationFactory factory)
        {
            _scopeFactory = factory.Services
                .GetRequiredService<IServiceScopeFactory>();
        }

        [Fact]
        public async Task CreateAsync_PersistsDiscussion_AndReturnsDto()
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider
                .GetRequiredService<DiscussionService>();

            var request = new CreateDiscussionRequest
            {
                Title = "Service integration test",
                Description = "Created through full pipeline"
            };

            var dto = await service.CreateAsync(request);

            Assert.True(dto.Id > 0);
            Assert.Equal(request.Title, dto.Title);
            Assert.Equal(request.Description, dto.Description);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsDto_WhenExists()
        {
            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider
                .GetRequiredService<DiscussionService>();

            var created = await service.CreateAsync(
                new CreateDiscussionRequest
                {
                    Title = "GetById test",
                    Description = "Lookup"
                });

            var fromDb = await service.GetByIdAsync(created.Id);

            Assert.NotNull(fromDb);
            Assert.Equal(created.Id, fromDb!.Id);
        }

        [Fact]
        public async Task GetAllAsync_Newest_ReturnsNewestFirst()
        {
            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider
                .GetRequiredService<DiscussionService>();

            await service.CreateAsync(new CreateDiscussionRequest
            {
                Title = "Old",
                Description = "Old"
            });

            await Task.Delay(50);

            await service.CreateAsync(new CreateDiscussionRequest
            {
                Title = "New",
                Description = "New"
            });

            var result = await service.GetAllAsync("newest");

            Assert.Equal("New", result.First().Title);
        }

        [Fact]
        public async Task GetAllAsync_Oldest_ReturnsOldestFirst()
        {
            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider
                .GetRequiredService<DiscussionService>();

            await service.CreateAsync(new CreateDiscussionRequest
            {
                Title = "Oldest",
                Description = "First"
            });

            await Task.Delay(50);

            await service.CreateAsync(new CreateDiscussionRequest
            {
                Title = "Newest",
                Description = "Last"
            });

            var result = await service.GetAllAsync("oldest");

            Assert.Equal("Oldest", result.First().Title);
        }
    }
}