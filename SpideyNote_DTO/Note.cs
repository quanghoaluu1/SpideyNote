using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpideyNote_DTO;

public class Note
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("title")] public string Title { get; set; }
    [BsonElement("content")] public string Content { get; set; }
    [BsonElement("notebookId")] public string NotebookId { get; set; }
    [BsonElement("userId")] public string UserId { get; set; }
    [BsonElement("tags")] public List<string> Tags { get; set; }
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }
    [BsonElement("updatedAt")] public DateTime UpdatedAt { get; set; }
    public bool IsNotebook = false;
}