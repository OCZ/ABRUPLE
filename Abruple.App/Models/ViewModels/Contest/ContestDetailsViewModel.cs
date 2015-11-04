namespace Abruple.App.Models.ViewModels.Contest
{
    using System;

    public class ContestDetailsViewModel
    {
        // ID
        public int Id { get; set; }

        // TITLE
        public string Title { get; set; }

        // DESCRIPTION
        public string Description { get; set; }

        // DATE
        public DateTime Date { get; set; }

        // AUTHOR
        public string Author { get; set; }
            
        // DEADLINE BY TIME OR PARTICIPANT LIMIT

        // TIME
        public DateTime? EndDate { get; set; }

        // PARTICIPANTS
        public int? ParticipantCount { get; set; }
    }
}