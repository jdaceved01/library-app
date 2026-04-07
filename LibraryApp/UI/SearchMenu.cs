namespace LibraryApp.UI;

/// <summary>
/// Submenú de búsquedas y reportes.
/// </summary>
public static class SearchMenu
{
    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "🔍",
                "BÚSQUEDAS Y REPORTES",
                "Consulta y analiza la información"
            );

            ConsoleHelper.PrintMenuOption(
                "1",
                "📖",
                "Buscar libro (título / autor / ID / categoría)"
            );
            ConsoleHelper.PrintMenuOption("2", "👤", "Buscar usuario (nombre / ID / documento)");
            ConsoleHelper.PrintMenuOption("3", "📊", "Reportes");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int option = ConsoleHelper.ReadInt(0, 3);

            switch (option)
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
        ConsoleHelper.PrintStub("SearchBook — Busca por título, autor, ISBN o categoría");
        ConsoleHelper.PressAnyKey();
    }

    private static void SearchUser()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("👤", "BUSCAR USUARIO");
        ConsoleHelper.PrintStub("SearchUser — Busca por nombre, ID o documento");
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportsMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "📊",
                "REPORTES",
                "Estadísticas y análisis del sistema"
            );

            ConsoleHelper.PrintMenuOption("1", "👤", "Reporte por usuario");
            ConsoleHelper.PrintMenuOption("2", "📖", "Reporte por libro");
            ConsoleHelper.PrintMenuOption("3", "⏰", "Préstamos vencidos");
            ConsoleHelper.PrintMenuOption("4", "📈", "Resumen general del sistema");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-4]");
            int option = ConsoleHelper.ReadInt(0, 4);

            switch (option)
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
        ConsoleHelper.PrintStub("ReportByUser — Historial de préstamos por usuario");
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportByBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📖", "REPORTE POR LIBRO");
        ConsoleHelper.PrintStub("ReportByBook — Historial de préstamos por libro");
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportOverdue()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("⏰", "PRÉSTAMOS VENCIDOS");
        ConsoleHelper.PrintStub("ReportOverdue — Lista de préstamos vencidos sin devolver");
        ConsoleHelper.PressAnyKey();
    }

    private static void ReportSummary()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📈", "RESUMEN GENERAL");
        ConsoleHelper.PrintStub("ReportSummary — KPIs y estadísticas generales del sistema");
        ConsoleHelper.PressAnyKey();
    }
}
