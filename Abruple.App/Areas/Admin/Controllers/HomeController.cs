namespace Abruple.App.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Abruple.Models;
    using Abruple.Models.Enums;
    using App.Controllers.BaseControllers;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using Models.ViewModels.ContestEntry;

    public class HomeController : BaseController
    {

#region Ctors
        public HomeController(IPhotoMasterData data) : base(data)
        {
        }

        public HomeController(IPhotoMasterData data, User user) : base(data, user)
        {
        }
#endregion


        // ADMIN HOME
        public ActionResult Index()
        {
            var entries = this.Data.ContestEntries.All()
                .Where(e => e.State == ContestEntryState.Pending)
                .OrderByDescending(e => e.Upploaded)
                .ProjectTo<ContestEntryShortViewModel>();

            return this.View();
        }
    }
}