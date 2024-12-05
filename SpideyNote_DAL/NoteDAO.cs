using MongoDB.Driver;
using SpideyNote_DTO;

namespace SpideyNote_DAL;

public class NoteDAO
{
    private MongoDbContext _dbContext;
    private IMongoCollection<Note> _noteCollection;
    
    public NoteDAO()
    {
        _dbContext = new MongoDbContext("mongodb://localhost:27017", "SpideyNoteDB");
        _noteCollection = _dbContext.GetCollection<Note>("notes");
    }
    public void InsertNote(Note note)
    {
        _noteCollection.InsertOne(note);
    }
    
    public List<Note> FindNotesByNotebookId(string notebookId)
    {
        return _noteCollection.Find(n => n.NotebookId == notebookId).ToList();
    }

    public Note FindNoteById(string note)
    {
        return _noteCollection.Find(n => n.Id == note).FirstOrDefault();
    }


    public List<Note> FindNotesByUserId(string userId)
    {
        return _noteCollection.Find(n => n.UserId == userId).ToList();
    }
    
    public void UpdateNoteTitle(string noteId, string title)
    {
        var filter = Builders<Note>.Filter.Eq(n => n.Id, noteId);
        var update = Builders<Note>.Update.Set(n => n.Title, title);
        _noteCollection.UpdateOne(filter, update);
    }
    
    public void UpdateNoteContent(string noteId, string content)
    {
        var filter = Builders<Note>.Filter.Eq(n => n.Id, noteId);
        var update = Builders<Note>.Update.Set(n => n.Content, content);
        _noteCollection.UpdateOne(filter, update);
    }

    public void UpdateUpdatedTime(string noteId, DateTime updateAt)
    {
        var filter = Builders<Note>.Filter.Eq(n => n.Id, noteId);
        var update = Builders<Note>.Update.Set(n => n.UpdatedAt, updateAt);
        _noteCollection.UpdateOne(filter, update);
    }
}