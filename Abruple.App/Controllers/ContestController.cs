namespace Abruple.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using Abruple.Models;
    using Microsoft.AspNet.Identity;

    public class ContestController : BaseController
    {
        // GET: Contest
        
       public ActionResult Index()
       {
           var loggedUserId = this.User.Identity.GetUserId();
           var loggedUser = this.Data.Users.Find(loggedUserId);
           this.Data.Contests.Add(new Contest()
           {
               Title = "contest1",
               CreatedOn = DateTime.Now,
               Description = "first contest with basic proparties",
               Creator = loggedUser
           });

           this.Data.SaveChanges();



           return RedirectToAction("Index", "Home");
       }
    }
}