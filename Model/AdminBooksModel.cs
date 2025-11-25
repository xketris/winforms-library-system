using LibraryApp.Data;
using LibraryApp.Models.Entities;
using LibraryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryApp.Models
{
    public class AdminBooksModel : IDisposable
    {
        private readonly LibraryContext _context;
        private List<Author> _tempAuthors = new List<Author>();
        private List<Genre> _tempGenres = new List<Genre>();

        public AdminBooksModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=Library.db");
            _context = new LibraryContext(optionsBuilder.Options);
        }

        public List<BookDTO> GetAllBooks()
        {
            return _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Type = b.Type.ToString(),
                    CoverType = b.CoverType.ToString(),
                    Authors = string.Join(", ", b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}")),
                    Genres = string.Join(", ", b.BookGenres.Select(bg => bg.Genre.Name)),
                    ISBN = b.ISBN,
                    ReleaseYear = b.ReleaseYear
                }).ToList();
        }

        // --- Metody pomocnicze (listy tymczasowe) ---
        public void LoadBookToTemp(int bookId)
        {
            var book = _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                _tempAuthors = book.BookAuthors.Select(ba => ba.Author).ToList();
                _tempGenres = book.BookGenres.Select(bg => bg.Genre).ToList();
            }
            else
            {
                ClearTempAuthors();
                ClearTempGenres();
            }
        }

        public string AddAuthorToTemp(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)) return GetAuthorsString();
            var author = _context.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);
            if (author == null)
            {
                author = new Author { FirstName = firstName, LastName = lastName };
                _context.Authors.Add(author);
                _context.SaveChanges();
            }
            if (!_tempAuthors.Any(a => a.Id == author.Id)) _tempAuthors.Add(author);
            return GetAuthorsString();
        }

        public string AddGenreToTemp(string name)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Name == name);
            if (genre != null && !_tempGenres.Any(g => g.Id == genre.Id)) _tempGenres.Add(genre);
            return GetGenresString();
        }

        public string ClearTempAuthors() { _tempAuthors.Clear(); return ""; }
        public string ClearTempGenres() { _tempGenres.Clear(); return ""; }
        public string GetAuthorsString() => string.Join(", ", _tempAuthors.Select(a => $"{a.FirstName} {a.LastName}"));
        public string GetGenresString() => string.Join(", ", _tempGenres.Select(g => g.Name));
        public string[] GetGenres() => _context.Genres.Select(g => g.Name).ToArray();

        // --- Operacje CRUD ---

        public ServiceResult AddBook(string title, string type, string cover, DateTime date, string isbn, int quantity)
        {
            try
            {
                if (quantity <= 0) return new ServiceResult { Success = false, ErrorMessage = "Quantity must be at least 1." };
                if (_tempAuthors.Count == 0 || _tempGenres.Count == 0) return new ServiceResult { Success = false, ErrorMessage = "Authors and Genres are required." };

                var bookType = Enum.Parse<BookType>(type);
                var coverType = Enum.Parse<CoverType>(cover);

                for (int i = 0; i < quantity; i++)
                {
                    var book = new Book { Title = title, Type = bookType, CoverType = coverType, ReleaseYear = date.Year, ISBN = isbn, Status = BookStatus.Available };
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    foreach (var auth in _tempAuthors) _context.BookAuthors.Add(new BookAuthor { BookId = book.Id, AuthorId = auth.Id });
                    foreach (var gen in _tempGenres) _context.BookGenres.Add(new BookGenre { BookId = book.Id, GenreId = gen.Id });
                }
                _context.SaveChanges();
                ClearTempAuthors(); ClearTempGenres();
                return new ServiceResult { Success = true, Message = $"Added {quantity} books." };
            }
            catch (Exception ex) { return new ServiceResult { Success = false, ErrorMessage = ex.Message }; }
        }

        public ServiceResult UpdateBook(int id, string title, string type, string cover, DateTime date, string isbn)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book == null) return new ServiceResult { Success = false, ErrorMessage = "Book not found" };
                if (_tempAuthors.Count == 0 || _tempGenres.Count == 0) return new ServiceResult { Success = false, ErrorMessage = "Authors/Genres required." };

                book.Title = title;
                book.Type = Enum.Parse<BookType>(type);
                book.CoverType = Enum.Parse<CoverType>(cover);
                book.ReleaseYear = date.Year;
                book.ISBN = isbn;

                var existingAuthors = _context.BookAuthors.Where(ba => ba.BookId == id);
                _context.BookAuthors.RemoveRange(existingAuthors);
                var existingGenres = _context.BookGenres.Where(bg => bg.BookId == id);
                _context.BookGenres.RemoveRange(existingGenres);

                foreach (var auth in _tempAuthors) _context.BookAuthors.Add(new BookAuthor { BookId = id, AuthorId = auth.Id });
                foreach (var gen in _tempGenres) _context.BookGenres.Add(new BookGenre { BookId = id, GenreId = gen.Id });

                _context.SaveChanges();
                ClearTempAuthors(); ClearTempGenres();
                return new ServiceResult { Success = true, Message = "Book updated successfully." };
            }
            catch (Exception ex) { return new ServiceResult { Success = false, ErrorMessage = ex.Message }; }
        }

        // --- POPRAWIONA METODA DELETE ---
        public ServiceResult DeleteBook(int bookId)
        {
            try
            {
                var book = _context.Books
                    .Include(b => b.Loans) // Sprawdź wypożyczenia
                    .FirstOrDefault(b => b.Id == bookId);

                if (book == null)
                {
                    return new ServiceResult { Success = false, ErrorMessage = "Book not found" };
                }

                // Blokada usunięcia, jeśli książka jest wypożyczona
                if (book.Loans.Any())
                {
                    return new ServiceResult
                    {
                        Success = false,
                        ErrorMessage = "Cannot delete a book that is currently borrowed."
                    };
                }

                _context.Books.Remove(book);
                _context.SaveChanges();

                return new ServiceResult
                {
                    Success = true,
                    Message = "Book deleted successfully." // Ta wiadomość trafi do MessageBoxa
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Success = false, ErrorMessage = "Error deleting book: " + ex.Message };
            }
        }

        public void Dispose() => _context?.Dispose();
    }
}