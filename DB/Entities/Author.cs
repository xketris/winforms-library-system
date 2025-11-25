using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Entities
{
    [Table("AUTHORS")]
    public class Author
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("FIRST_NAME")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Column("NATIONALITY")]
        [MaxLength(50)]
        public string Nationality { get; set; } = "Unknown";

        [Column("BIRTH_DATE")]
        public DateTime? BirthDate { get; set; }

        [Column("DEATH_DATE")]
        public DateTime? DeathDate { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}