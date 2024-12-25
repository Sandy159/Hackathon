using CommonLibrary.Contracts;

namespace HRManagerApp.Strategy
{
    public interface ITeamBuildingStrategy
    {
        /// <summary>
        /// Распределяет тиилидов и джунов по командам
        /// </summary>
        /// <param name="teamLeads">Тимлиды</param>
        /// <param name="juniors">Джуны</param>
        /// <returns>Список команд</returns>
        IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<PreferencesMessage> teamLeadsWishlists, IEnumerable<PreferencesMessage> juniorsWishlists);
    }
}