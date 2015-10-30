namespace Abruple.App.Controllers.BaseControllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Abruple.Models;
    using Abruple.Models.Enums;
    using Data.Contracts;

    public abstract class BaseContestController : BaseController
    {
        protected BaseContestController(IPhotoMasterData data)
            : base(data)
        {
        }

        protected BaseContestController(IPhotoMasterData data, User user)
            : base(data, user)
        {
        }

        [NonAction]
        public IQueryable<Contest> GetContestsCollection(string filter)
        {
            var contests = this.Data.Contests.All();

            switch (filter)
            {
                case "active": contests = contests.Where(c => c.State == ContestState.Active); break;
                case "own": contests = contests.Where(c => c.CreatorId == this.UserProfile.Id); break;
                case "past": contests = contests.Where(c => c.State != ContestState.Active); break;
            }

            // IF FILTER DOESTN MATCH CASES => RETURN ALL
            var result = contests.OrderByDescending(c => c.CreatedOn);

            return result;
        }

        [NonAction]
        public Contest GetContestById(int id)
        {
            var contest = this.Data.Contests.All().FirstOrDefault(c => c.Id == id);
            return contest;
        }


        [NonAction]
        public bool? AllowedVoting(int contestId)
        {
            var contest = this.GetContestById(contestId);
            if (contest != null)
            {
                if (contest.VotingStrategy == EntryType.Open)
                {
                    return true;
                }
                if (this.UserProfile == null)
                {
                    return false;
                }
                if (contest.Committee.Contains(this.UserProfile))
                {
                    return true;
                }
                return false;
            }
            return null;
        }

        [NonAction]
        public bool? AllowedParticipation(int contestId)
        {
            var contest = this.GetContestById(contestId);
            if (contest != null)
            {
                if (contest.ParticipationStrategy == EntryType.Open)
                {
                    return true;
                }
                if (this.UserProfile == null)
                {
                    return false;
                }
                if (contest.AllowedParticipants.Contains(this.UserProfile))
                {
                    return true;
                }
                return false;
            }
            return null;
        }
    }
}