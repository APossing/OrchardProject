using System;
using System.Collections.Generic;

namespace OrchardProject.Models
{
    public class Order
    {
        public Order()
        {
        }

        public Order(string name, int numberOfChems, DateTime timeSubmited, List<InventoryItem> items)
        {
            this.Name = name;
            this.NumberOfChems = numberOfChems;
            this.TimeSubmited = timeSubmited;
            this.Items = items;
        }

        public string Name { get; set; }
        public int NumberOfChems { get; set; }
        public DateTime TimeSubmited { get; set; }
        public List<InventoryItem> Items { get; set; }
    }
}