namespace ToDoList.Services
{
    public static class GetDB
    {
        public static Service Service()
        {
            return StoreXML.GetStorage() ? new SQLService() : new XMLService();
        }
    }
}
