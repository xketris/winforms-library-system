using LibraryApp.Data;
using LibraryApp.Models.Entities;
using LibraryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryApp.Models
{
    public class BookRepository : IDisposable
    {
        private readonly LibraryContext _context;

        public BookRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=Library.db");
            _context = new LibraryContext(optionsBuilder.Options);
        }

        public List<BookDTO> Filter(string title, string genre, string isbn, string author, string releaseYear, string type, string coverType)
        {
            try
            {
                // KLUCZOWY FRAGMENT: Include musi być obecne, aby pobrać Autorów i Gatunki
                var query = _context.Books
                    .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author) // <-- WAŻNE
                    .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)   // <-- WAŻNE
                    .Include(b => b.Loans)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(title)) query = query.Where(b => b.Title.Contains(title));
                if (!string.IsNullOrEmpty(genre)) query = query.Where(b => b.BookGenres.Any(bg => bg.Genre.Name.Contains(genre)));
                if (!string.IsNullOrEmpty(author)) query = query.Where(b => b.BookAuthors.Any(ba => ba.Author.FirstName.Contains(author) || ba.Author.LastName.Contains(author)));
                if (!string.IsNullOrEmpty(isbn)) query = query.Where(b => b.ISBN.Contains(isbn));

                if (!string.IsNullOrEmpty(type))
                {
                    var typeEnum = ParseBookType(type);
                    query = query.Where(b => b.Type == typeEnum);
                }
                if (!string.IsNullOrEmpty(coverType))
                {
                    var coverEnum = ParseCoverType(coverType);
                    query = query.Where(b => b.CoverType == coverEnum);
                }

                if (int.TryParse(releaseYear, out int year))
                {
                    query = query.Where(b => b.ReleaseYear == year);
                }

                // Grupowanie po ISBN (lub tytule, jeśli brak ISBN)
                return query.ToList()
                    .GroupBy(b => !string.IsNullOrWhiteSpace(b.ISBN) ? b.ISBN : b.Title)
                    .Select(group => ConvertToBookDTO(group.ToList()))
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering books: {ex.Message}");
                return new List<BookDTO>();
            }
        }

        public List<BookDTO> Sort(List<BookDTO> input, string sortBy)
        {
            return sortBy switch
            {
                "Author" => input.OrderBy(b => b.Authors).ToList(),
                "Title" => input.OrderBy(b => b.Title).ToList(),
                "Release Date" => input.OrderBy(b => b.ReleaseYear).ToList(),
                _ => input
            };
        }

        private BookDTO ConvertToBookDTO(List<Book> books)
        {
            var first = books.First();

            // Tworzenie stringów z list autorów i gatunków
            var authors = string.Join(", ", first.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
            var genres = string.Join(", ", first.BookGenres.Select(bg => bg.Genre.Name));

            var dto = new BookDTO(
                first.Id,
                first.Title,
                genres,
                first.ISBN ?? "No ISBN",
                authors,
                first.ReleaseYear,
                first.Type.ToString(),
                first.CoverType.ToString(),
                first.Price
            );

            foreach (var b in books)
            {
                var loan = b.Loans.FirstOrDefault();
                dto.AddExemplar(new Exemplar
                {
                    BookId = b.Id,
                    Name = $"Exemplar {b.Id}",
                    BorrowDate = loan?.LoanDate,
                    ReturnDate = loan?.DueDate
                });
            }
            return dto;
        }

        private BookType ParseBookType(string type) => type?.ToLower() switch
        {
            "book" or "książka" => BookType.Book,
            "comic" or "komiks" => BookType.Comic,
            "album" => BookType.Album,
            _ => BookType.Book
        };

        private CoverType ParseCoverType(string type) => type?.ToLower() switch
        {
            "hard" or "twarda" => CoverType.Hard,
            "soft" or "miękka" => CoverType.Soft,
            _ => CoverType.Soft
        };

        public void Dispose() => _context?.Dispose();
    }
}