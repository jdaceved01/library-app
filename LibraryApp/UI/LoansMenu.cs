using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.UI;

public static class LoansMenu
{
    private static LoanService _loanService = null!;
    private static BookService _bookService = null!;
    private static UserService _userService = null!;

    public static void Init(LoanService ls, BookService bs, UserService us)
    {
        _loanService = ls;
        _bookService = bs;
        _userService = us;
    }

    public static void Show()
    {
        _loanService.RefreshOverdueStatus();
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "🔄",
                "GESTIÓN DE PRÉSTAMOS",
                $"Total: {_loanService.TotalLoans()}  |  🟢 {_loanService.TotalActive()} activos  |  ⚠️ {_loanService.TotalOverdue()} vencidos"
            );

            ConsoleHelper.PrintMenuOption("1", "➕", "Crear préstamo");
            ConsoleHelper.PrintMenuOption("2", "📋", "Listar préstamos");
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle (por ID)");
            ConsoleHelper.PrintMenuOption("4", "↩️ ", "Registrar devolución");
            ConsoleHelper.PrintMenuOption("5", "🗑️ ", "Eliminar préstamo");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int opt = ConsoleHelper.ReadInt(0, 5);
            switch (opt)
            {
                case 1:
                    CreateLoan();
                    break;
                case 2:
                    ListLoansMenu();
                    break;
                case 3:
                    ViewLoanDetail();
                    break;
                case 4:
                    RegisterReturn();
                    break;
                case 5:
                    DeleteLoan();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void CreateLoan()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("➕", "CREAR NUEVO PRÉSTAMO");

        ConsoleHelper.PrintPrompt("ID del libro");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var book = _bookService.FindById(bookId);
        if (book == null)
        {
            ConsoleHelper.PrintError("Libro no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (!book.IsAvailable)
        {
            ConsoleHelper.PrintError("El libro no está disponible actualmente.");
            ConsoleHelper.PressAnyKey();
            return;
        }

        ConsoleHelper.PrintPrompt("ID del usuario");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var user = _userService.FindById(userId);
        if (user == null)
        {
            ConsoleHelper.PrintError("Usuario no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (!user.IsActive)
        {
            ConsoleHelper.PrintError("El usuario está inactivo.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (_loanService.HasActiveLoans(userId))
        {
            ConsoleHelper.PrintWarning("El usuario ya tiene un préstamo activo.");
        }

        ConsoleHelper.PrintPrompt("Días de préstamo [ej: 14]");
        int.TryParse(Console.ReadLine(), out int days);
        if (days <= 0)
            days = 14;

        var loan = new Loan(0, book.Id, book.Title, user.Id, user.Name, DateTime.Now.AddDays(days));
        _loanService.Add(loan);
        _bookService.SetAvailability(book.Id, false);

        ConsoleHelper.PrintSuccess(
            $"Préstamo creado. ID [{loan.Id:D3}] — Vence: {loan.DueDate:dd/MM/yyyy}"
        );
        ConsoleHelper.PressAnyKey();
    }

    private static void ListLoansMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("📋", "LISTAR PRÉSTAMOS");
            ConsoleHelper.PrintMenuOption("1", "📑", "Todos");
            ConsoleHelper.PrintMenuOption("2", "🟢", "Activos");
            ConsoleHelper.PrintMenuOption("3", "✅", "Cerrados / Devueltos");
            ConsoleHelper.PrintMenuOption("4", "⚠️ ", "Vencidos");
            ConsoleHelper.PrintMenuOption("5", "📅", "Ordenados por fecha límite");
            ConsoleHelper.PrintBackOption();
            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int opt = ConsoleHelper.ReadInt(0, 5);
            switch (opt)
            {
                case 1:
                    PrintLoanList(_loanService.GetAll(), "TODOS LOS PRÉSTAMOS");
                    break;
                case 2:
                    PrintLoanList(
                        _loanService.GetActive(),
                        "PRÉSTAMOS ACTIVOS",
                        ConsoleColor.Green
                    );
                    break;
                case 3:
                    PrintLoanList(
                        _loanService.GetClosed(),
                        "PRÉSTAMOS CERRADOS",
                        ConsoleColor.Gray
                    );
                    break;
                case 4:
                    PrintLoanList(
                        _loanService.GetOverdue(),
                        "PRÉSTAMOS VENCIDOS",
                        ConsoleColor.Red
                    );
                    break;
                case 5:
                    PrintLoanList(_loanService.GetSortedByDueDate(), "ORDENADOS POR FECHA");
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void PrintLoanList(
        List<Loan> loans,
        string title,
        ConsoleColor color = ConsoleColor.White
    )
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📋", title, $"Total: {loans.Count}");
        if (loans.Count == 0)
            ConsoleHelper.PrintInfo("No hay préstamos en esta categoría.");
        else
            foreach (var l in loans)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"  {l.ShortSummary()}");
            }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewLoanDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE PRÉSTAMO");
        ConsoleHelper.PrintPrompt("ID del préstamo");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var loan = _loanService.FindById(id);
            if (loan != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(loan.FullDetail());
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"\n  EstaVencido()       : {loan.IsOverdue()}");
                Console.WriteLine($"  DiasTranscurridos() : {loan.DaysElapsed()} días");
                Console.ResetColor();
            }
            else
                ConsoleHelper.PrintError("Préstamo no encontrado.");
        }
        else
            ConsoleHelper.PrintError("ID inválido.");
        ConsoleHelper.PressAnyKey();
    }

    private static void RegisterReturn()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("↩️", "REGISTRAR DEVOLUCIÓN");
        ConsoleHelper.PrintPrompt("ID del préstamo");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var loan = _loanService.FindById(id);
        if (loan == null)
        {
            ConsoleHelper.PrintError("Préstamo no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (loan.Status != LoanStatus.Active)
        {
            ConsoleHelper.PrintError("Este préstamo no está activo.");
            ConsoleHelper.PressAnyKey();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(loan.FullDetail());
        Console.ResetColor();

        if (ConsoleHelper.AskConfirmation("¿Confirmas registrar la devolución?"))
        {
            _loanService.RegisterReturn(id);
            _bookService.SetAvailability(loan.BookId, true);
            ConsoleHelper.PrintSuccess("Devolución registrada. El libro ya está disponible.");
        }
        else
            ConsoleHelper.PrintInfo("Operación cancelada.");
        ConsoleHelper.PressAnyKey();
    }

    private static void DeleteLoan()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR PRÉSTAMO");
        ConsoleHelper.PrintPrompt("ID del préstamo");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var loan = _loanService.FindById(id);
        if (loan == null)
        {
            ConsoleHelper.PrintError("Préstamo no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (loan.Status == LoanStatus.Active)
        {
            ConsoleHelper.PrintError(
                "No se puede eliminar un préstamo activo. Primero regístralo como devuelto."
            );
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (ConsoleHelper.AskConfirmation("¿Confirmas eliminar este préstamo?"))
        {
            _loanService.Delete(id);
            ConsoleHelper.PrintSuccess("Préstamo eliminado.");
        }
        else
            ConsoleHelper.PrintInfo("Operación cancelada.");
        ConsoleHelper.PressAnyKey();
    }
}
