using System.Collections.Generic;

namespace OrchardProject.BusinessLogic.DocumentWriters
{
    interface IDocumentWriter
    {
        void AddRow(RowData data);
        void AddRows(List<RowData> data);
        void GenerateDocument(string path);
    }
}