using BookClub.Models.Entities;
using BookClub.Strategies.Interfaces;

namespace BookClub.Strategies
{
    public class OldestFirstSortingStrategy : IDiscussionSortingStrategy
    {
        public IQueryable<Discussion> Apply(IQueryable<Discussion> query)
            => query.OrderBy(d => d.CreatedAt);
    }
}
