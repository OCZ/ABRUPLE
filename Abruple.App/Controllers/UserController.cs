using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abruple.App.Controllers.BaseControllers;
using Abruple.App.Models.ViewModels.User;
using Abruple.Data.Contracts;
using AutoMapper.QueryableExtensions;

namespace Abruple.App.Controllers
{
    public class UserController : BaseUserController
    {
        public UserController(IPhotoMasterData data)
            :base(data)
        {            
        }

        //GET User
        public JsonResult Search(string name)
        {

            var users = this.Data.Users.All()
                .Where(u => u.UserName.StartsWith(name.Trim()))
                .OrderBy(u => u.UserName)
                .Take(5)
                .Select(u => u.UserName);
                
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}