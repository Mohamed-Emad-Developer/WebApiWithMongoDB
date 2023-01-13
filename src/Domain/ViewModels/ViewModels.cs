namespace Domain.ViewModels
{

    public class WebApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        //public string BooksCollectionName { get; set; }
    }

    public class ResponseModel
    {
        public ApiStatus Status { get; set; }
        public object? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

    }

    public enum ApiStatus
    {
        Failed,
        Succeeded

    }
}
