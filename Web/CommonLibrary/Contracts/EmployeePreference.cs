using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Contracts;

public class EmployeePreference
{
    public int Id { get; set; } // Первичный ключ

    public long EmployeeId { get; set; } // Идентификатор сотрудника

    public required string Role { get; set; } // Роль сотрудника (Junior или TeamLead)

    required public string Preferences { get; set;}// Список предпочтений в формате JSON
}
