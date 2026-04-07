using LibraryApp.Models;

namespace LibraryApp.Services;

public class LoanService
{
    private readonly List<Loan> _loans = new();

    public void Add(Loan loan)
    {
        loan.Id = IdGenerator.NextLoanId();
        _loans.Add(loan);
    }

    public bool Delete(int id)
    {
        var loan = FindById(id);
        if (loan == null || loan.Status == LoanStatus.Active)
            return false;
        _loans.Remove(loan);
        return true;
    }

    public List<Loan> GetAll() => new(_loans);

    public List<Loan> GetActive() => _loans.FindAll(l => l.Status == LoanStatus.Active);

    public List<Loan> GetClosed() => _loans.FindAll(l => l.Status != LoanStatus.Active);

    public List<Loan> GetOverdue() =>
        _loans.FindAll(l => l.Status == LoanStatus.Active && l.IsOverdue());

    public Loan? FindById(int id) => _loans.Find(l => l.Id == id);

    public List<Loan> GetByUser(int userId) => _loans.FindAll(l => l.UserId == userId);

    public List<Loan> GetByBook(int bookId) => _loans.FindAll(l => l.BookId == bookId);

    public bool HasActiveLoans(int userId) =>
        _loans.Exists(l => l.UserId == userId && l.Status == LoanStatus.Active);

    public bool RegisterReturn(int loanId)
    {
        var loan = FindById(loanId);
        if (loan == null || loan.Status != LoanStatus.Active)
            return false;
        loan.Status = LoanStatus.Returned;
        loan.ReturnDate = DateTime.Now;
        return true;
    }

    public List<Loan> GetSortedByDueDate() => _loans.OrderBy(l => l.DueDate).ToList();

    // KPIs
    public int TotalLoans() => _loans.Count;

    public int TotalActive() => _loans.Count(l => l.Status == LoanStatus.Active);

    public int TotalReturned() => _loans.Count(l => l.Status == LoanStatus.Returned);

    public int TotalOverdue() => _loans.Count(l => l.Status == LoanStatus.Active && l.IsOverdue());

    public double AverageLoanDays()
    {
        var closed = _loans
            .Where(l => l.Status == LoanStatus.Returned && l.ReturnDate.HasValue)
            .ToList();
        if (closed.Count == 0)
            return 0;
        return closed.Average(l => (l.ReturnDate!.Value - l.StartDate).TotalDays);
    }

    // Actualizar estado de vencidos automáticamente
    public void RefreshOverdueStatus()
    {
        foreach (var loan in _loans.Where(l => l.Status == LoanStatus.Active && l.IsOverdue()))
            loan.Status = LoanStatus.Overdue;
    }

    public List<Loan> GetRaw() => _loans;

    public void LoadFrom(IEnumerable<Loan> loans)
    {
        _loans.Clear();
        _loans.AddRange(loans);
    }
}
