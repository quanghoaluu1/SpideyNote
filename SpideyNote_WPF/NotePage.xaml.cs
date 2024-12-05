using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Driver.Linq;
using SpideyNote_BLL;
using SpideyNote_DTO;

namespace SpideyNote
{
    /// <summary>
    /// Interaction logic for NotePage.xaml
    /// </summary>
    public partial class NotePage : Page
    {
        private readonly Note _note;
        private NoteBLL _noteBll;
        private UserBLL _userBll;
        public static event Action OnNoteUpdated;
        public NotePage(Note note)
        {
            InitializeComponent();
            _note = note;
            _noteBll = new NoteBLL();
            _userBll = new UserBLL();
            LoadNote();
        }

        public void LoadNote()
        {
            TextBoxTitle.Text = _note.Title;
            TextBlockUpdateDate.Text = _note.UpdatedAt.ToLocalTime().ToString();
            LoadContentToRichTextBoxSafe(RichTextBoxContent, _note.Content);
        }
        
        public void LoadContentToRichTextBoxSafe(RichTextBox richTextBox, string? content)
        {
            richTextBox.Document.Blocks.Clear();

            if (string.IsNullOrEmpty(content))
            {
                // Nếu null hoặc rỗng, hiển thị một văn bản mặc định hoặc để trống
                richTextBox.Document.Blocks.Add(new Paragraph(new Run("Add Content here...")));
            }
            else
            {
                // Xử lý văn bản như trường hợp bình thường
                var paragraph = new Paragraph(new Run(content));
                richTextBox.Document.Blocks.Add(paragraph);
            }
        }
        
        public string GetContentFromRichTextBox(RichTextBox richTextBox)
        {
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            return textRange.Text;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string content = GetContentFromRichTextBox(RichTextBoxContent);
            string title = TextBoxTitle.Text;
            _noteBll.UpdateNoteContent(_note.Id, content);
            _noteBll.UpdateNoteTitle(_note.Id, title);
            _noteBll.UpdateUpdatedTime(_note.Id, DateTime.Now.ToLocalTime());
            TextBlockUpdateDate.Text = DateTime.Now.ToLocalTime().ToString();
            OnNoteUpdated?.Invoke();
        }
    }
}
