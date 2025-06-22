namespace ToDoList.Services
{
    
    public interface IToDoServiceFactory
    {
        Service GetService();
    }
    
    public class ToDoServiceFactory : IToDoServiceFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ToDoServiceFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Service GetService()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var storageType = httpContext?.Request.Headers["Storage-Type"].ToString().ToLower();
            return storageType switch
            {
                "sql" => new SQLService(),
                "xml" => new XMLService(),
                _ => StoreXML.GetStorage() ? new SQLService() : new XMLService()
            };
        }
    }
}
