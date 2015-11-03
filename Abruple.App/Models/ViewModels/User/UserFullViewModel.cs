namespace Abruple.App.Models.ViewModels.User
{
    using System.Collections.Generic;
    using Contest;
    using ContestEntry;

    public class UserFullViewModel
    {
        public UserPersonalDataViewModel PersonalData { get; set; }

        public ICollection<ContestConciseViewModel> ContestsCreated { get; set; }

        public ICollection<ContestConciseViewModel> ContestsParticipated { get; set; }

        public ICollection<ContestEntryConciseViewModel> ContestEntries { get; set; }
    }
}