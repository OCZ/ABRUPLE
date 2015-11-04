using Abruple.App.Models.BindingModels.Contest;
using Abruple.App.Models.ViewModels.ContestEntry;
using Abruple.App.Models.ViewModels.User;

namespace Abruple.App.Models
{
    using BindingModels.ContestEntry;
    using ViewModels.Contest;

    public class ModelWrapper
    {
        public UserPersonalDataViewModel UserPersonalDataViewModel { get; set; }

        public NewContestBindingModel NewContestBindingModel { get; set; }

        public ContestEntryConciseViewModel ContestEntryConciseViewModel { get; set; }

        public ContestDetailsViewModel ContestDetailsViewModel { get; set; }

        public NewContestEntryBindingModel NewContestEntryBindingModel { get; set; }
    }
}