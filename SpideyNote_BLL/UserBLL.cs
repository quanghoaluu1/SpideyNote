using SpideyNote_DAL;
using SpideyNote_DTO;

namespace SpideyNote_BLL;

public class UserBLL
{
    public bool Register(string email, string username, string password)
    {
        User existedUsername = UserDAO.Instance.FindUserByUsername(username);
        User existedEmail = UserDAO.Instance.FindUserByEmail(email);
        if (existedUsername != null || existedEmail != null)
        {
            return false;
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        User newUser = new User
        {
            Username = username,
            Email = email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        UserDAO.Instance.InsertUser(newUser);
        return true;
    }

    public bool Login(string username, string password)
    {
        User user = UserDAO.Instance.FindUserByUsername(username);
        if (user == null)
        {
            return false;
        }
        string hashedPassword = user.PasswordHash;
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}