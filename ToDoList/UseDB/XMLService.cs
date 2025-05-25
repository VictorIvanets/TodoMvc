using GreenDonut;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using ToDoList.Models;

namespace ToDoList.UseDB
{
    public class XMLService
    {
        public bool AddTask(CreateTaskModel task)
        {
            try
            {
                XDocument doc = XDocument.Load("./MyTask.xml");
                if (doc == null || doc.Root == null) throw new Exception("file MyTask error");

                doc.Root.Add(new XElement("MyTask",
                        new XAttribute("Id", new Random().Next(10000).ToString()),
                        new XElement("Task", task.Task),
                        new XElement("DataTime", task.DataTime),
                        new XElement("CategoryId", task.CategoryId),
                        new XElement("IsCompleted", (byte)(task.IsCompleted ? 1 : 0))
                    ));

                doc.Save("./MyTask.xml");

                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }

        }
        public List<TaskModel> AllTask()
        {
            List<TaskModel> dataList = new List<TaskModel>();
            try
            {
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");

                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? Enumerable.Empty<XElement>();
                foreach (var item in items)
                {
                    TaskModel oneTask = new();
                    {
                        oneTask.id = Int32.Parse(item.Attribute("Id")?.Value ?? "");
                        oneTask.MyTask = item.Element("Task")?.Value ?? "";
                        oneTask.DueDate = DateTime.Parse(item.Element("DataTime")?.Value ?? "");
                        oneTask.DueTimeSpan = DateTime.Parse(item.Element("DataTime")?.Value ?? "") - DateTime.Now;
                        oneTask.CategoryId = Int32.Parse(item.Element("CategoryId")?.Value ?? "");
                        oneTask.IsCompleted = item.Element("IsCompleted")?.Value == "1" ? true : false;
                        oneTask.CategoryName = category
                            .First(i => i.id == Int32.Parse(item.Element("CategoryId")?.Value ?? ""))
                            .CategoryName;
                    };
                    dataList.Add(oneTask);
                }
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }

            return dataList;

        }

        public TaskModel GetOne(int id)
        {
            TaskModel oneTask = new();
            try
            {
                List<TaskModel> dataList = new List<TaskModel>();
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");
                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? Enumerable.Empty<XElement>();
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString());

                {
                    oneTask.id = Int32.Parse(getOneTask.Attribute("Id")?.Value ?? "");
                    oneTask.MyTask = getOneTask.Element("Task")?.Value ?? "";
                    oneTask.DueDate = DateTime.Parse(getOneTask.Element("DataTime")?.Value ?? "");
                    oneTask.DueTimeSpan = DateTime.Parse(getOneTask.Element("DataTime")?.Value ?? "") - DateTime.Now;
                    oneTask.CategoryId = Int32.Parse(getOneTask.Element("CategoryId")?.Value ?? "");
                    oneTask.IsCompleted = getOneTask.Element("IsCompleted")?.Value == "1" ? true : false;
                    oneTask.CategoryName = category
                        .First(i => i.id == Int32.Parse(getOneTask.Element("CategoryId")?.Value ?? ""))
                        .CategoryName;
                };
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            return oneTask;
        }

        public bool IsCompleted(int id, bool isCompleted, string date)
        {
            TaskModel oneTask = new();
            try
            {
                List<TaskModel> dataList = new List<TaskModel>();
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");
                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? Enumerable.Empty<XElement>();
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString());

                if (getOneTask == null)
                {
                    throw new Exception("id error");
                }
                string isCompletedString = !isCompleted ? "0" : "1";
                getOneTask.SetElementValue("IsCompleted", isCompletedString);
                getOneTask.SetElementValue("DataTime", date);
                doc.Save("./MyTask.xml");
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }

        }

        public bool UpdateTask(int id, CreateTaskModel task)
        {
            TaskModel oneTask = new();
            try
            {
                List<TaskModel> dataList = new List<TaskModel>();
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");
                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? Enumerable.Empty<XElement>();
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString());

                if (getOneTask == null)
                {
                    throw new Exception("id error");
                }
                getOneTask.SetElementValue("IsCompleted", (byte)(task.IsCompleted ? 1 : 0));
                getOneTask.SetElementValue("DataTime", task.DataTime);
                getOneTask.SetElementValue("Task", task.Task);
                getOneTask.SetElementValue("CategoryId", task.CategoryId);
                doc.Save("./MyTask.xml");
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }


        public List<CategoryModel> GetAllCategory()
        {
            List<CategoryModel> dataList = new List<CategoryModel>();
            try
            {
                XDocument doc = XDocument.Load("./Category.xml");
                var items = doc.Element("Root")?.Elements("Category") ?? Enumerable.Empty<XElement>();

                foreach (var item in items)
                {
                    CategoryModel oneCatrgory = new();
                    {
                        oneCatrgory.id = Int32.Parse(item.Attribute("Id")?.Value ?? "");
                        oneCatrgory.CategoryName = item.Element("CategoryName")?.Value ?? "";
                    };
                    dataList.Add(oneCatrgory);
                }

            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            return dataList;
        }

        public bool DeleteTask(int id)
        {
            TaskModel oneTask = new();
            try
            {

                XDocument doc = XDocument.Load("./MyTask.xml");
                doc.Descendants("MyTask")
                    .Where(x => (string)x.Attribute("Id") == id.ToString())
                    .Remove();
                doc.Save("./MyTask.xml");

                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }


        public bool AddCategoryXML()
        {

            XDocument xmlDoc = new XDocument(
             new XDeclaration("1.0", "utf-8", null),

             (new XElement("Root",

                new XElement("Category",
                    new XAttribute("Id", "1"),
                    new XElement("CategoryName", "family")
                ),
                new XElement("Category",
                    new XAttribute("Id", "2"),
                    new XElement("CategoryName", "work")
                ),
                new XElement("Category",
                    new XAttribute("Id", "3"),
                    new XElement("CategoryName", "hobby")
                ),
                new XElement("Category",
                    new XAttribute("Id", "4"),
                    new XElement("CategoryName", "party")
                ),
                new XElement("Category",
                    new XAttribute("Id", "5"),
                    new XElement("CategoryName", "car")
                )
                )
                ));

            xmlDoc.Save("./Category.xml");

            return true;

        }
    }

}
