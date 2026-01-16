using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Controllers;
using BookClub.Models.Dtos;
using BookClub.Models.Requests;
using BookClub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookClub.Tests.Controllers
{
    public class DiscussionsControllerTests
    {
        private readonly Mock<IDiscussionService> _serviceMock;
        private readonly DiscussionsController _controller;

        public DiscussionsControllerTests()
        {
            _serviceMock = new Mock<IDiscussionService>();
            _controller = new DiscussionsController(_serviceMock.Object);
        }

        [Fact]
        public async Task CreateDiscussion_ReturnsCreatedAtAction_WhenValid()
        {
           
            var request = new CreateDiscussionRequest();

            var dto = new DiscussionDto
            {
                Id = 1
            };

            _serviceMock
                .Setup(s => s.CreateAsync(request))
                .ReturnsAsync(dto);

           
            var result = await _controller.CreateDiscussion(request);

           
            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(DiscussionsController.GetDiscussionById), created.ActionName);
            Assert.Equal(dto, created.Value);
        }

        [Fact]
        public async Task CreateDiscussion_ReturnsBadRequest_WhenArgumentExceptionThrown()
        {
            
            var request = new CreateDiscussionRequest();

            _serviceMock
                .Setup(s => s.CreateAsync(request))
                .ThrowsAsync(new ArgumentException("Invalid request"));

           
            var result = await _controller.CreateDiscussion(request);

            
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid request", badRequest.Value);
        }

        [Fact]
        public async Task GetDiscussionById_ReturnsOk_WhenFound()
        {
            
            var dto = new DiscussionDto { Id = 1 };

            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(dto);

            
            var result = await _controller.GetDiscussionById(1);

            
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, ok.Value);
        }

        [Fact]
        public async Task GetDiscussionById_ReturnsNotFound_WhenMissing()
        {
           
            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((DiscussionDto?)null);

           
            var result = await _controller.GetDiscussionById(1);

            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetDiscussions_ReturnsOk()
        {
           
            var list = new List<DiscussionDto>
            {
                new DiscussionDto { Id = 1 },
                new DiscussionDto { Id = 2 }
            };

            _serviceMock
                .Setup(s => s.GetAllAsync("newest"))
                .ReturnsAsync(list);

           
            var result = await _controller.GetDiscussions();

            
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(list, ok.Value);
        }
    }
}