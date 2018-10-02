using System;
using System.Collections.Generic;
using System.Data;

namespace OrchardProject.Models
{
    public static class ExtensionMethods
    {
        public static DataTable ConvertToDataTable(this InventoryItem[] data)
        {
            throw new NotImplementedException();
        }

        public static DataTable ConvertToDataTable(this List<InventoryItem> data)
        {
            return ConvertToDataTable(data.ToArray());
        }
    }
}