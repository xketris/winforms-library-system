using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models.Entities
{
    [Table("ADDRESSES")]
    public class Address
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CITY")]
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = "Gliwice";

        [Column("STREET")]
        [Required]
        [MaxLength(100)]
        public string Street { get; set; }

        [Column("PROVINCE")]
        [MaxLength(50)]
        public string Province { get; set; } = "Śląskie";

        [Column("HOUSE_NUM")]
        [MaxLength(10)]
        public string HouseNumber { get; set; }

        [Column("APT_NUM")]
        [MaxLength(10)]
        public string? ApartmentNumber { get; set; } // Zmiana na nullable (string?)

        [Column("ZIP_CODE")]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}