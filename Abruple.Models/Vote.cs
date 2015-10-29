namespace Abruple.Models
{
    public class Vote
    {
        public int Id { get; set; }

        //VOTED FROM
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        //VOTED FOR
        public int ContestEntryId { get; set; }
        public virtual ContestEntry ContestEntry { get; set; }

    }
}
