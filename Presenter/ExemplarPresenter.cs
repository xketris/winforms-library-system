using LibraryApp.Models;
using LibraryApp.Models.Services;
using LibraryApp.View;

namespace LibraryApp.Presenters
{
    public class ExemplarPresenter
    {
        private readonly ExemplarsForm _view;
        private readonly BorrowingService _service;

        public ExemplarPresenter(ExemplarsForm view, BorrowingService service)
        {
            _view = view;
            _service = service;
            _view.OnBorrow += BorrowBook;
            _view.OnReturn += ReturnBook;
        }

        private void BorrowBook(Exemplar exemplar)
        {
            if (UserSession.IsLoggedIn)
                _service.BorrowBook(exemplar.BookId, UserSession.UserId.Value);
        }

        private void ReturnBook(Exemplar exemplar)
        {
            if (UserSession.IsLoggedIn)
                _service.ReturnBook(exemplar.BookId, UserSession.UserId.Value);
        }
    }
}