﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpideyNote_DTO;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("username")] public string Username { get; set; }
    [BsonElement("email")] public string Email { get; set; }
    [BsonElement("passwordHash")] public string PasswordHash { get; set; }
    [BsonElement("notebooks")] public List<string> Notebooks { get; set; }
    [BsonElement("notes")] public List<string> Notes { get; set; }
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }
    [BsonElement("updatedAt")] public DateTime UpdatedAt { get; set; }
}