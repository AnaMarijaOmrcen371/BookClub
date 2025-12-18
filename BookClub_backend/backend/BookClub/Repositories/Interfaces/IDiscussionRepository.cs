using BookClub.Models.Entities;

namespace BookClub.Repositories.Interfaces;

public interface IDiscussionRepository
{
    Task AddAsync(Discussion discussion);
    Task<List<Discussion>> GetAllAsync();
    Task<Discussion?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
