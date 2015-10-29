namespace Abruple.App.Controllers
{

    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Linq;
    
    using Abruple.Models;
    using Data.Contracts;

    public abstract class BaseController: Controller
    {
        protected BaseController(IPhotoMasterData data)
        {
            this.Data = data;
        }

        public BaseController(IPhotoMasterData data, User user)
            : this(data)
        {

            this.UserProfile = user;
        }

        public IPhotoMasterData Data { get; private set; }
        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
       
    }
}