using System.Threading.Tasks;
using CommonLibrary.Dto;
using Refit;

namespace HRManagerApp.RefitClients
{
    public interface IHRDirectorApi
    {
        [Post("/api/hrdirector/calculate-harmony")]
        Task SendFinalDistributionAsync([Body] TeamDistributionDto preferencesEntity);
    }
}
