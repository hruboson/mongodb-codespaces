using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentApi.Models;

public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string FirstName { get; set; } = "";
    public string Lastname { get; set; } = "";
    public int Age { get; set; }
}