namespace LibraryApp.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        // Opcjonalnie, jeśli potrzebujesz zwracać obiekt po operacji
        public object? Data { get; set; }
    }
}