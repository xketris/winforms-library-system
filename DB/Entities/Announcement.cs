using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Entities
{
    [Table("ANNOUNCEMENTS")]
    public class Announcement
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("TITLE")]
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Column("DESCRIPTION")]
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Column("CREATED_AT")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}