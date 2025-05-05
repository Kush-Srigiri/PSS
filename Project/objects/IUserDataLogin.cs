using System.Security.Cryptography;
using System.Text;

namespace Project.objects;

public interface IUserDataLogin
{
    /// <summary>
    /// The email of the user
    /// </summary>
    public string Email { get; }
    /// <summary>
    /// The hashed password of the user
    /// </summary>
    public virtual string PasswordHash
    {
        get { return this.PasswordHash; }
        private set { this.PasswordHash = value; }
    }
    
    /// <summary>
    /// Set a new password so that the user can reset and add a new password
    /// </summary>
    /// <param name="password">@Password</param>
    public abstract void SetPassword(string password);
    /// <summary>
    /// Checks if the given password matches the saved password
    /// </summary>
    /// <param name="password">@Password</param>
    /// <returns>@Valid</returns>
    public abstract bool ValidatePassword(string password);
    /// <summary>
    /// Hashes the given password and gives the hash back
    /// </summary>
    /// <param name="password">@Password</param>
    /// <returns>@Hash</returns>
    public static abstract string HashPassword(string password);

}