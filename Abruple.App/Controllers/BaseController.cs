namespace Abruple.App.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Data.Contracts;

    public abstract class BaseController: Controller
    {
        protected BaseController()
            : this(new PhotoMasterData())
        {
        }

        protected BaseController(IPhotoMasterData data)
        {
            this.Data = data;
        }

        protected IPhotoMasterData Data { get; private set; }
    }
}