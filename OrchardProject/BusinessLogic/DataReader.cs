using System;
using System.Collections.Generic;
using OrchardProject.Models;
using System.Data;
using System.IO;
using OrchardProject.DAL;
using System.Xml.Serialization;
using System.Xml;

namespace OrchardProject.BusinessLogic
{
    class DataReader
    {
        public List<InventoryItem> ReadXML(string path)
        {
            path += ".xml";
            DataTable newTable = new DataTable();
            try
            {
                newTable.ReadXml(path);
                if (newTable.Rows.Count == 0)
                    return null;
                else
                    return tableToList(newTable);
            }
            catch
            {
                return null;
            }
        }

        private List<InventoryItem> tableToList(DataTable table)
        {
            DataAccessLayer dal = new DataAccessLayer(false);
            List<InventoryItem> iItems = new List<InventoryItem>();
            try
            {
                Chemical[] chems;
                chems = dal.Chemicals;

                foreach (DataRow row in table.Rows)
                {
                    foreach (Chemical chem in chems)
                    {
                        if (chem.Name == row[0].ToString())
                        {
                            InventoryItem newItem = new InventoryItem();
                            newItem.Chemical = chem;
                            newItem.QuantityTotal = (double) row[1];
                            newItem.QuantitySMWConventional = (double) row[2];
                            newItem.QuantitySMWOrganic = (double) row[3];
                            newItem.QuantityKTWConventional = (double) row[4];
                            newItem.QuantityKTWOrganic = (double) row[5];
                            newItem.QuantityTKM = (double) row[6];
                            newItem.DateTaken = (DateTime) row[7];
                            iItems.Add(newItem);
                            break;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            return iItems;
        }

        public List<OrderFile> getOrders()
        {
            DirectoryInfo d = new DirectoryInfo(@"..\..\LocalData\Orders");
            FileInfo[] files = d.GetFiles("*.ord");
            List<OrderFile> orders = new List<OrderFile>();

            XmlSerializer xs = new XmlSerializer(typeof(Order));
            foreach (FileInfo file in files)
            {
                string fileLocation = @"..\..\LocalData\Orders\" + file.Name;
                FileStream fs = new FileStream(fileLocation, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                Order i = (Order) xs.Deserialize(reader);
                OrderFile orderFile = new OrderFile(i, file);
                orders.Add(orderFile);
                fs.Close();
            }

            return orders;
        }
    }
}