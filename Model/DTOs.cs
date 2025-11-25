using System;

namespace LibraryApp.Models
{
    // Wynik operacji aktualizacji danych użytkownika
    public class UserUpdateResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    // Informacje o wypożyczonej książce (do wyświetlania na liście)
    public class BorrowedBookInfo
    {
        public string BookTitle { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsOverdue { get; set; }

        public override string ToString()
        {
            var status = IsOverdue ? " (OVERDUE)" : "";
            return $"{BookTitle} - {Authors} (Due: {ReturnDate:dd.MM.yyyy}){status}";
        }
    }

    // Informacje o ulubionej książce (do wyświetlania na liście)
    public class FavoriteBookInfo
    {
        public string BookTitle { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public string Genres { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{BookTitle} - {Authors} ({Genres})";
        }
    }
}