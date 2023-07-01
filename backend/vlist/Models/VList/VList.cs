using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace vlist.Models.VList
{
    public class VList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Created { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Updated { get; set; }

        
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public List<VListItem> ListItems { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Expiry { get; set; }

        public VList(string title, string description, string createdBy, DateTime expiry) 
        {
            Title = title;
            Description = description;
            CreatedBy = createdBy;
            Expiry = expiry;
            ListItems = new List<VListItem>();

            Created = DateTime.UtcNow;
            Updated = Created;
            UpdatedBy = CreatedBy;
        }
    }
}
