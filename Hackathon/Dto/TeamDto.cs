using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class TeamDto
    {
        public int TeamPk { get; set; }
        public required EmployeeDto Junior { get; set; }
        public required EmployeeDto TeamLead { get; set; }
        public required CompitionDto Hackathon { get; set; }
    }
}