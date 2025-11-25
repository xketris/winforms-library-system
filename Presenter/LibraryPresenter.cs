using System.Diagnostics;
using LibraryApp.Models;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class LibraryPresenter
    {
        private readonly MainForm _view;
        private readonly BookRepository _repository;

        // Prezenterzy (instancje trzymane na stałe)
        private LoginPresenter _loginPresenter;
        private RegisterPresenter _registerPresenter;
        private UserPresenter _userPresenter;
        private AdminBooksPresenter _adminBooksPresenter;
        private AdminUsersPresenter _adminUsersPresenter;
        private AdminAnnouncementsPresenter _adminAnnouncementsPresenter;

        // Modele (instancje trzymane na stałe, aby zachować np. listy tymczasowe)
        private LoginModel _loginModel;
        private RegisterModel _registerModel;
        private UserModel _userModel;
        private AdminBooksModel _adminBooksModel;
        private AdminUsersModel _adminUsersModel;
        private AdminAnnouncementsModel _adminAnnouncementsModel;

        public LibraryPresenter(MainForm view, BookRepository repository)
        {
            _view = view;
            _repository = repository;

            InitializePresenters();
            BindNavigationEvents();

            // Start
            ShowMainPanel();
        }

        private void InitializePresenters()
        {
            // Inicjalizacja Modeli
            _loginModel = new LoginModel();
            _registerModel = new RegisterModel();
            _userModel = new UserModel();
            _adminBooksModel = new AdminBooksModel();
            _adminUsersModel = new AdminUsersModel();
            _adminAnnouncementsModel = new AdminAnnouncementsModel();

            // Inicjalizacja Prezenterów (tylko raz!)
            _loginPresenter = new LoginPresenter(_view.LoginControl, _loginModel);
            _registerPresenter = new RegisterPresenter(_view.RegisterControl, _registerModel);
            _userPresenter = new UserPresenter(_view.UserDashboardControl, _userModel);
            _adminBooksPresenter = new AdminBooksPresenter(_view.AdminBooksControl, _adminBooksModel);
            _adminUsersPresenter = new AdminUsersPresenter(_view.AdminUsersControl, _adminUsersModel);
            _adminAnnouncementsPresenter = new AdminAnnouncementsPresenter(_view.AdminAnnouncementsControl, _adminAnnouncementsModel);
        }

        private void BindNavigationEvents()
        {
            // Główne zdarzenia MainForm
            _view.OnFilterChanged += UpdateBookList;
            _view.OnShowLogin += ShowLoginPanel;
            _view.OnShowRegister += ShowRegisterPanel;
            _view.OnShowUserDashboard += ShowUserDashboard;
            _view.OnShowAdminDashboard += ShowAdminBooks; // Domyślny widok admina to Książki

            // Logowanie i Rejestracja (Sukces)
            _loginPresenter.OnLoginSuccess += OnLoginSuccess;
            _registerPresenter.OnRegisterSuccess += OnRegisterSuccess;

            // Powroty i Wylogowanie (ze wszystkich widoków)
            _view.LoginControl.OnReturnToMain += ShowMainPanel;
            _view.RegisterControl.OnReturnToMain += ShowMainPanel;

            _view.UserDashboardControl.OnReturn += ShowMainPanel;
            _view.UserDashboardControl.OnLogout += Logout;

            // Nawigacja Admina (podpinamy się pod widoki, nie prezenterów)
            // Books View
            _view.AdminBooksControl.OnSwitchToUsers += ShowAdminUsers;
            _view.AdminBooksControl.OnSwitchToAnnouncements += ShowAdminAnnouncements;
            _view.AdminBooksControl.OnReturnToMain += ShowMainPanel;
            _view.AdminBooksControl.OnLogout += Logout;

            // Users View
            _view.AdminUsersControl.OnSwitchToBooks += ShowAdminBooks;
            _view.AdminUsersControl.OnSwitchToAnnouncements += ShowAdminAnnouncements;
            _view.AdminUsersControl.OnReturnToMain += ShowMainPanel;
            _view.AdminUsersControl.OnLogout += Logout;

            // Announcements View
            _view.AdminAnnouncementsControl.OnSwitchToBooks += ShowAdminBooks;
            _view.AdminAnnouncementsControl.OnSwitchToUsers += ShowAdminUsers;
            _view.AdminAnnouncementsControl.OnReturnToMain += ShowMainPanel;
            _view.AdminAnnouncementsControl.OnLogout += Logout;
        }

        private void UpdateBookList()
        {
            var filtered = _repository.Filter(
                _view.FilterTitle, _view.FilterGenre, _view.FilterISBN,
                _view.FilterAuthor, _view.FilterReleaseYear, _view.FilterType, _view.FilterCoverType
            );
            var sorted = _repository.Sort(filtered, _view.SortBy);
            _view.SetBookList(sorted);
        }

        // --- Metody Nawigacji ---

        private void HideAllPanels()
        {
            _view.MainTableLayout.Hide();
            _view.LoginControl.Hide();
            _view.RegisterControl.Hide();
            _view.UserDashboardControl.Hide();
            _view.AdminBooksControl.Hide();
            _view.AdminUsersControl.Hide();
            _view.AdminAnnouncementsControl.Hide();
        }

        private void ShowMainPanel()
        {
            HideAllPanels();
            _view.MainTableLayout.Show();

            if (UserSession.IsLoggedIn)
            {
                _view.LoginButton.Text = "User Panel";
                _view.RegisterButton.Hide();
            }
            else
            {
                _view.LoginButton.Text = "Login";
                _view.RegisterButton.Show();
            }
            UpdateBookList();
        }

        private void ShowLoginPanel()
        {
            HideAllPanels();
            _view.LoginControl.Show();
        }

        private void ShowRegisterPanel()
        {
            HideAllPanels();
            _view.RegisterControl.Show();
        }

        private void OnLoginSuccess()
        {
            if (UserSession.IsAdmin)
                ShowAdminBooks();
            else
                ShowUserDashboard();
        }

        private void OnRegisterSuccess()
        {
            ShowUserDashboard();
        }

        private void ShowUserDashboard()
        {
            HideAllPanels();
            _userPresenter.RefreshData(); // Odśwież dane użytkownika
            _view.UserDashboardControl.Show();
        }

        // --- Panel Admina (Zakładki) ---

        private void ShowAdminBooks()
        {
            HideAllPanels();
            _adminBooksPresenter.RefreshData();
            _view.AdminBooksControl.Show();
        }

        private void ShowAdminUsers()
        {
            HideAllPanels();
            _adminUsersPresenter.RefreshData();
            _view.AdminUsersControl.Show();
        }

        private void ShowAdminAnnouncements()
        {
            HideAllPanels();
            _adminAnnouncementsPresenter.RefreshData();
            _view.AdminAnnouncementsControl.Show();
        }

        private void Logout()
        {
            UserSession.Logout();
            ShowMainPanel();
        }
    }
}