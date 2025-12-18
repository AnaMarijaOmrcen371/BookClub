using BookClub.Models.Dtos;
using BookClub.Models.Entities;
using BookClub.Models.Requests;
using BookClub.Repositories.Interfaces;
using BookClub.Services.Interfaces;

namespace BookClub.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _repository;

        public DiscussionService(IDiscussionRepository repository)
        {
            _repository = repository;
        }

        public async Task<DiscussionDto> CreateAsync(CreateDiscussionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title is required.");

            var discussion = new Discussion
            {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim() ?? string.Empty,
                CreatedByUserId = 1,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(discussion);
            await _repository.SaveChangesAsync();

            return new DiscussionDto
            {
                Id = discussion.Id,
                Title = discussion.Title,
                Description = discussion.Description,
                CreatedAt = discussion.CreatedAt
            };
        }

        public async Task<List<DiscussionDto>> GetAllAsync()
        {
            var discussions = await _repository.GetAllAsync();

            return discussions.Select(d => new DiscussionDto
            {
                Id = d.Id,
                Title = d.Title,
                CreatedAt = d.CreatedAt
            }).ToList();
        }

        public async Task<DiscussionDto?> GetByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            if (d == null) return null;

            return new DiscussionDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description,
                CreatedAt = d.CreatedAt
            };
        }
    }
}
