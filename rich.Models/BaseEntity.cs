using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace rich.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
