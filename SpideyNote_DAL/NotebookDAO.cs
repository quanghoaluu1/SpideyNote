using MongoDB.Driver;
using SpideyNote_DTO;

namespace SpideyNote_DAL;

public class NotebookDAO
{
    private readonly MongoDbContext _dbContext;
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
    
    public void InsertNoteIdToNotebook(string notebookId, string noteId)
    {
        _notebookCollection.UpdateOne(n => n.Id == notebookId, Builders<Notebook>.Update.Push(n => n.Notes, noteId));
    }
    
    public void UpdateNotebookTitle(string notebookId, string title)
    {
        var filter = Builders<Notebook>.Filter.Eq(n => n.Id, notebookId);
        var update = Builders<Notebook>.Update.Set(n => n.Title, title);
        _notebookCollection.UpdateOne(filter, update);
    }
}