namespace BookClub.Models.Dtos
{
    public class CreateDiscussionRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
