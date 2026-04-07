using LibraryApp.Models;

namespace LibraryApp.Services;

/// <summary>
/// Servicio de gestión de libros. Contiene la lista y toda la lógica de negocio.
/// </summary>
public class BookService
{
    private readonly List<Book> _books = new();

    // ── CRUD ───────────────────────────────────────────────────────────────

    public void Add(Book book)
    {
        book.Id = IdGenerator.NextBookId();
        _books.Add(book);
    }

    public bool Remove(int id)
    {
        var book = FindById(id);
        if (book == null)
            return false;
        if (!book.IsAvailable)
            return false; // no se elimina si está prestado
        _books.Remove(book);
        return true;
    }

    public List<Book> GetAll() => new(_books);

    public List<Book> GetAvailable() => _books.FindAll(b => b.IsAvailable);

    public List<Book> GetBorrowed() => _books.FindAll(b => !b.IsAvailable);

    public Book? FindById(int id) => _books.Find(b => b.Id == id);

    public Book? FindByIsbn(string isbn) => _books.Find(b => b.Isbn == isbn);

    public List<Book> Search(string term)
    {
        string t = term.ToLower();
        return _books.FindAll(b =>
            b.Title.ToLower().Contains(t)
            || b.Author.ToLower().Contains(t)
            || b.Category.ToLower().Contains(t)
            || b.Isbn.Contains(t)
        );
    }

    // ── Ordenación ─────────────────────────────────────────────────────────

    public List<Book> GetSortedByTitle() => _books.OrderBy(b => b.Title).ToList();

    public List<Book> GetSortedByYear() => _books.OrderByDescending(b => b.Year).ToList();

    // ── Updates ────────────────────────────────────────────────────────────

    public bool UpdateTitle(int id, string newTitle)
    {
        var book = FindById(id);
        if (book == null)
            return false;
        book.Title = newTitle;
        return true;
    }

    public bool UpdateAuthor(int id, string newAuthor)
    {
        var book = FindById(id);
        if (book == null)
            return false;
        book.Author = newAuthor;
        return true;
    }

    public bool UpdateYearCategory(int id, int year, string category)
    {
        var book = FindById(id);
        if (book == null)
            return false;
        book.Year = year;
        book.Category = category;
        return true;
    }

    public void SetAvailability(int id, bool available)
    {
        var book = FindById(id);
        if (book != null)
            book.IsAvailable = available;
    }

    // ── KPIs ───────────────────────────────────────────────────────────────

    public int TotalBooks() => _books.Count;

    public int TotalAvailable() => _books.Count(b => b.IsAvailable);

    public int TotalBorrowed() => _books.Count(b => !b.IsAvailable);

    // ── Seed data (datos de ejemplo) ───────────────────────────────────────

    public void SeedData()
    {
        if (_books.Count > 0)
            return;
        Add(
            new Book
            {
                Title = "Cien Años de Soledad",
                Author = "Gabriel García Márquez",
                Isbn = "978-0-06-088328-7",
                Year = 1967,
                Category = "Novela",
            }
        );
        Add(
            new Book
            {
                Title = "El Principito",
                Author = "Antoine de Saint-Exupéry",
                Isbn = "978-0-15-601219-5",
                Year = 1943,
                Category = "Fábula",
            }
        );
        Add(
            new Book
            {
                Title = "Don Quijote de la Mancha",
                Author = "Miguel de Cervantes",
                Isbn = "978-84-376-0494-7",
                Year = 1605,
                Category = "Clásico",
            }
        );
        Add(
            new Book
            {
                Title = "1984",
                Author = "George Orwell",
                Isbn = "978-0-452-28423-4",
                Year = 1949,
                Category = "Distopía",
            }
        );
        Add(
            new Book
            {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Isbn = "978-0-13-235088-4",
                Year = 2008,
                Category = "Tecnología",
            }
        );
    }

    // ── Acceso a la lista interna (para persistencia) ──────────────────────
    public List<Book> GetRaw() => _books;

    public void LoadFrom(IEnumerable<Book> books)
    {
        _books.Clear();
        _books.AddRange(books);
    }
}
