namespace LibraryApp.UI;

/// <summary>
/// Submenú de gestión de préstamos.
/// </summary>
public static class LoansMenu
{
    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "🔄",
                "GESTIÓN DE PRÉSTAMOS",
                "Crea y administra los préstamos"
            );

            ConsoleHelper.PrintMenuOption("1", "➕", "Crear préstamo");
            ConsoleHelper.PrintMenuOption("2", "📋", "Listar préstamos");
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle de préstamo (por ID)");
            ConsoleHelper.PrintMenuOption("4", "↩️ ", "Registrar devolución");
            ConsoleHelper.PrintMenuOption("5", "🗑️ ", "Eliminar préstamo");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int option = ConsoleHelper.ReadInt(0, 5);

            switch (option)
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
        ConsoleHelper.PrintSectionHeader("➕", "CREAR PRÉSTAMO");
        ConsoleHelper.PrintStub("CreateLoan — Registra un nuevo préstamo");
        ConsoleHelper.PrintInfo("Validaciones que se aplicarán:");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("     • El libro debe existir en el catálogo.");
        Console.WriteLine("     • El libro debe estar disponible (no prestado).");
        Console.WriteLine("     • El usuario debe existir y estar activo.");
        Console.WriteLine("     • El usuario no debe tener préstamos vencidos pendientes.");
        Console.WriteLine("     • Se registrará fecha de inicio y fecha límite de devolución.");
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ListLoansMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("📋", "LISTAR PRÉSTAMOS", "Filtra por estado");

            ConsoleHelper.PrintMenuOption("1", "📑", "Todos los préstamos");
            ConsoleHelper.PrintMenuOption("2", "🟢", "Préstamos activos");
            ConsoleHelper.PrintMenuOption("3", "✅", "Préstamos cerrados / devueltos");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int option = ConsoleHelper.ReadInt(0, 3);

            switch (option)
            {
                case 1:
                    ListLoansAll();
                    break;
                case 2:
                    ListLoansActive();
                    break;
                case 3:
                    ListLoansClosed();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void ListLoansAll()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📑", "TODOS LOS PRÉSTAMOS");
        ConsoleHelper.PrintStub("ListLoansAll — Lista completa de préstamos");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListLoansActive()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🟢", "PRÉSTAMOS ACTIVOS");
        ConsoleHelper.PrintStub("ListLoansActive — Préstamos con estado Activo");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListLoansClosed()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("✅", "PRÉSTAMOS CERRADOS");
        ConsoleHelper.PrintStub("ListLoansClosed — Préstamos devueltos o vencidos");
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewLoanDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE PRÉSTAMO");
        ConsoleHelper.PrintStub("ViewLoanDetail — Busca préstamo por ID");
        ConsoleHelper.PressAnyKey();
    }

    private static void RegisterReturn()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("↩️", "REGISTRAR DEVOLUCIÓN");
        ConsoleHelper.PrintStub(
            "RegisterReturn — Marca préstamo como devuelto y libro como disponible"
        );
        ConsoleHelper.PressAnyKey();
    }

    private static void DeleteLoan()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR PRÉSTAMO");
        ConsoleHelper.PrintStub("DeleteLoan — Elimina registro de préstamo");
        ConsoleHelper.PrintWarning("Solo se puede eliminar si el préstamo ya fue devuelto.");
        ConsoleHelper.PressAnyKey();
    }
}
