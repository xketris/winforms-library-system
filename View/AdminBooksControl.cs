using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LibraryApp.View
{
    public partial class AdminBooksControl : UserControl
    {
        public event Action<string> OnAddGenre;
        public event Action OnClearGenres;
        public event Action<string, string> OnAddAuthor;
        public event Action OnClearAuthors;
        public event Action<string, string, string, DateTime, string, int> OnAddBook;
        public event Action<string, string, string, DateTime, string, int> OnEditBook;
        public event Action<BookDTO> OnSelectBook;
        public event Action<BookDTO> OnDeleteBook;
        public event Action OnCancelEdit;

        public event Action OnSwitchToUsers;
        public event Action OnSwitchToAnnouncements;
        public event Action OnReturnToMain;
        public event Action OnLogout;

        private bool _isEditing = false;

        public AdminBooksControl()
        {
            InitializeComponent();
        }

        // Właściwości UI
        public string TitleText { get => textBox1.Text; set => textBox1.Text = value; }
        public string ISBN { get => textBox2.Text; set => textBox2.Text = value; }
        public string GenreLabel { get => label8.Text; set => label8.Text = value; }
        public string AuthorsLabel { get => label12.Text; set => label12.Text = value; }
        public DateTime ReleaseDate { get => dateTimePicker1.Value; set => dateTimePicker1.Value = value; }

        public int Quantity
        {
            get => (int)numericUpDown1.Value;
            set
            {
                if (value < numericUpDown1.Minimum) value = (int)numericUpDown1.Minimum;
                if (value > numericUpDown1.Maximum) value = (int)numericUpDown1.Maximum;
                numericUpDown1.Value = value;
            }
        }

        public string SelectedType
        {
            get => comboBox2.SelectedItem?.ToString() ?? "Book";
            set
            {
                if (comboBox2.Items.Contains(value)) comboBox2.SelectedItem = value;
                else if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            }
        }

        public string SelectedCover
        {
            get => comboBox3.SelectedItem?.ToString() ?? "Soft";
            set
            {
                if (comboBox3.Items.Contains(value)) comboBox3.SelectedItem = value;
                else if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            }
        }

        // POPRAWIONA METODA: Czyści również pola imienia i nazwiska autora
        public void ClearInputs()
        {
            TitleText = "";
            ISBN = "";
            GenreLabel = "";
            AuthorsLabel = "";

            // Czyść pola autora
            textBox5.Text = ""; // First Name
            textBox7.Text = ""; // Last Name

            ReleaseDate = DateTime.Now;
            Quantity = 1;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
        }

        public void SetGenres(string[] genres)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(genres);
        }

        public void SetBookTypes(string[] types)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(types);
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
        }

        public void SetCoverTypes(string[] types)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(types);
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
        }

        public void LoadBookList(List<BookDTO> books)
        {
            listBox1.Items.Clear();
            foreach (var book in books) listBox1.Items.Add(book);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
                OnAddGenre?.Invoke(comboBox1.SelectedItem.ToString());
        }

        // POPRAWIONA METODA: Czyści pola po dodaniu autora
        private void button4_Click(object sender, EventArgs e)
        {
            OnAddAuthor?.Invoke(textBox5.Text, textBox7.Text);
            textBox5.Text = ""; // Wyczyść imię
            textBox7.Text = ""; // Wyczyść nazwisko
        }

        private void button5_Click(object sender, EventArgs e) => OnClearAuthors?.Invoke();
        private void button6_Click(object sender, EventArgs e) => OnClearGenres?.Invoke();

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isEditing)
                OnEditBook?.Invoke(TitleText, SelectedType, SelectedCover, ReleaseDate, ISBN, Quantity);
            else
                OnAddBook?.Invoke(TitleText, SelectedType, SelectedCover, ReleaseDate, ISBN, Quantity);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is BookDTO selected) OnSelectBook?.Invoke(selected);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "Cancel")
            {
                OnCancelEdit?.Invoke();
            }
            else
            {
                if (listBox1.SelectedItem is BookDTO selected) OnSelectBook?.Invoke(selected);
            }
        }

        public void SetEditMode(bool isEditing)
        {
            _isEditing = isEditing;
            button1.Text = isEditing ? "Update" : "Add Book";
            button7.Text = isEditing ? "Cancel" : "Edit";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is BookDTO selected) OnDeleteBook?.Invoke(selected);
        }

        private void button8_Click(object sender, EventArgs e) => OnSwitchToUsers?.Invoke();
        private void button9_Click(object sender, EventArgs e) => OnSwitchToAnnouncements?.Invoke();
        private void button10_Click(object sender, EventArgs e) => OnReturnToMain?.Invoke();
        private void button11_Click(object sender, EventArgs e) => OnLogout?.Invoke();
    }
}