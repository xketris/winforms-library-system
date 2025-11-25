using System.Collections.Generic;
using System.Linq;
using LibraryApp.Models.Entities; // Potrzebne dla Exemplar jeśli jest w Entities, lub LibraryApp.Models

namespace LibraryApp.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // WAŻNE: Te nazwy muszą być w liczbie mnogiej, bo tak ustawiliśmy w MainForm
        public string Authors { get; set; }
        public string Genres { get; set; }

        public string ISBN { get; set; }
        public int? ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string CoverType { get; set; }

        public List<Exemplar> Exemplars { get; set; } = new List<Exemplar>();

        public string ExemplarsStatus => $"{Exemplars?.Count(e => e.IsAvailable) ?? 0}/{Exemplars?.Count ?? 0}";

        public BookDTO() { }

        // Konstruktor używany przez BookRepository
        public BookDTO(int id, string title, string genres, string isbn, string authors, int? releaseYear, string type, string coverType, decimal price)
        {
            Id = id;
            Title = title;
            Genres = genres;   // Przypisanie do Genres
            ISBN = isbn;
            Authors = authors; // Przypisanie do Authors
            ReleaseYear = releaseYear;
            Type = type;
            CoverType = coverType;
            Price = price;
        }

        public void AddExemplar(Exemplar exemplar)
        {
            Exemplars.Add(exemplar);
        }

        public override string ToString()
        {
            return $"{Title} - {Authors} ({ReleaseYear})";
        }
    }
}