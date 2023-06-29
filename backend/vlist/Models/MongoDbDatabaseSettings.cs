namespace vlist.Models
{
    public class MongoDbDatabaseSettings
    {
        public string? ConnectionString { get; set; } = null;
        public string? CollectionName { get; set; } = null;

        public string? DatabaseName { get; set; } = null;
    }
}
