using System.Threading.Tasks;
using CommonLibrary.Contracts;
using Refit;

namespace HRManagerApp.RefitClients
{
    public interface IHRDirectorApi
    {
        [Post("/api/hrdirector/calculate-harmony")]
        Task EvaluateHarmonyAsync([Body] PreferencesAndTeamsResponse preferencesAndTeamsResponse);
    }
}
