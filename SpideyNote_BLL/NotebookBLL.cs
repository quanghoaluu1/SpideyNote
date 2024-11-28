using SpideyNote_DAL;
using SpideyNote_DTO;

namespace SpideyNote_BLL;

public class NotebookBLL
{
    private NotebookDAO _notebookDAO = new NotebookDAO();
    
    public bool CreateNewNotebook(string userId, string title)
    {
        Notebook existedNotebook = _notebookDAO.FindNotebookByUserIdAndTitle(userId, title);
        if (existedNotebook != null)
        {
            return false;
        }

        Notebook newNotebook = new Notebook
        {
            UserId = userId,
            Title = title,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _notebookDAO.InsertNotebook(newNotebook);
        return true;
    } 
}