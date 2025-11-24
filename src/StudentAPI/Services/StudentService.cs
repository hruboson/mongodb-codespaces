using MongoDB.Driver;
using Microsoft.Extensions.Options;

using StudentApi.Models;

namespace StudentApi.Services;

public class StudentService
{
    private readonly IMongoCollection<Student> collection;

    public StudentService(IOptions<StudentDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var db = client.GetDatabase(settings.Value.DatabaseName);
        collection = db.GetCollection<Student>(settings.Value.CollectionName);
    }

    public async Task<List<Student>> GetAsync() => 
        await collection.Find(_ => true).ToListAsync();
    
    public async Task<Student> GetAsync(string id) => 
        await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Student s) =>
        await collection.InsertOneAsync(s);
    
    public async Task UpdateAsync(string id, Student s) => 
        await collection.ReplaceOneAsync(x => x.Id == id, s);
    
    public async Task RemoveAsync(string id) =>
        await collection.DeleteOneAsync(x => x.Id == id);

    /*public async Task CreateMany(Student[] s) =>
        await collection.InsertManyAsync();*/
}