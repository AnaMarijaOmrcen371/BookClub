using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub.Models.Entities;
using BookClub.Strategies.Interfaces;

namespace BookClub.Tests.Strategies
{
    public class CreatedAtAscendingStrategy : IDiscussionSortingStrategy
    {
        public IQueryable<Discussion> Apply(IQueryable<Discussion> query)
        {
            return query.OrderBy(d => d.CreatedAt);
        }
    }
}