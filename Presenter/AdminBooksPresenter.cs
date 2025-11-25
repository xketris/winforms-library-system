using LibraryApp.Models;
using LibraryApp.View;
using LibraryManagement.Models.Enums;
using System;
using System.Windows.Forms;

namespace LibraryApp.Presenters
{
    public class AdminBooksPresenter
    {
        private readonly AdminBooksControl _view;
        private readonly AdminBooksModel _model;
        private int? _selectedBookId;

        public AdminBooksPresenter(AdminBooksControl view, AdminBooksModel model)
        {
            _view = view;
            _model = model;

            RefreshData();

            _view.OnAddBook += AddBook;
            _view.OnEditBook += EditBook;
            _view.OnDeleteBook += DeleteBook;

            _view.OnAddGenre += AddGenre;
            _view.OnClearGenres += ClearGenres;
            _view.OnAddAuthor += AddAuthor;
            _view.OnClearAuthors += ClearAuthors;
            _view.OnSelectBook += SelectBook;

            // Podpięcie nowego zdarzenia
            _view.OnCancelEdit += ClearForm;
        }

        public void RefreshData()
        {
            _view.SetGenres(_model.GetGenres());
            _view.SetBookTypes(Enum.GetNames(typeof(BookType)));
            _view.SetCoverTypes(Enum.GetNames(typeof(CoverType)));
            _view.LoadBookList(_model.GetAllBooks());
        }

        private void AddBook(string title, string type, string cover, DateTime date, string isbn, int qty)
        {
            var result = _model.AddBook(title, type, cover, date, isbn, qty);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditBook(string title, string type, string cover, DateTime date, string isbn, int qty)
        {
            if (_selectedBookId == null) return;

            var result = _model.UpdateBook(_selectedBookId.Value, title, type, cover, date, isbn);

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteBook(BookDTO book)
        {
            var confirm = MessageBox.Show($"Delete '{book.Title}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                var result = _model.DeleteBook(book.Id);
                if (result.Success)
                {
                    MessageBox.Show(string.IsNullOrEmpty(result.Message) ? "Deleted" : result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SelectBook(BookDTO book)
        {
            _selectedBookId = book.Id;
            _model.LoadBookToTemp(book.Id);

            _view.TitleText = book.Title;
            _view.ISBN = book.ISBN ?? "";
            _view.SelectedType = book.Type;
            _view.SelectedCover = book.CoverType;
            _view.ReleaseDate = new DateTime(book.ReleaseYear ?? DateTime.Now.Year, 1, 1);

            _view.AuthorsLabel = _model.GetAuthorsString();
            _view.GenreLabel = _model.GetGenresString();

            _view.SetEditMode(true);
        }

        private void AddAuthor(string first, string last) { _view.AuthorsLabel = _model.AddAuthorToTemp(first, last); }
        private void ClearAuthors() { _view.AuthorsLabel = _model.ClearTempAuthors(); }
        private void AddGenre(string genre) { _view.GenreLabel = _model.AddGenreToTemp(genre); }
        private void ClearGenres() { _view.GenreLabel = _model.ClearTempGenres(); }

        // POPRAWIONA METODA: Czyści Model ORAZ Widok
        private void ClearForm()
        {
            _selectedBookId = null;

            // 1. Wyczyść Model (listy tymczasowe)
            _model.ClearTempAuthors();
            _model.ClearTempGenres();

            // 2. Wyczyść Widok (pola tekstowe, data, etc.)
            _view.ClearInputs();
            _view.SetEditMode(false);
        }
    }
}