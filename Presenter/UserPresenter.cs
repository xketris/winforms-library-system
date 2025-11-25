using LibraryApp.Models;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class UserPresenter
    {
        private readonly UserDashboardControl _view;
        private readonly UserModel _model;

        public UserPresenter(UserDashboardControl view, UserModel model)
        {
            _view = view;
            _model = model;
            _view.OnUpdateData += UpdateData;
            // Return i Logout obsługuje LibraryPresenter
        }

        public void RefreshData()
        {
            _view.Email = _model.GetEmail();
            _view.Phone = _model.GetPhone();
            _view.Province = _model.GetProvince();
            _view.City = _model.GetCity();
            _view.Street = _model.GetStreet();
            _view.HouseNumber = _model.GetHouseNum();
            _view.ApartmentNumber = _model.GetApartmentNum();
            _view.ZipCode1 = _model.GetZipCodePart1();
            _view.ZipCode2 = _model.GetZipCodePart2();

            _view.SetUserInfo(_model.GetUserFullName(), _model.GetUserBalance());
            _view.SetBorrowedBooks(_model.GetBorrowedBooks());
            _view.SetFavoriteBooks(_model.GetFavoriteBooks());
        }

        private void UpdateData(string e, string p, string prov, string c, string s, string h, string a, string z1, string z2)
        {
            var res = _model.UpdateUserData(e, p, prov, c, s, h, a, z1, z2);
            if (res.Success) { MessageBox.Show("Updated!"); RefreshData(); }
            else MessageBox.Show(res.ErrorMessage);
        }
    }
}