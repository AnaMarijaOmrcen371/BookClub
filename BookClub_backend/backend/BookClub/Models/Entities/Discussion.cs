namespace BookClub.Models.Entities
{
    public class Discussion
    {
        public int Id { get; set; }                 // PK u tablici
        public string Title { get; set; } = null!; 
        public string Description { get; set; } = null!;
        public int CreatedByUserId { get; set; }    // kasnije FK na Users
        public DateTime CreatedAt { get; set; }     
    }
}
