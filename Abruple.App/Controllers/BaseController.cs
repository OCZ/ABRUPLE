namespace Abruple.App.Controllers
{
    using Data;
    using Data.Contracts;

    public abstract class BaseController
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