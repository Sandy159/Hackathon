using CommonLibrary.Contracts;
using EmployeeApp.RefitClients;
using CommonLibrary.Utils;

namespace EmployeeApp.Service;

public class EmployeeService
{
    private readonly IHRManagerApi _hrManagerApi;

    public EmployeeService(IHRManagerApi hrManagerApi)
    {
        _hrManagerApi = hrManagerApi;
    }

    public async Task ProcessWishlistsAsync()
    {
        // Загружаем сотрудников
        var juniors = DataLoader.LoadEmployees("data/juniors.csv");
        var teamLeads = DataLoader.LoadEmployees("data/teamleads.csv");

        // Генерируем списки предпочтений
        var wishlists = WishlistGenerator.GenerateWishlist(juniors, teamLeads);

        // Отправляем списки предпочтений через HRManager API
        foreach (var wishlist in wishlists)
        {
            var preferencesMessage = new PreferencesMessage
            {
                EmployeeId = wishlist.EmployeeId,
                Preferences = wishlist.Preferences
            };

            await _hrManagerApi.SubmitPreferencesAsync(preferencesMessage);
        }
    }
}
