namespace OrchardProject.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class
        Configuration : DbMigrationsConfiguration<DAL.OrchardContext> // DbMigrationsConfiguration<OrchardProject.DAL.OrchardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DAL.OrchardContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}