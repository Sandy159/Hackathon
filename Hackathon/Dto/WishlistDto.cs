using Nsu.HackathonProblem.Contracts;

namespace Hackathon
    {
    public class WishlistDto
    {
        public int WishlistPk { get; set; }
        public required EmployeeDto Employee { get; set; }
        public required int[] DesiredEmployees { get; set; }
        public required CompitionDto Hackathon { get; set; }
    }
}