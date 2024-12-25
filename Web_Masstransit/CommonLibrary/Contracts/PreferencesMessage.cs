namespace CommonLibrary.Contracts;

public class PreferencesMessage
{
    public int HackathonId { get; set; }
    public int EmployeeId { get; set; }
    public required string Role { get; set; }
    public required int[] Preferences { get; set; }
}
