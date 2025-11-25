# 📚 WinForms Library Management System

This project is a desktop-based Library Management System developed using **C#** and **Windows Forms (.NET 8.0)**. The application is designed to help libraries manage their book collection, users, and borrowing processes efficiently.

The system implements the **Model-View-Presenter (MVP)** architectural pattern to ensure a clean separation of concerns, testability, and maintainability. It utilizes **Entity Framework Core** with **SQLite** for lightweight, portable data persistence.

## Key Features

### User Panel
* **Authentication:** Secure Login and Registration system.
* **Dashboard:** View personal information, account balance, and current loans.
* **Book Browser:** Search and filter books by Title, Genre, Author, ISBN, Year, Type (Book/Comic/Album), and Cover Type.
* **Borrowing:** Users can view available copies and borrow books if eligible.
* **Profile Management:** Update contact details (Address, Phone, Email).

### Admin Panel
* **Book Management:** Add, Edit, and Delete books. Manage specific details like ISBN, release year, and quantity.
* **User Management:** View all registered users and **Ban/Unban** users to restrict borrowing privileges.
* **Announcements:** Create and manage library announcements visible to all users.
* **Inventory Control:** Track the status of specific book copies (Exemplars).

## Technology Stack

* **Language:** C#
* **Framework:** .NET 8.0 (Windows Forms)
* **Database:** SQLite
* **ORM:** Entity Framework Core 9.0.6
* **Architecture:** Model-View-Presenter (MVP)

## Project Structure

The solution follows the MVP pattern strictly:

* **Model:** Contains database entities (`/DB/Entities`), Data Transfer Objects (DTOs), Service logic (e.g., `BorrowingService`), and Database Context (`LibraryContext`). It handles all data processing and business rules.
* **View:** Contains Windows Forms and User Controls (`.cs`, `.Designer.cs`). These are passive views that only handle UI events and display data provided by the Presenter.
* **Presenter:** Acts as the middleman (`/Presenter`). It retrieves data from the Model, formats it, and updates the View. It also handles events triggered by the View (e.g., button clicks).

## Installation & Setup

1.  **Prerequisites:**
    * Visual Studio
    * .NET 8.0 SDK.

2.  **Clone the Repository:**
    ```bash
    git clone <repository-url>
    ```

3.  **Database Setup:**
    * No manual SQL setup is required. The application uses **Code-First** Entity Framework.
    * On the first run, the application will automatically generate the `Library.db` SQLite file and seed it with default data (Admin account and Genres).

4.  **Run the Application:**
    * Open `LibraryApp.sln` in Visual Studio.
    * Set the solution configuration to `Debug` or `Release`.
    * Press `F5` or click **Start**.

## Default Credentials

The database is seeded with a default Administrator account for testing purposes:

* **Email:** `admin@library.com`
* **Password:** `admin123`

## Usage Guide

1.  **Login/Register:** Start by logging in with the admin credentials or registering a new user account.
2.  **Browsing:** Use the filters at the top of the main window to find books.
3.  **Borrowing:** Click on a book in the grid to see available copies (Exemplars) and borrow them.
4.  **Admin Functions:** Log in as Admin to access the specific management tabs (Books, Users, Announcements) via the navigation buttons.
