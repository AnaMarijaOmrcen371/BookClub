namespace BookClub.Models.Requests
{
    public class CreateDiscussionRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
