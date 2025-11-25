using LibraryApp.Models;
using LibraryApp.Presenters;
using LibraryApp.Models.Entities;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace LibraryApp.View
{
    public partial class MainForm : Form
    {
        // Właściwości publiczne do dostępu do kontrolek z poziomu Prezentera
        public TableLayoutPanel MainTableLayout => tableLayoutPanel1;
        public LoginControl LoginControl => logowanie1;
        public RegisterControl RegisterControl => rejestracja1;
        public UserDashboardControl UserDashboardControl => paneluzytkownika1;
        public AdminBooksControl AdminBooksControl => adminpanelksiazki1;
        public AdminUsersControl AdminUsersControl => adminpaneluzytkownicy1;
        public AdminAnnouncementsControl AdminAnnouncementsControl => adminpanelogloszenia1;

        // Przyciski nawigacyjne (potrzebne do ukrywania/zmiany tekstu)
        public Button LoginButton => button1;
        public Button RegisterButton => button2;

        // Właściwości filtrów
        public string FilterTitle => txtTitle.Text;
        public string FilterGenre => txtGenre.Text;
        public string FilterISBN => txtISBN.Text;
        public string FilterAuthor => txtAuthor.Text;
        public string FilterReleaseYear => txtReleaseYear.Text;
        public string FilterType => comboBox1.SelectedItem?.ToString();
        public string FilterCoverType => comboBox2.SelectedItem?.ToString();
        public string SortBy => cmbSort.SelectedItem?.ToString() ?? "Title";

        // Zdarzenia
        public event Action OnFilterChanged;
        public event Action OnShowLogin;
        public event Action OnShowRegister;
        public event Action OnShowUserDashboard;
        public event Action OnShowAdminDashboard;

        private UserControl[] _userControls;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomControls();

            // Podpięcie zdarzeń zmiany filtrów
            txtTitle.TextChanged += (s, e) => OnFilterChanged?.Invoke();
            txtGenre.TextChanged += (s, e) => OnFilterChanged?.Invoke();
            txtISBN.TextChanged += (s, e) => OnFilterChanged?.Invoke();
            txtAuthor.TextChanged += (s, e) => OnFilterChanged?.Invoke();
            txtReleaseYear.TextChanged += (s, e) => OnFilterChanged?.Invoke();
            cmbSort.SelectedIndexChanged += (s, e) => OnFilterChanged?.Invoke();
            comboBox1.SelectedIndexChanged += (s, e) => OnFilterChanged?.Invoke();
            comboBox2.SelectedIndexChanged += (s, e) => OnFilterChanged?.Invoke();

            // Obsługa kliknięć w tabeli (przycisk wypożyczania)
            dgvBooks.CellClick += DgvBooks_CellClick;
        }

        private void InitializeCustomControls()
        {
            // Tablica wszystkich paneli, aby łatwo nimi zarządzać
            _userControls = new UserControl[]
            {
                LoginControl, RegisterControl, UserDashboardControl,
                AdminBooksControl, AdminUsersControl, AdminAnnouncementsControl
            };

            // Wypełnienie ComboBoxów
            cmbSort.Items.AddRange(new string[] { "Author", "Title", "Release Date" });
            comboBox1.Items.AddRange(new string[] { "Book", "Album", "Comic" });
            comboBox2.Items.AddRange(new string[] { "Soft", "Hard" });
            cmbSort.SelectedIndex = 0;

            // Ukrycie wszystkich paneli na start (pokazujemy tylko główną tabelę)
            foreach (var ctrl in _userControls) ctrl.Hide();
        }

        public void SetBookList(List<BookDTO> books)
        {
            dgvBooks.Columns.Clear();
            dgvBooks.DataSource = null;
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.DataSource = books;

            // Konfiguracja kolumn (musi pasować do nazw właściwości w BookDTO)
            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                HeaderText = "Title",
                Width = 260
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Genres", // Liczba mnoga (poprawione)
                HeaderText = "Genre"
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ISBN",
                HeaderText = "ISBN",
                Width = 150
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Authors", // Liczba mnoga (poprawione)
                HeaderText = "Author",
                Width = 150
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReleaseYear",
                HeaderText = "Year",
                Width = 80
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Type",
                HeaderText = "Type"
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CoverType",
                HeaderText = "Cover"
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ExemplarsStatus",
                HeaderText = "Availability"
            });

            // Dodanie przycisku akcji tylko dla zalogowanych użytkowników
            if (UserSession.IsLoggedIn)
            {
                var borrowBtn = new DataGridViewButtonColumn
                {
                    HeaderText = "Actions",
                    Name = "BorrowButton1",
                    Text = "Details/Borrow",
                    UseColumnTextForButtonValue = true
                };
                dgvBooks.Columns.Add(borrowBtn);
            }
        }

        private void DgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sprawdzenie czy kliknięto w przycisk (a nie w nagłówek czy inną komórkę)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvBooks.Columns[e.ColumnIndex].Name == "BorrowButton1")
            {
                BookDTO book = dgvBooks.Rows[e.RowIndex].DataBoundItem as BookDTO;

                // Otwórz okno z egzemplarzami
                var exemplarsForm = new ExemplarsForm(book.Title, book.Exemplars, dgvBooks);
                var borrowingService = new Models.Services.BorrowingService();
                var presenter = new ExemplarPresenter(exemplarsForm, borrowingService);
                exemplarsForm.ShowDialog();
            }
        }

        // Przycisk "Login" / "User Panel"
        private void button1_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
                OnShowLogin?.Invoke();
            else if (UserSession.IsAdmin)
                OnShowAdminDashboard?.Invoke();
            else
                OnShowUserDashboard?.Invoke();
        }

        // Przycisk "Register"
        private void button2_Click(object sender, EventArgs e)
        {
            OnShowRegister?.Invoke();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Wymuszenie odświeżenia listy przy starcie
            OnFilterChanged?.Invoke();
        }
    }
}