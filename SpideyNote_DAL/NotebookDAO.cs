using MongoDB.Driver;
using SpideyNote_DTO;

namespace SpideyNote_DAL;

public class NotebookDAO
{
    private MongoDbContext _dbContext;
    private IMongoCollection<Notebook> _notebookCollection;
    
    public NotebookDAO()
    {
        _dbContext = new MongoDbContext("mongodb://localhost:27017", "SpideyNoteDB");
        _notebookCollection = _dbContext.GetCollection<Notebook>("notebooks");
    }
    
    public List<Notebook> FindNotebooksByUserId(string userId)
    {
        return _notebookCollection.Find(n => n.UserId == userId).ToList();
    }
    
    public Notebook FindNotebookById(string id)
    {
        return _notebookCollection.Find(n => n.Id == id).FirstOrDefault();
    }
    
    public Notebook FindNotebookByUserIdAndTitle(string userId, string title)
    {
        return _notebookCollection.Find(n => n.UserId == userId && n.Title == title).FirstOrDefault();
    }
    
    public void InsertNotebook(Notebook notebook)
    {
        _notebookCollection.InsertOne(notebook);
    }
}