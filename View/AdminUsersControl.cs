using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LibraryApp.View
{
    public partial class AdminUsersControl : UserControl
    {
        public event Action OnSwitchToBooks;
        public event Action<string> OnUserSelected;
        public event Action<string> OnToggleBan;
        public event Action OnSwitchToAnnouncements;
        public event Action OnReturnToMain;
        public event Action OnLogout;

        private bool _isSettingInternal = false;

        public AdminUsersControl()
        {
            InitializeComponent();
        }

        public void LoadUserList(List<UserInfo> users)
        {
            listBox1.Items.Clear();
            foreach (var user in users) listBox1.Items.Add(user);
        }

        public void SetSelectedUser(UserInfo user)
        {
            _isSettingInternal = true;
            Ban.Checked = user.IsBanned;
            Ban.Text = $"Ban - {user.FullName}";
            Ban.Tag = user;
            _isSettingInternal = false;
        }

        // Nav to Books
        private void button1_Click(object sender, EventArgs e) => OnSwitchToBooks?.Invoke();

        // Nav to Announcements
        private void button2_Click(object sender, EventArgs e) => OnSwitchToAnnouncements?.Invoke();

        // Nav to Main
        private void button3_Click(object sender, EventArgs e) => OnReturnToMain?.Invoke();

        // Logout
        private void button4_Click(object sender, EventArgs e) => OnLogout?.Invoke();

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is UserInfo user)
            {
                SetSelectedUser(user);
                // Passing string representation to match Presenter expectations from legacy code
                OnUserSelected?.Invoke(user.ToString());
            }
        }

        private void Ban_CheckedChanged(object sender, EventArgs e)
        {
            if (_isSettingInternal) return;

            if (listBox1.SelectedItem is UserInfo user)
            {
                OnToggleBan?.Invoke(user.ToString());
            }
        }
    }
}