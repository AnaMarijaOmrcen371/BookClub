namespace BookClub.Models.Dtos
{
    public class DiscussionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
