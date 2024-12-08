using Nsu.HackathonProblem.Contracts;

namespace Hackathon
{
    public class TeamDto
    {
        public int TeamPk { get; set; }
        public EmployeeDto Junior { get; set; }
        public EmployeeDto TeamLead { get; set; }
        public CompitionDto Hackathon { get; set; }
    }
}