namespace Abruple.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class Contest
    {
        private ICollection<User> _participants;
        private ICollection<User> _allowedParticipants;
        private ICollection<User> _committee;
        
        private ICollection<ContestEntry> _contestEntries;
        private ICollection<ContestEntry> _winners;
        
        private ICollection<Prize> _prizes;

        public Contest()
        {
            this._winners = new HashSet<ContestEntry>();
            this._contestEntries = new HashSet<ContestEntry>();
            this._allowedParticipants = new HashSet<User>();
            this._participants = new HashSet<User>();
            this._committee = new HashSet<User>();
            this._prizes = new HashSet<Prize>();
        }

        // ID
        public int Id { get; set; }

        // TITLE
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        // DESCRIPTION
        [Required]
        [MaxLength(250, ErrorMessage = "The {0} must not be longer than {1} characters.")]
        public string Description { get; set; }

        // STATE
        [Required]
        public ContestState State { get; set; }

        // DATE CREATED
        [Required]
        public DateTime CreatedOn { get; set; }

        // CREATOR
        [Required]
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }

        // CONTEST ENTRIES
        public virtual ICollection<ContestEntry> ContestEntries
        {
            get { return this._contestEntries; }
            set { this._contestEntries = value; }
        }

        // PARTICIPANTS -> unique users that are participated in the contest
        // NB : 1 user can participate with some entries in the contest
        public virtual ICollection<User> Participants
        {
            get { return this._participants; }
            set { this._participants = value; }
        }

        // VOTING STRATEGY -> type: OPEN/CLOSE
        public EntryType VotingStrategy { get; set; }

        // VOTINGSTRATEGY -> if is CLOSE => USERS WHICH ARE ALLOWED TO VOTE
        public virtual ICollection<User> Committee
        {
            get { return this._committee; }
            set { this._committee = value; }
        }
        
        // PARTICIPATION STRATEGY -> type: OPEN/CLOSE
        public EntryType ParticipationStrategy { get; set; }

        // PARTICIPATION STRATEGY -> if is CLOSE => USERS  WHICH ARE ALLOWED TO UPLOADED ENTRIES
        public virtual ICollection<User> AllowedParticipants
        {
            get { return this._allowedParticipants; }
            set { this._allowedParticipants = value; }
        }
        
        // REWARD STRATEGY - type of winning => single winner/multiple winners
        public RewardStrategy RewardStrategy { get; set; }

        // REWARD STRATEGY - if reward strategy == multiple => collection of prizes
        public virtual ICollection<Prize> Prizes
        {
            get { return this._prizes; }
            set { this._prizes = value; }
        }

        // REWARD STRATEGY - winners
        public virtual ICollection<ContestEntry> Winners
        {
            get { return this._winners; }
            set { this._winners = value; }
        }

        // DEADLINE STRATEGY -> type of ending the contest => byTyme/byParticipants(unique users)
        public DeadlineStrategy DeadlineStrategy { get; set; }
        
        //DEADLINE STRATEGY 
        public TimeSpan? TimeSpan { get; set; }

        // PARTICIPATION COUNT
        public int? ParticipantCount { get; set; }
    }
}
