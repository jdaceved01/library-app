namespace LibraryApp.UI;

/// <summary>
/// Submenú de gestión de libros.
/// </summary>
public static class BooksMenu
{
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

    // ── Funciones stub ────────────────────────────────────────────────────

    private static void RegisterBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("➕", "REGISTRAR LIBRO");
        ConsoleHelper.PrintStub("RegisterBook — Registrar un nuevo libro en el catálogo");
        ConsoleHelper.PrintInfo(
            "Se solicitarán: Título, Autor, ISBN, Año, Categoría, Cantidad disponible."
        );
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader(
                "📋",
                "LISTAR LIBROS",
                "Elige el filtro de visualización"
            );

            ConsoleHelper.PrintMenuOption("1", "📚", "Listar todos los libros");
            ConsoleHelper.PrintMenuOption("2", "✅", "Listar libros disponibles");
            ConsoleHelper.PrintMenuOption("3", "🔄", "Listar libros prestados");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int option = ConsoleHelper.ReadInt(0, 3);

            switch (option)
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
        ConsoleHelper.PrintSectionHeader("📚", "TODOS LOS LIBROS");
        ConsoleHelper.PrintStub("ListBooksAll — Lista completa del catálogo");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksAvailable()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("✅", "LIBROS DISPONIBLES");
        ConsoleHelper.PrintStub("ListBooksAvailable — Libros sin préstamo activo");
        ConsoleHelper.PressAnyKey();
    }

    private static void ListBooksBorrowed()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔄", "LIBROS PRESTADOS");
        ConsoleHelper.PrintStub("ListBooksBorrowed — Libros con préstamo activo");
        ConsoleHelper.PressAnyKey();
    }

    private static void ViewBookDetail()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🔎", "VER DETALLE DE LIBRO");
        ConsoleHelper.PrintStub("ViewBookDetail — Busca por ID o ISBN y muestra detalle completo");
        ConsoleHelper.PressAnyKey();
    }

    private static void UpdateBookMenu()
    {
        bool running = true;
        while (running)
        {
            ConsoleHelper.PrintAppHeader();
            ConsoleHelper.PrintSectionHeader("✏️", "ACTUALIZAR LIBRO", "¿Qué campo deseas editar?");

            ConsoleHelper.PrintMenuOption("1", "📝", "Editar título");
            ConsoleHelper.PrintMenuOption("2", "👤", "Editar autor");
            ConsoleHelper.PrintMenuOption("3", "📅", "Editar año / categoría");
            ConsoleHelper.PrintBackOption();

            ConsoleHelper.PrintPrompt("Selecciona una opción [0-3]");
            int option = ConsoleHelper.ReadInt(0, 3);

            switch (option)
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
        ConsoleHelper.PrintStub("EditBookTitle — Actualiza el título de un libro por ID");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditBookAuthor()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("👤", "EDITAR AUTOR");
        ConsoleHelper.PrintStub("EditBookAuthor — Actualiza el autor de un libro por ID");
        ConsoleHelper.PressAnyKey();
    }

    private static void EditBookYearCategory()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("📅", "EDITAR AÑO / CATEGORÍA");
        ConsoleHelper.PrintStub("EditBookYearCategory — Actualiza año y categoría por ID");
        ConsoleHelper.PressAnyKey();
    }

    private static void DeleteBook()
    {
        ConsoleHelper.PrintAppHeader();
        ConsoleHelper.PrintSectionHeader("🗑️", "ELIMINAR LIBRO");
        ConsoleHelper.PrintStub("DeleteBook — Elimina libro por ID");
        ConsoleHelper.PrintWarning(
            "Validación: No se permite eliminar si el libro tiene préstamo activo."
        );
        ConsoleHelper.PressAnyKey();
    }
}
