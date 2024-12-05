using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpideyNote_BLL;
using SpideyNote_DAL;
using SpideyNote_DTO;

namespace SpideyNote;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly string _username;
    private readonly UserBLL _userBll;
    private readonly NotebookBLL _notebookBll;
    private readonly NoteBLL _noteBll;
    private readonly User _user;    
    private Notebook? _selectedNotebook = null;
    private Note? _selectedNote = null;

    public MainWindow(string username)
    {
        InitializeComponent();
        _username = username;
        _userBll = new();
        _notebookBll = new();
        _noteBll = new();
        _user = _userBll.FindUserByUsername(_username);
        
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        TextBlockUsername.Text = _username;
        LoadAndDisplayYourNotes();
        NotePage.OnNoteUpdated += LoadAndDisplayYourNotes;
    }

    public void LoadAndDisplayYourNotes()
    {
        var notebooks = _notebookBll.FindNotebooksByUserId(_user.Id);
        var notes = _noteBll.FindNotesByUserId(_user.Id);
        var items = CombineNotebookAndNote(notebooks, notes);
        
        StackPanelNotebookContainer.Children.Clear();

        foreach (var item in items)
        {
            CreateItemPanel(item);
        }
        
        
    }

    private IOrderedEnumerable<dynamic> CombineNotebookAndNote(List<Notebook> notebooks, List<Note> notes)
    {
        return notebooks.Select(nb => new
        {
            nb.Id,
            nb.Title,
            nb.CreatedAt,
            nb.IsNotebook
        }).Concat(notes.Select(note => new
        {
            note.Id,
            note.Title,
            note.CreatedAt,
            note.IsNotebook
        })).OrderByDescending(item => item.CreatedAt);
    }

    private StackPanel CreateItemPanel(dynamic item)
    {
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(5)
        };
        var icon = new MaterialDesignThemes.Wpf.PackIcon
        {
            Kind = item.IsNotebook
                ? MaterialDesignThemes.Wpf.PackIconKind.Notebook
                : MaterialDesignThemes.Wpf.PackIconKind.NoteText,
            Height = 20,
            Width = 20
        };
        var textBox = new TextBox()
        {
            Tag = "TextBoxTitle",
            Cursor = Cursors.Hand,
            IsReadOnly = true,
            Text = item.Title,
            FontSize = 20,
            Margin = new Thickness(10, 0, 0, 0)
        };
        textBox.MouseDoubleClick += (sender,e ) =>
        {
            textBox.IsReadOnly = false;
            textBox.Focus();
            textBox.SelectAll();
            if (item.IsNotebook)
            {
                _selectedNotebook = _notebookBll.FindNotebookById(item.Id);
            }
            else
            {
                _selectedNote = _noteBll.FindNoteById(item.Id);
                MainFrame.Navigate(new NotePage(_selectedNote));
            }
        };

        textBox.LostFocus += (sender, e) =>
        {
            textBox.IsReadOnly = true;
            var newTitle = ((TextBox)sender).Text;
            if (item.IsNotebook) _notebookBll.UpdateNotebookTitle(item.Id, newTitle);
            else _noteBll.UpdateNoteTitle(item.Id, newTitle);
        };
        
        textBox.PreviewMouseLeftButtonDown  += (sender, e) =>
        {
            if (item.IsNotebook)
            {
                _selectedNotebook = _notebookBll.FindNotebookById(item.Id);
            }
            else
            {
                _selectedNotebook = null;
                _selectedNote = _noteBll.FindNoteById(item.Id);
                MainFrame.Navigate(new NotePage(_selectedNote));
            }
        };
        
        stackPanel.Children.Add(icon);
        stackPanel.Children.Add(textBox);
        StackPanelNotebookContainer.Children.Add(stackPanel);

        return stackPanel;
    }

    private void IconNewNote_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (_selectedNotebook != null)
        {
            MessageBox.Show(_selectedNotebook.Title);
            string userId = _user.Id;
            string title = "New Note in Notebook";
            var note = _noteBll.CreateNewNote(userId, title, _selectedNotebook.Id);
            CreateItemPanel(note);
        }
        else
        {
            MessageBox.Show("No notebook selected");
            string userId = _user.Id;
            string title = "New Note";
            var note = _noteBll.CreateNewNote(userId, title, null);
            CreateItemPanel(note);
        }
        
    }

    private void IconNewNotebook_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        string userId = _user.Id;
        string title = "New Notebook";
        var notebook = _notebookBll.CreateNewNotebook(userId, title);
        CreateItemPanel(notebook);
    }

    private void MainWindow_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) 
    {
        
    }
    
}