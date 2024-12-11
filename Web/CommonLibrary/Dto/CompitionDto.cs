using CommonLibrary.Contracts;

namespace CommonLibrary.Dto
{
    public class CompitionDto
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public List<WishlistDto> Wishlists { get; set; }
        public List<TeamDto> Teams { get; set; }
    }
}