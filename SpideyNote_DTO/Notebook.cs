using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpideyNote_DTO;

public class Notebook
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("title")] public string Title { get; set; }
    [BsonElement("description")] public string Description { get; set; }
    [BsonElement("userId")] public string UserId { get; set; }
    
    [BsonElement("notebookParent")] public string NotebookParentId { get; set; }
    [BsonElement("notes")] public List<string> Notes { get; set; }
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }
    [BsonElement("updatedAt")] public DateTime UpdatedAt { get; set; }

    public bool IsNotebook = true;
}