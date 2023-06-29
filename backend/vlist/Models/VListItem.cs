using MongoDB.Bson.Serialization.Attributes;

namespace vlist.Models
{
    public class VListItem
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Created { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Updated { get; set; }

        public String AddedBy { get; set; } = string.Empty;

        public String UpdatedBy { get; set; } = String.Empty;

        public String Value { get; set; } = string.Empty;

        public Boolean IsCompleted { get; set; }
    }
}
