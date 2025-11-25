using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Models.Enums;

namespace LibraryApp.Models.Entities
{
    [Table("BOOKS")]
    public class Book
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("TITLE")]
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Column("PRICE")]
        public decimal Price { get; set; } = 0;

        [Column("TYPE")]
        [Required]
        public BookType Type { get; set; }

        [Column("COVER_TYPE")]
        [Required]
        public CoverType CoverType { get; set; }

        [Column("DAMAGE")]
        [Required]
        public DamageLevel Damage { get; set; } = DamageLevel.None;

        [Column("STATUS")]
        [Required]
        public BookStatus Status { get; set; } = BookStatus.Available;

        [Column("ISBN")]
        [MaxLength(20)]
        public string? ISBN { get; set; }

        [Column("RELEASE_YEAR")]
        public int? ReleaseYear { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}