namespace Abruple.Data.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public interface IPhotoMasterData
    {
        // TODO: ADD EACH DATABASE ENTITY

        IRepository<User> Users { get; } // NOT SURE IF IS REQUIRED

        IRepository<IdentityRole> UserRoles { get; } // NOT SURE IF IS REQUIRED

        // SAVE
        int SaveChanges();
    }
}
