namespace Abruple.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Migrations;
    using Models;

    public class PhotoMasterDbContext : IdentityDbContext<User>
    {
        public PhotoMasterDbContext()
            : base("PhotoMasterDbContext", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoMasterDbContext, Configuration>());
        }

        // CREATE
        public static PhotoMasterDbContext Create()
        {
            return new PhotoMasterDbContext();
        }

        public IDbSet<Contest> Contests { get; set; }
    }
}