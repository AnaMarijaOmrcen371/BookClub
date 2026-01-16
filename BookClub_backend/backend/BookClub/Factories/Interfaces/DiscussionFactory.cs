using BookClub.Factories.Interfaces;
using BookClub.Models.Entities;
using BookClub.Models.Requests;

namespace BookClub.Factories
{
    public class DiscussionFactory : IDiscussionFactory
    {
        public Discussion Create(CreateDiscussionRequest request, int userId)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title is required.");

            return new Discussion
            {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim() ?? string.Empty,
                CreatedByUserId = userId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
