using CommonLibrary.Contracts;
using Refit;

namespace EmployeeApp.RefitClients;

public interface IHRManagerApi
{
    [Post("/api/hr/submit-preferences")]
    Task SubmitPreferencesAsync([Body] PreferencesMessage preferencesMessage);
}
