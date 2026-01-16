using BookClub.Models.Dtos;
using BookClub.Models.Entities;
using BookClub.Strategies.Interfaces;

namespace BookClub.Repositories.Interfaces;

public interface IDiscussionRepository
{
    Task AddAsync(Discussion discussion);
    Task<Discussion?> GetByIdAsync(int id);
    Task<List<Discussion>> GetAllAsync(IDiscussionSortingStrategy strategy);
    Task SaveChangesAsync();

}
