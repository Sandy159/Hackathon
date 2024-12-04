using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class EmployeeDto
    {
        public int EmployeePk { get; set; }
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Role { get; set; }
    }
}