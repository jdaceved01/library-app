namespace LibraryApp.UI;

/// <summary>
/// Utilidades de consola: colores, títulos, separadores, inputs.
/// </summary>
public static class ConsoleHelper
{
    // ── Paleta de colores ──────────────────────────────────────────────────
    public static readonly ConsoleColor ColorPrimary = ConsoleColor.Cyan;
    public static readonly ConsoleColor ColorSecondary = ConsoleColor.DarkCyan;
    public static readonly ConsoleColor ColorAccent = ConsoleColor.Yellow;
    public static readonly ConsoleColor ColorSuccess = ConsoleColor.Green;
    public static readonly ConsoleColor ColorWarning = ConsoleColor.DarkYellow;
    public static readonly ConsoleColor ColorError = ConsoleColor.Red;
    public static readonly ConsoleColor ColorMuted = ConsoleColor.DarkGray;
    public static readonly ConsoleColor ColorText = ConsoleColor.White;

    // ── Header principal ───────────────────────────────────────────────────
    public static void PrintAppHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ColorPrimary;
        Console.WriteLine();
        Console.WriteLine("  ╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("  ║                                                          ║");
        Console.WriteLine("  ║   📚  L I B R A R Y   A P P                             ║");
        Console.WriteLine("  ║       Sistema de Gestión de Biblioteca                   ║");
        Console.WriteLine("  ║                                                          ║");
        Console.WriteLine("  ╚══════════════════════════════════════════════════════════╝");
        Console.ForegroundColor = ColorMuted;
        Console.WriteLine($"  Versión 1.0  •  {DateTime.Now:dddd, dd MMMM yyyy  HH:mm}");
        Console.WriteLine();
        Console.ResetColor();
    }

    // ── Título de sección ──────────────────────────────────────────────────
    public static void PrintSectionHeader(string emoji, string title, string subtitle = "")
    {
        Console.WriteLine();
        Console.ForegroundColor = ColorPrimary;
        Console.WriteLine("  ┌─────────────────────────────────────────────────────────┐");
        Console.WriteLine($"  │  {emoji}  {title, -52} │");
        if (!string.IsNullOrEmpty(subtitle))
        {
            Console.ForegroundColor = ColorMuted;
            Console.WriteLine($"  │     {subtitle, -53} │");
        }
        Console.ForegroundColor = ColorPrimary;
        Console.WriteLine("  └─────────────────────────────────────────────────────────┘");
        Console.ResetColor();
        Console.WriteLine();
    }

    // ── Opción de menú ─────────────────────────────────────────────────────
    public static void PrintMenuOption(string key, string emoji, string label)
    {
        Console.ForegroundColor = ColorAccent;
        Console.Write($"     [{key}]");
        Console.ForegroundColor = ColorText;
        Console.WriteLine($"  {emoji}  {label}");
        Console.ResetColor();
    }

    // ── Separador ─────────────────────────────────────────────────────────
    public static void PrintSeparator()
    {
        Console.ForegroundColor = ColorMuted;
        Console.WriteLine("  ─────────────────────────────────────────────────────────");
        Console.ResetColor();
    }

    // ── Mensajes de estado ─────────────────────────────────────────────────
    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ColorSuccess;
        Console.WriteLine($"  ✅  {message}");
        Console.ResetColor();
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ColorError;
        Console.WriteLine($"  ❌  {message}");
        Console.ResetColor();
    }

    public static void PrintWarning(string message)
    {
        Console.ForegroundColor = ColorWarning;
        Console.WriteLine($"  ⚠️   {message}");
        Console.ResetColor();
    }

    public static void PrintInfo(string message)
    {
        Console.ForegroundColor = ColorPrimary;
        Console.WriteLine($"  ℹ️   {message}");
        Console.ResetColor();
    }

    public static void PrintStub(string actionName)
    {
        Console.WriteLine();
        Console.ForegroundColor = ColorSecondary;
        Console.WriteLine($"  ┌── Acción: {actionName}");
        Console.ForegroundColor = ColorMuted;
        Console.WriteLine(
            $"  │   [Stub] Esta función está registrada y será implementada en la siguiente fase."
        );
        Console.ForegroundColor = ColorSecondary;
        Console.WriteLine($"  └─────────────────────────────────────────────────────");
        Console.ResetColor();
    }

    // ── Input ──────────────────────────────────────────────────────────────
    public static void PrintPrompt(string label)
    {
        Console.WriteLine();
        Console.ForegroundColor = ColorAccent;
        Console.Write($"  ▶  {label}: ");
        Console.ForegroundColor = ColorText;
    }

    public static void PressAnyKey()
    {
        Console.WriteLine();
        Console.ForegroundColor = ColorMuted;
        Console.Write("  Presiona cualquier tecla para continuar...");
        Console.ResetColor();
        Console.ReadKey(true);
        Console.WriteLine();
    }

    // ── Confirmación S/N ───────────────────────────────────────────────────
    public static bool AskConfirmation(string question)
    {
        while (true)
        {
            Console.WriteLine();
            Console.ForegroundColor = ColorWarning;
            Console.Write($"  ⚠️   {question} (S/N): ");
            Console.ForegroundColor = ColorText;
            string? input = Console.ReadLine()?.Trim().ToUpper();
            if (input == "S")
                return true;
            if (input == "N")
                return false;
            PrintError("Opción inválida. Escribe S o N.");
        }
    }

    // ── Leer entero válido ─────────────────────────────────────────────────
    public static int ReadInt(int min, int max)
    {
        while (true)
        {
            string? raw = Console.ReadLine()?.Trim();
            if (int.TryParse(raw, out int value) && value >= min && value <= max)
                return value;
            Console.ForegroundColor = ColorError;
            Console.Write($"  ❌  Opción inválida. Ingresa un número entre {min} y {max}: ");
            Console.ForegroundColor = ColorText;
        }
    }

    // ── Footer del menú ────────────────────────────────────────────────────
    public static void PrintBackOption()
    {
        PrintSeparator();
        Console.ForegroundColor = ColorMuted;
        Console.WriteLine("     [0]  ↩️   Volver al menú anterior");
        Console.ResetColor();
    }
}
