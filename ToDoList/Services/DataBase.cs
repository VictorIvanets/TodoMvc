using System.Data.SqlClient;

namespace ToDoList.Services
{
    public class DataBase

    {
        private readonly SqlConnection Connection = new(@"Data Source=DESKTOP-M9HRBRR;Initial Catalog=todolist;Integrated Security=True");

        public void OpenConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                 Connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                 Connection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return Connection;
        }
    }
}
