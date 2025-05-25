//using System.Data.SqlClient;

//namespace ToDoList.UseDB
//{
//    public static class GetDataFromDB
//    {
//        public async static Task<List<List<string>>> GetDataList(string sqlQuery)
//        {
//            DataBase dataBase = new DataBase();
//            List<List<string>> data = new List<List<string>>();

//            try
//            {
//                await dataBase.openConnection();
//                await using SqlCommand command = new SqlCommand(sqlQuery, dataBase.getConnection());
//                await using SqlDataReader reader = await command.ExecuteReaderAsync();

//                while (await reader.ReadAsync())
//                {
//                    List<string> row = new();
//                    for (int i = 0; i < reader.FieldCount; i++)
//                    {
//                        row.Add(reader.GetValue(i).ToString());
//                    }
//                    data.Add(row);
//                }
//                await dataBase.closeConnection();
//            }
//            catch (Exception e)
//            {
//                var testError = e.Message;          
//            }
//            return data;
//        }
//    }
//}
