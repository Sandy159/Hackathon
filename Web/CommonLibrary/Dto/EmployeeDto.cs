using CommonLibrary.Contracts;

namespace CommonLibrary.Dto
{
    public class EmployeeDto
    {
        public int EmployeePk { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}