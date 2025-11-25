using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Entities
{
    [Table("GENRES")]
    public class Genre
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}