namespace LibraryApp.Models
{
    public class Exemplar
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool HasReturnDatePassed => ReturnDate < DateTime.Now && ReturnDate.HasValue;
        public bool IsAvailable => !BorrowDate.HasValue;
    }
}