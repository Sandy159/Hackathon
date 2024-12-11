using CommonLibrary.Contracts;

namespace HRManagerApp.Service
{
    public interface IDistributionService
    {
        void SaveJuniorPreferences(Employee employee, Wishlist wishlist);
        void SaveTeamLeadPreferences(Employee employee, Wishlist wishlist);
    }
}
