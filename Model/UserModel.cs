using LibraryApp.Data;
using LibraryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models
{
    public class UserModel : IDisposable
    {
        private readonly LibraryContext _context;

        public UserModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=Library.db");
            _context = new LibraryContext(optionsBuilder.Options);
        }

        public UserUpdateResult UpdateUserData(string email, string phone, string province, string city, string street, string houseNum, string aptNum, string zip1, string zip2)
        {
            try
            {
                if (!UserSession.IsLoggedIn)
                {
                    return new UserUpdateResult { Success = false, ErrorMessage = "User not logged in" };
                }

                var userId = UserSession.UserId!.Value;

                // Pobieramy użytkownika z bazy wraz z adresem
                var user = _context.Users
                    .Include(u => u.Address)
                    .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return new UserUpdateResult { Success = false, ErrorMessage = "User not found in DB" };
                }

                // Aktualizacja danych podstawowych
                user.Email = email;
                user.PhoneNumber = phone;

                // Aktualizacja lub tworzenie adresu
                if (user.Address == null)
                {
                    user.Address = new Address { UserId = userId };
                    _context.Addresses.Add(user.Address); // Ważne: Dodajemy nowy adres do śledzenia
                }

                user.Address.Province = province;
                user.Address.City = city;
                user.Address.Street = street;
                user.Address.HouseNumber = houseNum;
                user.Address.ApartmentNumber = aptNum;
                user.Address.ZipCode = $"{zip1}-{zip2}";

                // Zapis do bazy
                _context.SaveChanges();

                // Aktualizacja sesji, aby UI od razu widziało zmiany
                UserSession.UpdateCurrentUser(user);

                return new UserUpdateResult { Success = true };
            }
            catch (Exception ex)
            {
                return new UserUpdateResult { Success = false, ErrorMessage = "Update error: " + ex.Message };
            }
        }

        // Pobieranie danych z sesji (bezpieczne na null)
        public string GetEmail() => UserSession.CurrentUser?.Email ?? "";
        public string GetPhone() => UserSession.CurrentUser?.PhoneNumber ?? "";
        public string GetProvince() => UserSession.CurrentUser?.Address?.Province ?? "";
        public string GetCity() => UserSession.CurrentUser?.Address?.City ?? "";
        public string GetStreet() => UserSession.CurrentUser?.Address?.Street ?? "";
        public string GetHouseNum() => UserSession.CurrentUser?.Address?.HouseNumber ?? "";
        public string GetApartmentNum() => UserSession.CurrentUser?.Address?.ApartmentNumber ?? "";

        public string GetZipCodePart1()
        {
            var zip = UserSession.CurrentUser?.Address?.ZipCode ?? "";
            return zip.Split('-').FirstOrDefault() ?? "";
        }

        public string GetZipCodePart2()
        {
            var zip = UserSession.CurrentUser?.Address?.ZipCode ?? "";
            var parts = zip.Split('-');
            return parts.Length > 1 ? parts[1] : "";
        }

        public string GetUserFullName() => UserSession.UserFullName ?? "Guest";
        public decimal GetUserBalance() => UserSession.CurrentUser?.Balance ?? 0;

        public List<BorrowedBookInfo> GetBorrowedBooks()
        {
            // (Kod bez zmian - pobieranie wypożyczeń)
            if (!UserSession.IsLoggedIn) return new List<BorrowedBookInfo>();
            var userId = UserSession.UserId.Value;
            return _context.Loans
                .Where(l => l.UserId == userId)
                .Include(l => l.Book).ThenInclude(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Select(l => new BorrowedBookInfo
                {
                    BookTitle = l.Book.Title,
                    Authors = string.Join(", ", l.Book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}")),
                    ReturnDate = l.DueDate
                }).ToList();
        }

        public List<FavoriteBookInfo> GetFavoriteBooks() => new List<FavoriteBookInfo>(); // Placeholder

        public void Dispose() => _context?.Dispose();
    }
}