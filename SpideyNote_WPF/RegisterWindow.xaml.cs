using System.Windows;
using SpideyNote_BLL;

namespace SpideyNote;

public partial class RegisterWindow : Window
{
    private UserBLL _userBLL;
    public RegisterWindow()
    {
        InitializeComponent();
        _userBLL = new();
    }
    private void ButtonRegister_OnClick_OnClick(object sender, RoutedEventArgs e)
    {
        string email = TextBoxEmail.Text;
        string username = TextBoxUsername.Text;
        string password = PasswordBoxPassword.Password;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter email, username and password");
            return;
        }
        if (_userBLL.Register(email, username, password))
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Registration failed");
        }
    }

    private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        Close();
    }
}