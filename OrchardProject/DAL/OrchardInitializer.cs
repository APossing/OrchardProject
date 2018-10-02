namespace OrchardProject.DAL
{
    public class OrchardInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrchardContext>
    {
        protected override void Seed(OrchardContext context)
        {
            //List<Chemical> chemicals = new List<Chemical>();
            //Chemical chem1 = new Chemical()
            //{

            //};
            //chemicals.Add(chem1);
            //Chemical chem2 = new Chemical()
            //{

            //};
            //chemicals.Add(chem2);

            //chemicals.ForEach(c => context.Chemicals.Add(c));
            context.SaveChanges();
            //List<InventoryItem> inventoryItems = new List<InventoryItem>();
            //InventoryItem list1 = new InventoryItem();
            //list1.Chemical = chem1;
            //list1.
        }
    }
}