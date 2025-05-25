using System.Xml.Linq;
using ToDoList.Models;

namespace ToDoList.UseDB
{
    public static class StoreXML
    {

        public static bool SetStorage(int set)
        {
            try
            {
                XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Root",
                        new XElement("Store", set)
                )
            );
                xmlDoc.Save("./store.xml");
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }
        public static bool GetStorage()
        {
            try
            {
                XDocument doc = XDocument.Load("./store.xml");
                var items = doc.Element("Root")?.Element("Store")?.Value ?? "";
                if (items == null) throw new Exception("file store error");
                if (items == "1") return true;
                else return false;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }


    }
}

//public async Task ADDTABEL()
//{
//    var res = await GetDataFromDB.GetDataList(SqlQuery.ADDTABLE_CAREGORY());
//    var res2 = await GetDataFromDB.GetDataList(SqlQuery.ADDTABLE_TODOLIST());
//}