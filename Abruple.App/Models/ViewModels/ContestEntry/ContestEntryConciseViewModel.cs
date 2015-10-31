namespace Abruple.App.Models.ViewModels.ContestEntry
{
    using System;
    using Abruple.Models.Enums;

    public class ContestEntryConciseViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Upploaded { get; set; }

        public string PhotoUrl { get; set; }

        public string Author { get; set; }

        public int ContestId { get; set; }
        
        public string Contest { get; set; }

        public int Votes { get; set; }

        public ContestEntryState State { get; set; }

    }
}