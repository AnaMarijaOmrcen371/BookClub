using BookClub.Models.Entities;

namespace BookClub.Strategies.Interfaces
{
    public interface IDiscussionSortingStrategy
    {
        IQueryable<Discussion> Apply(IQueryable<Discussion> query);
    }
}
