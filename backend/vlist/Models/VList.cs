using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace vlist.Models
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

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Expiry { get; set; }

        public String CreatedBy { get; set; } = string.Empty;

        public VListItem ListItems { get; set; } = new VListItem();
    }
}
