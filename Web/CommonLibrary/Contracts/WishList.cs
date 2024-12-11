namespace CommonLibrary.Contracts;

public class Wishlist
{
    public int EmployeeId { get; }
    public int[] Preferences { get; }

    public Wishlist(int employeeId, int[] preferences)
    {
        EmployeeId = employeeId;
        Preferences = preferences;
    }
}
