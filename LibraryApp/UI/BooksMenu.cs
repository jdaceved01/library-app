using LibraryApp.Models;

namespace LibraryApp.UI;

public static class BooksMenu
{
    // Datos de prueba — serán reemplazados por el Service en EV08
    private static readonly List<Book> _sampleBooks = new()
    {
        new Book(
            1,
            "Cien Años de Soledad",
            "Gabriel García Márquez",
            "978-0-06-088328-7",
            1967,
            "Novela"
        ),
        new Book(
            2,
            "El Principito",
            "Antoine de Saint-Exupéry",
            "978-0-15-601219-5",
            1943,
            "Fábula"
        )
        {
            IsAvailable = false,
        },
        new Book(
            3,
            "Don Quijote de la Mancha",
            "Miguel de Cervantes",
            "978-84-376-0494-7",
            1605,
            "Clásico"
        ),
    };

    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "📖",
                "GESTIÓN DE LIBROS",
                "Administra el catálogo de la biblioteca"
            );

            ConsoleHelper.PrintMenuOption("1", "➕", "Registrar libro");
            ConsoleHelper.PrintMenuOption("2", "📋", "Listar libros");
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle de un libro (por ID / ISBN)");
            ConsoleHelper.PrintMenuOption("4", "✏️ ", "Actualizar libro");
            ConsoleHelper.PrintMenuOption("5", "🗑️ ", "Eliminar libro");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int option = ConsoleHelper.ReadInt(0, 5);

            switch (option)
            {
                case 1:
                    RegisterBook();
                    break;
                case 2:
                    ListBooksMenu();
                    break;
                case 3:
                    ViewBookDetail();
                    break;
                case 4:
                    UpdateBookMenu();
                    break;
                case 5:
                    DeleteBook();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void RegisterBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("➕", "REGISTRAR LIBRO");
        ConsoleHelper.PrintStub("RegisterBook — Se implementará con Service en EV08");
        ConsoleHelper.PrintInfo("Campos: Título, Autor, ISBN, Año, Categoría.");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("📋", "LISTAR LIBROS", "Elige el filtro");
            ConsoleHelper.PrintMenuOption("1", "📚", "Listar todos");
            ConsoleHelper.PrintMenuOption("2", "✅", "Listar disponibles");
            ConsoleHelper.PrintMenuOption("3", "🔄", "Listar prestados");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int opt = ConsoleHelper.ReadInt(0, 3);
            switch (opt)
            {
                case 1:
                    ListBooksAll();
                    break;
                case 2:
                    ListBooksAvailable();
                    break;
                case 3:
                    ListBooksBorrowed();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void ListBooksAll()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader(
            "📚",
            "TODOS LOS LIBROS",
            $"Total: {_sampleBooks.Count} libros"
        );
        foreach (var book in _sampleBooks)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {book.ShortSummary()}");
        }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksAvailable()
    {
        ConsoleHelper.PrintAppHeader();
        var available = _sampleBooks.FindAll(b => b.IsAvailable);
        ConsoleHelper.PrintSectionHeader("✅", "LIBROS DISPONIBLES", $"Total: {available.Count}");
        foreach (var book in available)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  {book.ShortSummary()}");
        }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksBorrowed()
    {
        ConsoleHelper.PrintAppHeader();
        var borrowed = _sampleBooks.FindAll(b => !b.IsAvailable);
        ConsoleHelper.PrintSectionHeader("🔄", "LIBROS PRESTADOS", $"Total: {borrowed.Count}");
        foreach (var book in borrowed)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  {book.ShortSummary()}");
        }
        Console.ResetColor();
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewBookDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE LIBRO");
        ConsoleHelper.PrintPrompt("Ingresa el ID del libro");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var book = _sampleBooks.Find(b => b.Id == id);
            if (book != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(book.FullDetail());
                Console.ResetColor();
            }
            else
                ConsoleHelper.PrintError($"No se encontró libro con ID {id}.");
        }
        else
            ConsoleHelper.PrintError("ID inválido.");
        ConsoleHelper.PressAnyKey();
    }

    private static void UpdateBookMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("✏️", "ACTUALIZAR LIBRO");
            ConsoleHelper.PrintMenuOption("1", "📝", "Editar título");
            ConsoleHelper.PrintMenuOption("2", "👤", "Editar autor");
            ConsoleHelper.PrintMenuOption("3", "📅", "Editar año / categoría");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int opt = ConsoleHelper.ReadInt(0, 3);
            switch (opt)
            {
                case 1:
                    EditBookTitle();
                    break;
                case 2:
                    EditBookAuthor();
                    break;
                case 3:
                    EditBookYearCategory();
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void EditBookTitle()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📝", "EDITAR TÍTULO");
        ConsoleHelper.PrintStub("EditBookTitle — Se implementará con Service en EV08");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditBookAuthor()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("👤", "EDITAR AUTOR");
        ConsoleHelper.PrintStub("EditBookAuthor — Se implementará con Service en EV08");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditBookYearCategory()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📅", "EDITAR AÑO / CATEGORÍA");
        ConsoleHelper.PrintStub("EditBookYearCategory — Se implementará con Service en EV08");
        ConsoleHelper.PressAnyKey();
    }

    private static void DeleteBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR LIBRO");
        ConsoleHelper.PrintStub("DeleteBook — Se implementará con Service en EV08");
        ConsoleHelper.PrintWarning("Validación: No permitir si tiene préstamo activo.");
        ConsoleHelper.PressAnyKey();
    }
}
