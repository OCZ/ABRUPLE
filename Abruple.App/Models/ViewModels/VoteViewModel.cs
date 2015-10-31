namespace Abruple.App.Models.ViewModels
{
    using ContestEntry;

    public class VoteViewModel
    {

        public int Id { get; set; }

        //VOTED FROM
        public string Autor { get; set; }
       
        //VOTED FOR
        public ContestEntryConciseViewModel ContestEntry { get; set; }
    }
}