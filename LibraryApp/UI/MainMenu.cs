namespace LibraryApp.UI;

/// <summary>
/// Menú principal de la aplicación.
/// </summary>
public static class MainMenu
{
    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "🏠",
                "MENÚ PRINCIPAL",
                "Selecciona una opción para continuar"
            );

            ConsoleHelper.PrintMenuOption(
                "1",
                "📖",
                "Libros             — Registrar, listar, editar, eliminar"
            );
            ConsoleHelper.PrintMenuOption(
                "2",
                "👤",
                "Usuarios           — Gestión de usuarios del sistema"
            );
            ConsoleHelper.PrintMenuOption(
                "3",
                "🔄",
                "Préstamos          — Crear, listar y gestionar préstamos"
            );
            ConsoleHelper.PrintMenuOption(
                "4",
                "🔍",
                "Búsquedas          — Buscar libros, usuarios y reportes"
            );
            ConsoleHelper.PrintMenuOption("5", "💾", "Guardar / Cargar   — Persistencia de datos");
            ConsoleHelper.PrintSeparator();
            ConsoleHelper.PrintMenuOption("6", "🚪", "Salir              — Cerrar la aplicación");

            Console.WriteLine();
            ConsoleHelper.PrintPrompt("Selecciona una opción [1-6]");
            int option = ConsoleHelper.ReadInt(1, 6);

            switch (option)
            {
                case 1:
                    BooksMenu.Show();
                    break;
                case 2:
                    UsersMenu.Show();
                    break;
                case 3:
                    LoansMenu.Show();
                    break;
                case 4:
                    SearchMenu.Show();
                    break;
                case 5:
                    PersistenceMenu.Show();
                    break;
                case 6:
                    running = ConfirmExitAndSave();
                    break;
            }
        }
    }

    private static bool ConfirmExitAndSave()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🚪", "SALIR DE LA APLICACIÓN");
        bool save = ConsoleHelper.AskConfirmation("¿Deseas guardar los datos antes de salir?");
        if (save)
            ConsoleHelper.PrintSuccess(
                "Datos guardados correctamente. [Stub — se implementa en EV08]"
            );

        bool confirm = ConsoleHelper.AskConfirmation("¿Confirmas que deseas salir?");
        if (confirm)
        {
            ConsoleHelper.PrintAppHeader();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║                                                          ║");
            Console.WriteLine("  ║   👋  ¡Hasta pronto! Gracias por usar Library App       ║");
            Console.WriteLine("  ║                                                          ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            return true; // running = false → sale del loop
        }
        return false; // running = true  → vuelve al menú
    }
}
