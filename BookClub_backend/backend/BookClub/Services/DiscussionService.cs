using BookClub.Factories.Interfaces;
using BookClub.Models.Dtos;
using BookClub.Models.Requests;
using BookClub.Repositories.Interfaces;
using BookClub.Services.Interfaces;
using BookClub.Strategies.Interfaces;
using BookClub.Strategies;

namespace BookClub.Services
{
    public class DiscussionService : IDiscussionService

    {
        
        private readonly IDiscussionRepository _repository;
        private readonly IDiscussionFactory _factory;

        public DiscussionService(
            IDiscussionRepository repository,
            IDiscussionFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task<DiscussionDto> CreateAsync(CreateDiscussionRequest request)
        {
            // 👇 SAV kreiranje ide kroz Factory
            var discussion = _factory.Create(request, userId: 1);

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

        public async Task<List<DiscussionDto>> GetAllAsync(string sort)
        {
            IDiscussionSortingStrategy strategy = sort switch
            {
                "oldest" => new OldestFirstSortingStrategy(),
                _ => new NewestFirstSortingStrategy()
            };

            var discussions = await _repository.GetAllAsync(strategy);

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
