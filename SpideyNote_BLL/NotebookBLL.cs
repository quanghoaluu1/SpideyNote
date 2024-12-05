using SpideyNote_DAL;
using SpideyNote_DTO;

namespace SpideyNote_BLL;

public class NotebookBLL
{
    private readonly NotebookDAO _notebookDAO = new NotebookDAO();
    private readonly UserDAO _userDAO = new UserDAO();
    
    public Notebook CreateNewNotebook(string userId, string title)
    {
        Notebook newNotebook = new Notebook
        {
            UserId = userId,
            Title = title,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _notebookDAO.InsertNotebook(newNotebook);
        _userDAO.InsertNotebookIdToUser(userId, newNotebook.Id);
        return newNotebook;
    } 
    
    public void UpdateNotebookTitle(string notebookId, string title)
    {
        _notebookDAO.UpdateNotebookTitle(notebookId, title);
    }
    
    public List<Notebook> FindNotebooksByUserId(string userId)
    {
        return _notebookDAO.FindNotebooksByUserId(userId);
    }
    
    public Notebook FindNotebookById(string id)
    {
        return _notebookDAO.FindNotebookById(id);
    }
}