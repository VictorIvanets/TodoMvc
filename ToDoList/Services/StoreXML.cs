﻿using System.Xml.Linq;

namespace ToDoList.Services
{
    public static class StoreXML
    {
        public static bool SetStorage(int set)
        {
            try
            {
                XDocument doc = new(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Root",
                        new XElement("Store", set)
                )
            );
                doc.Save("./store.xml");
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
                var items = (doc.Element("Root")?.Element("Store")?.Value ?? "") ?? throw new Exception("file store error");
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