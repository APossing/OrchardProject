using System;

namespace OrchardProject.Models
{
    public class InventoryItem
    {
        public int InventoryItemID { get; set; }
        public DateTime DateTaken { get; set; }
        public virtual Chemical Chemical { get; set; }
        public double QuantityTotal { get; set; }
        public double QuantitySMWConventional { get; set; }
        public double QuantitySMWOrganic { get; set; }
        public double QuantityKTWConventional { get; set; }
        public double QuantityKTWOrganic { get; set; }
        public double QuantityTKM { get; set; }

        public InventoryItem()
        {
            InventoryItemID = -1;
            DateTaken = new DateTime();
            Chemical = new Chemical();
        }
    }
}