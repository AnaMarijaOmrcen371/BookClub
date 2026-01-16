using BookClub.Data;
using BookClub.Models.Entities;
using BookClub.Repositories.Interfaces;
using BookClub.Strategies.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Repositories;

public class DiscussionRepository : IDiscussionRepository
{
    private readonly ApplicationDbContext _context;

    public DiscussionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Discussion discussion)
        => await _context.Discussions.AddAsync(discussion);

    public async Task<List<Discussion>> GetAllAsync()
        => await _context.Discussions
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();

    public async Task<Discussion?> GetByIdAsync(int id)
        => await _context.Discussions.FirstOrDefaultAsync(d => d.Id == id);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
    public async Task<List<Discussion>> GetAllAsync(
    IDiscussionSortingStrategy sortingStrategy)
    {
        var query = _context.Discussions.AsQueryable();
        query = sortingStrategy.Apply(query);

        return await query.ToListAsync();
    }

}
