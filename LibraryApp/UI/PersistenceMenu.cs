namespace LibraryApp.UI;

/// <summary>
/// Submenú de persistencia de datos.
/// </summary>
public static class PersistenceMenu
{
    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "💾",
                "GUARDAR / CARGAR DATOS",
                "Gestiona la persistencia del sistema"
            );

            ConsoleHelper.PrintMenuOption("1", "💾", "Guardar datos");
            ConsoleHelper.PrintMenuOption("2", "📂", "Cargar datos");
            ConsoleHelper.PrintMenuOption("3", "🔁", "Reiniciar datos");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int option = ConsoleHelper.ReadInt(0, 3);

            switch (option)
            {
                case 1:
                    SaveData();
                    break;
                case 2:
                    LoadData();
                    break;
                case 3:
                    ResetDataFlow();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void SaveData()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("💾", "GUARDAR DATOS");
        ConsoleHelper.PrintStub("SaveData — Serializa y guarda los datos en archivo local");
        ConsoleHelper.PressAnyKey();
    }

    private static void LoadData()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📂", "CARGAR DATOS");
        ConsoleHelper.PrintStub("LoadData — Carga los datos desde el archivo local");
        ConsoleHelper.PressAnyKey();
    }

    private static void ResetDataFlow()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔁", "REINICIAR DATOS");
        ConsoleHelper.PrintWarning("Esta acción eliminará TODOS los datos del sistema.");
        bool confirm = ConsoleHelper.AskConfirmation(
            "¿Estás seguro de que deseas reiniciar todos los datos?"
        );
        if (confirm)
        {
            ResetData();
            ConsoleHelper.PrintSuccess("Datos reiniciados correctamente. [Stub]");
        }
        else
        {
            ConsoleHelper.PrintInfo("Operación cancelada. No se realizaron cambios.");
        }
        ConsoleHelper.PressAnyKey();
    }

    private static void ResetData()
    {
        ConsoleHelper.PrintStub("ResetData — Limpia todas las colecciones del sistema");
    }
}
