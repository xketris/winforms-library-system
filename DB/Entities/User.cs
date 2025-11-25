using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Entities
{
    [Table("USERS")]
    public class User
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

        [Column("BIRTH_DATE")]
        public DateTime? BirthDate { get; set; }

        [Column("BALANCE")]
        public decimal Balance { get; set; }

        [Column("EMAIL")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("IS_BANNED")]
        public bool IsBanned { get; set; }

        [Column("PHONE")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Column("PASSWORD_HASH")]
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Column("IS_ADMIN")]
        public bool IsAdmin { get; set; } = false;

        public virtual Address Address { get; set; }
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}