using Abruple.App.Hubs;
using Abruple.App.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Abruple.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Abruple.Models;
    using AutoMapper.QueryableExtensions;

    using BaseControllers;
    using Data.Contracts;

    using Models.BindingModels.Contest;
    using Models.ViewModels.Contest;

    [Authorize]
    public class ContestController : BaseContestController
    {
        public ContestController(IPhotoMasterData data)
            : base(data)
        {
        }

        // INDEX
        [AllowAnonymous]
        public ActionResult Index()
        {
            return this.View();
        }

        // GET CONTESTS
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetContests(int entriesToSkip = 0, string filter = "active")
        {
            if (!Request.IsAjaxRequest())
            {
                return HttpNotFound();
            }

            const int entriesToLoad = 10;
            entriesToSkip *= entriesToLoad;

            var totalEntries = this.GetContestsCollection(filter).Count();

            if (entriesToSkip < totalEntries)
            {
                var result = this.GetContestsCollection(filter)
                         .Skip(entriesToSkip)
                         .Take(entriesToLoad)
                         .ProjectTo<ContestConciseViewModel>();


                return this.PartialView("_contestsListPartial", result);
            }

            return null;
        }

        // GET CONTEST DETAILS
        public ActionResult Details(int id)
        {
            var contest = this.Data.Contests.All()
                .Where(c => c.Id == id)
                .ProjectTo<ContestConciseViewModel>();

            return this.View("Details"/*, contest.First()*/);
        }

        // NEW CONTEST
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult NewContest(ModelWrapper model)
        {
            if (model.NewContestBindingModel == null)
            {
                return this.HttpNotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.HttpNotFound();
            }

            var contest = Mapper.Map<NewContestBindingModel, Contest>(model.NewContestBindingModel);
            contest.CreatedOn = DateTime.Now;
            contest.CreatorId = User.Identity.GetUserId();

            this.Data.Contests.Add(contest);
            this.Data.SaveChanges();

            var hub = new ContestsHub();
            hub.UpdateContest();

            return RedirectToAction("Index", "Contest");
        }

        // SEARCH CONTEST
        [HttpGet]
        public JsonResult Search(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var contests = this.Data.Contests.All()
                    .Where(c => c.Title.StartsWith(name))
                    .OrderBy(c => c.Title)
                    .ProjectTo<ContestConciseViewModel>();

                return this.Json(contests, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        /*
         * FOR TESTING THE DB -  Uncomment the Index Action
         */

        //// TESTING DB Action
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == "Admin");
        //    var contest = new Contest()
        //    {
        //        CreatedOn = DateTime.Now,
        //        Creator = user,
        //        Title = "pyrwo systezanie",
        //        Description = "pyrwo systezanie za prowerka wsichko li wliza w bazata kakto trqbwa",
        //        VotingStrategy = EntryType.Close,
        //        ParticipationStrategy = EntryType.Close,
        //        RewardStrategy = RewardStrategy.MultipleWinners,
        //        DeadlineStrategy = DeadlineStrategy.ByParticipants,
        //        ParticipantCount = 5
        //    };

        //    this.Data.Contests.Add(contest);
        //    this.Data.SaveChanges();

        //    var anotherContest = new Contest()
        //    {
        //        CreatedOn = DateTime.Now,
        //        Creator = user,
        //        Title = "wtoro systezanie",
        //        Description = "wtoro systezanie za prowerka wsichko li wliza w bazata kakto trqbwa"
        //    };

        //    this.Data.Contests.Add(anotherContest);
        //    this.Data.SaveChanges();

        //    var contestResult = this.Data.Contests.All().FirstOrDefault(c => c.Title == "pyrwo systezanie");
        //    var contestResult2 = this.Data.Contests.All().FirstOrDefault(c => c.Title == "wtoro systezanie");
            
        //    var contestEntry = new ContestEntry()
        //    {
        //        Title = "Pyrwo ContestEntry",
        //        Author = user,
        //        Contest = new Contest()
        //        {
        //            CreatedOn = DateTime.Now,
        //            Creator = user,
        //            Title = "treto systezanie",
        //            Description = "treto systezanie za prowerka wsichko li wliza w bazata kakto trqbwa"
        //        },
        //        IsApproved = true,
        //        PhotoUrl = "http://c0178598.cdn1.cloudfiles.rackspacecloud.com/crazy-service-request.jpg",
        //        Upploaded = DateTime.Now,
        //        WonContest = contestResult2,
        //    };
        //    this.Data.ContestEntries.Add(contestEntry);
        //    this.Data.SaveChanges();
            
        //    var contestEntryRezult = this.Data.ContestEntries.All()
        //        .FirstOrDefault(ce => ce.Title == "Pyrwo ContestEntry");
            
        //    this.Data.Votes.Add(new Vote()
        //    {
        //        ContestEntry = contestEntryRezult,
        //        Author = user
        //    });
        //    this.Data.SaveChanges();

        //    return RedirectToAction("Index", "Home");
        //}
    }
}