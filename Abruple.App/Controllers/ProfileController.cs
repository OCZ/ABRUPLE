﻿using Abruple.App.Models;
using Microsoft.AspNet.Identity;

namespace Abruple.App.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BaseControllers;
    using Data.Contracts;
    using Models.ViewModels.User;

    [Authorize]
    public class ProfileController : BaseController
    {

        #region Ctors
        public ProfileController(IPhotoMasterData data)
            : base(data)
        {
        }

        public ProfileController(IPhotoMasterData data, User user)
            : base(data, user)
        {
        }
        #endregion

        // GET: User Profile => Profile/Show
        public ActionResult Details(string username)
        {
            var user = this.UserProfile;

            if (username != null)
            {
                user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            }


            if (user == null)
            {
                return this.HttpNotFound();
            }

           var result = Mapper.Map<User, UserPersonalDataViewModel>(user);

            var output = new ModelWrapper {UserPersonalDataViewModel = result};

            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ModelWrapper m)
        {
            var userId = User.Identity.GetUserId();

            //var model = m.EditProfileBindingModel;

            var profile = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);
            //TODO: Mapping

            return Content("");
        }

    }
}