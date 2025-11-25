using LibraryApp.Data;
using LibraryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryApp.Models
{
    public class AdminUsersModel : IDisposable
    {
        private readonly LibraryContext _context;

        public AdminUsersModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=Library.db");
            _context = new LibraryContext(optionsBuilder.Options);
        }

        public List<UserInfo> GetAllUsers()
        {
            try
            {
                var users = _context.Users
                    .Include(u => u.Address)
                    .Include(u => u.Loans)
                    .ToList();

                return users.Select(u => new UserInfo
                {
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Email = u.Email,
                    Phone = u.PhoneNumber ?? "None",
                    City = u.Address?.City ?? "None",
                    IsBanned = u.IsBanned,
                    IsAdmin = u.IsAdmin,
                    Balance = u.Balance,
                    BorrowedBooksCount = u.Loans?.Count ?? 0
                })
                    .OrderBy(u => u.FullName)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetAllUsers: {ex.Message}");
                return new List<UserInfo>();
            }
        }

        public UserInfo? GetUserById(int userId)
        {
            try
            {
                return _context.Users
                    .Include(u => u.Address)
                    .Include(u => u.Loans)
                    .Where(u => u.Id == userId)
                    .Select(u => new UserInfo
                    {
                        Id = u.Id,
                        FullName = $"{u.FirstName} {u.LastName}",
                        Email = u.Email,
                        Phone = u.PhoneNumber ?? "None",
                        City = u.Address != null ? u.Address.City : "None",
                        IsBanned = u.IsBanned,
                        IsAdmin = u.IsAdmin,
                        Balance = u.Balance,
                        BorrowedBooksCount = u.Loans.Count
                    })
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AdminActionResult ToggleBan(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null) return new AdminActionResult { Success = false, ErrorMessage = "User not found" };
                if (user.IsAdmin) return new AdminActionResult { Success = false, ErrorMessage = "Cannot ban an administrator" };

                user.IsBanned = !user.IsBanned;
                _context.SaveChanges();

                var action = user.IsBanned ? "banned" : "unbanned";
                return new AdminActionResult
                {
                    Success = true,
                    Message = $"User has been {action}",
                    UpdatedUser = GetUserById(userId)
                };
            }
            catch (Exception ex)
            {
                return new AdminActionResult { Success = false, ErrorMessage = "Error changing status: " + ex.Message };
            }
        }

        // Helper method for legacy compatibility in Presenter
        public string ToggleBanByString(string userString)
        {
            try
            {
                var allUsers = GetAllUsers();
                var selectedUser = allUsers.FirstOrDefault(u => u.ToString() == userString);

                if (selectedUser != null)
                {
                    var result = ToggleBan(selectedUser.Id);
                    if (result.Success)
                    {
                        var updatedUser = GetUserById(selectedUser.Id);
                        return updatedUser?.ToString() ?? userString;
                    }
                }
                return userString;
            }
            catch
            {
                return userString;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }
        public decimal Balance { get; set; }
        public int BorrowedBooksCount { get; set; }

        public override string ToString()
        {
            var status = IsBanned ? " [BANNED]" : "";
            var admin = IsAdmin ? " [ADMIN]" : "";
            return $"{FullName} - {Email} (Balance: {Balance:C}, Loans: {BorrowedBooksCount}){admin}{status}";
        }
    }

    public class AdminActionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public UserInfo? UpdatedUser { get; set; }
    }
}