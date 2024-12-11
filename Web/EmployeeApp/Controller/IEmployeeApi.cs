using Refit;

using CommonLibrary.Dto;

public interface IEmployeeApi
{
    [Get("/api/employees/juniors")]
    Task<List<EmployeeDto>> GetJuniors();

    [Get("/api/employees/teamleads")]
    Task<List<EmployeeDto>> GetTeamLeads();
}
