namespace LibraryApp.Services;

/// <summary>
/// Genera IDs auto-incrementales para cada entidad.
/// </summary>
public static class IdGenerator
{
    private static int _bookId = 0;
    private static int _userId = 0;
    private static int _loanId = 0;

    public static int NextBookId() => ++_bookId;

    public static int NextUserId() => ++_userId;

    public static int NextLoanId() => ++_loanId;

    public static void SetCounters(int maxBookId, int maxUserId, int maxLoanId)
    {
        _bookId = maxBookId;
        _userId = maxUserId;
        _loanId = maxLoanId;
    }
}
