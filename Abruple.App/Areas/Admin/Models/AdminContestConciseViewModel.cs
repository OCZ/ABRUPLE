namespace Abruple.App.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AdminContestConciseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int EntriesCount { get; set; }

        public int PendingEntriesCount { get; set; }
    }
}