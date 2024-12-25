using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Contracts;

public class EmployeePreference
{
    public int HackathonId { get; set; } // Первичный ключ

    public int EmployeeId { get; set; } // Идентификатор сотрудника

    public required string Role { get; set; } // Роль сотрудника (Junior или TeamLead)

    required public string Preferences { get; set;}// Список предпочтений в формате JSON
}
