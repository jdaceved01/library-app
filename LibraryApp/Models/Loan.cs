namespace LibraryApp.Models;

/// <summary>
/// Representa un préstamo de un libro a un usuario.
/// </summary>
public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // null = no devuelto aún
    public LoanStatus Status { get; set; }

    // ── Constructor completo ───────────────────────────────────────────────
    public Loan(int id, int bookId, string bookTitle, int userId, string userName, DateTime dueDate)
    {
        Id = id;
        BookId = bookId;
        BookTitle = bookTitle;
        UserId = userId;
        UserName = userName;
        StartDate = DateTime.Now;
        DueDate = dueDate;
        ReturnDate = null;
        Status = LoanStatus.Active;
    }

    // ── Constructor vacío ──────────────────────────────────────────────────
    public Loan()
    {
        StartDate = DateTime.Now;
        Status = LoanStatus.Active;
        ReturnDate = null;
    }

    // ── Métodos ────────────────────────────────────────────────────────────

    public bool IsOverdue() => Status == LoanStatus.Active && DateTime.Now > DueDate;

    public int DaysElapsed() => (int)(DateTime.Now - StartDate).TotalDays;

    public int DaysUntilDue() => (int)(DueDate - DateTime.Now).TotalDays;

    public string ShortSummary()
    {
        string statusIcon = Status switch
        {
            LoanStatus.Active => IsOverdue() ? "⚠️  Vencido" : "🟢 Activo",
            LoanStatus.Returned => "✅ Devuelto",
            LoanStatus.Overdue => "🔴 Vencido",
            _ => "?",
        };
        return $"[{Id:D3}] {BookTitle} → {UserName} | Vence: {DueDate:dd/MM/yyyy} | {statusIcon}";
    }

    public string FullDetail()
    {
        string statusLabel = Status switch
        {
            LoanStatus.Active => IsOverdue() ? "⚠️  Vencido (sin registrar)" : "🟢 Activo",
            LoanStatus.Returned => "✅ Devuelto",
            LoanStatus.Overdue => "🔴 Vencido",
            _ => "Desconocido",
        };

        return $"  ┌────────────────────────────────────────────────\n"
            + $"  │  🔄  DETALLE DEL PRÉSTAMO\n"
            + $"  ├────────────────────────────────────────────────\n"
            + $"  │  ID Préstamo : {Id:D3}\n"
            + $"  │  Libro       : [{BookId:D3}] {BookTitle}\n"
            + $"  │  Usuario     : [{UserId:D3}] {UserName}\n"
            + $"  │  Inicio      : {StartDate:dd/MM/yyyy HH:mm}\n"
            + $"  │  Vence       : {DueDate:dd/MM/yyyy}\n"
            + $"  │  Devolución  : {(ReturnDate.HasValue ? ReturnDate.Value.ToString("dd/MM/yyyy HH:mm") : "— Pendiente —")}\n"
            + $"  │  Días trans. : {DaysElapsed()}\n"
            + $"  │  Estado      : {statusLabel}\n"
            + $"  └────────────────────────────────────────────────";
    }

    public override string ToString() => ShortSummary();
}
