using BookClub.Models.Dtos;
using BookClub.Models.Requests;

namespace BookClub.Services.Interfaces
{
    public interface IDiscussionService
    {
        Task<DiscussionDto> CreateAsync(CreateDiscussionRequest request);
        Task<List<DiscussionDto>> GetAllAsync(string sort);
        Task<DiscussionDto?> GetByIdAsync(int id);
    }
}
