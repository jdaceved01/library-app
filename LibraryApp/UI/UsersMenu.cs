using LibraryApp.Models;

namespace LibraryApp.UI;

public static class UsersMenu
{
    private static readonly List<User> _sampleUsers = new()
    {
        new User(1, "Ana Martínez", "1001234567", "ana@email.com", "311-111-1111"),
        new User(2, "Carlos Pérez", "1007654321", "carlos@email.com", "322-222-2222")
        {
            IsActive = false,
        },
    };

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
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle (por ID / Documento)");
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
        ConsoleHelper.PrintStub("RegisterUser — Se implementará con Service en EV08");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListUsers()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📋", "LISTAR USUARIOS", $"Total: {_sampleUsers.Count}");
        foreach (var user in _sampleUsers)
        {
            Console.ForegroundColor = user.IsActive ? ConsoleColor.Green : ConsoleColor.DarkGray;
            Console.WriteLine($"  {user.ShortSummary()}");
        }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewUserDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE USUARIO");
        ConsoleHelper.PrintPrompt("Ingresa el ID del usuario");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var user = _sampleUsers.Find(u => u.Id == id);
            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(user.FullDetail());
                Console.ResetColor();
            }
            else
                ConsoleHelper.PrintError($"No se encontró usuario con ID {id}.");
        }
        else
            ConsoleHelper.PrintError("ID inválido.");
        ConsoleHelper.PressAnyKey();
    }

    private static void UpdateUserMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("✏️", "ACTUALIZAR USUARIO");
            ConsoleHelper.PrintMenuOption("1", "📝", "Editar nombre");
            ConsoleHelper.PrintMenuOption("2", "📞", "Editar contacto");
            ConsoleHelper.PrintMenuOption("3", "🔘", "Activar / Desactivar");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int opt = ConsoleHelper.ReadInt(0, 3);
            switch (opt)
            {
                case 1:
                    ConsoleHelper.PrintStub("EditUserName");
                    ConsoleHelper.PressAnyKey();
                    break;
                case 2:
                    ConsoleHelper.PrintStub("EditUserContact");
                    ConsoleHelper.PressAnyKey();
                    break;
                case 3:
                    ConsoleHelper.PrintStub("ToggleUserActiveStatus");
                    ConsoleHelper.PressAnyKey();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void DeleteUser()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR USUARIO");
        ConsoleHelper.PrintStub("DeleteUser — Se implementará con Service en EV08");
        ConsoleHelper.PrintWarning("Validación: No permitir si tiene préstamos activos.");
        ConsoleHelper.PressAnyKey();
    }
}
