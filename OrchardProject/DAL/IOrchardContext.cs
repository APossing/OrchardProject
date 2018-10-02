using OrchardProject.Models;
using System.Data.Entity;


namespace OrchardProject.DAL
{
    interface IOrchardContext
    {
        DbSet<Chemical> Chemicals { get; set; }
        DbSet<InventoryItem> InventoryItems { get; set; }

        void ISaveChanges();
    }
}