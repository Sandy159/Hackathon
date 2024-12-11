using Refit;
using CommonLibrary.Dto;

namespace HRDirector.Controller
{
    public interface IHRDirectorApi
    {
        [Post("/api/director/calculate-harmony")]
        Task CalculateHarmony([Body] HarmonyCalculationRequest request);
    }

    public class HarmonyCalculationRequest
    {
        public int HackathonId { get; set; }
        public List<TeamDto> Teams { get; set; }
    }
}