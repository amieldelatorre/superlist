using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace vlist.Models.VList
{
    public class VList
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
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 8)]
        public string PassPhrase { get; set; }
        [Required]
        public List<VListItem> ListItems { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [Required]

        public DateTime Expiry { get; set; }

        public VList(string title, string description, string createdBy, DateTime expiry, string passPhrase) 
        {
            Title = title;
            Description = description;
            CreatedBy = createdBy;
            Expiry = expiry;
            PassPhrase = BCrypt.Net.BCrypt.EnhancedHashPassword(passPhrase);
            
            ListItems = new List<VListItem>();

            Created = DateTime.UtcNow;
            Updated = Created;
            UpdatedBy = CreatedBy;
        }
    }
}
