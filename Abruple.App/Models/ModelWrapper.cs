using Abruple.App.Models.BindingModels.Profile;

namespace Abruple.App.Models
{
    using BindingModels.Contest;
    using ViewModels.ContestEntry;
    using ViewModels.User;
    using BindingModels.ContestEntry;
    using PagedList;
    using ViewModels.Contest;

    public class ModelWrapper
    {
        public UserPersonalDataViewModel UserPersonalDataViewModel { get; set; }

        public NewContestBindingModel NewContestBindingModel { get; set; }

        public ContestEntryConciseViewModel ContestEntryConciseViewModel { get; set; }

        public ContestDetailsViewModel ContestDetailsViewModel { get; set; }

        public NewContestEntryBindingModel NewContestEntryBindingModel { get; set; }
        
        public IPagedList<ContestEntryShortViewModel> ContestEntryShortViewModel { get; set; }

        public EditProfileBindingModel EditProfileBindingModel { get; set; }

        public IPagedList<ContestEntryShortViewModel> ContestWinners { get; set; }

    }
}