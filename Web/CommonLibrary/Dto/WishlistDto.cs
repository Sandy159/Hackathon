using CommonLibrary.Contracts;

namespace CommonLibrary.Dto
    {
    public class WishlistDto
    {
        public int WishlistPk { get; set; }
        public required EmployeeDto Employee { get; set; }
        public required int[] Preferences { get; set; }
        public required CompitionDto Hackathon { get; set; }
    }
}