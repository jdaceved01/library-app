namespace LibraryApp.Models;

/// <summary>
/// Representa un libro del catálogo de la biblioteca.
/// </summary>
public class Book
{
    // ── Propiedades ────────────────────────────────────────────────────────
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }

    // ── Constructor completo ───────────────────────────────────────────────
    public Book(int id, string title, string author, string isbn, int year, string category)
    {
        Id = id;
        Title = title;
        Author = author;
        Isbn = isbn;
        Year = year;
        Category = category;
        IsAvailable = true; // todo libro empieza disponible
    }

    // ── Constructor vacío ──────────────────────────────────────────────────
    public Book()
    {
        IsAvailable = true;
    }

    // ── Métodos ────────────────────────────────────────────────────────────

    /// <summary>Resumen de una línea para listar.</summary>
    public string ShortSummary() =>
        $"[{Id:D3}] {Title} — {Author} ({Year}) | {Category} | {(IsAvailable ? "✅ Disponible" : "🔄 Prestado")}";

    /// <summary>Detalle completo del libro.</summary>
    public string FullDetail()
    {
        return $"  ┌────────────────────────────────────────────────\n"
            + $"  │  📖  DETALLE DEL LIBRO\n"
            + $"  ├────────────────────────────────────────────────\n"
            + $"  │  ID        : {Id:D3}\n"
            + $"  │  Título    : {Title}\n"
            + $"  │  Autor     : {Author}\n"
            + $"  │  ISBN      : {Isbn}\n"
            + $"  │  Año       : {Year}\n"
            + $"  │  Categoría : {Category}\n"
            + $"  │  Estado    : {(IsAvailable ? "✅ Disponible" : "🔄 Prestado")}\n"
            + $"  └────────────────────────────────────────────────";
    }

    public override string ToString() => ShortSummary();
}
