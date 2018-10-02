using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OrchardProject.Models;

namespace OrchardProject.DAL
{
    class DataAccessLayer
    {
        IOrchardContext OrchardContext;

        private bool _onlineMode;

        public DataAccessLayer(bool OnlineMode)
        {
            _onlineMode = OnlineMode;
            if (OnlineMode)
                OrchardContext = new OrchardContext();
            else
                OrchardContext = new LocalDataContext();
        }

        public bool InsertInventoryItem(InventoryItem newItem)
        {
            OrchardContext.InventoryItems.Add(newItem);
            try
            {
                OrchardContext.ISaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool InsertListMultipleInventoryItems(List<InventoryItem> newItemList)
        {
            newItemList.ForEach(c => OrchardContext.InventoryItems.Add(c));
            try
            {
                OrchardContext.ISaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool InsertChemical(Chemical newItem)
        {
            OrchardContext.Chemicals.Add(newItem);
            try
            {
                OrchardContext.ISaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool InsertListMultipleChemicals(List<Chemical> newItemList)
        {
            newItemList.ForEach(c => OrchardContext.Chemicals.Add(c));
            try
            {
                OrchardContext.ISaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Chemical[] Chemicals => OrchardContext.Chemicals.ToArray();

        public List<String> GetAllChemicalsStrings()
        {
            List<String> chemList = new List<String>();
            foreach (Chemical chem in OrchardContext.Chemicals)
            {
                chemList.Add(chem.Name);
            }

            return chemList;
        }

        public List<Models.InventoryItem> GetAllInventoryItems()
        {
            List<InventoryItem> IIList = new List<InventoryItem>();
            foreach (InventoryItem II in OrchardContext.InventoryItems)
            {
                IIList.Add(II);
            }

            return IIList;
        }


        public bool IsDataStoredLocal(string fileName)
        {
            fileName += ".xml";
            DataSet DS = new DataSet();
            try
            {
                DS.ReadXml(fileName);
                if (DS.Tables[0].Rows.Count == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public void SaveDataLocal(DataTable dataTable, string fileName)
        {
            try
            {
                fileName += ".xml";
                dataTable.WriteXml(fileName, XmlWriteMode.WriteSchema);
            }
            catch
            {
            }
        }

        public DataTable GetDataLocal(string fileName)
        {
            if (IsDataStoredLocal(fileName) == false)
            {
                return null;
            }

            try
            {
                fileName += ".xml";
                DataTable DT = new DataTable();
                DT.ReadXml(fileName);
                return DT;
            }
            catch
            {
                return null;
            }
        }
    }
}