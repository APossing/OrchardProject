using System;
using System.Collections.Generic;
using OrchardProject.Models;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace OrchardProject.BusinessLogic
{
    public class DataWriter
    {
        private Workbook _workbook;
        private Worksheet _worksheet;
        private Microsoft.Office.Interop.Excel.Application _app;
        private string pathExcel;
        private string pathXML;
        private string pathPDF;

        public DataWriter(string pathExcel = "UnnamedExcelDoc", string pathXML = "UnnamedXMLDoc",
            string pathPDF = "UnnamedPDf")
        {
            this.pathExcel = pathExcel;
            this.pathXML = pathXML + ".xml";
            this.pathPDF = pathPDF;
        }

        public void WriteOrder(OrderFile orderFile)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Order));
            TextWriter tw = new StreamWriter(@"..\..\LocalData\Orders\" + orderFile.Order.Name + ".ord");
            xs.Serialize(tw, orderFile.Order);
            tw.Close();
        }

        public bool saveToExcel(List<InventoryItem> newItems)
        {
            _app = new Microsoft.Office.Interop.Excel.Application();
            _app.Visible = true;
            _workbook = _app.Workbooks.Add();

            _worksheet = (Microsoft.Office.Interop.Excel.Worksheet) _workbook.Sheets[1];
            createHeaders();
            if (newItems.Count > 0)
                fillData(newItems.ToArray(), 3, 1, "All");

            _workbook.Sheets.Add(After: _workbook.Sheets[_workbook.Sheets.Count]);
            _worksheet = (Microsoft.Office.Interop.Excel.Worksheet) _workbook.Sheets[2];
            createHeadersSMWOrganic();
            if (newItems.Count > 0)
                fillData(newItems.ToArray(), 3, 1, "SMW");
            return true;
        }

        private void createHeadersSMWOrganic()
        {
            _worksheet.Cells[2, 1] = "Chemical";
            _worksheet.Columns[1].ColumnWidth += 15;
            _worksheet.Cells[2, 2] = "tot";
            _worksheet.Columns[2].ColumnWidth -= 5;
            _worksheet.Cells[2, 3] = "Type";

            _worksheet.Cells[1, 1] = "SMW Organic";
            _worksheet.Range[_worksheet.Cells[1, 1], _worksheet.Cells[1, 7]].Merge();
            _worksheet.Range[_worksheet.Cells[1, 1], _worksheet.Cells[1, 7]].Style.HOrizontalAlignment =
                Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            _worksheet.Cells[2, 4] = "Box";
            _worksheet.Columns[4].ColumnWidth -= 5;
            _worksheet.Cells[2, 5] = "Unit";
            _worksheet.Columns[5].ColumnWidth -= 5;
            _worksheet.Cells[2, 6] = "Tot";
            _worksheet.Columns[6].ColumnWidth -= 5;
            for (int i = 1; i <= 7; i++)
            {
                Range range = _worksheet.Range[_worksheet.Cells[1, 1], _worksheet.Cells[1, i]];
                range.BorderAround2();
                range = _worksheet.Range[_worksheet.Cells[2, 1], _worksheet.Cells[2, i]];
                range.BorderAround2();
            }

            _worksheet.Cells[2, 7] = "DateTaken";
        }

        private void createHeaders()
        {
            _worksheet.Cells[2, 1] = "Chemical";
            _worksheet.Columns[1].ColumnWidth += 15;
            _worksheet.Cells[2, 2] = "tot";
            _worksheet.Columns[2].ColumnWidth -= 5;
            _worksheet.Cells[2, 3] = "Type";

            _worksheet.Cells[1, 4] = "SMW";
            _worksheet.Range[_worksheet.Cells[1, 4], _worksheet.Cells[1, 6]].Merge();
            _worksheet.Range[_worksheet.Cells[1, 4], _worksheet.Cells[1, 6]].Style.HOrizontalAlignment =
                Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            _worksheet.Cells[2, 4] = "Box";
            _worksheet.Columns[4].ColumnWidth -= 5;
            _worksheet.Cells[2, 5] = "Unit";
            _worksheet.Columns[5].ColumnWidth -= 5;
            _worksheet.Cells[2, 6] = "Tot";
            _worksheet.Columns[6].ColumnWidth -= 5;

            _worksheet.Cells[1, 7] = "KTW";
            _worksheet.Range[_worksheet.Cells[1, 7], _worksheet.Cells[1, 9]].Merge();
            _worksheet.Range[_worksheet.Cells[1, 7], _worksheet.Cells[1, 9]].Style.HOrizontalAlignment =
                Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            _worksheet.Cells[2, 7] = "Box";
            _worksheet.Columns[7].ColumnWidth -= 5;
            _worksheet.Cells[2, 8] = "Unit";
            _worksheet.Columns[8].ColumnWidth -= 5;
            _worksheet.Cells[2, 9] = "Tot";
            _worksheet.Columns[9].ColumnWidth -= 5;

            _worksheet.Cells[1, 10] = "TKM";
            _worksheet.Range[_worksheet.Cells[1, 10], _worksheet.Cells[1, 12]].Merge();
            _worksheet.Range[_worksheet.Cells[1, 10], _worksheet.Cells[1, 12]].Style.HOrizontalAlignment =
                Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            _worksheet.Cells[2, 10] = "Box";
            _worksheet.Columns[10].ColumnWidth -= 5;
            _worksheet.Cells[2, 11] = "Unit";
            _worksheet.Columns[11].ColumnWidth -= 5;
            _worksheet.Cells[2, 12] = "Tot";
            _worksheet.Columns[12].ColumnWidth -= 5;
            for (int i = 1; i <= 13; i++)
            {
                Range range = _worksheet.Range[_worksheet.Cells[1, 1], _worksheet.Cells[1, i]];
                range.BorderAround2();
                range = _worksheet.Range[_worksheet.Cells[2, 1], _worksheet.Cells[2, i]];
                range.BorderAround2();
            }

            _worksheet.Cells[2, 13] = "DateTaken";
        }

        private void fillData(InventoryItem[] items, int startRow, int startColumn, string DataSheetType)
        {
            int currentRow = startRow;
            foreach (InventoryItem item in items)
            {
                int currentColumn = startColumn;
                int SMWBoxes = 0;
                int SMWSingles = 0;
                int KTWBoxes = 0;
                int KTWSingles = 0;
                int TKMBoxes = 0;
                int TKMSingles = 0;
                if (item.QuantitySMWConventional > 0)
                {
                    SMWBoxes = (int) (item.QuantitySMWConventional /
                                      (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit));
                    SMWSingles =
                        (int) ((item.QuantitySMWConventional %
                                (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit)) /
                               item.Chemical.QuantityInUnit);
                }

                if (item.QuantitySMWOrganic > 0)
                {
                    SMWBoxes = (int) (item.QuantitySMWOrganic /
                                      (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit));
                    SMWSingles =
                        (int) ((item.QuantitySMWOrganic % (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit)) /
                               item.Chemical.QuantityInUnit);
                }

                if (item.QuantityKTWConventional > 0)
                {
                    KTWBoxes = (int) (item.QuantityKTWConventional /
                                      (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit));
                    KTWSingles =
                        (int) ((item.QuantityKTWConventional %
                                (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit)) /
                               item.Chemical.QuantityInUnit);
                }

                if (item.QuantityKTWOrganic > 0)
                {
                    KTWBoxes = (int) (item.QuantityKTWOrganic /
                                      (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit));
                    KTWSingles =
                        (int) ((item.QuantityKTWOrganic % (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit)) /
                               item.Chemical.QuantityInUnit);
                }

                if (item.QuantityTKM > 0)
                {
                    TKMBoxes = (int) (item.QuantityTKM / (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit));
                    TKMSingles = (int) ((item.QuantityTKM % (item.Chemical.UnitsInBox * item.Chemical.QuantityInUnit)) /
                                        item.Chemical.QuantityInUnit);
                }

                _worksheet.Cells[currentRow, currentColumn] = item.Chemical.Name;
                _worksheet.Cells[currentRow, currentColumn += 1] = item.QuantityTotal;
                _worksheet.Cells[currentRow, currentColumn += 1] = item.Chemical.QuantityType;
                if (DataSheetType == "All" || DataSheetType == "SMW")
                {
                    _worksheet.Cells[currentRow, currentColumn += 1] = SMWBoxes;
                    _worksheet.Cells[currentRow, currentColumn += 1] = SMWSingles;
                    _worksheet.Cells[currentRow, currentColumn += 1] = item.QuantitySMWConventional;
                }

                if (DataSheetType == "All" || DataSheetType == "KTW")
                {
                    _worksheet.Cells[currentRow, currentColumn += 1] = KTWBoxes;
                    _worksheet.Cells[currentRow, currentColumn += 1] = KTWSingles;
                    _worksheet.Cells[currentRow, currentColumn += 1] = item.QuantityKTWConventional;
                }

                if (DataSheetType == "All" || DataSheetType == "TKM")
                {
                    _worksheet.Cells[currentRow, currentColumn += 1] = TKMBoxes;
                    _worksheet.Cells[currentRow, currentColumn += 1] = TKMSingles;
                    _worksheet.Cells[currentRow, currentColumn += 1] = item.QuantityTKM;
                }

                _worksheet.Cells[currentRow, currentColumn += 1] = item.DateTaken.ToShortDateString();

                for (int i = 0; i < (currentColumn - startColumn + 1); i++)
                {
                    Range range = _worksheet.Range[_worksheet.Cells[currentRow, startColumn],
                        _worksheet.Cells[currentRow, startColumn + i]];
                    range.BorderAround2();
                }

                currentRow++;
            }
        }

        public void XMLWriter(InventoryItem[] newItems)
        {
            System.Data.DataTable table = toDataTable(newItems);
            table.TableName = "New Inventory";
            table.WriteXml(pathXML, XmlWriteMode.WriteSchema);
        }

        public void XMLWriteOrder(Order newOrder)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Order));
            TextWriter tw = new StreamWriter(@"..\..\LocalData\Orders\" + newOrder.Name + ".ord");
            xs.Serialize(tw, newOrder);
            tw.Close();
        }

        private System.Data.DataTable toDataTable(InventoryItem[] items)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Chemical", typeof(string));
            table.Columns.Add("Quantity", typeof(double));
            table.Columns.Add("SMWQuantityConventional", typeof(double));
            table.Columns.Add("SMWQuantityOrganic", typeof(double));
            table.Columns.Add("KTWQuantityConventional", typeof(double));
            table.Columns.Add("KTWQuantityOrganic", typeof(double));
            table.Columns.Add("TKMQuantity", typeof(double));
            table.Columns.Add("DateTime", typeof(DateTime));

            foreach (InventoryItem item in items)
            {
                DataRow newRow = table.NewRow();
                newRow[0] = item.Chemical.Name;
                newRow[1] = item.QuantityTotal;
                newRow[2] = item.QuantitySMWConventional;
                newRow[3] = item.QuantitySMWOrganic;
                newRow[4] = item.QuantityKTWConventional;
                newRow[5] = item.QuantityKTWOrganic;
                newRow[6] = item.QuantityTKM;
                newRow[7] = item.DateTaken;
                table.Rows.Add(newRow);
            }

            return table;
        }
    }
}