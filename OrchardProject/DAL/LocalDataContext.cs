using System.Data.Entity;
using OrchardProject.Models;
using SQLite.CodeFirst;

namespace OrchardProject.DAL
{
    class LocalDataContext : DbContext, IOrchardContext
    {
        public LocalDataContext() : base("OrchardContextOffline")
        {
        }

        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<LocalDataContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
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