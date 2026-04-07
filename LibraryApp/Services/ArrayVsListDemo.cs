namespace LibraryApp.Services;

/// <summary>
/// EV08 — Comparación obligatoria: Array vs List&lt;T&gt;
/// Demuestra las diferencias fundamentales entre ambas estructuras.
/// </summary>
public static class ArrayVsListDemo
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("\n  ╔══ COMPARACIÓN: Array vs List<T> ══════════════════╗");

        // ── ARRAY ─────────────────────────────────────────────────────
        Console.WriteLine("\n  [ARRAY] — Tamaño fijo, acceso por índice");
        string[] bookTitlesArray = new string[3];
        bookTitlesArray[0] = "Cien Años de Soledad";
        bookTitlesArray[1] = "El Principito";
        bookTitlesArray[2] = "1984";
        // bookTitlesArray[3] = "Nuevo"; ← ERROR en tiempo de ejecución
        Console.WriteLine($"  Tamaño fijo: {bookTitlesArray.Length} elementos");
        foreach (var t in bookTitlesArray)
            Console.WriteLine($"    - {t}");

        // ── LIST<T> ────────────────────────────────────────────────────
        Console.WriteLine("\n  [List<T>] — Tamaño dinámico, más métodos");
        var bookTitlesList = new List<string>();
        bookTitlesList.Add("Cien Años de Soledad");
        bookTitlesList.Add("El Principito");
        bookTitlesList.Add("1984");
        bookTitlesList.Add("Clean Code"); // Se puede agregar sin límite
        bookTitlesList.Remove("El Principito"); // Se puede eliminar fácilmente
        Console.WriteLine($"  Tamaño dinámico: {bookTitlesList.Count} elementos");
        foreach (var t in bookTitlesList)
            Console.WriteLine($"    - {t}");

        Console.WriteLine("\n  ╔══ CONCLUSIÓN ════════════════════════════════════╗");
        Console.WriteLine("  ║  Array : tamaño fijo, más eficiente en memoria  ║");
        Console.WriteLine("  ║  List  : tamaño dinámico, más flexible y útil   ║");
        Console.WriteLine("  ╚═════════════════════════════════════════════════╝\n");
    }
}
