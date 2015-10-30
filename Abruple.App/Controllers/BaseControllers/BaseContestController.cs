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

            var result = contests.OrderByDescending(c => c.CreatedOn);

            return result;
        }
    }
}