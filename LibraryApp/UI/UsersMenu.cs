using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.UI;

public static class UsersMenu
{
    private static UserService _service = null!;

    public static void Init(UserService service) => _service = service;

    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "👤",
                "GESTIÓN DE USUARIOS",
                $"Total: {_service.TotalUsers()}  |  🟢 {_service.TotalActive()} activos  |  🔴 {_service.TotalInactive()} inactivos"
            );

            ConsoleHelper.PrintMenuOption("1", "➕", "Registrar usuario");
            ConsoleHelper.PrintMenuOption("2", "📋", "Listar usuarios");
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle (por ID)");
            ConsoleHelper.PrintMenuOption("4", "✏️ ", "Actualizar usuario");
            ConsoleHelper.PrintMenuOption("5", "🗑️ ", "Eliminar usuario");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int opt = ConsoleHelper.ReadInt(0, 5);
            switch (opt)
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
        ConsoleHelper.PrintSectionHeader("➕", "REGISTRAR NUEVO USUARIO");
        ConsoleHelper.PrintPrompt("Nombre completo");
        string name = Console.ReadLine() ?? "";
        ConsoleHelper.PrintPrompt("Documento de identidad");
        string doc = Console.ReadLine() ?? "";
        ConsoleHelper.PrintPrompt("Email");
        string email = Console.ReadLine() ?? "";
        ConsoleHelper.PrintPrompt("Teléfono");
        string phone = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(doc))
        {
            ConsoleHelper.PrintError("Nombre y documento son obligatorios.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var user = new User
        {
            Name = name,
            Document = doc,
            Email = email,
            Phone = phone,
        };
        _service.Add(user);
        ConsoleHelper.PrintSuccess($"Usuario registrado con ID [{user.Id:D3}].");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListUsers()
    {
        ConsoleHelper.PrintAppHeader();
        var users = _service.GetSortedByName();
        ConsoleHelper.PrintSectionHeader("📋", "TODOS LOS USUARIOS", $"Total: {users.Count}");
        if (users.Count == 0)
            ConsoleHelper.PrintInfo("No hay usuarios registrados.");
        else
            foreach (var u in users)
            {
                Console.ForegroundColor = u.IsActive ? ConsoleColor.Green : ConsoleColor.DarkGray;
                Console.WriteLine($"  {u.ShortSummary()}");
            }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewUserDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE USUARIO");
        ConsoleHelper.PrintPrompt("ID del usuario");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var u = _service.FindById(id);
            if (u != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(u.FullDetail());
                Console.ResetColor();
            }
            else
                ConsoleHelper.PrintError("Usuario no encontrado.");
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
            ConsoleHelper.PrintMenuOption("2", "📞", "Editar contacto (email / teléfono)");
            ConsoleHelper.PrintMenuOption("3", "🔘", "Activar / Desactivar");
            ConsoleHelper.PrintBackOption();
            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int opt = ConsoleHelper.ReadInt(0, 3);
            switch (opt)
            {
                case 1:
                    ConsoleHelper.PrintAppHeader();
                    ConsoleHelper.PrintSectionHeader("📝", "EDITAR NOMBRE");
                    ConsoleHelper.PrintPrompt("ID");
                    if (!int.TryParse(Console.ReadLine(), out int id1))
                        break;
                    ConsoleHelper.PrintPrompt("Nuevo nombre");
                    string name = Console.ReadLine() ?? "";
                    if (_service.UpdateName(id1, name))
                        ConsoleHelper.PrintSuccess("Nombre actualizado.");
                    else
                        ConsoleHelper.PrintError("Usuario no encontrado.");
                    ConsoleHelper.PressAnyKey();
                    break;
                case 2:
                    ConsoleHelper.PrintAppHeader();
                    ConsoleHelper.PrintSectionHeader("📞", "EDITAR CONTACTO");
                    ConsoleHelper.PrintPrompt("ID");
                    if (!int.TryParse(Console.ReadLine(), out int id2))
                        break;
                    ConsoleHelper.PrintPrompt("Nuevo email");
                    string email = Console.ReadLine() ?? "";
                    ConsoleHelper.PrintPrompt("Nuevo teléfono");
                    string phone = Console.ReadLine() ?? "";
                    if (_service.UpdateContact(id2, email, phone))
                        ConsoleHelper.PrintSuccess("Contacto actualizado.");
                    else
                        ConsoleHelper.PrintError("Usuario no encontrado.");
                    ConsoleHelper.PressAnyKey();
                    break;
                case 3:
                    ConsoleHelper.PrintAppHeader();
                    ConsoleHelper.PrintSectionHeader("🔘", "ACTIVAR / DESACTIVAR");
                    ConsoleHelper.PrintPrompt("ID");
                    if (!int.TryParse(Console.ReadLine(), out int id3))
                        break;
                    var u = _service.FindById(id3);
                    if (u == null)
                    {
                        ConsoleHelper.PrintError("Usuario no encontrado.");
                        ConsoleHelper.PressAnyKey();
                        break;
                    }
                    ConsoleHelper.PrintInfo(
                        $"Estado actual: {(u.IsActive ? "🟢 Activo" : "🔴 Inactivo")}"
                    );
                    if (_service.ToggleActive(id3))
                        ConsoleHelper.PrintSuccess(
                            $"Estado cambiado a: {(u.IsActive ? "🟢 Activo" : "🔴 Inactivo")}"
                        );
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
        ConsoleHelper.PrintPrompt("ID del usuario");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var u = _service.FindById(id);
        if (u == null)
        {
            ConsoleHelper.PrintError("Usuario no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(u.FullDetail());
        Console.ResetColor();
        if (ConsoleHelper.AskConfirmation("¿Confirmas eliminar este usuario?"))
        {
            // hasActiveLoans = false por ahora — LoanService lo valida en EV08 conectado
            if (_service.Remove(id, false))
                ConsoleHelper.PrintSuccess("Usuario eliminado.");
            else
                ConsoleHelper.PrintError("No se pudo eliminar.");
        }
        else
            ConsoleHelper.PrintInfo("Operación cancelada.");
        ConsoleHelper.PressAnyKey();
    }
}
