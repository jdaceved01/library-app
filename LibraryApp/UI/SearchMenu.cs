using LibraryApp.Services;

namespace LibraryApp.UI;

public static class SearchMenu
{
    private static BookService _bookService = null!;
    private static UserService _userService = null!;
    private static LoanService _loanService = null!;

    public static void Init(BookService bs, UserService us, LoanService ls)
    {
        _bookService = bs;
        _userService = us;
        _loanService = ls;
    }

    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("🔍", "BÚSQUEDAS Y REPORTES");
            ConsoleHelper.PrintMenuOption("1", "📖", "Buscar libro");
            ConsoleHelper.PrintMenuOption("2", "👤", "Buscar usuario");
            ConsoleHelper.PrintMenuOption("3", "📊", "Reportes");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int opt = ConsoleHelper.ReadInt(0, 3);
            switch (opt)
            {
                case 1:
                    SearchBook();
                    break;
                case 2:
                    SearchUser();
                    break;
                case 3:
                    ReportsMenu();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void SearchBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📖", "BUSCAR LIBRO");
        ConsoleHelper.PrintPrompt("Ingresa título, autor, ISBN o categoría");
        string term = Console.ReadLine() ?? "";
        var results = _bookService.Search(term);
        Console.WriteLine();
        if (results.Count == 0)
            ConsoleHelper.PrintInfo("No se encontraron resultados.");
        else
        {
            ConsoleHelper.PrintSuccess($"Se encontraron {results.Count} resultado(s):");
            Console.WriteLine();
            foreach (var b in results)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  {b.ShortSummary()}");
            }
            Console.ResetColor();
        }
        ConsoleHelper.PressAnyKey();
    }

    private static void SearchUser()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("👤", "BUSCAR USUARIO");
        ConsoleHelper.PrintPrompt("Ingresa nombre, documento o email");
        string term = Console.ReadLine() ?? "";
        var results = _userService.Search(term);
        Console.WriteLine();
        if (results.Count == 0)
            ConsoleHelper.PrintInfo("No se encontraron resultados.");
        else
        {
            ConsoleHelper.PrintSuccess($"Se encontraron {results.Count} resultado(s):");
            Console.WriteLine();
            foreach (var u in results)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  {u.ShortSummary()}");
            }
            Console.ResetColor();
        }
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportsMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("📊", "REPORTES Y ESTADÍSTICAS");
            ConsoleHelper.PrintMenuOption("1", "👤", "Reporte por usuario");
            ConsoleHelper.PrintMenuOption("2", "📖", "Reporte por libro");
            ConsoleHelper.PrintMenuOption("3", "⏰", "Préstamos vencidos");
            ConsoleHelper.PrintMenuOption("4", "📈", "Resumen general del sistema");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-4]");
            int opt = ConsoleHelper.ReadInt(0, 4);
            switch (opt)
            {
                case 1:
                    ReportByUser();
                    break;
                case 2:
                    ReportByBook();
                    break;
                case 3:
                    ReportOverdue();
                    break;
                case 4:
                    ReportSummary();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void ReportByUser()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("👤", "REPORTE POR USUARIO");
        ConsoleHelper.PrintPrompt("ID del usuario");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var user = _userService.FindById(id);
        if (user == null)
        {
            ConsoleHelper.PrintError("Usuario no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var loans = _loanService.GetByUser(id);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(user.FullDetail());
        Console.ResetColor();
        Console.WriteLine();
        ConsoleHelper.PrintInfo($"Préstamos totales: {loans.Count}");
        foreach (var l in loans)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {l.ShortSummary()}");
        }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportByBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📖", "REPORTE POR LIBRO");
        ConsoleHelper.PrintPrompt("ID del libro");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var book = _bookService.FindById(id);
        if (book == null)
        {
            ConsoleHelper.PrintError("Libro no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var loans = _loanService.GetByBook(id);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(book.FullDetail());
        Console.ResetColor();
        Console.WriteLine();
        ConsoleHelper.PrintInfo($"Préstamos asociados: {loans.Count}");
        foreach (var l in loans)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {l.ShortSummary()}");
        }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportOverdue()
    {
        ConsoleHelper.PrintAppHeader();
        var overdue = _loanService.GetOverdue();
        ConsoleHelper.PrintSectionHeader("⏰", "PRÉSTAMOS VENCIDOS", $"Total: {overdue.Count}");
        if (overdue.Count == 0)
            ConsoleHelper.PrintSuccess("No hay préstamos vencidos. ¡Todo en orden!");
        else
            foreach (var l in overdue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {l.ShortSummary()}");
            }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportSummary()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📈", "RESUMEN GENERAL DEL SISTEMA");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();
        Console.WriteLine("  ┌── 📖 LIBROS ──────────────────────────────────────");
        Console.WriteLine($"  │   Total          : {_bookService.TotalBooks()}");
        Console.WriteLine($"  │   Disponibles    : {_bookService.TotalAvailable()}");
        Console.WriteLine($"  │   Prestados      : {_bookService.TotalBorrowed()}");
        Console.WriteLine();
        Console.WriteLine("  ├── 👤 USUARIOS ─────────────────────────────────────");
        Console.WriteLine($"  │   Total          : {_userService.TotalUsers()}");
        Console.WriteLine($"  │   Activos        : {_userService.TotalActive()}");
        Console.WriteLine($"  │   Inactivos      : {_userService.TotalInactive()}");
        Console.WriteLine();
        Console.WriteLine("  ├── 🔄 PRÉSTAMOS ────────────────────────────────────");
        Console.WriteLine($"  │   Total          : {_loanService.TotalLoans()}");
        Console.WriteLine($"  │   Activos        : {_loanService.TotalActive()}");
        Console.WriteLine($"  │   Devueltos      : {_loanService.TotalReturned()}");
        Console.WriteLine($"  │   Vencidos       : {_loanService.TotalOverdue()}");
        Console.WriteLine($"  │   Prom. días     : {_loanService.AverageLoanDays():F1}");
        Console.WriteLine("  └────────────────────────────────────────────────────");
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }
}
