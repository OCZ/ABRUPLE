namespace Abruple.App.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using App.Controllers.BaseControllers;
    using App.Models.ViewModels.ContestEntry;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data.Contracts;
    using Models;
    using PagedList;

    public class ManageController : BaseController
    {

        #region ctors
        public ManageController(IPhotoMasterData data)
            : base(data)
        {
        }

        public ManageController(IPhotoMasterData data, User user)
            : base(data, user)
        {
        }
        #endregion

        //// GET: Admin/Manage
        //public ActionResult Contest()
        //{
        //    return View();
        //}


        public ActionResult ContestEntries(int? page, int id)
        {
            this.ViewBag.Contest = id;
            this.ViewBag.Page = page;
            var entries = this.Data.ContestEntries.All()
                .Where(e => e.ContestId == id)
                .Where(e => e.State == ContestEntryState.Pending)
                .OrderByDescending(e => e.Upploaded)
                .ProjectTo<ContestEntryShortViewModel>()
                .ToPagedList(page ?? GlobalConstants.DefaultStartPage, GlobalConstants.DefaultPageSizeCe);
            return this.View(entries);
        }

        public ActionResult Users(int? page, bool? filter)
        {
            this.ViewBag.filter = filter;
            var users = this.Data.Users.All()
                //.Where(u => u.IsBlocked == filter)
                .OrderByDescending(u => u.Registration)
                .ProjectTo<AdminUserPersonalDataViewModel>()
                .ToPagedList(page ?? GlobalConstants.DefaultStartPage, GlobalConstants.DefaultPageSizeUsers);
            return this.View(users);
        }

        [HttpGet]
        public ActionResult BlockUser(string username)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (user == null) return this.HttpNotFound();
            user.IsBlocked = true;
            this.Data.SaveChanges();
            return RedirectToAction("Users");
        }

        public ActionResult UnBlockUser(string username)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            if (user == null) return this.HttpNotFound();
            user.IsBlocked = false;
            this.Data.SaveChanges();
            return RedirectToAction("Users");

        }

        public ActionResult AcceptEntry(int id)
        {
            var entry = this.Data.ContestEntries.All().FirstOrDefault(e => e.Id == id);
            if (entry == null)
            {
                return this.HttpNotFound();
            }
            entry.State = ContestEntryState.Acceppted;

            var msg = new Notification()
            {
                Type = NotificationType.Info,
                Content = string.Format("Contest Entry #{0} has been Accepted by Administrator", entry.Id),
                RecipientId = entry.AuthorId
            };
            this.Data.Notifications.Add(msg);
            this.Data.SaveChanges();

            return RedirectToAction("ContestEntries", new { id = entry.ContestId });
        }

        public ActionResult DeleteEntry(int id)
        {

            var entry = this.Data.ContestEntries.All().FirstOrDefault(e => e.Id == id);
            if (entry == null)
            {
                return this.HttpNotFound();
            }
            entry.State = ContestEntryState.Deleted;

            var msg = new Notification()
            {
                Type = NotificationType.Info,
                Content = string.Format("Contest Entry #{0} has been DELETED by Administrator due to violation of site rules", entry.Id),
                RecipientId = entry.AuthorId
            };
            this.Data.Notifications.Add(msg);
            this.Data.SaveChanges();
            this.Data.SaveChanges();

            return RedirectToAction("ContestEntries", new { id = entry.ContestId });
        }
    }
}