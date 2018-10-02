using System.IO;

namespace OrchardProject.Models
{
    public class OrderFile
    {
        public OrderFile(Order newOrder)
        {
            this.Order = new Order
            {
                Items = newOrder.Items,
                Name = newOrder.Name,
                NumberOfChems = newOrder.NumberOfChems,
                TimeSubmited = newOrder.TimeSubmited
            };
        }

        public OrderFile(Order newOrder, FileInfo fileInfo)
        {
            this.Order = new Order
            {
                Items = newOrder.Items,
                Name = newOrder.Name,
                NumberOfChems = newOrder.NumberOfChems,
                TimeSubmited = newOrder.TimeSubmited
            };
            this.FileInfo = fileInfo;
        }


        public FileInfo FileInfo { get; set; }
        public Order Order { get; set; }
    }
}