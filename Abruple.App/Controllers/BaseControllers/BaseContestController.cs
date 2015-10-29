namespace Abruple.App.Controllers.BaseControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using Microsoft.AspNet.Identity;


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
        public IQueryable<Contest> GetContestsCollection(string filter )
        {
            

            var contests = this.Data.Contests.All();
         
            switch (filter)
            {
                case "active":
                    contests = contests.Where(c => c.State == ContestState.Active);
                    break;
                case "own":
                    contests = contests
                        .Where(c => c.CreatorId == this.UserProfile.Id);
                    break;
                case "past":
                    contests = contests.Where(c => c.State != ContestState.Active);
                    break;
            }

            IQueryable<Contest> result = contests.OrderByDescending(c => c.CreatedOn);
                

            return result;

        }

       
    }
}