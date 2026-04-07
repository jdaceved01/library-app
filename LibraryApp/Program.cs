using LibraryApp.Services;
using LibraryApp.UI;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Title = "📚 Library App — Sistema de Gestión de Biblioteca";

// ── Instanciar servicios ────────────────────────────────────────────────────
var bookService = new BookService();
var userService = new UserService();
var loanService = new LoanService();

// ── Cargar datos de ejemplo ─────────────────────────────────────────────────
bookService.SeedData();
userService.SeedData();

// ── Inyectar servicios en los menús ─────────────────────────────────────────
BooksMenu.Init(bookService);
UsersMenu.Init(userService);
LoansMenu.Init(loanService, bookService, userService);
SearchMenu.Init(bookService, userService, loanService);
PersistenceMenu.Init(bookService, userService, loanService);

// ── Arrancar la aplicación ──────────────────────────────────────────────────
MainMenu.Show();
