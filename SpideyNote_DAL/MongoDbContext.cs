using MongoDB.Driver;

namespace SpideyNote_DAL;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private static MongoDbContext _instance;
    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    
    public static MongoDbContext GetInstance(string connectionString, string databaseName)
    {
        if (_instance == null)
        {
            _instance = new MongoDbContext(connectionString, databaseName);
        }

        return _instance;
    }
    
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}