namespace LibraryApp.UI;

/// <summary>
/// Submenú de gestión de usuarios.
/// </summary>
public static class UsersMenu
{
    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "👤",
                "GESTIÓN DE USUARIOS",
                "Administra los usuarios del sistema"
            );

            ConsoleHelper.PrintMenuOption("1", "➕", "Registrar usuario");
            ConsoleHelper.PrintMenuOption("2", "📋", "Listar usuarios");
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle de usuario (por ID / Documento)");
            ConsoleHelper.PrintMenuOption("4", "✏️ ", "Actualizar usuario");
            ConsoleHelper.PrintMenuOption("5", "🗑️ ", "Eliminar usuario");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int option = ConsoleHelper.ReadInt(0, 5);

            switch (option)
            {
                case 1:
                    RegisterUser();
                    break;
                case 2:
                    ListUsers();
                    break;
                case 3:
                    ViewUserDetail();
                    break;
                case 4:
                    UpdateUserMenu();
                    break;
                case 5:
                    DeleteUser();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void RegisterUser()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("➕", "REGISTRAR USUARIO");
        ConsoleHelper.PrintStub("RegisterUser — Registra un nuevo usuario");
        ConsoleHelper.PrintInfo("Se solicitarán: Nombre, Documento, Email, Teléfono.");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListUsers()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📋", "LISTAR USUARIOS");
        ConsoleHelper.PrintStub("ListUsers — Lista todos los usuarios registrados");
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewUserDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE USUARIO");
        ConsoleHelper.PrintStub("ViewUserDetail — Busca por ID o documento");
        ConsoleHelper.PressAnyKey();
    }

    private static void UpdateUserMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "✏️",
                "ACTUALIZAR USUARIO",
                "¿Qué campo deseas editar?"
            );

            ConsoleHelper.PrintMenuOption("1", "📝", "Editar nombre");
            ConsoleHelper.PrintMenuOption("2", "📞", "Editar contacto");
            ConsoleHelper.PrintMenuOption("3", "🔘", "Activar / Desactivar usuario");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int option = ConsoleHelper.ReadInt(0, 3);

            switch (option)
            {
                case 1:
                    EditUserName();
                    break;
                case 2:
                    EditUserContact();
                    break;
                case 3:
                    ToggleUserActiveStatus();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void EditUserName()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📝", "EDITAR NOMBRE DE USUARIO");
        ConsoleHelper.PrintStub("EditUserName — Actualiza el nombre del usuario por ID");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditUserContact()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📞", "EDITAR CONTACTO");
        ConsoleHelper.PrintStub("EditUserContact — Actualiza email y teléfono por ID");
        ConsoleHelper.PressAnyKey();
    }

    private static void ToggleUserActiveStatus()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔘", "ACTIVAR / DESACTIVAR USUARIO");
        ConsoleHelper.PrintStub("ToggleUserActiveStatus — Cambia estado activo/inactivo");
        ConsoleHelper.PressAnyKey();
    }

    private static void DeleteUser()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR USUARIO");
        ConsoleHelper.PrintStub("DeleteUser — Elimina usuario por ID");
        ConsoleHelper.PrintWarning(
            "Validación: No se permite eliminar si el usuario tiene préstamos activos."
        );
        ConsoleHelper.PressAnyKey();
    }
}
