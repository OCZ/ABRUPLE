﻿using Abruple.App.Hubs;
using Abruple.App.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Abruple.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Abruple.Models;
    using Abruple.Models.Enums;
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


     
        // pepster's code 

        //Cheks if user can uppload pictures/participate in contest  
        [HttpGet]
        public ActionResult Participate(int id)
        {
            var contest = this.Data.Contests.All().FirstOrDefault(c => c.Id == id);
            if (contest == null)
            {
                return this.HttpNotFound();
            }

            //chek if Contest has to be closed
            if (IfkDeadlineRiched(contest))
            {
                contest.State = ContestState.Closed;

                this.Data.SaveChanges();

                return RedirectToAction("Winners", new { closedContest = contest });
            }
  
            var isAllowed = false;

            if (contest.ParticipationStrategy == EntryType.Open)
            {
                isAllowed = true;
            }
            else
            {
                if (UserProfile != null && contest.AllowedParticipants.Contains(UserProfile))
                {
                    isAllowed = true;
                }
            }

            if (isAllowed)
            {
                return null; // has to redirect to Action for upploading picture;
            }

            this.ViewBag.Msg = "Not allowed Participation";
            return RedirectToAction("Details", contest.Id);

        }

        //CLOSE CONTEST => called by button CLOSE

        [HttpGet]
        public ActionResult Close(int id)
        {
            var contest = this.Data.Contests.All().FirstOrDefault(c => c.Id == id);
            if (contest == null)
            {
                return this.HttpNotFound();
            }
            contest.State = ContestState.Closed;

            this.Data.SaveChanges();

            return RedirectToAction("Winners", new { closedContest = contest });
        }


        //SET WINNERS FOR THE CONTEST
        [HttpGet]
        public ActionResult Winners(Contest closedContest)
        {
           
            //TODO select winner according to reward strategy

            return null;
        }

        //Dismiss contest -> called by Ditails button
        [HttpGet]
        public ActionResult Dismissed(int id)
        {
            var contest = this.Data.Contests.All().FirstOrDefault(c => c.Id == id);
            if (contest == null)
            {
                return this.HttpNotFound();
            }

            if (this.UserProfile.Id != contest.CreatorId)
            {
                this.ViewBag.Msg = "action not allowed";
                return RedirectToAction("Details", new {id = contest.Id});
            }
            contest.State = ContestState.Dismissed;
            this.ViewBag.Msg = "Contest is dismissed";
            return RedirectToAction("Details", new { id = contest.Id });
        }


        //CHECK if contest has to be closed due to DeadlineStrategy
        [NonAction]
        private bool IfkDeadlineRiched(Contest contest)
        {
            if ((contest.DeadlineStrategy == DeadlineStrategy.ByParticipants) 
                && (contest.Participants.Count >= contest.ParticipantCount))
            {
                return true;
            }

            var dateToClose = contest.CreatedOn.AddDays(contest.TimeSpan.Value.Days);
            if (DateTime.Now >= dateToClose)
            {
                return true;
            }

            return false;
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