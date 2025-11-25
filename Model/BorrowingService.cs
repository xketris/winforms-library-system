using LibraryApp.Data;
using LibraryApp.Models.Entities;
using LibraryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Services
{
    public class BorrowingService : IDisposable
    {
        private readonly LibraryContext _context;

        public BorrowingService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=Library.db");
            _context = new LibraryContext(optionsBuilder.Options);
        }

        public ServiceResult BorrowBook(int bookId, int userId)
        {
            try
            {
                var book = _context.Books.Include(b => b.Loans).FirstOrDefault(b => b.Id == bookId);
                if (book == null) return new ServiceResult { Success = false, ErrorMessage = "Book not found" };
                if (book.Status != BookStatus.Available) return new ServiceResult { Success = false, ErrorMessage = "Book not available" };
                if (book.Loans.Any()) return new ServiceResult { Success = false, ErrorMessage = "Book already borrowed" };

                var user = _context.Users.Find(userId);
                if (user == null || user.IsBanned) return new ServiceResult { Success = false, ErrorMessage = "User invalid or banned" };

                var loan = new Loan
                {
                    BookId = bookId,
                    UserId = userId,
                    LoanDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14)
                };

                _context.Loans.Add(loan);
                book.Status = BookStatus.Borrowed;
                _context.SaveChanges();

                return new ServiceResult { Success = true, Message = "Book borrowed successfully" };
            }
            catch (Exception ex) { return new ServiceResult { Success = false, ErrorMessage = ex.Message }; }
        }

        public ServiceResult ReturnBook(int bookId, int userId)
        {
            try
            {
                var loan = _context.Loans.Include(l => l.Book).FirstOrDefault(l => l.BookId == bookId && l.UserId == userId);
                if (loan == null) return new ServiceResult { Success = false, ErrorMessage = "Loan not found" };

                _context.Loans.Remove(loan);
                loan.Book.Status = BookStatus.Available;
                _context.SaveChanges();

                return new ServiceResult { Success = true, Message = "Book returned" };
            }
            catch (Exception ex) { return new ServiceResult { Success = false, ErrorMessage = ex.Message }; }
        }

        public void Dispose() => _context?.Dispose();
    }

    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}