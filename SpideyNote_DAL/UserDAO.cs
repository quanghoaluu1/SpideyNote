using MongoDB.Driver;
using SpideyNote_DTO;

namespace SpideyNote_DAL;

public class UserDAO
{
    private static MongoDbContext _dbContext;
    private static IMongoCollection<User> _userCollection ;
    private static UserDAO _instance;

    public static UserDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserDAO();
                _dbContext = new MongoDbContext("mongodb://localhost:27017", "SpideyNoteDB");
                _userCollection = _dbContext.GetCollection<User>("users");
            }

            return _instance;
        }
    }
    
    public User FindUserByUsername(string username)
    {
        return _userCollection.Find(u => u.Username == username).FirstOrDefault();
    }
    
    public User FindUserById(string id)
    {
        return _userCollection.Find(u => u.Id == id).FirstOrDefault();
    }
    
    public User FindUserByEmail(string email)
    {
        return _userCollection.Find(u => u.Email == email).FirstOrDefault();
    }
    
    public void InsertUser(User user)
    {
        _userCollection.InsertOne(user);
    }
    
    public void InsertNotebookIdToUser(string userId, string notebookId)
    {
        _userCollection.UpdateOne(u => u.Id == userId, Builders<User>.Update.Push(u => u.Notebooks, notebookId));
    }

    public void InsertNoteIdToUser(string userId, string noteId)
    {
        _userCollection.UpdateOne(u => u.Id == userId, Builders<User>.Update.Push(u => u.Notes, noteId));
    }
}