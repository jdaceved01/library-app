namespace LibraryApp.Models;

/// <summary>
/// Estado del préstamo en el ciclo de vida.
/// </summary>
public enum LoanStatus
{
    Active, // Préstamo activo, dentro del plazo
    Returned, // Libro devuelto correctamente
    Overdue, // Plazo vencido sin devolución
}
