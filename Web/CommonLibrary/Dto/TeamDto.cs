using CommonLibrary.Contracts;

namespace CommonLibrary.Dto
{
    public class TeamDto
    {
        public int TeamPk { get; set; }
        public EmployeeDto Junior { get; set; }
        public EmployeeDto TeamLead { get; set; }
        public CompitionDto Hackathon { get; set; }
    }
}