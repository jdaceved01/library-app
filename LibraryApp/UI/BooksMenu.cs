using LibraryApp.Models;
using LibraryApp.Services;

namespace LibraryApp.UI;

public static class BooksMenu
{
    private static BookService _service = null!;

    public static void Init(BookService service) => _service = service;

    public static void Show()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "📖",
                "GESTIÓN DE LIBROS",
                $"Catálogo: {_service.TotalBooks()} libros  |  ✅ {_service.TotalAvailable()} disponibles  |  🔄 {_service.TotalBorrowed()} prestados"
            );

            ConsoleHelper.PrintMenuOption("1", "➕", "Registrar libro");
            ConsoleHelper.PrintMenuOption("2", "📋", "Listar libros");
            ConsoleHelper.PrintMenuOption("3", "🔎", "Ver detalle (por ID)");
            ConsoleHelper.PrintMenuOption("4", "✏️ ", "Actualizar libro");
            ConsoleHelper.PrintMenuOption("5", "🗑️ ", "Eliminar libro");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int opt = ConsoleHelper.ReadInt(0, 5);
            switch (opt)
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
        ConsoleHelper.PrintSectionHeader("➕", "REGISTRAR NUEVO LIBRO");

        ConsoleHelper.PrintPrompt("Título");
        string title = Console.ReadLine() ?? "";
        ConsoleHelper.PrintPrompt("Autor");
        string author = Console.ReadLine() ?? "";
        ConsoleHelper.PrintPrompt("ISBN");
        string isbn = Console.ReadLine() ?? "";

        ConsoleHelper.PrintPrompt("Año de publicación");
        int.TryParse(Console.ReadLine(), out int year);

        ConsoleHelper.PrintPrompt("Categoría");
        string category = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            ConsoleHelper.PrintError("Título y autor son obligatorios.");
            ConsoleHelper.PressAnyKey();
            return;
        }

        var book = new Book
        {
            Title = title,
            Author = author,
            Isbn = isbn,
            Year = year,
            Category = category,
        };
        _service.Add(book);
        ConsoleHelper.PrintSuccess($"Libro registrado correctamente con ID [{book.Id:D3}].");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("📋", "LISTAR LIBROS");
            ConsoleHelper.PrintMenuOption("1", "📚", "Todos los libros");
            ConsoleHelper.PrintMenuOption("2", "✅", "Disponibles");
            ConsoleHelper.PrintMenuOption("3", "🔄", "Prestados");
            ConsoleHelper.PrintMenuOption("4", "🔤", "Ordenados por título");
            ConsoleHelper.PrintMenuOption("5", "📅", "Ordenados por año");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-5]");
            int opt = ConsoleHelper.ReadInt(0, 5);
            switch (opt)
            {
                case 1:
                    PrintBookList(_service.GetAll(), "TODOS LOS LIBROS");
                    break;
                case 2:
                    PrintBookList(
                        _service.GetAvailable(),
                        "LIBROS DISPONIBLES",
                        ConsoleColor.Green
                    );
                    break;
                case 3:
                    PrintBookList(_service.GetBorrowed(), "LIBROS PRESTADOS", ConsoleColor.Yellow);
                    break;
                case 4:
                    PrintBookList(_service.GetSortedByTitle(), "ORDENADOS POR TÍTULO");
                    break;
                case 5:
                    PrintBookList(_service.GetSortedByYear(), "ORDENADOS POR AÑO");
                    break;
                case 0:
                    running = false;
                    break;
            }
        }
    }

    private static void PrintBookList(
        List<Book> books,
        string title,
        ConsoleColor color = ConsoleColor.White
    )
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📚", title, $"Total: {books.Count}");
        if (books.Count == 0)
        {
            ConsoleHelper.PrintInfo("No hay libros en esta categoría.");
        }
        else
        {
            foreach (var b in books)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"  {b.ShortSummary()}");
            }
            Console.ResetColor();
        }
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewBookDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE LIBRO");
        ConsoleHelper.PrintPrompt("Ingresa el ID");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var book = _service.FindById(id);
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
        ConsoleHelper.PrintPrompt("ID del libro");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        ConsoleHelper.PrintPrompt("Nuevo título");
        string val = Console.ReadLine() ?? "";
        if (_service.UpdateTitle(id, val))
            ConsoleHelper.PrintSuccess("Título actualizado.");
        else
            ConsoleHelper.PrintError("No se encontró el libro.");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditBookAuthor()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("👤", "EDITAR AUTOR");
        ConsoleHelper.PrintPrompt("ID del libro");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        ConsoleHelper.PrintPrompt("Nuevo autor");
        string val = Console.ReadLine() ?? "";
        if (_service.UpdateAuthor(id, val))
            ConsoleHelper.PrintSuccess("Autor actualizado.");
        else
            ConsoleHelper.PrintError("No se encontró el libro.");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditBookYearCategory()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📅", "EDITAR AÑO / CATEGORÍA");
        ConsoleHelper.PrintPrompt("ID del libro");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        ConsoleHelper.PrintPrompt("Nuevo año");
        int.TryParse(Console.ReadLine(), out int year);
        ConsoleHelper.PrintPrompt("Nueva categoría");
        string cat = Console.ReadLine() ?? "";
        if (_service.UpdateYearCategory(id, year, cat))
            ConsoleHelper.PrintSuccess("Año y categoría actualizados.");
        else
            ConsoleHelper.PrintError("No se encontró el libro.");
        ConsoleHelper.PressAnyKey();
    }

    private static void DeleteBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR LIBRO");
        ConsoleHelper.PrintPrompt("ID del libro a eliminar");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.PrintError("ID inválido.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        var book = _service.FindById(id);
        if (book == null)
        {
            ConsoleHelper.PrintError("Libro no encontrado.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        if (!book.IsAvailable)
        {
            ConsoleHelper.PrintError("No se puede eliminar: el libro está prestado actualmente.");
            ConsoleHelper.PressAnyKey();
            return;
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(book.FullDetail());
        Console.ResetColor();
        if (ConsoleHelper.AskConfirmation("¿Confirmas eliminar este libro?"))
        {
            _service.Remove(id);
            ConsoleHelper.PrintSuccess("Libro eliminado correctamente.");
        }
        else
            ConsoleHelper.PrintInfo("Operación cancelada.");
        ConsoleHelper.PressAnyKey();
    }
}
