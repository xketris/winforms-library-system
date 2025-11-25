using LibraryApp.Data;
using LibraryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryApp.Models
{
    public class LoginModel : IDisposable
    {
        private readonly LibraryContext _context;
        public LoginModel()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>().UseSqlite("Data Source=Library.db").Options;
            _context = new LibraryContext(options);
        }

        public LoginResult Login(string email, string password)
        {
            var hash = HashPassword(password);
            var user = _context.Users.Include(u => u.Address).FirstOrDefault(u => u.Email == email && u.PasswordHash == hash);

            if (user == null) return new LoginResult { Success = false, ErrorMessage = "Invalid credentials" };
            if (user.IsBanned) return new LoginResult { Success = false, ErrorMessage = "Account banned" };

            UserSession.Login(user);
            return new LoginResult { Success = true, User = user, IsAdmin = user.IsAdmin };
        }

        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "salt"));
                return Convert.ToBase64String(bytes);
            }
        }
        public void Dispose() => _context?.Dispose();
    }

    public class LoginResult { public bool Success; public string ErrorMessage; public User User; public bool IsAdmin; }
}