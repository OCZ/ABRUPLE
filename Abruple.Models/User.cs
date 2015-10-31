namespace Abruple.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Contest> _contestsCreated;
        private ICollection<Contest> _contestsParticipated;
        private ICollection<Contest> _allowedVoting;
        private ICollection<Contest> _allowedParticipation;
        
        private ICollection<ContestEntry> _contestEntries;
        private ICollection<Vote> _votes;
        private ICollection<Notification> _notifications;

        private const string DefaultProfilePic = "~/Content/images/user.jpg";

        public User()
        {
            this._contestsParticipated = new HashSet<Contest>();
            this._contestsCreated = new HashSet<Contest>();
            this._contestEntries = new HashSet<ContestEntry>();
            this._votes = new HashSet<Vote>();
            this._allowedVoting = new HashSet<Contest>();
            this._allowedParticipation = new HashSet<Contest>();
            this._notifications = new HashSet<Notification>();
            this.ProfilePic = DefaultProfilePic;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public string FullName 
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public Gender Gender { get; set; }

        public string AboutMe { get; set; }

        public string ProfilePic { get; set; }

        [Required]
        public DateTime Registration { get; set; }

        //if user is blockes by the Admin (if is blocked forbiden login, display info msg that is blocked by Admin )
        public bool IsBlocked { get; set; }

        public virtual ICollection<Contest> ContestsCreated
        {
            get { return this._contestsCreated; }
            set { this._contestsCreated = value; }
        }

        
        public virtual ICollection<ContestEntry> ContestEntries
        {
            get { return this._contestEntries; }
            set { this._contestEntries = value; }
        }

        //VOTES GIVEN BY THE USER
        public virtual ICollection<Vote> Votes
        {
            get { return this._votes; }
            set { this._votes = value; }
        }

        //CONTESTS IN WHICH PARTICIPATED
        public virtual ICollection<Contest> ContestsParticipated
        {
            get { return this._contestsParticipated; }
            set { this._contestsParticipated = value; }
        }

        //COLLECTION OF STRATEGIES/CONTEST IN WHICH USER IS ALLOWED TO VOTE BY THE OWNER OF CONTEST
        public virtual ICollection<Contest> AllowedVoting
        {
            get { return this._allowedVoting; }
            set { this._allowedVoting = value; }
        }

        //COLLECTION OF STRATEGIES/CONTEST IN WHICH USER IS ALLOWED TO PARTICIPATE BY THE OWNER OF CONTEST
        public virtual ICollection<Contest> AllowedParticipation
        {
            get { return this._allowedParticipation; }
            set { this._allowedParticipation = value; }
        }

        //NOTIFICATIONS RECIVED
        public virtual ICollection<Notification> Notifications
        {
            get { return this._notifications; }
            set { this._notifications = value; }
        }







        // GENERATE IDENTITY
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }

    }
}
