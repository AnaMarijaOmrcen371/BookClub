using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using BookClub.Models.Requests;
using Xunit;
using System.Net;

namespace BookClub.IntegrationTests.Controllers
{
    public class DiscussionsControllerIntegrationTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public DiscussionsControllerIntegrationTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateDiscussion_ReturnsCreated()
        {
            // Arrange
            var request = new CreateDiscussionRequest
            {
                Title = "Integration test discussion",
                Description = "Optional description"
            };

            // Act
            var response = await _client
                .PostAsJsonAsync("/api/discussions", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetDiscussionById_ReturnsOk_WhenExists()
        {
            // Arrange
            var createRequest = new CreateDiscussionRequest
            {
                Title = "GetById test",
                Description = "Test description"
            };

            var createResponse = await _client
                .PostAsJsonAsync("/api/discussions", createRequest);

            var created = await createResponse
                .Content.ReadFromJsonAsync<dynamic>();

            int id = created.id;

            // Act
            var response = await _client
                .GetAsync($"/api/discussions/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetDiscussions_ReturnsOk()
        {
            // Act
            var response = await _client
                .GetAsync("/api/discussions");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}