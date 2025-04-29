using System.Data.SqlClient;

namespace ToDoList.UseDB
{
    public class DataBase

    {
        private readonly SqlConnection connection = new(@"Data Source=DESKTOP-M9HRBRR;Initial Catalog=todolist;Integrated Security=True");

        public async Task openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
        }

        public async Task closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }

        public SqlConnection getConnection()
        {
            return connection;
        }
    }
}
