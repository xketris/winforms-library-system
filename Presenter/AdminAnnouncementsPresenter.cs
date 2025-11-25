using LibraryApp.Models;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class AdminAnnouncementsPresenter // Lub AdminAnnouncementsPresenter
    {
        private readonly AdminAnnouncementsControl _view;
        private readonly AdminAnnouncementsModel _model;
        private AnnouncementInfo? _selectedAnnouncement;
        private bool _isEditing = false;

        public AdminAnnouncementsPresenter(AdminAnnouncementsControl view, AdminAnnouncementsModel model)
        {
            _view = view;
            _model = model;

            _view.OnAddAnnouncement += AddOrUpdate;
            _view.OnSelectAnnouncement += SetSelected;
            _view.OnEditAnnouncement += Edit; // Przekierowanie do AddOrUpdate
            _view.OnDeleteAnnouncement += Delete;

            RefreshData();
        }

        public void RefreshData()
        {
            _view.LoadAnnouncementList(_model.GetAllAnnouncements());
        }

        private void AddOrUpdate(string title, string description)
        {
            AnnouncementResult result;
            if (_isEditing && _selectedAnnouncement != null)
                result = _model.UpdateAnnouncement(_selectedAnnouncement.Id, title, description);
            else
                result = _model.AddAnnouncement(title, description);

            if (result.Success) { MessageBox.Show(result.Message); RefreshData(); ClearForm(); }
            else MessageBox.Show(result.ErrorMessage);
        }

        private void SetSelected(AnnouncementInfo info)
        {
            _selectedAnnouncement = info;
            _view.TitleText = info.Title;
            _view.DescriptionText = info.Description;
            _view.SetEditMode(true);
            _isEditing = true;
        }

        private void Edit(string t, string d) => AddOrUpdate(t, d);

        private void Delete(AnnouncementInfo info)
        {
            if (MessageBox.Show($"Delete '{info.Title}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = _model.DeleteAnnouncement(info.Id);
                if (res.Success) { MessageBox.Show(res.Message); RefreshData(); ClearForm(); }
                else MessageBox.Show(res.ErrorMessage);
            }
        }

        private void ClearForm()
        {
            _view.TitleText = ""; _view.DescriptionText = ""; _view.SetEditMode(false);
            _selectedAnnouncement = null; _isEditing = false;
        }
    }
}