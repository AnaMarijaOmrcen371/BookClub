using BookClub.Models.Dtos;
using BookClub.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookClub.Data;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiscussionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /api/discussions
        [HttpPost]
        public async Task<ActionResult<DiscussionDto>> CreateDiscussion(
            [FromBody] CreateDiscussionRequest request)
        {
            // 1) osnovna validacija inputa
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title is required.");
            }

            // 2) odredi trenutno prijavljenog usera (zasad hard-coded)
            var currentUserId = 1; // TODO: zamijeni auth-om

            // 3) mapiranje DTO -> entitet (MODEL)
            var discussion = new Discussion
            {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim() ?? string.Empty,
                CreatedByUserId = currentUserId,
                CreatedAt = DateTime.UtcNow
            };

            // 4) spremi u bazu
            _context.Discussions.Add(discussion);
            await _context.SaveChangesAsync();

            // 5) mapiranje entitet -> DTO za odgovor
            var dto = new DiscussionDto
            {
                Id = discussion.Id,
                Title = discussion.Title,
                Description = discussion.Description,
                CreatedAt = discussion.CreatedAt
            };

            // 6) vrati HTTP 201 + DTO
            return CreatedAtAction(nameof(GetDiscussionById),
                new { id = dto.Id }, dto);
        }

        // GET: /api/discussions/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DiscussionDto>> GetDiscussionById(int id)
        {
            var dto = await _context.Discussions
                .Where(d => d.Id == id)
                .Select(d => new DiscussionDto
                {
                    Id = d.Id,
                    Title = d.Title,
                    Description = d.Description,
                    CreatedAt = d.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscussions()
        {
            var discussions = await _context.Discussions
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new
                {
                    d.Id,
                    d.Title,
                    d.CreatedAt
                })
                .ToListAsync();

            return Ok(discussions);
        }

    }
}

