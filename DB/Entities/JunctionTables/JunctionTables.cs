using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Entities
{
    [Table("BOOK_AUTHORS")]
    public class BookAuthor
    {
        [Column("BOOK_ID")]
        public int BookId { get; set; }
        [Column("AUTHOR_ID")]
        public int AuthorId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }

    [Table("BOOK_GENRES")]
    public class BookGenre
    {
        [Column("BOOK_ID")]
        public int BookId { get; set; }
        [Column("GENRE_ID")]
        public int GenreId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }

    [Table("LOANS")]
    public class Loan
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("BOOK_ID")]
        public int BookId { get; set; }
        [Column("USER_ID")]
        public int UserId { get; set; }
        [Column("LOAN_DATE")]
        public DateTime LoanDate { get; set; }
        [Column("DUE_DATE")]
        public DateTime DueDate { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

    [Table("FAVORITES")]
    public class Favorite
    {
        [Column("BOOK_ID")]
        public int BookId { get; set; }
        [Column("USER_ID")]
        public int UserId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}