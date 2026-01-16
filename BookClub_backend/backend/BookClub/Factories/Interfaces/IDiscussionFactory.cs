using BookClub.Models.Entities;
using BookClub.Models.Requests;

namespace BookClub.Factories.Interfaces
{
    public interface IDiscussionFactory
    {
        Discussion Create(CreateDiscussionRequest request, int userId);
    }
}
