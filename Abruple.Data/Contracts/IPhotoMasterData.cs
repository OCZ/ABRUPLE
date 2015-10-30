namespace Abruple.Data.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public interface IPhotoMasterData
    {
        IRepository<Notification> Notifications { get; }

        IRepository<Prize> Prizes { get; }

        IRepository<Vote> Votes { get; }

        IRepository<ContestEntry> ContestEntries { get; }

        IRepository<Contest> Contests { get; }
        
        IRepository<User> Users { get; }

        IRepository<IdentityRole> UserRoles { get; }

        void SaveChanges();
    }
}
