namespace Abruple.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<PhotoMasterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        // SEED
        protected override void Seed(PhotoMasterDbContext context)
        {
            
        }
    }
}
