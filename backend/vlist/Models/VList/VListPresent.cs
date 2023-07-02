using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace vlist.Models.VList
{
    public class VListPresent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [Required]
        public DateTime Created { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [Required]
        public DateTime Updated { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string UpdatedBy { get; set; }

        public List<VListItem> ListItems { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [Required]
        public DateTime Expiry { get; set; }

        public VListPresent(VList vList) 
        { 
            Id = vList.Id;
            Created = vList.Created;
            Updated = vList.Updated;
            Title = vList.Title;
            Description = vList.Description;
            CreatedBy = vList.CreatedBy;
            UpdatedBy = vList.UpdatedBy;
            ListItems = vList.ListItems;
            Expiry = vList.Expiry;
        }
    }
}
