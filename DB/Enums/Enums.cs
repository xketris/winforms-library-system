namespace LibraryManagement.Models.Enums
{
    public enum BookType // RodzajKsiazki
    {
        Book,
        Comic,
        Album
    }

    public enum CoverType // RodzajOkladki
    {
        Hard,
        Soft
    }

    public enum DamageLevel // Uszkodzenia
    {
        None,
        Slight,
        Significant,
        RepairNeeded // Do_naprawy
    }

    public enum BookStatus // StatusKsiazki
    {
        Available,  // Dostępna
        Borrowed,   // Wypożyczona
        Other       // Inne
    }
}