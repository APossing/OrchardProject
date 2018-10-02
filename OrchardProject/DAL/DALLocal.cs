using System.Data;

namespace OrchardProject.DAL.LocalData
{
    public class DALLocal
    {
        public bool IsDataStored(string fileName)
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

        public void SaveData(DataTable dataTable, string fileName)
        {
            try
            {
                fileName += ".xml";
                DataSet DS = new DataSet();
                DS.Tables.Add(dataTable);
                DS.WriteXml(fileName);
            }
            catch
            {
            }
        }

        public bool GetData(string fileName)
        {
            if (IsDataStored(fileName) == false)
            {
                return false;
            }

            try
            {
                fileName += ".xml";
                DataSet DS = new DataSet();
                DS.ReadXml(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }


        //public static void SaveData(object IClass, string filename)
        //{
        //    StreamWriter writer = null;
        //    try
        //    {
        //        XmlSerializer xmlSerializer = new XmlSerializer((IClass.GetType()));
        //        writer = new StreamWriter(filename);
        //        xmlSerializer.Serialize(writer, IClass);
        //    }
        //    finally
        //    {
        //        if (writer != null)
        //            writer.Close();
        //        writer = null;
        //    }
        //}

        //public static Type type;

        //public void XMLLoad(object T, string filename)
        //{
        //    type = typeof(T);
        //}
    }
}