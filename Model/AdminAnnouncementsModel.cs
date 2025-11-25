using LibraryApp.Data;
using LibraryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models
{
    public class AdminAnnouncementsModel : IDisposable
    {
        private readonly LibraryContext _context;

        public AdminAnnouncementsModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite("Data Source=Library.db");
            _context = new LibraryContext(optionsBuilder.Options);
        }

        // Get all announcements
        public List<AnnouncementInfo> GetAllAnnouncements()
        {
            try
            {
                return _context.Announcements
                    .OrderByDescending(a => a.CreatedAt)
                    .Select(a => new AnnouncementInfo
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Description = a.Description,
                        CreatedAt = a.CreatedAt
                    })
                    .ToList();
            }
            catch (Exception)
            {
                return new List<AnnouncementInfo>();
            }
        }

        // Add new announcement
        public AnnouncementResult AddAnnouncement(string title, string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
                {
                    return new AnnouncementResult { Success = false, ErrorMessage = "Title and description are required" };
                }

                var announcement = new Announcement
                {
                    Title = title.Trim(),
                    Description = description.Trim(),
                    CreatedAt = DateTime.Now
                };

                _context.Announcements.Add(announcement);
                _context.SaveChanges();

                return new AnnouncementResult
                {
                    Success = true,
                    Message = "Announcement added successfully",
                    Announcement = new AnnouncementInfo
                    {
                        Id = announcement.Id,
                        Title = announcement.Title,
                        Description = announcement.Description,
                        CreatedAt = announcement.CreatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new AnnouncementResult { Success = false, ErrorMessage = "Error adding announcement: " + ex.Message };
            }
        }

        // Update existing announcement
        public AnnouncementResult UpdateAnnouncement(int id, string title, string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
                {
                    return new AnnouncementResult { Success = false, ErrorMessage = "Title and description are required" };
                }

                var announcement = _context.Announcements.Find(id);
                if (announcement == null)
                {
                    return new AnnouncementResult { Success = false, ErrorMessage = "Announcement not found" };
                }

                announcement.Title = title.Trim();
                announcement.Description = description.Trim();
                _context.SaveChanges();

                return new AnnouncementResult
                {
                    Success = true,
                    Message = "Announcement updated successfully",
                    Announcement = new AnnouncementInfo
                    {
                        Id = announcement.Id,
                        Title = announcement.Title,
                        Description = announcement.Description,
                        CreatedAt = announcement.CreatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new AnnouncementResult { Success = false, ErrorMessage = "Error updating announcement: " + ex.Message };
            }
        }

        // Delete announcement
        public AnnouncementResult DeleteAnnouncement(int id)
        {
            try
            {
                var announcement = _context.Announcements.Find(id);
                if (announcement == null)
                {
                    return new AnnouncementResult { Success = false, ErrorMessage = "Announcement not found" };
                }

                _context.Announcements.Remove(announcement);
                _context.SaveChanges();

                return new AnnouncementResult
                {
                    Success = true,
                    Message = "Announcement deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new AnnouncementResult { Success = false, ErrorMessage = "Error deleting announcement: " + ex.Message };
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    // DTOs
    public class AnnouncementInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Title} ({CreatedAt:dd.MM.yyyy HH:mm})";
        }
    }

    public class AnnouncementResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public AnnouncementInfo? Announcement { get; set; }
    }
}