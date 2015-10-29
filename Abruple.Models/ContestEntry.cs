namespace Abruple.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ContestEntry
    {
        private ICollection<Vote> _votes;

        public ContestEntry()
        {
            this.IsWinner = false;
            this.WinningPlace = null;
            this._votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Upploaded { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        [Required]
        public int ContestId { get; set; }
        public virtual Contest Contest { get; set; }

        //IF ENTRY IS WINNER
        public bool IsWinner { get; private set; }

        //IF ENTRY IS WINNER => THE WON CONTEST
        public int? WonContestId { get; set; }
        public virtual Contest WonContest { get; set; }

        //IF ENTRY IS WINNER => WINING PLACE
        public int? WinningPlace { get; private set; }

        //IF ENTRY IS DELETED BY ADMIN
        public bool IsDeleted { get; set; }

        //IF ENTRY IS APPROVED BY ADMIN => NOT DISPLAYED IN THE LIST
        public bool IsApproved { get; set; }

        //VOTES RECIVED
        public virtual ICollection<Vote> Votes
        {
            get { return this._votes; }
            set { this._votes = value; }
        }

        // METHOD TO SET THE WINNING PLACE
        public void Win(int? winingPlace = null)
        {
            this.IsWinner = true;
            this.WinningPlace = winingPlace;
        }
    }
}
