namespace Abruple.App.Controllers.BaseControllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using Data.Contracts;


    public abstract class BaseUserController : BaseController
    {
        protected BaseUserController(IPhotoMasterData data) : base(data)
        {
        }

        protected BaseUserController(IPhotoMasterData data, User user)
            : base(data, user)
        {
        }


    }
}