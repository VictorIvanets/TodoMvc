using System.Xml.Linq;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class XMLService : Service
    {
        public override int AddTask(CreateTaskModel task)
        {
            try
            {
                if (!DateTime.TryParse(task.DateTime, out DateTime result))
                    throw new Exception("DateTime is not valid! format date YYYY-MM-DDTHH:MM:SS");

                int countCategory = GetAllCategory().Count;
                if (countCategory < task.CategoryId)
                    throw new Exception($"CategoryId is not valid! Max value: {countCategory}");

                XDocument doc = XDocument.Load("./MyTask.xml");
                if (doc == null || doc.Root == null) throw new Exception("file MyTask error");

                int randomId = Int32.Parse(new Random().Next(10000).ToString());
                doc.Root.Add(new XElement("MyTask",
                        new XAttribute("Id", randomId),
                        new XElement("Task", task.Task),
                        new XElement("DateTime", task.DateTime),
                        new XElement("CategoryId", task.CategoryId),
                        new XElement("IsCompleted", (byte)(task.IsCompleted ? 1 : 0))
                    ));

                doc.Save("./MyTask.xml");

                return randomId;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }
        public override List<TaskModel> AllTask()
        {
            List<TaskModel> dataList = [];
            try
            {
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");

                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? [];
                foreach (var item in items)
                {
                    TaskModel oneTask = new();
                    {
                        oneTask.Id = Int32.Parse(item.Attribute("Id")?.Value ?? "");
                        oneTask.MyTask = item.Element("Task")?.Value ?? "";
                        oneTask.DueDate = DateTime.Parse(item.Element("DateTime")?.Value ?? "");
                        oneTask.DueTimeSpan = DateTime.Parse(item.Element("DateTime")?.Value ?? "") - DateTime.Now;
                        oneTask.CategoryId = Int32.Parse(item.Element("CategoryId")?.Value ?? "");
                        oneTask.IsCompleted = item.Element("IsCompleted")?.Value == "1" ? true : false;
                        oneTask.CategoryName = category
                            .First(i => i.Id == Int32.Parse(item.Element("CategoryId")?.Value ?? ""))
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

        public override TaskModel GetOne(int id)
        {
            TaskModel oneTask = new();
            try
            {
                List<TaskModel> dataList = [];
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");
                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? [];
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString()) ?? throw new Exception("Error! Id not found");

                {
                    oneTask.Id = Int32.Parse(getOneTask?.Attribute("Id")?.Value ?? "");
                    oneTask.MyTask = getOneTask?.Element("Task")?.Value ?? "";
                    oneTask.DueDate = DateTime.Parse(getOneTask?.Element("DateTime")?.Value ?? "");
                    oneTask.DueTimeSpan = DateTime.Parse(getOneTask?.Element("DateTime")?.Value ?? "") - DateTime.Now;
                    oneTask.CategoryId = Int32.Parse(getOneTask?.Element("CategoryId")?.Value ?? "");
                    oneTask.IsCompleted = getOneTask?.Element("IsCompleted")?.Value == "1" ? true : false;
                    oneTask.CategoryName = category
                        .First(i => i.Id == Int32.Parse(getOneTask?.Element("CategoryId")?.Value ?? ""))
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

        public override bool IsCompleted(int id, string date)
        {
            TaskModel oneTask = new();
            try
            {
                List<TaskModel> dataList = [];
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");
                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? [];
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString()) ?? throw new Exception("Error! Id not found");

                bool isCompl = getOneTask.Element("IsCompleted")?.Value == "1" ? true : false;

                getOneTask.SetElementValue("IsCompleted", !isCompl ? "1" : "0");
                getOneTask.SetElementValue("DateTime", date);
                doc.Save("./MyTask.xml");
                return !isCompl;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }

        }

        public override int UpdateTask(int id, CreateTaskModel task)
        {
            TaskModel oneTask = new();
            try
            {
                List<TaskModel> dataList = [];
                List<CategoryModel> category = GetAllCategory();
                if (category.Count == 0) throw new Exception("Category is empty");
                XDocument doc = XDocument.Load("./MyTask.xml");
                var items = doc.Element("Root")?.Elements("MyTask") ?? [];
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString()) ?? throw new Exception("Error! Id not found");
                getOneTask.SetElementValue("IsCompleted", (byte)(task.IsCompleted ? 1 : 0));
                getOneTask.SetElementValue("DateTime", task.DateTime);
                getOneTask.SetElementValue("Task", task.Task);
                getOneTask.SetElementValue("CategoryId", task.CategoryId);
                doc.Save("./MyTask.xml");
                return id;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }


        public override List<CategoryModel> GetAllCategory()
        {
            List<CategoryModel> dataList = [];
            try
            {
                XDocument doc = XDocument.Load("./Category.xml");
                var items = doc.Element("Root")?.Elements("Category") ?? [];

                foreach (var item in items)
                {
                    CategoryModel oneCatrgory = new();
                    {
                        oneCatrgory.Id = Int32.Parse(item.Attribute("Id")?.Value ?? "");
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

        public override int DeleteTask(int id)
        {
            TaskModel oneTask = new();
            try
            {
                XDocument doc = XDocument.Load("./MyTask.xml");

                var items = doc.Element("Root")?.Elements("MyTask") ?? [];
                var getOneTask = items.FirstOrDefault(i => i.Attribute("Id")?.Value == id.ToString()) ?? throw new Exception("Error! Id not found");
                doc.Descendants("MyTask")
                    .Where(x => (string)x.Attribute("Id") == id.ToString())
                    .Remove();
                doc.Save("./MyTask.xml");
                return id;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
        }


        public bool AddCategoryXML()
        {
            XDocument xmlDoc = new(
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
                )));
            xmlDoc.Save("./Category.xml");
            return true;
        }
    }
}
