using System.Diagnostics;
using LibraryApp.View;
using LibraryApp.Models;
using LibraryApp.Presenters;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data; //

namespace LibraryApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Konfiguracja bazy danych
            var connectionString = "Data Source=Library.db";
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite(connectionString);

            // Upewnij siê, ¿e baza danych istnieje
            using (var context = new LibraryContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }

            // Inicjalizacja g³ównego formularza i repozytorium
            var mainForm = new MainForm(); // Dawniej BookForm
            var repository = new BookRepository();

            // Debug: Sprawdzenie po³¹czenia (opcjonalne)
            // U¿ywamy Filter z pustymi parametrami, aby pobraæ wszystkie,
            // poniewa¿ GetAll() zosta³o zast¹pione przez Filter() w nowym repozytorium
            var allBooksCount = repository.Filter("", "", "", "", "", "", "").Count;
            Debug.WriteLine($"Books in DB: {allBooksCount}");

            // Inicjalizacja g³ównego prezentera
            // Prezenter sam podepnie zdarzenia widoku w swoim konstruktorze
            var presenter = new LibraryPresenter(mainForm, repository);

            // Uruchomienie aplikacji
            Application.Run(mainForm);
        }
    }
}