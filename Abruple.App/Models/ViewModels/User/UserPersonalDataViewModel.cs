namespace Abruple.App.Models.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using Abruple.Models.Enums;
    using Microsoft.AspNet.Identity;

    public class UserPersonalDataViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string ProfilePic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime Registration { get; set; }
        public string AboutMe { get; set; }

        public int PhotosUpplodedCount { get; set; }
        public int WinningPhotosCount { get; set; }
        public int ContestsParticipatedCount { get; set; }
        public int ContestsCreatedCount { get; set; }
    }
}