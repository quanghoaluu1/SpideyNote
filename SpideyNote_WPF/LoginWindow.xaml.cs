using System.Windows;
using SpideyNote_BLL;

namespace SpideyNote;

public partial class LoginWindow : Window
{
    private UserBLL _userBLL;
    public LoginWindow()
    {
        InitializeComponent();
        _userBLL = new();
    }

    private void ButtonRegister_OnClick(object sender, RoutedEventArgs e)
    {
        var registerWindow = new RegisterWindow();
        registerWindow.Show();
        Close();
    }

    private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
    {
        string username = TextBoxUserName.Text;
        string password = PasswordBoxPassword.Password;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter username and password");
            return;
        }
        if (_userBLL.Login(username, password))
        {
            var mainWindow = new MainWindow(username);
            mainWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Login failed");
        }
    }
}