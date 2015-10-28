namespace Abruple.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Contest
    {
        // ID
        public int Id { get; set; }

        // TITLE
        [Required]
        public string Title { get; set; }

        // DESCRIPTION
        [Required]
        public string Description { get; set; }

        //// STATE
        //[Required]
        //public ContestState State { get; set; }

        //DATE CREATED
        [Required]
        public DateTime CreatedOn { get; set; }

        // CREATOR
//        [Required]
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }

    }
}
