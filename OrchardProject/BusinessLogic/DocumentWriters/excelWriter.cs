using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using OrchardProject.Models;

namespace OrchardProject.BusinessLogic.DocumentWriters.PDFWriter
{
    class ExcelWriter
    {
        Workbook _workBook;

        public void CreateExcelDoc(List<InventoryItem> newItems, string path)
        {
            //System.Data.DataTable table = _createDataTable(newItems);
            //excelApp = new Excel.
            //_workBook.
        }

        private System.Data.DataTable _createDataTable(List<InventoryItem> newItems)
        {
            System.Data.DataTable newTable = new System.Data.DataTable("newItems");
            newTable.Columns.Add("Chemical", typeof(string));
            newTable.Columns.Add("Quantity", typeof(double));
            newTable.Columns.Add("Measurement", typeof(string));
            newTable.Columns.Add("SMWConventional", typeof(double));
            newTable.Columns.Add("SMWOrganic", typeof(double));
            newTable.Columns.Add("KTWConventional", typeof(double));
            newTable.Columns.Add("KTWOrganic", typeof(double));
            newTable.Columns.Add("TKM", typeof(double));
            newTable.Columns.Add("Date", typeof(DateTime));

            foreach (InventoryItem item in newItems)
            {
                DataRow newRow = newTable.NewRow();
                newRow[0] = item.Chemical.Name;
                newRow[1] = item.QuantityTotal;
                newRow[2] = item.Chemical.QuantityType;
                newRow[3] = item.QuantitySMWConventional;
                newRow[4] = item.QuantitySMWOrganic;
                newRow[5] = item.QuantityKTWConventional;
                newRow[6] = item.QuantityKTWOrganic;
                newRow[7] = item.QuantityTKM;
                newRow[8] = item.DateTaken;
                newTable.Rows.Add(newRow);
            }

            return newTable;
        }
    }
}