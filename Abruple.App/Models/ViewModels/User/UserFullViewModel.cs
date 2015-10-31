namespace Abruple.App.Models.ViewModels.User
{
    using System.Collections.Generic;
    using Abruple.Models;
    using Contest;
    using ContestEntry;

    public class UserFullViewModel: UserPersonalDataViewModel
    {

        public ICollection<ContestConciseViewModel> ContestsCreated { get; set; }
       
        public ICollection<ContestEntryConciseViewModel> ContestEntries{ get; set; }
                 
        //VOTES GIVEN BY THE USER
        public ICollection<VoteViewModel> Votes { get; set; }
          
         //CONTESTS IN WHICH PARTICIPATED
        public ICollection<ContestConciseViewModel> ContestsParticipated { get; set; }
         
         //COLLECTION OF STRATEGIES/CONTEST IN WHICH USER IS ALLOWED TO VOTE BY THE OWNER OF CONTEST
        public ICollection<ContestConciseViewModel> AllowedVoting { get; set; }
         
         //COLLECTION OF STRATEGIES/CONTEST IN WHICH USER IS ALLOWED TO PARTICIPATE BY THE OWNER OF CONTEST
        public ICollection<ContestConciseViewModel> AllowedParticipation { get; set; }
         
         //NOTIFICATIONS RECIVED
        public ICollection<Notification> Notifications{ get; set; }
        
    }
}