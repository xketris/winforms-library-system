using LibraryApp.Models.Entities; //

namespace LibraryApp
{
    public static class UserSession
    {
        private static User? _currentUser;

        public static User? CurrentUser
        {
            get => _currentUser;
            private set => _currentUser = value;
        }

        public static bool IsLoggedIn => _currentUser != null;

        public static bool IsAdmin => _currentUser?.IsAdmin ?? false;

        public static int? UserId => _currentUser?.Id;

        public static string? UserFullName =>
            _currentUser != null ? $"{_currentUser.FirstName} {_currentUser.LastName}" : null;

        public static void Login(User user)
        {
            _currentUser = user;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static void UpdateCurrentUser(User updatedUser)
        {
            // Aktualizacja danych w sesji tylko jeœli ID siê zgadza
            if (_currentUser?.Id == updatedUser.Id)
            {
                _currentUser = updatedUser;
            }
        }
    }
}