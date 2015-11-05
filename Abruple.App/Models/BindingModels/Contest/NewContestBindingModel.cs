namespace Abruple.App.Models.BindingModels.Contest
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    using Abruple.Models.Enums;

    public class NewContestBindingModel
    {
        // TITLE
        [Required]
        [Display(Name = "Content title")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        // DESCRIPTION
        [Required]
        [Display(Name = "Content description")]
        [MaxLength(250, ErrorMessage = "The {0} must not be longer than {1} characters.")]
        public string Description { get; set; }

        // VOTING STRATEGY
        [Display(Name = "Who can to vote?")]
        public EntryType VotingStrategy { get; set; }

        // COMITTEE
        [Display(Name = "Members allowed to vote")]
        public virtual string[] Committee { get; set; }

        // PARTICIPATION STATEGY
        [Display(Name = "Who can to participate?")]
        public EntryType ParticipationStrategy { get; set; }

        // ALLOWED PARTICIPANTS
        [Display(Name = "Members allowed to participate")]
        public string[] AllowedParticipants { get; set; }

        // DEADLINE STRATEGY
        [Display(Name = "When the contest will over?")]
        public DeadlineStrategy DeadlineStrategy { get; set; }

        // DEADLINE BY TIME
        [Display(Name = "Deadline End Date")]
        public DateTime? EndDate { get; set; }

        // DEADLINE BY MAX PARTICIPANTS
        [Display(Name = "Max Participants")]
        public int? ParticipantCount { get; set; }

        // REWARD STRATEGY
        [Display(Name = "How many participants can receive prizes?")]
        public RewardStrategy RewardStrategy { get; set; }
    }
}