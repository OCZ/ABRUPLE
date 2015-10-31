namespace Abruple.App.Models.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using Abruple.Models.Enums;
    using Microsoft.AspNet.Identity;

    public class UserFullViewModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePic { get; set; }
        public string AboutMe { get; set; }
        public DateTime Registration { get; set; }
        public bool BrowserRemembered { get; set; }

        /*
         *         public virtual ICollection<Contest> ContestsCreated
         *         public virtual ICollection<ContestEntry> ContestEntries
         *         
         * //VOTES GIVEN BY THE USER
         *         public virtual ICollection<Vote> Votes
         *  
         * //CONTESTS IN WHICH PARTICIPATED
         *         public virtual ICollection<Contest> ContestsParticipated
         * 
         * //COLLECTION OF STRATEGIES/CONTEST IN WHICH USER IS ALLOWED TO VOTE BY THE OWNER OF CONTEST
         *         public virtual ICollection<Contest> AllowedVoting
         * 
         * //COLLECTION OF STRATEGIES/CONTEST IN WHICH USER IS ALLOWED TO PARTICIPATE BY THE OWNER OF CONTEST
         *         public virtual ICollection<Contest> AllowedParticipation
         * 
         * //NOTIFICATIONS RECIVED
         *         public virtual ICollection<Notification> Notifications
         */
    }
}