using LibraryApp.Data;
using LibraryApp.Models.Entities;
using LibraryApp.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryApp.Models
{
    public class RegisterModel : IDisposable
    {
        private readonly LibraryContext _context;

        public RegisterModel()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>().UseSqlite("Data Source=Library.db").Options;
            _context = new LibraryContext(options);
        }

        public ServiceResult Register(
            string firstName,
            string lastName,
            string password,
            string email,
            string phone,
            DateTime birthDate,
            string province, // Dodano parametr
            string city,
            string street,
            string houseNum,
            string aptNum,
            string zip)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == email))
                    return new ServiceResult { Success = false, ErrorMessage = "User with this email already exists." };

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone,
                    BirthDate = birthDate,
                    PasswordHash = HashPassword(password),
                    Balance = 0,
                    IsAdmin = false,
                    IsBanned = false,
                    Address = new Address
                    {
                        Province = province, // Zapisujemy województwo
                        City = city,
                        Street = street,
                        HouseNumber = houseNum,
                        ApartmentNumber = aptNum,
                        ZipCode = zip
                    }
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Automatyczne logowanie po rejestracji
                UserSession.Login(user);

                return new ServiceResult { Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Success = false, ErrorMessage = ex.Message };
            }
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
}