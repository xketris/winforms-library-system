using LibraryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace LibraryApp.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().Property(k => k.Type).HasConversion<string>();
            modelBuilder.Entity<Book>().Property(k => k.CoverType).HasConversion<string>();

            // POPRAWKA: Zmiana z k.DamageLevel na k.Damage
            modelBuilder.Entity<Book>().Property(k => k.Damage).HasConversion<string>();

            modelBuilder.Entity<Book>().Property(k => k.Status).HasConversion<string>();
            modelBuilder.Entity<Book>().HasIndex(k => k.ISBN);
            modelBuilder.Entity<Book>().Property(k => k.Price).HasColumnType("TEXT").HasConversion<string>();
            modelBuilder.Entity<User>().Property(u => u.Balance).HasColumnType("TEXT").HasConversion<string>();

            // Composite Keys
            modelBuilder.Entity<BookAuthor>().HasKey(ka => new { ka.BookId, ka.AuthorId });
            modelBuilder.Entity<BookGenre>().HasKey(kg => new { kg.BookId, kg.GenreId });
            modelBuilder.Entity<Favorite>().HasKey(u => new { u.BookId, u.UserId });

            // Relationships
            modelBuilder.Entity<User>().HasOne(u => u.Address).WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();

            // Seed Data
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Poetry" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 11, Name = "Fantasy" },
                new Genre { Id = 14, Name = "SciFi" }
            );

            var adminPassword = HashPassword("admin123");
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FirstName = "Administrator",
                LastName = "System",
                Email = "admin@library.com",
                PasswordHash = adminPassword,
                IsAdmin = true,
                IsBanned = false,
                Balance = 0,
                PhoneNumber = "123-456-789",
                BirthDate = new DateTime(1990, 1, 1)
            });

            modelBuilder.Entity<Address>().HasData(new Address
            {
                Id = 1,
                UserId = 1,
                City = "New York",
                Street = "5th Avenue",
                Province = "NY",
                HouseNumber = "1",
                ApartmentNumber = "",
                ZipCode = "10001"
            });
        }

        private static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var hashedBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "salt"));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}