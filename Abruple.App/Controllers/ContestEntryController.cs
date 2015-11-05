using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Parse;

namespace Abruple.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using BaseControllers;
    using Data.Contracts;

    using AutoMapper;

    using Abruple.Models;
    using Abruple.Models.Enums;
    using Models;
    using Models.BindingModels.ContestEntry;
    using Models.ViewModels.ContestEntry;

    [Authorize]
    public class ContestEntryController : BaseController
    {

        #region Ctors
        public ContestEntryController(IPhotoMasterData data)
            : base(data)
        {
        }

        public ContestEntryController(IPhotoMasterData data, User user)
            : base(data, user)
        {
        }
        #endregion

        // CREATE CONTEST ENTRY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewEntry(ModelWrapper m)
        {
            if (m.NewContestEntryBindingModel.ImageInputFile == null || m.NewContestEntryBindingModel.ImageInputFile.ContentLength < 1)
            {
                return View("Error");
            }
            string[] formats = { ".jpg", ".png", ".gif", ".jpeg" };

            if (!formats.Any(f => m.NewContestEntryBindingModel.ImageInputFile.FileName
                .EndsWith(f, StringComparison.OrdinalIgnoreCase)))
            {
                return Content("File is in different format!");
            }

            var userId = User.Identity.GetUserId();

            var imgUrl = UploadFile(m.NewContestEntryBindingModel);

            var entry = new ContestEntry
            {
                Title = m.NewContestEntryBindingModel.Title,
                ContestId = m.NewContestEntryBindingModel.ContestId,
                PhotoUrl = imgUrl.Result,
                AuthorId = userId,
                Upploaded = DateTime.Now
            };

           
            this.Data.ContestEntries.Add(entry);

            this.Data.SaveChanges();

            return RedirectToAction("Details","Contest",new {id = entry.ContestId});
        }


        // GET: GET ContestEntry (not Pending, not Deleted) by id
        public ActionResult Details(int id)
        {
            var entry = this.GetContestEntry(id);

            if (entry == null)
            {
                return this.HttpNotFound();
            }
            return View(Mapper.Map<ContestEntry, ContestEntryConciseViewModel>(entry));
        }

        //Vote is Called by ContestEntry Details View -> button VOTE
        //check if user chan VOTE and create Vote
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Vote(int id)
        {
            var entry = this.GetContestEntry(id);

            if (entry == null)
            {
                return this.HttpNotFound();
            }

            ////chek if Contest has to be closed
            //if (this.IfkDeadlineRiched(entry.ContestId))
            //{

            //   entry.Contest.State = ContestState.Closed;

            //    this.Data.SaveChanges();

            //    return RedirectToAction("Winners", "Contest", new { closedContest = entry.Contest });
            //}


            bool votable = false;


            if (entry.Contest.VotingStrategy == EntryType.Open)
            {
                votable = true;
            }
            else if (entry.Contest.Committee.Contains(this.UserProfile))
            {
                votable = true;
            }

            if (votable)
            {
                if (this.UserProfile != null & this.CheckMultipleUserVoting(entry.Id))
                {
                    this.ViewBag.Msg = "already voted";
                    return RedirectToAction("Details", "Contest", new { id = entry.ContestId });
                }

                this.Data.Votes.Add(new Vote()
                {
                    AuthorId = UserProfile.Id,
                    ContestEntryId = entry.Id,
                });
                this.Data.SaveChanges();

                this.ViewBag.Msg = "+1 vote added";
                return RedirectToAction("Details", "Contest", new { id = entry.ContestId });
            }
            else
            {
                this.ViewBag.Msg = "Not Allowed to vote";
                return RedirectToAction("Details", "Contest", new { id = entry.ContestId });
            }
            return null;
        }

        //return 1 ContestEntry not (Pending, not Deleted)
        [NonAction]
        private ContestEntry GetContestEntry(int id)
        {
            var entry = this.Data.ContestEntries
                .All()
                .Where(ce => ce.State == ContestEntryState.Acceppted)
                .FirstOrDefault(ce => ce.Id == id);

            return (entry);
        }

        //Checks if logned user is voted for current ContestEntry
        [NonAction]
        private bool CheckMultipleUserVoting(int id)
        {
            return this.Data.Votes.All().Any(v => v.AuthorId == UserProfile.Id && v.ContestEntry.Id == id);
        }

        [NonAction]
        private bool IfkDeadlineRiched(int id)
        {
            var contest = this.Data.Contests.All().FirstOrDefault(c => c.Id == id);

            if ((contest.DeadlineStrategy == DeadlineStrategy.ByParticipants)
                && (contest.Participants.Count >= contest.ParticipantCount))
            {
                return true;
            }

            
            if (DateTime.Now >= contest.EndDate)
            {
                return true;
            }

            return false;
        }

        [NonAction]
        public async Task<string> UploadFile(NewContestEntryBindingModel m)
        {
            byte[] buffer;

            using (var reader = new BinaryReader(m.ImageInputFile.InputStream))
            {
                buffer = reader.ReadBytes(m.ImageInputFile.ContentLength);
            }

            var file = new ParseFile(m.ImageInputFile.FileName, buffer);
            await file.SaveAsync().ConfigureAwait(false);

            var jobApplication = new ParseObject("ContestEntries");

            jobApplication["ContestId"] = "";
            jobApplication["AuthorId"] = "";
            jobApplication["Picture"] = file;

            await jobApplication.SaveAsync().ConfigureAwait(false);

            return file.Url.ToString();
        }
    }
}