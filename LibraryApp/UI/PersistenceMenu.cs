using System.Text.Json;
using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.UI;

public static class PersistenceMenu
{
    private static BookService _bookService = null!;
    private static UserService _userService = null!;
    private static LoanService _loanService = null!;

    private const string DataFolder = "data";
    private const string BooksFile = "data/books.json";
    private const string UsersFile = "data/users.json";
    private const string LoansFile = "data/loans.json";

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
            ConsoleHelper.PrintSectionHeader("💾", "GUARDAR / CARGAR DATOS");
            ConsoleHelper.PrintMenuOption("1", "💾", "Guardar datos");
            ConsoleHelper.PrintMenuOption("2", "📂", "Cargar datos");
            ConsoleHelper.PrintMenuOption("3", "🔁", "Reiniciar datos");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int opt = ConsoleHelper.ReadInt(0, 3);
            switch (opt)
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

    public static void SaveData()
    {
        try
        {
            Directory.CreateDirectory(DataFolder);
            var opts = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(BooksFile, JsonSerializer.Serialize(_bookService.GetRaw(), opts));
            File.WriteAllText(UsersFile, JsonSerializer.Serialize(_userService.GetRaw(), opts));
            File.WriteAllText(LoansFile, JsonSerializer.Serialize(_loanService.GetRaw(), opts));
            ConsoleHelper.PrintSuccess($"Datos guardados en carpeta '{DataFolder}/'.");
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Error al guardar: {ex.Message}");
        }
        ConsoleHelper.PressAnyKey();
    }

    private static void LoadData()
    {
        try
        {
            if (!File.Exists(BooksFile))
            {
                ConsoleHelper.PrintWarning("No se encontraron archivos de datos guardados.");
                ConsoleHelper.PressAnyKey();
                return;
            }
            var books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(BooksFile));
            var users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(UsersFile));
            var loans = JsonSerializer.Deserialize<List<Loan>>(File.ReadAllText(LoansFile));
            if (books != null)
                _bookService.LoadFrom(books);
            if (users != null)
                _userService.LoadFrom(users);
            if (loans != null)
                _loanService.LoadFrom(loans);
            ConsoleHelper.PrintSuccess("Datos cargados correctamente.");
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Error al cargar: {ex.Message}");
        }
        ConsoleHelper.PressAnyKey();
    }

    private static void ResetDataFlow()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔁", "REINICIAR DATOS");
        ConsoleHelper.PrintWarning("Esta acción eliminará TODOS los datos del sistema.");
        if (ConsoleHelper.AskConfirmation("¿Estás seguro?"))
        {
            _bookService.LoadFrom(new List<Book>());
            _userService.LoadFrom(new List<User>());
            _loanService.LoadFrom(new List<Loan>());
            if (File.Exists(BooksFile))
                File.Delete(BooksFile);
            if (File.Exists(UsersFile))
                File.Delete(UsersFile);
            if (File.Exists(LoansFile))
                File.Delete(LoansFile);
            ConsoleHelper.PrintSuccess("Todos los datos han sido eliminados.");
        }
        else
            ConsoleHelper.PrintInfo("Operación cancelada.");
        ConsoleHelper.PressAnyKey();
    }
}
