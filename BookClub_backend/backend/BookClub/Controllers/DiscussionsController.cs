using BookClub.Models.Dtos;
using BookClub.Models.Requests;
using BookClub.Services;
using BookClub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly IDiscussionService _service;

        public DiscussionsController(IDiscussionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscussion([FromBody] CreateDiscussionRequest request)
        {
            try
            {
                var dto = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(GetDiscussionById), new { id = dto.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDiscussionById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscussions()
        {
            var discussions = await _service.GetAllAsync();
            return Ok(discussions);
        }
    }
}
