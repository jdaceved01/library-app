namespace LibraryApp.Models;

/// <summary>
/// Representa un usuario registrado en el sistema.
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public User(int id, string name, string document, string email, string phone)
    {
        Id = id;
        Name = name;
        Document = document;
        Email = email;
        Phone = phone;
        IsActive = true; // todo usuario empieza activo
    }

    public User()
    {
        IsActive = true;
    }

    public string ShortSummary() =>
        $"[{Id:D3}] {Name} | Doc: {Document} | {Email} | {(IsActive ? "🟢 Activo" : "🔴 Inactivo")}";

    public string FullDetail()
    {
        return $"  ┌────────────────────────────────────────────────\n"
            + $"  │  👤  DETALLE DEL USUARIO\n"
            + $"  ├────────────────────────────────────────────────\n"
            + $"  │  ID        : {Id:D3}\n"
            + $"  │  Nombre    : {Name}\n"
            + $"  │  Documento : {Document}\n"
            + $"  │  Email     : {Email}\n"
            + $"  │  Teléfono  : {Phone}\n"
            + $"  │  Estado    : {(IsActive ? "🟢 Activo" : "🔴 Inactivo")}\n"
            + $"  └────────────────────────────────────────────────";
    }

    public override string ToString() => ShortSummary();
}
