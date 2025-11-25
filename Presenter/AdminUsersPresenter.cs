using LibraryApp.Models;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class AdminUsersPresenter
    {
        private readonly AdminUsersControl _view;
        private readonly AdminUsersModel _model;

        public AdminUsersPresenter(AdminUsersControl view, AdminUsersModel model)
        {
            _view = view;
            _model = model;

            _view.OnUserSelected += HandleUserSelection;
            _view.OnToggleBan += HandleBanToggle;

            RefreshData();
        }

        public void RefreshData()
        {
            var users = _model.GetAllUsers();
            _view.LoadUserList(users);
        }

        private void HandleUserSelection(string userString) { /* Opcjonalna logika szczegółów */ }

        private void HandleBanToggle(string userString)
        {
            var resultString = _model.ToggleBanByString(userString);
            if (resultString != userString) RefreshData();
        }
    }
}