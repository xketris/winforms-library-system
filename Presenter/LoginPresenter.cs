using LibraryApp.Models;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class LoginPresenter
    {
        private readonly LoginControl _view;
        private readonly LoginModel _model;

        // Zdarzenie informujące o sukcesie logowania
        public event Action OnLoginSuccess;

        // Usuwamy UserDashboardControl z konstruktora - nawigacją zajmie się LibraryPresenter
        public LoginPresenter(LoginControl view, LoginModel model)
        {
            _view = view;
            _model = model;
            _view.OnLoginAttempt += HandleLogin;
        }

        private void HandleLogin(string email, string password)
        {
            var result = _model.Login(email, password);
            if (result.Success)
            {
                // Zgłoś sukces, zamiast ręcznie pokazywać widok
                OnLoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}