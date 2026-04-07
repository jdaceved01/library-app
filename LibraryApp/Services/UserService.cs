using LibraryApp.Models;

namespace LibraryApp.Services;

public class UserService
{
    private readonly List<User> _users = new();

    public void Add(User user)
    {
        user.Id = IdGenerator.NextUserId();
        _users.Add(user);
    }

    public bool Remove(int id, bool hasActiveLoans)
    {
        if (hasActiveLoans)
            return false;
        var user = FindById(id);
        if (user == null)
            return false;
        _users.Remove(user);
        return true;
    }

    public List<User> GetAll() => new(_users);

    public List<User> GetActive() => _users.FindAll(u => u.IsActive);

    public List<User> GetInactive() => _users.FindAll(u => !u.IsActive);

    public User? FindById(int id) => _users.Find(u => u.Id == id);

    public User? FindByDocument(string doc) => _users.Find(u => u.Document == doc);

    public List<User> Search(string term)
    {
        string t = term.ToLower();
        return _users.FindAll(u =>
            u.Name.ToLower().Contains(t) || u.Document.Contains(t) || u.Email.ToLower().Contains(t)
        );
    }

    public List<User> GetSortedByName() => _users.OrderBy(u => u.Name).ToList();

    public bool UpdateName(int id, string newName)
    {
        var u = FindById(id);
        if (u == null)
            return false;
        u.Name = newName;
        return true;
    }

    public bool UpdateContact(int id, string email, string phone)
    {
        var u = FindById(id);
        if (u == null)
            return false;
        u.Email = email;
        u.Phone = phone;
        return true;
    }

    public bool ToggleActive(int id)
    {
        var u = FindById(id);
        if (u == null)
            return false;
        u.IsActive = !u.IsActive;
        return true;
    }

    // KPIs
    public int TotalUsers() => _users.Count;

    public int TotalActive() => _users.Count(u => u.IsActive);

    public int TotalInactive() => _users.Count(u => !u.IsActive);

    public void SeedData()
    {
        if (_users.Count > 0)
            return;
        Add(
            new User
            {
                Name = "Ana Martínez",
                Document = "1001234567",
                Email = "ana@email.com",
                Phone = "311-111-1111",
            }
        );
        Add(
            new User
            {
                Name = "Carlos Pérez",
                Document = "1007654321",
                Email = "carlos@email.com",
                Phone = "322-222-2222",
            }
        );
        Add(
            new User
            {
                Name = "Laura Gómez",
                Document = "1009876543",
                Email = "laura@email.com",
                Phone = "333-333-3333",
            }
        );
    }

    public List<User> GetRaw() => _users;

    public void LoadFrom(IEnumerable<User> users)
    {
        _users.Clear();
        _users.AddRange(users);
    }
}
