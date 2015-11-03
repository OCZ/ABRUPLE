namespace Abruple.App.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using App.Controllers.BaseControllers;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data.Contracts;
    using Models;
    using PagedList;


    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {

#region Ctors
        public HomeController(IPhotoMasterData data) : base(data)
        {
        }

        public HomeController(IPhotoMasterData data, User user) : base(data, user)
        {
        }
#endregion

        //get all pending entries by contest id
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var contests = this.Data.Contests.All()
                .Where(c => c.ContestEntries.Any(ce => ce.State == ContestEntryState.Pending))
                .OrderByDescending(e => e.ContestEntries.Count(ce => ce.State == ContestEntryState.Pending))
                    .ThenByDescending(c => c.CreatedOn)
                .ProjectTo<AdminContestConciseViewModel>()
                .ToPagedList(page ?? GlobalConstants.DefaultStartPage, GlobalConstants.DefaultPageSizeContests);
            return this.View(contests);

        }
    }
}