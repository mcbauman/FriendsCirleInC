using BCrypt.Net;

namespace CSFriendCircle.Security;

public class PasswordHandler
{
    public string HashPassword(string pw)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pw, SaltRevision.Revision2A);
        return hashedPassword;
    }

    public bool PasswordMatch(string pw, string hashedPW)
    {
        bool passwordValid = BCrypt.Net.BCrypt.Verify(pw, hashedPW);
        return passwordValid;
    }
}