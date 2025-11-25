using LibraryApp.Models;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class RegisterPresenter
    {
        private readonly RegisterControl _view;
        private readonly RegisterModel _model;

        public event Action OnRegisterSuccess;

        public RegisterPresenter(RegisterControl view, RegisterModel model)
        {
            _view = view;
            _model = model;
            _view.OnRegisterAttempt += HandleRegister;
        }

        private void HandleRegister(string firstName, string lastName, string password, string email, string phone, string username, DateTime birthDate, string province, string city, string street, string house, string apt, string zip)
        {
            var result = _model.Register(firstName, lastName, password, email, phone, birthDate, province, city, street, house, apt, zip);

            if (result.Success)
            {
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnRegisterSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}