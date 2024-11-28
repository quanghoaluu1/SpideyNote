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

namespace SpideyNote;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly string _username;
    private UserDAO _userDao;
    public MainWindow(string username)
    {
        InitializeComponent();
        _username = username;
        _userDao = new();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        TextBlockUsername.Text = _username;
    }
}