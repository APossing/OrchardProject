using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using OrchardProject.Models;


namespace OrchardProject.DAL
{
    public class OrchardContext : DbContext, IOrchardContext
    {
        public OrchardContext() : base("OrchardContextOnline")
        {
        }

        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void ISaveChanges()
        {
            this.SaveChanges();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=blogging.db");
        //}
    }
}