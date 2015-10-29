namespace Abruple.App.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using AutoMapper.QueryableExtensions;
    using BaseControllers;
    using Data.Contracts;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Contest;


    public class ContestController : BaseContestController
    {
        public ContestController(IPhotoMasterData data) : base(data)
        {
            
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetContest(int entriesToSkip, string filter = "active")
        {
            System.Threading.Thread.Sleep(1000);
            var totalEntries = this.Data.Contests.All().Count();
            const int entriesToLoad = 10;

            entriesToSkip *= entriesToLoad;

            if (entriesToSkip < totalEntries)
            {
               IQueryable<ContestConciseViewModel> result = this.GetContestsCollection(filter)
                        .Skip(entriesToSkip)
                        .Take(entriesToLoad)
                        .ProjectTo<ContestConciseViewModel>();
              
                return this.Json(result, JsonRequestBehavior.AllowGet);
            }

            return null;
        }













        /*
         * FOR TESTING THE DB -  Uncomment the Index Action
         */

       // // TESTING DB Action
        public ActionResult Index()
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == "Admin");
            var contest = new Contest()
            {
                CreatedOn = DateTime.Now,
                Creator = user,
                Title = "pyrwo systezanie",
                Description = "pyrwo systezanie za prowerka wsichko li wliza w bazata kakto trqbwa",
                VotingStrategy = EntryType.Close,
                ParticipationStrategy = EntryType.Close,
                RewardStrategy = RewardStrategy.MultipleWinners,
                DeadlineStrategy = DeadlineStrategy.ByParticipants,
                ParticipantCount = 5,
            };
            this.Data.Contests.Add(contest);
            this.Data.SaveChanges();
            this.Data.Contests.Add(new Contest()
            {
                CreatedOn = DateTime.Now,
                Creator = user,
                Title = "wtoro systezanie",
                Description = "wtoro systezanie za prowerka wsichko li wliza w bazata kakto trqbwa"
            });
            this.Data.SaveChanges();

            var contestResult = this.Data.Contests.All().FirstOrDefault(c => c.Title == "pyrwo systezanie");
            var contestResult2 = this.Data.Contests.All().FirstOrDefault(c => c.Title == "wtoro systezanie");
            var contestEntry = new ContestEntry()
            {
                Title = "Pyrwo ContestEntry",
                Author = user,
                Contest = new Contest()
                {
                    CreatedOn = DateTime.Now,
                    Creator = user,
                    Title = "treto systezanie",
                    Description = "treto systezanie za prowerka wsichko li wliza w bazata kakto trqbwa"
                },
                IsApproved = true,
                PhotoUrl = "http://c0178598.cdn1.cloudfiles.rackspacecloud.com/crazy-service-request.jpg",
                Upploaded = DateTime.Now,
                WonContest = contestResult2,
            };
            this.Data.ContestEntries.Add(contestEntry);
            this.Data.SaveChanges();
            var contestEntryRezult = this.Data.ContestEntries.All()
                .FirstOrDefault(ce => ce.Title == "Pyrwo ContestEntry");
            this.Data.Votes.Add(new Vote()
            {
                ContestEntry = contestEntryRezult,
                Author = user
            });
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}