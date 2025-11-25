using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LibraryApp.View
{
    public partial class UserDashboardControl : UserControl
    {
        public event Action<string, string, string, string, string, string, string, string, string> OnUpdateData;
        public event Action OnReturn;
        public event Action OnLogout;

        public UserDashboardControl()
        {
            InitializeComponent();
        }

        // Właściwości (Properties) powiązane z polami tekstowymi
        public string Email { get => textBox1.Text; set => textBox1.Text = value; }
        public string Phone { get => textBox2.Text; set => textBox2.Text = value; }
        public string Province { get => textBox3.Text; set => textBox3.Text = value; }
        public string City { get => textBox4.Text; set => textBox4.Text = value; }
        public string Street { get => textBox5.Text; set => textBox5.Text = value; }
        public string HouseNumber { get => textBox6.Text; set => textBox6.Text = value; }
        public string ApartmentNumber { get => textBox7.Text; set => textBox7.Text = value; }
        public string ZipCode1 { get => textBox8.Text; set => textBox8.Text = value; }
        public string ZipCode2 { get => textBox9.Text; set => textBox9.Text = value; }

        // Obsługa przycisku Update
        private void button1_Click(object sender, EventArgs e)
        {
            if (OnUpdateData == null)
            {
                MessageBox.Show("Error: Presenter not connected!");
                return;
            }

            OnUpdateData.Invoke(Email, Phone, Province, City, Street, HouseNumber, ApartmentNumber, ZipCode1, ZipCode2);
        }

        // Obsługa przycisku Back
        private void button2_Click(object sender, EventArgs e)
        {
            OnReturn?.Invoke();
        }

        // Obsługa przycisku Logout
        private void button3_Click(object sender, EventArgs e)
        {
            OnLogout?.Invoke();
        }

        public void SetUserInfo(string fullName, decimal balance)
        {
            var parts = fullName.Split(' ');
            label2.Text = parts.FirstOrDefault() ?? ""; // Imię
            label3.Text = parts.Skip(1).FirstOrDefault() ?? ""; // Nazwisko
            label5.Text = $"{balance:C}"; // Saldo
        }

        public void SetBorrowedBooks(List<BorrowedBookInfo> borrowedBooks)
        {
            listBox2.Items.Clear();
            foreach (var book in borrowedBooks)
            {
                listBox2.Items.Add(book.ToString());
            }
        }

        public void SetFavoriteBooks(List<FavoriteBookInfo> favoriteBooks)
        {
            listBox1.Items.Clear();
            foreach (var book in favoriteBooks)
            {
                listBox1.Items.Add(book.ToString());
            }
        }
    }
}