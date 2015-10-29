namespace Abruple.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Migrations;
    using Models;

    public class PhotoMasterDbContext : IdentityDbContext<User>
    {
        public PhotoMasterDbContext()
            : base("PhotoMasterDbContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoMasterDbContext, Configuration>());
        }

        // CREATE
        public static PhotoMasterDbContext Create()
        {
            return new PhotoMasterDbContext();
        }

        //DB SETS
        public IDbSet<Contest> Contests { get; set; }
        public IDbSet<ContestEntry> ContestEntries { get; set; }
        public IDbSet<Vote> Votes { get; set; }
        public IDbSet<Prize> Prizes { get; set; }
        public IDbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Contest>().HasMany(c => c.ContestEntries).WithRequired(ce => ce.Contest).WillCascadeOnDelete(false);
            //    modelBuilder.Entity<Contest>().HasMany(c => c.Winners).WithRequired(w => w.WonContest).WillCascadeOnDelete(false);
            modelBuilder.Entity<ContestEntry>().HasOptional(ce => ce.WonContest);
            modelBuilder.Entity<User>().HasMany(u => u.ContestsCreated).WithRequired(c => c.Creator).WillCascadeOnDelete(false);

            modelBuilder.Entity<Contest>()
                  .HasMany<User>(c => c.Participants)
                  .WithMany(p => p.ContestsParticipated)
                  .Map(pc =>
                  {
                      pc.MapLeftKey("ContestId");
                      pc.MapRightKey("UserId");
                      pc.ToTable("ContestsParticipants");
                  });

            modelBuilder.Entity<Contest>()
                 .HasMany<User>(c => c.AllowedParticipants)
                 .WithMany(p => p.AllowedParticipation)
                 .Map(pc =>
                 {
                     pc.MapLeftKey("ContestId");
                     pc.MapRightKey("UserId");
                     pc.ToTable("AllowedContestsParticipations");
                 });

            modelBuilder.Entity<Contest>()
                .HasMany<User>(c => c.Committee)
                .WithMany(p => p.AllowedVoting)
                .Map(pc =>
                {
                    pc.MapLeftKey("ContestId");
                    pc.MapRightKey("UserId");
                    pc.ToTable("AllowedContestsVoting");
                });


            base.OnModelCreating(modelBuilder);
        }
    }
}