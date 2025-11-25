using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LibraryApp.View
{
    public partial class AdminAnnouncementsControl : UserControl
    {
        public event Action<string, string> OnAddAnnouncement;
        public event Action<string, string> OnEditAnnouncement;
        public event Action<AnnouncementInfo> OnDeleteAnnouncement;
        public event Action<AnnouncementInfo> OnSelectAnnouncement;

        public event Action OnSwitchToUsers;
        public event Action OnSwitchToBooks;
        public event Action OnReturnToMain;
        public event Action OnLogout;

        public AdminAnnouncementsControl()
        {
            InitializeComponent();
        }

        public string TitleText { get => textBox1.Text; set => textBox1.Text = value; }
        public string DescriptionText { get => textBox2.Text; set => textBox2.Text = value; }

        public void LoadAnnouncementList(List<AnnouncementInfo> list)
        {
            listBox1.Items.Clear();
            foreach (var item in list) listBox1.Items.Add(item);
        }

        public void SetEditMode(bool isEditing)
        {
            button1.Text = isEditing ? "Update" : "Add";
            button4.Text = isEditing ? "Cancel" : "Edit";
        }

        // Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            OnAddAnnouncement?.Invoke(TitleText, DescriptionText);
        }

        // Edit/Cancel Button
        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Cancel")
            {
                SetEditMode(false);
                TitleText = ""; DescriptionText = "";
            }
            else
            {
                OnEditAnnouncement?.Invoke(TitleText, DescriptionText);
            }
        }

        // Delete Button
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is AnnouncementInfo selected)
                OnDeleteAnnouncement?.Invoke(selected);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is AnnouncementInfo selected)
                OnSelectAnnouncement?.Invoke(selected);
        }

        // Nav Buttons
        private void button2_Click(object sender, EventArgs e) => OnSwitchToBooks?.Invoke();
        private void button3_Click(object sender, EventArgs e) => OnSwitchToUsers?.Invoke();
        private void button6_Click(object sender, EventArgs e) => OnReturnToMain?.Invoke();
        private void button7_Click(object sender, EventArgs e) => OnLogout?.Invoke();
    }
}