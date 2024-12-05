using SpideyNote_DAL;
using SpideyNote_DTO;

namespace SpideyNote_BLL;

public class NoteBLL
{
    private NoteDAO _noteDAO = new NoteDAO();
    private UserDAO _userDAO = new UserDAO();
    private NotebookDAO _notebookDAO = new NotebookDAO();
    
    public Note CreateNewNote(string userId, string title, string notebookId)
    {
        Note newNote = new Note
        {
            UserId = userId,
            Title = title,
            NotebookId = notebookId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _noteDAO.InsertNote(newNote);
        _userDAO.InsertNoteIdToUser(userId, newNote.Id);
        if (!String.IsNullOrEmpty(notebookId))
        {
            _notebookDAO.InsertNoteIdToNotebook(notebookId, newNote.Id);
        }
        return newNote;
    }
    
    public List<Note> FindNotesByUserId(string userId)
    {
        return _noteDAO.FindNotesByUserId(userId);
    }
    
    public void UpdateNoteTitle(string noteId, string title)
    {
        _noteDAO.UpdateNoteTitle(noteId, title);
    }

    public Note FindNoteById(string noteId)
    {
        return _noteDAO.FindNoteById(noteId);
    }
    
    public void UpdateNoteContent(string noteId, string content)
    {
        _noteDAO.UpdateNoteContent(noteId, content);
    }
    
    public void UpdateUpdatedTime(string noteId, DateTime updateAt)
    {
        _noteDAO.UpdateUpdatedTime(noteId, updateAt);
    }
}