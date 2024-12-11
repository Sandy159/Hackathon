using Refit;

using CommonLibrary.Dto;

namespace Web
{
    public interface IHRManagerApi
    {
        [Post("/api/manager/assign-teams")]
        Task AssignTeams([Body] AssignTeamsRequest request);

        [Get("/api/manager/wishlist")]
        Task<List<WishlistDto>> GetAllWishlists();
    }

    public class AssignTeamsRequest
    {
        public int HackathonId { get; set; }
    }
}
