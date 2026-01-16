using BookClub.Models.Entities;
using BookClub.Strategies.Interfaces;

namespace BookClub.Strategies
{
    public class NewestFirstSortingStrategy : IDiscussionSortingStrategy
    {
        public IQueryable<Discussion> Apply(IQueryable<Discussion> query)
            => query.OrderByDescending(d => d.CreatedAt);
    }
}
